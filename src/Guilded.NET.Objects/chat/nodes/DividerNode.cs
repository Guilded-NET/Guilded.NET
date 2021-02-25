using System.Collections.Generic;

namespace Guilded.NET.Objects.Chat
{
    /// <summary>
    /// Represents Guilded's divider node.
    /// </summary>
    public class DividerNode : ContainerNode<IMessageObject>
    {
        /// <summary>
        /// Represents Guilded's divider node.
        /// </summary>
        public DividerNode() =>
            Type = NodeType.Divider;
        /// <summary>
        /// Generates divider node.
        /// </summary>
        /// <returns>Divider node</returns>
        public static DividerNode Generate() =>
            new DividerNode
            {
                // If it's a list, don't turn it to list again
                Nodes = new List<IMessageObject> {
                    TextObj.GenerateText("")
                }
            };
        /// <summary>
        /// Turns divider node to string.
        /// </summary>
        /// <returns>Divider as a string</returns>
        public override string ToString() => "---";
    }
}