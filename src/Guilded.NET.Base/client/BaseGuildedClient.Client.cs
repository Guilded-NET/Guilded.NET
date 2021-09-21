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
    /// An API wrapping layer for all Guilded client.
    /// </summary>
    /// <remarks>
    /// <para>A base that adds a layer to Guilded API wrapping.</para>
    /// <para>This is a base for all Guilded clients.</para>
    /// </remarks>
    public abstract partial class BaseGuildedClient : IDisposable
    {
        /// <summary>
        /// An event when client is connected
        /// </summary>
        /// <remarks>
        /// <para>An event that occurs once Guilded client connects to Guilded.</para>
        /// <para>This usually occurs once <see cref="ConnectAsync"/> is called and no errors get thrown.</para>
        /// </remarks>
        /// <seealso cref="ConnectAsync"/>
        protected EventHandler ConnectedEvent;
        /// <summary>
        /// An event when client gets disconnected
        /// </summary>
        /// <remarks>
        /// <para>An event that occurs once Guilded client disconnects from Guilded.</para>
        /// <para>This usually occurs once <see cref="DisconnectAsync"/> is called and no errors get thrown.</para>
        /// </remarks>
        /// <seealso cref="ConnectAsync"/>
        protected EventHandler DisconnectedEvent;
        /// <inheritdoc cref="ConnectedEvent"/>
        public event EventHandler Connected
        {
            add => ConnectedEvent += value;
            remove => ConnectedEvent -= value;
        }
        /// <inheritdoc cref="DisconnectedEvent"/>
        public event EventHandler Disconnected
        {
            add => DisconnectedEvent += value;
            remove => DisconnectedEvent += value;
        }
        /// <summary>
        /// Settings for <see cref="Rest"/> client's JSON (de)serialization.
        /// </summary>
        /// <remarks>
        /// <para>JSON settings that are used in:</para>
        /// <list>
        ///     <item>
        ///         <description><see cref="GuildedSerializer"/></description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="Rest"/></description>
        ///     </item>
        /// </list>
        /// </remarks>
        /// <value>Serializer Settings</value>
        /// <seealso cref="Rest"/>
        /// <seealso cref="GuildedSerializer"/>
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
        /// Creates default settings for <see cref="BaseGuildedClient"/>'s child types.
        /// </summary>
        /// <remarks>
        /// <para>Inititates REST client and serializer settings.</para>
        /// </remarks>
        /// <param name="apiUrl">URL of Guilded API</param>
        /// <exception cref="ArgumentNullException">When <paramref name="apiUrl"/> is <see langword="null"/></exception>
        protected BaseGuildedClient(Uri apiUrl)
        {
            if(apiUrl == null)
                throw new ArgumentNullException($"Expected {nameof(apiUrl)} to have a value");
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
        /// Creates default settings for <see cref="BaseGuildedClient"/>'s child types with <see cref="GuildedUrl.Api"/> as URL.
        /// </summary>
        /// <remarks>
        /// <para>Inititates REST client and serializer settings.</para>
        /// <para>Relies on <see cref="BaseGuildedClient(Uri)"/> with <see cref="GuildedUrl.Api"/> as API URL.</para>
        /// </remarks>
        protected BaseGuildedClient() : this(GuildedUrl.Api) { }
        /// <summary>
        /// Connects this client to Guilded.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new connection to Guilded with this client.</para>
        /// <blockquote class="note">See documentation of child types for more information.</blockquote>
        /// </remarks>
        /// <seealso cref="DisconnectAsync"/>
        public abstract Task ConnectAsync();
        /// <summary>
        /// Disconnects this client from Guilded.
        /// </summary>
        /// <remarks>
        /// <para>Stops any connections this client has with Guilded.</para>
        /// <blockquote class="note">See documentation of child types for more information.</blockquote>
        /// </remarks>
        /// <seealso cref="ConnectAsync"/>
        /// <seealso cref="Dispose"/>
        public abstract Task DisconnectAsync();
        /// <summary>
        /// Disposes <see cref="BaseGuildedClient"/> instance.
        /// </summary>
        /// <seealso cref="DisconnectAsync"/>
        public abstract void Dispose();
    }
}