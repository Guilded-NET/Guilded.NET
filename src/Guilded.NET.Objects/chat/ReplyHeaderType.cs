using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Guilded.NET.Objects.Chat
{
    /// <summary>
    /// A type of the reply header.
    /// </summary>
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