using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// A node in a rich text markup tree.
    /// </summary>
    /// <seealso cref="BlockQuote"/>
    /// <seealso cref="ChatEmbed"/>
    /// <seealso cref="ChatEmote"/>
    /// <seealso cref="ChatList"/>
    /// <seealso cref="CodeContainer"/>
    /// <seealso cref="Form"/>
    /// <seealso cref="Heading"/>
    /// <seealso cref="Image"/>
    /// <seealso cref="MarkdownText"/>
    /// <seealso cref="Paragraph"/>
    /// <seealso cref="ContainerNode{T}"/>
    public abstract class Node : ChatElement
    {
        /// <summary>
        /// Type of the node.
        /// </summary>
        /// <value>Node type</value>
        [JsonProperty(Required = Required.Always)]
        public NodeType Type
        {
            get; set;
        }
        /// <summary>
        /// Data of this node.
        /// </summary>
        /// <value>Node data</value>
        public ElementData Data
        {
            get; set;
        } = new ElementData();
        /// <summary>
        /// Represents a node part of message content tree.
        /// </summary>
        /// <param name="type">The type of node it is</param>
        /// <param name="obj">The type of the chat element</param>
        /// <param name="data">The data containing information about the element</param>
        protected Node(NodeType type, ElementType obj, ElementData data) : base(obj) =>
            (Type, Object, Data) = (type, obj, data);
        /// <summary>
        /// Represents a node part of message content tree.
        /// </summary>
        /// <param name="type">The type of node it is</param>
        /// <param name="obj">The type of the chat element</param>
        protected Node(NodeType type, ElementType obj = ElementType.Block) : this(type, obj, new ElementData()) { }
    }
}