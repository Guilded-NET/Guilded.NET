using System.Collections.Generic;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// Seperator that divides document into sections.
    /// </summary>
    /// <example>
    /// <code>
    /// MessageContent content = new MessageContent
    /// (
    ///     new Paragraph("Text here"),
    ///     new Divider(),    
    ///     new Paragraph("Something else")
    /// );
    /// </code>
    /// </example>
    public class Divider : ContainerNode<TextContainer, Divider>
    {
        /// <summary>
        /// Seperator that divides document into sections.
        /// </summary>
        public Divider() : base(NodeType.Divider, ElementType.Block, new TextContainer("")) { }
        /// <summary>
        /// Converts divider to its Markdown equivalent.
        /// </summary>
        /// <returns>Divider as string</returns>
        public override string ToString() => "---\n";
    }
}