using Newtonsoft.Json;

namespace Guilded.NET.Objects.Events {
    /// <summary>
    /// Event when group gets deleted, archived or restored.
    /// </summary>
    /// <value>TeamGroupDeleted, TeamGroupArchived, TeamGroupRestored</value>
    public class TeamGroupEvent: Event {
        /// <summary>
        /// ID of the team group is in.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty("teamId", Required = Required.Always)]
        public GId TeamId {
            get; set;
        }
        /// <summary>
        /// ID of the deleted/archived/restored group.
        /// </summary>
        /// <value>Group ID</value>
        [JsonProperty("groupId", Required = Required.Always)]
        public GId GroupId {
            get; set;
        }
    }
}