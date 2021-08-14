using System;
using System.Collections.Generic;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// A paragraph of quote block.
    /// </summary>
    /// <remarks>
    /// A line in a quote block.
    /// </remarks>
    /// <example>
    /// <code>
    /// LineQuote line = new LineQuote("Quoted text");
    /// BlockQuote quote = new BlockQuote(line);
    /// </code>
    /// </example>
    /// <seealso cref="BlockQuote"/>
    public class LineQuote : ContainerNode<LineQuote>
    {
        #region Constructors
        /// <summary>
        /// A paragraph of quoteblock.
        /// </summary>
        /// <param name="nodes">The list of message objects this node holds</param>
        public LineQuote(IList<ChatElement> nodes) : base(NodeType.BlockQuoteLine, ElementType.Block, nodes) { }
        /// <summary>
        /// A paragraph of quoteblock.
        /// </summary>
        /// <param name="node">Message element this quote line holds</param>
        public LineQuote(ChatElement node) : this(new List<ChatElement> { node }) { }
        /// <summary>
        /// A paragraph of quoteblock.
        /// </summary>
        /// <param name="leaves">The list of leaves of the text container, which quote line should hold</param>
        public LineQuote(params Leaf[] leaves) : this(new TextContainer(leaves)) { }
        /// <summary>
        /// A paragraph of quoteblock.
        /// </summary>
        /// <param name="content">The text that should be converted to text container</param>
        public LineQuote(string content) : this(new TextContainer(content)) { }
        /// <summary>
        /// A paragraph of quoteblock.
        /// </summary>
        /// <param name="content">The text that should be converted to text container</param>
        /// <param name="formatting">The formatting of the text</param>
        public LineQuote(string content, params Mark[] formatting) : this(new TextContainer(content, formatting)) { }
        /// <summary>
        /// A paragraph of quoteblock.
        /// </summary>
        /// <param name="content">The text that should be converted to text container</param>
        /// <param name="formatting">The formatting of the text</param>
        public LineQuote(string content, params MarkType[] formatting) : this(new TextContainer(content, formatting)) { }
        /// <summary>
        /// A paragraph of quoteblock.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        public LineQuote(string format, params object[] args) : this(string.Format(format, args)) { }
        /// <summary>
        /// A paragraph of quoteblock.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        public LineQuote(IFormatProvider provider, string format, params object[] args) : this(string.Format(provider, format, args)) { }
        /// <summary>
        /// A paragraph of quoteblock.
        /// </summary>
        /// <param name="content">The contents that should be converted to text container</param>
        public LineQuote(object content) : this(new TextContainer(content)) { }
        /// <summary>
        /// A paragraph of quoteblock.
        /// </summary>
        public LineQuote() : this(new List<ChatElement>()) { }
        #endregion

        #region Overrides
        /// <summary>
        /// Converts quote block line to its Markdown equivalent.
        /// </summary>
        /// <returns>Quoteblock line as string</returns>
        public override string ToString() => $"> {base.ToString().Replace("\n", "\n>")}";
        #endregion
    }
}