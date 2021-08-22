using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Serilog;
using Serilog.Core;
using Websocket.Client;
using System.Net.WebSockets;

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
        /// Logs all of the Guilded.NET related things.
        /// </summary>
        /// <value>Guilded.NET logger</value>
        internal Logger GuildedLogger
        {
            get; set;
        }
        /// <summary>
        /// A base for user bot clients and normal bot clients.
        /// </summary>
        protected AbstractGuildedClient() : base()
        {
            GuildedLogger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Filter.ByIncludingOnly(l => EnabledLogLevels.Contains(l.Level))
                .WriteTo.Console()
                .CreateLogger();
            Converters = new JsonConverter[] {
                new RichTextConverter(),
                new ContentConverter(),
                new HexColorConverter()
            };
            // Adds default converters
            foreach (JsonConverter converter in Converters)
                GuildedSerializer.Converters.Add(converter);
            /*(ClientConfig, MessageCommands, CommentCommands) = (config, new Dictionary<CommandAttribute, CommandMethod<MessageCreatedEvent>>(), new Dictionary<CommandAttribute, CommandMethod<ContentReplyCreatedEvent>>());*/
            WebsocketMessage += HandleSocketMessages;
            // If the client should be disposed on CTRL + C, then add method to CancelKeyPress
            Console.CancelKeyPress += (o, e) => Dispose();
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
            GuildedLogger.Information("Connecting to Guilded");
            // Inits websocket
            GuildedLogger.Verbose("Creating a websocket for the client");
            await InitWebsocket();
            // Thread for ping and heartbeat
            HeartbeatTimer = new Timer(HeartbeatInterval)
            {
                AutoReset = true,
                Enabled = true
            };
            HeartbeatTimer.Elapsed += SendHeartbeat;
            // Starts heartbeat thread
            GuildedLogger.Verbose("Starting heartbeat thread");
            HeartbeatTimer.Start();
        }
        /// <summary>
        /// Disconnects this client from Guilded.
        /// </summary>
        /// <remarks>
        /// Stop any connections this client has with Guilded.
        /// </remarks>
        public override async Task DisconnectAsync()
        {
            GuildedLogger.Information("Disconnecting from Guilded");
            // Disconnects its websockets
            foreach (WebsocketClient ws in Websockets.Values)
                // If it can be stopped, stop it
                if(ws.IsRunning)
                    await ws.StopOrFail(WebSocketCloseStatus.NormalClosure, "manual");
            // Invoke disconnection event
            DisconnectedEvent?.Invoke(this, EventArgs.Empty);
        }
        /// <summary>
        /// Disposes the bot.
        /// </summary>
        public override void Dispose()
        {
            // Disconnects itself
            DisconnectAsync().GetAwaiter().GetResult();
            // Disposes the base
            base.Dispose();
        }
        private async Task<JContainer> GetObject(string resource, Method method, object body = null) =>
            JObject.Parse((await ExecuteRequest(resource, method, body)).Content ?? "{}");
        private async Task<T> GetObject<T>(string resource, Method method, object body = null) =>
            (await GetObject(resource, method, body)).ToObject<T>(GuildedSerializer);
        private async Task<T> GetObject<T>(string resource, Method method, object key, object body = null) =>
            (await GetObject(resource, method, body))[key].ToObject<T>(GuildedSerializer);
    }
}