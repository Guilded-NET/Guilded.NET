using Newtonsoft.Json;

namespace Guilded.NET.Objects.Events {
    using Teams;
    /// <summary>
    /// When a channel gets seen by the client.
    /// </summary>
    public class ChannelSeenEvent: CommonEvent {
        /// <summary>
        /// If all notifications got cleared in that channel.
        /// </summary>
        /// <value>Cleared all notifications</value>
        [JsonProperty("clearAllBadges", Required = Required.Always)]
        public bool ClearAllBadges {
            get; set;
        }
        /// <summary>
        /// Channel type where the notifications were cleared.
        /// </summary>
        /// <value>Channel type</value>
        [JsonProperty("contentType", Required = Required.Always)]
        public ChannelType ContentType {
            get; set;
        }
    }
}