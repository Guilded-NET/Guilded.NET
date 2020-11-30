using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Message posted in chat.
    /// </summary>
    public class Message: ClientObject, IMessage {
        /// <summary>
        /// Message posted in chat.
        /// </summary>
        public Message() =>
            (IsPinned, ChannelId) = (false, null);
        /// <summary>
        /// ID of the message.
        /// </summary>
        /// <value>Message ID</value>
        [JsonProperty("id", Required = Required.Always)]
        public Guid Id {
            get; set;
        }
        /// <summary>
        /// ID of the channel this message is in.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonProperty("channelId", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? ChannelId {
            get; set;
        }
        /// <summary>
        /// ID of the author.
        /// </summary>
        /// <value>Message ID</value>
        [JsonProperty("createdBy", Required = Required.Always)]
        public GId AuthorId {
            get; set;
        }
        /// <summary>
        /// Type of the message.
        /// </summary>
        /// <value>Message type</value>
        [JsonProperty("type")]
        public MessageType Type {
            get; set;
        }
        /// <summary>
        /// Date of when the message was posted.
        /// </summary>
        [JsonProperty("createdAt")]
        public DateTime CreatedAt {
            get; set;
        }
        /// <summary>
        /// Content of the message.
        /// </summary>
        /// <value>Message content</value>
        [JsonProperty("content", Required = Required.Always)]
        public MessageContent Content {
            get; set;
        }
        /// <summary>
        /// Type of the channel. Whether it was posted in DMs or in a Team.
        /// </summary>
        /// <value></value>
        [JsonProperty("channelType")]
        public ChatType ChannelType {
            get; set;
        }
        /// <summary>
        /// Whether or not this message was pinned.
        /// </summary>
        /// <value>Pinned</value>
        [JsonProperty("isPinned")]
        public bool IsPinned {
            get; set;
        }
        /// <summary>
        /// Gets message content nodes.
        /// </summary>
        /// <value>List of Nodes</value>
        [JsonIgnore]
        public IList<Node> Nodes {
            get => Content.Nodes;
        }
        /// <summary>
        /// Generates a new message.
        /// </summary>
        /// <param name="nodes">List of nodes</param>
        /// <returns>NewMessage</returns>
        public static NewMessage Generate(IList<Node> nodes) =>
            new NewMessage {
                // Generate random ID and give it to the message
                Id = Guid.NewGuid(),
                // Create content for the message
                Content = MessageContent.Generate(nodes)
            };
        /// <summary>
        /// Generates a new message using string.
        /// </summary>
        /// <param name="content">Content of the message</param>
        /// <returns>NewMessage</returns>
        public static NewMessage Generate(Node node) => Generate(new List<Node> { node });
        /// <summary>
        /// Generates a new message using string.
        /// </summary>
        /// <param name="content">Content of the message</param>
        /// <returns>NewMessage</returns>
        public static NewMessage Generate(string content) => Generate(MarkDownText.Generate(content));
        /// <summary>
        /// Generates a new message using embed.
        /// </summary>
        /// <param name="embed">Embed to send</param>
        /// <returns>NewMessage</returns>
        public static NewMessage Generate(Embed embed) => Generate(new List<Node> {EmbedNode.Generate(embed)});
        /// <summary>
        /// Turns a message into a string.
        /// </summary>
        /// <returns>Message as a string</returns>
        public override string ToString() => string.Concat(Nodes);
    }
}