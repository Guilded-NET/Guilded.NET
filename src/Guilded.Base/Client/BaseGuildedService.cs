using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Guilded.Base.Servers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace Guilded.Base.Client;

/// <summary>
/// Represents the base for any kinds of <see cref="BaseGuildedClient">Guilded clients</see>.
/// </summary>
public abstract class BaseGuildedService
{
    #region Static fields
    private static readonly Dictionary<string, string> _contentType = new()
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
    #endregion

    #region Field
    private readonly Subject<RestResponse> _onResponseReceived = new();

    private DateTime? _globalRateLimit;

    private readonly Dictionary<Guid, DateTime> _channelCooldowns = new();
    #endregion

    #region Properties
    /// <summary>
    /// Gets the REST client for Guilded.
    /// </summary>
    /// <value>Rest client</value>
    /// <seealso cref="Websocket" />
    protected internal RestClient Rest { get; set; }

    /// <summary>
    /// Gets the response received from a request that was made with <see cref="Rest">Guilded's REST client</see>.
    /// </summary>
    public IObservable<RestResponse> ResponseReceived => _onResponseReceived.AsObservable();

    /// <summary>
    /// Settings for <see cref="Rest" /> client's JSON (de)serialization.
    /// </summary>
    /// <remarks>
    /// <para>JSON settings that are used in <see cref="GuildedSerializer" /> and <see cref="Rest" />.</para>
    /// </remarks>
    /// <value>Serializer Settings</value>
    /// <seealso cref="Rest" />
    /// <seealso cref="GuildedSerializer" />
    public JsonSerializerSettings SerializerSettings { get; set; }

    /// <summary>
    /// A serializer to (de)serialize for JSON from Guilded API.
    /// </summary>
    /// <value>Serializer from <see cref="SerializerSettings" /></value>
    public JsonSerializer GuildedSerializer { get; set; }

    /// <summary>
    /// Headers that will be used for REST and WebSocket clients.
    /// </summary>
    /// <value>Dictionary of headers</value>
    protected Dictionary<string, string> AdditionalHeaders { get; set; } = new();
    #endregion

    #region Constructors
    /// <summary>
    /// Creates default settings for <see cref="BaseGuildedService" />'s child types.
    /// </summary>
    /// <remarks>
    /// <para>Initializes client serialization and REST.</para>
    /// </remarks>
    /// <param name="apiUrl">The URL to any kind of Guilded API</param>
    protected BaseGuildedService(Uri apiUrl)
    {
        // For REST client
        SerializerSettings = new JsonSerializerSettings
        {
            // For CientObject to receive this client
            Context = new StreamingContext(StreamingContextStates.Persistence, this),
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy { OverrideSpecifiedNames = false }
            }
        };
        GuildedSerializer = JsonSerializer.Create(SerializerSettings);

        Rest = new RestClient(apiUrl ?? throw new ArgumentNullException(nameof(apiUrl)))
            .AddDefaultHeader("Origin", "https://www.guilded.gg/")
            // Guilded.NET, Guilded.NET's version, Common Language Runtime version
            .AddDefaultHeader("User-Agent", $"Guilded-NET GNET-{typeof(BaseGuildedClient).Assembly.GetName().Version} CLR-{Environment.Version}")
            .UseNewtonsoftJson(SerializerSettings);
    }
    #endregion

    #region Methods

    #region Methods Serialization
    /// <summary>
    /// Serializes object with client's Guilded serializer.
    /// </summary>
    /// <remarks>
    /// <para>Serializes given object to JSON using <see cref="GuildedSerializer" />. Use this if you want to send REST request or WebSocket message.</para>
    /// </remarks>
    /// <param name="obj">The parameter to serialize</param>
    /// <returns>Serialized object</returns>
    /// <seealso cref="Deserialize" />
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
    /// <para>Deserializes given JSON file/text using <see cref="GuildedSerializer" />. Use this if you want to deserialize Guilded response or WebSocket message.</para>
    /// </remarks>
    /// <param name="json">Raw JSON to deserialize</param>
    /// <typeparam name="T">The type of deserialized instance</typeparam>
    /// <returns>Deserialized object</returns>
    /// <seealso cref="Serialize" />
    public T? Deserialize<T>(string json)
    {
        using StringReader strReader = new(json);
        using JsonReader reader = new JsonTextReader(strReader);

        return GuildedSerializer.Deserialize<T>(reader);
    }
    #endregion

    #region Methods Requests
    /// <summary>
    /// Uploads a file to Guilded.
    /// </summary>
    /// <remarks>
    /// <para>Uploads any image, text or document file to Guilded with content type as <paramref name="contentType" />.</para>
    /// <para>The new image uploaded to Guilded will be received as <see cref="Uri" /> return value.</para>
    /// </remarks>
    /// <param name="filename">The name of the file being uploaded</param>
    /// <param name="filedata">The contents of the file being uploaded</param>
    /// <param name="contentType">Content type for multipart form data</param>
    /// <exception cref="ArgumentException">When <paramref name="filename" /> is empty or <see langword="null" /></exception>
    /// <exception cref="GuildedException" />
    /// <returns>File URL</returns>
    public async Task<Uri?> UploadFileAsync(string filename, byte[] filedata, string contentType)
    {
        if (string.IsNullOrWhiteSpace(filename))
            throw new ArgumentException($"{nameof(filename)} can not be empty.");

        RestRequest req = new RestRequest(GuildedUrl.MediaFileUpload, Method.Post)
            .AddFile("file", filedata, filename, contentType)
            //"multipart/form-data",
            .AddParameter("uploadTrackingId", FormId.Random, ParameterType.GetOrPost)
            .AddParameter("Content-Type", "multipart/form-data");

        RestResponse<JObject> response = await ExecuteRequestAsync<JObject>(req).ConfigureAwait(false);

        _onResponseReceived.OnNext(response);

        JObject obj = response.Data!;

        return obj.ContainsKey("url") ? obj.Value<Uri>("url") : null;
    }

    /// <summary>
    /// Uploads a file to Guilded.
    /// </summary>
    /// <remarks>
    /// <para>Uploads any image, text or document file to Guilded with content type automatically assigned.</para>
    /// <para>The new image uploaded to Guilded will be received as <see cref="Uri" /> return value.</para>
    /// </remarks>
    /// <param name="filename">The name of the file being uploaded</param>
    /// <param name="filedata">The contents of the file being uploaded</param>
    /// <exception cref="ArgumentNullException">When <paramref name="filename" /> is empty or <see langword="null" /></exception>
    /// <exception cref="GuildedException" />
    /// <returns>File URL</returns>
    public Task<Uri?> UploadFileAsync(string filename, byte[] filedata)
    {
        if (string.IsNullOrWhiteSpace(filename))
            throw new ArgumentNullException(nameof(filename));

        string ext = Path.GetExtension(filename).ToLower();
        // Automatically get content type instead of manually typing it
        string contentType = _contentType.ContainsKey(ext) ? _contentType[ext] : _contentType.Values.First();

        return UploadFileAsync(filename, filedata, contentType);
    }

    /// <summary>
    /// Uploads a file to Guilded.
    /// </summary>
    /// <remarks>
    /// <para>Uploads an image from link <paramref name="url" /> to Guilded.</para>
    /// <para>The new image uploaded to Guilded will be received as <see cref="Uri" /> return value.</para>
    /// </remarks>
    /// <param name="url">URL link to an image to upload</param>
    /// <exception cref="GuildedException" />
    /// <returns>File URL</returns>
    public async Task<Uri?> UploadFileAsync(Uri url)
    {
        if (url is null)
            throw new ArgumentException($"{nameof(url)} can not be null.");

        RestRequest req = new(GuildedUrl.MediaUrlUpload, Method.Post);
        /* {
         *   "mediaInfo": { "src": "url" },
         *   "dynamicMediaTypeId": "ContentMedia",
         *   "uploadTrackingId": "r-1000000-1000000"
         * }
         */
        req.AddJsonBody($"{{ \"mediaInfo\": {{ \"src\": \"{url}\" }}, \"dynamicMediaTypeId\": \"ContentMedia\", \"uploadTrackingId\": \"{FormId.Random}\" }}");

        JObject obj = (await ExecuteRequestAsync<JObject>(req).ConfigureAwait(false)).Data!;

        return obj.ContainsKey("url") ? obj.Value<Uri>("url") : null;
    }

    /// <summary>
    /// Executes a request and receives a response or an error.
    /// </summary>
    /// <param name="request">The request to send to execute</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException">When <paramref name="request" />'s URL refers to an invalid endpoint</exception>
    /// <returns>Guilded request response</returns>
    protected Task<RestResponse<object>> ExecuteRequestAsync(RestRequest request) =>
        ExecuteRequestAsync<object>(request);

    /// <summary>
    /// Executes a request and receives response or an error.
    /// </summary>
    /// <remarks>
    /// <para>Does additional checks on channel slowmode cooldowns.</para>
    /// </remarks>
    /// <param name="request">The request to send to execute</param>
    /// <param name="channel"><see cref="ServerChannel">The channel</see> where the <paramref name="request" /> is being executed</param>
    /// <returns>Guilded API's request's response</returns>
    protected Task<RestResponse<T>> ExecuteRequestAsync<T>(RestRequest request, Guid channel)
    {
        DateTime? cooldown = _channelCooldowns.GetValueOrDefault(channel);

        // Could just do `_channelCooldowns.Remove(channel);` everytime, but why?
        // Would rather have this mess
        if (cooldown is not null)
        {
            if (DateTime.Now < cooldown) throw new GuildedTooManyRequestsException($"The slowmode cooldown in channel by id '{channel}' has not been exhausted yet", (DateTime)cooldown - DateTime.Now, true);
            else _channelCooldowns.Remove(channel);
        }

        return InternalExecuteRequestAsync<T>(request, (code, errorMessage, response, afterSeconds) =>
        {
            bool isSlowmode = response.Headers!.Any(x => x.Name == "x-slowmode-cooldown");

            DateTime elapsesOn = DateTime.Now.AddSeconds(afterSeconds);

            if (isSlowmode) _channelCooldowns.Add(channel, elapsesOn);
            else _globalRateLimit = elapsesOn;

            throw new GuildedTooManyRequestsException(code, errorMessage, response, TimeSpan.FromSeconds(afterSeconds), isSlowmode);
        });
    }

    /// <summary>
    /// Executes a request and receives response or an error.
    /// </summary>
    /// <param name="request">The request to send to execute</param>
    /// <typeparam name="T">Type of the response to get</typeparam>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException">When <paramref name="request" />'s URL refers to an invalid endpoint</exception>
    /// <returns>Guilded API's request's response</returns>
    protected Task<RestResponse<T>> ExecuteRequestAsync<T>(RestRequest request) =>
        InternalExecuteRequestAsync<T>(request, (code, errorMessage, response, afterSeconds) =>
        {
            _globalRateLimit = DateTime.Now.AddSeconds(afterSeconds);

            throw new GuildedTooManyRequestsException(code, errorMessage, response, TimeSpan.FromSeconds(afterSeconds), false);
        });

    private async Task<RestResponse<T>> InternalExecuteRequestAsync<T>(RestRequest request, Action<string, string, RestResponse<T>, double> onRateLimit)
    {
        // Rate-limiting
        if (_globalRateLimit is not null && DateTime.Now < _globalRateLimit)
            throw new GuildedTooManyRequestsException("The global cooldown has not been exhausted yet", (DateTime)_globalRateLimit - DateTime.Now, false);

        _globalRateLimit = null;

        RestResponse<T> response = await Rest.ExecuteAsync<T>(request).ConfigureAwait(false);

        // For logging
        _onResponseReceived.OnNext(response);

        if (!response.IsSuccessful)
        {
            JToken token = JToken.Parse(response.Content!);
            JObject obj = (JObject)token;

            // For giving Guilded exception more information
            string code = obj.Value<string>("code")!,
                    errorMessage = obj.Value<string>("message")!;

            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                HeaderParameter? retryAfterHeader = response.Headers!.FirstOrDefault(x => x.Name == "Retry-After");
                double afterSeconds = retryAfterHeader?.Value is null ? 5 : double.Parse((string)retryAfterHeader.Value!);

                onRateLimit(code, errorMessage, response, afterSeconds);
            }

            JArray? missingPerms = obj.Value<JObject>("meta")?.Value<JArray>("missingPermissions");

            GuildedException exc =
                response.StatusCode switch
                {
                    // Missing permissions from the bot
                    HttpStatusCode.Forbidden =>
                        new GuildedPermissionException(code, missingPerms is not null ? errorMessage + ". Missing permissions: " + string.Join(", ", missingPerms) : errorMessage, response),
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

    #endregion
}