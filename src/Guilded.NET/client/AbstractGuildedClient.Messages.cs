using System;
using System.Collections.Generic;
using Guilded.NET.Base.Events;

namespace Guilded.NET
{
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
        protected Dictionary<object, IEventInfo<object>> GuildedEvents
        {
            get; set;
        }

        #region WebSocket
        /// <inheritdoc cref="WelcomeEvent"/>
        /// <seealso cref="Resume"/>
        public IObservable<WelcomeEvent> Welcome => ((IEventInfo<WelcomeEvent>)GuildedEvents[1]).Observable;

        /// <inheritdoc cref="ResumeEvent"/>
        /// <seealso cref="Welcome"/>
        public IObservable<ResumeEvent> Resume => ((IEventInfo<ResumeEvent>)GuildedEvents[2]).Observable;
        /// <summary>
        /// Event that occurs when pong is received.
        /// </summary>
        /// <remarks>
        /// <para>An event of opcode <c>10</c> that is received as a response once client sends a ping.</para>
        /// <para>This does not have any associated data with it.</para>
        /// </remarks>
        public IObservable<GuildedSocketMessage> Pong => ((IEventInfo<GuildedSocketMessage>)GuildedEvents[10]).Observable;
        #endregion

        #region Teams
        /// <inheritdoc cref="XpAddedEvent"/>
        /// <seealso cref="RolesUpdated"/>
        /// <seealso cref="MemberUpdated"/>
        public IObservable<XpAddedEvent> XpAdded => ((IEventInfo<XpAddedEvent>)GuildedEvents["TeamXpAdded"]).Observable;

        /// <inheritdoc cref="RolesUpdatedEvent"/>
        /// <seealso cref="XpAdded"/>
        /// <seealso cref="MemberUpdated"/>
        public IObservable<RolesUpdatedEvent> RolesUpdated => ((IEventInfo<RolesUpdatedEvent>)GuildedEvents["teamRolesUpdated"]).Observable;

        /// <inheritdoc cref="MemberUpdatedEvent"/>
        /// <seealso cref="RolesUpdated"/>
        /// <seealso cref="XpAdded"/>
        public IObservable<MemberUpdatedEvent> MemberUpdated => ((IEventInfo<MemberUpdatedEvent>)GuildedEvents["TeamMemberUpdated"]).Observable;
        #endregion

        #region Chat channels
        /// <inheritdoc cref="MessageCreatedEvent"/>
        /// <seealso cref="MessageUpdated"/>
        /// <seealso cref="MessageDeleted"/>
        public IObservable<MessageCreatedEvent> MessageCreated => ((IEventInfo<MessageCreatedEvent>)GuildedEvents["ChatMessageCreated"]).Observable;

        /// <inheritdoc cref="MessageUpdatedEvent"/>
        /// <seealso cref="MessageCreated"/>
        /// <seealso cref="MessageDeleted"/>
        public IObservable<MessageUpdatedEvent> MessageUpdated => ((IEventInfo<MessageUpdatedEvent>)GuildedEvents["ChatMessageUpdated"]).Observable;
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
            if (message is null) return;

            object eventName = message.EventName ?? (object)message.Opcode;
            // Checks if this event is supported by Guilded.NET
            if (GuildedEvents.ContainsKey(eventName))
            {
                IEventInfo<object> ev = GuildedEvents?[eventName];

                if (ev != default)
                {
                    object data = message.RawData.ToObject(ev?.ArgumentType, GuildedSerializer);

                    ev.OnNext(data);
                }
            }
        }
        #endregion
    }
}