using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Guilded.NET.Objects.Chat
{
    /// <summary>
    /// Type of the node.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum NodeType
    {
        // Embeds & blocks
        /// <summary>
        /// A container which holds block quote lines.
        /// </summary>
        [EnumMember(Value = "block-quote-container")]
        BlockQuoteContainer,
        /// <summary>
        /// A line in a block quote.
        /// </summary>
        [EnumMember(Value = "block-quote-line")]
        BlockQuoteLine,
        /// <summary>
        /// Represents website and rich embeds
        /// </summary>
        [EnumMember(Value = "webhookMessage")]
        Embed,
        // Texts & Paragraphs
        /// <summary>
        /// A normal paragraph containing text.
        /// </summary>
        [EnumMember(Value = "paragraph")]
        Paragraph,
        /// <summary>
        /// A piece of text with a link inside it.
        /// </summary>
        [EnumMember(Value = "link")]
        Link,
        /// <summary>
        /// Turns given markdown to Guilded nodes and leaves.
        /// </summary>
        [EnumMember(Value = "markdown-plain-text")]
        MarkdownPlainText,
        /// <summary>
        /// A message which is created when someone renames a channel. Can't be created by a user.
        /// </summary>
        [EnumMember(Value = "systemMessage")]
        SystemMessage,
        /// <summary>
        /// A large title in a document or a forum post.
        /// </summary>
        [EnumMember(Value = "heading-large")]
        HeadingLarge,
        /// <summary>
        /// A small title in a document or a forum post.
        /// </summary>
        [EnumMember(Value = "heading-small")]
        HeadingSmall,
        /// <summary>
        /// 3 dots which divide a document.
        /// </summary>
        [EnumMember(Value = "divider")]
        Divider,
        // Images & Emotes
        /// <summary>
        /// An emote.
        /// </summary>
        [EnumMember(Value = "reaction")]
        Reaction,
        /// <summary>
        /// An image or a video.
        /// </summary>
        [EnumMember(Value = "image")]
        Image,
        // Code
        /// <summary>
        /// A container which highlights code.
        /// </summary>
        [EnumMember(Value = "code-container")]
        CodeContainer,
        /// <summary>
        /// A line inside a code container
        /// </summary>
        [EnumMember(Value = "code-line")]
        CodeLine,
        // Lists
        /// <summary>
        /// A dotted/bulleted list.
        /// </summary>
        [EnumMember(Value = "unordered-list")]
        UnorderedList,
        /// <summary>
        /// A numerated list.
        /// </summary>
        [EnumMember(Value = "ordered-list")]
        OrderedList,
        /// <summary>
        /// An item inside of unorder or ordered list.
        /// </summary>
        [EnumMember(Value = "list-item")]
        ListItem,
        // Mentions
        /// <summary>
        /// A channel mention(#channel name)
        /// </summary>
        [EnumMember(Value = "channel")]
        Channel,
        /// <summary>
        /// A role, user, @everyone or @here mention.
        /// </summary>
        [EnumMember(Value = "mention")]
        Mention,
        /// <summary>
        /// A poll/form node.
        /// </summary>
        [EnumMember(Value = "form")]
        Form,
        /// <summary>
        /// A header which tells to who a comment is replying to.
        /// </summary>
        [EnumMember(Value = "replying-to-user-header")]
        ReplyHeader,
        /// <summary>
        /// A caption for the image.
        /// </summary>
        [EnumMember(Value = "image-caption-line")]
        ImageCaptionLine
    }
}