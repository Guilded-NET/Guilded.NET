using System.Threading.Tasks;
using RestSharp;
using System;
using System.Net.WebSockets;
using Websocket.Client;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Guilded.NET.API {
    /// <summary>
    /// A base for Guilded client.
    /// </summary>
    public abstract class BaseGuildedClient: IDisposable {
        /// <summary>
        /// A random for generating IDs.
        /// </summary>
        protected static readonly Random IdRandom = new Random();
        /// <summary>
        /// When no team is given, use this query.
        /// </summary>
        static readonly string emptyTeam = "jwt=undefined";
        /// <summary>
        /// An additional query to websockets.
        /// </summary>
        internal static readonly string WebsocketQuery = "EIO=3&transport=websocket";
        /// <summary>
        /// Events when client gets Connected/Disconnected.
        /// </summary>
        protected EventHandler ConnectedEvent, DisconnectedEvent;
        /// <summary>
        /// Thread for heartbeats.
        /// </summary>
        /// <value>Thread</value>
        protected Thread HeartbeatThread {
            get; set;
        }
        /// <summary>
        /// Token for cancelling heartbeat thread.
        /// </summary>
        /// <value>Cancellation Token</value>
        protected CancellationTokenSource HeartbeatToken {
            get; set;
        }
        /// <summary>
        /// Guilded API URL.
        /// </summary>
        /// <value>URL</value>
        public static readonly Uri GuildedAPIURL = new Uri("https://api.guilded.gg/");
        /// <summary>
        /// Guilded media URL. Allows you to upload images and videos.
        /// </summary>
        /// <value>URL</value>
        public static readonly Uri GuildedMediaURL = new Uri("https://media.guilded.gg/");
        /// <summary>
        /// Guilded image CDN URL.
        /// </summary>
        /// <value>URL</value>
        public static readonly Uri GuildedImgURL = new Uri("https://img.guildedcdn.com/");
        /// <summary>
        /// Guilded Websocket URL.
        /// </summary>
        /// <value>URL</value>
        public static readonly string GuildedWebsocketURL = "wss://api.guilded.gg/socket.io";
        /// <summary>
        /// Default span of time between heartbeats.
        /// </summary>
        /// <value>Seconds</value>
        public static readonly double DefaultHeartbeat = 25;
        /// <summary>
        /// Rest client for Guilded.
        /// </summary>
        /// <seealso cref="Websocket"/>
        /// <value>Rest client</value>
        protected internal RestClient Rest {
            get; set;
        }
        /// <summary>
        /// Websocket clients for Guilded. Initialize websocket with <see cref="InitWebsocket"/>.
        /// </summary>
        /// <seealso cref="Rest"/>
        /// <value>String, websocket dictionary</value>
        public Dictionary<string, WebsocketClient> Websockets {
            get; set;
        }
        /// <summary>
        /// Span of time between each heartbeat.
        /// </summary>
        /// <value>Seconds</value>
        public double HeartbeatTime {
            get; set;
        }
        /// <summary>
        /// Event when client connects to the Guilded.
        /// </summary>
        public event EventHandler Connected {
            add => ConnectedEvent += value;
            remove => ConnectedEvent -= value;
        }
        /// <summary>
        /// Event when client disconnects from Guilded.
        /// </summary>
        public event EventHandler Disconnected {
            add => DisconnectedEvent += value;
            remove => DisconnectedEvent += value;
        }
        /// <summary>
        /// Cookies given when client logs in.
        /// </summary>
        /// <value>Login cookies</value>
        public IList<GuildedCookie> LoginCookies {
           get; set;
        }
        /// <summary>
        /// To what this event is referring to. Allows Guilded to keep track of where you are right now.
        /// </summary>
        /// <value>Referer header</value>
        public string Referer {
            get; set;
        }
        /// <summary>
        /// A base for Guilded client.
        /// </summary>
        /// <param name="apiUrl">URL of Guilded API</param>
        /// <exception cref="ArgumentNullException">When apiurl or socketurl are null</exception>
        /// <exception cref="UriFormatException">When apiurl or socketurl are invalid</exception>
        protected BaseGuildedClient(Uri apiUrl) {
            // Initialize Rest client
            Rest = new RestClient(apiUrl ?? throw new ArgumentNullException($"{nameof(apiUrl)} can not be empty."));
            // Because the client has not logged in yet
            LoginCookies = null;
            // Refer to nothing
            Referer = "find/team";
            // Sets heartbeat as 25 seconds
            HeartbeatTime = 25;
            // Sets heartbeat things as null
            (HeartbeatToken, HeartbeatThread) = (null, null);
            // 0 websockets
            Websockets = new Dictionary<string, WebsocketClient>();
        }
        /// <summary>
        /// A base for Guilded client.
        /// </summary>
        /// <exception cref="ArgumentNullException">When apiurl or socketurl are null</exception>
        /// <exception cref="UriFormatException">When apiurl or socketurl are invalid</exception>
        protected BaseGuildedClient(): this(GuildedAPIURL) {}
        /// <summary>
        /// Sends a request to Guilded's API with given arguments.
        /// </summary>
        /// <param name="endpoint">Guilded API endpoint</param>
        /// <param name="addables">Args to be given to that endpoint</param>
        /// <param name="enableCookies">If it should add cookies to the request</param>
        /// <exception cref="GuildedException">When Guilded itself throws an error</exception>
        /// <typeparam name="T">Type of the response</typeparam>
        /// <returns>Request response</returns>
        public async Task<ExecutionResponse<T>> ExecuteRequest<T>(Endpoint endpoint, bool enableCookies, params IReqAddable[] addables) {
            // Create new request
            RestRequest req = new RestRequest(endpoint.Path, endpoint.EndpointMethod);
            // Add parameters
            foreach(var addable in enableCookies && LoginCookies != null ? addables.Concat(LoginCookies) : addables)
                addable.AddTo(req);
            // To what it is referring
            req.AddHeader("referer", $"https://guilded.gg/{Referer}/");
            // Executes response
            IRestResponse<T> response = await Rest.ExecuteAsync<T>(req);
            // Check if content isn't null 
            if(string.IsNullOrEmpty(response.Content)) return new ExecutionResponse<T>(response);
            // Parses it
            JToken token = JToken.Parse(response.Content);
            // Check if it has 2 properties
            if(token.Type == JTokenType.Object) {
                // Gets object
                JObject obj = (JObject)token;
                // Check if it's an error
                if(obj.ContainsKey("code") && obj.ContainsKey("message") && obj.Properties().Count() == 2) {
                    // If it does, treat it as an error
                    GuildedException exc = new GuildedException() {
                        Code = obj["code"].Value<string>(),
                        ErrorMessage = obj["message"].Value<string>()
                    };
                    // Returns null
                    throw exc;
                }
            }
            // Returns it
            return new ExecutionResponse<T>(response);
        }
        /// <summary>
        /// Sends a request to Guilded's API with given arguments.
        /// </summary>
        /// <param name="endpoint">Guilded API endpoint</param>
        /// <param name="addables">Args to be given to that endpoint</param>
        /// <exception cref="GuildedException">When Guilded itself throws an error</exception>
        /// <typeparam name="T">Type of the response</typeparam>
        /// <returns>Request response</returns>
        public async Task<ExecutionResponse<T>> ExecuteRequest<T>(Endpoint endpoint, params IReqAddable[] addables) =>
            await ExecuteRequest<T>(endpoint, true, addables);
        /// <summary>
        /// Sends a request to Guilded's API with given arguments.
        /// </summary>
        /// <param name="endpoint">Guilded API endpoint</param>
        /// <param name="addables">Args to be given to that endpoint</param>
        /// <param name="enableCookies">If it should add cookies to the request</param>
        /// <exception cref="GuildedException">When Guilded itself throws an error</exception>
        /// <returns>Request response</returns>
        public async Task<ExecutionResponse<object>> ExecuteRequest(Endpoint endpoint, bool enableCookies, params IReqAddable[] addables) =>
            await ExecuteRequest<object>(endpoint, enableCookies, addables);
        /// <summary>
        /// Sends a request to Guilded's API with given arguments.
        /// </summary>
        /// <param name="endpoint">Guilded API endpoint</param>
        /// <param name="addables">Args to be given to that endpoint</param>
        /// <exception cref="GuildedException">When Guilded itself throws an error</exception>
        /// <returns>Request response</returns>
        public async Task<ExecutionResponse<object>> ExecuteRequest(Endpoint endpoint, params IReqAddable[] addables) =>
            await ExecuteRequest(endpoint, true, addables);
        /// <summary>
        /// Uploads image to Guilded.
        /// </summary>
        /// <param name="filepath">Path of the file</param>
        /// <param name="filedata">Data of the file</param>
        /// <exception cref="GuildedException">When Guilded itself throws an error</exception>
        /// <returns>Image URL</returns>
        public async Task<Uri> UploadImage(string filepath, byte[] filedata) {
            if(string.IsNullOrWhiteSpace(filepath)) throw new ArgumentException($"{nameof(filepath)} can not be empty.");
            // Create new request
            RestRequest req = new RestRequest($"{GuildedMediaURL}/{Endpoint.UPLOAD_MEDIA.Path}?dynamicMediaTypeId=ContentMedia", Endpoint.UPLOAD_MEDIA.EndpointMethod) {
                AlwaysMultipartFormData = true
            };
            // Add parameters
            if(LoginCookies != null)
                foreach(var addable in LoginCookies)
                    addable.AddTo(req);
            // To what it is referring
            req.AddHeader("referer", $"https://guilded.gg/{Referer}/");
            // Adds that file
            req.AddFile(Path.GetFileName(filepath), filedata, filepath);
            // Adds a header telling its type
            req.AddHeader("Content-Type", "multipart/form-data");
            // Sends that request and gets URL from it
            return await GetMedia(req);
        }
        /// <summary>
        /// Uploads image to Guilded.
        /// </summary>
        /// <param name="url">Link to the image</param>
        /// <exception cref="GuildedException">When Guilded itself throws an error</exception>
        /// <returns>Image URL</returns>
        public async Task<Uri> UploadImage(Uri url) {
            if(url == null) throw new ArgumentException($"{nameof(url)} can not be null.");
            // Create new request
            RestRequest req = new RestRequest($"{GuildedMediaURL}/{Endpoint.UPLOAD_MEDIA.Path}", Endpoint.UPLOAD_MEDIA.EndpointMethod);
            // Add parameters
            if(LoginCookies != null)
                foreach(var addable in LoginCookies)
                    addable.AddTo(req);
            // To what it is referring
            req.AddHeader("referer", $"https://guilded.gg/{Referer}/");
            // Adds a new JSON
            /* {
             *   "mediaInfo": { "src": "url" },
             *   "dynamicMediaTypeId": "ContentMedia",
             *   "uploadTrackingId": "r-1000000-1000000"
             * }
             */
            req.AddJsonBody($"{{ \"mediaInfo\": {{ \"src\": \"{url}\" }}, \"dynamicMediaTypeId\": \"ContentMedia\", \"uploadTrackingId\": \"r-{IdRandom.Next(1000000, int.MaxValue)}-{IdRandom.Next(1000000, int.MaxValue)}\" }}");
            // Sends that request and gets URL from it
            return await GetMedia(req);
        }
        /// <summary>
        /// Sends a request and gets media URL from it.
        /// </summary>
        /// <param name="request">Request to send</param>
        /// <returns>Media URL</returns>
        async Task<Uri> GetMedia(RestRequest request) {
            // Executes response
            IRestResponse<object> response = await Rest.ExecuteAsync<object>(request);
            // Check if content isn't null and get it as JSON object
            if(string.IsNullOrEmpty(response.Content)) return null;
            // Gets it as a JSON token
            JToken token = JToken.Parse(response.Content);
            // Check if it has 2 properties
            if(token.Type == JTokenType.Object) {
                // Gets object
                JObject obj = (JObject)token;
                // Check if it's an error
                if(obj.ContainsKey("code") && obj.ContainsKey("message") && obj.Properties().Count() == 2) {
                    // If it is, then throw it as an error
                    throw new GuildedException() {
                        Code = obj["code"].Value<string>(),
                        ErrorMessage = obj["message"].Value<string>()
                    };
                }
                // If it has property "url" and it's the only property.
                else if(obj.ContainsKey("url") && obj.Properties().Count() == 1) return new Uri(obj["url"].Value<string>());
            }
            // Else, return null
            return null;
        }
        /// <summary>
        /// Initializes websocket.
        /// </summary>
        /// <param name="reconnection">Seconds of time between each reconnection</param>
        /// <param name="additionalQuery">Query which should be added to the websocket link</param>
        public virtual WebsocketClient InitWebsocket(double? reconnection = null, string additionalQuery = null) {
            // Initialize Websocket client
            var factory = new Func<ClientWebSocket>(() => new ClientWebSocket {
                // Options of the ClientWebSocket
                Options = {
                    KeepAliveInterval = TimeSpan.FromSeconds(HeartbeatTime),
                    Cookies = CookieUtil.From(LoginCookies)
                }
            });
            // Creates a new websocket
            WebsocketClient client = new WebsocketClient(new Uri($"{GuildedWebsocketURL}/?{(string.IsNullOrWhiteSpace(additionalQuery) ? emptyTeam : additionalQuery)}&{WebsocketQuery}"), factory);
            // Reconnection
            if(reconnection != null)
                client.ReconnectTimeout = TimeSpan.FromSeconds(reconnection.Value);
            // Adds it to websocket dictionary
            Websockets.Add(additionalQuery ?? "", client);
            // Returns that websocket
            return client;
        }
        /// <summary>
        /// Removes a websocket from a specific team.
        /// </summary>
        /// <param name="teamId">ID of the team to remove websocket in</param>
        public virtual void RemoveWebsocket(string teamId = null) {
            // Gets a key for the team
            string teamQuery = "teamId=" + teamId;
            // If that websocket exists
            if(Websockets.ContainsKey(teamQuery)) {
                // Disposes that websocket
                Websockets[teamQuery].Dispose();
                // Removes it from dictionary
                Websockets.Remove(teamQuery);
            }
        }
        /// <summary>
        /// Connects to Guilded client/user.
        /// </summary>
        public abstract Task ConnectAsync();
        /// <summary>
        /// Disconnects from Guilded client/user.
        /// </summary>
        public abstract Task DisconnectAsync();
        /// <summary>
        /// Disposes BaseGuildedClient.
        /// </summary>
        public virtual void Dispose() {
            HeartbeatToken?.Cancel();
            HeartbeatThread?.Join();
            // Disposes all websockets
            foreach(WebsocketClient client in Websockets.Values) client.Dispose();
        }
        /// <summary>
        /// Method for a heartbeat thread.
        /// </summary>
        /// <param name="token">Token for cancelling while loop</param>
        /// <exception cref="GuildedException">When it fails to send a ping through REST client</exception>
        protected virtual async Task HeartbeatThreadMethod(CancellationToken token) {
            // Turn seconds into milliseconds
            int ms = (int)HeartbeatTime * 1000;
            // If thread wasn't cancelled
            while(!token.IsCancellationRequested) {
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
        protected virtual async Task SendHeartbeat(string value) {
            // Websocket sends ping
            foreach(WebsocketClient client in Websockets.Values) client.Send(value);
            // Rest client sends a ping too
            await ExecuteRequest<object>(Endpoint.PING);
        }
    }
}
