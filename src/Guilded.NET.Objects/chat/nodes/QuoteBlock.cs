using System.Linq;

namespace Guilded.NET.Objects.Chat
{
    /// <summary>
    /// Represents Guilded's quote block node.
    /// </summary>
    public class QuoteBlock : ContainerNode<QuoteBlockLine>
    {
        /// <summary>
        /// Represents Guilded's quote block node.
        /// </summary>
        public QuoteBlock() =>
            Type = NodeType.BlockQuoteContainer;
        /// <summary>
        /// Generates quote block node.
        /// </summary>
        /// <param name="objs">List of text objects</param>
        /// <returns>Quote block node</returns>
        public static QuoteBlock Generate(params QuoteBlockLine[] objs) =>
            new QuoteBlock
            {
                Nodes = objs
            };
        /// <summary>
        /// Generates quote block node.
        /// </summary>
        /// <param name="objs">List of text objects</param>
        /// <returns>Quote block node</returns>
        public static QuoteBlock Generate(params TextObj[] objs) =>
            Generate(objs.Select(x => QuoteBlockLine.Generate(x)).ToArray());
        /// <summary>
        /// Generates quote block node.
        /// </summary>
        /// <param name="content">List of strings to generate quote block from</param>
        /// <returns>Quote block node</returns>
        public static QuoteBlock Generate(params string[] content) => Generate(content.Select(x => QuoteBlockLine.Generate(x)).ToArray());
        /// <summary>
        /// Generates quote block node from given leaves.
        /// </summary>
        /// <param name="leaves">List of leaves to generate quote block from</param>
        /// <returns>Quote block node</returns>
        public static QuoteBlock GenerateFromLeaves(params Leaf[] leaves) => Generate(TextObj.GenerateText(leaves));
        /// <summary>
        /// Turns quote block to string.
        /// </summary>
        /// <returns>Quote block as a string</returns>
        public override string ToString() => string.Join('\n', Nodes);
    }
}