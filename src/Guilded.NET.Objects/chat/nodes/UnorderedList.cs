using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Represents Guilded's bulleted/unordered list node.
    /// </summary>
    public class UnorderedList: ContainerNode<Node> {
        /// <summary>
        /// Represents Guilded's bulleted/unordered list node.
        /// </summary>
        public UnorderedList() =>
            (Object, Type) = (MsgObject.Block, NodeType.UnorderedList);
        /// <summary>
        /// Turns list to string.
        /// </summary>
        /// <returns>List as a string</returns>
        public override string ToString() => string.Join('\n', Nodes.Select((x, i) => $"- {string.Join("\n  ", x.ToString().Split('\n'))}"));
        /// <summary>
        /// Generates unordered(a.k.a. bulleted) list node.
        /// </summary>
        /// <param name="nodes">List of nodes</param>
        /// <returns>Unordered list node</returns>
        public static UnorderedList Generate(params Node[] nodes) =>
            new UnorderedList {
                // Sets its nodes
                Nodes = nodes
            };
        /// <summary>
        /// Generates unordered(a.k.a. bulleted) list node.
        /// </summary>
        /// <param name="objs">List of text objects</param>
        /// <returns>Unordered list node</returns>
        public static UnorderedList Generate(params TextObj[] objs) =>
            Generate(objs.Select(x => (Node)ListItem.Generate(x)).ToArray());
    }
}