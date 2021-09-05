using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace Guilded.NET.Base
{
    /// <summary>
    /// An object that has a parent client.
    /// </summary>
    /// <seealso cref="BaseGuildedClient"/>
    /// <seealso cref="BaseObject"/>
    public abstract class ClientObject : BaseObject
    {
        /// <summary>
        /// The parent client that adopts this object.
        /// </summary>
        /// <value>Client</value>
        [JsonIgnore]
        public BaseGuildedClient ParentClient
        {
            get; set;
        }
        /// <summary>
        /// Adds a parent client if context contains it.
        /// </summary>
        /// <param name="context">The context given by the serializer</param>
        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            if (context.Context is BaseGuildedClient client)
                ParentClient = client;
        }
    }
}