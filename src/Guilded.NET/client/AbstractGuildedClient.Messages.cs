using System;
using System.Reactive.Linq;
using System.Collections.Generic;

namespace Guilded.NET
{
    using Base.Events;

    /// <summary>
    /// A base for all Guilded clients.
    /// </summary>
    public abstract partial class AbstractGuildedClient
    {
        /// <summary>
        /// A dictionary of Guilded events.
        /// </summary>
        /// <remarks>
        /// <para>A dictionary of all supported Guilded events, containing their event names and information about the event to use.</para>
        /// <para>Use this if you need to support events that Guilded.NET does not.</para>
        /// </remarks>
        /// <value>Dictionary of events</value>
        protected Dictionary<string, IEventInfo<object>> GuildedEvents
        {
            get; set;
        }
        private Dictionary<int, IDisposable> Disposables
        {
            get; set;
        } = new Dictionary<int, IDisposable>();

        /// <summary>
        /// An event when WebSocket receives a welcome.
        /// </summary>
        /// <remarks>
        /// This event is received once WebSocket is initiated and is confirmed on Guilded's side.
        /// </remarks>
        public IObservable<WelcomeEvent> OnWelcome => ((IEventInfo<WelcomeEvent>)GuildedEvents[""]).Observable;
        /// <summary>
        /// An event when WebSocket receives a welcome.
        /// </summary>
        /// <remarks>
        /// This event is received once WebSocket is initiated and is confirmed on Guilded's side.
        /// </remarks>
        public event Action<WelcomeEvent> Welcome
        {
            add => Disposables.Add(value.GetHashCode(), OnWelcome.Subscribe(value));
            remove
            {
                Disposables[value.GetHashCode()].Dispose();
                Disposables.Remove(value.GetHashCode());
            }
        }

        #region Teams
        /// <summary>
        /// When a list of users get certain amount of XP.
        /// </summary>
        /// <remarks>
        /// This event is received once a given list of users get certain amount of XP(between -1000 and 1000).
        /// </remarks>
        public IObservable<XpAddedEvent> OnXpAdded => ((IEventInfo<XpAddedEvent>)GuildedEvents["TeamXpAdded"]).Observable;
        /// <summary>
        /// When a list of users get certain amount of XP.
        /// </summary>
        /// <remarks>
        /// This event is received once a given list of users get certain amount of XP(between -1000 and 1000).
        /// </remarks>
        public event Action<XpAddedEvent> XpAdded
        {
            add => Disposables.Add(value.GetHashCode(), OnXpAdded.Subscribe(value));
            remove
            {
                Disposables[value.GetHashCode()].Dispose();
                Disposables.Remove(value.GetHashCode());
            }
        }
        #endregion

        #region Chat channels
        /// <summary>
        /// When a message gets posted in the chat.
        /// </summary>
        /// <remarks>
        /// This event is received when any type of message gets sent/created, including system messages.
        /// </remarks>
        public IObservable<MessageCreatedEvent> OnMessageCreated => ((IEventInfo<MessageCreatedEvent>)GuildedEvents["ChatMessageCreated"]).Observable;
        /// <summary>
        /// When a message gets posted in the chat.
        /// </summary>
        /// <remarks>
        /// This event is received when any type of message gets sent/created, including system messages.
        /// </remarks>
        public event Action<MessageCreatedEvent> MessageCreated
        {
            add => Disposables.Add(value.GetHashCode(), OnMessageCreated.Subscribe(value));
            remove
            {
                Disposables[value.GetHashCode()].Dispose();
                Disposables.Remove(value.GetHashCode());
            }
        }

        /// <summary>
        /// When a message gets updated/edited in the chat.
        /// </summary>
        /// <remarks>
        /// This event is received when a text message gets updated/edited.
        /// </remarks>
        public IObservable<MessageUpdatedEvent> OnMessageUpdated => ((IEventInfo<MessageUpdatedEvent>)GuildedEvents["ChatMessageUpdated"]).Observable;
        /// <summary>
        /// When a message gets updated/edited in the chat.
        /// </summary>
        /// <remarks>
        /// This event is received when a text message gets updated/edited.
        /// </remarks>
        public event Action<MessageUpdatedEvent> MessageUpdated
        {
            add => Disposables.Add(value.GetHashCode(), OnMessageUpdated.Subscribe(value));
            remove
            {
                Disposables[value.GetHashCode()].Dispose();
                Disposables.Remove(value.GetHashCode());
            }
        }
        /// <summary>
        /// When a message gets removed/deleted in the chat.
        /// </summary>
        /// <remarks>
        /// This event is received when any type of message gets deleted, including system messages.
        /// </remarks>
        public IObservable<MessageDeletedEvent> OnMessageDeleted => ((IEventInfo<MessageDeletedEvent>)GuildedEvents["ChatMessageDeleted"]).Observable;
        /// <summary>
        /// When a message gets removed/deleted in the chat.
        /// </summary>
        /// <remarks>
        /// This event is received when any type of message gets deleted, including system messages.
        /// </remarks>
        public event Action<MessageDeletedEvent> MessageDeleted
        {
            add => Disposables.Add(value.GetHashCode(), OnMessageDeleted.Subscribe(value));
            remove
            {
                Disposables[value.GetHashCode()].Dispose();
                Disposables.Remove(value.GetHashCode());
            }
        }
        #endregion

        #region Handling
        /// <summary>
        /// When the socket message event is invoked.
        /// </summary>
        /// <param name="_">The object that invoked the event</param>
        /// <param name="message">A message received from a WebSocket</param>
        protected void HandleSocketMessages(object _, GuildedEvent message)
        {
            // Makes sure that the event received is not null
            if (message is null) return;

            string eventName = message?.EventName ?? "";

            // Checks if this event is supported by Guilded.NET
            if (GuildedEvents.ContainsKey(eventName))
            {
                // Gets event based on its name
                IEventInfo<object> ev = GuildedEvents?[eventName];
                // Deserialized data from the event
                object data = message.RawData.ToObject(ev.ArgumentType, GuildedSerializer);

                // Makes sure it exists and then invokes the observable
                if (ev != default)
                    ev.OnNext(data);
            }
        }
        #endregion
    }
}