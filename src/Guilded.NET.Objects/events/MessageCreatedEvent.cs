using Newtonsoft.Json;
using System;

namespace Guilded.NET.Objects.Events {
    using Chat;
    using Teams;
    /// <summary>
    /// Event when message is posted in the chat.
    /// </summary>
    public class MessageCreatedEvent: TeamEvent {
        /// <summary>
        /// Type of the content.
        /// </summary>
        /// <value>Content type</value>
        [JsonProperty("contentType")]
        public ChannelType ContentType {
            get; set;
        }
        /// <summary>
        /// The message which was posted.
        /// </summary>
        /// <value>Message</value>
        [JsonProperty("message")]
        public Message Message {
            get; set;
        }
    }
}