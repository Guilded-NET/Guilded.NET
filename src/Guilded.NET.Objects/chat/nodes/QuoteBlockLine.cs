using Newtonsoft.Json;
using System.Linq;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Line of quote block in Guilded.
    /// </summary>
    public class QuoteBlockLine: ContainerNode<IMessageObject> {
        public QuoteBlockLine() {
            Type = NodeType.BlockQuoteLine;
            Object = MsgObject.Block;
        }
        /// <summary>
        /// Generates quote block line.
        /// </summary>
        /// <param name="objs">Text objects to create line from</param>
        /// <returns>Quote block line</returns>
        public static QuoteBlockLine Generate(params IMessageObject[] objs) =>
            new QuoteBlockLine {
                Nodes = objs.ToList()
            };
        /// <summary>
        /// Turns quote block to string.
        /// </summary>
        /// <returns>Quote block as a string</returns>
        public override string ToString() => $"> {string.Concat(Nodes)}\n";
    }
}