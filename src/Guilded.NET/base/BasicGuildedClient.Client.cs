using System.Threading.Tasks;
using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.WebSockets;
using Websocket.Client;
using System.Text.RegularExpressions;
using System.Threading;

namespace Guilded.NET {
    using API;
    using Util;
    using Objects.Converters;
    using Objects;
    using Objects.Teams;
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
        /// <summary>
        /// A base for user bot clients and normal bot clients.
        /// </summary>
        /// <param name="config">A configuration which will change how Guilded.NET client will work</param>
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
        protected Task BasicConnectAsync() {
            // Inits websocket
            InitWebsocket(25);
#pragma warning restore 0618
            // A cancellation token for the thread
            HeartbeatToken = new CancellationTokenSource();
            // Thread for ping and heartbeat
            HeartbeatThread = new Thread(async (o) => await HeartbeatThreadMethod(HeartbeatToken.Token));
            // Starts heartbeat thread.
            HeartbeatThread.Start();
            return Task.CompletedTask;
        }
        /// <summary>
        /// Initializes websocket.
        /// </summary>
        /// <param name="reconnection">Seconds of time between each reconnection</param>
        /// <param name="teamId">ID of the team to initialize websocket in</param>
        public override WebsocketClient InitWebsocket(double? reconnection = null, string teamId = null) {
            // Calls base to create a websocket
            WebsocketClient websocket = base.InitWebsocket(reconnection, teamId);
            Console.WriteLine("Creating client for {0}", teamId);
            // Subscribe to message event, so we could get events such as message creation event
            websocket.MessageReceived.Subscribe(o => Console.WriteLine("{0}:\n{1}", websocket.Url, o.Text));
            // Start that websocket
            websocket.StartOrFail().GetAwaiter().GetResult();
            return websocket;
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
        /// Sets referer as a specific team channel.
        /// </summary>
        /// <param name="channel">Channel to refer to</param>
        public async Task SetReferer(Channel channel) {
            // Team that channel is in
            Team team = await channel.GetTeamAsync();
            // All groups of that team
            IList<Group> groups = await team.GetGroupsAsync();
            // Sets new referer
            Referer = $"{team.Subdomain}/groups/{groups.FirstOrDefault(x => x.Id == channel.GroupId)}/channels/{channel.Id}/chat";
        }
        /// <summary>
        /// Sets referer as a specific team thread.
        /// </summary>
        /// <param name="channel">Thread to refer to</param>
        public async Task SetReferer(ThreadChannel channel) {
            // Team that channel is in
            Team team = await channel.GetTeamAsync();
            // All groups of that team
            IList<Group> groups = await team.GetGroupsAsync();
            // Sets new referer
            Referer = $"{team.Subdomain}/groups/{groups.FirstOrDefault(x => x.Id == channel.GroupId)}/channels/{channel.Id}/chat";
        }
        /// <summary>
        /// Sets referer as a specific DM channel.
        /// </summary>
        /// <param name="channel">DM channel to set referer as</param>
        public void SetReferer(DMChannel channel) =>
            Referer = $"{channel.Id}/chat";
        /// <summary>
        /// Sets referer as a specific team's overvierw, audit, member list or applications.
        /// </summary>
        /// <param name="team">Team to refer to</param>
        /// <param name="refer">To what it should refer to in the team</param>
        public void SetReferer(Team team, TeamRefer refer) =>
            Referer = $"{team.Subdomain}/{refer.ToString().ToLower()}";
        /// <summary>
        /// Creates a new websocket which focuses on a specific server.
        /// </summary>
        /// <param name="id">ID of the server. Nullable.</param>
        public void FocusOnTeam(GId id) =>
            InitWebsocket(25, id != null ? $"teamId={id}" : "jwt=undefined");
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