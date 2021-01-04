using Newtonsoft.Json;
using System.Linq;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// List item in Guilded.
    /// </summary>
    public class ListItem: ContainerNode<IMessageObject> {
        /// <summary>
        /// List item in Guilded.
        /// </summary>
        public ListItem() =>
            Type = NodeType.ListItem;
        /// <summary>
        /// List item in Guilded.
        /// </summary>
        /// <param name="objs">Text objects to create line from</param>
        /// <returns>New ListItem</returns>
        public static ListItem Generate(params IMessageObject[] objs) =>
            new ListItem {
                Nodes = objs
            };
    }
}