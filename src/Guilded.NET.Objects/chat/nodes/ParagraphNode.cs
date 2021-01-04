using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Represents Guilded's paragraph node.
    /// </summary>
    public class ParagraphNode: ContainerNode<IMessageObject> {
        /// <summary>
        /// Represents Guilded's paragraph node.
        /// </summary>
        public ParagraphNode() =>
            Type = NodeType.Paragraph;
        /// <summary>
        /// Turns paragraph node to string.
        /// </summary>
        /// <returns>Paragraph as a string</returns>
        public override string ToString() => string.Concat(Nodes);
        /// <summary>
        /// Generates paragraph node.
        /// </summary>
        /// <param name="objs">List of text objects</param>
        /// <returns>Paragraph node</returns>
        public static ParagraphNode Generate(params IMessageObject[] objs) =>
            new ParagraphNode {
                // If it's a list, don't turn it to list again
                Nodes = objs
            };
        /// <summary>
        /// Generates paragraph node.
        /// </summary>
        /// <param name="leaves">List of message leaves</param>
        /// <returns>Paragraph node</returns>
        public static ParagraphNode Generate(params Leaf[] leaves) => Generate(TextObj.GenerateText(leaves));
    }
}