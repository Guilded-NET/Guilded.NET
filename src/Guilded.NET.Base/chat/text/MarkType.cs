using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// The type of formatting for <see cref="Mark"/> used in <see cref="Leaf"/>.
    /// </summary>
    /// <seealso cref="Leaf"/>
    /// <seealso cref="Mark"/>
    [JsonConverter(typeof(StringEnumConverter), true)]
    public enum MarkType
    {
        /// <summary>
        /// A line which goes over a piece of text.
        /// </summary>
        Strikethrough,
        /// <summary>
        /// A line which goes below a piece of text.
        /// </summary>
        Underline,
        /// <summary>
        /// Makes a piece of text bulkier/heavier.
        /// </summary>
        Bold,
        /// <summary>
        /// Makes a piece of text slightly tilted to the right side.
        /// </summary>
        Italic,
        /// <summary>
        /// Puts a piece of text in a small inline code container.
        /// </summary>
        [EnumMember(Value = "inline-code-v2")]
        InlineCode,
        /// <summary>
        /// Puts a piece of text in a small inline code container. Older version of <see cref="InlineCode"/>.
        /// </summary>
        [EnumMember(Value = "inline-code")]
        InlineCodeLegacy,
        /// <summary>
        /// Makes a piece of text hidden unless clicked.
        /// </summary>
        Spoiler
    }
}