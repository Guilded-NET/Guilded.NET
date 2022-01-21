using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Guilded.NET.Base
{
    public abstract partial class BaseGuildedClient : IDisposable
    {
        private static readonly Dictionary<string, string> contentType = new()
        {
            // Text/Document
            { "txt", MediaTypeNames.Text.Plain },
            { "rtf", MediaTypeNames.Text.RichText },
            { "html", MediaTypeNames.Text.Html },
            { "xml", MediaTypeNames.Text.Xml },
            // Images
            { "png", "image/png" },
            { "svg", "image/svg+xml" },
            { "jpeg", MediaTypeNames.Image.Jpeg },
            { "jpg", MediaTypeNames.Image.Jpeg },
            { "webp", "image/webp" },
            { "tiff", MediaTypeNames.Image.Tiff }
        };

        #region Properties
        /// <summary>
        /// The REST client for Guilded.
        /// </summary>
        /// <remarks>
        /// <para>The REST client that is used to send requests to Guilded API.</para>
        /// </remarks>
        /// <value>Rest client</value>
        /// <seealso cref="Websocket"/>
        protected internal IRestClient Rest { get; set; }
        #endregion

        #region Serialization
        /// <summary>
        /// Serializes object with client's Guilded serializer.
        /// </summary>
        /// <remarks>
        /// <para>Serializes given object to JSON using <see cref="GuildedSerializer"/>. Use this if you want to send REST request or WebSocket message.</para>
        /// </remarks>
        /// <param name="obj">The parameter to serialize</param>
        /// <returns>Serialized object</returns>
        /// <seealso cref="Deserialize"/>
        public string Serialize(object obj)
        {
            using StringWriter strWriter = new();
            using JsonWriter writer = new JsonTextWriter(strWriter);

            GuildedSerializer!.Serialize(writer, obj);
            return strWriter.ToString();
        }
        /// <summary>
        /// Deserializes JSON with client's Guilded serializer.
        /// </summary>
        /// <remarks>
        /// <para>Deserializes given JSON file/text using <see cref="GuildedSerializer"/>. Use this if you want to deserialize Guilded response or WebSocket message.</para>
        /// </remarks>
        /// <param name="json">Raw JSON to deserialize</param>
        /// <typeparam name="T">The type of deserialized instance</typeparam>
        /// <returns>Deserialized object</returns>
        /// <seealso cref="Serialize"/>
        public T? Deserialize<T>(string json)
        {
            using StringReader strReader = new(json);
            using JsonReader reader = new JsonTextReader(strReader);

            return GuildedSerializer!.Deserialize<T>(reader);
        }
        #endregion

        #region Requests
        /// <summary>
        /// Uploads a file to Guilded.
        /// </summary>
        /// <remarks>
        /// <para>Uploads any image, text or document file to Guilded with content type as <paramref name="contentType"/>.</para>
        /// <para>The new image uploaded to Guilded will be received as <see cref="Uri"/> return value.</para>
        /// </remarks>
        /// <param name="filename">The name of the file being uploaded</param>
        /// <param name="filedata">The contents of the file being uploaded</param>
        /// <param name="contentType">Content type for multipart form data</param>
        /// <exception cref="ArgumentException">When <paramref name="filename"/> is empty or <see langword="null"/></exception>
        /// <exception cref="GuildedException"/>
        /// <returns>File URL</returns>
        public async Task<Uri?> UploadFileAsync(string filename, byte[] filedata, string contentType)
        {
            if (string.IsNullOrWhiteSpace(filename))
                throw new ArgumentException($"{nameof(filename)} can not be empty.");

            IRestRequest req = new RestRequest(GuildedUrl.MediaFileUpload, Method.POST, DataFormat.Json)
                .AddFile("file", filedata, filename, contentType)
                // Make sure Guilded is not angry
                .AddParameter("uploadTrackingId", FormId.Random.ToString(), "multipart/form-data", ParameterType.GetOrPost)
                .AddParameter("Content-Type", "multipart/form-data");

            JObject obj = (await ExecuteRequestAsync<JObject>(req).ConfigureAwait(false)).Data;

            return obj.ContainsKey("url") ? obj.Value<Uri>("url") : null;
        }
        /// <summary>
        /// Uploads a file to Guilded.
        /// </summary>
        /// <remarks>
        /// <para>Uploads any image, text or document file to Guilded with content type automatically assigned.</para>
        /// <para>The new image uploaded to Guilded will be received as <see cref="Uri"/> return value.</para>
        /// </remarks>
        /// <param name="filename">The name of the file being uploaded</param>
        /// <param name="filedata">The contents of the file being uploaded</param>
        /// <exception cref="ArgumentNullException">When <paramref name="filename"/> is empty or <see langword="null"/></exception>
        /// <exception cref="GuildedException"/>
        /// <returns>File URL</returns>
        public async Task<Uri?> UploadFileAsync(string filename, byte[] filedata)
        {
            if (string.IsNullOrWhiteSpace(filename))
                throw new ArgumentNullException(nameof(filename));

            string ext = Path.GetExtension(filename).ToLower();
            // Automatically get content type instead of manually typing it
            string contentType = BaseGuildedClient.contentType.ContainsKey(ext) ? BaseGuildedClient.contentType[ext] : BaseGuildedClient.contentType.Values.First();

            return await UploadFileAsync(filename, filedata, contentType).ConfigureAwait(false);
        }
        /// <summary>
        /// Uploads a file to Guilded.
        /// </summary>
        /// <remarks>
        /// <para>Uploads an image from link <paramref name="url"/> to Guilded.</para>
        /// <para>The new image uploaded to Guilded will be received as <see cref="Uri"/> return value.</para>
        /// </remarks>
        /// <param name="url">URL link to an image to upload</param>
        /// <exception cref="GuildedException"/>
        /// <returns>File URL</returns>
        public async Task<Uri?> UploadFileAsync(Uri url)
        {
            if (url is null)
                throw new ArgumentException($"{nameof(url)} can not be null.");

            IRestRequest req = new RestRequest(GuildedUrl.MediaUrlUpload, Method.POST);
            /* {
             *   "mediaInfo": { "src": "url" },
             *   "dynamicMediaTypeId": "ContentMedia",
             *   "uploadTrackingId": "r-1000000-1000000"
             * }
             */
            req.AddJsonBody($"{{ \"mediaInfo\": {{ \"src\": \"{url}\" }}, \"dynamicMediaTypeId\": \"ContentMedia\", \"uploadTrackingId\": \"{FormId.Random}\" }}");

            JObject obj = (await ExecuteRequestAsync<JObject>(req).ConfigureAwait(false)).Data;

            return obj.ContainsKey("url") ? obj.Value<Uri>("url") : null;
        }
        /// <summary>
        /// Executes a request and receives a response or an error.
        /// </summary>
        /// <param name="request">The request to send to execute</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException">When <paramref name="request"/>'s URL refers to an invalid endpoint</exception>
        /// <returns>Guilded request response</returns>
        protected async Task<IRestResponse<object>> ExecuteRequestAsync(IRestRequest request) =>
            await ExecuteRequestAsync<object>(request).ConfigureAwait(false);
        /// <summary>
        /// Executes a request and receives ra esponse or an error.
        /// </summary>
        /// <param name="request">The request to send to execute</param>
        /// <typeparam name="T">Type of the response to get</typeparam>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException">When <paramref name="request"/>'s URL refers to an invalid endpoint</exception>
        /// <returns>Guilded request response</returns>
        protected async Task<IRestResponse<T>> ExecuteRequestAsync<T>(IRestRequest request)
        {
            IRestResponse<T> response = await Rest.ExecuteAsync<T>(request).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                JToken token = JToken.Parse(response.Content);
                JObject obj = (JObject)token;

                // For giving Guilded exception more information
                string code = obj.Value<string>("code")!,
                       errorMessage = obj.Value<string>("message")!;

                GuildedException exc =
                    response.StatusCode switch
                    {
                        // Missing permissions from the bot
                        HttpStatusCode.Forbidden =>
                            new GuildedPermissionException(code, errorMessage, response),
                        // Given path has not been found
                        HttpStatusCode.NotFound =>
                            new GuildedResourceException(code, errorMessage, response),
                        // Bad token
                        HttpStatusCode.Unauthorized =>
                            new GuildedAuthorizationException(code, errorMessage, response),
                        // Bad request parameters/something else related to the request
                        HttpStatusCode.BadRequest =>
                            new GuildedRequestException(code, errorMessage, response),
                        // Any other error, such as internal server error
                        _ =>
                            new GuildedException(code, errorMessage, response)
                    };
                throw exc;
            }
            return response;
        }
        #endregion
    }
}
