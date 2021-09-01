// using System;
// using System.Linq;
// using System.Collections.Generic;

// using Newtonsoft.Json;
// using Newtonsoft.Json.Linq;

// namespace Guilded.NET.Base
// {
//     /// <summary>
//     /// Converts lists with 1 item to that item and item to list with 1 item.
//     /// </summary>
//     public class FlatListConverter : JsonConverter
//     {
//         /// <summary>
//         /// Writes given object as JSON.
//         /// </summary>
//         /// <param name="writer">The writer to use to write to JSON</param>
//         /// <param name="value">The object to write to JSON</param>
//         /// <param name="serializer">The serializer that is serializing the object</param>
//         public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
//             // Converts object to the list
//             IList<object> objList = (IList<object>)value;
//             // If there is 1 or no items, write the first value or default
//             if (objList.Count < 2)
//             {
//                 writer.WriteValue(objList.FirstOrDefault());
//             }
//             else
//             {
//                 // Starts the array
//                 writer.WriteStartArray();
//                 // Writes all values
//                 foreach (var item in objList) writer.WriteValue(item);
//                 // Ends the array
//                 writer.WriteEndArray();
//             }
//         }
//         /// <summary>
//         /// Reads the given JSON value or array as <see cref="Array"/>.
//         /// </summary>
//         /// <param name="reader">The reader that was used to read JSON</param>
//         /// <param name="objectType">The type of the object to convert</param>
//         /// <param name="existingValue">The previous value of the property being converted</param>
//         /// <param name="serializer">The serializer that is deserializing the object</param>
//         /// <returns><see cref="Array"/></returns>
//         public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
//         {
//             // Gets it as a token
//             JToken token = JToken.ReadFrom(reader);
//             // If it's an array, then convert it normally
//             if (token.Type == JTokenType.Array)
//             {
//                 return token.ToObject(objectType, serializer);
//             }
//             // If it's a value, make it an array and convert it normally
//             else
//             {
//                 // Turns it into an array
//                 JArray array = new JArray(token);
//                 // Converts it properly and returns it
//                 return array.ToObject(objectType, serializer);
//             }
//         }
//         /// <summary>
//         /// Returns whether the converter supports converting the given type.
//         /// </summary>
//         /// <param name="objectType">The type of object that potentially can be converted</param>
//         /// <returns>Type can be converted</returns>
//         public override bool CanConvert(Type objectType) =>
//             false;
//     }
// }