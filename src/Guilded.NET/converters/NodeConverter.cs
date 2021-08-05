using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Converters
{
    using Base.Chat;
    /// <summary>
    /// Converts JSON objects to nodes.
    /// </summary>
    public class NodeConverter : JsonConverter
    {
        static readonly Type node = typeof(Node);
        static readonly Type msgobj = typeof(ChatElement);
        static readonly IDictionary<string, Type> types = new Dictionary<string, Type> {
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
        static readonly IDictionary<string, Type> objs = new Dictionary<string, Type> {
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
        /// Writes node to the JSON.
        /// </summary>
        /// <param name="writer">JsonWriter</param>
        /// <param name="value">ID</param>
        /// <param name="serializer">Serializer</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => writer.WriteValue(JObject.FromObject(value));
        /// <summary>
        /// Converts object to node.
        /// </summary>
        /// <param name="reader">Reader</param>
        /// <param name="objectType">Type of the object</param>
        /// <param name="existingValue">Previous property value</param>
        /// <param name="serializer">Serializer</param>
        /// <returns>GId</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            // Convert JSON to Node
            string objparam = obj["object"].Value<string>();
            // If it's neither text, nor mark, nor leaf
            if (!objs.ContainsKey(objparam))
            {
                // Gets object type
                string objtype = obj["type"].Value<string>();
                // If it has objtype property(e.g., if types has key paragraph, code-line, etc.)
                if (types.ContainsKey(objtype)) return obj.ToObject(types[objtype], serializer);
                // If it doesn't, return null
                else return null;
            }
            // Else, parse it as leaf/text/mark.
            else return obj.ToObject(objs[objparam], serializer);
        }
        /// <summary>
        /// Whether this converter can convert given type.
        /// </summary>
        /// <param name="objectType">Type of the object</param>
        /// <returns>Can convert the type</returns>
        public override bool CanConvert(Type objectType) => objectType == node || objectType == msgobj;
    }
}