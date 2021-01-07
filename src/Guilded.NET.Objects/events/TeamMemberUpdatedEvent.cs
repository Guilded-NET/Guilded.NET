using Newtonsoft.Json;

namespace Guilded.NET.Objects.Events {
    /// <summary>
    /// When a member in a team gets updated.
    /// </summary>
    /// <value>TeamMemberUpdated</value>
    public class TeamMemberUpdatedEvent: ClientEvent {
        /// <summary>
        /// User who has been updated.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("userId", Required = Required.Always)]
        public GId UserId {
            get; set;
        }
        /// <summary>
        /// Who updated this user.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("updatedBy", Required = Required.Always)]
        public GId UpdatedBy {
            get; set;
        }
        /// <summary>
        /// In which team this user was updated.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty("teamId", Required = Required.Always)]
        public GId TeamId {
            get; set;
        }
        /// <summary>
        /// How this user was updated.
        /// </summary>
        /// <value>Member update</value>
        [JsonProperty("userInfo", Required = Required.Always)]
        public MemberUpdated UserInfo {
            get; set;
        }
    }
}