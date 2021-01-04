namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Type of the node.
    /// </summary>
    public enum NodeType {
        // Embeds & blocks
        /// <summary>
        /// A container which holds block quote lines.
        /// </summary>
        BlockQuoteContainer,
        /// <summary>
        /// A line in a block quote.
        /// </summary>
        BlockQuoteLine,
        /// <summary>
        /// Represents website and rich embeds
        /// </summary>
        Embed,
        // Texts & Paragraphs
        /// <summary>
        /// A normal paragraph containing text.
        /// </summary>
        Paragraph,
        /// <summary>
        /// A piece of text with a link inside it.
        /// </summary>
        Link,
        /// <summary>
        /// Turns given markdown to Guilded nodes and leaves.
        /// </summary>
        MarkdownPlainText,
        /// <summary>
        /// A message which is created when someone renames a channel. Can't be created by a user.
        /// </summary>
        SystemMessage,
        /// <summary>
        /// A large title in a document or a forum post.
        /// </summary>
        HeadingLarge,
        /// <summary>
        /// A small title in a document or a forum post.
        /// </summary>
        HeadingSmall,
        /// <summary>
        /// 3 dots which divide a document.
        /// </summary>
        Divider,
        // Images & Emotes
        /// <summary>
        /// An emote.
        /// </summary>
        Reaction,
        /// <summary>
        /// An image or a video.
        /// </summary>
        Image,
        // Code
        /// <summary>
        /// A container which highlights code.
        /// </summary>
        CodeContainer,
        /// <summary>
        /// A line inside a code container
        /// </summary>
        CodeLine,
        // Lists
        /// <summary>
        /// A dotted/bulleted list.
        /// </summary>
        UnorderedList,
        /// <summary>
        /// A numerated list.
        /// </summary>
        OrderedList,
        /// <summary>
        /// An item inside of unorder or ordered list.
        /// </summary>
        ListItem,
        // Mentions
        /// <summary>
        /// A channel mention(#channel name)
        /// </summary>
        Channel,
        /// <summary>
        /// A role, user, @everyone or @here mention.
        /// </summary>
        Mention,
        /// <summary>
        /// A poll/form node.
        /// </summary>
        Form,
        /// <summary>
        /// A header which tells to who a comment is replying to.
        /// </summary>
        ReplyHeader
    }
}