using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Represents Guilded's link node.
    /// </summary>
    public class LinkNode: ContainerNode<IMessageObject> {
        public LinkNode() {
            Object = MsgObject.Inline;
            Type = NodeType.Link;
        }
        /// <summary>
        /// Turns link node to a string.
        /// </summary>
        /// <returns>Link node as a string</returns>
        public override string ToString() => $"[{string.Concat(Nodes)}]({Data?["href"]})";
        /// <summary>
        /// Generates link node.
        /// </summary>
        /// <param name="leaves">List of message leaves</param>
        /// <returns>Link node</returns>
        public static LinkNode Generate(Uri url, params Leaf[] leaves) =>
            new LinkNode {
                // Adds link to the link node
                Data = new Dictionary<string, object> {
                    { "href", url.ToString() }
                },
                // Generate list of 1 text object with given leaves
                Nodes = new List<IMessageObject> {
                    new TextObj {
                        Leaves = leaves.ToList(),
                        Object = MsgObject.Text
                    }
                }
            };
        /// <summary>
        /// Generates link node.
        /// </summary>
        /// <param name="objs">List of text objects</param>
        /// <returns>Link node</returns>
        public static LinkNode Generate(Uri url, params TextObj[] objs) =>
            new LinkNode {
                // Adds link to the link node
                Data = new Dictionary<string, object> {
                    { "href", url.ToString() }
                },
                // Generate list of 1 text object with given leaves
                Nodes = objs.Select(x => (IMessageObject)x).ToList()
            };
    }
}