using System.Runtime.Serialization;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Type of the message object.
    /// </summary>
    public enum MsgObject {
        /// <summary>
        /// A block, which is the only thing in a line.
        /// </summary>
        [EnumMember(Value = "block")]
        Block,
        /// <summary>
        /// Inline, can be multiple of them in one line.
        /// </summary>
        [EnumMember(Value = "inline")]
        Inline,
        /// <summary>
        /// Marks text in a specific way(bold, italic).
        /// </summary>
        [EnumMember(Value = "mark")]
        Mark,
        /// <summary>
        /// A text object which contains leafs.
        /// </summary>
        [EnumMember(Value = "text")]
        Text,
        /// <summary>
        /// A piece of text.
        /// </summary>
        [EnumMember(Value = "leaf")]
        Leaf,
        /// <summary>
        /// Message content which can be found in messages, statuses, forum posts, profile posts.
        /// </summary>
        [EnumMember(Value = "value")]
        Value,
        /// <summary>
        /// A message document which is found inside message content.
        /// </summary>
        [EnumMember(Value = "document")]
        Document
    }
}