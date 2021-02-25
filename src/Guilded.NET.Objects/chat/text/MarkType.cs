using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Markdown mark type.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter), true)]
    public enum MarkType {
        /// <summary>
        /// Produces a line which goes over whole text.
        /// </summary>
        [EnumMember(Value = "strikethrough")]
        Strikethrough,
        /// <summary>
        /// Produces a line which goes below text.
        /// </summary>
        [EnumMember(Value = "underline")]
        Underline,
        /// <summary>
        /// Makes text heavier/bigger.
        /// </summary>
        [EnumMember(Value = "bold")]
        Bold,
        /// <summary>
        /// Makes a text slightly tilted to the right side.
        /// </summary>
        [EnumMember(Value = "italic")]
        Italic,
        /// <summary>
        /// Puts a text in a small container with a darker background.
        /// </summary>
        [EnumMember(Value = "inline-code-v2")]
        InlineCode,
        /// <summary>
        /// Old type of inline code.
        /// </summary>
        [EnumMember(Value = "inline-code")]
        InlineCodeLegacy,
        /// <summary>
        /// Makes text invisible until it's clicked on.
        /// </summary>
        [EnumMember(Value = "spoiler")]
        Spoiler,
        /// <summary>
        /// Unknown.
        /// </summary>
        [EnumMember(Value = "code-format")]
        CodeFormat
    }
}