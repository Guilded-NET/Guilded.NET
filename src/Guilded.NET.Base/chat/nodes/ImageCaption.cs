using System;
using System.Linq;
using System.Collections.Generic;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// A message explaining an image.
    /// </summary>
    public class ImageCaption : ContainerNode<ImageCaption>
    {
        #region Constructors
        /// <summary>
        /// A message explaining an image.
        /// </summary>
        public ImageCaption() : base(NodeType.ImageCaptionLine, ElementType.Block) { }
        /// <summary>
        /// A message explaining an image.
        /// </summary>
        /// <param name="nodes">The list of message objects this node holds</param>
        public ImageCaption(IList<ChatElement> nodes) : base(NodeType.ImageCaptionLine, ElementType.Block, nodes) { }
        /// <summary>
        /// A message explaining an image.
        /// </summary>
        /// <param name="nodes">The array of message objects this node holds</param>
        public ImageCaption(params ChatElement[] nodes) : this(nodes.ToList()) { }
        /// <summary>
        /// A message explaining an image.
        /// </summary>
        /// <param name="leaves">The list of leaves of the text container this caption holds</param>
        public ImageCaption(IList<Leaf> leaves) : this(new TextContainer(leaves)) { }
        /// <summary>
        /// A message explaining an image.
        /// </summary>
        /// <param name="leaves">The array of leaves of the text container this caption holds</param>
        public ImageCaption(params Leaf[] leaves) : this(leaves.ToList()) { }
        /// <summary>
        /// A message explaining an image.
        /// </summary>
        /// <param name="content">The contents of the caption</param>
        public ImageCaption(string content) : this(new TextContainer(content)) { }
        /// <summary>
        /// A message explaining an image.
        /// </summary>
        /// <param name="content">The contents of the caption</param>
        /// <param name="formatting">The formatting of the text</param>
        public ImageCaption(string content, params Mark[] formatting) : this(new TextContainer(content, formatting)) { }
        /// <summary>
        /// A message explaining an image.
        /// </summary>
        /// <param name="content">The contents of the caption</param>
        /// <param name="formatting">The formatting of the text</param>
        public ImageCaption(string content, params MarkType[] formatting) : this(new TextContainer(content, formatting)) { }
        /// <summary>
        /// A message explaining an image.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        public ImageCaption(string format, params object[] args) : this(string.Format(format, args)) { }
        /// <summary>
        /// A message explaining an image.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        public ImageCaption(IFormatProvider provider, string format, params object[] args) : this(string.Format(provider, format, args)) { }
        /// <summary>
        /// A message explaining an image.
        /// </summary>
        /// <param name="content">The contents that should be converted to text container</param>
        public ImageCaption(object content) : this(new TextContainer(content)) { }
        #endregion

        #region Overrides
        /// <summary>
        /// Returns caption's content as string.
        /// </summary>
        /// <returns>Caption as string</returns>
        public override string ToString() => $"| {string.Concat(Nodes)} |";
        #endregion
    }
}