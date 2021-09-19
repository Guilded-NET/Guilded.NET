using System.Collections.Generic;
using System.Linq;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// A quote of someone's text.
    /// </summary>
    /// <example>
    /// <para>Quote block with strings:</para>
    /// <code lang="csharp">
    /// BlockQuote quote = new BlockQuote("Quote line #1", "Quote line #2");
    /// </code>
    /// <para>Quote block with quote lines:</para>
    /// <code lang="csharp">
    /// BlockQuote quote = new BlockQuote
    /// (
    ///     new LineQuote("Quote line #1"),
    ///     new LineQuote("Quote line #2")
    /// );
    /// </code>
    /// </example>
    /// <seealso cref="LineQuote"/>
    /// <seealso cref="Paragraph"/>
    public class BlockQuote : ContainerNode<LineQuote, BlockQuote>
    {
        #region Constructors
        /// <summary>
        /// A quote of someone's text.
        /// </summary>
        public BlockQuote() : base(NodeType.BlockQuoteContainer, ElementType.Block) { }
        /// <summary>
        /// A quote of someone's text.
        /// </summary>
        /// <param name="nodes">The list of lines this quote holds</param>
        public BlockQuote(IList<LineQuote> nodes) : base(NodeType.BlockQuoteContainer, ElementType.Block, nodes) { }
        /// <summary>
        /// A quote of someone's text.
        /// </summary>
        /// <param name="nodes">The list of lines this quote holds</param>
        public BlockQuote(params LineQuote[] nodes) : this(nodes.ToList()) { }
        /// <summary>
        /// A quote of someone's text.
        /// </summary>
        /// <param name="content">The text that should be converted to quote lines</param>
        public BlockQuote(params string[] content) : this(content.Select(line => new LineQuote(line)).ToList()) { }
        /// <summary>
        /// A quote of someone's text.
        /// </summary>
        /// <param name="content">The text that should be converted to quote lines</param>
        /// <param name="formatting">The formatting of the text</param>
        public BlockQuote(string content, params Mark[] formatting) : this(new LineQuote(content, formatting)) { }
        /// <summary>
        /// A quote of someone's text.
        /// </summary>
        /// <param name="content">The text that should be converted to quote lines</param>
        /// <param name="formatting">The formatting of the text</param>
        public BlockQuote(string content, params MarkType[] formatting) : this(new LineQuote(content, formatting)) { }
        /// <summary>
        /// A quote of someone's text.
        /// </summary>
        /// <param name="content">The array of objects that should be converted to quote lines</param>
        public BlockQuote(params object[] content) : this(content.Select(line => new LineQuote(line)).ToList()) { }
        #endregion
    }
}