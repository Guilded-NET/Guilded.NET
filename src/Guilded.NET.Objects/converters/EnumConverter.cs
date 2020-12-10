using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

// Ultimate spaghetti code
namespace Guilded.NET.Objects.Converters {
    using Chat;
    using Teams;
    using Content;
    /// <summary>
    /// Converts enum to string and vice versa.
    /// </summary>
    public class EnumConverter: JsonConverter {
        // For checking types
        static readonly Type msgobjtype = typeof(MsgObject);
        static readonly Type marktype = typeof(MarkType);
        static readonly Type nodetype = typeof(NodeType);
        static readonly Type member = typeof(Membership);
        static readonly Type chattype = typeof(ChatType);
        static readonly Type msgtype = typeof(MessageType);
        static readonly Type mentiontype = typeof(MentionType);
        static readonly Type channeltype = typeof(ChannelType);
        static readonly Type mediatype = typeof(MediaType);
        // All of the allowed types
        static readonly Type[] allowed = new Type[] { msgobjtype, marktype, nodetype, member, chattype, msgtype, mentiontype, channeltype };
        // All msgobj enums and their string equivalents
        static readonly IDictionary<string, MsgObject> msgobj = new Dictionary<string, MsgObject> {
            {"block", MsgObject.Block},
            {"document", MsgObject.Document},
            {"inline", MsgObject.Inline},
            {"leaf", MsgObject.Leaf},
            {"mark", MsgObject.Mark},
            {"text", MsgObject.Text},
            {"value", MsgObject.Value}
        };
        // All marktype enums and their string equivalents
        static readonly IDictionary<string, MarkType> marktypes = new Dictionary<string, MarkType> {
            {"bold", MarkType.Bold},
            {"inline-code-v2", MarkType.InlineCode},
            {"italic", MarkType.Italic},
            {"spoiler", MarkType.Spoiler},
            {"strikethrough", MarkType.Strikethrough},
            {"underline", MarkType.Underline}
        };
        // All nodetype enums and their string equivalents
        static readonly IDictionary<string, NodeType> nodetypes = new Dictionary<string, NodeType> {
            {"block-quote-container", NodeType.BlockQuoteContainer},
            {"webhookMessage", NodeType.Embed},
            {"block-quote-line", NodeType.BlockQuoteLine},
            {"paragraph", NodeType.Paragraph},
            {"markdown-plain-text", NodeType.MarkdownPlainText},
            {"code-container", NodeType.CodeContainer},
            {"code-line", NodeType.CodeLine},
            {"unordered-list", NodeType.UnorderedList},
            {"ordered-list", NodeType.OrderedList},
            {"list-item", NodeType.ListItem},
            {"image", NodeType.Image},
            {"reaction", NodeType.Reaction},
            {"systemMessage", NodeType.SystemMessage},
            {"mention", NodeType.Mention},
            {"channel", NodeType.Channel},
            {"heading-large", NodeType.HeadingLarge},
            {"heading-small", NodeType.HeadingSmall}
        };
        static readonly IDictionary<string, MembershipType> membershiptypes = new Dictionary<string, MembershipType> {
            {"joined", MembershipType.Joined},
            {"left", MembershipType.Left},
            {"following", MembershipType.Following}
        };
        static readonly IDictionary<string, ChatType> chattypes = new Dictionary<string, ChatType> {
            {"Team", ChatType.Team},
            {"DM", ChatType.DM}
        };
        static readonly IDictionary<string, ChannelType> channeltypes = new Dictionary<string, ChannelType> {
            {"chat", ChannelType.Chat},
            {"announcement", ChannelType.Announcement},
            {"voice", ChannelType.Voice},
            {"forum", ChannelType.Forum},
            {"doc", ChannelType.Document},
            {"media", ChannelType.Media},
            {"event", ChannelType.Event},
            {"list", ChannelType.List},
            {"scheduling", ChannelType.Scheduling},
            {"stream", ChannelType.Stream}
        };
        static readonly IDictionary<string, MessageType> messagetype = new Dictionary<string, MessageType> {
            {"default", MessageType.Default},
            {"system", MessageType.System}
        };
        static readonly IDictionary<string, MentionType> mentions = new Dictionary<string, MentionType> {
            {"person", MentionType.Person},
            {"here", MentionType.Here},
            {"everyone", MentionType.Everyone},
            {"role", MentionType.Role}
        };
        static readonly IDictionary<string, MediaType> media = new Dictionary<string, MediaType> {
            {"image", MediaType.Image},
            {"video", MediaType.Video}
        };
        /// <summary>
        /// Writes enum to the string.
        /// </summary>
        /// <param name="writer">JsonWriter</param>
        /// <param name="value">Enum</param>
        /// <param name="serializer">Serializer</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
            writer.WriteValue(ConvertTo(value, value.GetType()));
        /// <summary>
        /// Converts enum value to string.
        /// </summary>
        /// <param name="value">Value to convert to string</param>
        /// <param name="type">Type of the value</param>
        /// <returns>String</returns>
        public static string ConvertTo(object value, Type type) {
            if(type == msgobjtype) return ConvertTo(msgobj, (MsgObject)value);
            else if(type == marktype) return ConvertTo(marktypes, (MarkType)value);
            else if(type == nodetype) return ConvertTo(nodetypes, (NodeType)value);
            else if(type == member) return ConvertTo(membershiptypes, (MembershipType)value);
            else if(type == msgtype) return ConvertTo(messagetype, (MessageType)value);
            else if(type == mentiontype) return ConvertTo(mentions, (MentionType)value);
            else if(type == channeltype) return ConvertTo(channeltypes, (ChannelType)value);
            else if(type == chattype) return ConvertTo(chattypes, (ChatType)value);
            else if(type == mediatype) return ConvertTo(media, (MediaType)value);
            else throw new ArgumentException($"{nameof(value)} can not be converted. Given type: {type.FullName}");
        }
        /// <summary>
        /// Converts enum value to string.
        /// </summary>
        /// <param name="dict">Dictionary of the enum</param>
        /// <param name="t">Value</param>
        /// <returns>String</returns>
        protected static string ConvertTo<T>(IDictionary<string, T> dict, T t) where T: IConvertible => dict.FirstOrDefault(x => object.Equals(x.Value, t)).Key;
        /// <summary>
        /// Converts string to enum value.
        /// </summary>
        /// <param name="value">String to be parsed</param>
        /// <returns>Any enum value</returns>
        public static object ConvertFrom(string value, Type type) {
            if(type == msgobjtype) return msgobj[value];
            else if(type == marktype) return marktypes[value];
            else if(type == member) return membershiptypes[value];
            else if(type == chattype) return chattypes[value];
            else if(type == msgtype) return messagetype[value];
            else if(type == mentiontype) return mentions[value];
            else if(type == channeltype) return channeltypes[value];
            else if(type == chattype) return chattypes[value];
            else if(type == mediatype) return media[value];
            else return nodetypes[value];
        }
        
        /// <summary>
        /// Converts string to enum.
        /// </summary>
        /// <param name="reader">Reader</param>
        /// <param name="objectType">Type of the object</param>
        /// <param name="existingValue">Previous property value</param>
        /// <param name="serializer">Serializer</param>
        /// <returns>GLongId or GId</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) => ConvertFrom((string)reader.Value, objectType);
        /// <summary>
        /// Whether or not this converter can convert given type.
        /// </summary>
        /// <param name="objectType">Type of the object</param>
        /// <returns>Can convert the type</returns>
        public override bool CanConvert(Type objectType) => allowed.Contains(objectType);
    }
}