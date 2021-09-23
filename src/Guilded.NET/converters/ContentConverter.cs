// using System;
// using System.Linq;

// using Newtonsoft.Json;
// using Newtonsoft.Json.Linq;

// namespace Guilded.NET.Converters
// {
//     using Base;
//     /// <summary>
//     /// Converts specific interface types.
//     /// </summary>
//     public class ContentConverter : JsonConverter
//     {
//         private static readonly Type formResponseField = typeof(FormResponseField);
//             // channel = typeof(BaseChannel), teamChannel = typeof(TeamChannel), reply = typeof(Reply);
//         /// <summary>
//         /// Writes given object as JSON.
//         /// </summary>
//         /// <param name="writer">The writer to use to write to JSON</param>
//         /// <param name="value">The object to write to JSON</param>
//         /// <param name="serializer">The serializer that is serializing the object</param>
//         public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
//             writer.WriteValue(value is FormResponseField field && !(field.TextValue is null) ? JToken.FromObject(field.TextValue) : JObject.FromObject(value));
//         /// <summary>
//         /// Reads the given JSON object as <see cref="FormField"/>.
//         /// </summary>
//         /// <param name="reader">The reader that was used to read JSON</param>
//         /// <param name="objectType">The type of the object to convert</param>
//         /// <param name="existingValue">The previous value of the property being converted</param>
//         /// <param name="serializer">The serializer that is deserializing the object</param>
//         /// <returns><see cref="FormField"/></returns>
//         public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
//         {
//             JToken token = JToken.Load(reader);

//             JObject obj = token as JObject;
//             // if (objectType == channel || objectType == teamChannel)
//             // {
//             //     if (obj.ContainsKey("type"))
//             //     {
//             //         string type = obj["type"].Value<string>();
//             //         // Check if it's a DM channel
//             //         if (type == "DM")
//             //             return obj.ToObject<DMChannel>(serializer);
//             //         // If it has threadMessageId property, then it's a temporal channel/a thread
//             //         else if (obj.ContainsKey("threadMessageId"))
//             //             return obj.ToObject<ThreadChannel>(serializer);
//             //         // Otherwise it's a normal channel
//             //         else
//             //             return obj.ToObject<Channel>(serializer);
//             //     }
//             //     // If it doesn't have a type, then it's a category
//             //     else
//             //     {
//             //         return obj.ToObject<Category>(serializer);
//             //     }
//             // }
//             // else
//             if (objectType == formResponseField)
//             {
//                 return token.Type == JTokenType.String ?
//                     // Guilded passes forms as strings or objects
//                     new FormResponseField
//                     {
//                         TextValue = ((JValue)token).Value<string>()
//                     } :
//                     new FormResponseField
//                     {
//                         OptionName = new FormId(obj["optionName"].Value<string>())
//                     };
//             }
//             // else if (objectType == reply)
//             // {
//             //     // If it contains `repliesTo`, it's a forum reply
//             //     if (obj.ContainsKey("repliesTo"))
//             //         return obj.ToObject<ForumReply>(serializer);
//             //     // If it has postId that is a string, it's announcement reply
//             //     else if (obj.ContainsKey("postId") && obj["postId"].Type == JTokenType.String)
//             //         return obj.ToObject<AnnouncementReply>(serializer);
//             //     // Otherwise it can be any other reply
//             //     else
//             //         return obj.ToObject<ContentReply>(serializer);
//             // }
//             else
//             {
//                 return null;
//             }
//         }
//         /// <summary>
//         /// Returns whether the converter supports converting the given type.
//         /// </summary>
//         /// <param name="objectType">The type of object that potentially can be converted</param>
//         /// <returns>Type can be converted</returns>
//         public override bool CanConvert(Type objectType) =>
//             objectType == formResponseField;
//     }
// }