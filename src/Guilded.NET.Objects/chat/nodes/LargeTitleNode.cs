using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Represents Guilded's large heading node.
    /// </summary>
    public class LargeTitleNode: ContainerNode<IMessageObject> {
        /// <summary>
        /// Represents Guilded's large heading node.
        /// </summary>
        public LargeTitleNode() =>
            Type = NodeType.HeadingLarge;
        /// <summary>
        /// Generates large heading node.
        /// </summary>
        /// <param name="objs">List of text objects</param>
        /// <returns>Large heading node</returns>
        public static LargeTitleNode Generate(params IMessageObject[] objs) =>
            new LargeTitleNode {
                Nodes = objs
            };
        /// <summary>
        /// Generates large heading node.
        /// </summary>
        /// <param name="leaves">List of message leaves</param>
        /// <returns>Large heading node</returns>
        public static LargeTitleNode Generate(params Leaf[] leaves) => Generate((IMessageObject[])leaves);
        /// <summary>
        /// Generates large heading node.
        /// </summary>
        /// <param name="objs">List of text objects</param>
        /// <returns>Large heading node</returns>
        public static LargeTitleNode Generate(params TextObj[] objs) => Generate((IMessageObject[])objs);
        /// <summary>
        /// Turns large heading node to string.
        /// </summary>
        /// <returns>Large heading node</returns>
        public override string ToString() => $"# {string.Concat(Nodes)}";
    }
}