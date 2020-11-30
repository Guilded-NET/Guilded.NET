using System;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Converters {
    /// <summary>
    /// Converts ID to string or vice versa in a JSON.
    /// </summary>
    public class IdConverter: JsonConverter {
        static readonly Type id = typeof(GId);
        static readonly Type guid = typeof(Guid);
        /// <summary>
        /// Writes ID to the JSON string.
        /// </summary>
        /// <param name="writer">JsonWriter</param>
        /// <param name="value">ID</param>
        /// <param name="serializer">Serializer</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => writer.WriteValue(value.ToString());
        /// <summary>
        /// Converts string to ID.
        /// </summary>
        /// <param name="reader">Reader</param>
        /// <param name="objectType">Type of the object</param>
        /// <param name="existingValue">Previous property value</param>
        /// <param name="serializer">Serializer</param>
        /// <returns>GId</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            string str = (string)reader.Value;
            if(objectType == id) return GId.Parse(str);
            else if(objectType == guid) return Guid.Parse(str);
            else return reader.Value;
        }
        /// <summary>
        /// Whether or not this converter can convert given type.
        /// </summary>
        /// <param name="objectType">Type of the object</param>
        /// <returns>Can convert the type</returns>
        public override bool CanConvert(Type objectType) => objectType == id || objectType == guid;
    }
}