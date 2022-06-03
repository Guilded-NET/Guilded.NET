using System;
using System.Collections.Generic;
using Guilded.Base;
using Guilded.Base.Content;
using Guilded.Base.Events;
using Guilded.Base.Servers;
using Guilded.Base.Users;

namespace Guilded;

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
    /// Gets <see cref="IObservable{T}">the observable</see> for an event when <see cref="BaseGuildedClient.Websocket">WebSocket</see> is connected.
    /// </summary>
    /// <remarks>
    /// <para>An event with the opcode <c>1</c>.</para>
    /// </remarks>
    /// <seealso cref="Resume" />
    public IObservable<WelcomeEvent> Welcome => ((IEventInfo<WelcomeEvent>)GuildedEvents[SocketOpcode.Welcome]).Observable;

    /// <summary>
    /// Gets <see cref="IObservable{T}">the observable</see> for an event when all lost <see cref="GuildedSocketMessage">WebSocket messages</see> get re-sent.
    /// </summary>
    /// <remarks>
    /// <para>An event with an event opcode of <c>2</c>.</para>
    /// </remarks>
    /// <seealso cref="Welcome" />
    public IObservable<ResumeEvent> Resume => ((IEventInfo<ResumeEvent>)GuildedEvents[SocketOpcode.Resume]).Observable;
    #endregion

    #region Properties Teams
    /// <summary>
    /// Gets <see cref="IObservable{T}">the observable</see> for an event when <see cref="Member">members</see> receive <see cref="XpAddedEvent.Amount">XP</see>.
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
    /// Gets <see cref="IObservable{T}">the observable</see> for an event when <see cref="Member">members</see> receive or lose roles.
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
    /// Gets <see cref="IObservable{T}">the observable</see> for an event when server-wide profile of <see cref="Member">a member</see> gets changed.
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
    /// Gets <see cref="IObservable{T}">the observable</see> for an event when <see cref="Member">a member</see> joins <see cref="Server">a server</see>.
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
    /// Gets <see cref="IObservable{T}">the observable</see> for an event when <see cref="Member">a member</see> leaves <see cref="Server">a server</see>, gets kicked or gets banned from <see cref="Server">a server</see>.
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
    /// Gets <see cref="IObservable{T}">the observable</see> for an event when <see cref="Member">a member</see> gets banned from <see cref="Server">the server</see>.
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
    /// Gets <see cref="IObservable{T}">the observable</see> for an event when <see cref="User">user</see> gets unbanned in <see cref="Server">a server</see>.
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
    /// Gets <see cref="IObservable{T}">the observable</see> for an event when <see cref="Webhook">a new webhook</see> is created.
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
    /// Gets <see cref="IObservable{T}">the observable</see> for an event when <see cref="Webhook">a webhook</see> is edited.
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
    /// Gets <see cref="IObservable{T}">the observable</see> for an event when <see cref="ServerChannel">a new channel</see> is created.
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
    /// Gets <see cref="IObservable{T}">the observable</see> for an event when <see cref="ServerChannel">a channel</see> is edited.
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
    /// Gets <see cref="IObservable{T}">the observable</see> for an event when <see cref="ServerChannel">a channel</see> is deleted.
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
    /// Gets <see cref="IObservable{T}">the observable</see> for an event when <see cref="Message">a new message</see> is sent.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChatMessageUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MessageUpdated" />
    /// <seealso cref="MessageDeleted" />
    public IObservable<MessageEvent> MessageCreated => ((IEventInfo<MessageEvent>)GuildedEvents["ChatMessageCreated"]).Observable;

    /// <summary>
    /// Gets <see cref="IObservable{T}">the observable</see> for an event when <see cref="Message">a message</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChatMessageUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MessageCreated" />
    /// <seealso cref="MessageDeleted" />
    public IObservable<MessageEvent> MessageUpdated => ((IEventInfo<MessageEvent>)GuildedEvents["ChatMessageUpdated"]).Observable;

    /// <summary>
    /// Gets <see cref="IObservable{T}">the observable</see> for an event when <see cref="Message">a message</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChatMessageDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MessageCreated" />
    /// <seealso cref="MessageUpdated" />
    public IObservable<MessageDeletedEvent> MessageDeleted => ((IEventInfo<MessageDeletedEvent>)GuildedEvents["ChatMessageDeleted"]).Observable;
    #endregion

    #region Properties List channels
    /// <summary>
    /// Gets <see cref="IObservable{T}">the observable</see> for an event when <see cref="ListItem">a new list item</see> is posted.
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
    /// Gets <see cref="IObservable{T}">the observable</see> for an event when <see cref="ListItem">a list item</see> is edited.
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
    /// Gets <see cref="IObservable{T}">the observable</see> for an event when <see cref="ListItem">a list item</see> is deleted.
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
    /// Gets <see cref="IObservable{T}">the observable</see> for an event when <see cref="ListItem">a list item</see> is set as completed.
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
    /// Gets <see cref="IObservable{T}">the observable</see> for an event when <see cref="ListItem">a list item</see> is set as not completed.
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
    /// Gets <see cref="IObservable{T}">the observable</see> for an event when <see cref="Doc">a new document</see> is posted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>DocCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="DocUpdated" />
    /// <seealso cref="DocDeleted" />
    public IObservable<DocEvent> DocCreated => ((IEventInfo<DocEvent>)GuildedEvents["DocCreated"]).Observable;

    /// <summary>
    /// Gets <see cref="IObservable{T}">the observable</see> for an event when <see cref="Doc">a new document</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>DocUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="DocCreated" />
    /// <seealso cref="DocDeleted" />
    public IObservable<DocEvent> DocUpdated => ((IEventInfo<DocEvent>)GuildedEvents["DocUpdated"]).Observable;

    /// <summary>
    /// Gets <see cref="IObservable{T}">the observable</see> for an event when <see cref="Doc">a new document</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>DocDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="DocCreated" />
    /// <seealso cref="DocUpdated" />
    public IObservable<DocEvent> DocDeleted => ((IEventInfo<DocEvent>)GuildedEvents["DocDeleted"]).Observable;
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

            object data = message.RawData!.ToObject(ev.ArgumentType, GuildedSerializer)!;

            ev.OnNext(data);
        }
    }
    #endregion

    #endregion
}