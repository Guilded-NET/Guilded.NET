using System;
using System.Linq;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Base
{
    /// <summary>
    /// Converts lists with 1 item to that item and item to list with 1 item.
    /// </summary>
    public class FlatListConverter : JsonConverter
    {
        /// <summary>
        /// Writes specific object to the JSON.
        /// </summary>
        /// <param name="writer">JsonWriter</param>
        /// <param name="value">A list or an array</param>
        /// <param name="serializer">Serializer</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            // Converts object to the list
            IList<object> objList = (IList<object>)value;
            // If there is 1 or no items, write the first value or default
            if(objList.Count < 2) writer.WriteValue(objList.FirstOrDefault());
            else {
                // Starts the array
                writer.WriteStartArray();
                // Writes all values
                foreach(var item in objList) writer.WriteValue(item);
                // Ends the array
                writer.WriteEndArray();
            }
        }
        /// <summary>
        /// Converts json to an object of a specific type.
        /// </summary>
        /// <param name="reader">Reader</param>
        /// <param name="objectType">Type of the object</param>
        /// <param name="existingValue">Previous property value</param>
        /// <param name="serializer">Serializer</param>
        /// <returns>Read object</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Gets it as a token
            JToken token = JToken.ReadFrom(reader);
            // If it's an array, then convert it normally
            if(token.Type == JTokenType.Array) return token.ToObject(objectType, serializer);
            // If it's a value, make it an array and convert it normally
            else {
                // Turns it into an array
                JArray array = new JArray(token);
                // Converts it properly and returns it
                return array.ToObject(objectType, serializer);
            }
        }
        /// <summary>
        /// Whether this converter can convert given type.
        /// </summary>
        /// <param name="objectType">Type of the object</param>
        /// <returns>Can convert the type</returns>
        public override bool CanConvert(Type objectType) =>
            false;
    }
}