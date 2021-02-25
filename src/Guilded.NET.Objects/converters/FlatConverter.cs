using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Objects.Converters
{
    /// <summary>
    /// Flattens a list.
    /// </summary>
    public class FlatConverter: JsonConverter {
        static readonly Type ilist = typeof(IList<>);
        static readonly Type list = typeof(List<>);
        /// <summary>
        /// Writes list to the JSON string.
        /// </summary>
        /// <param name="writer">JsonWriter</param>
        /// <param name="value">ID</param>
        /// <param name="serializer">Serializer</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => writer.WriteValue(JArray.FromObject(new object[] {value}));
        /// <summary>
        /// Flattens the list.
        /// </summary>
        /// <param name="reader">Reader</param>
        /// <param name="objectType">Type of the object</param>
        /// <param name="existingValue">Previous property value</param>
        /// <param name="serializer">Serializer</param>
        /// <returns>Flat list</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            // Gets it as array
            JArray array = JArray.Load(reader);
            // If it has another array in it
            JToken first = array.FirstOrDefault(x => x != null);
            // If first item is not null and first item's type is array, then flatten it
            if(first != null && first?.Type == JTokenType.Array)
                // Flattens the array
                return JArray.FromObject(array.SelectMany(x => x)).ToObject(objectType, serializer);
            // Else, we don't need to flatten it
            else return array.ToObject(objectType, serializer);
        }
        /// <summary>
        /// Whether or not this converter can convert given type.
        /// </summary>
        /// <param name="objectType">Type of the object</param>
        /// <returns>Can convert the type</returns>
        public override bool CanConvert(Type objectType) =>
            objectType.FullName == list.FullName || objectType.FullName == ilist.FullName;
    }
}