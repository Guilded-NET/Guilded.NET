using System.Linq;

namespace Guilded.NET.Objects.Chat
{
    /// <summary>
    /// Line of quote block in Guilded.
    /// </summary>
    public class QuoteBlockLine: ContainerNode<IMessageObject> {
        /// <summary>
        /// Line of quote block in Guilded.
        /// </summary>
        public QuoteBlockLine() =>
            Type = NodeType.BlockQuoteLine;
        /// <summary>
        /// Generates quote block line.
        /// </summary>
        /// <param name="objs">Text objects to create line from</param>
        /// <returns>Quote block line</returns>
        public static QuoteBlockLine Generate(params IMessageObject[] objs) =>
            new QuoteBlockLine {
                Nodes = objs
            };
        /// <summary>
        /// Generates quote block line.
        /// </summary>
        /// <param name="content">Strings to create line from</param>
        /// <returns>Quote block line</returns>
        public static QuoteBlockLine Generate(params string[] content) => Generate(content.Select(x => Leaf.Generate(x)).ToArray());
        /// <summary>
        /// Generates quote block line.
        /// </summary>
        /// <param name="leaves">Leaves to create line from</param>
        /// <returns>Quote block line</returns>
        public static QuoteBlockLine GenerateFromLeaves(params Leaf[] leaves) => Generate(TextObj.GenerateText(leaves));
        /// <summary>
        /// Turns quote block to string.
        /// </summary>
        /// <returns>Quote block as a string</returns>
        public override string ToString() => $"> {string.Concat(Nodes)}";
    }
}