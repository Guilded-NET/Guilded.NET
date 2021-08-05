using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Guilded.NET.Base
{
    /// <summary>
    /// A base for Guilded client.
    /// </summary>
    public abstract partial class BaseGuildedClient : IDisposable
    {
        /// <summary>
        /// Content types for each format.
        /// </summary>
        /// <returns>Content type dictionary</returns>
        public static readonly Dictionary<string, string> ContentType = new Dictionary<string, string>()
        {
            {"txt", "plain/text"},
            {"png", "image/png"},
            {"svg", "image/svg+xml"},
            {"jpeg", "image/jpeg"},
            {"jpg", "image/jpeg"},
            {"webp", "image/webp"}
        };
        /// <summary>
        /// Rest client for Guilded.
        /// </summary>
        /// <seealso cref="Websocket"/>
        /// <value>Rest client</value>
        protected internal IRestClient Rest
        {
            get; set;
        }
        /// <summary>
        /// Cookies received from client log-in.
        /// </summary>
        /// <value>Guilded cookies</value>
        public CookieContainer GuildedCookies
        {
            get; set;
        }
        /// <summary>
        /// Serializes object with client's Guilded serializer.
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <returns>Serialized object</returns>
        public string Serialize(object obj)
        {
            using StringWriter strWriter = new StringWriter();
            using JsonWriter writer = new JsonTextWriter(strWriter);

            GuildedSerializer.Serialize(writer, obj);
            return strWriter.ToString();
        }
        /// <summary>
        /// Deserializes JSON with client's Guilded serializer.
        /// </summary>
        /// <param name="json">JSON to deserialize</param>
        /// <typeparam name="T">Returning type</typeparam>
        /// <returns>Deserialized object</returns>
        public T Deserialize<T>(string json)
        {
            using StringReader strReader = new StringReader(json);
            using JsonReader reader = new JsonTextReader(strReader);

            // Deserializes JSON to the given type
            object deserialized = GuildedSerializer.Deserialize(reader, typeof(T));
            // Gets value we deserialized and casts it to the given type
            return (T)deserialized;
        }
        /// <summary>
        /// Sends a request to Guilded's API with given arguments.
        /// </summary>
        /// <param name="resource">The full URL of the endpoint</param>
        /// <param name="method">The action method of the endpoint</param>
        /// <param name="body">The object to be used as request's body</param>
        /// <param name="encodeQuery">Whether to encode all given queries</param>
        /// <param name="query">The dictionary of queries and their values</param>
        /// <param name="headers">The dictionary of headers and their values</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Request response</returns>
        public async Task<ExecutionResponse<object>> ExecuteRequest(Uri resource, Method method, object body = null, bool encodeQuery = true, IDictionary<string, string> query = null, IDictionary<string, string> headers = null) =>
            await SendRequest<object>(BuildRequest(new RestRequest(resource, method), body, encodeQuery, query, headers));
        /// <summary>
        /// Sends a request to Guilded's API with given arguments.
        /// </summary>
        /// <param name="resource">The path of the endpoint</param>
        /// <param name="method">The action method of the endpoint</param>
        /// <param name="body">The object to be used as request's body</param>
        /// <param name="encodeQuery">Whether to encode all given queries</param>
        /// <param name="query">The dictionary of queries and their values</param>
        /// <param name="headers">The dictionary of headers and their values</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Request response</returns>
        public async Task<ExecutionResponse<object>> ExecuteRequest(string resource, Method method, object body = null, bool encodeQuery = true, IDictionary<string, string> query = null, IDictionary<string, string> headers = null) =>
            await SendRequest<object>(BuildRequest(new RestRequest(resource, method), body, encodeQuery, query, headers));
        /// <summary>
        /// Builds upon given REST request.
        /// </summary>
        /// <param name="request">The request to build</param>
        /// <param name="body">The object to be used as request's body</param>
        /// <param name="encodeQuery">Whether to encode all given queries</param>
        /// <param name="query">The dictionary of queries and their values</param>
        /// <param name="headers">The dictionary of headers and their values</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Given request</returns>
        private IRestRequest BuildRequest(IRestRequest request, object body = null, bool encodeQuery = true, IDictionary<string, string> query = null, IDictionary<string, string> headers = null)
        {
            // Add body
            if (!(body is null))
                request.AddJsonBody(Serialize(body));
            // Add dictionary as query parameter list
            if (!(query is null))
                foreach (KeyValuePair<string, string> q in query)
                    request.AddQueryParameter(q.Key, q.Value, encodeQuery);
            // Add dictionary as header list
            if (!(headers is null))
                request.AddHeaders(headers);
            // Returns the request
            return request;
        }
        /// <summary>
        /// Uploads an image or a file to Guilded.
        /// </summary>
        /// <param name="filename">Path to the file</param>
        /// <param name="filedata">Content of the file</param>
        /// <param name="contentType">Content type for multipart form data</param>
        /// <exception cref="ArgumentException">If argument filename is empty or null</exception>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>File URL</returns>
        public async Task<Uri> UploadFileAsync(string filename, byte[] filedata, string contentType)
        {
            if (string.IsNullOrWhiteSpace(filename)) throw new ArgumentException($"{nameof(filename)} can not be empty.");
            ApiLogger.Verbose("Creating an image request");
            // Create new request
            IRestRequest req = new RestRequest($"{GuildedUrl.Media}/media/upload?dynamicMediaTypeId=ContentMedia", Method.POST, DataFormat.Json)
                // Adds that file
                .AddFile("file", filedata, filename, contentType)
                .AddParameter("uploadTrackingId", FormId.Random.ToString(), "multipart/form-data", ParameterType.GetOrPost)
                .AddParameter("Content-Type", "multipart/form-data");
            // Sends that request and gets response
            string content = (await SendRequest<object>(req)).Response.Content;
            // Gets object with url from the response
            JObject obj = JObject.Parse(content);
            // Checks if obj contains the property
            if (!obj.ContainsKey("url")) return null;
            // Returns the url property
            return obj["url"].ToObject<Uri>();
        }
        /// <summary>
        /// Uploads an image or a file to Guilded.
        /// </summary>
        /// <param name="filename">Path to the file</param>
        /// <param name="filedata">Content of the file</param>
        /// <exception cref="ArgumentException">If argument filename is empty or null</exception>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>File URL</returns>
        public async Task<Uri> UploadFileAsync(string filename, byte[] filedata)
        {
            // Gets file extension for content type 
            string ext = Path.GetExtension(filename).ToLower();
            // Either gets content type or plain/text
            string contentType = ContentType.ContainsKey(ext) ? ContentType[ext] : ContentType.Values.FirstOrDefault();
            // Uploads a file with created content type
            return await UploadFileAsync(filename, filedata, contentType);
        }
        /// <summary>
        /// Uploads an image or a file to Guilded.
        /// </summary>
        /// <param name="url">Link to the image</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>File URL</returns>
        public async Task<Uri> UploadFileAsync(Uri url)
        {
            if (url is null) throw new ArgumentException($"{nameof(url)} can not be null.");
            // Create new request
            IRestRequest req = new RestRequest($"{GuildedUrl.Media}/media/upload", Method.POST);
            // Adds a new JSON
            /* {
             *   "mediaInfo": { "src": "url" },
             *   "dynamicMediaTypeId": "ContentMedia",
             *   "uploadTrackingId": "r-1000000-1000000"
             * }
             */
            req.AddJsonBody($"{{ \"mediaInfo\": {{ \"src\": \"{url}\" }}, \"dynamicMediaTypeId\": \"ContentMedia\", \"uploadTrackingId\": \"{FormId.Random}\" }}");
            // Sends that request and gets response
            string content = (await SendRequest<object>(req)).Response.Content;
            // Gets object with url from the response
            JObject obj = JObject.Parse(content);
            // Checks if obj contains the property
            if (!obj.ContainsKey("url")) return null;
            // Returns the url property
            return obj["url"].ToObject<Uri>();
        }
        /// <summary>
        /// Executes a request and receives response or an error.
        /// </summary>
        /// <param name="request">The request to send to execute</param>
        /// <typeparam name="T">Type of the response to get</typeparam>
        /// <returns>Request response</returns>
        private async Task<ExecutionResponse<T>> SendRequest<T>(IRestRequest request)
        {
            ApiLogger.Debug("Sending a request [{@Method}] {@Path}", request.Method, request.Resource);
            // Executes given request and gets response
            IRestResponse<T> response = await Rest.ExecuteAsync<T>(request);
            // Check if content isn't null 
            if (string.IsNullOrEmpty(response.Content)) return new ExecutionResponse<T>(response);
            // Checks if the request was successful
            ApiLogger.Verbose("Request was successful: {@Success}", response.IsSuccessful);
            if (!response.IsSuccessful)
            {
                // Parses it
                JToken token = JToken.Parse(response.Content);
                ApiLogger.Error("Error while executing a request: {@StatusCode}", response.StatusCode);
                ApiLogger.Debug("Error caused by [{@Method}] {@Resource}", request.Method, request.Resource);
                // Checks if it's an object
                if (token.Type == JTokenType.Object)
                {
                    // Gets the object
                    JObject obj = (JObject)token;
                    // If it does, treat it as an error
                    GuildedException exc = new GuildedException()
                    {
                        Code = obj["code"].Value<string>(),
                        ErrorMessage = obj["message"].Value<string>(),
                        StatusCode = response.StatusCode
                    };
                    // Throws it
                    throw exc;
                }
                else throw new GuildedException()
                {
                    Code = "Unknown",
                    ErrorMessage = response.Content,
                    StatusCode = response.StatusCode
                };
            }
            // Returns it
            return new ExecutionResponse<T>(response);
        }
    }
}
