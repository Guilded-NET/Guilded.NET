using System;
using System.Linq;
using System.Collections.Generic;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// A line of text in a message.
    /// </summary>
    /// <example>
    /// <para>Fully text-based paragraph:</para>
    /// <code>
    /// Paragraph para = new Paragraph("Hello!");
    /// </code>
    /// <para>Paragraph with mentions:</para>
    /// <code>
    /// Paragraph para = new Paragraph
    /// (
    ///     new TextContainer("Hello, "),
    ///     new MemberMention(user),
    ///     new TextContainer("!")
    /// );
    /// </code>
    /// <para>Simple text with formatting:</para>
    /// <code>
    /// Paragraph para = new Paragraph("DO NOT BREAK RULES", MarkType.Bold, MarkType.Underline);
    /// </code>
    /// <para>Escaping given arguments:</para>
    /// <code>
    /// Paragraph para = new Paragraph
    /// (
    ///     new Leaf("Invalid argument "),
    ///     // We do not need to escape \, *, ~, _ or | in this case,
    ///     // because this is not Markdown plain text
    ///     new Leaf(arg, MarkType.InlineCode),  
    ///     new Leaf(".")
    /// );
    /// </code>
    /// <para>Building paragraph:</para>
    /// <code>
    /// Paragraph para = new Paragraph()
    ///     .WithText("Hello there, ")
    ///     .WithMention(user)
    ///     .WithText("!");
    /// </code>
    /// </example>
    public class Paragraph : ContainerNode<Paragraph>
    {
        #region Constructors
        /// <summary>
        /// A line of text in a message.
        /// </summary>
        /// <param name="nodes">The list of message elements this paragraph holds</param>
        public Paragraph(IList<ChatElement> nodes) : base(NodeType.Paragraph, ElementType.Block, nodes) { }
        /// <summary>
        /// A line of text in a message.
        /// </summary>
        /// <param name="nodes">The array of message elements this paragraph holds</param>
        public Paragraph(params ChatElement[] nodes) : this(nodes.ToList()) { }
        /// <summary>
        /// A line of text in a message.
        /// </summary>
        /// <param name="leaves">The list of leaves of the text container, which paragraph should hold</param>
        public Paragraph(IList<Leaf> leaves) : this(new TextContainer(leaves)) { }
        /// <summary>
        /// A line of text in a message.
        /// </summary>
        /// <param name="leaves">The array of leaves of the text container, which paragraph should hold</param>
        public Paragraph(params Leaf[] leaves) : this((IList<Leaf>)leaves) { }
        /// <summary>
        /// A line of text in a message.
        /// </summary>
        /// <param name="content">The text that should be converted to text container</param>
        public Paragraph(string content) : this(new TextContainer(content)) { }
        /// <summary>
        /// A line of text in a message.
        /// </summary>
        /// <param name="content">The text that should be converted to text container</param>
        /// <param name="formatting">The formatting of the text</param>
        public Paragraph(string content, params Mark[] formatting) : this(new TextContainer(content, formatting)) { }
        /// <summary>
        /// A line of text in a message.
        /// </summary>
        /// <param name="content">The text that should be converted to text container</param>
        /// <param name="formatting">The formatting of the text</param>
        public Paragraph(string content, params MarkType[] formatting) : this(new TextContainer(content, formatting)) { }
        /// <summary>
        /// A line of text in a message.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        public Paragraph(string format, params object[] args) : this(string.Format(format, args)) { }
        /// <summary>
        /// A line of text in a message.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        public Paragraph(IFormatProvider provider, string format, params object[] args) : this(string.Format(provider, format, args)) { } 
        /// <summary>
        /// A line of text in a message.
        /// </summary>
        /// <param name="content">The contents that should be converted to text container</param>
        public Paragraph(object content) : this(new TextContainer(content)) { }
        /// <summary>
        /// A line of text in a message.
        /// </summary>
        public Paragraph() : this(new List<ChatElement>()) { }
        #endregion
    }
}