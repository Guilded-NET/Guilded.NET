using System;
using System.Net.WebSockets;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

using Websocket.Client;

namespace Guilded.NET.API
{
    using Objects;
    public abstract partial class BaseGuildedClient
    {
        /// <summary>
        /// An event when Guilded gives a heartbeat response.
        /// </summary>
        event EventHandler<int> HeartbeatEvent;
        /// <summary>
        /// An event when Guilded gives a heartbeat response.
        /// </summary>
        public event EventHandler<int> HeartbeatResponse
        {
            add => HeartbeatEvent += value;
            remove => HeartbeatEvent -= value;
        }
        /// <summary>
        /// Thread for heartbeats.
        /// </summary>
        /// <value>Thread</value>
        protected Thread HeartbeatThread
        {
            get; set;
        }
        /// <summary>
        /// Token for cancelling heartbeat thread.
        /// </summary>
        /// <value>Cancellation Token</value>
        protected CancellationTokenSource HeartbeatToken
        {
            get; set;
        }
        /// <summary>
        /// Event when Websocket receives a message.
        /// </summary>
        event EventHandler<SocketMessage> GuildedWebsocketMessageEvent;
        /// <summary>
        /// Event when Websocket receives a message.
        /// </summary>
        protected event EventHandler<SocketMessage> GuildedWebsocketMessage
        {
            add => GuildedWebsocketMessageEvent += value;
            remove => GuildedWebsocketMessageEvent -= value;
        }
        /// <summary>
        /// Regexp for numbers at the start of Guilded's mess
        /// </summary>
        /// <returns></returns>
        protected static Regex NumberStart = new Regex("^([0-9]+)");
        /// <summary>
        /// Used for when Websocket receives a message.
        /// </summary>
        /// <param name="msg">Websocket message</param>
        protected virtual void WebsocketMessageReceived(ResponseMessage msg)
        {
            if (msg.MessageType == WebSocketMessageType.Text)
            {
                // Matches the number using Regex
                string strnum = NumberStart.Match(msg.Text).Value;
                // Parses the number
                uint.TryParse(strnum, out uint num);
                // Trimmed string
                string trimmed = msg.Text[strnum.Length..];
                // If there is nothing else besides number, invoke the event
                if (string.IsNullOrWhiteSpace(trimmed))
                {
                    GuildedWebsocketMessageEvent?.Invoke(this, new SocketMessage(num));
                    return;
                }
                // Parses it as token
                JToken token = JToken.Parse(trimmed);
                // Get type of the socket message
                if (token.Type == JTokenType.Array)
                {
                    JArray array = (JArray)token;
                    // If first item is string and second item is object, then it's SocketEvent
                    if (array[0].Type == JTokenType.String && array[1].Type == JTokenType.Object)
                    {
                        // Get first item as value and second item as object
                        JValue value = (JValue)array[0];
                        JObject obj = (JObject)array[1];
                        // Invoke the event
                        GuildedWebsocketMessageEvent?.Invoke(this, new SocketEvent(num, obj, value.ToString()));
                    }
                }
                else if (token.Type == JTokenType.Object)
                {
                    // Get token as object
                    JObject jobj = (JObject)token;
                    // Invoke the event
                    GuildedWebsocketMessageEvent?.Invoke(this, new ObjectMessage(num, jobj));
                }
            }
        }
        /// <summary>
        /// Invokes a heartbeat event.
        /// </summary>
        /// <param name="sender">Who is invoking the event</param>
        /// <param name="value">Heartbeat response</param>
        protected void InvokeHeartbeatEvent(object sender, int value) => HeartbeatEvent?.Invoke(sender, value);
        /// <summary>
        /// Method for a heartbeat thread.
        /// </summary>
        /// <param name="token">Token for cancelling while loop</param>
        /// <exception cref="GuildedException">When it fails to send a ping through REST client</exception>
        protected virtual async Task HeartbeatThreadMethod(CancellationToken token)
        {
            // Turn seconds into milliseconds
            int ms = (int)HeartbeatTime * 1000;
            // If thread wasn't cancelled
            while (!token.IsCancellationRequested)
            {
                // Sends a heartbeat
                await SendHeartbeat("2");
                // Make it sleep until the next hearbeat
                Thread.Sleep(ms);
            }
        }
        /// <summary>
        /// Sends a heartbeat to the websocket server.
        /// </summary>
        /// <param name="value">Heartbeat value</param>
        /// <exception cref="GuildedException">When it fails to send a ping through REST client</exception>
        protected virtual async Task SendHeartbeat(string value)
        {
            // Websocket sends ping
            foreach (WebsocketClient client in Websockets.Values) client.Send(value);
            // Rest client sends a ping too
            await ExecuteRequest<object>(Endpoint.PING);
        }
        /// <summary>
        /// Removes a websocket from a specific team.
        /// </summary>
        /// <param name="teamId">ID of the team to remove websocket in</param>
        public virtual void RemoveWebsocket(GId teamId)
        {
            // Gets a key for the team
            string teamQuery = "teamId=" + teamId;
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