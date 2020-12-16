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
            (Type, Object) = (NodeType.ListItem, MsgObject.Block);
        /// <param name="objs">Text objects to create line from</param>
        public ListItem(params IMessageObject[] objs): this() => Nodes = objs.ToList();
        /// <summary>
        /// Turns list item to string.
        /// </summary>
        /// <returns>List item as a string</returns>
        public override string ToString() => string.Concat(Nodes);
    }
}