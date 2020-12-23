using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Objects.Converters {
    using Teams;
    /// <summary>
    /// Converts specific interface types.
    /// </summary>
    public class GInterfaceConverter: JsonConverter {
        static readonly Type channel = typeof(IChannel);
        static readonly Type teamChannel = typeof(ITeamChannel);
        // All of the allowed types
        static readonly Type[] allowed = new Type[] { channel, teamChannel };
        /// <summary>
        /// Writes specific object to the JSON.
        /// </summary>
        /// <param name="writer">JsonWriter</param>
        /// <param name="value">ID</param>
        /// <param name="serializer">Serializer</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => writer.WriteValue(JObject.FromObject(value));
        /// <summary>
        /// Converts string to specific type.
        /// </summary>
        /// <param name="reader">Reader</param>
        /// <param name="objectType">Type of the object</param>
        /// <param name="existingValue">Previous property value</param>
        /// <param name="serializer">Serializer</param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            JObject obj = JObject.Load(reader);
            // If it's a channel type
            if(objectType == channel || objectType == teamChannel) {
                // If it has a property `type`
                if(obj.ContainsKey("type")) {
                    // Gets the type
                    string type = obj["type"].Value<string>();
                    // If it's a DM channel
                    if(type == "DM") return obj.ToObject<DMChannel>(serializer);
                    // If it has threadMessageId property, then it's temporal channel
                    else if(obj.ContainsKey("threadMessageId")) return obj.ToObject<ThreadChannel>(serializer);
                    // Else, it's a normal channel
                    else return obj.ToObject<Channel>(serializer);
                // If it doesn't, then it is a category
                } else return obj.ToObject<Category>(serializer);
            // If it doesn't know the type
            } else return null;
        }
        /// <summary>
        /// Whether or not this converter can convert given type.
        /// </summary>
        /// <param name="objectType">Type of the object</param>
        /// <returns>Can convert the type</returns>
        public override bool CanConvert(Type objectType) =>
            allowed.Contains(objectType);
    }
}