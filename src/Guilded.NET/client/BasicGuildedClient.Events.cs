using System;
using System.Collections.Generic;

namespace Guilded.NET
{
    using Base.Events;
    /// <summary>
    /// A base for user bot clients and normal bot clients.
    /// </summary>
    public abstract partial class BasicGuildedClient
    {
        /// <summary>
        /// A list of all events that could be received from Guilded and their names.
        /// </summary>
        /// <value>Dictionary of events</value>
        protected Dictionary<string, EventInfo> GuildedEvents
        {
            get; set;
        }

        /// <summary>
        /// Information of the event when WebSocket receives welcome message.
        /// </summary>
        protected EventInfo WelcomeEvent => GuildedEvents[""];
        /// <summary>
        /// When WebSocket receives welcome message.
        /// </summary>
        public event ClientEventHandler<WelcomeEvent> Welcome
        {
            add => WelcomeEvent.Invokable = Delegate.Combine(WelcomeEvent.Invokable, value);
            remove => WelcomeEvent.Invokable = Delegate.Remove(WelcomeEvent.Invokable, value);
        }

        #region Teams
        /// <summary>
        /// When a list of users get certain amount of XP.
        /// </summary>
        protected EventInfo XpAddedEvent => GuildedEvents["TeamXpAdded"];
        /// <summary>
        /// When a list of users get certain amount of XP.
        /// </summary>
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
        protected EventInfo MessageCreatedEvent => GuildedEvents["ChatMessageCreated"];
        /// <summary>
        /// When a message gets posted in the chat.
        /// </summary>
        public event ClientEventHandler<MessageCreatedEvent> MessageCreated
        {
            add => MessageCreatedEvent.Invokable = Delegate.Combine(MessageCreatedEvent.Invokable, value);
            remove => MessageCreatedEvent.Invokable = Delegate.Remove(MessageCreatedEvent.Invokable, value);
        }

        /// <summary>
        /// When a message gets updated/edited in the chat.
        /// </summary>
        protected EventInfo MessageUpdatedEvent => GuildedEvents["ChatMessageUpdated"];
        /// <summary>
        /// When a message gets updated/edited in the chat.
        /// </summary>
        public event ClientEventHandler<MessageUpdatedEvent> MessageUpdated
        {
            add => MessageUpdatedEvent.Invokable = Delegate.Combine(MessageUpdatedEvent.Invokable, value);
            remove => MessageUpdatedEvent.Invokable = Delegate.Remove(MessageUpdatedEvent.Invokable, value);
        }
        /// <summary>
        /// When a message gets removed/deleted in the chat.
        /// </summary>
        protected EventInfo MessageDeletedEvent => GuildedEvents["ChatMessageDeleted"];
        /// <summary>
        /// When a message gets removed/deleted in the chat.
        /// </summary>
        public event ClientEventHandler<MessageDeletedEvent> MessageDeleted
        {
            add => MessageDeletedEvent.Invokable = Delegate.Combine(MessageDeletedEvent.Invokable, value);
            remove => MessageDeletedEvent.Invokable = Delegate.Remove(MessageDeletedEvent.Invokable, value);
        }
        #endregion
    }
}