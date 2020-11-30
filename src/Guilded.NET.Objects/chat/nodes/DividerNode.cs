using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Represents Guilded's divider node.
    /// </summary>
    public class DividerNode: ContainerNode<IMessageObject> {
        public DividerNode() {
            Object = MsgObject.Block;
            Type = NodeType.Divider;
        }
        /// <summary>
        /// Generates divider node.
        /// </summary>
        /// <returns>Divider node</returns>
        public static DividerNode Generate() =>
            new DividerNode {
                // Set data to nothing, because dividers don't need anything
                Data = new Dictionary<string, object>(),
                // If it's a list, don't turn it to list again
                Nodes = new List<IMessageObject> {
                    TextObj.GenerateText("")
                }
            };
        /// <summary>
        /// Turns divider node to string.
        /// </summary>
        /// <returns>Divider as a string</returns>
        public override string ToString() => "---\n";
    }
}