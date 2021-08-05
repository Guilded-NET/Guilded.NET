using System;
using System.Linq;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Chat
{
    using Embeds;
    /// <summary>
    /// The document of the content containing all of the nodes and information.
    /// </summary>
    public class MessageDocument : MessageRoot<MessageDocument>
    {
        #region JSON properties
        /// <summary>
        /// The list of nodes in the message.
        /// </summary>
        /// <value>List of nodes</value>
        [JsonProperty(Required = Required.Always)]
        public IList<Node> Nodes
        {
            get; set;
        }
        /// <summary>
        /// Data of this message document.
        /// </summary>
        /// <value>Message document data</value>
        [JsonProperty(Required = Required.Always)]
        public ElementData Data
        {
            get; set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// The document of the content containing all of the nodes and information.
        /// </summary>
        /// <param name="data">The data of this message document</param>
        /// <param name="nodes">The list of nodes in the document</param>
        public MessageDocument(ElementData data, IList<Node> nodes) : base(ElementType.Document) =>
            (Data, Nodes) = (data, nodes);
        /// <summary>
        /// The document of the content containing all of the nodes and information.
        /// </summary>
        /// <param name="nodes">The list of nodes in the document</param>
        public MessageDocument(IList<Node> nodes) : this(new ElementData(), nodes) { }
        /// <summary>
        /// The document of the content containing all of the nodes and information.
        /// </summary>
        /// <param name="data">The data of this message document</param>
        /// <param name="nodes">The array of nodes in the document</param>
        public MessageDocument(ElementData data, params Node[] nodes) : this(data, nodes.ToList()) { }
        /// <summary>
        /// The document of the content containing all of the nodes and information.
        /// </summary>
        /// <param name="nodes">The array of nodes in the document</param>
        public MessageDocument(params Node[] nodes) : this(nodes.ToList()) { }
        /// <summary>
        /// The document of the content containing all of the nodes and information.
        /// </summary>
        /// <param name="data">The data of this message document</param>
        /// <param name="embeds">The list of embeds that chat embed will hold</param>
        public MessageDocument(ElementData data, params Embed[] embeds) : this(data, new ChatEmbed(embeds)) { }
        /// <summary>
        /// The document of the content containing all of the nodes and information.
        /// </summary>
        /// <param name="embeds">The list of embeds that chat embed will hold</param>
        public MessageDocument(params Embed[] embeds) : this(new ElementData(), embeds) { }
        /// <summary>
        /// The document of the content containing all of the nodes and information.
        /// </summary>
        /// <param name="data">The data of this message document</param>
        /// <param name="text">The text container that paragraph will hold</param>
        public MessageDocument(ElementData data, TextContainer text) : this(data, new Paragraph(text)) { }
        /// <summary>
        /// The document of the content containing all of the nodes and information.
        /// </summary>
        /// <param name="text">The text container that paragraph will hold</param>
        public MessageDocument(TextContainer text) : this(new ElementData(), text) { }
        /// <summary>
        /// The document of the content containing all of the nodes and information.
        /// </summary>
        /// <param name="data">The data of this message document</param>
        /// <param name="leaves">The array of leaves that paragraph will hold</param>
        public MessageDocument(ElementData data, params Leaf[] leaves) : this(data, new TextContainer(leaves)) { }
        /// <summary>
        /// The document of the content containing all of the nodes and information.
        /// </summary>
        /// <param name="leaves">The array of leaves that paragraph will hold</param>
        public MessageDocument(params Leaf[] leaves) : this(new ElementData(), leaves) { }
        /// <summary>
        /// The document of the content containing all of the nodes and information.
        /// </summary>
        /// <param name="data">The data of this message document</param>
        /// <param name="text">The text document that will be used as a paragraph</param>
        public MessageDocument(ElementData data, string text) : this(data, new Paragraph(text)) { }
        /// <summary>
        /// The document of the content containing all of the nodes and information.
        /// </summary>
        /// <param name="text">The text document that will be used as a paragraph</param>
        public MessageDocument(string text) : this(new ElementData(), text) { }
        /// <summary>
        /// The document of the content containing all of the nodes and information.
        /// </summary>
        /// <param name="data">The data of this message document</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        public MessageDocument(ElementData data, string format, params object[] args) : this(data, new Paragraph(format, args)) { }
        /// <summary>
        /// The document of the content containing all of the nodes and information.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        public MessageDocument(string format, params object[] args) : this(new ElementData(), format, args) { }
        /// <summary>
        /// The document of the content containing all of the nodes and information.
        /// </summary>
        /// <param name="data">The data of this message document</param>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        public MessageDocument(ElementData data, IFormatProvider provider, string format, params object[] args) : this(data, new Paragraph(provider, format, args)) { }
        /// <summary>
        /// The document of the content containing all of the nodes and information.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        public MessageDocument(IFormatProvider provider, string format, params object[] args) : this(new ElementData(), provider, format, args) { }
        /// <summary>
        /// The document of the content containing all of the nodes and information.
        /// </summary>
        /// <param name="data">The data of this message document</param>
        /// <param name="content">The contents of the paragraph that should be converted to strings</param>
        public MessageDocument(ElementData data, object content) : this(data, new Paragraph(content)) { }
        /// <summary>
        /// The document of the content containing all of the nodes and information.
        /// </summary>
        /// <param name="content">The contents of the paragraph that should be converted to strings</param>
        public MessageDocument(object content) : this(new ElementData(), content) { }
        #endregion

        #region Additional
        /// <summary>
        /// Adds the list of URLs in the message share list.
        /// </summary>
        /// <param name="urls">The list of URLs that will be shared</param>
        /// <returns>This</returns>
        public MessageDocument Share(IList<Uri> urls)
        {
            // Sets the list, if the property is null
            if(Data.ShareUrls is null) Data.ShareUrls = urls;
            // Otherwise, it combines the lists
            else Data.ShareUrls = Data.ShareUrls.Concat(urls).ToList();
            return this;
        }
        /// <summary>
        /// Adds an URL in the message share list.
        /// </summary>
        /// <param name="url">A URL that will be shared</param>
        /// <returns>This</returns>
        public MessageDocument Share(Uri url) =>
            Share(new List<Uri> { url });
        /// <summary>
        /// Adds an URL in the message share list.
        /// </summary>
        /// <param name="url">A URL that will be shared</param>
        /// <returns>This</returns>
        public MessageDocument Share(string url) =>
            Share(new Uri(url));
        /// <summary>
        /// Adds a list of nodes to the message document.
        /// </summary>
        /// <param name="nodes">The list of nodes to add</param>
        /// <returns>This</returns>
        public override MessageDocument With(IList<Node> nodes)
        {
            Nodes = Nodes.Concat(nodes).ToList();
            return this;
        }
        /// <summary>
        /// Adds a node to the message document.
        /// </summary>
        /// <param name="node">The node to add</param>
        /// <returns>This</returns>
        public override MessageDocument With(Node node)
        {
            Nodes.Add(node);
            return this;
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Converts all nodes to string and joins them together.
        /// </summary>
        /// <returns>List of nodes as string</returns>
        public override string ToString() => string.Concat(Nodes);
        #endregion
    }
}