using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Represents markdown marks.
    /// </summary>
    public class Mark: BaseObject, IMessageObject {
        internal static IDictionary<MarkType, string> MarkSymbols = new Dictionary<MarkType, string> {
            { MarkType.Bold, "**" },
            { MarkType.InlineCode, "`" },
            { MarkType.Italic, "*" },
            { MarkType.Spoiler, "||" },
            { MarkType.Strikethrough, "~~" },
            { MarkType.Underline, "__" }
        };
        /// <summary>
        /// Object of the mark.
        /// </summary>
        /// <value>MsgObject.Mark</value>
        public MsgObject Object {
            get; set;
        } = MsgObject.Mark;
        /// <summary>
        /// Type of the markdown.
        /// </summary>
        /// <value>Markdown type</value>
        [JsonProperty("type", Required = Required.Always)]
        public MarkType Type {
            get; set;
        }
        /// <summary>
        /// Data of the mark.
        /// </summary>
        /// <value>Mark data</value>
        [JsonProperty("data")]
        public IDictionary<string, object> Data {
            get; set;
        } = null;
    }
}