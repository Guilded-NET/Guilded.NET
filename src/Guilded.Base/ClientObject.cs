using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Guilded.Base
{
    /// <summary>
    /// A base that with a parent client.
    /// </summary>
    /// <remarks>
    /// <para>A base object that also contains definining parent client. The parent client that defined this method is referenced in <see cref="ParentClient"/>, that is initiated with internal OnDeserialized method. This allows having methods like <see cref="Content.Message.CreateMessageAsync(string)"/>, where it requires to call the parent client's methods.</para>
    /// </remarks>
    /// <seealso cref="BaseGuildedClient"/>
    /// <seealso cref="BaseObject"/>
    public abstract class ClientObject : BaseObject
    {
#nullable disable
        /// <summary>
        /// The parent client that adopts this object.
        /// </summary>
        /// <remarks>
        /// <para>The parent client that deserialized this object. This is initiated via internal OnDeserialized method and only available after deserialization, but not during it.</para>
        /// </remarks>
        /// <value>Client</value>
        [JsonIgnore]
        public BaseGuildedClient ParentClient { get; private set; }
        /// <summary>
        /// Adds a parent client if the context contains it.
        /// </summary>
        /// <param name="context">The context given by the serializer</param>
        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            if (context.Context is BaseGuildedClient client)
                ParentClient = client;
        }
#nullable restore
    }
}