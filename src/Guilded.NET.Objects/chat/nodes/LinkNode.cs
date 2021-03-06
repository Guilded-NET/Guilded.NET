using System;

using Newtonsoft.Json.Linq;

namespace Guilded.NET.Objects.Chat
{
    /// <summary>
    /// Represents Guilded's link node.
    /// </summary>
    public class LinkNode : ContainerNode<IMessageObject>
    {
        /// <summary>
        /// Represents Guilded's link node.
        /// </summary>
        public LinkNode() =>
            (Type, Object) = (NodeType.Link, MsgObject.Inline);
        /// <summary>
        /// Turns link node to a string.
        /// </summary>
        /// <returns>Link node as a string</returns>
        public override string ToString() => $"[{base.ToString()}]({Data?["href"]})";
        /// <summary>
        /// Generates link node.
        /// </summary>
        /// <param name="href">Link which should be used by this node</param>
        /// <param name="objs">List of text objects</param>
        /// <returns>Link node</returns>
        public static LinkNode Generate(Uri href, params TextObj[] objs) =>
            new LinkNode
            {
                // Adds link to the link node
                Data = JObject.FromObject(new { href }),
                // Generate list of 1 text object with given leaves
                Nodes = objs
            };
        /// <summary>
        /// Generates link node.
        /// </summary>
        /// <param name="href">Link which should be used by this node</param>
        /// <param name="leaves">List of message leaves</param>
        /// <returns>Link node</returns>
        public static LinkNode Generate(Uri href, params Leaf[] leaves) =>
            Generate(href, TextObj.GenerateText(leaves));
    }
}