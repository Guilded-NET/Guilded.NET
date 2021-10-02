using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Guilded.NET
{
    using Base;
    using Base.Events;

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
        /// <para>An event that occurs once Guilded client has added finishing touches. You can use this as a signal <see cref="Prepared"/> ensures all client functions are properly working and can be used.</para>
        /// <para>As of now, this is called at the same time as <see cref="BaseGuildedClient.Connected"/> event.</para>
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
            GuildedSerializer = JsonSerializer.Create(SerializerSettings);

            WebsocketMessage.Subscribe(
                OnSocketMessage,
                // Relay the error onto welcome observable
                // TODO: Change welcome observable with something else
                e => GuildedEvents[1].OnError(e)
            );

            #region Event list
            // Dictionary of supported events, so we wouldn't need to manually do it.
            // The only manual work to be done is in AbstractGuildedClient.Messages.cs file,
            // which only allows us to subscribe to events and it is literally +1 member
            // to be added and copy pasting for the most part.
            // No idea if this can put back a bit of performance.
            GuildedEvents = new Dictionary<object, IEventInfo<object>>
            {
                // Event messages
                { 1,                        new EventInfo<WelcomeEvent>() },
                { 2,                        new EventInfo<ResumeEvent>() },
                // Team events
                { "TeamXpAdded",            new EventInfo<XpAddedEvent>() },
                { "teamRolesUpdated",       new EventInfo<RolesUpdatedEvent>() },
                { "TeamMemberUpdated",      new EventInfo<MemberUpdatedEvent>() },
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
        /// <para>Connects to Guilded and starts Guilded's WebSocket, as well as its heartbeat.</para>
        /// </remarks>
        /// <seealso cref="DisconnectAsync"/>
        /// <seealso cref="GuildedBotClient.ConnectAsync()"/>
        /// <seealso cref="GuildedBotClient.ConnectAsync(string)"/>
        public override async Task ConnectAsync()
        {
            // Determine whether to start it again or start it new
            if(HeartbeatTimer is null)
            {
                HeartbeatTimer = new Timer(DefaultHeartbeatInterval)
                {
                    AutoReset = true,
                    Enabled = true
                };

                HeartbeatTimer.Elapsed += SendHeartbeat;
            }
            else
            {
                HeartbeatTimer.Enabled = true;
            }

            if(Websocket is null)
                Websocket = await InitWebsocket().ConfigureAwait(false);
            else
                await Websocket.StartOrFail().ConfigureAwait(false);

            HeartbeatTimer.Start();
        }
        /// <summary>
        /// Disconnects this client from Guilded.
        /// </summary>
        /// <remarks>
        /// <para>The method that stops Guilded WebSocket and its heartbeat.</para>
        /// </remarks>
        /// <seealso cref="ConnectAsync"/>
        /// <seealso cref="Dispose"/>
        /// <seealso cref="GuildedBotClient.ConnectAsync()"/>
        /// <seealso cref="GuildedBotClient.ConnectAsync(string)"/>
        public override async Task DisconnectAsync()
        {
            if (Websocket.IsRunning)
                await Websocket.StopOrFail(WebSocketCloseStatus.NormalClosure, "manual").ConfigureAwait(false);
            // Stop the heartbeats
            HeartbeatTimer?.Stop();

            DisconnectedEvent?.Invoke(this, EventArgs.Empty);
        }
        /// <summary>
        /// Disposes <see cref="AbstractGuildedClient"/> instance.
        /// </summary>
        /// <remarks>
        /// <para>Disposes <see cref="AbstractGuildedClient"/> and its WebSockets and heartbeat.</para>
        /// </remarks>
        /// <seealso cref="DisconnectAsync"/>
        public override void Dispose()
        {
            DisconnectAsync().GetAwaiter().GetResult();
            // Dispose them entirely; they aren't disposed by DisconnectAsync, only shut down
            Websocket.Dispose();
            HeartbeatTimer?.Dispose();
        }
        private async Task<T> GetObject<T>(string resource, Method method, object key, object body = null) =>
            (await ExecuteRestAsync<JContainer>(resource, method, body).ConfigureAwait(false)).Data[key].ToObject<T>(GuildedSerializer);
    }
}