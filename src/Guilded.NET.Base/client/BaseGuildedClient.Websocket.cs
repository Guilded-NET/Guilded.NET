using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Timers;
using Websocket.Client;

namespace Guilded.NET.Base
{
    using Events;
    public abstract partial class BaseGuildedClient
    {
        internal const int welcome_opc = 1, resume_opc = 2;
        /// <summary>
        /// A dictionary of all websocket clients.
        /// </summary>
        /// <value>Websocket dictionary</value>
        public Dictionary<string, WebsocketClient> Websockets
        {
            get; set;
        } = new Dictionary<string, WebsocketClient>();
        /// <summary>
        /// Span of time between each heartbeat.
        /// </summary>
        /// <value>Milliseconds</value>
        public int HeartbeatInterval
        {
            get; set;
        } = 22500;
        /// <summary>
        /// Timer for heartbeats.
        /// </summary>
        /// <value>Timer</value>
        protected Timer HeartbeatTimer
        {
            get; set;
        }
        /// <summary>
        /// Event when Websocket receives a message.
        /// </summary>
        private event EventHandler<GuildedEvent> WebsocketMessageEvent;
        /// <summary>
        /// Event when Websocket receives a message.
        /// </summary>
        protected event EventHandler<GuildedEvent> WebsocketMessage
        {
            add => WebsocketMessageEvent += value;
            remove => WebsocketMessageEvent -= value;
        }
        /// <summary>
        /// Initializes websocket for Guilded API.
        /// </summary>
        /// <returns>Created websocket</returns>
        public virtual WebsocketClient InitWebsocket()
        {
            WebsocketLogger.Debug("Creating a new websocket");
            // Initialize Websocket client
            Func<ClientWebSocket> factory = new Func<ClientWebSocket>(() => {
                // Creates new websocket
                ClientWebSocket socket = new ClientWebSocket
                {
                    // Options of the ClientWebSocket
                    Options = {
                        Cookies = GuildedCookies
                    }
                };
                // Adds additional headers to this WebSocket, such as authentication header
                foreach(KeyValuePair<string, string> header in AdditionalHeaders)
                    socket.Options.SetRequestHeader(header.Key, header.Value);
                // Returns the made websocket
                return socket;
            });
            // Creates a new websocket
            WebsocketClient client = new WebsocketClient(
                GuildedUrl.Websocket,
                factory
            );
            // Adds it to websocket dictionary
            Websockets.Add("", client);
            // Subscribe to message event, so we could get events such as message creation event
            client.MessageReceived.Subscribe(WebsocketMessageReceived);
            // Start that websocket
            WebsocketLogger.Verbose("Starting the websocket");
            client.StartOrFail().GetAwaiter().GetResult();
            // Returns that websocket
            return client;
        }
        /// <summary>
        /// Used for when Websocket receives a message.
        /// </summary>
        /// <param name="msg">Websocket message</param>
        protected virtual void WebsocketMessageReceived(ResponseMessage msg)
        {
            WebsocketLogger.Debug("Received websocket event with type {Type}", msg.MessageType);
            if (msg.MessageType == WebSocketMessageType.Text)
            {
                // Deserializes it
                GuildedEvent @event = Deserialize<GuildedEvent>(msg.Text);
                // Checks if it's welcome event
                if(@event is { EventName: null, Opcode: welcome_opc })
                    HeartbeatInterval = @event.RawData.Value<int>("heartbeatIntervalMs");
                // Only send received data in debug version
                #if DEBUG
                    WebsocketLogger.Verbose("Received websocket data:\n{Data}", msg.Text);
                #endif
                WebsocketMessageEvent?.Invoke(this, @event);
            }
        }
        /// <summary>
        /// Sends a heartbeat to the websocket server.
        /// </summary>
        /// <param name="sender">Who invoked the event</param>
        /// <param name="args">Arguments of the timer's elapsed event</param>
        /// <exception cref="GuildedException">When it fails to send a ping through REST client</exception>
        protected virtual void SendHeartbeat(object sender, ElapsedEventArgs args)
        {
            ApiLogger.Verbose("Sending a heartbeat");
            // Websocket sends ping
            foreach (WebsocketClient client in Websockets.Values)
                client.Send("2");
        }
        /// <summary>
        /// Removes a websocket from a specific team.
        /// </summary>
        /// <param name="teamId">ID of the team to remove websocket in</param>
        public virtual void RemoveWebsocket(GId teamId)
        {
            WebsocketLogger.Debug("Removing a websocket from team with an ID '{TeamId}'", teamId);
            // Gets a key for the team
            string teamQuery = $"teamId={teamId}";
            // If that websocket exists
            if (Websockets.ContainsKey(teamQuery))
            {
                // Disposes that websocket
                Websockets[teamQuery].Dispose();
                // Removes it from dictionary
                Websockets.Remove(teamQuery);
            }
        }
    }
}