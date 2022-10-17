using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Connection;
using Guilded.Events;
using Guilded.Json;
using Guilded.Users;
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
    /// <summary>
    /// An observable event that occurs once Guilded client has connected and added finishing touches.
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
    /// Gets whether the client is <see cref="Prepared">prepared</see>.
    /// </summary>
    /// <value>Whether the client is <see cref="Prepared">prepared</see></value>
    public bool IsPrepared { get; protected set; }

    /// <inheritdoc cref="ClientUser.Id" />
    public HashId? Id => Me?.Id;

    /// <inheritdoc cref="ClientUser.BotId" />
    public Guid? BotId => Me?.BotId;

    /// <inheritdoc cref="ClientUser.CreatedBy" />
    public HashId? CreatedBy => Me?.CreatedBy;

    /// <inheritdoc cref="ClientUser.CreatedAt" />
    public DateTime? CreatedAt => Me?.CreatedAt;

    /// <inheritdoc cref="ClientUser.Name" />
    public string? Name => Me?.Name;
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

        #region Event list
        // Dictionary of supported events, so we wouldn't need to manually do it.
        // The only manual work to be done is in AbstractGuildedClient.Messages.cs file,
        // which only allows us to subscribe to events and it is literally +1 member
        // to be added and copy pasting for the most part.
        GuildedEvents = new Dictionary<object, IEventInfo<object>>
        {
            // Event messages
            { SocketOpcode.Welcome,            new EventInfo<WelcomeEvent>() },
            { SocketOpcode.Resume,             new EventInfo<ResumeEvent>() },

            // Team events
            { "TeamXpAdded",                   new EventInfo<XpAddedEvent>() },
            { "TeamMemberRemoved",             new EventInfo<MemberRemovedEvent>() },
            { "TeamMemberBanned",
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
            { "TeamMemberUnbanned",            new EventInfo<MemberBanEvent>() },
            { "TeamChannelCreated",            new EventInfo<ChannelEvent>() },
            { "TeamChannelUpdated",            new EventInfo<ChannelEvent>() },
            { "TeamChannelDeleted",            new EventInfo<ChannelEvent>() },
            { "TeamWebhookCreated",            new EventInfo<WebhookEvent>() },
            { "TeamWebhookUpdated",            new EventInfo<WebhookEvent>() },
            { "TeamMemberUpdated",             new EventInfo<MemberUpdatedEvent>() },
            { "teamRolesUpdated",              new EventInfo<RolesUpdatedEvent>() },
            { "TeamMemberJoined",
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

            // Chat messages
            { "ChatMessageCreated",            new EventInfo<MessageEvent>() },
            { "ChatMessageUpdated",            new EventInfo<MessageEvent>() },
            { "ChatMessageDeleted",            new EventInfo<MessageDeletedEvent>() },
            { "ChannelMessageReactionCreated", new EventInfo<MessageReactionEvent>() },
            { "ChannelMessageReactionDeleted", new EventInfo<MessageReactionEvent>() },

            // Forum topics
            { "ForumTopicCreated",             new EventInfo<TopicEvent>() },
            { "ForumTopicUpdated",             new EventInfo<TopicEvent>() },
            { "ForumTopicDeleted",             new EventInfo<TopicEvent>() },
            { "ForumTopicPinned",              new EventInfo<TopicEvent>() },
            { "ForumTopicUnpinned",            new EventInfo<TopicEvent>() },
            { "ForumTopicLocked",              new EventInfo<TopicEvent>() },
            { "ForumTopicUnlocked",            new EventInfo<TopicEvent>() },

            // List items
            { "ListItemCreated",               new EventInfo<ListItemEvent>() },
            { "ListItemUpdated",               new EventInfo<ListItemEvent>() },
            { "ListItemDeleted",               new EventInfo<ListItemEvent>() },
            { "ListItemCompleted",             new EventInfo<ListItemEvent>() },
            { "ListItemUncompleted",           new EventInfo<ListItemEvent>() },

            // Docs
            { "DocCreated",                    new EventInfo<DocEvent>() },
            { "DocUpdated",                    new EventInfo<DocEvent>() },
            { "DocDeleted",                    new EventInfo<DocEvent>() },

            // Calendar events
            { "CalendarEventCreated",          new EventInfo<CalendarEventEvent>() },
            { "CalendarEventUpdated",          new EventInfo<CalendarEventEvent>() },
            { "CalendarEventDeleted",          new EventInfo<CalendarEventEvent>() },
            { "CalendarEventRsvpUpdated",      new EventInfo<CalendarRsvpEvent>() },
            { "CalendarEventRsvpManyUpdated",  new EventInfo<CalendarRsvpManyEvent>() },
            { "CalendarEventRsvpDeleted",      new EventInfo<CalendarRsvpEvent>() },
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
        transform(await GetResponseProperty<JObject>(request, key)).ToObject<T>(GuildedSerializer)!;

    private async Task<IList<T>> TransformListResponseAsync<T>(RestRequest request, object key, Func<JObject, T> transform) =>
        (await GetResponseProperty<IList<JObject>>(request, key).ConfigureAwait(false)).Select(transform).ToList();

    private async Task<T> GetResponseProperty<T>(RestRequest request, object key) =>
        (await ExecuteRequestAsync<JContainer>(request).ConfigureAwait(false)).Data![key]!.ToObject<T>(GuildedSerializer)!;

    private async Task<T> GetResponseProperty<T>(RestRequest request, object key, Guid channel) =>
        (await ExecuteRequestAsync<JContainer>(request, channel).ConfigureAwait(false)).Data![key]!.ToObject<T>(GuildedSerializer)!;
    #endregion
}