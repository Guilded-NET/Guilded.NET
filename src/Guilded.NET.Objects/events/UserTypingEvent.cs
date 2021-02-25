using System;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Events {
    /// <summary>
    /// Event when message is posted in the chat.
    /// </summary>
    public class UserTypingEvent: Event {
        /// <summary>
        /// ID of the channel this message was posted in.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonProperty("channelId", Required = Required.Always)]
        public Guid ChannelId {
            get; set;
        }
        /// <summary>
        /// ID of the channel this message was posted in.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonProperty("userId", Required = Required.Always)]
        public GId UserId {
            get; set;
        }
    }
}