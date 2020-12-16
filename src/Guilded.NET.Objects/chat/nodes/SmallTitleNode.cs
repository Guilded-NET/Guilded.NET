using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Represents Guilded's small heading node.
    /// </summary>
    public class SmallTitleNode: ContainerNode<IMessageObject> {
        /// <summary>
        /// Represents Guilded's small heading node.
        /// </summary>
        public SmallTitleNode() =>
            (Object, Type) = (MsgObject.Block, NodeType.HeadingSmall);
        /// <summary>
        /// Gets all leaves in SmallTitle.
        /// </summary>
        /// <value>List of SmallTitle leaves</value>
        [JsonIgnore]
        public IEnumerable<Leaf> Leaves {
            get =>
                // Get all text objects and link nodes, because others shouldn't be there
                Nodes.Where(x => !(x is TextObj) && !(x is LinkNode)).Select(x =>
                    x is LinkNode xl
                    // Get all leaves in link node
                    ? xl.Nodes.Select(y =>
                        y is TextObj yt
                        ? yt.Leaves
                        : new List<Leaf>()
                    // Flatten the list
                    ).SelectMany(x => x)
                    // Else, get all text object leaves
                    : ((TextObj)x).Leaves 
                // Flatten the enumerable
                ).SelectMany(x => x);
        }
        /// <summary>
        /// Generates small heading node.
        /// </summary>
        /// <param name="objs">List of text objects</param>
        /// <returns>Small heading node</returns>
        public static SmallTitleNode Generate(IEnumerable<IMessageObject> objs) =>
            new SmallTitleNode {
                // Set data to nothing, because SmallTitles don't need anything
                Data = new Dictionary<string, object>(),
                // If it's a list, don't turn it to list again
                Nodes = objs is IList<IMessageObject> list ? list : objs.ToList()
            };
        /// <summary>
        /// Generates small heading node.
        /// </summary>
        /// <param name="objs">List of text objects</param>
        /// <returns>Small heading node</returns>
        public static SmallTitleNode Generate(params IMessageObject[] objs) => Generate((IEnumerable<IMessageObject>)objs);
        /// <summary>
        /// Generates small heading node.
        /// </summary>
        /// <param name="leaves">List of message leaves</param>
        /// <returns>Small heading node</returns>
        public static SmallTitleNode Generate(params Leaf[] leaves) => Generate((IEnumerable<IMessageObject>)leaves);
        /// <summary>
        /// Generates small heading node.
        /// </summary>
        /// <param name="objs">List of text objects</param>
        /// <returns>Small heading node</returns>
        public static SmallTitleNode Generate(params TextObj[] objs) => Generate((IEnumerable<IMessageObject>)objs);
        /// <summary>
        /// Turns small heading node to string.
        /// </summary>
        /// <returns>Small heading node</returns>
        public override string ToString() => $"## {string.Concat(Nodes)}";
    }
}