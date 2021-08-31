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
    public class ContentConverter : JsonConverter
    {
        private static readonly Type channel = typeof(BaseChannel), teamChannel = typeof(TeamChannel), formResponseField = typeof(FormResponseField), reply = typeof(Reply);
        /// <summary>
        /// Writes given object as JSON.
        /// </summary>
        /// <param name="writer">The writer to use to write to JSON</param>
        /// <param name="value">The object to write to JSON</param>
        /// <param name="serializer">The serializer that is serializing the object</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
            writer.WriteValue(value is FormResponseField field && !(field.TextValue is null) ? JToken.FromObject(field.TextValue) : JObject.FromObject(value));
        /// <summary>
        /// Reads the given JSON object as <see cref="Reply"/>, <see cref="FormField"/> or <see cref="BaseChannel"/>.
        /// </summary>
        /// <param name="reader">The reader that was used to read JSON</param>
        /// <param name="objectType">The type of the object to convert</param>
        /// <param name="existingValue">The previous value of the property being converted</param>
        /// <param name="serializer">The serializer that is deserializing the object</param>
        /// <returns><see cref="Reply"/> | <see cref="FormField"/> | <see cref="BaseChannel"/></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            // Get it as an object
            JObject obj = token as JObject;
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
                // If it's a form response field
                else
                {
                    return obj.ToObject<Category>(serializer);
                }
            }
            else if (objectType == formResponseField)
            {
                return
               token.Type == JTokenType.String ?
                   // If token is a string
                   new FormResponseField
                   {
                       TextValue = ((JValue)token).Value<string>()
                   } :
                   // Else, it's an object
                   new FormResponseField
                   {
                       OptionName = new FormId(obj["optionName"].Value<string>())
                   };
            }
            else if (objectType == reply)
            {
                // If it contains `repliesTo`, it's a forum reply
                if (obj.ContainsKey("repliesTo")) return obj.ToObject<ForumReply>(serializer);
                // If it has postId that is string
                else if (obj.ContainsKey("postId") && obj["postId"].Type == JTokenType.String) return obj.ToObject<AnnouncementReply>(serializer);
                // If it doesn't, it's a normal reply
                else return obj.ToObject<ContentReply>(serializer);
            }
            // If it doesn't know the type
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Returns whether the converter supports converting the given type.
        /// </summary>
        /// <param name="objectType">The type of object that potentially can be converted</param>
        /// <returns>Type can be converted</returns>
        public override bool CanConvert(Type objectType) =>
            objectType == channel || objectType == teamChannel || objectType == formResponseField || objectType == reply;
    }
}