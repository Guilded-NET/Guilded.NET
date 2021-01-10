using RestSharp;

namespace Guilded.NET.API {
    /// <summary>
    /// Represents endpoint in Guilded API.
    /// </summary>
    public class Endpoint {
        /// <summary>
        /// Endpoint for logging into Guilded user.
        /// </summary>
        public static readonly Endpoint LOGIN = new Endpoint("login", Method.POST);
        /// <summary>
        /// Endpoint for logging out of the Guilded.
        /// </summary>
        public static readonly Endpoint LOGOUT = new Endpoint("logout", Method.POST);
        /// <summary>
        /// Endpoint for pinging Guilded.
        /// </summary>
        public static readonly Endpoint PING = new Endpoint("users/me/ping", Method.POST);
        /// <summary>
        /// Endpoint for getting this user.
        /// </summary>
        public static readonly Endpoint ME = new Endpoint("me", Method.GET);
        /// <summary>
        /// Endpoint for changing presence(online, invisible, idle, do not disturb) to other presence.
        /// </summary>
        public static readonly Endpoint PRESENCE = new Endpoint("users/me/presence", Method.POST);
        /// <summary>
        /// Endpoint for changing your status message and/or status emote.
        /// </summary>
        public static readonly Endpoint STATUS = new Endpoint("users/me/status", Method.POST);
        /// <summary>
        /// Endpoint for setting a new password.
        /// </summary>
        public static readonly Endpoint PASSWORD = new Endpoint("users/me/password", Method.POST);
        /// <summary>
        /// Endpoint for uploading media to Guilded and receiving new URL. Use Guilded media URL for this, not a normal Guilded API URL.
        /// </summary>
        public static readonly Endpoint UPLOAD_MEDIA = new Endpoint("media/upload", Method.POST);
        /// <summary>
        /// Path to the REST Endpoint.
        /// </summary>
        /// <value>Endpoint Path</value>
        public string Path {
            get; set;
        }
        /// <summary>
        /// Method of the Endpoint(GET, POST, ...).
        /// </summary>
        /// <value>EndPointMethod</value>
        public Method EndpointMethod {
            get; set;
        }
        /// <summary>
        /// Represents endpoint in Guilded API.
        /// </summary>
        /// <param name="path">Path of the REST Endpoint</param>
        /// <param name="method">Method of the REST Endpoint</param>
        public Endpoint(string path, Method method) =>
            (Path, EndpointMethod) = (path, method);
    }
}