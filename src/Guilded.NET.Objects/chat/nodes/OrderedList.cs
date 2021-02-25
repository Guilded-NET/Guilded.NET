using System.Linq;

namespace Guilded.NET.Objects.Chat
{
    /// <summary>
    /// Represents Guilded's numbered/ordered list node.
    /// </summary>
    public class OrderedList : ContainerNode<Node>
    {
        /// <summary>
        /// Represents Guilded's numbered/ordered list node.
        /// </summary>
        public OrderedList() =>
            Type = NodeType.OrderedList;
        /// <summary>
        /// Turns list to string.
        /// </summary>
        /// <returns>List as a string</returns>
        public override string ToString() => string.Join('\n', Nodes.Select((x, i) =>
        {
            // Start of list items
            string start = $"{i + 1}. ";
            string startspace = string.Concat(Enumerable.Repeat(" ", start.Length));
            // Join it all together
            return $"{start}{string.Join('\n' + startspace, x.ToString().Split('\n'))}";
        }));
        /// <summary>
        /// Generates ordered(a.k.a. list with numbers) list node.
        /// </summary>
        /// <param name="nodes">List of nodes</param>
        /// <returns>Ordered list node</returns>
        public static OrderedList Generate(params Node[] nodes) =>
            new OrderedList
            {
                // Sets its nodes
                Nodes = nodes
            };
        /// <summary>
        /// Generates ordered(a.k.a. list with numbers) list node.
        /// </summary>
        /// <param name="objs">List of text objects</param>
        /// <returns>Ordered list node</returns>
        public static OrderedList Generate(params TextObj[] objs) =>
            Generate(objs.Select(x => (Node)ListItemNode.Generate(x)).ToArray());
    }
}