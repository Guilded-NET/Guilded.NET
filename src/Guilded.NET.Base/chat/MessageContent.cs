using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Chat
{
    using Embeds;
    /// <summary>
    /// The contents of messages, list items, etc. based on rich text markup.
    /// </summary>
    /// <remarks>
    /// Content for messages, forum posts, etc. using rich text markup.
    /// </remarks>
    /// <example>
    /// <para>Markdown text:</para>
    /// <code lang="csharp">
    /// MesssageContent content = new MessageContent("> Quote\n\nParagraph");
    /// </code>
    /// <para>Simple nodes:</para>
    /// <code lang="csharp">
    /// MessageContent content = new MessageContent
    /// (
    ///     new BlockQuote(msg),
    ///     new Paragraph("Sorry, we cannot do that yet.")
    /// );
    /// </code>
    /// <para>Building message content:</para>
    /// <code lang="csharp">
    /// MessageContent content = new MessageContent()
    ///     .AddParagraph("Hello there.")
    ///     .AddParagraph("...");
    /// </code>
    /// </example>
    /// <seealso cref="BlockQuote"/>
    /// <seealso cref="Paragraph"/>
    /// <seealso cref="MarkdownText"/>
    /// <seealso cref="MessageDocument"/>
    /// <seealso cref="Node"/>
    /// <seealso cref="ContainerNode{T}"/>
    public class MessageContent : MessageRoot<MessageContent>
    {
        #region Properties
        /// <summary>
        /// The document containing all of the message nodes.
        /// </summary>
        /// <value>Message document</value>
        [JsonProperty(Required = Required.Always)]
        public MessageDocument Document
        {
            get; set;
        }
        /// <summary>
        /// Gets the list of nodes of the document.
        /// </summary>
        /// <value>List of nodes</value>
        [JsonIgnore]
        public IList<Node> Nodes => Document.Nodes;
        #endregion

        #region Constructors
        /// <summary>
        /// The contents of messages, list items, etc. based on rich text markup.
        /// </summary>
        /// <param name="document">The document containing all of the message nodes</param>
        public MessageContent(MessageDocument document) : base(ElementType.Value) =>
            Document = document;
        /// <summary>
        /// The contents of messages, list items, etc. based on rich text markup.
        /// </summary>
        /// <param name="nodes">The list of nodes message content's document will hold</param>
        public MessageContent(IList<Node> nodes) : this(new MessageDocument(nodes)) { }
        /// <summary>
        /// The contents of messages, list items, etc. based on rich text markup.
        /// </summary>
        /// <param name="nodes">The array of nodes message content's document will hold</param>
        public MessageContent(params Node[] nodes) : this(nodes.ToList()) { }
        /// <summary>
        /// The contents of messages, list items, etc. based on rich text markup.
        /// </summary>
        /// <param name="embeds">The list of embeds that chat embed will hold</param>
        public MessageContent(params Embed[] embeds) : this(new ChatEmbed(embeds)) { }
        /// <summary>
        /// The contents of messages, list items, etc. based on rich text markup.
        /// </summary>
        /// <param name="text">The text container that paragraph will hold</param>
        public MessageContent(TextContainer text) : this(new Paragraph(text)) { }
        /// <summary>
        /// The contents of messages, list items, etc. based on rich text markup.
        /// </summary>
        /// <param name="leaves">The array of leaves that paragraph will hold</param>
        public MessageContent(params Leaf[] leaves) : this(new TextContainer(leaves)) { }
        /// <summary>
        /// The contents of messages, list items, etc. based on rich text markup.
        /// </summary>
        /// <param name="text">The text document that will be used as a paragraph</param>
        public MessageContent(string text) : this(new Paragraph(text)) { }
        /// <summary>
        /// The contents of messages, list items, etc. based on rich text markup.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        public MessageContent(string format, params object[] args) : this(new Paragraph(format, args)) { }
        /// <summary>
        /// The contents of messages, list items, etc. based on rich text markup.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        public MessageContent(IFormatProvider provider, string format, params object[] args) : this(new Paragraph(provider, format, args)) { }
        /// <summary>
        /// The contents of messages, list items, etc. based on rich text markup.
        /// </summary>
        /// <param name="content">The contents of the paragraph that should be converted to strings</param>
        public MessageContent(object content) : this(new Paragraph(content)) { }
        /// <summary>
        /// The contents of messages, list items, etc. based on rich text markup.
        /// </summary>
        public MessageContent() : this(new List<Node>()) { }
        #endregion

        #region Additional
        /// <summary>
        /// Adds a list of nodes to the message document.
        /// </summary>
        /// <param name="nodes">The list of nodes to add</param>
        /// <returns>This</returns>
        public override MessageContent Add(IList<Node> nodes)
        {
            Document.Add(nodes);
            return this;
        }
        /// <summary>
        /// Adds a node to the message document.
        /// </summary>
        /// <param name="node">The node to add</param>
        /// <returns>This</returns>
        public override MessageContent Add(Node node)
        {
            Document.Add(node);
            return this;
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Returns the string equivalent of the message document.
        /// </summary>
        /// <returns>Message document as string</returns>
        public override string ToString() =>
            Document?.ToString();
        #endregion
    }
}