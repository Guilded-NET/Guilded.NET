using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.NET {
    using API;

    using Objects;
    using Objects.Converters;
    /// <summary>
    /// A base for user bot clients and normal bot clients.
    /// </summary>
    public abstract partial class BasicGuildedClient: BaseGuildedClient {
        /// <summary>
        /// A random for generating IDs.
        /// </summary>
        protected Random RandomId;
        /// <summary>
        /// An event when client is connected and it is fully ready to be used.
        /// </summary>
        protected EventHandler PreparedEvent;
        /// <summary>
        /// An event when client is connected and it is fully ready to be used.
        /// </summary>
        public event EventHandler Prepared {
            add => PreparedEvent += value;
            remove => PreparedEvent -= value;
        }
        /// <summary>
        /// User account this client is using.
        /// </summary>
        /// <value>Me</value>
        public Me Me {
            get; protected set;
        }
        /// <summary>
        /// JSON converters used to (de)serialize Guilded responses and websocket events.
        /// </summary>
        /// <value>List of JSON converters</value>
        public JsonConverter[] Converters {
            get; set;
        }
        /// <summary>
        /// Configuration of this client.
        /// </summary>
        /// <value>Configuration</value>
        public GuildedClientConfig ClientConfig {
            get; set;
        }
        /// <summary>
        /// Serializer used to (de)serialize JSON given by Guilded or made for Guilded.
        /// </summary>
        /// <value>Serializer</value>
        public JsonSerializer GuildedSerializer {
            get; set;
        }
        /// <summary>
        /// A base for user bot clients and normal bot clients.
        /// </summary>
        /// <param name="config">A configuration which will change how Guilded.NET client will work</param>
        protected BasicGuildedClient(GuildedClientConfig config): base() {
            Me = null;
            // Create new serializer
            (GuildedSerializer, Converters) = (new JsonSerializer(),
                new JsonConverter[] {
                    new ClientObjectConverter(this),
                    new IdConverter(),
                    new NodeConverter(),
                    new MiscConverter(),
                    new ColourConverter()
                }
            );
            // Adds default converters
            foreach(JsonConverter converter in Converters)
                GuildedSerializer.Converters.Add(converter);
            // Sets properties
            (ClientConfig, CommandDictionary, RandomId) = (config, new Dictionary<CommandAttribute, CommandMethod>(), new Random());
            GuildedWebsocketMessage += HandleSocketMessages;
            // If the client should be disposed on CTRL + C, then add method to CancelKeyPress
            if(!ClientConfig.DisableCancelKeyPress) Console.CancelKeyPress += (o, e) => Dispose();
            // Events
            MessageCreated += CommandChecking;
            CommandInvoked += InvokeCommand;
        }
        /// <summary>
        /// Base for connecting to Guilded.
        /// </summary>
        /// <returns>Async Task</returns>
        protected Task BasicConnectAsync() {
            // Inits websocket
            InitWebsocket(25);
            // A cancellation token for the thread
            HeartbeatToken = new CancellationTokenSource();
            // Thread for ping and heartbeat
            HeartbeatThread = new Thread(async (o) => await HeartbeatThreadMethod(HeartbeatToken.Token));
            // Starts heartbeat thread.
            HeartbeatThread.Start();
            return Task.CompletedTask;
        }
        /// <summary>
        /// Base for disconnect method.
        /// </summary>
        /// <returns>Task</returns>
        protected Task BasicDisconnectAsync() {
            DisconnectedEvent?.Invoke(this, EventArgs.Empty);
            return Task.CompletedTask;
        }
        /// <summary>
        /// Disposes the bot.
        /// </summary>
        public override void Dispose() {
            DisconnectAsync().GetAwaiter().GetResult();
            base.Dispose();
        }
        /// <summary>
        /// Sends a request to Guilded, gets an object and gets a specific key.
        /// </summary>
        async Task<JObject> FromObject(Endpoint endpoint, params IReqAddable[] addables) {
            // Gets a response
            ExecutionResponse<object> response = await ExecuteRequest(endpoint, addables);
            // Gets the response as an object and returns it
            return JObject.Parse(response.Content ?? "{}");
        }
        /// <summary>
        /// Sends a request to Guilded, gets an object and gets a specific key.
        /// </summary>
        /// <typeparam name="T">Result type</typeparam>
        async Task<T> FromObject<T>(Endpoint endpoint, params IReqAddable[] addables) =>
            (await FromObject(endpoint, addables)).ToObject<T>(GuildedSerializer);
        /// <summary>
        /// Sends a request to Guilded, gets an object and gets a specific key.
        /// </summary>
        /// <typeparam name="T">Result type</typeparam>
        async Task<T> FromObject<T>(Endpoint endpoint, string key, params IReqAddable[] addables) =>
            (await FromObject(endpoint, addables))[key].ToObject<T>(GuildedSerializer);
        /// <summary>
        /// Sends a request to Guilded, gets an array.
        /// </summary>
        /// <typeparam name="T">Result type</typeparam>
        async Task<List<T>> FromArray<T>(Endpoint endpoint, params IReqAddable[] addables) {
            // Gets a response
            ExecutionResponse<object> response = await ExecuteRequest(endpoint, addables);
            // Gets the response as an object
            JArray array = JArray.Parse(response.Content ?? "[]");
            // Gets and returns parsed object
            return array.ToObject<List<T>>(GuildedSerializer);
        }
    }
}