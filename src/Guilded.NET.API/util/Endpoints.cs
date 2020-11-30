using RestSharp;

namespace Guilded.NET.API {
    /// <summary>
    /// Represents endpoint in Guilded API.
    /// </summary>
    public class Endpoint {
        public static readonly Endpoint LOGIN = new Endpoint("login", Method.POST);
        public static readonly Endpoint LOGOUT = new Endpoint("logout", Method.POST);
        public static readonly Endpoint PING = new Endpoint("users/me/ping", Method.POST);
        public static readonly Endpoint ME = new Endpoint("me?isLogin=false", Method.GET);
        public static readonly Endpoint PRESENCE = new Endpoint("users/me/presence", Method.POST);
        public static readonly Endpoint STATUS = new Endpoint("users/me/status", Method.POST);
        public static readonly Endpoint PASSWORD = new Endpoint("users/me/password", Method.POST);
        public static readonly Endpoint UPLOAD_MEDIA = new Endpoint("media/upload?dynamicMediaTypeId=ContentMedia", Method.POST);
        /// <summary>
        /// Path to the Rest Endpoint.
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
        /// <param name="path">Path of the Rest Endpoint</param>
        /// <param name="method">Method of the Rest Endpoint</param>
        public Endpoint(string path, Method method) {
            Path = path;
            EndpointMethod = method;
        }
    }
}