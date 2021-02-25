using System.Linq;

namespace Guilded.NET.Objects.Chat
{
    /// <summary>
    /// Represents Guilded's paragraph node.
    /// </summary>
    public class ParagraphNode : ContainerNode<IMessageObject>
    {
        /// <summary>
        /// Represents Guilded's paragraph node.
        /// </summary>
        public ParagraphNode() =>
            Type = NodeType.Paragraph;
        /// <summary>
        /// Generates paragraph node.
        /// </summary>
        /// <param name="objs">List of text objects</param>
        /// <returns>Paragraph node</returns>
        public static ParagraphNode Generate(params IMessageObject[] objs) =>
            new ParagraphNode
            {
                Nodes = objs
            };
        /// <summary>
        /// Generates paragraph node from given string array.
        /// </summary>
        /// <param name="content">A list of strings to turn to text objects</param>
        /// <returns>Paragraph node</returns>
        public static ParagraphNode Generate(params string[] content) => Generate(content.Select(x => Leaf.Generate(x)).ToArray());
        /// <summary>
        /// Generates paragraph node from given leaves.
        /// </summary>
        /// <param name="leaves">List of message leaves</param>
        /// <returns>Paragraph node</returns>
        public static ParagraphNode GenerateFromLeaves(params Leaf[] leaves) => Generate(TextObj.GenerateText(leaves));
    }
}