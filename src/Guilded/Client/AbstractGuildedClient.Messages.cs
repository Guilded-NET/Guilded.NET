using System;
using System.Collections.Generic;
using Guilded.Base.Client;
using Guilded.Base.Content;
using Guilded.Base.Events;
using Guilded.Base.Servers;
using Guilded.Base.Users;

namespace Guilded.Client;

public abstract partial class AbstractGuildedClient
{
    #region Properties
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
    /// <seealso cref="ListItemCreated" />
    /// <seealso cref="DocCreated" />
    protected Dictionary<object, IEventInfo<object>> GuildedEvents { get; set; }

    #region WebSocket
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when <see cref="BaseGuildedClient.Websocket">WebSocket</see> is connected.
    /// </summary>
    /// <remarks>
    /// <para>An event with the opcode <c>1</c>.</para>
    /// </remarks>
    /// <seealso cref="Resume" />
    public IObservable<WelcomeEvent> Welcome => ((IEventInfo<WelcomeEvent>)GuildedEvents[SocketOpcode.Welcome]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when all lost <see cref="GuildedSocketMessage">WebSocket messages</see> get re-sent.
    /// </summary>
    /// <remarks>
    /// <para>An event with an event opcode of <c>2</c>.</para>
    /// </remarks>
    /// <seealso cref="Welcome" />
    public IObservable<ResumeEvent> Resume => ((IEventInfo<ResumeEvent>)GuildedEvents[SocketOpcode.Resume]).Observable;
    #endregion

    #region Properties Teams
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when <see cref="Member">members</see> receive <see cref="XpAddedEvent.Amount">XP</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>TeamXpAdded</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="RolesUpdated" />
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberBanned" />
    /// <seealso cref="MemberUnbanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    public IObservable<XpAddedEvent> XpAdded => ((IEventInfo<XpAddedEvent>)GuildedEvents["TeamXpAdded"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when <see cref="Member">members</see> receive or lose roles.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>teamRolesUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="XpAdded" />
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberBanned" />
    /// <seealso cref="MemberUnbanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    public IObservable<RolesUpdatedEvent> RolesUpdated => ((IEventInfo<RolesUpdatedEvent>)GuildedEvents["teamRolesUpdated"]).Observable;
    #endregion

    #region Properties Members
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when server-wide profile of a <see cref="Member">member</see> gets changed.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>TeamMemberUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="RolesUpdated" />
    /// <seealso cref="XpAdded" />
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberBanned" />
    /// <seealso cref="MemberUnbanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    public IObservable<MemberUpdatedEvent> MemberUpdated => ((IEventInfo<MemberUpdatedEvent>)GuildedEvents["TeamMemberUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Member">member</see> joins a <see cref="Server">server</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>TeamMemberJoined</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="RolesUpdated" />
    /// <seealso cref="XpAdded" />
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberBanned" />
    /// <seealso cref="MemberUnbanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    public IObservable<MemberJoinedEvent> MemberJoined => ((IEventInfo<MemberJoinedEvent>)GuildedEvents["TeamMemberJoined"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Member">member</see> leaves a <see cref="Server">server</see>, gets kicked or gets banned from a <see cref="Server">server</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>TeamMemberRemoved</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="RolesUpdated" />
    /// <seealso cref="XpAdded" />
    /// <seealso cref="MemberBanned" />
    /// <seealso cref="MemberUnbanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    public IObservable<MemberRemovedEvent> MemberRemoved => ((IEventInfo<MemberRemovedEvent>)GuildedEvents["TeamMemberRemoved"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Member">member</see> gets banned from <see cref="Server">the server</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>TeamMemberBanned</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="RolesUpdated" />
    /// <seealso cref="XpAdded" />
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberUnbanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    public IObservable<MemberBanEvent> MemberBanned => ((IEventInfo<MemberBanEvent>)GuildedEvents["TeamMemberBanned"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when <see cref="User">user</see> gets unbanned in a <see cref="Server">server</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>TeamMemberUnbanned</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="RolesUpdated" />
    /// <seealso cref="XpAdded" />
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberBanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    public IObservable<MemberBanEvent> MemberUnbanned => ((IEventInfo<MemberBanEvent>)GuildedEvents["TeamMemberUnbanned"]).Observable;
    #endregion

    #region Properties Channels
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when <see cref="Webhook">a new webhook</see> is created.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>TeamWebhookCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="RolesUpdated" />
    /// <seealso cref="XpAdded" />
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberBanned" />
    /// <seealso cref="MemberUnbanned" />
    /// <seealso cref="WebhookUpdated" />
    public IObservable<WebhookEvent> WebhookCreated => ((IEventInfo<WebhookEvent>)GuildedEvents["TeamWebhookCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Webhook">webhook</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>TeamWebhookUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="ChannelCreated" />
    /// <seealso cref="ChannelUpdated" />
    /// <seealso cref="ChannelDeleted" />
    public IObservable<WebhookEvent> WebhookUpdated => ((IEventInfo<WebhookEvent>)GuildedEvents["TeamWebhookUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a new <see cref="ServerChannel">channel</see> is created.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>TeamChannelCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ChannelUpdated" />
    /// <seealso cref="ChannelDeleted" />
    public IObservable<ChannelEvent> ChannelCreated => ((IEventInfo<ChannelEvent>)GuildedEvents["TeamChannelCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="ServerChannel">channel</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>TeamChannelUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ChannelCreated" />
    /// <seealso cref="ChannelDeleted" />
    public IObservable<ChannelEvent> ChannelUpdated => ((IEventInfo<ChannelEvent>)GuildedEvents["TeamChannelUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="ServerChannel">channel</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>TeamChannelDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ChannelCreated" />
    /// <seealso cref="ChannelUpdated" />
    public IObservable<ChannelEvent> ChannelDeleted => ((IEventInfo<ChannelEvent>)GuildedEvents["TeamChannelDeleted"]).Observable;
    #endregion

    #region Properties Chat channels
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a new <see cref="Message">message</see> is sent.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChatMessageCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MessageUpdated" />
    /// <seealso cref="MessageDeleted" />
    /// <seealso cref="MessageReactionAdded" />
    /// <seealso cref="MessageReactionRemoved" />
    public IObservable<MessageEvent> MessageCreated => ((IEventInfo<MessageEvent>)GuildedEvents["ChatMessageCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Message">message</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChatMessageUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MessageCreated" />
    /// <seealso cref="MessageDeleted" />
    /// <seealso cref="MessageReactionAdded" />
    /// <seealso cref="MessageReactionRemoved" />
    public IObservable<MessageEvent> MessageUpdated => ((IEventInfo<MessageEvent>)GuildedEvents["ChatMessageUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Message">message</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChatMessageDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MessageCreated" />
    /// <seealso cref="MessageUpdated" />
    /// <seealso cref="MessageReactionAdded" />
    /// <seealso cref="MessageReactionRemoved" />
    public IObservable<MessageDeletedEvent> MessageDeleted => ((IEventInfo<MessageDeletedEvent>)GuildedEvents["ChatMessageDeleted"]).Observable;

    /// <inheritdoc cref="MessageReactionAdded" />
    [Obsolete("Migrate to MessageReactionAdded. This might be used for global reactions instead")]
    public IObservable<MessageReactionEvent> ReactionAdded => MessageReactionAdded;

    /// <inheritdoc cref="MessageReactionRemoved" />
    [Obsolete("Migrate to MessageReactionRemoved. This might be used for global reactions instead")]
    public IObservable<MessageReactionEvent> ReactionRemoved => MessageReactionRemoved;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Reaction">reaction</see> on a <see cref="Message">message</see> is added.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChannelMessageReactionCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MessageCreated" />
    /// <seealso cref="MessageUpdated" />
    /// <seealso cref="MessageDeleted" />
    /// <seealso cref="MessageReactionRemoved" />
    public IObservable<MessageReactionEvent> MessageReactionAdded => ((IEventInfo<MessageReactionEvent>)GuildedEvents["ChannelMessageReactionCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Reaction">reaction</see> on a <see cref="Message">message</see> is removed.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChannelMessageReactionDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MessageCreated" />
    /// <seealso cref="MessageUpdated" />
    /// <seealso cref="MessageDeleted" />
    /// <seealso cref="MessageReactionAdded" />
    public IObservable<MessageReactionEvent> MessageReactionRemoved => ((IEventInfo<MessageReactionEvent>)GuildedEvents["ChannelMessageReactionCreated"]).Observable;
    #endregion

    #region Properties Forum channels
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a new <see cref="Topic">forum topic</see> is posted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ForumTopicCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="TopicUpdated" />
    /// <seealso cref="TopicDeleted" />
    public IObservable<TopicEvent> TopicCreated => ((IEventInfo<TopicEvent>)GuildedEvents["ForumTopicCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Topic">forum topic</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ForumTopicUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="TopicCreated" />
    /// <seealso cref="TopicDeleted" />
    public IObservable<TopicEvent> TopicUpdated => ((IEventInfo<TopicEvent>)GuildedEvents["ForumTopicUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Topic">forum topic</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ForumTopicDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="TopicCreated" />
    /// <seealso cref="TopicUpdated" />
    public IObservable<TopicEvent> TopicDeleted => ((IEventInfo<TopicEvent>)GuildedEvents["ForumTopicDeleted"]).Observable;
    #endregion

    #region Properties List channels
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a new <see cref="ListItem">list item</see> is posted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ListItemCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="ListItemUpdated" />
    /// <seealso cref="ListItemDeleted" />
    /// <seealso cref="ListItemCompleted" />
    /// <seealso cref="ListItemUncompleted" />
    public IObservable<ListItemEvent> ListItemCreated => ((IEventInfo<ListItemEvent>)GuildedEvents["ListItemCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="ListItem">list item</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ListItemUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="ListItemCreated" />
    /// <seealso cref="ListItemDeleted" />
    /// <seealso cref="ListItemCompleted" />
    /// <seealso cref="ListItemUncompleted" />
    public IObservable<ListItemEvent> ListItemUpdated => ((IEventInfo<ListItemEvent>)GuildedEvents["ListItemUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="ListItem">list item</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ListItemDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="ListItemCreated" />
    /// <seealso cref="ListItemUpdated" />
    /// <seealso cref="ListItemCompleted" />
    /// <seealso cref="ListItemUncompleted" />
    public IObservable<ListItemEvent> ListItemDeleted => ((IEventInfo<ListItemEvent>)GuildedEvents["ListItemDeleted"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="ListItem">list item</see> is set as completed.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ListItemCompleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="ListItemUpdated" />
    /// <seealso cref="ListItemUpdated" />
    /// <seealso cref="ListItemCompleted" />
    /// <seealso cref="ListItemUncompleted" />
    public IObservable<ListItemEvent> ListItemCompleted => ((IEventInfo<ListItemEvent>)GuildedEvents["ListItemDeleted"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="ListItem">list item</see> is set as not completed.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ListItemUncompleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="ListItemUpdated" />
    /// <seealso cref="ListItemDeleted" />
    /// <seealso cref="ListItemCompleted" />
    /// <seealso cref="ListItemUncompleted" />
    public IObservable<ListItemEvent> ListItemUncompleted => ((IEventInfo<ListItemEvent>)GuildedEvents["ListItemDeleted"]).Observable;
    #endregion

    #region Properties Docs channels
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a new <see cref="Doc">document</see> is posted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>DocCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="DocUpdated" />
    /// <seealso cref="DocDeleted" />
    public IObservable<DocEvent> DocCreated => ((IEventInfo<DocEvent>)GuildedEvents["DocCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Doc">document</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>DocUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="DocCreated" />
    /// <seealso cref="DocDeleted" />
    public IObservable<DocEvent> DocUpdated => ((IEventInfo<DocEvent>)GuildedEvents["DocUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Doc">document</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>DocDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="DocCreated" />
    /// <seealso cref="DocUpdated" />
    public IObservable<DocEvent> DocDeleted => ((IEventInfo<DocEvent>)GuildedEvents["DocDeleted"]).Observable;
    #endregion

    #region Properties Calendar channels

    #region Properties Calendar channels > Events
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a new <see cref="CalendarEvent">calendar event</see> is posted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="EventUpdated" />
    /// <seealso cref="EventDeleted" />
    public IObservable<CalendarEventEvent> EventCreated => ((IEventInfo<CalendarEventEvent>)GuildedEvents["CalendarEventCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="CalendarEvent">calendar event</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="EventCreated" />
    /// <seealso cref="EventDeleted" />
    public IObservable<CalendarEventEvent> EventUpdated => ((IEventInfo<CalendarEventEvent>)GuildedEvents["CalendarEventUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="CalendarEvent">calendar event</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="EventCreated" />
    /// <seealso cref="EventUpdated" />
    public IObservable<CalendarEventEvent> EventDeleted => ((IEventInfo<CalendarEventEvent>)GuildedEvents["CalendarEventDeleted"]).Observable;
    #endregion

    #region Properties Calendar channels > RSVPs
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="CalendarRsvp">RSVP</see> of a <see cref="CalendarEvent">calendar event</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>This may include deletion as well. This hasn't been checked yet.</para>
    /// <para>An event with the name <c>CalendarEventRssvpUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="RsvpManyUpdated" />
    /// <seealso cref="RsvpDeleted" />
    /// <seealso cref="EventUpdated" />
    /// <seealso cref="EventDeleted" />
    public IObservable<CalendarRsvpEvent> RsvpUpdated => ((IEventInfo<CalendarRsvpEvent>)GuildedEvents["CalendarEventRsvpUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when multiple <see cref="CalendarRsvp">RSVPs</see> of a <see cref="CalendarEvent">calendar event</see> are edited or created.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventRsvpManyUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="RsvpUpdated" />
    /// <seealso cref="RsvpDeleted" />
    /// <seealso cref="EventCreated" />
    /// <seealso cref="EventDeleted" />
    public IObservable<CalendarRsvpManyEvent> RsvpManyUpdated => ((IEventInfo<CalendarRsvpManyEvent>)GuildedEvents["CalendarEventRsvpManyUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="CalendarRsvp">RSVP</see> of a <see cref="CalendarEvent">calendar event</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventRsvpDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="RsvpUpdated" />
    /// <seealso cref="RsvpManyUpdated" />
    /// <seealso cref="EventCreated" />
    /// <seealso cref="EventUpdated" />
    public IObservable<CalendarRsvpEvent> RsvpDeleted => ((IEventInfo<CalendarRsvpEvent>)GuildedEvents["CalendarEventRsvpDeleted"]).Observable;
    #endregion

    #endregion

    #endregion

    #region Methods

    #region Methods Handling
    /// <summary>
    /// When the socket message event is invoked.
    /// </summary>
    /// <remarks>
    /// <para>Receives and handles received <see cref="GuildedSocketMessage" /> messages.</para>
    /// </remarks>
    /// <param name="message">A message received from a WebSocket</param>
    protected void OnSocketMessage(GuildedSocketMessage message)
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
    #endregion

    #endregion
}