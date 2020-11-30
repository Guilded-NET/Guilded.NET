using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Represents Guilded's quote block node.
    /// </summary>
    public class QuoteBlock: ContainerNode<QuoteBlockLine> {
        public QuoteBlock() {
            Object = MsgObject.Block;
            Type = NodeType.BlockQuoteContainer;
        }
        /// <summary>
        /// Generates quote block node.
        /// </summary>
        /// <param name="objs">List of text objects</param>
        /// <returns>Quote block node</returns>
        public static QuoteBlock Generate(params TextObj[] objs) =>
            new QuoteBlock {
                Nodes = objs.Select(x => QuoteBlockLine.Generate(x)).ToList()
            };
        /// <summary>
        /// Turns quote block to string.
        /// </summary>
        /// <returns>Quote block as a string</returns>
        public override string ToString() => string.Concat(Nodes);
    }
}