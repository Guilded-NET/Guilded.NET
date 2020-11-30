using Guilded.NET.Objects.Chat;
using Guilded.NET.Objects.Teams;
using Newtonsoft.Json;
using System;

namespace Guilded.NET.Objects.Events {
    /// <summary>
    /// Event when message gets updated.
    /// </summary>
    public class MessageUpdatedEvent: TeamEvent {
        /// <summary>
        /// Type of the content.
        /// </summary>
        /// <value>Content type</value>
        [JsonProperty("contentType")]
        public ChannelType ContentType {
            get; set;
        }
        /// <summary>
        /// Message which has been updated.
        /// </summary>
        /// <value>Message updated</value>
        [JsonProperty("message", Required = Required.Always)]
        public MessageUpdated Message {
            get; set;
        }
        /// <summary>
        /// Who updated this message.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("updatedBy", Required = Required.Always)]
        public GId UpdatedBy {
            get; set;
        }
        /// <summary>
        /// ID of the message.
        /// </summary>
        /// <value>Message ID</value>
        [JsonProperty("contentId")]
        public Guid ContentId {
            get; set;
        }
    }
}