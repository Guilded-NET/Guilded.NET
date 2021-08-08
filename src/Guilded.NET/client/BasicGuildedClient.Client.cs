using System;
using System.Collections.Generic;
using System.Timers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Serilog;
using Serilog.Core;
using Websocket.Client;

namespace Guilded.NET
{
    using Base;
    using Base.Users;
    using Base.Events;

    using Converters;

    /// <summary>
    /// A base for user bot clients and normal bot clients.
    /// </summary>
    public abstract partial class BasicGuildedClient : BaseGuildedClient
    {
        /// <summary>
        /// An event when client is connected and it is fully ready to be used.
        /// </summary>
        protected EventHandler PreparedEvent;
        /// <summary>
        /// An event when client is connected and it is fully ready to be used.
        /// </summary>
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
        /// JSON converters used to (de)serialize Guilded responses and websocket events.
        /// </summary>
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
        protected BasicGuildedClient() : base()
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
            // A list of all events
            GuildedEvents = new Dictionary<string, EventInfo> {
                // Utils
                { "",                       new EventInfo(typeof(WelcomeEvent)) },
                // Team events
                { "TeamXpAdded",            new EventInfo(typeof(XpAddedEvent)) },
                // Chat messages
                { "ChatMessageCreated",     new EventInfo(typeof(MessageCreatedEvent)) },
                { "ChatMessageUpdated",     new EventInfo(typeof(MessageUpdatedEvent)) },
                { "ChatMessageDeleted",     new EventInfo(typeof(MessageDeletedEvent)) }
            };
            #endregion
        }
        /// <summary>
        /// Base for connecting to Guilded.
        /// </summary>
        protected void BasicConnectAsync()
        {
            GuildedLogger.Information("Connecting to Guilded");
            // Inits websocket
            GuildedLogger.Verbose("Creating a websocket for the client");
            InitWebsocket();
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
        /// Disconnects the client from Guilded.
        /// </summary>
        public void BasicDisconnect()
        {
            GuildedLogger.Information("Disconnecting from Guilded");
            // Disconnects its websockets
            foreach (WebsocketClient ws in Websockets.Values)
                ws.Dispose();
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