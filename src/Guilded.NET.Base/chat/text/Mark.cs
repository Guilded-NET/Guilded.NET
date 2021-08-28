using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// A formatting of a leaf.
    /// </summary>
    /// <seealso cref="TextContainer"/>
    /// <seealso cref="Leaf"/>
    /// <seealso cref="MarkType"/>
    public class Mark : ChatElement, ICloneable
    {
        internal static IDictionary<MarkType, string> MarkSymbols = new Dictionary<MarkType, string>
        {
            { MarkType.Bold, "**" },
            { MarkType.InlineCode, "`" },
            { MarkType.Italic, "*" },
            { MarkType.Spoiler, "||" },
            { MarkType.Strikethrough, "~~" },
            { MarkType.Underline, "__" }
        };
        /// <summary>
        /// The type of the formatting.
        /// </summary>
        /// <value>Mark type</value>
        [JsonProperty(Required = Required.Always)]
        public MarkType Type
        {
            get; set;
        }
        /// <summary>
        /// The data of the formatting.
        /// </summary>
        /// <value>Mark data</value>
        public IDictionary<string, object> Data
        {
            get; set;
        }
        /// <summary>
        /// Creates a new formatting <see cref="Mark"/> based on given <see cref="MarkType"/>.
        /// </summary>
        /// <param name="type">The type of the formatting</param>
        /// <param name="data">The information about the formatting</param>
        public Mark(MarkType type, Dictionary<string, object> data) : base(ElementType.Mark) =>
            (Type, Data) = (type, data);
        /// <summary>
        /// Creates a new formatting <see cref="Mark"/> based on given <see cref="MarkType"/>.
        /// </summary>
        /// <param name="type">The type of the formatting</param>
        public Mark(MarkType type) : this(type, new Dictionary<string, object>()) { }
        /// <summary>
        /// Creates a new mark with similar properties.
        /// </summary>
        /// <returns>Mark</returns>
        public object Clone() =>
            new Mark(Type, new Dictionary<string, object>(Data));
        /// <summary>
        /// Returns this mark's formatting prefix/postfix.
        /// </summary>
        /// <returns>Prefix/postfix</returns>
        public override string ToString() =>
            MarkSymbols[Type];
    }
}