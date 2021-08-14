using System;
using System.Collections.Generic;

namespace Guilded.NET
{
    using Base.Events;
    /// <summary>
    /// A base for all Guilded clients.
    /// </summary>
    /// <seealso cref="GuildedBotClient"/>
    /// <seealso cref="Base.BaseGuildedClient"/>
    public abstract partial class GuildedClient
    {
        /// <summary>
        /// A dictionary of Guilded events.
        /// </summary>
        /// <remarks>
        /// <para>A dictionary of all supported Guilded events, containing their event names and information about the event to use.</para>
        /// <para>Use this if you need to support events that Guilded.NET does not.</para>
        /// </remarks>
        /// <value>Dictionary of events</value>
        protected Dictionary<string, EventInfo> GuildedEvents
        {
            get; set;
        }

        /// <summary>
        /// An event when WebSocket receives a welcome.
        /// </summary>
        /// <remarks>
        /// This event is received once WebSocket is initiated and is confirmed on Guilded's side.
        /// </remarks>
        protected EventInfo WelcomeEvent => GuildedEvents[""];
        /// <summary>
        /// An event when WebSocket receives a welcome.
        /// </summary>
        /// <remarks>
        /// This event is received once WebSocket is initiated and is confirmed on Guilded's side.
        /// </remarks>
        public event ClientEventHandler<WelcomeEvent> Welcome
        {
            add => WelcomeEvent.Invokable = Delegate.Combine(WelcomeEvent.Invokable, value);
            remove => WelcomeEvent.Invokable = Delegate.Remove(WelcomeEvent.Invokable, value);
        }

        #region Teams
        /// <summary>
        /// When a list of users get certain amount of XP.
        /// </summary>
        /// <remarks>
        /// This event is received once a given list of users get certain amount of XP(between -1000 and 1000).
        /// </remarks>
        protected EventInfo XpAddedEvent => GuildedEvents["TeamXpAdded"];
        /// <summary>
        /// When a list of users get certain amount of XP.
        /// </summary>
        /// <remarks>
        /// This event is received once a given list of users get certain amount of XP(between -1000 and 1000).
        /// </remarks>
        public event ClientEventHandler<XpAddedEvent> XpAdded
        {
            add => XpAddedEvent.Invokable = Delegate.Combine(XpAddedEvent.Invokable, value);
            remove => XpAddedEvent.Invokable = Delegate.Remove(XpAddedEvent.Invokable, value);
        }
        #endregion

        #region Chat channels
        /// <summary>
        /// When a message gets posted in the chat.
        /// </summary>
        /// <remarks>
        /// This event is received when any type of message gets sent/created, including system messages.
        /// </remarks>
        protected EventInfo MessageCreatedEvent => GuildedEvents["ChatMessageCreated"];
        /// <summary>
        /// When a message gets posted in the chat.
        /// </summary>
        /// <remarks>
        /// This event is received when any type of message gets sent/created, including system messages.
        /// </remarks>
        public event ClientEventHandler<MessageCreatedEvent> MessageCreated
        {
            add => MessageCreatedEvent.Invokable = Delegate.Combine(MessageCreatedEvent.Invokable, value);
            remove => MessageCreatedEvent.Invokable = Delegate.Remove(MessageCreatedEvent.Invokable, value);
        }

        /// <summary>
        /// When a message gets updated/edited in the chat.
        /// </summary>
        /// <remarks>
        /// This event is received when a text message gets updated/edited.
        /// </remarks>
        protected EventInfo MessageUpdatedEvent => GuildedEvents["ChatMessageUpdated"];
        /// <summary>
        /// When a message gets updated/edited in the chat.
        /// </summary>
        /// <remarks>
        /// This event is received when a text message gets updated/edited.
        /// </remarks>
        public event ClientEventHandler<MessageUpdatedEvent> MessageUpdated
        {
            add => MessageUpdatedEvent.Invokable = Delegate.Combine(MessageUpdatedEvent.Invokable, value);
            remove => MessageUpdatedEvent.Invokable = Delegate.Remove(MessageUpdatedEvent.Invokable, value);
        }
        /// <summary>
        /// When a message gets removed/deleted in the chat.
        /// </summary>
        /// <remarks>
        /// This event is received when any type of message gets deleted, including system messages.
        /// </remarks>
        protected EventInfo MessageDeletedEvent => GuildedEvents["ChatMessageDeleted"];
        /// <summary>
        /// When a message gets removed/deleted in the chat.
        /// </summary>
        /// <remarks>
        /// This event is received when any type of message gets deleted, including system messages.
        /// </remarks>
        public event ClientEventHandler<MessageDeletedEvent> MessageDeleted
        {
            add => MessageDeletedEvent.Invokable = Delegate.Combine(MessageDeletedEvent.Invokable, value);
            remove => MessageDeletedEvent.Invokable = Delegate.Remove(MessageDeletedEvent.Invokable, value);
        }
        #endregion
    }
}