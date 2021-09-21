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
        /// <para>An event that occurs once Guilded client has added finishing touches.</para>
        /// <para>These things need to be completed in order for it to occur:</para>
        /// <list type="bullet">
        ///     <item>
        ///         <term>Connected</term>
        ///         <description>Guilded client must be connected.</description>
        ///     </item>
        /// </list>
        /// <para>You can use this as a signal that ensures all client functions are properly
        /// working and can be used.</para>
        /// </remarks>
        protected EventHandler PreparedEvent;
        /// <inheritdoc cref="PreparedEvent"/>
        public event EventHandler Prepared
        {
            add => PreparedEvent += value;
            remove => PreparedEvent -= value;
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
        protected AbstractGuildedClient()
        {
            // Serializer converters for REST
            SerializerSettings.Converters = new JsonConverter[]
            {
                new RichTextConverter(),
                new ContentConverter(),
                new HexColorConverter()
            };
            GuildedSerializer = JsonSerializer.Create(SerializerSettings);
            WebsocketMessage.Subscribe(HandleSocketMessages);
            #region Event list
            // Dictionary of supported events, so we wouldn't need to manually do it
            GuildedEvents = new Dictionary<object, IEventInfo<object>>
            {
                // Utils
                { 1,                        new EventInfo<WelcomeEvent>(typeof(WelcomeEvent)) },
                { 2,                        new EventInfo<ResumeEvent>(typeof(ResumeEvent)) },
                // Team events
                { "TeamXpAdded",            new EventInfo<XpAddedEvent>(typeof(XpAddedEvent)) },
                { "teamRolesUpdated",       new EventInfo<RolesUpdatedEvent>(typeof(RolesUpdatedEvent)) },
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
            HeartbeatTimer = new Timer(DefaultHeartbeatInterval)
            {
                AutoReset = true,
                Enabled = true
            };

            HeartbeatTimer.Elapsed += SendHeartbeat;
            Websockets.Add("", await InitWebsocket().ConfigureAwait(false));

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
            foreach (string wsKey in Websockets.Keys)
            {
                WebsocketClient ws = Websockets[wsKey];

                if (ws.IsRunning)
                    await ws.StopOrFail(WebSocketCloseStatus.NormalClosure, "manual").ConfigureAwait(false);

                ws.Dispose();
                Websockets.Remove(wsKey);
            }
            // Stop the heartbeats
            HeartbeatTimer?.Stop();
            HeartbeatTimer?.Dispose();

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