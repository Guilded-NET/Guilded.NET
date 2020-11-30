using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Represents Guilded's paragraph node.
    /// </summary>
    public class MarkDownText: ContainerNode<IMessageObject> {
        public MarkDownText() {
            Object = MsgObject.Block;
            Type = NodeType.MarkdownPlainText;
        }
        /// <summary>
        /// Generates paragraph node.
        /// </summary>
        /// <param name="content">Message content</param>
        /// <returns>Markdown plain text node</returns>
        public static MarkDownText Generate(string content) =>
            new MarkDownText {
                // Set data to nothing, because paragraphs don't need anything
                Data = new Dictionary<string, object>(),
                // Generate list of 1 text object with 1 leaf
                Nodes = new List<IMessageObject> {
                    new TextObj {
                        Leaves = new List<Leaf> {
                            Leaf.Generate(content)
                        }
                    }
                }
            };
        /// <summary>
        /// Turns quote block to string.
        /// </summary>
        /// <returns>Quote block as a string</returns>
        public override string ToString() => string.Concat(Nodes);
    }
}