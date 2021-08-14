using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Timers;
using Websocket.Client;

namespace Guilded.NET.Base
{
    using Events;
    using Websocket.Client.Exceptions;

    /// <summary>
    /// A base for Guilded client.
    /// </summary>
    /// <remarks>
    /// A base type for all Guilded.NET client containing WebSocket and REST things, as well as abstract methods to be overriden.
    /// </remarks>
    public abstract partial class BaseGuildedClient
    {
        internal const int welcome_opc = 1, resume_opc = 2;
        /// <summary>
        /// A dictionary of all websocket clients.
        /// </summary>
        /// <seealso cref="Rest"/>
        /// <value>Websocket dictionary</value>
        public Dictionary<string, WebsocketClient> Websockets
        {
            get; set;
        } = new Dictionary<string, WebsocketClient>();
        /// <summary>
        /// A span of time between each heartbeat.
        /// </summary>
        /// <remarks>
        /// <para>A span of time in milliseconds between each heartbeat.</para>
        /// <para>This automatically gets set once WebSocket gets initiated and receives interval from Welcome message.</para>
        /// </remarks>
        /// <value>Milliseconds</value>
        public int HeartbeatInterval
        {
            get; set;
        } = 22500;
        /// <summary>
        /// A timer for heartbeats.
        /// </summary>
        /// <remarks>
        /// A timer used for sending WebSocket heartbeats to Guilded.
        /// </remarks>
        /// <value>Timer</value>
        protected Timer HeartbeatTimer
        {
            get; set;
        }
        /// <summary>
        /// An event when WebSocket receives a message.
        /// </summary>
        /// <remarks>
        /// An event when WebSocket receives any kind of message from Guilded.
        /// </remarks>
        private event EventHandler<GuildedEvent> WebsocketMessageEvent;
        /// <summary>
        /// An event when WebSocket receives a message.
        /// </summary>
        /// <remarks>
        /// An event when WebSocket receives any kind of message from Guilded.
        /// </remarks>
        protected event EventHandler<GuildedEvent> WebsocketMessage
        {
            add => WebsocketMessageEvent += value;
            remove => WebsocketMessageEvent -= value;
        }
        /// <summary>
        /// Initializes a new WebSocket client.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new WebSocket client and adds it to <see cref="Websockets"/>.</para>
        /// <para>If <paramref name="lastMessageId"/> is passed, it gets all of the events that occurred after that message.</para>
        /// </remarks>
        /// <param name="lastMessageId">The identifier of the last event before WebSocket disconnection</param>
        /// <exception cref="WebsocketException">Either <paramref name="lastMessageId"/> or <see cref="AdditionalHeaders"/> has a bad formatting</exception>
        /// <returns>Created websocket</returns>
        protected virtual async Task<WebsocketClient> InitWebsocket(string lastMessageId = null)
        {
            WebsocketLogger.Debug("Creating a new websocket");
            // Initialize WebSocket factory and WebSocket sub-client
            Func<ClientWebSocket> factory = new Func<ClientWebSocket>(() =>
            {
                // Create a sub-client
                ClientWebSocket socket = new ClientWebSocket
                {
                    Options = {
                        Cookies = GuildedCookies
                    }
                };
                // Adds additional headers to this WebSocket, such as authentication header
                foreach (KeyValuePair<string, string> header in AdditionalHeaders)
                    socket.Options.SetRequestHeader(header.Key, header.Value);
                // If last event ID is passed, add it as a header
                if (!string.IsNullOrWhiteSpace(lastMessageId))
                    socket.Options.SetRequestHeader("guilded-last-message-id", lastMessageId);
                // Returns the made WebSocket sub-client
                return socket;
            });
            // Creates a new WebSocket based on factory we made
            WebsocketClient client = new WebsocketClient(
                GuildedUrl.Websocket,
                factory
            );
            // Add it to the WebSocket dictionary
            Websockets.Add("", client);
            // Subscribe to WebSocket messages
            client.MessageReceived.Subscribe(WebsocketMessageReceived);
            // Starts it
            WebsocketLogger.Verbose("Starting a websocket");
            await client.StartOrFail();
            // Returns this WebSocket
            return client;
        }
        /// <summary>
        /// Initializes a new WebSocket client.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new WebSocket client and adds it to <see cref="Websockets"/>.</para>
        /// <para>If <paramref name="event"/> is passed, it gets all of the events that occurred after that message.</para>
        /// </remarks>
        /// <param name="event">The last event before WebSocket disconnection</param>
        /// <exception cref="WebsocketException">Either <paramref name="event"/>'s identifier or <see cref="AdditionalHeaders"/> has a bad formatting</exception>
        /// <returns>Created websocket</returns>
        protected virtual async Task<WebsocketClient> InitWebsocket(GuildedEvent @event) =>
            await InitWebsocket(@event.MessageId);
        /// <summary>
        /// Used for when any WebSocket receives a message.
        /// </summary>
        /// <remarks>
        /// <para>An event handler method that gets called once any message is received from a WebSocket.</para>
        /// <para>You can override it if you don't like how Guilded.NET handles events or need any additional changes/features to it.</para>
        /// </remarks>
        /// <param name="msg">Websocket message</param>
        protected virtual void WebsocketMessageReceived(ResponseMessage msg)
        {
            WebsocketLogger.Debug("Received websocket event with type {Type}", msg.MessageType);
            if (msg.MessageType == WebSocketMessageType.Text)
            {
                // Deserializes it
                GuildedEvent @event = Deserialize<GuildedEvent>(msg.Text);
                // Checks if it's welcome event
                if (@event is { EventName: null, Opcode: welcome_opc })
                    HeartbeatInterval = @event.RawData.Value<int>("heartbeatIntervalMs");
                // Only send received data in debug version
#if DEBUG
                WebsocketLogger.Verbose("Received websocket data:\n{Data}", msg.Text);
#endif
                WebsocketMessageEvent?.Invoke(this, @event);
            }
        }
        /// <summary>
        /// Sends a heartbeat.
        /// </summary>
        /// <remarks>
        /// Sends a heartbeat through all WebSocket clients in <see cref="Websockets"/> dictionary. 
        /// </remarks>
        /// <param name="sender">Who invoked the event</param>
        /// <param name="args">Arguments of the timer's elapsed event</param>
        protected virtual void SendHeartbeat(object sender, ElapsedEventArgs args)
        {
            ApiLogger.Verbose("Sending a heartbeat");
            // Websocket sends ping
            foreach (WebsocketClient client in Websockets.Values)
                client.Send("2");
        }
    }
}