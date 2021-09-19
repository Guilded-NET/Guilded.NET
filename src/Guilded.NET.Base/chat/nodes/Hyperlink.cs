using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// A piece of text that references a link.
    /// </summary>
    /// <remarks>
    /// Holds a text and hyperlinks URL in the text.
    /// </remarks>
    /// <example>
    /// <code lang="csharp">
    /// Hyperlink link = new Hyperlink("https://guilded.gg/", "Guilded's website");
    /// </code>
    /// </example>
    /// <seealso cref="Uri"/>
    public class Hyperlink : ContainerNode<Hyperlink>
    {
        #region Properties
        /// <summary>
        /// The link that hyperlink references.
        /// </summary>
        /// <value>URL?</value>
        [JsonIgnore]
        public Uri Href
        {
            get => Data.Href;
            set => Data.Href = value;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// A piece of text that references a link.
        /// </summary>
        /// <param name="href">The link this hyperlink holds</param>
        /// <param name="nodes">The list of message objects this node holds</param>
        public Hyperlink(Uri href, IList<ChatElement> nodes) : base(NodeType.Link, ElementType.Inline, nodes) =>
            Data.Href = href;
        /// <summary>
        /// A piece of text that references a link.
        /// </summary>
        /// <param name="href">The link this hyperlink holds</param>
        /// <param name="nodes">The list of message objects this node holds</param>
        public Hyperlink(string href, IList<ChatElement> nodes) : this(new Uri(href), nodes) { }
        /// <summary>
        /// A piece of text that references a link.
        /// </summary>
        /// <param name="href">The link this hyperlink holds</param>
        /// <param name="nodes">The array of message objects this node holds</param>
        public Hyperlink(Uri href, params ChatElement[] nodes) : this(href, nodes.ToList()) { }
        /// <summary>
        /// A piece of text that references a link.
        /// </summary>
        /// <param name="href">The link this hyperlink holds</param>
        /// <param name="nodes">The array of message objects this node holds</param>
        public Hyperlink(string href, params ChatElement[] nodes) : this(new Uri(href), nodes) { }
        /// <summary>
        /// A piece of text that references a link.
        /// </summary>
        /// <param name="href">The link this hyperlink holds</param>
        public Hyperlink(Uri href) : this(href, new TextContainer(href.ToString())) { }
        /// <summary>
        /// A piece of text that references a link.
        /// </summary>
        /// <param name="href">The link this hyperlink holds</param>
        public Hyperlink(string href) : this(new Uri(href)) { }
        /// <summary>
        /// A piece of text that references a link.
        /// </summary>
        /// <param name="href">The link this hyperlink holds</param>
        /// <param name="leaves">The list of leaves of the text container, which hyperlink should hold</param>
        public Hyperlink(Uri href, IList<Leaf> leaves) : this(href, new TextContainer(leaves)) { }
        /// <summary>
        /// A piece of text that references a link.
        /// </summary>
        /// <param name="href">The link this hyperlink holds</param>
        /// <param name="leaves">The list of leaves of the text container, which hyperlink should hold</param>
        public Hyperlink(string href, IList<Leaf> leaves) : this(new Uri(href), leaves) { }
        /// <summary>
        /// A piece of text that references a link.
        /// </summary>
        /// <param name="href">The link this hyperlink holds</param>
        /// <param name="leaves">The array of leaves of the text container, which hyperlink should hold</param>
        public Hyperlink(Uri href, params Leaf[] leaves) : this(href, (IList<Leaf>)leaves) { }
        /// <summary>
        /// A piece of text that references a link.
        /// </summary>
        /// <param name="href">The link this hyperlink holds</param>
        /// <param name="leaves">The array of leaves of the text container, which hyperlink should hold</param>
        public Hyperlink(string href, params Leaf[] leaves) : this(new Uri(href), leaves) { }
        /// <summary>
        /// A piece of text that references a link.
        /// </summary>
        /// <param name="href">The link this hyperlink holds</param>
        /// <param name="content">The text that should be converted to text container</param>
        public Hyperlink(Uri href, string content) : this(href, new TextContainer(content)) { }
        /// <summary>
        /// A piece of text that references a link.
        /// </summary>
        /// <param name="href">The link this hyperlink holds</param>
        /// <param name="content">The text that should be converted to text container</param>
        public Hyperlink(string href, string content) : this(new Uri(href), content) { }
        /// <summary>
        /// A piece of text that references a link.
        /// </summary>
        /// <param name="href">The link this hyperlink holds</param>
        /// <param name="content">The text that should be converted to text container</param>
        /// <param name="formatting">The formatting of the text</param>
        public Hyperlink(Uri href, string content, params Mark[] formatting) : this(href, new TextContainer(content, formatting)) { }
        /// <summary>
        /// A piece of text that references a link.
        /// </summary>
        /// <param name="href">The link this hyperlink holds</param>
        /// <param name="content">The text that should be converted to text container</param>
        /// <param name="formatting">The formatting of the text</param>
        public Hyperlink(string href, string content, params Mark[] formatting) : this(new Uri(href), content, formatting) { }
        /// <summary>
        /// A piece of text that references a link.
        /// </summary>
        /// <param name="href">The link this hyperlink holds</param>
        /// <param name="content">The text that should be converted to text container</param>
        /// <param name="formatting">The formatting of the text</param>
        public Hyperlink(Uri href, string content, params MarkType[] formatting) : this(href, new TextContainer(content, formatting)) { }
        /// <summary>
        /// A piece of text that references a link.
        /// </summary>
        /// <param name="href">The link this hyperlink holds</param>
        /// <param name="content">The text that should be converted to text container</param>
        /// <param name="formatting">The formatting of the text</param>
        public Hyperlink(string href, string content, params MarkType[] formatting) : this(new Uri(href), content, formatting) { }
        /// <summary>
        /// A piece of text that references a link.
        /// </summary>
        /// <param name="href">The link this hyperlink holds</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        public Hyperlink(Uri href, string format, params object[] args) : this(href, string.Format(format, args)) { }
        /// <summary>
        /// A piece of text that references a link.
        /// </summary>
        /// <param name="href">The link this hyperlink holds</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        public Hyperlink(string href, string format, params object[] args) : this(new Uri(href), format, args) { }
        /// <summary>
        /// A piece of text that references a link.
        /// </summary>
        /// <param name="href">The link this hyperlink holds</param>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        public Hyperlink(Uri href, IFormatProvider provider, string format, params object[] args) : this(href, string.Format(provider, format, args)) { }
        /// <summary>
        /// A piece of text that references a link.
        /// </summary>
        /// <param name="href">The link this hyperlink holds</param>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        public Hyperlink(string href, IFormatProvider provider, string format, params object[] args) : this(new Uri(href), provider, format, args) { }
        /// <summary>
        /// A piece of text that references a link.
        /// </summary>
        /// <param name="href">The link this hyperlink holds</param>
        /// <param name="content">The contents that should be converted to text container</param>
        public Hyperlink(Uri href, object content) : this(href, new TextContainer(content)) { }
        /// <summary>
        /// A piece of text that references a link.
        /// </summary>
        /// <param name="href">The link this hyperlink holds</param>
        /// <param name="content">The contents that should be converted to text container</param>
        public Hyperlink(string href, object content) : this(href, new TextContainer(content)) { }
        #endregion

        #region Overrides
        /// <summary>
        /// Converts hyperlink to its Markdown equivalent.
        /// </summary>
        /// <returns>Hyperlink as string</returns>
        public override string ToString() =>
            $"[{base.ToString()}]({Data.Href})";
        #endregion
    }
}