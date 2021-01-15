using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Guilded.NET.Objects.Converters;

namespace Guilded.NET.Objects.Teams {
    /// <summary>
    /// Guilded team/guild/server.
    /// </summary>
    public class Team: BaseTeam {
        /// <summary>
        /// Team's "biography".
        /// </summary>
        /// <value>Biography</value>
        [JsonProperty("bio", Required = Required.AllowNull)]
        public string Bio {
            get; set;
        }
        /// <summary>
        /// Description of this team.
        /// </summary>
        /// <value>Description</value>
        [JsonProperty("description", Required = Required.AllowNull)]
        public string Description {
            get; set;
        }
        /// <summary>
        /// All of the members in the team.
        /// </summary>
        /// <value>List of members or list of list of members</value>
        [JsonConverter(typeof(FlatConverter))]
        [JsonProperty("members", Required = Required.Always)]
        public IList<TeamMember> Members {
            get; set;
        }
        /// <summary>
        /// Count of various things in this team.
        /// </summary>
        /// <value>Team measurements</value>
        [JsonProperty("measurements", Required = Required.Always)]
        public TeamMeasurements Measurements {
            get; set;
        }
        /// <summary>
        /// A list of all webhooks in this team.
        /// </summary>
        /// <value>List of webhooks</value>
        [JsonProperty("webhooks", Required = Required.Always)]
        public IList<Webhook> Webhooks {
            get; set;
        }
        /// <summary>
        /// If this user is following this team.
        /// </summary>
        /// <value>Following</value>
        [JsonProperty("userFollowsTeam", Required = Required.Always)]
        public bool IsFollowing {
            get; set;
        }
        /// <summary>
        /// If this user has applied for this server.
        /// </summary>
        /// <value>Applied</value>
        [JsonProperty("isUserApplicant", Required = Required.Always)]
        public bool IsApplicant {
            get; set;
        }
        /// <summary>
        /// If this user was invited.
        /// </summary>
        /// <value>Invited</value>
        [JsonProperty("isUserInvited", Required = Required.Always)]
        public bool IsInvited {
            get; set;
        }
        /// <summary>
        /// If this used was banned from this team.
        /// </summary>
        /// <value>Banned</value>
        [JsonProperty("isUserBannedFromTeam", Required = Required.Always)]
        public bool IsBanned {
            get; set;
        }
        /// <summary>
        /// If this team has imported a Discord server through Discord integration.
        /// </summary>
        /// <value>Imported Discord server</value>
        [JsonProperty("hasImportedDiscordServer", Required = Required.Always)]
        public bool ImportedDiscord {
            get; set;
        }
        /// <summary>
        /// If the team has a member with given ID.
        /// </summary>
        /// <param name="memberId">ID of the member</param>
        /// <returns>Has a member with given ID</returns>
        public bool HasMember(GId memberId) =>
            Members.FirstOrDefault(x => x.Id == memberId) != null;
    }
}