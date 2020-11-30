using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Represents Guilded's paragraph node.
    /// </summary>
    public class ParagraphNode: ContainerNode<IMessageObject> {
        public ParagraphNode() {
            Object = MsgObject.Block;
            Type = NodeType.Paragraph;
        }
        /// <summary>
        /// Gets all leaves in paragraph.
        /// </summary>
        /// <value>List of paragraph leaves</value>
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
        /// Generates paragraph node.
        /// </summary>
        /// <param name="objs">List of text objects</param>
        /// <returns>Paragraph node</returns>
        public static ParagraphNode Generate(IEnumerable<IMessageObject> objs) =>
            new ParagraphNode {
                // Set data to nothing, because paragraphs don't need anything
                Data = new Dictionary<string, object>(),
                // If it's a list, don't turn it to list again
                Nodes = objs is IList<IMessageObject> list ? list : objs.ToList()
            };
        /// <summary>
        /// Generates paragraph node.
        /// </summary>
        /// <param name="objs">List of text objects</param>
        /// <returns>Paragraph node</returns>
        public static ParagraphNode Generate(params IMessageObject[] objs) => Generate((IEnumerable<IMessageObject>)objs);
        /// <summary>
        /// Generates paragraph node.
        /// </summary>
        /// <param name="leaves">List of message leaves</param>
        /// <returns>Paragraph node</returns>
        public static ParagraphNode Generate(params Leaf[] leaves) => Generate((IEnumerable<IMessageObject>)leaves);
        /// <summary>
        /// Generates paragraph node.
        /// </summary>
        /// <param name="objs">List of text objects</param>
        /// <returns>Paragraph node</returns>
        public static ParagraphNode Generate(params TextObj[] objs) => Generate((IEnumerable<IMessageObject>)objs);
        /// <summary>
        /// Turns paragraph node to string.
        /// </summary>
        /// <returns>Paragraph as a string</returns>
        public override string ToString() => string.Concat(Nodes) + '\n';
    }
}