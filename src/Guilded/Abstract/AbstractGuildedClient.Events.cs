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
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="GuildedBotClient">client bot</see> gets added to a <see cref="Server">server</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>BotServerMembershipCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="ServerRemoved" />
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="RolesUpdated" />
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
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberBanned" />
    /// <seealso cref="MemberUnbanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    public IObservable<ServerAddedEvent> ServerRemoved => ((IEventInfo<ServerAddedEvent>)GuildedEvents["BotServerMembershipDeleted"]).Observable;
    #endregion

    #region Properties Groups
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a new <see cref="Group">group</see> is created.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>GroupCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="GroupUpdated" />
    /// <seealso cref="GroupDeleted" />
    public IObservable<GroupEvent> GroupCreated => ((IEventInfo<GroupEvent>)GuildedEvents["GroupCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Group">group</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>GroupUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="GroupCreated" />
    /// <seealso cref="GroupDeleted" />
    public IObservable<GroupEvent> GroupUpdated => ((IEventInfo<GroupEvent>)GuildedEvents["GroupUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Group">group</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>GroupDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="GroupCreated" />
    /// <seealso cref="GroupUpdated" />
    public IObservable<GroupEvent> GroupDeleted => ((IEventInfo<GroupEvent>)GuildedEvents["GroupDeleted"]).Observable;
    #endregion

    #region Properties Role
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a new <see cref="Role">role</see> is created.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>RoleCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="RoleUpdated" />
    /// <seealso cref="RoleDeleted" />
    public IObservable<RoleEvent> RoleCreated => ((IEventInfo<RoleEvent>)GuildedEvents["RoleCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Role">role</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>RoleUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="RoleCreated" />
    /// <seealso cref="RoleDeleted" />
    public IObservable<RoleEvent> RoleUpdated => ((IEventInfo<RoleEvent>)GuildedEvents["RoleUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Role">role</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>RoleDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="RoleCreated" />
    /// <seealso cref="RoleUpdated" />
    public IObservable<RoleEvent> RoleDeleted => ((IEventInfo<RoleEvent>)GuildedEvents["RoleDeleted"]).Observable;
    #endregion

    #region Properties Members
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when <see cref="Member">members</see> receive or lose roles.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerRolesUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberBanned" />
    /// <seealso cref="MemberUnbanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ServerAdded" />
    public IObservable<RolesUpdatedEvent> MemberRolesUpdated => ((IEventInfo<RolesUpdatedEvent>)GuildedEvents["ServerRolesUpdated"]).Observable;

    /// <inheritdoc cref="MemberRolesUpdated" />
    [Obsolete($"Renamed to `{nameof(MemberRolesUpdated)}` due to possible confusion with RoleUpdated")]
    public IObservable<RolesUpdatedEvent> RolesUpdated => MemberRolesUpdated;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when server-wide profile of a <see cref="Member">member</see> gets changed.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerMemberUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="RolesUpdated" />
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
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberUnbanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ServerAdded" />
    public IObservable<MemberBanEvent> MemberBanned => ((IEventInfo<MemberBanEvent>)GuildedEvents["ServerMemberBanned"]).Observable;

    /// <inheritdoc cref="MemberBanRemoved" />
    [Obsolete("Use `MemberBanned` instead")]
    public IObservable<MemberBanEvent> MemberBanAdded => MemberBanned;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when <see cref="User">user</see> gets unbanned in a <see cref="Server">server</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerMemberUnbanned</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="RolesUpdated" />
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberBanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ServerAdded" />
    public IObservable<MemberBanEvent> MemberUnbanned => ((IEventInfo<MemberBanEvent>)GuildedEvents["ServerMemberUnbanned"]).Observable;

    /// <inheritdoc cref="MemberUnbanned" />
    [Obsolete("Use `MemberUnbanned` instead")]
    public IObservable<MemberBanEvent> MemberBanRemoved => MemberUnbanned;
    #endregion

    #region Properties Member Social Links
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="SocialLink">social link</see> is added to <see cref="Member">member's</see> profile.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerMemberSocialLinkCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MemberSocialLinkUpdated" />
    /// <seealso cref="MemberSocialLinkDeleted" />
    public IObservable<MemberSocialLinkEvent> MemberSocialLinkCreated => ((IEventInfo<MemberSocialLinkEvent>)GuildedEvents["ServerMemberSocialLinkCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="SocialLink">social link</see> in <see cref="Member">member's</see> profile gets updated.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerMemberSocialLinkUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MemberSocialLinkCreated" />
    /// <seealso cref="MemberSocialLinkDeleted" />
    public IObservable<MemberSocialLinkEvent> MemberSocialLinkUpdated => ((IEventInfo<MemberSocialLinkEvent>)GuildedEvents["ServerMemberSocialLinkUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="SocialLink">social link</see> in <see cref="Member">member's</see> profile gets deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerMemberSocialLinkDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MemberSocialLinkCreated" />
    /// <seealso cref="MemberSocialLinkUpdated" />
    public IObservable<MemberSocialLinkEvent> MemberSocialLinkDeleted => ((IEventInfo<MemberSocialLinkEvent>)GuildedEvents["ServerMemberSocialLinkDeleted"]).Observable;
    #endregion

    #region Properties User status
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when <see cref="UserStatus">user status</see> is added or changed.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>UserStatusCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="UserStatusDeleted" />
    public IObservable<UserStatusEvent> UserStatusCreated => ((IEventInfo<UserStatusEvent>)GuildedEvents["UserStatusCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when <see cref="UserStatus">user status</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>UserStatusDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="UserStatusCreated" />
    public IObservable<UserStatusEvent> UserStatusDeleted => ((IEventInfo<UserStatusEvent>)GuildedEvents["UserStatusDeleted"]).Observable;
    #endregion

    #region Properties Channels
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a new <see cref="Category">category</see> is created.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CategoryCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="CategoryUpdated" />
    /// <seealso cref="CategoryDeleted" />
    public IObservable<CategoryEvent> CategoryCreated => ((IEventInfo<CategoryEvent>)GuildedEvents["CategoryCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Category">category</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CategoryUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="CategoryCreated" />
    /// <seealso cref="CategoryDeleted" />
    public IObservable<CategoryEvent> CategoryUpdated => ((IEventInfo<CategoryEvent>)GuildedEvents["CategoryUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Category">category</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CategoryDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="CategoryCreated" />
    /// <seealso cref="CategoryUpdated" />
    public IObservable<CategoryEvent> CategoryDeleted => ((IEventInfo<CategoryEvent>)GuildedEvents["CategoryDeleted"]).Observable;
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
    /// <seealso cref="ChannelArchived" />
    /// <seealso cref="ChannelRestored" />
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
    /// <seealso cref="ChannelArchived" />
    /// <seealso cref="ChannelRestored" />
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
    /// <seealso cref="ChannelArchived" />
    /// <seealso cref="ChannelRestored" />
    public IObservable<ChannelEvent> ChannelDeleted => ((IEventInfo<ChannelEvent>)GuildedEvents["ServerChannelDeleted"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="ServerChannel">channel</see> is archived.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChannelArchived</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ChannelCreated" />
    /// <seealso cref="ChannelUpdated" />
    /// <seealso cref="ChannelDeleted" />
    /// <seealso cref="ChannelRestored" />
    public IObservable<ChannelEvent> ChannelArchived => ((IEventInfo<ChannelEvent>)GuildedEvents["ChannelArchived"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="ServerChannel">channel</see> is restored from being archived.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChannelRestored</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ChannelCreated" />
    /// <seealso cref="ChannelUpdated" />
    /// <seealso cref="ChannelDeleted" />
    /// <seealso cref="ChannelArchived" />
    public IObservable<ChannelEvent> ChannelRestored => ((IEventInfo<ChannelEvent>)GuildedEvents["ChannelRestored"]).Observable;
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
    /// <seealso cref="MessagePinned" />
    /// <seealso cref="MessageUnpinned" />
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
    /// <seealso cref="MessagePinned" />
    /// <seealso cref="MessageUnpinned" />
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
    /// <seealso cref="MessagePinned" />
    /// <seealso cref="MessageUnpinned" />
    /// <seealso cref="MessageReactionAdded" />
    /// <seealso cref="MessageReactionRemoved" />
    public IObservable<MessageDeletedEvent> MessageDeleted => ((IEventInfo<MessageDeletedEvent>)GuildedEvents["ChatMessageDeleted"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Message">message</see> pin is added.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChannelMessagePinned</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MessageCreated" />
    /// <seealso cref="MessageUpdated" />
    /// <seealso cref="MessageDeleted" />
    /// <seealso cref="MessageUnpinned" />
    /// <seealso cref="MessageReactionAdded" />
    /// <seealso cref="MessageReactionRemoved" />
    public IObservable<MessageEvent> MessagePinned => ((IEventInfo<MessageEvent>)GuildedEvents["ChannelMessagePinned"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Message">message</see> pin is removed.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChannelMessageUnpinned</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MessageCreated" />
    /// <seealso cref="MessageUpdated" />
    /// <seealso cref="MessageDeleted" />
    /// <seealso cref="MessagePinned" />
    /// <seealso cref="MessageReactionAdded" />
    /// <seealso cref="MessageReactionRemoved" />
    public IObservable<MessageEvent> MessageUnpinned => ((IEventInfo<MessageEvent>)GuildedEvents["ChannelMessageUnppinned"]).Observable;

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
    public IObservable<TopicCommentReactionEvent> TopicCommentReactionAdded => ((IEventInfo<TopicCommentReactionEvent>)GuildedEvents["ForumTopicCommentReactionCreated"]).Observable;

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
    public IObservable<TopicCommentReactionEvent> TopicCommentReactionRemoved => ((IEventInfo<TopicCommentReactionEvent>)GuildedEvents["ForumTopicCommentReactionDeleted"]).Observable;
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

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="DocReaction">reaction</see> is added to a <see cref="Doc">document</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>DocReactionCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="DocCreated" />
    /// <seealso cref="DocUpdated" />
    /// <seealso cref="DocDeleted" />
    /// <seealso cref="DocReactionRemoved" />
    public IObservable<DocReactionEvent> DocReactionAdded => ((IEventInfo<DocReactionEvent>)GuildedEvents["DocReactionCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="DocReaction">reaction</see> is added to a <see cref="Doc">document</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>DocReactionDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="DocCreated" />
    /// <seealso cref="DocUpdated" />
    /// <seealso cref="DocDeleted" />
    /// <seealso cref="DocReactionAdded" />
    public IObservable<DocReactionEvent> DocReactionRemoved => ((IEventInfo<DocReactionEvent>)GuildedEvents["DocReactionDeleted"]).Observable;
    #endregion

    #region Properties Docs channels > Comments
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a new <see cref="DocComment">document comment</see> is posted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>DocCommentCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="DocCommentUpdated" />
    /// <seealso cref="DocCommentDeleted" />
    /// <seealso cref="DocCommentReactionAdded" />
    /// <seealso cref="DocCommentReactionRemoved" />
    public IObservable<DocCommentEvent> DocCommentCreated => ((IEventInfo<DocCommentEvent>)GuildedEvents["DocCommentCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="DocComment">document comment</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>DocCommentUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="DocCommentCreated" />
    /// <seealso cref="DocCommentDeleted" />
    /// <seealso cref="DocCommentReactionAdded" />
    /// <seealso cref="DocCommentReactionRemoved" />
    public IObservable<DocCommentEvent> DocCommentUpdated => ((IEventInfo<DocCommentEvent>)GuildedEvents["DocCommentUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="DocComment">document comment</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>DocCommentDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="DocCommentCreated" />
    /// <seealso cref="DocCommentUpdated" />
    /// <seealso cref="DocCommentReactionAdded" />
    /// <seealso cref="DocCommentReactionRemoved" />
    public IObservable<DocCommentEvent> DocCommentDeleted => ((IEventInfo<DocCommentEvent>)GuildedEvents["DocCommentDeleted"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="DocCommentReaction">reaction</see> is added to a <see cref="DocComment">document comment</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>DocCommentReactionCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="DocCommentCreated" />
    /// <seealso cref="DocCommentUpdated" />
    /// <seealso cref="DocCommentDeleted" />
    /// <seealso cref="DocCommentReactionRemoved" />
    public IObservable<DocCommentReactionEvent> DocCommentReactionAdded => ((IEventInfo<DocCommentReactionEvent>)GuildedEvents["DocCommentReactionCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="DocCommentReaction">reaction</see> is added to a <see cref="DocComment">document comment</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>DocCommentReactionDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="DocCommentCreated" />
    /// <seealso cref="DocCommentUpdated" />
    /// <seealso cref="DocCommentDeleted" />
    /// <seealso cref="DocCommentReactionAdded" />
    public IObservable<DocCommentReactionEvent> DocCommentReactionRemoved => ((IEventInfo<DocCommentReactionEvent>)GuildedEvents["DocCommentReactionDeleted"]).Observable;
    #endregion

    #region Properties Announcements channels
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a new <see cref="Announcement">announcement</see> is posted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>AnnouncementCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="AnnouncementUpdated" />
    /// <seealso cref="AnnouncementDeleted" />
    public IObservable<AnnouncementEvent> AnnouncementCreated => ((IEventInfo<AnnouncementEvent>)GuildedEvents["AnnouncementCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Announcement">announcement</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>AnnouncementUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="AnnouncementCreated" />
    /// <seealso cref="AnnouncementDeleted" />
    public IObservable<AnnouncementEvent> AnnouncementUpdated => ((IEventInfo<AnnouncementEvent>)GuildedEvents["AnnouncementUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Announcement">announcement</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>AnnouncementDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="AnnouncementCreated" />
    /// <seealso cref="AnnouncementUpdated" />
    public IObservable<AnnouncementEvent> AnnouncementDeleted => ((IEventInfo<AnnouncementEvent>)GuildedEvents["AnnouncementDeleted"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="AnnouncementReaction">reaction</see> is added to a <see cref="Announcement">announcement</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>AnnouncementReactionCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="AnnouncementCreated" />
    /// <seealso cref="AnnouncementUpdated" />
    /// <seealso cref="AnnouncementDeleted" />
    /// <seealso cref="AnnouncementReactionRemoved" />
    public IObservable<AnnouncementReactionEvent> AnnouncementReactionAdded => ((IEventInfo<AnnouncementReactionEvent>)GuildedEvents["AnnouncementReactionCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="AnnouncementReaction">reaction</see> is added to a <see cref="Announcement">announcement</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>AnnouncementReactionDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="AnnouncementCreated" />
    /// <seealso cref="AnnouncementUpdated" />
    /// <seealso cref="AnnouncementDeleted" />
    /// <seealso cref="AnnouncementReactionAdded" />
    public IObservable<AnnouncementReactionEvent> AnnouncementReactionRemoved => ((IEventInfo<AnnouncementReactionEvent>)GuildedEvents["AnnouncementReactionDeleted"]).Observable;
    #endregion

    #region Properties Announcements channels > Comments
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a new <see cref="AnnouncementComment">announcement comment</see> is posted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>AnnouncementCommentCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="AnnouncementCommentUpdated" />
    /// <seealso cref="AnnouncementCommentDeleted" />
    /// <seealso cref="AnnouncementCommentReactionAdded" />
    /// <seealso cref="AnnouncementCommentReactionRemoved" />
    public IObservable<AnnouncementCommentEvent> AnnouncementCommentCreated => ((IEventInfo<AnnouncementCommentEvent>)GuildedEvents["AnnouncementCommentCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="AnnouncementComment">announcement comment</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>AnnouncementCommentUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="AnnouncementCommentCreated" />
    /// <seealso cref="AnnouncementCommentDeleted" />
    /// <seealso cref="AnnouncementCommentReactionAdded" />
    /// <seealso cref="AnnouncementCommentReactionRemoved" />
    public IObservable<AnnouncementCommentEvent> AnnouncementCommentUpdated => ((IEventInfo<AnnouncementCommentEvent>)GuildedEvents["AnnouncementCommentUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="AnnouncementComment">announcement comment</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>AnnouncementCommentDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="AnnouncementCommentCreated" />
    /// <seealso cref="AnnouncementCommentUpdated" />
    /// <seealso cref="AnnouncementCommentReactionAdded" />
    /// <seealso cref="AnnouncementCommentReactionRemoved" />
    public IObservable<AnnouncementCommentEvent> AnnouncementCommentDeleted => ((IEventInfo<AnnouncementCommentEvent>)GuildedEvents["AnnouncementCommentDeleted"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="AnnouncementCommentReaction">reaction</see> is added to a <see cref="AnnouncementComment">announcement comment</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>AnnouncementCommentReactionCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="AnnouncementCommentCreated" />
    /// <seealso cref="AnnouncementCommentUpdated" />
    /// <seealso cref="AnnouncementCommentDeleted" />
    /// <seealso cref="AnnouncementCommentReactionRemoved" />
    public IObservable<AnnouncementCommentReactionEvent> AnnouncementCommentReactionAdded => ((IEventInfo<AnnouncementCommentReactionEvent>)GuildedEvents["AnnouncementCommentReactionCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="AnnouncementCommentReaction">reaction</see> is added to a <see cref="AnnouncementComment">announcement comment</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>AnnouncementCommentReactionDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="AnnouncementCommentCreated" />
    /// <seealso cref="AnnouncementCommentUpdated" />
    /// <seealso cref="AnnouncementCommentDeleted" />
    /// <seealso cref="AnnouncementCommentReactionAdded" />
    public IObservable<AnnouncementCommentReactionEvent> AnnouncementCommentReactionRemoved => ((IEventInfo<AnnouncementCommentReactionEvent>)GuildedEvents["AnnouncementCommentReactionDeleted"]).Observable;
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

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="CalendarEventReaction">reaction</see> is added to a <see cref="CalendarEvent">calendar event</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventReactionCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="EventCreated" />
    /// <seealso cref="EventUpdated" />
    /// <seealso cref="EventDeleted" />
    /// <seealso cref="EventReactionRemoved" />
    public IObservable<CalendarEventReactionEvent> EventReactionAdded => ((IEventInfo<CalendarEventReactionEvent>)GuildedEvents["CalendarEventReactionCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="CalendarEventReaction">reaction</see> is added to a <see cref="CalendarEvent">calendar event</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventReactionDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="EventCreated" />
    /// <seealso cref="EventUpdated" />
    /// <seealso cref="EventDeleted" />
    /// <seealso cref="EventReactionAdded" />
    public IObservable<CalendarEventReactionEvent> EventReactionRemoved => ((IEventInfo<CalendarEventReactionEvent>)GuildedEvents["CalendarEventReactionDeleted"]).Observable;
    #endregion

    #region Properties Calendar channels > RSVPs
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="CalendarEventRsvp">RSVP</see> of a <see cref="CalendarEvent">calendar event</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventRsvpUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="EventRsvpManyUpdated" />
    /// <seealso cref="EventRsvpDeleted" />
    /// <seealso cref="EventUpdated" />
    /// <seealso cref="EventDeleted" />
    public IObservable<CalendarEventRsvpEvent> EventRsvpUpdated => ((IEventInfo<CalendarEventRsvpEvent>)GuildedEvents["CalendarEventRsvpUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when multiple <see cref="CalendarEventRsvp">RSVPs</see> of a <see cref="CalendarEvent">calendar event</see> are edited or created.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventRsvpManyUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="EventRsvpUpdated" />
    /// <seealso cref="EventRsvpDeleted" />
    /// <seealso cref="EventCreated" />
    /// <seealso cref="EventDeleted" />
    public IObservable<CalendarEventRsvpManyEvent> EventRsvpManyUpdated => ((IEventInfo<CalendarEventRsvpManyEvent>)GuildedEvents["CalendarEventRsvpManyUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="CalendarEventRsvp">RSVP</see> of a <see cref="CalendarEvent">calendar event</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventRsvpDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="EventRsvpUpdated" />
    /// <seealso cref="EventRsvpManyUpdated" />
    /// <seealso cref="EventCreated" />
    /// <seealso cref="EventUpdated" />
    public IObservable<CalendarEventRsvpEvent> EventRsvpDeleted => ((IEventInfo<CalendarEventRsvpEvent>)GuildedEvents["CalendarEventRsvpDeleted"]).Observable;
    #endregion

    #region Properties Calendar channels > Series
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="CalendarEventSeries">calendar event series</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventSeriesUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="EventSeriesDeleted" />
    public IObservable<CalendarEventSeriesEvent> EventSeriesUpdated => ((IEventInfo<CalendarEventSeriesEvent>)GuildedEvents["CalendarEventSeriesUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="CalendarEventSeries">calendar event series</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventSeriesDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="EventSeriesUpdated" />
    public IObservable<CalendarEventSeriesEvent> EventSeriesDeleted => ((IEventInfo<CalendarEventSeriesEvent>)GuildedEvents["CalendarEventSeriesDeleted"]).Observable;
    #endregion

    #region Properties Calendar channels > Comments
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a new <see cref="CalendarEventComment">calendar event comment</see> is posted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventCommentCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="EventCommentUpdated" />
    /// <seealso cref="EventCommentDeleted" />
    /// <seealso cref="EventCommentReactionAdded" />
    /// <seealso cref="EventCommentReactionRemoved" />
    public IObservable<CalendarEventCommentEvent> EventCommentCreated => ((IEventInfo<CalendarEventCommentEvent>)GuildedEvents["CalendarEventCommentCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="CalendarEventComment">calendar event comment</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventCommentUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="EventCommentCreated" />
    /// <seealso cref="EventCommentDeleted" />
    /// <seealso cref="EventCommentReactionAdded" />
    /// <seealso cref="EventCommentReactionRemoved" />
    public IObservable<CalendarEventCommentEvent> EventCommentUpdated => ((IEventInfo<CalendarEventCommentEvent>)GuildedEvents["CalendarEventCommentUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="CalendarEventComment">calendar event comment</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventCommentDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="EventCommentCreated" />
    /// <seealso cref="EventCommentUpdated" />
    /// <seealso cref="EventCommentReactionAdded" />
    /// <seealso cref="EventCommentReactionRemoved" />
    public IObservable<CalendarEventCommentEvent> EventCommentDeleted => ((IEventInfo<CalendarEventCommentEvent>)GuildedEvents["CalendarEventCommentDeleted"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="CalendarEventCommentReaction">reaction</see> is added to a <see cref="CalendarEventComment">calendar event comment</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventCommentReactionCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="EventCommentCreated" />
    /// <seealso cref="EventCommentUpdated" />
    /// <seealso cref="EventCommentDeleted" />
    /// <seealso cref="EventCommentReactionRemoved" />
    public IObservable<CalendarEventCommentReactionEvent> EventCommentReactionAdded => ((IEventInfo<CalendarEventCommentReactionEvent>)GuildedEvents["CalendarEventCommentReactionCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="CalendarEventCommentReaction">reaction</see> is added to a <see cref="CalendarEventComment">calendar event comment</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventCommentReactionDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="EventCommentCreated" />
    /// <seealso cref="EventCommentUpdated" />
    /// <seealso cref="EventCommentDeleted" />
    /// <seealso cref="EventCommentReactionAdded" />
    public IObservable<CalendarEventCommentReactionEvent> EventCommentReactionRemoved => ((IEventInfo<CalendarEventCommentReactionEvent>)GuildedEvents["CalendarEventCommentReactionDeleted"]).Observable;
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