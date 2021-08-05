using System;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Converters
{
    using Base;
    using Base.Content;
    using Base.Forms;
    using Base.Teams;
    using Base.Users;
    /// <summary>
    /// Converts specific interface types.
    /// </summary>
    public class MiscConverter : JsonConverter
    {
        private static readonly Type channel = typeof(BaseChannel);
        private static readonly Type teamChannel = typeof(TeamChannel);
        private static readonly Type formResponseField = typeof(FormResponseField);
        private static readonly Type reply = typeof(Reply);
        // All of the allowed types
        private static readonly Type[] allowed = new Type[] { channel, teamChannel, formResponseField, reply };
        /// <summary>
        /// Writes specific object to the JSON.
        /// </summary>
        /// <param name="writer">JsonWriter</param>
        /// <param name="value">Object to convert</param>
        /// <param name="serializer">Serializer</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
            writer.WriteValue(value is FormResponseField field && !(field.TextValue is null) ? JToken.FromObject(field.TextValue) : JObject.FromObject(value));
        /// <summary>
        /// Converts string to a specific type.
        /// </summary>
        /// <param name="reader">Reader</param>
        /// <param name="objectType">Type of the object</param>
        /// <param name="existingValue">Previous property value</param>
        /// <param name="serializer">Serializer</param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken tkn = JToken.Load(reader);
            // Get it as an object
            JObject obj = tkn as JObject;
            // If it's a channel type
            if (objectType == channel || objectType == teamChannel)
            {
                // If it has a property `type`
                if (obj.ContainsKey("type"))
                {
                    // Gets the type
                    string type = obj["type"].Value<string>();
                    // If it's a DM channel
                    if (type == "DM") return obj.ToObject<DMChannel>(serializer);
                    // If it has threadMessageId property, then it's temporal channel
                    else if (obj.ContainsKey("threadMessageId")) return obj.ToObject<ThreadChannel>(serializer);
                    // Else, it's a normal channel
                    else return obj.ToObject<Channel>(serializer);
                    // If it doesn't, then it is a category
                }
                else return obj.ToObject<Category>(serializer);
                // If it's a form response field
            }
            // else if (objectType == formResponseField)
            //     return
            //     tkn.Type == JTokenType.String ?
            //         // If token is a string
            //         new FormResponseField
            //         {
            //             TextValue = ((JValue)tkn).Value<string>()
            //         } :
            //         // Else, it's an object
            //         new FormResponseField
            //         {
            //             OptionName = FormId.Parse(obj["optionName"].Value<string>())
            //         };
            else if (objectType == reply)
                // If it contains `repliesTo`, it's a forum reply
                if (obj.ContainsKey("repliesTo")) return obj.ToObject<ForumReply>(serializer);
                // If it has postId that is string
                else if (obj.ContainsKey("postId") && obj["postId"].Type == JTokenType.String) return obj.ToObject<AnnouncementReply>(serializer);
                // If it doesn't, it's a normal reply
                else return obj.ToObject<ContentReply>(serializer);
            // If it doesn't know the type
            else return null;
        }
        /// <summary>
        /// Whether this converter can convert given type.
        /// </summary>
        /// <param name="objectType">Type of the object</param>
        /// <returns>Can convert the type</returns>
        public override bool CanConvert(Type objectType) =>
            allowed.Contains(objectType);
    }
}