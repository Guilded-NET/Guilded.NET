namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Type of the node.
    /// </summary>
    public enum NodeType {
        // Embeds & blocks
        BlockQuoteContainer, BlockQuoteLine, Embed,
        // Texts & Paragraphs
        Paragraph, Link, MarkdownPlainText, SystemMessage, HeadingLarge, HeadingSmall, Divider,
        // Images & Emotes
        Reaction, Image,
        // Code
        CodeContainer, CodeLine,
        // Lists
        UnorderedList, OrderedList, ListItem,
        // Mentions
        Channel, Mention
    }
}