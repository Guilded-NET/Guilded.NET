using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// A large or a small title.
    /// </summary>
    /// <remarks>
    /// A title that is either small or big.
    /// </remarks>
    /// <example>
    /// <para>Big heading</para>
    /// <code language="csharp">
    /// Heading heading = new Heading("Title here");
    /// </code>
    /// <para>Small heading</para>
    /// <code language="csharp">
    /// Heading heading = new Heading("Sub-title here", false);
    /// </code>
    /// </example>
    public class Heading : ContainerNode<Heading>
    {
        private const string large = "heading-large";

        #region Properties
        /// <summary>
        /// Gets whether the header is large and not small
        /// </summary>
        public bool IsLarge => Type == NodeType.HeadingLarge;
        #endregion

        #region Constructors
        /// <summary>
        /// A large or a small title.
        /// </summary>
        /// <param name="nodes">The list of message objects this node holds</param>
        /// <param name="isLarge">Whether the header is large and not small</param>
        public Heading(IList<ChatElement> nodes, bool isLarge = true) : base(isLarge ? NodeType.HeadingLarge : NodeType.HeadingSmall, ElementType.Block, nodes) { }
        /// <summary>
        /// A large or a small title.
        /// </summary>
        /// <param name="nodes">The array of message objects this node holds</param>
        public Heading(params ChatElement[] nodes) : this(nodes.ToList(), true) { }
        /// <summary>
        /// A large or a small title.
        /// </summary>
        /// <param name="node">Message objects this node holds</param>
        /// <param name="isLarge">Whether the header is large and not small</param>
        public Heading(ChatElement node, bool isLarge) : this(new List<ChatElement> { node }, isLarge) { }
        /// <summary>
        /// A large or a small title.
        /// </summary>
        /// <param name="leaves">The list of leaves of the text container, which heading should hold</param>
        /// <param name="isLarge">Whether the header is large and not small</param>
        public Heading(IList<Leaf> leaves, bool isLarge = true) : this(new TextContainer(leaves), isLarge) { }
        /// <summary>
        /// A large or a small title.
        /// </summary>
        /// <param name="leaves">The array of leaves of the text container, which heading should hold</param>
        public Heading(params Leaf[] leaves) : this((IList<Leaf>)leaves) { }
        /// <summary>
        /// A large or a small title.
        /// </summary>
        /// <param name="content">The text that should be converted to text container</param>
        /// <param name="isLarge">Whether the header is large and not small</param>
        public Heading(string content, bool isLarge = true) : this(new TextContainer(content), isLarge) { }
        /// <summary>
        /// A large or a small title.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <param name="isLarge">Whether the header is large and not small</param>
        public Heading(string format, object[] args, bool isLarge) : this(string.Format(format, args), isLarge) { }
        /// <summary>
        /// A large or a small title.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        public Heading(string format, params object[] args) : this(format, args, true) { }
        /// <summary>
        /// A large or a small title.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <param name="isLarge">Whether the header is large and not small</param>
        public Heading(IFormatProvider provider, string format, object[] args, bool isLarge) : this(string.Format(provider, format, args), isLarge) { }
        /// <summary>
        /// A large or a small title.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        public Heading(IFormatProvider provider, string format, params object[] args) : this(provider, format, args, true) { }
        /// <summary>
        /// A large or a small title.
        /// </summary>
        /// <param name="content">The contents that should be converted to text container</param>
        /// <param name="isLarge">Whether the header is large and not small</param>
        public Heading(object content, bool isLarge = true) : this(new TextContainer(content), isLarge) { }
        /// <summary>
        /// A large or a small title.
        /// </summary>
        /// <param name="isLarge">Whether the header is large and not small</param>
        public Heading(bool isLarge = true) : this(new List<ChatElement>(), isLarge) { }
        /// <summary>
        /// A large or a small title.
        /// </summary>
        /// <param name="type">The type of the node</param>
        [JsonConstructor]
        public Heading([JsonProperty(Required = Required.Always)] string type) : this(type == large) { }
        #endregion

        #region Overrides
        /// <summary>
        /// Converts header/title to its Markdown equivalent.
        /// </summary>
        /// <returns>Heading as string</returns>
        public override string ToString()
        {
            // # or ##
            string prefix = "#" + (IsLarge ? "" : "#");

            return $"{prefix} {base.ToString()}";
        }
        #endregion
    }
}