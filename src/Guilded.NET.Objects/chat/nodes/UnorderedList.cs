using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Represents Guilded's bulleted/unordered list node.
    /// </summary>
    public class UnorderedList: ContainerNode<ListItem> {
        public UnorderedList() {
            Object = MsgObject.Block;
            Type = NodeType.UnorderedList;
        }
        /// <summary>
        /// Generates unordered(a.k.a. bulleted) list node.
        /// </summary>
        /// <param name="nodes">List of text objects</param>
        /// <returns>Unordered list node</returns>
        public static UnorderedList Generate(params TextObj[] objs) =>
           new UnorderedList {
               // Generate list of 1 text object with given leaves
               Nodes = objs.Select(x => new ListItem(x)).ToList()
           };
        /// <summary>
        /// Turns list to string.
        /// </summary>
        /// <returns>List as a string</returns>
        public override string ToString() => string.Join('\n', Nodes.Select((x, i) => {
            // Join it all together
            return $"- {string.Join("\n  ", x.ToString().Split('\n'))}";
        }));
    }
}