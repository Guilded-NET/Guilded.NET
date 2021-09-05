using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Converters
{
    using Base.Chat;
    /// <summary>
    /// Converts JSON to rich text markup objects.
    /// </summary>
    public class RichTextConverter : JsonConverter
    {
        private static readonly Type node = typeof(Node);
        private static readonly Type element = typeof(ChatElement);
        private static readonly IDictionary<string, Type> types = new Dictionary<string, Type>
        {
            {"paragraph", typeof(Paragraph)},
            {"link", typeof(Hyperlink)},
            {"block-quote-container", typeof(BlockQuote)},
            {"block-quote-line", typeof(LineQuote)},
            {"code-container", typeof(CodeContainer)},
            {"code-line", typeof(CodeLine)},
            {"markdown-plain-text", typeof(MarkdownText)},
            {"unordered-list", typeof(ChatList)},
            {"ordered-list", typeof(ChatList)},
            {"list-item", typeof(ChatListItem)},
            {"reaction", typeof(ChatEmote)},
            {"heading-large", typeof(Heading)},
            {"heading-small", typeof(Heading)},
            {"webhookMessage", typeof(ChatEmbed)},
            //{"systemMessage", typeof(SystemMessage)},
            {"mention", typeof(MemberMention)},
            {"channel", typeof(ChannelMention)},
            {"image", typeof(Image)}
        };
        private static readonly IDictionary<string, Type> objs = new Dictionary<string, Type>
        {
            {"text", typeof(TextContainer)},
            {"mark", typeof(Mark)},
            {"leaf", typeof(Leaf)}
        };
        /// <summary>
        /// If this converter can write.
        /// </summary>
        /// <value>False</value>
        public override bool CanWrite => false;
        /// <summary>
        /// Writes given object as JSON.
        /// </summary>
        /// <param name="writer">The writer to use to write to JSON</param>
        /// <param name="value">The object to write to JSON</param>
        /// <param name="serializer">The serializer that is serializing the object</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
            writer.WriteValue(JObject.FromObject(value));
        /// <summary>
        /// Reads the given JSON object as <see cref="ChatElement"/>.
        /// </summary>
        /// <param name="reader">The reader that was used to read JSON</param>
        /// <param name="objectType">The type of the object to convert</param>
        /// <param name="existingValue">The previous value of the property being converted</param>
        /// <param name="serializer">The serializer that is deserializing the object</param>
        /// <returns><see cref="ChatElement"/></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            string objparam = obj["object"].Value<string>();

            // If it's a node
            if (!objs.ContainsKey(objparam))
            {
                string objtype = obj["type"].Value<string>();
                // To check if it's a supported node type
                if (types.ContainsKey(objtype))
                    return obj.ToObject(types[objtype], serializer);
                else return null;
            }
            else
            {
                return obj.ToObject(objs[objparam], serializer);
            }
        }
        /// <summary>
        /// Returns whether the converter supports converting the given type.
        /// </summary>
        /// <param name="objectType">The type of object that potentially can be converted</param>
        /// <returns>Type can be converted</returns>
        public override bool CanConvert(Type objectType) =>
            objectType == node || objectType == element;
    }
}