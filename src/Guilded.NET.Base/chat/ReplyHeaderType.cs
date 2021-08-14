using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// A type of the reply header.
    /// </summary>
    /// <seealso cref="MessageType"/>
    /// <seealso cref="MentionType"/>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ReplyHeaderType
    {
        /// <summary>
        /// A reply header in a block-quote.
        /// </summary>
        BlockQuote,
        /// <summary>
        /// A normal reply header.
        /// </summary>
        Reply
    }
}