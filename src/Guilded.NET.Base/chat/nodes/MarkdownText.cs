using System;
using System.Linq;
using System.Collections.Generic;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// A piece of Markdown text.
    /// </summary>
    /// <remarks>
    /// A node for converting given string to Markdown.
    /// </remarks>
    /// <example>
    /// <para>With lines(requires at least 2 strings):</para>
    /// <code>
    /// MarkdownText markdown = new MarkdownText("> Quote block", "", "Paragraph here");
    /// </code>
    /// <para>Without lines:</para>
    /// <code>
    /// MarkdownText markdown = new MarkdownText("> Quote Block\n\nParagraph here");
    /// </code>
    /// </example>
    /// <seealso cref="Paragraph"/>
    /// <seealso cref="MessageContent"/>
    public class MarkdownText : ContainerNode<TextContainer, MarkdownText>
    {
        #region JSON properties
        /// <summary>
        /// A piece of Markdown text.
        /// </summary>
        /// <param name="nodes">The list of message objects this node holds</param>
        public MarkdownText(IList<TextContainer> nodes) : base(NodeType.MarkdownPlainText, ElementType.Block, nodes) { }
        /// <summary>
        /// A piece of Markdown text.
        /// </summary>
        /// <param name="nodes">The list of message objects this node holds</param>
        public MarkdownText(params TextContainer[] nodes) : this(nodes.ToList()) { }
        /// <summary>
        /// A piece of Markdown text.
        /// </summary>
        /// <param name="text">Markdown document's content</param>
        public MarkdownText(string text) : this(new TextContainer(text)) { }
        /// <summary>
        /// A piece of Markdown text.
        /// </summary>
        /// <param name="lines">The list of lines in Markdown document</param>
        public MarkdownText(IList<string> lines) : this(string.Join("\n", lines)) { }
        /// <summary>
        /// A piece of Markdown text.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        public MarkdownText(string format, params object[] args) : this(string.Format(format, args)) { }
        /// <summary>
        /// A piece of Markdown text.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        public MarkdownText(IFormatProvider provider, string format, params object[] args) : this(string.Format(provider, format, args)) { }
        /// <summary>
        /// A piece of Markdown text.
        /// </summary>
        /// <param name="content">The contents that should be converted to strings</param>
        public MarkdownText(object content) : this(content.ToString()) { }
        #endregion

        #region Additional
        /// <summary>
        /// Adds a text container based on given leaves.
        /// </summary>
        /// <param name="leaves">The array of leaves to add</param>
        /// <returns>This</returns>
        public MarkdownText AddText(params Leaf[] leaves) =>
            Add(new TextContainer(leaves));
        /// <summary>
        /// Adds a text container based on given string.
        /// </summary>
        /// <param name="content">The text that text container holds</param>
        /// <returns>This</returns>
        public MarkdownText AddText(string content) =>
            Add(new TextContainer(content));
        #endregion
    }
}