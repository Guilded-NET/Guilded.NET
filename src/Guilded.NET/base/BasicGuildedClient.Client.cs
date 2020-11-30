using RestSharp;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.WebSockets;
using Websocket.Client;
using System.Text.RegularExpressions;

namespace Guilded.NET {
    using API;
    using Util;
    using Objects.Converters;
    using Objects;
    /// <summary>
    /// A base for user bot clients and normal bot clients.
    /// </summary>
    public abstract partial class BasicGuildedClient: BaseGuildedClient {
        event EventHandler<SocketMessage> GuildedWebsocketMessageEvent;
        event EventHandler<int> HeartbeatEvent;
        /// <summary>
        /// User account this client is using.
        /// </summary>
        /// <value>Client user</value>
        public User CurrentUser {
            get; protected set;
        } = null;
        /// <summary>
        /// JSON converters used to (de)serialize Guilded responses and websocket events.
        /// </summary>
        /// <value>List of JSON converters</value>
        protected static JsonConverter[] Converters {
            get; set;
        }
        /// <summary>
        /// Regexp for numbers at the start of Guilded's mess
        /// </summary>
        /// <returns></returns>
        protected static Regex NumberStart = new Regex("^([0-9]+)");
        /// <summary>
        /// Serializer used to (de)serialize JSON given by Guilded or made for Guilded.
        /// </summary>
        /// <value>Serializer</value>
        protected JsonSerializer GuildedSerializer {
            get; set;
        }
        /// <summary>
        /// Configuration of this client.
        /// </summary>
        /// <value>Configuration</value>
        public GuildedClientConfig ClientConfig {
            get; set;
        }
        protected BasicGuildedClient(GuildedClientConfig config): base() {
            // Create new serializer
            GuildedSerializer = new JsonSerializer();
            // Converters for JSON
            Converters = new JsonConverter[] {
                new EnumConverter(),
                new IdConverter(),
                new NodeConverter(),
                new ClientObjectConverter(this)
            };
            // Adds default converters
            foreach(JsonConverter converter in Converters)
                GuildedSerializer.Converters.Add(converter);
            // Sets properties
            ClientConfig = config;
            CommandDictionary = new Dictionary<CommandAttribute, CommandMethod>();
            RandomId = new Random();
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
#pragma warning disable 0618
        protected async Task BasicConnectAsync() {
            // Inits websocket
            base.InitWebsocket(25, GuildedWebsocketURL, 25);
#pragma warning restore 0618
            // Message
            Websocket.MessageReceived.Subscribe(WebsocketMessageReceived);
            // Start
            await Websocket.StartOrFail();
            HeartbeatThread.Start();
        }
        /// <summary>
        /// Used for when Websocket receives a message.
        /// </summary>
        /// <param name="msg">Websocket message</param>
        protected virtual void WebsocketMessageReceived(ResponseMessage msg) {
            if(msg.MessageType == WebSocketMessageType.Text) {
                // Matches the number using Regex
                string strnum = NumberStart.Match(msg.Text).Value;
                // Parses the number
                uint.TryParse(strnum, out uint num);
                // Trimmed string
                string trimmed = msg.Text[strnum.Length..];
                // If there is nothing else besides number, invoke the event
                if(string.IsNullOrWhiteSpace(trimmed)) {
                    GuildedWebsocketMessageEvent?.Invoke(this, new SocketMessage(num));
                    return;
                }
                // Parses it as token
                JToken token = JToken.Parse(trimmed);
                // Get type of the socket message
                if(token.Type == JTokenType.Array) {
                    JArray array = (JArray)token;
                    // If first item is string and second item is object, then it's SocketEvent
                    if (array[0].Type == JTokenType.String && array[1].Type == JTokenType.Object) {
                        // Get first item as value and second item as object
                        JValue value = (JValue)array[0];
                        JObject obj = (JObject)array[1];
                        // Invoke the event
                        GuildedWebsocketMessageEvent?.Invoke(this, new SocketEvent(num, obj, value.ToString()));
                    }
                }
                else if(token.Type == JTokenType.Object) {
                    // Get token as object
                    JObject jobj = (JObject)token;
                    // Invoke the event
                    GuildedWebsocketMessageEvent?.Invoke(this, new ObjectMessage(num, jobj));
                }
            }
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
        /// Invokes a heartbeat event.
        /// </summary>
        /// <param name="sender">Who is invoking the event</param>
        /// <param name="value">Heartbeat response</param>
        protected void InvokeHeartbeatEvent(object sender, int value) => HeartbeatEvent?.Invoke(sender, value);
    }
}