using Newtonsoft.Json;
using System;

namespace Guilded.NET.Objects.Events {
    /// <summary>
    /// Event when message is posted in the chat.
    /// </summary>
    public class UserTypingEvent: Event {
        /// <summary>
        /// ID of the channel this message was posted in.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonProperty("channelId")]
        public Guid ChannelId {
            get; set;
        }
        /// <summary>
        /// ID of the channel this message was posted in.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonProperty("userId")]
        public GId UserId {
            get; set;
        }
    }
}