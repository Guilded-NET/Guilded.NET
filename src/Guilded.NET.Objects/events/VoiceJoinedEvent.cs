using Newtonsoft.Json;

namespace Guilded.NET.Objects.Events {
    using Teams;
    /// <summary>
    /// When user joins or leaves a voice or stream channel.
    /// </summary>
    public class VoiceUpdatedEvent: TeamEvent {
        /// <summary>
        /// Type of the channel
        /// </summary>
        /// <value>Channel type</value>
        [JsonProperty("contentType", Required = Required.Always)]
        public ChannelType ContentType {
            get; set;
        }
        /// <summary>
        /// User which joined the voice or stream channel.
        /// </summary>
        /// <value>With ID: User ID</value>
        [JsonProperty("user", Required = Required.Always)]
        public WithId<GId> User {
            get; set;
        }
    }
}