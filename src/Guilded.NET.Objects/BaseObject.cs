using Newtonsoft.Json;
using System;
using System.Linq;

namespace Guilded.NET.Objects {
    /// <summary>
    /// Base object for all JSON-based Guilded objects.
    /// </summary>
    /// <typeparam name="T">Child type - Type which is inheriting from this type</typeparam>
    public abstract class BaseObject {
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
        public string Serialize(params JsonConverter[] converters) =>
            JsonConvert.SerializeObject(this, converters);
    }
}
