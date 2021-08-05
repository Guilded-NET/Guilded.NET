using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// Type of the node.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter), true)]
    public enum NodeType
    {
        /// <summary>
        /// Container that holds block quote lines.
        /// </summary>
        [EnumMember(Value = "block-quote-container")]
        BlockQuoteContainer,
        /// <summary>
        /// Line in a block quote.
        /// </summary>
        [EnumMember(Value = "block-quote-line")]
        BlockQuoteLine,
        /// <summary>
        /// Customizable version of the embed.
        /// </summary>
        WebhookMessage,
        /// <summary>
        /// An embed for the link posted.
        /// </summary>
        [EnumMember(Value = "content-embed")]
        ContentEmbed,
        /// <summary>
        /// Text paragraph with mentions and text leaves.
        /// </summary>
        Paragraph,
        /// <summary>
        /// Piece of text that links a URL.
        /// </summary>
        Link,
        /// <summary>
        /// Turns given Markdown document to Guilded's nodes and leaves.
        /// </summary>
        [EnumMember(Value = "markdown-plain-text")]
        MarkdownPlainText,
        /// <summary>
        /// Message that got created after an event.
        /// </summary>
        SystemMessage,
        /// <summary>
        /// Large title in a document or a forum post.
        /// </summary>
        [EnumMember(Value = "heading-large")]
        HeadingLarge,
        /// <summary>
        /// Small title in a document or a forum post.
        /// </summary>
        [EnumMember(Value = "heading-small")]
        HeadingSmall,
        /// <summary>
        /// Splits documents into sections
        /// </summary>
        Divider,
        /// <summary>
        /// An emote/emoji, which shows an emotion.
        /// </summary>
        Reaction,
        /// <summary>
        /// An image or a video.
        /// </summary>
        Image,
        /// <summary>
        /// Container that holds code lines.
        /// </summary>
        [EnumMember(Value = "code-container")]
        CodeContainer,
        /// <summary>
        /// Line inside a code container
        /// </summary>
        [EnumMember(Value = "code-line")]
        CodeLine,
        /// <summary>
        /// Dotted/bulleted list.
        /// </summary>
        [EnumMember(Value = "unordered-list")]
        UnorderedList,
        /// <summary>
        /// Numerated list.
        /// </summary>
        [EnumMember(Value = "ordered-list")]
        OrderedList,
        /// <summary>
        /// An item inside of unordered or ordered list.
        /// </summary>
        [EnumMember(Value = "list-item")]
        ListItem,
        /// <summary>
        /// Mention of a channel or a thread.
        /// </summary>
        Channel,
        /// <summary>
        /// Mention that mentions a group people or one specific member.
        /// </summary>
        Mention,
        /// <summary>
        /// Poll/form for submitting applications or voting.
        /// </summary>
        Form,
        /// <summary>
        /// Header that tells to what document is replying to.
        /// </summary>
        [EnumMember(Value = "replying-to-user-header")]
        ReplyHeader,
        /// <summary>
        /// Caption for the image.
        /// </summary>
        [EnumMember(Value = "image-caption-line")]
        ImageCaptionLine
    }
}