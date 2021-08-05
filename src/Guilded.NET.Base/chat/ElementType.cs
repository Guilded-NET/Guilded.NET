using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// Type of the message object.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter), true)]
    public enum ElementType
    {
        /// <summary>
        /// A block, that is the only thing in a line.
        /// </summary>
        Block,
        /// <summary>
        /// Inline, can be multiple of them in one line.
        /// </summary>
        Inline,
        /// <summary>
        /// Marks text in a specific way(bold, italic).
        /// </summary>
        Mark,
        /// <summary>
        /// A text object which contains leafs.
        /// </summary>
        Text,
        /// <summary>
        /// A piece of text.
        /// </summary>
        Leaf,
        /// <summary>
        /// Message content which can be found in messages, statuses, forum posts, profile posts.
        /// </summary>
        Value,
        /// <summary>
        /// A message document that is found inside message content.
        /// </summary>
        Document
    }
}