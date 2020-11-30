using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Node which contains other nodes or <see cref="IMessageObject"/>.
    /// </summary>
    public class ContainerNode<T>: Node where T: IMessageObject {
        /// <summary>
        /// List of inner nodes.
        /// </summary>
        /// <value>List of IMessageObject</value>
        [JsonProperty("nodes")]
        public IList<T> Nodes {
            get; set;
        } = new List<T>();
    }
}