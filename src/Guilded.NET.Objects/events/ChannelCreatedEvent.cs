using Newtonsoft.Json;

namespace Guilded.NET.Objects.Events {
    using Teams;
    /// <summary>
    /// When channel gets created in a specific team.
    /// </summary>
    public class ChannelCreatedEvent: ClientEvent {
        /// <summary>
        /// ID of the team channel was created in.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty("teamId", Required = Required.Always)]
        public GId TeamId {
            get; set;
        }
        /// <summary>
        /// Channel which was created.
        /// </summary>
        /// <value>Channel</value>
        [JsonProperty("channel", Required = Required.Always)]
        public Channel Channel {
            get; set;
        }
    }
}