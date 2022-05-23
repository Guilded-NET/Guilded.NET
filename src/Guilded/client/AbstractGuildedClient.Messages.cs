using System;
using System.Collections.Generic;

using Guilded.Base.Events;

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
    /// <inheritdoc cref="WelcomeEvent" />
    /// <seealso cref="Resume" />
    public IObservable<WelcomeEvent> Welcome => ((IEventInfo<WelcomeEvent>)GuildedEvents[(byte)1]).Observable;

    /// <inheritdoc cref="ResumeEvent" />
    /// <seealso cref="Welcome" />
    public IObservable<ResumeEvent> Resume => ((IEventInfo<ResumeEvent>)GuildedEvents[(byte)2]).Observable;
    #endregion

    #region Properties Teams
    /// <inheritdoc cref="XpAddedEvent" />
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="RolesUpdated" />
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberBanned" />
    /// <seealso cref="MemberUnbanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    public IObservable<XpAddedEvent> XpAdded => ((IEventInfo<XpAddedEvent>)GuildedEvents["TeamXpAdded"]).Observable;

    /// <inheritdoc cref="RolesUpdatedEvent" />
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
    /// <inheritdoc cref="MemberUpdatedEvent" />
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="RolesUpdated" />
    /// <seealso cref="XpAdded" />
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberBanned" />
    /// <seealso cref="MemberUnbanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    public IObservable<MemberUpdatedEvent> MemberUpdated => ((IEventInfo<MemberUpdatedEvent>)GuildedEvents["TeamMemberUpdated"]).Observable;

    /// <inheritdoc cref="MemberJoinedEvent" />
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="RolesUpdated" />
    /// <seealso cref="XpAdded" />
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberBanned" />
    /// <seealso cref="MemberUnbanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    public IObservable<MemberJoinedEvent> MemberJoined => ((IEventInfo<MemberJoinedEvent>)GuildedEvents["TeamMemberJoined"]).Observable;

    /// <inheritdoc cref="MemberRemovedEvent" />
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="RolesUpdated" />
    /// <seealso cref="XpAdded" />
    /// <seealso cref="MemberBanned" />
    /// <seealso cref="MemberUnbanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    public IObservable<MemberRemovedEvent> MemberRemoved => ((IEventInfo<MemberRemovedEvent>)GuildedEvents["TeamMemberRemoved"]).Observable;

    /// <inheritdoc cref="MemberBanEvent" />
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="RolesUpdated" />
    /// <seealso cref="XpAdded" />
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberUnbanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    public IObservable<MemberBanEvent> MemberBanned => ((IEventInfo<MemberBanEvent>)GuildedEvents["TeamMemberBanned"]).Observable;

    /// <inheritdoc cref="MemberBanEvent" />
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
    /// <inheritdoc cref="WebhookEvent" />
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="RolesUpdated" />
    /// <seealso cref="XpAdded" />
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberBanned" />
    /// <seealso cref="MemberUnbanned" />
    /// <seealso cref="WebhookUpdated" />
    public IObservable<WebhookEvent> WebhookCreated => ((IEventInfo<WebhookEvent>)GuildedEvents["TeamWebhookCreated"]).Observable;

    /// <inheritdoc cref="WebhookEvent" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="ChannelCreated" />
    /// <seealso cref="ChannelUpdated" />
    /// <seealso cref="ChannelDeleted" />
    public IObservable<WebhookEvent> WebhookUpdated => ((IEventInfo<WebhookEvent>)GuildedEvents["TeamWebhookUpdated"]).Observable;

    /// <inheritdoc cref="ChannelEvent" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ChannelUpdated" />
    /// <seealso cref="ChannelDeleted" />
    public IObservable<ChannelEvent> ChannelCreated => ((IEventInfo<ChannelEvent>)GuildedEvents["TeamChannelCreated"]).Observable;

    /// <inheritdoc cref="ChannelEvent" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ChannelCreated" />
    /// <seealso cref="ChannelDeleted" />
    public IObservable<ChannelEvent> ChannelUpdated => ((IEventInfo<ChannelEvent>)GuildedEvents["TeamChannelUpdated"]).Observable;

    /// <inheritdoc cref="ChannelEvent" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ChannelCreated" />
    /// <seealso cref="ChannelUpdated" />
    public IObservable<ChannelEvent> ChannelDeleted => ((IEventInfo<ChannelEvent>)GuildedEvents["TeamChannelDeleted"]).Observable;
    #endregion

    #region Properties Chat channels
    /// <inheritdoc cref="MessageEvent" />
    /// <seealso cref="MessageUpdated" />
    /// <seealso cref="MessageDeleted" />
    public IObservable<MessageEvent> MessageCreated => ((IEventInfo<MessageEvent>)GuildedEvents["ChatMessageCreated"]).Observable;

    /// <inheritdoc cref="MessageEvent" />
    /// <seealso cref="MessageCreated" />
    /// <seealso cref="MessageDeleted" />
    public IObservable<MessageEvent> MessageUpdated => ((IEventInfo<MessageEvent>)GuildedEvents["ChatMessageUpdated"]).Observable;

    /// <inheritdoc cref="MessageDeletedEvent" />
    /// <seealso cref="MessageCreated" />
    /// <seealso cref="MessageUpdated" />
    public IObservable<MessageDeletedEvent> MessageDeleted => ((IEventInfo<MessageDeletedEvent>)GuildedEvents["ChatMessageDeleted"]).Observable;
    #endregion

    #region Properties List channels
    /// <inheritdoc cref="ListItemEvent" />
    /// <seealso cref="ListItemUpdated" />
    /// <seealso cref="ListItemDeleted" />
    /// <seealso cref="ListItemCompleted" />
    /// <seealso cref="ListItemUncompleted" />
    public IObservable<ListItemEvent> ListItemCreated => ((IEventInfo<ListItemEvent>)GuildedEvents["ListItemCreated"]).Observable;

    /// <inheritdoc cref="ListItemEvent" />
    /// <seealso cref="ListItemCreated" />
    /// <seealso cref="ListItemDeleted" />
    /// <seealso cref="ListItemCompleted" />
    /// <seealso cref="ListItemUncompleted" />
    public IObservable<ListItemEvent> ListItemUpdated => ((IEventInfo<ListItemEvent>)GuildedEvents["ListItemUpdated"]).Observable;

    /// <inheritdoc cref="ListItemEvent" />
    /// <seealso cref="ListItemCreated" />
    /// <seealso cref="ListItemUpdated" />
    /// <seealso cref="ListItemCompleted" />
    /// <seealso cref="ListItemUncompleted" />
    public IObservable<ListItemEvent> ListItemDeleted => ((IEventInfo<ListItemEvent>)GuildedEvents["ListItemDeleted"]).Observable;

    /// <inheritdoc cref="ListItemEvent" />
    /// <seealso cref="ListItemUpdated" />
    /// <seealso cref="ListItemUpdated" />
    /// <seealso cref="ListItemCompleted" />
    /// <seealso cref="ListItemUncompleted" />
    public IObservable<ListItemEvent> ListItemCompleted => ((IEventInfo<ListItemEvent>)GuildedEvents["ListItemDeleted"]).Observable;

    /// <inheritdoc cref="ListItemEvent" />
    /// <seealso cref="ListItemUpdated" />
    /// <seealso cref="ListItemDeleted" />
    /// <seealso cref="ListItemCompleted" />
    /// <seealso cref="ListItemUncompleted" />
    public IObservable<ListItemEvent> ListItemUncompleted => ((IEventInfo<ListItemEvent>)GuildedEvents["ListItemDeleted"]).Observable;
    #endregion

    #region Properties Docs channels
    /// <inheritdoc cref="DocEvent" />
    /// <seealso cref="DocUpdated" />
    /// <seealso cref="DocDeleted" />
    public IObservable<DocEvent> DocCreated => ((IEventInfo<DocEvent>)GuildedEvents["DocCreated"]).Observable;

    /// <inheritdoc cref="DocEvent" />
    /// <seealso cref="DocCreated" />
    /// <seealso cref="DocDeleted" />
    public IObservable<DocEvent> DocUpdated => ((IEventInfo<DocEvent>)GuildedEvents["DocUpdated"]).Observable;

    /// <inheritdoc cref="DocEvent" />
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
        if (GuildedEvents?.ContainsKey(eventName) ?? false)
        {
            IEventInfo<object> ev = GuildedEvents[eventName];

            object data = message.RawData!.ToObject(ev.ArgumentType, GuildedSerializer)!;

            ev.OnNext(data);
        }
    }
    #endregion

    #endregion
}