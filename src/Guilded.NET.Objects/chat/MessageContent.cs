using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Content of the message.
    /// </summary>
    public class MessageContent: BaseObject, IMessageObject {
        /// <summary>
        /// Content of the message.
        /// </summary>
        public MessageContent() =>
            Object = MsgObject.Value;
        /// <summary>
        /// Object of the content.
        /// </summary>
        /// <value>Content object</value>
        [JsonProperty("object", Required = Required.Always)]
        public MsgObject Object {
            get; set;
        }
        /// <summary>
        /// Document of the message content.
        /// </summary>
        /// <value>Message document</value>
        [JsonProperty("document", Required = Required.Always)]
        public MessageDocument Document {
            get; set;
        }
        /// <summary>
        /// Gets message content nodes.
        /// </summary>
        /// <value>List of Nodes</value>
        [JsonIgnore]
        public IList<Node> Nodes {
            get => Document.Nodes;
        }
        /// <summary>
        /// Turns a message content into a string.
        /// </summary>
        /// <returns>Content as a string</returns>
        public override string ToString() => Document?.ToString();
        /// <summary>
        /// Generates message content. Used by Message.Generate and for editing messages.
        /// </summary>
        /// <param name="nodes">List of nodes</param>
        /// <returns>Message content</returns>
        public static MessageContent Generate(IList<Node> nodes) =>
            new MessageContent {
                // Create document for message content and then assign given nodes
                Document = new MessageDocument {
                    Nodes = nodes
                }
            };
        /// <summary>
        /// Generates message content. Used by Message.Generate and for editing messages.
        /// </summary>
        /// <param name="node">Node to generate content with</param>
        /// <returns>Message content</returns>
        public static MessageContent Generate(Node node) => Generate(new List<Node> { node });
        /// <summary>
        /// Generates message content. Used by Message.Generate and for editing messages.
        /// </summary>
        /// <param name="content">String to generate Markdown text plain from</param>
        /// <returns>Message content</returns>
        public static MessageContent Generate(string content) => Generate(MarkDownText.Generate(content));
        /// <summary>
        /// Generates message content. Used by Message.Generate and for editing messages.
        /// </summary>
        /// <param name="embed">Embed to generate message content from</param>
        /// <returns>Message content</returns>
        public static MessageContent Generate(Embed embed) => Generate(new List<Node> {EmbedNode.Generate(embed)});
    }
}