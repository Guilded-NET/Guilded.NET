using Newtonsoft.Json;

namespace Guilded.NET.Objects.Events
{
    using Teams;
    /// <summary>
    /// Event when group gets created.
    /// </summary>
    /// <value>TEAM_GROUP_CREATED</value>
    public class GroupCreatedEvent : ClientEvent
    {
        /// <summary>
        /// ID of the team group is in.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty("teamId", Required = Required.Always)]
        public GId TeamId
        {
            get; set;
        }
        /// <summary>
        /// Group's new information.
        /// </summary>
        /// <value>Group</value>
        [JsonProperty("group", Required = Required.Always)]
        public Group Group
        {
            get; set;
        }
    }
}