using Guilded.NET.Objects.Chat;
using Guilded.NET.Objects.Teams;
using Newtonsoft.Json;
using System;

namespace Guilded.NET.Objects.Events {
    /// <summary>
    /// Event when message gets deleted.
    /// </summary>
    public class MessageDeletedEvent: TeamEvent {
        /// <summary>
        /// Type of the content.
        /// </summary>
        /// <value>Content type</value>
        [JsonProperty("contentType")]
        public ChannelType ContentType {
            get; set;
        }
        /// <summary>
        /// Message which has been deleted.
        /// </summary>
        /// <value>Message deleted</value>
        [JsonProperty("message", Required = Required.Always)]
        public MessageDeleted Message {
            get; set;
        }
    }
}