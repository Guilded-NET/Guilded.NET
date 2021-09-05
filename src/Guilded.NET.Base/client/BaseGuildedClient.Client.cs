using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace Guilded.NET.Base
{
    /// <summary>
    /// A base for Guilded client.
    /// </summary>
    /// <remarks>
    /// A base type for all Guilded.NET client containing WebSocket and REST things, as well as abstract methods to be overriden.
    /// </remarks>
    public abstract partial class BaseGuildedClient : IDisposable
    {
        /// <summary>
        /// Events when client gets Connected/Disconnected.
        /// </summary>
        protected EventHandler ConnectedEvent, DisconnectedEvent;
        /// <summary>
        /// Event when client connects to the Guilded.
        /// </summary>
        public event EventHandler Connected
        {
            add => ConnectedEvent += value;
            remove => ConnectedEvent -= value;
        }
        /// <summary>
        /// Event when client disconnects from Guilded.
        /// </summary>
        public event EventHandler Disconnected
        {
            add => DisconnectedEvent += value;
            remove => DisconnectedEvent += value;
        }
        /// <summary>
        /// ID of the client for websocket and REST stuff.
        /// </summary>
        /// <value>Guilded client ID</value>
        public Guid ClientId
        {
            get; set;
        }
        /// <summary>
        /// Settings for <see cref="Rest"/> client's JSON (de)serialization.
        /// </summary>
        /// <value>Serializer Settings</value>
        public JsonSerializerSettings SerializerSettings
        {
            get; set;
        }
        /// <summary>
        /// A serializer to (de)serialize for JSON from Guilded API.
        /// </summary>
        /// <value>Serializer from <see cref="SerializerSettings"/></value>
        public JsonSerializer GuildedSerializer
        {
            get; set;
        }
        /// <summary>
        /// Headers that will be used for REST and WebSocket clients.
        /// </summary>
        /// <value>Dictionary of headers</value>
        protected Dictionary<string, string> AdditionalHeaders
        {
            get; set;
        } = new Dictionary<string, string>();
        /// <summary>
        /// A base for Guilded client.
        /// </summary>
        /// <param name="apiUrl">URL of Guilded API</param>
        /// <exception cref="ArgumentNullException">When apiurl or socketurl are null</exception>
        /// <exception cref="UriFormatException">When apiurl or socketurl are invalid</exception>
        protected BaseGuildedClient(Uri apiUrl)
        {
            // Sets serialization settings for REST client
            SerializerSettings = new JsonSerializerSettings
            {
                // For CientObject to receive this client
                Context = new StreamingContext(StreamingContextStates.Persistence, this),
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy { OverrideSpecifiedNames = false }
                }
            };

            Rest = new RestClient(apiUrl ?? throw new ArgumentNullException($"{nameof(apiUrl)} can not be empty."))
                .AddDefaultHeader("Origin", "https://www.guilded.gg/")
                .UseNewtonsoftJson(SerializerSettings);
        }
        /// <summary>
        /// A base for Guilded client.
        /// </summary>
        /// <exception cref="ArgumentNullException">When apiurl or socketurl are null</exception>
        /// <exception cref="UriFormatException">When apiurl or socketurl are invalid</exception>
        protected BaseGuildedClient() : this(GuildedUrl.Api) { }

        /// <summary>
        /// Sets cookies that were fetched from login.
        /// </summary>
        /// <param name="container">The cookie container that will be used in the requests</param>
        /// <returns><paramref name="container"/></returns>
        protected CookieContainer SetCookies(CookieContainer container)
        {
            // Guilded cookies for user clients
            GuildedCookies = container;

            if (!(Rest is null))
                Rest.CookieContainer = container;
            return container;
        }
        /// <summary>
        /// Connects this client to Guilded.
        /// </summary>
        /// <remarks>
        /// Creates a new connection to Guilded with this client.
        /// </remarks>
        public abstract Task ConnectAsync();
        /// <summary>
        /// Disconnects this client from Guilded.
        /// </summary>
        /// <remarks>
        /// Stop any connections this client has with Guilded.
        /// </remarks>
        public abstract Task DisconnectAsync();
        /// <summary>
        /// Disposes <see cref="BaseGuildedClient"/> instance.
        /// </summary>
        public abstract void Dispose();
    }
}