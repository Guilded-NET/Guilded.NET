using System;
using System.Collections.Generic;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// A line of code in code block node.
    /// </summary>
    /// <remarks>
    /// A line in a code container.
    /// </remarks>
    /// <example>
    /// <para>Using code line in code container:</para>
    /// <code>
    /// CodeLine[] lines = new CodeLine[]
    /// {
    ///     new CodeLine("using System;"),
    ///     new CodeLine(new TextContainer("using System.Linq;")),
    ///     new CodeLine(new Leaf("using"), new Leaf(" "), new Leaf("System.Collections.Generic"), new Leaf(";"))
    /// };
    /// CodeContainer code = new CodeContainer("csharp", lines);
    /// </code>
    /// </example>
    public class CodeLine : ContainerNode<TextContainer, CodeLine>
    {
        #region Constructors
        /// <summary>
        /// A line of code in code block node.
        /// </summary>
        /// <param name="nodes">The list of text containers this node holds</param>
        public CodeLine(IList<TextContainer> nodes) : base(NodeType.CodeLine, ElementType.Block, nodes) { }
        /// <summary>
        /// A line of code in code block node.
        /// </summary>
        /// <param name="node">The text container this code line holds</param>
        public CodeLine(TextContainer node) : this(new List<TextContainer> { node }) { }
        /// <summary>
        /// A line of code in code block node.
        /// </summary>
        /// <param name="leaves">The list of leaves that text container will hold</param>
        public CodeLine(IList<Leaf> leaves) : this(new TextContainer(leaves)) { }
        /// <summary>
        /// A line of code in code block node.
        /// </summary>
        /// <param name="leaves">The array of leaves that text container will hold</param>
        public CodeLine(params Leaf[] leaves) : this((IList<Leaf>)leaves) { }
        /// <summary>
        /// A line of code in code block node.
        /// </summary>
        /// <param name="content">The content of this line</param>
        public CodeLine(string content) : this(new TextContainer[] { new TextContainer(content) }) { }
        /// <summary>
        /// A line of code in code block node.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        public CodeLine(string format, params object[] args) : this(string.Format(format, args)) { }
        /// <summary>
        /// A line of code in code block node.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        public CodeLine(IFormatProvider provider, string format, params object[] args) : this(string.Format(provider, format, args)) { }
        /// <summary>
        /// A line of code in code block node.
        /// </summary>
        /// <param name="content">The contents that should be converted to text container</param>
        public CodeLine(object content) : this(new TextContainer(content)) { }
        #endregion
    }
}