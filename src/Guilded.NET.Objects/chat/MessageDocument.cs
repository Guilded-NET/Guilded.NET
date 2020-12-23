using Newtonsoft.Json;
using System.Collections.Generic;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Document of the message content.
    /// </summary>
    public class MessageDocument: BaseObject, IMessageObject {
        /// <summary>
        /// List of nodes in message content document.
        /// </summary>
        /// <value>Document nodes</value>
        [JsonProperty("nodes", Required = Required.Always)]
        public IList<Node> Nodes {
            get; set;
        }
        /// <summary>
        /// Object of the content.
        /// </summary>
        /// <value>Content object</value>
        [JsonProperty("object", Required = Required.Always)]
        public MsgObject Object {
            get; set;
        } = MsgObject.Document;
        /// <summary>
        /// Data of the document.
        /// </summary>
        /// <value>Document data</value>
        [JsonProperty("data")]
        public IDictionary<string, object> Data {
            get; set;
        }
        /// <summary>
        /// Turns a message document into a string.
        /// </summary>
        /// <returns>Document as a string</returns>
        public override string ToString() => string.Join('\n', Nodes);
    }
}