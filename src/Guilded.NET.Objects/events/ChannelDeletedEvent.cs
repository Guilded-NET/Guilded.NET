using Newtonsoft.Json;
using System;

namespace Guilded.NET.Objects.Events {
    using Teams;
    /// <summary>
    /// When channel gets deleted in a specific team.
    /// </summary>
    public class ChannelDeletedEvent: ClientEvent {
        /// <summary>
        /// ID of the team channel was deleted in.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty("teamId", Required = Required.Always)]
        public GId TeamId {
            get; set;
        }
        /// <summary>
        /// ID of the channel which was deleted.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonProperty("channel", Required = Required.Always)]
        public Guid Channel {
            get; set;
        }
    }
}