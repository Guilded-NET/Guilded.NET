using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

using Guilded.Base.Client;
using Guilded.Base.Events;
using Guilded.Base.Users;
using Newtonsoft.Json.Linq;

using RestSharp;
using Websocket.Client;

namespace Guilded.Abstract;

/// <summary>
/// Represents the base for all Guilded clients.
/// </summary>
/// <remarks>
/// <para>There is not much to be used here. It is recommended to use <see cref="GuildedBotClient" />.</para>
/// </remarks>
/// <seealso cref="GuildedBotClient" />
/// <seealso cref="BaseGuildedClient" />
public abstract partial class AbstractGuildedClient : BaseGuildedClient
{
    #region Fields
    /// <summary>
    /// An observable event that occurs once Guilded client has connected and added finishing touches.
    /// </summary>
    /// <returns>Prepared subject</returns>
    protected Subject<Me> PreparedSubject { get; } = new();
    #endregion

    #region Properties
    /// <inheritdoc cref="PreparedSubject" />
    public IObservable<Me> Prepared => PreparedSubject.AsObservable();

    /// <inheritdoc cref="WelcomeEvent.User" />
    public Me? Me { get; protected set; }

    /// <summary>
    /// Gets whether the client is <see cref="Prepared">prepared</see>.
    /// </summary>
    /// <value>Whether the client is <see cref="Prepared">prepared</see></value>
    public bool IsPrepared { get; protected set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new base instance of <see cref="AbstractGuildedClient" /> children types.
    /// </summary>
    /// <seealso cref="GuildedBotClient()" />
    /// <seealso cref="GuildedBotClient(string)" />
    protected AbstractGuildedClient()
    {
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
            { "TeamMemberBanned",              new EventInfo<MemberBanEvent>() },
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
    /// <summary>
    /// Connects <see cref="AbstractGuildedClient">this client</see> to Guilded.
    /// </summary>
    /// <seealso cref="BaseGuildedClient.DisconnectAsync" />
    /// <seealso cref="GuildedBotClient.ConnectAsync()" />
    /// <seealso cref="GuildedBotClient.ConnectAsync(string)" />
    public override async Task ConnectAsync()
    {
        try
        {
            await Websocket.StartOrFail().ConfigureAwait(false);
            ConnectedSubject.OnNext(this);
        }
        catch (Exception e)
        {
            ConnectedSubject.OnError(e);
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

    private async Task<T> GetResponseProperty<T>(RestRequest request, object key) =>
        (await ExecuteRequestAsync<JContainer>(request).ConfigureAwait(false)).Data![key]!.ToObject<T>(GuildedSerializer)!;

    private async Task<T> GetResponseProperty<T>(RestRequest request, object key, Guid channel) =>
        (await ExecuteRequestAsync<JContainer>(request, channel).ConfigureAwait(false)).Data![key]!.ToObject<T>(GuildedSerializer)!;
    #endregion
}