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
    /// <remarks>
    /// <para>A base class for <see cref="GuildedBotClient"/> and soon other clients.</para>
    /// <para>There is not much to be used here. It is recommended to use <see cref="GuildedBotClient"/>.</para>
    /// </remarks>
    /// <seealso cref="GuildedBotClient"/>
    public abstract partial class AbstractGuildedClient : BaseGuildedClient
    {
        /// <summary>
        /// An event when the client is prepared.
        /// </summary>
        /// <remarks>
        /// <para>An event that occurs once Guilded client has added finishing touches.</para>
        /// <para>These things need to be completed in order for it to occur:</para>
        /// <list type="number">
        ///     <item>
        ///         <term>Connected</term>
        ///         <description>Guilded client must be connected.</description>
        ///     </item>
        /// </list>
        /// <para>You can use this as a signal <see cref="Prepared"/> ensures all client functions are properly
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
        /// A base constructor for creating Guilded clients.
        /// </summary>
        /// <seealso cref="GuildedBotClient()"/>
        /// <seealso cref="GuildedBotClient(string)"/>
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
            WebsocketMessage.Subscribe(OnSocketMessage);

            #region Event list
            // Dictionary of supported events, so we wouldn't need to manually do it.
            // The only manual work to be done is in AbstractGuildedClient.Messages.cs file,
            // which only allows us to subscribe to events and it is literally +1 member
            // to be added and copy pasting for the most part.
            // No idea if this can put back a bit of performance.
            GuildedEvents = new Dictionary<object, IEventInfo<object>>
            {
                // Utils
                { 1,                        new EventInfo<WelcomeEvent>() },
                { 2,                        new EventInfo<ResumeEvent>() },
                // Team events
                { "TeamXpAdded",            new EventInfo<XpAddedEvent>() },
                { "teamRolesUpdated",       new EventInfo<RolesUpdatedEvent>() },
                // Chat messages
                { "ChatMessageCreated",     new EventInfo<MessageCreatedEvent>() },
                { "ChatMessageUpdated",     new EventInfo<MessageUpdatedEvent>() },
                { "ChatMessageDeleted",     new EventInfo<MessageDeletedEvent>() }
            };
            #endregion
        }
        /// <summary>
        /// Connects this client to Guilded.
        /// </summary>
        /// <remarks>
        /// <para>Connects to Guilded and starts these functions:</para>
        /// <list type="bullet">
        ///     <item>
        ///         <description>Guilded WebSocket</description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="BaseGuildedClient.HeartbeatTimer"/> used for WebSocket heartbeats</description>
        ///     </item>
        /// </list>
        /// </remarks>
        /// <seealso cref="DisconnectAsync"/>
        /// <seealso cref="GuildedBotClient.ConnectAsync()"/>
        /// <seealso cref="GuildedBotClient.ConnectAsync(string)"/>
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
        /// <para>This method stops:</para>
        /// <list type="bullet">
        ///     <item>
        ///         <description>All Websockets in <see cref="BaseGuildedClient.Websockets"/></description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="BaseGuildedClient.HeartbeatTimer"/> used for WebSocket heartbeats</description>
        ///     </item>
        /// </list>
        /// </remarks>
        /// <seealso cref="ConnectAsync"/>
        /// <seealso cref="Dispose"/>
        /// <seealso cref="GuildedBotClient.ConnectAsync()"/>
        /// <seealso cref="GuildedBotClient.ConnectAsync(string)"/>
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
        /// <para>Disposes <see cref="AbstractGuildedClient"/> and its connections:</para>
        /// <list type="bullet">
        ///     <item>
        ///         <description>All Websockets in <see cref="BaseGuildedClient.Websockets"/></description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="BaseGuildedClient.HeartbeatTimer"/> used for WebSocket heartbeats</description>
        ///     </item>
        /// </list>
        /// </remarks>
        /// <seealso cref="DisconnectAsync"/>
        public override void Dispose() =>
            DisconnectAsync().GetAwaiter().GetResult();
        private async Task<T> GetObject<T>(string resource, Method method, object key, object body = null) =>
            (await ExecuteRequest<JContainer>(resource, method, body).ConfigureAwait(false)).Data[key].ToObject<T>(GuildedSerializer);
    }
}