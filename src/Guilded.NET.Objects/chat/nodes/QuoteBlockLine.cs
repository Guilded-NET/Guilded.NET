using Newtonsoft.Json;
using System.Linq;

namespace Guilded.NET.Objects.Chat {
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
        /// Turns quote block to string.
        /// </summary>
        /// <returns>Quote block as a string</returns>
        public override string ToString() => $"> {string.Concat(Nodes)}";
    }
}