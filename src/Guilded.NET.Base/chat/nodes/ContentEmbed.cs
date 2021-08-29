using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// Block that tells information about a link.
    /// </summary>
    /// <remarks>
    /// A normal embed for links.
    /// </remarks>
    /// <example>
    /// <code>
    /// ContentEmbed embed = new ContentEmbed("https://guilded.gg/");
    /// </code>
    /// </example>
    /// <seealso cref="ChatEmbed"/>
    /// <seealso cref="Embeds.Embed"/>
    public class ContentEmbed : ContainerNode<TextContainer, ContentEmbed>
    {
        #region Properties
        /// <summary>
        /// The link that the content embed references.
        /// </summary>
        /// <value>URL?</value>
        [JsonIgnore]
        public Uri Url
        {
            get => Data.Url;
            set => Data.Url = value;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Block that tells information about a link.
        /// </summary>
        /// <param name="url">The link that the content embed references</param>
        /// <param name="nodes">The list of message objects this node holds</param>
        public ContentEmbed(Uri url, IList<TextContainer> nodes) : base(NodeType.Link, ElementType.Inline, nodes) =>
            Data.Url = url;
        /// <summary>
        /// Block that tells information about a link.
        /// </summary>
        /// <param name="url">The link that the content embed references</param>
        /// <param name="nodes">The list of message objects this node holds</param>
        public ContentEmbed(string url, IList<TextContainer> nodes) : this(new Uri(url), nodes) { }
        /// <summary>
        /// Block that tells information about a link.
        /// </summary>
        /// <param name="url">The link that the content embed references</param>
        /// <param name="node">The node that the content embed holds</param>
        public ContentEmbed(Uri url, TextContainer node) : this(url, new TextContainer[] { node }) =>
            Data.Url = url;
        /// <summary>
        /// Block that tells information about a link.
        /// </summary>
        /// <param name="url">The link that the content embed references</param>
        /// <param name="node">The node that the content embed holds</param>
        public ContentEmbed(string url, TextContainer node) : this(new Uri(url), node) { }
        /// <summary>
        /// Block that tells information about a link.
        /// </summary>
        /// <param name="url">The link that the content embed references</param>
        public ContentEmbed(Uri url) : this(url, new TextContainer(url.ToString())) =>
            Data.Url = url;
        /// <summary>
        /// Block that tells information about a link.
        /// </summary>
        /// <param name="url">The link that the content embed references</param>
        public ContentEmbed(string url) : this(new Uri(url)) { }
        #endregion

        #region Overrides
        /// <summary>
        /// Converts a content embed to its string representation
        /// </summary>
        /// <returns>Content embed as string</returns>
        public override string ToString() => $"![{base.ToString()}]({Data.Url})";
        #endregion
    }
}