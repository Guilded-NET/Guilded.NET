using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Represents Guilded's numbered/ordered list node.
    /// </summary>
    public class OrderedList: ContainerNode<Node> {
        /// <summary>
        /// Represents Guilded's numbered/ordered list node.
        /// </summary>
        public OrderedList() =>
            (Object, Type) = (MsgObject.Block, NodeType.OrderedList);
        /// <summary>
        /// Generates ordered(a.k.a. list with numbers) list node.
        /// </summary>
        /// <param name="objs">List of text objects</param>
        /// <returns>Ordered list node</returns>
        public static OrderedList Generate(params TextObj[] objs) =>
            new OrderedList {
                // Set data to nothing, because lists don't need anything
                Data = new Dictionary<string, object>(),
                // Generate list of 1 text object with given leaves
                Nodes = objs.Select(x => (Node)new ListItem(x)).ToList()
            };
        /// <summary>
        /// Generates ordered(a.k.a. list with numbers) list node.
        /// </summary>
        /// <param name="nodes">List of nodes</param>
        /// <returns>Ordered list node</returns>
        public static OrderedList Generate(params Node[] nodes) =>
            new OrderedList {
                // Set data to nothing, because lists don't need anything
                Data = new Dictionary<string, object>(),
                // Sets its nodes
                Nodes = nodes.ToList()
            };
        /// <summary>
        /// Turns list to string.
        /// </summary>
        /// <returns>List as a string</returns>
        public override string ToString() => string.Join('\n', Nodes.Select((x, i) => {
            // Start of list items
            string start = $"{i + 1}. ";
            string startspace = string.Concat(Enumerable.Repeat(" ", start.Length));
            // Join it all together
            return $"{start}{string.Join('\n' + startspace, x.ToString().Split('\n'))}";
        }));
    }
}