using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Websocket.Client;

namespace Guilded.NET
{
    using Base;
    using Base.Events;
    using Base.Users;
    using Converters;

    /// <summary>
    /// A base for all Guilded clients.
    /// </summary>
    /// <seealso cref="GuildedBotClient"/>
    public abstract partial class AbstractGuildedClient : BaseGuildedClient
    {
        /// <summary>
        /// An event when the client is prepared.
        /// </summary>
        /// <remarks>
        /// An event when the client has added all of the finishing touches, such as getting <see cref="Me"/> data.
        /// </remarks>
        protected EventHandler PreparedEvent;
        /// <summary>
        /// An event when the client is prepared.
        /// </summary>
        /// <remarks>
        /// An event when the client has added all of the finishing touches, such as getting <see cref="Me"/> data.
        /// </remarks>
        public event EventHandler Prepared
        {
            add => PreparedEvent += value;
            remove => PreparedEvent -= value;
        }
        /// <summary>
        /// User account this client is using.
        /// </summary>
        /// <value>Me</value>
        public Me Me
        {
            get; protected set;
        }
        /// <summary>
        /// A list of JSON converters used to (de)serialize Guilded responses and WebSocket events.
        /// </summary>
        /// <remarks>
        /// <para>Use these converters to pass anything to Guilded REST client or Guilded WebSocket client.</para>
        /// </remarks>
        /// <value>List of JSON converters</value>
        public JsonConverter[] Converters
        {
            get; set;
        }
        /// <summary>
        /// A base for user bot clients and normal bot clients.
        /// </summary>
        protected AbstractGuildedClient() : base()
        {
            // Adds converters to serializers
            SerializerSettings.Converters = new JsonConverter[]
            {
                new RichTextConverter(),
                new ContentConverter(),
                new HexColorConverter()
            };
            GuildedSerializer = JsonSerializer.Create(SerializerSettings);
            WebsocketMessage += HandleSocketMessages;
            // Events
            #region Event list
            // The list of all events that are supported
            GuildedEvents = new Dictionary<string, IEventInfo<object>>
            {
                // Utils
                { "",                       new EventInfo<WelcomeEvent>(typeof(WelcomeEvent)) },
                // Team events
                { "TeamXpAdded",            new EventInfo<XpAddedEvent>(typeof(XpAddedEvent)) },
                // Chat messages
                { "ChatMessageCreated",     new EventInfo<MessageCreatedEvent>(typeof(MessageCreatedEvent)) },
                { "ChatMessageUpdated",     new EventInfo<MessageUpdatedEvent>(typeof(MessageUpdatedEvent)) },
                { "ChatMessageDeleted",     new EventInfo<MessageDeletedEvent>(typeof(MessageDeletedEvent)) }
            };
            #endregion
        }
        /// <summary>
        /// Connects this client to Guilded.
        /// </summary>
        /// <remarks>
        /// Creates a new connection to Guilded with this client.
        /// </remarks>
        public override async Task ConnectAsync()
        {
            // Inits websocket
            Websockets.Add("", await InitWebsocket().ConfigureAwait(false));
            // Thread for ping and heartbeat
            HeartbeatTimer = new Timer(DefaultHeartbeatInterval)
            {
                AutoReset = true,
                Enabled = true
            };
            HeartbeatTimer.Elapsed += SendHeartbeat;
            // Starts heartbeat timer
            HeartbeatTimer.Start();
        }
        /// <summary>
        /// Disconnects this client from Guilded.
        /// </summary>
        /// <remarks>
        /// Stops any connections this client has with Guilded.
        /// </remarks>
        public override async Task DisconnectAsync()
        {
            // Disconnects its websockets
            foreach (string wsKey in Websockets.Keys)
            {
                WebsocketClient ws = Websockets[wsKey];
                // If it can be stopped, stop it
                if (ws.IsRunning)
                    await ws.StopOrFail(WebSocketCloseStatus.NormalClosure, "manual").ConfigureAwait(false);
                // Dispose and remove it
                ws.Dispose();
                Websockets.Remove(wsKey);
            }
            // Stops the timer
            HeartbeatTimer?.Stop();
            HeartbeatTimer?.Dispose();
            // Invoke disconnection event
            DisconnectedEvent?.Invoke(this, EventArgs.Empty);
        }
        /// <summary>
        /// Disposes <see cref="AbstractGuildedClient"/> instance.
        /// </summary>
        /// <remarks>
        /// Disposes <see cref="AbstractGuildedClient"/>, its heartbeat and its WebSockets.
        /// </remarks>
        public override void Dispose() =>
            DisconnectAsync().GetAwaiter().GetResult();
        private async Task<T> GetObject<T>(string resource, Method method, object key, object body = null) =>
            (await ExecuteRequest<JContainer>(resource, method, body).ConfigureAwait(false)).Data[key].ToObject<T>(GuildedSerializer);
    }
}