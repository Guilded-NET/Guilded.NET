using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Chat
{
    /// <summary>
    /// Node which contains other nodes or <see cref="IMessageObject"/>.
    /// </summary>
    public abstract class ContainerNode<T> : Node where T : IMessageObject
    {
        /// <summary>
        /// Node which contains other nodes or <see cref="IMessageObject"/>.
        /// </summary>
        public ContainerNode() =>
            (Object, Nodes) = (MsgObject.Block, new List<T>());
        /// <summary>
        /// List of inner nodes.
        /// </summary>
        /// <value>List of IMessageObject</value>
        [JsonProperty("nodes")]
        public IList<T> Nodes
        {
            get; set;
        }
        /// <summary>
        /// Turns a container node to string
        /// </summary>
        /// <returns>Container node as a string</returns>
        public override string ToString() => string.Concat(Nodes);
    }
}