using System.Linq;

using Newtonsoft.Json;

namespace Guilded.NET.Base
{
    /// <summary>
    /// The base for all Guilded models.
    /// </summary>
    /// <seealso cref="ClientObject"/>
    public abstract class BaseObject
    {
        /// <summary>
        /// Returns serialized version of this object with the given <paramref name="serializer"/>.
        /// </summary>
        /// <param name="serializer">Serializer to serialize object with</param>
        /// <returns>Serialized object</returns>
        public string Serialize(JsonSerializer serializer) => Serialize(serializer.Converters.ToArray());
        /// <summary>
        /// Returns serialized version of this object with the given <paramref name="converters"/>.
        /// </summary>
        /// <param name="converters">Converters to serialize object with</param>
        /// <returns>Serialized object</returns>
        public virtual string Serialize(params JsonConverter[] converters) =>
            JsonConvert.SerializeObject(this, converters);
    }
}
