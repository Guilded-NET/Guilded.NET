using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace Guilded.NET.Base
{
    /// <summary>
    /// An object that has parent client given.
    /// </summary>
    public abstract class ClientObject : BaseObject
    {
        /// <summary>
        /// Client this object was created by.
        /// </summary>
        /// <value>Client</value>
        [JsonIgnore]
        public BaseGuildedClient ParentClient
        {
            get; set;
        }
        /// <summary>
        /// Adds a parent client if given context has a Guilded client
        /// </summary>
        /// <param name="context">JsonSerializer's given context</param>
        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            // If given object from context isn't null and its type is BaseGuildedClient, set ParentClient
            if (!(context.Context is null) && context.Context is BaseGuildedClient client)
                ParentClient = client;
        }
    }
}