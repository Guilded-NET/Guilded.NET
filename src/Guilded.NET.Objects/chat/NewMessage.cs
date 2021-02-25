using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Chat
{
    /// <summary>
    /// Message posted in chat.
    /// </summary>
    public class NewMessage : BaseObject, IMessage
    {
        /// <summary>
        /// ID of the message.
        /// </summary>
        /// <value>Message ID</value>
        [JsonProperty("messageId", Required = Required.Always)]
        public Guid Id
        {
            get; set;
        }
        /// <summary>
        /// Content of the message.
        /// </summary>
        /// <value>Message content</value>
        [JsonProperty("content", Required = Required.Always)]
        public MessageContent Content
        {
            get; set;
        }
        /// <summary>
        /// Gets message content nodes.
        /// </summary>
        /// <value>List of Nodes</value>
        [JsonIgnore]
        public IList<Node> Nodes
        {
            get => Content?.Nodes;
        }
        /// <summary>
        /// I don't even know what this is.
        /// </summary>
        /// <value>Something</value>
        [JsonProperty("confirmed")]
        public bool Confirmed
        {
            get; set;
        } = false;
        /// <summary>
        /// Turns a message into a string.
        /// </summary>
        /// <returns>Message as a string</returns>
        public override string ToString() => Content?.ToString();
    }
}