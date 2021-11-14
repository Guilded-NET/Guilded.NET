using System.IO;
using Newtonsoft.Json;

namespace Guilded.NET.Base
{
    /// <summary>
    /// The base for all Guilded models.
    /// </summary>
    /// <remarks>
    /// <para>Provides a base for all Guilded.NET objects. This object can be serialized with <see cref="Serialize(JsonSerializer)"/> or <see cref="Serialize(JsonConverter[])"/> methods.</para>
    /// </remarks>
    /// <seealso cref="ClientObject"/>
    public abstract class BaseObject
    {
        /// <summary>
        /// Returns serialized <see cref="BaseObject"/> instance.
        /// </summary>
        /// <remarks>
        /// <para>Returns serialized version of this <see cref="BaseObject"/> instance based on <paramref name="serializer"/>.</para>
        /// </remarks>
        /// <param name="serializer">The serializer that will serialize</param>
        /// <returns>Serialized <see cref="BaseObject"/> instance</returns>
        public string Serialize(JsonSerializer serializer)
        {
            using StringWriter strWriter = new();
            using JsonWriter writer = new JsonTextWriter(strWriter);

            serializer.Serialize(writer, this);
            return strWriter.ToString();
        }
        /// <summary>
        /// Returns serialized <see cref="BaseObject"/> instance.
        /// </summary>
        /// <remarks>
        /// <para>Returns serialized version of this <see cref="BaseObject"/> instance based on <paramref name="converters"/>.</para>
        /// </remarks>
        /// <param name="converters">Guilded object converters that will be used to serialize</param>
        /// <returns>Serialized <see cref="BaseObject"/> instance</returns>
        public virtual string Serialize(params JsonConverter[] converters) =>
            JsonConvert.SerializeObject(this, converters);
    }
}
