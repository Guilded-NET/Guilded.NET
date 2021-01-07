using Guilded.NET.Objects.Chat;
using Guilded.NET.Objects.Teams;
using Newtonsoft.Json;
using System;

namespace Guilded.NET.Objects.Events {
    /// <summary>
    /// Event when message gets updated.
    /// </summary>
    public class MessageUpdatedEvent: CommonEvent {
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
        /// <summary>
        /// If message was updated by given user.
        /// </summary>
        /// <param name="user">User to check if it's message update author</param>
        /// <returns>Message updated by that user</returns>
        public bool WasUpdatedBy(User user) =>
            UpdatedBy == user?.Id;
    }
}