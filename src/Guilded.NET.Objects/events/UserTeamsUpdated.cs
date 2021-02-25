using System.ComponentModel;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Events {
    /// <summary>
    /// Event when this user joins, leaves, gets kicked or gets banned.
    /// </summary>
    public class UserTeamsUpdated: Event {
        /// <summary>
        /// ID of the team which updated user teams.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty("teamId", Required = Required.Always)]
        public GId TeamId {
            get; set;
        }
        /// <summary>
        /// If the user was removed/kicked from the team.
        /// </summary>
        /// <value>Removed</value>
        [JsonProperty("isRemoved")]
        [DefaultValue(false)]
        public bool IsRemoved {
            get; set;
        }
        /// <summary>
        /// If the user was banned from the team.
        /// </summary>
        /// <value>Banned from team</value>
        [JsonProperty("isUserBannedFromTeam")]
        [DefaultValue(false)]
        public bool IsBanned {
            get; set;
        }
    }
}