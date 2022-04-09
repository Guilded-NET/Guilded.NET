using System;
using System.Collections.Generic;

using Guilded.Base.Events;
using Newtonsoft.Json.Linq;

namespace Guilded;

public abstract partial class AbstractGuildedClient
{
    /// <summary>
    /// A dictionary of Guilded events.
    /// </summary>
    /// <remarks>
    /// <para>A dictionary of all supported Guilded events, containing their event names and information about the event to use.</para>
    /// <para>You can add more events to this dictionary if Guilded.NET does not support certain events.</para>
    /// </remarks>
    /// <value>Dictionary of events</value>
    protected Dictionary<object, IEventInfo<object>> GuildedEvents { get; set; }

    #region WebSocket
    /// <inheritdoc cref="WelcomeEvent"/>
    /// <seealso cref="Resume"/>
    public IObservable<WelcomeEvent> Welcome => ((IEventInfo<WelcomeEvent>)GuildedEvents[(byte)1]).Observable;

    /// <inheritdoc cref="ResumeEvent"/>
    /// <seealso cref="Welcome"/>
    public IObservable<ResumeEvent> Resume => ((IEventInfo<ResumeEvent>)GuildedEvents[(byte)2]).Observable;
    #endregion

    #region Teams
    /// <inheritdoc cref="XpAddedEvent"/>
    /// <seealso cref="MemberJoined"/>
    /// <seealso cref="MemberUpdated"/>
    /// <seealso cref="RolesUpdated"/>
    /// <seealso cref="MemberRemoved"/>
    /// <seealso cref="MemberBanned"/>
    /// <seealso cref="MemberUnbanned"/>
    /// <seealso cref="WebhookCreated"/>
    /// <seealso cref="WebhookUpdated"/>
    public IObservable<XpAddedEvent> XpAdded => ((IEventInfo<XpAddedEvent>)GuildedEvents["TeamXpAdded"]).Observable;

    /// <inheritdoc cref="RolesUpdatedEvent"/>
    /// <seealso cref="MemberJoined"/>
    /// <seealso cref="MemberUpdated"/>
    /// <seealso cref="XpAdded"/>
    /// <seealso cref="MemberRemoved"/>
    /// <seealso cref="MemberBanned"/>
    /// <seealso cref="MemberUnbanned"/>
    /// <seealso cref="WebhookCreated"/>
    /// <seealso cref="WebhookUpdated"/>
    public IObservable<RolesUpdatedEvent> RolesUpdated => ((IEventInfo<RolesUpdatedEvent>)GuildedEvents["teamRolesUpdated"]).Observable;

    /// <inheritdoc cref="MemberUpdatedEvent"/>
    /// <seealso cref="MemberJoined"/>
    /// <seealso cref="RolesUpdated"/>
    /// <seealso cref="XpAdded"/>
    /// <seealso cref="MemberRemoved"/>
    /// <seealso cref="MemberBanned"/>
    /// <seealso cref="MemberUnbanned"/>
    /// <seealso cref="WebhookCreated"/>
    /// <seealso cref="WebhookUpdated"/>
    public IObservable<MemberUpdatedEvent> MemberUpdated => ((IEventInfo<MemberUpdatedEvent>)GuildedEvents["TeamMemberUpdated"]).Observable;
    /// <inheritdoc cref="MemberJoinedEvent"/>
    /// <seealso cref="MemberUpdated"/>
    /// <seealso cref="RolesUpdated"/>
    /// <seealso cref="XpAdded"/>
    /// <seealso cref="MemberRemoved"/>
    /// <seealso cref="MemberBanned"/>
    /// <seealso cref="MemberUnbanned"/>
    /// <seealso cref="WebhookCreated"/>
    /// <seealso cref="WebhookUpdated"/>
    public IObservable<MemberJoinedEvent> MemberJoined => ((IEventInfo<MemberJoinedEvent>)GuildedEvents["TeamMemberJoined"]).Observable;
    /// <inheritdoc cref="MemberRemovedEvent"/>
    /// <seealso cref="MemberJoined"/>
    /// <seealso cref="MemberUpdated"/>
    /// <seealso cref="RolesUpdated"/>
    /// <seealso cref="XpAdded"/>
    /// <seealso cref="MemberBanned"/>
    /// <seealso cref="MemberUnbanned"/>
    /// <seealso cref="WebhookCreated"/>
    /// <seealso cref="WebhookUpdated"/>
    public IObservable<MemberRemovedEvent> MemberRemoved => ((IEventInfo<MemberRemovedEvent>)GuildedEvents["TeamMemberRemoved"]).Observable;
    /// <inheritdoc cref="MemberBanEvent"/>
    /// <seealso cref="MemberJoined"/>
    /// <seealso cref="MemberUpdated"/>
    /// <seealso cref="RolesUpdated"/>
    /// <seealso cref="XpAdded"/>
    /// <seealso cref="MemberRemoved"/>
    /// <seealso cref="MemberUnbanned"/>
    /// <seealso cref="WebhookCreated"/>
    /// <seealso cref="WebhookUpdated"/>
    public IObservable<MemberBanEvent> MemberBanned => ((IEventInfo<MemberBanEvent>)GuildedEvents["TeamMemberBanned"]).Observable;
    /// <inheritdoc cref="MemberBanEvent"/>
    /// <seealso cref="MemberJoined"/>
    /// <seealso cref="MemberUpdated"/>
    /// <seealso cref="RolesUpdated"/>
    /// <seealso cref="XpAdded"/>
    /// <seealso cref="MemberRemoved"/>
    /// <seealso cref="MemberBanned"/>
    /// <seealso cref="WebhookCreated"/>
    /// <seealso cref="WebhookUpdated"/>
    public IObservable<MemberBanEvent> MemberUnbanned => ((IEventInfo<MemberBanEvent>)GuildedEvents["TeamMemberUnbanned"]).Observable;
    /// <inheritdoc cref="WebhookEvent"/>
    /// <seealso cref="MemberJoined"/>
    /// <seealso cref="MemberUpdated"/>
    /// <seealso cref="RolesUpdated"/>
    /// <seealso cref="XpAdded"/>
    /// <seealso cref="MemberRemoved"/>
    /// <seealso cref="MemberBanned"/>
    /// <seealso cref="MemberUnbanned"/>
    /// <seealso cref="WebhookUpdated"/>
    public IObservable<WebhookEvent> WebhookCreated => ((IEventInfo<WebhookEvent>)GuildedEvents["TeamWebhookCreated"]).Observable;
    /// <inheritdoc cref="WebhookEvent"/>
    /// <seealso cref="MemberJoined"/>
    /// <seealso cref="MemberUpdated"/>
    /// <seealso cref="RolesUpdated"/>
    /// <seealso cref="XpAdded"/>
    /// <seealso cref="MemberRemoved"/>
    /// <seealso cref="MemberBanned"/>
    /// <seealso cref="MemberUnbanned"/>
    /// <seealso cref="WebhookCreated"/>
    public IObservable<WebhookEvent> WebhookUpdated => ((IEventInfo<WebhookEvent>)GuildedEvents["TeamWebhookUpdated"]).Observable;
    #endregion

    #region Chat channels
    /// <inheritdoc cref="MessageEvent"/>
    /// <seealso cref="MessageUpdated"/>
    /// <seealso cref="MessageDeleted"/>
    public IObservable<MessageEvent> MessageCreated => ((IEventInfo<MessageEvent>)GuildedEvents["ChatMessageCreated"]).Observable;
    /// <inheritdoc cref="MessageEvent"/>
    /// <seealso cref="MessageCreated"/>
    /// <seealso cref="MessageDeleted"/>
    public IObservable<MessageEvent> MessageUpdated => ((IEventInfo<MessageEvent>)GuildedEvents["ChatMessageUpdated"]).Observable;
    /// <inheritdoc cref="MessageDeletedEvent"/>
    /// <seealso cref="MessageUpdated"/>
    /// <seealso cref="MessageUpdated"/>
    public IObservable<MessageDeletedEvent> MessageDeleted => ((IEventInfo<MessageDeletedEvent>)GuildedEvents["ChatMessageDeleted"]).Observable;
    #endregion

    #region Handling
    /// <summary>
    /// When the socket message event is invoked.
    /// </summary>
    /// <remarks>
    /// <para>Receives and handles received <see cref="GuildedSocketMessage"/> messages.</para>
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
}