using Newtonsoft.Json;

namespace Guilded.NET.Objects.Events {
    using Teams;
    /// <summary>
    /// Event when group gets updated.
    /// </summary>
    /// <value>TEAM_GROUP_UPDATED</value>
    public class GroupUpdatedEvent: ClientEvent {
        /// <summary>
        /// ID of the team group is in.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty("teamId", Required = Required.Always)]
        public GId TeamId {
            get; set;
        }
        /// <summary>
        /// ID of the group updated.
        /// </summary>
        /// <value>Group ID</value>
        [JsonProperty("groupId", Required = Required.Always)]
        public GId GroupId {
            get; set;
        }
        /// <summary>
        /// Group's new information.
        /// </summary>
        /// <value>Group</value>
        [JsonProperty("group", Required = Required.Always)]
        public Group Group {
            get; set;
        }
    }
}