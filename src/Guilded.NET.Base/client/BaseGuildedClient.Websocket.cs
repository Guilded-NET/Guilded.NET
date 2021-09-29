using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Timers;
using Websocket.Client;
using Websocket.Client.Exceptions;

namespace Guilded.NET.Base
{
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using Events;
    public abstract partial class BaseGuildedClient
    {
        internal const int welcome_opcode = 1, error_opcode = 8;
        /// <summary>
        /// The default timespan between each interval in milliseconds.
        /// </summary>
        public const double DefaultHeartbeatInterval = 22500;
        /// <summary>
        /// The WebSocket that will be used by the client.
        /// </summary>
        /// <remarks>
        /// <para>The WebSocket that will be used by the client to receive all Guilded events and event messages.</para>
        /// </remarks>
        /// <seealso cref="Rest"/>
        /// <value>Main WebSocket</value>
        public WebsocketClient Websocket
        {
            get; set;
        }
        /// <summary>
        /// A timer for heartbeats.
        /// </summary>
        /// <remarks>
        /// <para>A timer that sends a heartbeat through WebSockets to Guilded. This ensures that WebSockets are still connected to Guilded.</para>
        /// </remarks>
        /// <value>Timer</value>
        protected Timer HeartbeatTimer
        {
            get; set;
        }
        private readonly Subject<GuildedSocketMessage> OnWebsocketMessage = new Subject<GuildedSocketMessage>();
        /// <summary>
        /// An event when WebSocket receives a message.
        /// </summary>
        /// <remarks>
        /// <para>An event when WebSocket receives any kind of message from Guilded.</para>
        /// <para>If event with opcode <c>8</c> is received, it is given as an exception instead.</para>
        /// </remarks>
        /// <exception cref="GuildedWebsocketException">Received when any kind of error is received. Handled through <see cref="Subject{T}.OnError(Exception)"/>.</exception>
        protected IObservable<GuildedSocketMessage> WebsocketMessage => OnWebsocketMessage.AsObservable();
        /// <summary>
        /// Initializes a new WebSocket client.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new WebSocket client and sets it to <see cref="Websocket"/>.</para>
        /// <para>If <paramref name="lastMessageId"/> is passed, it gets all of the events that occurred after that message.</para>
        /// </remarks>
        /// <param name="lastMessageId">The identifier of the last event before WebSocket disconnection</param>
        /// <param name="websocketUrl">The URL to which WebSocket will connect</param>
        /// <exception cref="WebsocketException">Either <paramref name="lastMessageId"/> or <see cref="AdditionalHeaders"/> has a bad formatting</exception>
        /// <returns>Created websocket</returns>
        /// <seealso cref="ResumeEvent"/>
        /// <seealso cref="InitWebsocket(GuildedSocketMessage)"/>
        protected virtual async Task<WebsocketClient> InitWebsocket(string lastMessageId = null, Uri websocketUrl = null)
        {
            Func<ClientWebSocket> factory = new Func<ClientWebSocket>(() =>
            {
                ClientWebSocket socket = new ClientWebSocket
                {
                    // Options = {
                    //     Cookies = GuildedCookies
                    // }
                };
                // Add additional headers for authentication tokens and such
                foreach (KeyValuePair<string, string> header in AdditionalHeaders)
                    socket.Options.SetRequestHeader(header.Key, header.Value);
                // If event identifier is passed, get every event after the given identifier
                if (!string.IsNullOrWhiteSpace(lastMessageId))
                    socket.Options.SetRequestHeader("guilded-last-message-id", lastMessageId);

                return socket;
            });
            WebsocketClient client = new WebsocketClient
            (
                websocketUrl ?? GuildedUrl.Websocket,
                factory
            );

            client.MessageReceived
                .Where(msg => msg.MessageType == WebSocketMessageType.Text)
                .Subscribe(OnWebsocketResponse);
            await client.StartOrFail().ConfigureAwait(false);

            return Websocket = client;
        }
        /// <summary>
        /// Initializes a new WebSocket client.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new WebSocket client and sets it to <see cref="Websocket"/>.</para>
        /// <para>If <paramref name="event"/> is passed, it gets all of the events that occurred after that message.</para>
        /// </remarks>
        /// <param name="event">The last event before WebSocket disconnection</param>
        /// <exception cref="WebsocketException">Either <paramref name="event"/>'s identifier or <see cref="AdditionalHeaders"/> has a bad formatting</exception>
        /// <returns>Created websocket</returns>
        /// <seealso cref="ResumeEvent"/>
        /// <seealso cref="InitWebsocket(string, Uri)"/>
        protected virtual async Task<WebsocketClient> InitWebsocket(GuildedSocketMessage @event) =>
            await InitWebsocket(@event.MessageId).ConfigureAwait(false);
        /// <summary>
        /// Used for when any WebSocket receives a message.
        /// </summary>
        /// <remarks>
        /// <para>An event handler method that gets called once any message is received from a WebSocket.</para>
        /// <para>Override this if you don't like how Guilded.NET handles events or need any additional changes/features to it.</para>
        /// </remarks>
        /// <param name="response">The response received from Guilded WebSocket</param>
        protected virtual void OnWebsocketResponse(ResponseMessage response)
        {
            GuildedSocketMessage @event = Deserialize<GuildedSocketMessage>(response.Text);
            // Check for a welcome message to change hearbeat interval
            if (@event.Opcode == welcome_opcode)
            {
                HeartbeatTimer.Interval = @event.RawData.Value<double>("heartbeatIntervalMs");
            }
            // If it's an error, just send it through OnWebsocketMessage as an error
            else if(@event.Opcode == error_opcode)
            {
                OnWebsocketMessage.OnError(
                    new GuildedWebsocketException(response, @event.RawData.Value<string>("message"))
                );
                return;
            }
            OnWebsocketMessage.OnNext(@event);
        }
        /// <summary>
        /// Sends a heartbeat.
        /// </summary>
        /// <remarks>
        /// <para>Sends a heartbeat through <see cref="Websocket"/> client.</para>
        /// </remarks>
        /// <param name="sender">Who invoked the event</param>
        /// <param name="args">Arguments of the timer's elapsed event</param>
        protected virtual void SendHeartbeat(object sender, ElapsedEventArgs args) =>
            // Send ping through WebSocket to show that it's not dead
            Websocket.Send("2");
    }
}