using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Connection;
using Guilded.Events;
using Guilded.Json;
using Guilded.Users;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Websocket.Client;

namespace Guilded.Client;

/// <summary>
/// Represents the base for all Guilded clients.
/// </summary>
/// <remarks>
/// <para>There is not much to be used here. It is recommended to use <see cref="GuildedBotClient" />.</para>
/// </remarks>
/// <seealso cref="GuildedBotClient" />
/// <seealso cref="BaseGuildedConnection" />
/// <seealso cref="BaseGuildedService" />
public abstract partial class AbstractGuildedClient : BaseGuildedConnection
{
    #region Fields
    private readonly Subject<Exception> _onWebsocketEventError = new();

    /// <summary>
    /// Gets an <see cref="IObservable{T}">observable</see> for the event that occurs once Guilded client has connected and added finishing touches.
    /// </summary>
    /// <returns>Prepared subject</returns>
    protected Subject<ClientUser> PreparedSubject { get; } = new();
    #endregion

    #region Properties
    /// <inheritdoc cref="WelcomeEvent.User" />
    public ClientUser? Me { get; protected set; }

    /// <inheritdoc cref="PreparedSubject" />
    public IObservable<ClientUser> Prepared => PreparedSubject.AsObservable();

    /// <summary>
    /// Gets an <see cref="IObservable{T}">observable</see> that is invoked when there is an <see cref="Exception">error</see> while handling an <see cref="GuildedEvents">event</see>.
    /// </summary>
    public IObservable<Exception> WebsocketEventError => _onWebsocketEventError.AsObservable();

    /// <summary>
    /// Gets whether the client is <see cref="Prepared">prepared</see>.
    /// </summary>
    /// <value>Whether the client is <see cref="Prepared">prepared</see></value>
    public bool IsPrepared { get; protected set; }

    /// <inheritdoc cref="UserSummary.Id" />
    public HashId? Id => Me?.Id;

    /// <inheritdoc cref="ClientUser.BotId" />
    public Guid? BotId => Me?.BotId;

    /// <inheritdoc cref="ClientUser.CreatedBy" />
    public HashId? CreatedBy => Me?.CreatedBy;

    /// <inheritdoc cref="User.CreatedAt" />
    public DateTime? CreatedAt => Me?.CreatedAt;

    /// <inheritdoc cref="UserSummary.Name" />
    public string? Name => Me?.Name;

    /// <inheritdoc cref="UserSummary.Avatar" />
    public Uri? Avatar => Me?.Avatar;

    /// <inheritdoc cref="User.Banner" />
    public Uri? Banner => Me?.Banner;
    #endregion

    #region Properties Events
    /// <summary>
    /// Gets the dictionary of Guilded events, their names and other event information.
    /// </summary>
    /// <remarks>
    /// <para>You can add more events to this dictionary if Guilded.NET does not support certain events.</para>
    /// </remarks>
    /// <value>Dictionary of events</value>
    /// <seealso cref="Welcome" />
    /// <seealso cref="Resume" />
    /// <seealso cref="MessageCreated" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="ChannelCreated" />
    /// <seealso cref="ItemCreated" />
    /// <seealso cref="DocCreated" />
    protected Dictionary<object, IEventInfo<object>> GuildedEvents { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new base instance of <see cref="AbstractGuildedClient" /> children types.
    /// </summary>
    /// <seealso cref="GuildedBotClient()" />
    /// <seealso cref="GuildedBotClient(string)" />
    protected AbstractGuildedClient()
    {
        SerializerSettings.Converters.Add(new ServerChannelConverter());
        SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

        #region Event list
        // Dictionary of supported events, so we wouldn't need to manually do it.
        // The only manual work to be done is in AbstractGuildedClient.Messages.cs file,
        // which only allows us to subscribe to events and it is literally +1 member
        // to be added and copy pasting for the most part.
        GuildedEvents = new Dictionary<object, IEventInfo<object>>
        {
            // Event messages
            { SocketOpcode.Welcome,                   new EventInfo<WelcomeEvent>() },
            { SocketOpcode.Resume,                    new EventInfo<ResumeEvent>() },

            // Server events
            { "BotServerMembershipCreated",           new EventInfo<ServerAddedEvent>() },
            { "BotServerMembershipDeleted",           new EventInfo<ServerRemovedEvent>() },

            { "ServerMemberBanned",
                new EventInfo<MemberBanEvent>((type, serializer, message) =>
                {
                    // Add `serverId` to memberBan
                    JObject data = message.RawData!;
                    JToken? serverId = data["serverId"];
                    JObject? ban = data["serverMemberBan"] as JObject;
                    ban?.Add("serverId", serverId);

                    // Transform modified value
                    return data.ToObject(type, serializer)!;
                })
            },
            { "ServerMemberUnbanned",                 new EventInfo<MemberBanEvent>() },
            { "ServerMemberRemoved",                  new EventInfo<MemberRemovedEvent>() },
            { "ServerMemberUpdated",                  new EventInfo<MemberUpdatedEvent>() },

            { "ServerRolesUpdated",                   new EventInfo<RolesUpdatedEvent>() },
            { "ServerMemberJoined",
                new EventInfo<MemberJoinedEvent>((type, serializer, message) =>
                {
                    // Add `serverId` to member
                    JObject data = message.RawData!;
                    JToken? serverId = data["serverId"];
                    JObject? member = data["member"] as JObject;
                    member?.Add("serverId", serverId);

                    // Transform modified value
                    return data.ToObject(type, serializer)!;
                })
            },

            // Global user changes
            { "ServerMemberSocialLinkCreated",            new EventInfo<MemberSocialLinkEvent>() },
            { "ServerMemberSocialLinkUpdated",            new EventInfo<MemberSocialLinkEvent>() },
            { "ServerMemberSocialLinkDeleted",            new EventInfo<MemberSocialLinkEvent>() },
            { "UserStatusCreated",                        new EventInfo<UserStatusEvent>() },
            { "UserStatusDeleted",                        new EventInfo<UserStatusEvent>() },

            // Groups
            { "GroupCreated",                             new EventInfo<GroupEvent>() },
            { "GroupUpdated",                             new EventInfo<GroupEvent>() },
            { "GroupDeleted",                             new EventInfo<GroupEvent>() },

            // Groups
            { "RoleCreated",                              new EventInfo<RoleEvent>() },
            { "RoleUpdated",                              new EventInfo<RoleEvent>() },
            { "RoleDeleted",                              new EventInfo<RoleEvent>() },

            // Categories
            { "CategoryCreated",                          new EventInfo<CategoryEvent>() },
            { "CategoryUpdated",                          new EventInfo<CategoryEvent>() },
            { "CategoryDeleted",                          new EventInfo<CategoryEvent>() },

            // Channels
            { "ServerChannelCreated",                     new EventInfo<ChannelEvent>() },
            { "ServerChannelUpdated",                     new EventInfo<ChannelEvent>() },
            { "ServerChannelDeleted",                     new EventInfo<ChannelEvent>() },
            { "ServerWebhookCreated",                     new EventInfo<WebhookEvent>() },
            { "ServerWebhookUpdated",                     new EventInfo<WebhookEvent>() },

            // Permissions
            { "ChannelRolePermissionCreated",             new EventInfo<ChannelRolePermissionEvent>() },
            { "ChannelRolePermissionUpdated",             new EventInfo<ChannelRolePermissionEvent>() },
            { "ChannelRolePermissionDeleted",             new EventInfo<ChannelRolePermissionEvent>() },

            { "ChannelUserPermissionCreated",             new EventInfo<ChannelUserPermissionEvent>() },
            { "ChannelUserPermissionUpdated",             new EventInfo<ChannelUserPermissionEvent>() },
            { "ChannelUserPermissionDeleted",             new EventInfo<ChannelUserPermissionEvent>() },

            { "ChannelCategoryRolePermissionCreated",     new EventInfo<CategoryRolePermissionEvent>() },
            { "ChannelCategoryRolePermissionUpdated",     new EventInfo<CategoryRolePermissionEvent>() },
            { "ChannelCategoryRolePermissionDeleted",     new EventInfo<CategoryRolePermissionEvent>() },

            { "ChannelCategoryUserPermissionCreated",     new EventInfo<CategoryUserPermissionEvent>() },
            { "ChannelCategoryUserPermissionUpdated",     new EventInfo<CategoryUserPermissionEvent>() },
            { "ChannelCategoryUserPermissionDeleted",     new EventInfo<CategoryUserPermissionEvent>() },

            // Chat messages
            { "ChatMessageCreated",                       new EventInfo<MessageEvent>() },
            { "ChatMessageUpdated",                       new EventInfo<MessageEvent>() },
            { "ChatMessageDeleted",                       new EventInfo<MessageDeletedEvent>() },
            { "ChannelMessagePinned",                     new EventInfo<MessageEvent>() },
            { "ChannelMessageUnpinned",                   new EventInfo<MessageEvent>() },
            { "ChannelMessageReactionCreated",            new EventInfo<MessageReactionEvent>() },
            { "ChannelMessageReactionDeleted",            new EventInfo<MessageReactionEvent>() },

            // Forum topics
            { "ForumTopicCreated",                        new EventInfo<TopicEvent>() },
            { "ForumTopicUpdated",                        new EventInfo<TopicEvent>() },
            { "ForumTopicDeleted",                        new EventInfo<TopicEvent>() },
            { "ForumTopicPinned",                         new EventInfo<TopicEvent>() },
            { "ForumTopicUnpinned",                       new EventInfo<TopicEvent>() },
            { "ForumTopicLocked",                         new EventInfo<TopicEvent>() },
            { "ForumTopicUnlocked",                       new EventInfo<TopicEvent>() },
            { "ForumTopicCommentCreated",                 new EventInfo<TopicCommentEvent>() },
            { "ForumTopicCommentUpdated",                 new EventInfo<TopicCommentEvent>() },
            { "ForumTopicCommentDeleted",                 new EventInfo<TopicCommentEvent>() },
            { "ForumTopicReactionCreated",                new EventInfo<TopicReactionEvent>() },
            { "ForumTopicReactionDeleted",                new EventInfo<TopicReactionEvent>() },
            { "ForumTopicCommentReactionCreated",         new EventInfo<TopicCommentReactionEvent>() },
            { "ForumTopicCommentReactionDeleted",         new EventInfo<TopicCommentReactionEvent>() },

            // List items
            { "ListItemCreated",                          new EventInfo<ItemEvent>() },
            { "ListItemUpdated",                          new EventInfo<ItemEvent>() },
            { "ListItemDeleted",                          new EventInfo<ItemEvent>() },
            { "ListItemCompleted",                        new EventInfo<ItemEvent>() },
            { "ListItemUncompleted",                      new EventInfo<ItemEvent>() },

            // Docs
            { "DocCreated",                               new EventInfo<DocEvent>() },
            { "DocUpdated",                               new EventInfo<DocEvent>() },
            { "DocDeleted",                               new EventInfo<DocEvent>() },
            { "DocCommentCreated",                        new EventInfo<DocCommentEvent>() },
            { "DocCommentUpdated",                        new EventInfo<DocCommentEvent>() },
            { "DocCommentDeleted",                        new EventInfo<DocCommentEvent>() },
            { "DocReactionCreated",                       new EventInfo<DocReactionEvent>() },
            { "DocReactionDeleted",                       new EventInfo<DocReactionEvent>() },
            { "DocCommentReactionCreated",                new EventInfo<DocCommentReactionEvent>() },
            { "DocCommentReactionDeleted",                new EventInfo<DocCommentReactionEvent>() },

            // Announcements
            { "AnnouncementCreated",                      new EventInfo<AnnouncementEvent>() },
            { "AnnouncementUpdated",                      new EventInfo<AnnouncementEvent>() },
            { "AnnouncementDeleted",                      new EventInfo<AnnouncementEvent>() },
            { "AnnouncementCommentCreated",               new EventInfo<AnnouncementCommentEvent>() },
            { "AnnouncementCommentUpdated",               new EventInfo<AnnouncementCommentEvent>() },
            { "AnnouncementCommentDeleted",               new EventInfo<AnnouncementCommentEvent>() },
            { "AnnouncementReactionCreated",              new EventInfo<AnnouncementReactionEvent>() },
            { "AnnouncementReactionDeleted",              new EventInfo<AnnouncementReactionEvent>() },
            { "AnnouncementCommentReactionCreated",       new EventInfo<AnnouncementCommentReactionEvent>() },
            { "AnnouncementCommentReactionDeleted",       new EventInfo<AnnouncementCommentReactionEvent>() },

            // Calendar events
            { "CalendarEventCreated",                     new EventInfo<CalendarEventEvent>() },
            { "CalendarEventUpdated",                     new EventInfo<CalendarEventEvent>() },
            { "CalendarEventDeleted",                     new EventInfo<CalendarEventEvent>() },

            { "CalendarEventSeriesUpdated",               new EventInfo<CalendarEventSeriesEvent>() },
            { "CalendarEventSeriesDeleted",               new EventInfo<CalendarEventSeriesEvent>() },

            { "CalendarEventRsvpUpdated",                 new EventInfo<CalendarEventRsvpEvent>() },
            { "CalendarEventRsvpManyUpdated",             new EventInfo<CalendarEventRsvpManyEvent>() },
            { "CalendarEventRsvpDeleted",                 new EventInfo<CalendarEventRsvpEvent>() },

            { "CalendarEventCommentCreated",              new EventInfo<CalendarEventCommentEvent>() },
            { "CalendarEventCommentUpdated",              new EventInfo<CalendarEventCommentEvent>() },
            { "CalendarEventCommentDeleted",              new EventInfo<CalendarEventCommentEvent>() },

            { "CalendarEventReactionCreated",             new EventInfo<CalendarEventReactionEvent>() },
            { "CalendarEventReactionDeleted",             new EventInfo<CalendarEventReactionEvent>() },

            { "CalendarEventCommentReactionCreated",      new EventInfo<CalendarEventCommentReactionEvent>() },
            { "CalendarEventCommentReactionDeleted",      new EventInfo<CalendarEventCommentReactionEvent>() },
        };
        #endregion

        WebsocketMessage.Subscribe(OnSocketMessage);

        // Prepare state
        Welcome.Subscribe(welcome =>
        {
            Me = welcome.User;

            if (!IsPrepared)
            {
                PreparedSubject.OnNext(Me);
                IsPrepared = true;
            }
        });
        Disconnected.Subscribe(info =>
        {
            if (info.Type != DisconnectionType.NoMessageReceived)
                IsPrepared = false;
        });
    }
    #endregion

    #region Methods
    /// <summary>
    /// When the socket message event is invoked.
    /// </summary>
    /// <remarks>
    /// <para>Receives and handles received <see cref="GuildedSocketMessage" /> messages.</para>
    /// </remarks>
    /// <param name="message">A message received from a WebSocket</param>
    protected void OnSocketMessage(GuildedSocketMessage message)
    {
        try
        {
            object eventName = message.EventName ?? (object)message.Opcode;

            // Checks if this event is supported by Guilded.NET
            if (GuildedEvents.ContainsKey(eventName))
            {
                IEventInfo<object> ev = GuildedEvents[eventName];

                object data = ev.Transform(ev.ArgumentType, GuildedSerializer, message);

                ev.OnNext(data);
            }
        }
        catch (Exception e)
        {
            _onWebsocketEventError.OnNext(e);
        }
    }

    private static void EnforceLimit(string name, string value, short limit)
    {
        if (value.Length > limit)
            throw new ArgumentOutOfRangeException(name, value, $"{name} exceeds the {limit} character limit");
    }

    private static void EnforceLimitOnNullable(string name, string? value, short limit)
    {
        if (value is not null) EnforceLimit(name, value, limit);
    }

    private async Task<T> TransformResponseAsync<T>(RestRequest request, object key, Func<JObject, JObject> transform) =>
        transform((JObject)await GetResponsePropertyAsync(request, key)).ToObject<T>(GuildedSerializer)!;

    private async Task<IList<T>> TransformListResponseAsync<T>(RestRequest request, object key, Func<JObject, T> transform) =>
        (await GetResponsePropertyAsync<IList<JObject>>(request, key).ConfigureAwait(false)).Select(transform).ToList();

    private async Task<T> GetResponsePropertyAsync<T>(RestRequest request, object key) =>
        (await GetResponsePropertyAsync(request, key).ConfigureAwait(false)).ToObject<T>(GuildedSerializer)!;

    private async Task<T> GetResponsePropertyAsync<T>(RestRequest request, object key, Guid channel) =>
        (await ExecuteRequestAsync<JContainer>(request, channel).ConfigureAwait(false)).Data![key]!.ToObject<T>(GuildedSerializer)!;

    private async Task<JToken> GetResponsePropertyAsync(RestRequest request, object key) =>
        (await ExecuteRequestAsync<JContainer>(request).ConfigureAwait(false)).Data![key]!;
    #endregion
}