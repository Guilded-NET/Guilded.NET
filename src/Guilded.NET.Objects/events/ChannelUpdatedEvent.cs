using Newtonsoft.Json;

namespace Guilded.NET.Objects.Events {
    /// <summary>
    /// When channel gets updated in a specific team.
    /// </summary>
    public class ChannelUpdatedEvent: ClientEvent {
        /// <summary>
        /// ID of the team channel was updated in.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty("teamId", Required = Required.Always)]
        public GId TeamId {
            get; set;
        }
        /// <summary>
        /// How channel was updated.
        /// </summary>
        /// <value>Updated channel</value>
        [JsonProperty("channel", Required = Required.Always)]
        public ChannelUpdate Channel {
            get; set;
        }
    }
}