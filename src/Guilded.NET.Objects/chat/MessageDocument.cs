using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Objects.Chat
{
    /// <summary>
    /// Document of the message content.
    /// </summary>
    public class MessageDocument : BaseObject, IMessageObject
    {
        /// <summary>
        /// Document of the message content.
        /// </summary>
        public MessageDocument() =>
            (Object, Data) = (MsgObject.Document, JObject.Parse("{}"));
        /// <summary>
        /// List of nodes in message content document.
        /// </summary>
        /// <value>Document nodes</value>
        [JsonProperty("nodes", Required = Required.Always)]
        public IList<Node> Nodes
        {
            get; set;
        }
        /// <summary>
        /// Object of the content.
        /// </summary>
        /// <value>Content object</value>
        [JsonProperty("object", Required = Required.Always)]
        public MsgObject Object
        {
            get; set;
        }
        /// <summary>
        /// Data of this message document.
        /// </summary>
        /// <value>Message document data</value>
        [JsonProperty("data", Required = Required.Always)]
        public JObject Data
        {
            get; set;
        }
        /// <summary>
        /// Turns a message document into a string.
        /// </summary>
        /// <returns>Document as a string</returns>
        public override string ToString() => string.Join('\n', Nodes);
    }
}