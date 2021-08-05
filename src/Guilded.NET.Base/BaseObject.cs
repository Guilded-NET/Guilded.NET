using System.Linq;

using Newtonsoft.Json;

namespace Guilded.NET.Base
{
    /// <summary>
    /// Base object for all JSON-based Guilded objects.
    /// </summary>
    public abstract class BaseObject
    {
        /// <summary>
        /// Serializes this object.
        /// </summary>
        /// <param name="serializer">Serializer to serialize object with</param>
        /// <returns>Serialized object</returns>
        public string Serialize(JsonSerializer serializer) => Serialize(serializer.Converters.ToArray());
        /// <summary>
        /// Serializes this object.
        /// </summary>
        /// <param name="converters">Converters to serialize object with</param>
        /// <returns>Serialized object</returns>
        public virtual string Serialize(params JsonConverter[] converters) =>
            JsonConvert.SerializeObject(this, converters);
    }
}
