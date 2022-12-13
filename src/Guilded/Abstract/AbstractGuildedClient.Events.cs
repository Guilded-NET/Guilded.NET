using System;
using System.Collections.Generic;
using Guilded.Connection;
using Guilded.Content;
using Guilded.Events;
using Guilded.Servers;
using Guilded.Users;

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
    /// <seealso cref="ItemCreated" />
    /// <seealso cref="DocCreated" />
    protected Dictionary<object, IEventInfo<object>> GuildedEvents { get; set; }
    #endregion

    #region Properties WebSocket
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when <see cref="BaseGuildedConnection.Websocket">WebSocket</see> is connected.
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

    #region Properties Servers
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when <see cref="Member">members</see> receive <see cref="XpAddedEvent.Amount">XP</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerXpAdded</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="RolesUpdated" />
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberBanned" />
    /// <seealso cref="MemberUnbanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ServerAdded" />
    public IObservable<XpAddedEvent> XpAdded => ((IEventInfo<XpAddedEvent>)GuildedEvents["ServerXpAdded"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when <see cref="Member">members</see> receive or lose roles.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerRolesUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="XpAdded" />
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberBanned" />
    /// <seealso cref="MemberUnbanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ServerAdded" />
    public IObservable<RolesUpdatedEvent> RolesUpdated => ((IEventInfo<RolesUpdatedEvent>)GuildedEvents["ServerRolesUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="GuildedBotClient">client bot</see> gets added to a <see cref="Server">server</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>BotServerMembershipCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="ServerRemoved" />
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="RolesUpdated" />
    /// <seealso cref="XpAdded" />
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberBanned" />
    /// <seealso cref="MemberUnbanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    public IObservable<ServerAddedEvent> ServerAdded => ((IEventInfo<ServerAddedEvent>)GuildedEvents["BotServerMembershipCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="GuildedBotClient">client bot</see> gets removed from a <see cref="Server">server</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>BotServerMembershipDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="ServerAdded" />
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="RolesUpdated" />
    /// <seealso cref="XpAdded" />
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberBanned" />
    /// <seealso cref="MemberUnbanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    public IObservable<ServerAddedEvent> ServerRemoved => ((IEventInfo<ServerAddedEvent>)GuildedEvents["BotServerMembershipDeleted"]).Observable;
    #endregion

    #region Properties Members
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when server-wide profile of a <see cref="Member">member</see> gets changed.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerMemberUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="RolesUpdated" />
    /// <seealso cref="XpAdded" />
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberBanned" />
    /// <seealso cref="MemberUnbanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ServerAdded" />
    public IObservable<MemberUpdatedEvent> MemberUpdated => ((IEventInfo<MemberUpdatedEvent>)GuildedEvents["ServerMemberUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Member">member</see> joins a <see cref="Server">server</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerMemberJoined</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="RolesUpdated" />
    /// <seealso cref="XpAdded" />
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberBanned" />
    /// <seealso cref="MemberUnbanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ServerAdded" />
    public IObservable<MemberJoinedEvent> MemberJoined => ((IEventInfo<MemberJoinedEvent>)GuildedEvents["ServerMemberJoined"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Member">member</see> leaves a <see cref="Server">server</see>, gets kicked or gets banned from a <see cref="Server">server</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerMemberRemoved</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="RolesUpdated" />
    /// <seealso cref="XpAdded" />
    /// <seealso cref="MemberBanned" />
    /// <seealso cref="MemberUnbanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ServerAdded" />
    public IObservable<MemberRemovedEvent> MemberRemoved => ((IEventInfo<MemberRemovedEvent>)GuildedEvents["ServerMemberRemoved"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Member">member</see> gets banned from the <see cref="Server">server</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerMemberBanned</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="RolesUpdated" />
    /// <seealso cref="XpAdded" />
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberUnbanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ServerAdded" />
    public IObservable<MemberBanEvent> MemberBanAdded => ((IEventInfo<MemberBanEvent>)GuildedEvents["ServerMemberBanned"]).Observable;

    /// <inheritdoc cref="MemberBanAdded" />
    [Obsolete("Use `MemberBanAdded` instead")]
    public IObservable<MemberBanEvent> MemberBanned => MemberBanAdded;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when <see cref="User">user</see> gets unbanned in a <see cref="Server">server</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerMemberUnbanned</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="RolesUpdated" />
    /// <seealso cref="XpAdded" />
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberBanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ServerAdded" />
    public IObservable<MemberBanEvent> MemberBanRemoved => ((IEventInfo<MemberBanEvent>)GuildedEvents["ServerMemberUnbanned"]).Observable;

    /// <inheritdoc cref="MemberBanRemoved" />
    [Obsolete("Use `MemberBanRemoved` instead")]
    public IObservable<MemberBanEvent> MemberUnbanned => MemberBanAdded;
    #endregion

    #region Properties Channels
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when <see cref="Webhook">a new webhook</see> is created.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerWebhookCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ChannelCreated" />
    /// <seealso cref="ChannelUpdated" />
    /// <seealso cref="ChannelDeleted" />
    public IObservable<WebhookEvent> WebhookCreated => ((IEventInfo<WebhookEvent>)GuildedEvents["ServerWebhookCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Webhook">webhook</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerWebhookUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="ChannelCreated" />
    /// <seealso cref="ChannelUpdated" />
    /// <seealso cref="ChannelDeleted" />
    public IObservable<WebhookEvent> WebhookUpdated => ((IEventInfo<WebhookEvent>)GuildedEvents["ServerWebhookUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a new <see cref="ServerChannel">channel</see> is created.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerChannelCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ChannelUpdated" />
    /// <seealso cref="ChannelDeleted" />
    public IObservable<ChannelEvent> ChannelCreated => ((IEventInfo<ChannelEvent>)GuildedEvents["ServerChannelCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="ServerChannel">channel</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerChannelUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ChannelCreated" />
    /// <seealso cref="ChannelDeleted" />
    public IObservable<ChannelEvent> ChannelUpdated => ((IEventInfo<ChannelEvent>)GuildedEvents["ServerChannelUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="ServerChannel">channel</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerChannelDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ChannelCreated" />
    /// <seealso cref="ChannelUpdated" />
    public IObservable<ChannelEvent> ChannelDeleted => ((IEventInfo<ChannelEvent>)GuildedEvents["ServerChannelDeleted"]).Observable;
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
    /// <seealso cref="TopicPinned" />
    /// <seealso cref="TopicUnpinned" />
    /// <seealso cref="TopicLocked" />
    /// <seealso cref="TopicUnlocked" />
    /// <seealso cref="TopicReactionAdded" />
    /// <seealso cref="TopicReactionRemoved" />
    public IObservable<TopicEvent> TopicCreated => ((IEventInfo<TopicEvent>)GuildedEvents["ForumTopicCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Topic">forum topic</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ForumTopicUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="TopicCreated" />
    /// <seealso cref="TopicDeleted" />
    /// <seealso cref="TopicPinned" />
    /// <seealso cref="TopicUnpinned" />
    /// <seealso cref="TopicLocked" />
    /// <seealso cref="TopicUnlocked" />
    /// <seealso cref="TopicReactionAdded" />
    /// <seealso cref="TopicReactionRemoved" />
    public IObservable<TopicEvent> TopicUpdated => ((IEventInfo<TopicEvent>)GuildedEvents["ForumTopicUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Topic">forum topic</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ForumTopicDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="TopicCreated" />
    /// <seealso cref="TopicUpdated" />
    /// <seealso cref="TopicPinned" />
    /// <seealso cref="TopicUnpinned" />
    /// <seealso cref="TopicLocked" />
    /// <seealso cref="TopicUnlocked" />
    /// <seealso cref="TopicReactionAdded" />
    /// <seealso cref="TopicReactionRemoved" />
    public IObservable<TopicEvent> TopicDeleted => ((IEventInfo<TopicEvent>)GuildedEvents["ForumTopicDeleted"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Topic">forum topic</see> is pinned.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ForumTopicPinned</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="TopicCreated" />
    /// <seealso cref="TopicUpdated" />
    /// <seealso cref="TopicDeleted" />
    /// <seealso cref="TopicUnpinned" />
    /// <seealso cref="TopicLocked" />
    /// <seealso cref="TopicUnlocked" />
    /// <seealso cref="TopicReactionAdded" />
    /// <seealso cref="TopicReactionRemoved" />
    public IObservable<TopicEvent> TopicPinned => ((IEventInfo<TopicEvent>)GuildedEvents["ForumTopicPinned"]).Observable;

    /// <inheritdoc cref="TopicPinned" />
    [Obsolete("Use `TopicPinned` instead")]
    public IObservable<TopicEvent> TopicPinAdded => TopicPinned;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Topic">forum topic</see> is unpinned.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ForumTopicUnpinned</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="TopicCreated" />
    /// <seealso cref="TopicUpdated" />
    /// <seealso cref="TopicDeleted" />
    /// <seealso cref="TopicPinned" />
    /// <seealso cref="TopicLocked" />
    /// <seealso cref="TopicUnlocked" />
    /// <seealso cref="TopicReactionAdded" />
    /// <seealso cref="TopicReactionRemoved" />
    public IObservable<TopicEvent> TopicUnpinned => ((IEventInfo<TopicEvent>)GuildedEvents["ForumTopicUnpinned"]).Observable;

    /// <inheritdoc cref="TopicUnpinned" />
    [Obsolete("Use `TopicUnpinned` instead")]
    public IObservable<TopicEvent> TopicPinRemoved => TopicUnpinned;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Topic">forum topic</see> is pinned.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ForumTopicLocked</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="TopicCreated" />
    /// <seealso cref="TopicUpdated" />
    /// <seealso cref="TopicDeleted" />
    /// <seealso cref="TopicPinned" />
    /// <seealso cref="TopicUnpinned" />
    /// <seealso cref="TopicUnlocked" />
    /// <seealso cref="TopicReactionAdded" />
    /// <seealso cref="TopicReactionRemoved" />
    public IObservable<TopicEvent> TopicLocked => ((IEventInfo<TopicEvent>)GuildedEvents["ForumTopicLocked"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Topic">forum topic</see> is unpinned.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ForumTopicUnlocked</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="TopicCreated" />
    /// <seealso cref="TopicUpdated" />
    /// <seealso cref="TopicDeleted" />
    /// <seealso cref="TopicPinned" />
    /// <seealso cref="TopicUnpinned" />
    /// <seealso cref="TopicLocked" />
    /// <seealso cref="TopicReactionAdded" />
    /// <seealso cref="TopicReactionRemoved" />
    public IObservable<TopicEvent> TopicUnlocked => ((IEventInfo<TopicEvent>)GuildedEvents["ForumTopicUnlocked"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="TopicReaction">reaction</see> is added to a <see cref="Topic">forum topic</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ForumTopicReactionCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="TopicReactionRemoved" />
    /// <seealso cref="TopicCreated" />
    /// <seealso cref="TopicUpdated" />
    /// <seealso cref="TopicDeleted" />
    /// <seealso cref="TopicPinned" />
    /// <seealso cref="TopicUnpinned" />
    /// <seealso cref="TopicLocked" />
    /// <seealso cref="TopicUnlocked" />
    public IObservable<TopicReactionEvent> TopicReactionAdded => ((IEventInfo<TopicReactionEvent>)GuildedEvents["ForumTopicReactionCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="TopicReaction">reaction</see> is removed from a <see cref="Topic">forum topic</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ForumTopicReactionDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="TopicReactionAdded" />
    /// <seealso cref="TopicCreated" />
    /// <seealso cref="TopicUpdated" />
    /// <seealso cref="TopicDeleted" />
    /// <seealso cref="TopicPinned" />
    /// <seealso cref="TopicUnpinned" />
    /// <seealso cref="TopicLocked" />
    /// <seealso cref="TopicUnlocked" />
    public IObservable<TopicReactionEvent> TopicReactionRemoved => ((IEventInfo<TopicReactionEvent>)GuildedEvents["ForumTopicReactionDeleted"]).Observable;
    #endregion

    #region Properties Forum channels > Comments
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a new <see cref="TopicComment">forum topic comment</see> is posted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ForumTopicCommentCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="TopicCommentUpdated" />
    /// <seealso cref="TopicCommentDeleted" />
    /// <seealso cref="TopicCommentReactionAdded" />
    /// <seealso cref="TopicCommentReactionRemoved" />
    public IObservable<TopicCommentEvent> TopicCommentCreated => ((IEventInfo<TopicCommentEvent>)GuildedEvents["ForumTopicCommentCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="TopicComment">forum topic comment</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ForumTopicCommentUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="TopicCommentCreated" />
    /// <seealso cref="TopicCommentDeleted" />
    /// <seealso cref="TopicCommentReactionAdded" />
    /// <seealso cref="TopicCommentReactionRemoved" />
    public IObservable<TopicCommentEvent> TopicCommentUpdated => ((IEventInfo<TopicCommentEvent>)GuildedEvents["ForumTopicCommentUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="TopicComment">forum topic comment</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ForumTopicCommentDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="TopicCommentCreated" />
    /// <seealso cref="TopicCommentUpdated" />
    /// <seealso cref="TopicCommentReactionAdded" />
    /// <seealso cref="TopicCommentReactionRemoved" />
    public IObservable<TopicCommentEvent> TopicCommentDeleted => ((IEventInfo<TopicCommentEvent>)GuildedEvents["ForumTopicCommentDeleted"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="TopicCommentReaction">reaction</see> is added to a <see cref="TopicComment">forum topic comment</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ForumTopicCommentReactionCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="TopicCommentCreated" />
    /// <seealso cref="TopicCommentUpdated" />
    /// <seealso cref="TopicCommentDeleted" />
    /// <seealso cref="TopicCommentReactionRemoved" />
    public IObservable<TopicReactionEvent> TopicCommentReactionAdded => ((IEventInfo<TopicReactionEvent>)GuildedEvents["ForumTopicCommentReactionCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="TopicCommentReaction">reaction</see> is added to a <see cref="TopicComment">forum topic comment</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ForumTopicCommentReactionDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="TopicCommentCreated" />
    /// <seealso cref="TopicCommentUpdated" />
    /// <seealso cref="TopicCommentDeleted" />
    /// <seealso cref="TopicCommentReactionAdded" />
    public IObservable<TopicReactionEvent> TopicCommentReactionRemoved => ((IEventInfo<TopicReactionEvent>)GuildedEvents["ForumTopicCommentReactionDeleted"]).Observable;
    #endregion

    #region Properties List channels
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a new <see cref="Item">list item</see> is posted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ListItemCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="ItemUpdated" />
    /// <seealso cref="ItemDeleted" />
    /// <seealso cref="ItemCompleted" />
    /// <seealso cref="ItemUncompleted" />
    public IObservable<ItemEvent> ItemCreated => ((IEventInfo<ItemEvent>)GuildedEvents["ListItemCreated"]).Observable;

    /// <inheritdoc cref="ItemCreated" />
    [Obsolete("Use `ItemCreated` instead")]
    public IObservable<ItemEvent> ListItemCreated => ItemCreated;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Item">list item</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ListItemUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="ItemCreated" />
    /// <seealso cref="ItemDeleted" />
    /// <seealso cref="ItemCompleted" />
    /// <seealso cref="ItemUncompleted" />
    public IObservable<ItemEvent> ItemUpdated => ((IEventInfo<ItemEvent>)GuildedEvents["ListItemUpdated"]).Observable;

    /// <inheritdoc cref="ItemUpdated" />
    [Obsolete("Use `ItemUpdated` instead")]
    public IObservable<ItemEvent> ListItemUpdated => ItemUpdated;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Item">list item</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ListItemDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="ItemCreated" />
    /// <seealso cref="ItemUpdated" />
    /// <seealso cref="ItemCompleted" />
    /// <seealso cref="ItemUncompleted" />
    public IObservable<ItemEvent> ItemDeleted => ((IEventInfo<ItemEvent>)GuildedEvents["ListItemDeleted"]).Observable;

    /// <inheritdoc cref="ItemDeleted" />
    [Obsolete("Use `ItemDeleted` instead")]
    public IObservable<ItemEvent> ListItemDeleted => ItemDeleted;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Item">list item</see> is set as completed.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ListItemCompleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="ItemUpdated" />
    /// <seealso cref="ItemUpdated" />
    /// <seealso cref="ItemCompleted" />
    /// <seealso cref="ItemUncompleted" />
    public IObservable<ItemEvent> ItemCompleted => ((IEventInfo<ItemEvent>)GuildedEvents["ListItemDeleted"]).Observable;

    /// <inheritdoc cref="ItemCompleted" />
    [Obsolete("Use `ItemCompleted` instead")]
    public IObservable<ItemEvent> ListItemCompleted => ItemCompleted;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Item">list item</see> is set as not completed.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ListItemUncompleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="ItemUpdated" />
    /// <seealso cref="ItemDeleted" />
    /// <seealso cref="ItemCompleted" />
    /// <seealso cref="ItemUncompleted" />
    public IObservable<ItemEvent> ItemUncompleted => ((IEventInfo<ItemEvent>)GuildedEvents["ListItemDeleted"]).Observable;

    /// <inheritdoc cref="ItemUncompleted" />
    [Obsolete("Use `ItemUncompleted` instead")]
    public IObservable<ItemEvent> ListItemUncompleted => ItemUncompleted;
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
    #endregion
}