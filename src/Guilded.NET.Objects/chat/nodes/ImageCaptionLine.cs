using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// A caption under an image.
    /// </summary>
    public class ImageCaption: ContainerNode<IMessageObject> {
        /// <summary>
        /// A caption under an image.
        /// </summary>
        public ImageCaption() =>
            Type = NodeType.ImageCaptionLine;
        /// <summary>
        /// Turns an image caption node to string.
        /// </summary>
        /// <returns>Image caption as a string</returns>
        public override string ToString() => $"| {string.Concat(Nodes)} |";
        /// <summary>
        /// Generates an image caption node.
        /// </summary>
        /// <param name="objs">List of message objects</param>
        /// <returns>Image caption node</returns>
        public static ParagraphNode Generate(params IMessageObject[] objs) =>
            new ParagraphNode {
                Nodes = objs
            };
        /// <summary>
        /// Generates an image caption node.
        /// </summary>
        /// <param name="leaves">List of leaves</param>
        /// <returns>Image caption node</returns>
        public static ParagraphNode Generate(params Leaf[] leaves) => Generate(TextObj.GenerateText(leaves));
    }
}