using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Represents Guilded's quote block node.
    /// </summary>
    public class QuoteBlock: ContainerNode<QuoteBlockLine> {
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
            new QuoteBlock {
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
        /// Turns quote block to string.
        /// </summary>
        /// <returns>Quote block as a string</returns>
        public override string ToString() => string.Join('\n', Nodes);
    }
}