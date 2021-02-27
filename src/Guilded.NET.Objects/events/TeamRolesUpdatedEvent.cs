using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Events
{
    using Teams;
    /// <summary>
    /// When member's role list gets updated.
    /// </summary>
    /// <value>teamRolesUpdated</value>
    public class TeamRolesUpdatedEvent : Event
    {
        /// <summary>
        /// ID of the team where member roles got updated.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty("teamId", Required = Required.Always)]
        public GId TeamId
        {
            get; set;
        }
        /// <summary>
        /// A list of member role updates.
        /// </summary>
        /// <value>List of member role updates</value>
        [JsonProperty("memberRoleIds")]
        public IList<MemberRoleUpdate> MemberRoleIds
        {
            get; set;
        } = null;
        /// <summary>
        /// A new info of roles.
        /// </summary>
        /// <value>Roles by ID</value>
        [JsonProperty("rolesById")]
        public IDictionary<string, TeamRole> RolesById
        {
            get; set;
        } = null;
    }
}