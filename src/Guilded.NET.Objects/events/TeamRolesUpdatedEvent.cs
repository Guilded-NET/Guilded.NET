using Newtonsoft.Json;
using System.Collections.Generic;

namespace Guilded.NET.Objects.Events {
    /// <summary>
    /// When member's role list gets updated.
    /// </summary>
    /// <value>teamRolesUpdated</value>
    public class TeamRolesUpdatedEvent: Event {
        /// <summary>
        /// ID of the team where member roles got updated.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty("teamId", Required = Required.Always)]
        public GId TeamId {
            get; set;
        }
        /// <summary>
        /// A list of member role updates.
        /// </summary>
        /// <value>List of member role updates</value>
        [JsonProperty("memberRoleIds", Required = Required.Always)]
        public IList<MemberRoleUpdate> MemberRoleIds {
            get; set;
        }
    }
}