using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// An image file in a message.
    /// </summary>
    public class Image : ContainerNode<ChatElement, Image>
    {
        #region Properties
        /// <summary>
        /// The URL to the image's source
        /// </summary>
        /// <value>URL</value>
        [JsonIgnore]
        public Uri Source
        {
            get => Data.Src;
            set => Data.Src = value;
        }
        /// <summary>
        /// The message about the image.
        /// </summary>
        /// <returns>Caption?</returns>
        [JsonIgnore]
        public ImageCaption Caption => (ImageCaption)Nodes.FirstOrDefault(x => x is ImageCaption);
        #endregion

        #region Constructors
        /// <summary>
        /// An image file in a message.
        /// </summary>
        /// <param name="src">The URL to the image's source</param>
        /// <param name="nodes">The list of message objects this node holds</param>
        public Image(Uri src, IList<ChatElement> nodes) : base(NodeType.Image, ElementType.Block, nodes) =>
            Data.Src = src;
        /// <summary>
        /// An image file in a message.
        /// </summary>
        /// <param name="src">The URL to the image's source</param>
        /// <param name="nodes">The list of message objects this node holds</param>
        public Image(string src, IList<ChatElement> nodes) : this(new Uri(src), nodes) { }
        /// <summary>
        /// An image file in a message.
        /// </summary>
        /// <param name="src">The URL to the image's source</param>
        /// <param name="caption">The message about the image</param>
        public Image(Uri src, ImageCaption caption) : this(src, new List<ChatElement> { caption }) { }
        /// <summary>
        /// An image file in a message.
        /// </summary>
        /// <param name="src">The URL to the image's source</param>
        /// <param name="caption">The message about the image</param>
        public Image(string src, ImageCaption caption) : this(new Uri(src), caption) { }
        /// <summary>
        /// An image file in a message.
        /// </summary>
        /// <param name="src">The URL to the image's source</param>
        /// <param name="caption">The message about the image</param>
        public Image(Uri src, TextContainer caption) : this(src, new ImageCaption(caption)) { }
        /// <summary>
        /// An image file in a message.
        /// </summary>
        /// <param name="src">The URL to the image's source</param>
        /// <param name="caption">The message about the image</param>
        public Image(string src, TextContainer caption) : this(new Uri(src), caption) { }
        /// <summary>
        /// An image file in a message.
        /// </summary>
        /// <param name="src">The URL to the image's source</param>
        /// <param name="caption">The message about the image</param>
        public Image(Uri src, params Leaf[] caption) : this(src, new ImageCaption(caption)) { }
        /// <summary>
        /// An image file in a message.
        /// </summary>
        /// <param name="src">The URL to the image's source</param>
        /// <param name="caption">The message about the image</param>
        public Image(string src, params Leaf[] caption) : this(new Uri(src), caption) { }
        /// <summary>
        /// An image file in a message.
        /// </summary>
        /// <param name="src">The URL to the image's source</param>
        /// <param name="caption">The message about the image</param>
        public Image(Uri src, string caption) : this(src, new ImageCaption(caption)) { }
        /// <summary>
        /// An image file in a message.
        /// </summary>
        /// <param name="src">The URL to the image's source</param>
        /// <param name="caption">The message about the image</param>
        public Image(string src, string caption) : this(new Uri(src), caption) { }
        /// <summary>
        /// An image file in a message.
        /// </summary>
        /// <param name="src">The URL to the image's source</param>
        /// <param name="caption">The message about the image</param>
        /// <param name="formatting">The formatting of the caption</param>
        public Image(Uri src, string caption, params MarkType[] formatting) : this(src, new ImageCaption(caption, formatting)) { }
        /// <summary>
        /// An image file in a message.
        /// </summary>
        /// <param name="src">The URL to the image's source</param>
        /// <param name="caption">The message about the image</param>
        /// <param name="formatting">The formatting of the caption</param>
        public Image(string src, string caption, params MarkType[] formatting) : this(new Uri(src), caption, formatting) { }
        /// <summary>
        /// An image file in a message.
        /// </summary>
        /// <param name="src">The URL to the image's source</param>
        /// <param name="caption">The message about the image</param>
        /// <param name="formatting">The formatting of the caption</param>
        public Image(Uri src, string caption, params Mark[] formatting) : this(src, new ImageCaption(caption, formatting)) { }
        /// <summary>
        /// An image file in a message.
        /// </summary>
        /// <param name="src">The URL to the image's source</param>
        /// <param name="caption">The message about the image</param>
        /// <param name="formatting">The formatting of the caption</param>
        public Image(string src, string caption, params Mark[] formatting) : this(new Uri(src), caption, formatting) { }
        /// <summary>
        /// An image file in a message.
        /// </summary>
        /// <param name="src">The URL to the image</param>
        public Image(Uri src) : this(src, new List<ChatElement> { new TextContainer("") }) { }
        /// <summary>
        /// An image file in a message.
        /// </summary>
        /// <param name="src">The URL to the image</param>
        public Image(string src) : this(new Uri(src)) { }
        #endregion

        #region Overrides
        /// <summary>
        /// Converts the image to its Markdown equivalent.
        /// </summary>
        /// <returns>Image as string</returns>
        public override string ToString() => $"![{base.ToString()}]({Source})\n";
        #endregion
    }
}