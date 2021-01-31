using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Teams {
    /// <summary>
    /// Represents Guilded team group/subserver.
    /// </summary>
    public class Group: ClientObject {
        /// <summary>
        /// Represents Guilded team group/subserver.
        /// </summary>
        public Group() =>
            (MembershipUpdates, MembershipUpdatesByUserId, AdditionalMembershipRoles, AdditionalVisibilityRoles, UserPriority) = (new List<Membership>(), new Dictionary<uint, MembershipType>(), new uint[] {}, new uint[] {}, null);
        /// <summary>
        /// ID of this group.
        /// </summary>
        /// <value>Group ID</value>
        [JsonProperty("id", Required = Required.Always)]
        public GId Id {
            get; set;
        }
        /// <summary>
        /// Name of the group.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty("name", Required = Required.Always)]
        public string Name {
            get; set;
        }
        /// <summary>
        /// A description of a team.
        /// </summary>
        /// <value>Nullable string</value>
        [JsonProperty("description", Required = Required.AllowNull)]
        public string Description {
            get; set;
        }
        /// <summary>
        /// Priority of the group.
        /// </summary>
        /// <value>Priority</value>
        [JsonProperty("priority", Required = Required.AllowNull)]
        public long? Priority {
            get; set;
        }
        /// <summary>
        /// A type of this group.
        /// </summary>
        /// <value>Group type</value>
        [JsonProperty("type", Required = Required.Always)]
        public GroupType Type {
            get; set;
        }
        /// <summary>
        /// Icon of this group.
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("avatar", Required = Required.AllowNull)]
        public Uri Avatar {
            get; set;
        }
        /// <summary>
        /// Banner of this group. Currently unused by Guilded. Potential feature?
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("banner", Required = Required.AllowNull)]
        public Uri Banner {
            get; set;
        }
        /// <summary>
        /// ID of the team this group is in.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty("teamId", Required = Required.Always)]
        public GId TeamId {
            get; set;
        }
        /// <summary>
        /// ID of the game in this group.
        /// </summary>
        /// <value>Game ID</value>
        [JsonProperty("gameId", Required = Required.AllowNull)]
        public uint? GameId {
            get; set;
        }
        /// <summary>
        /// User membership updates in this group.
        /// </summary>
        /// <value>List of Membership Updates</value>
        [JsonProperty("membershipUpdates")]
        public IList<Membership> MembershipUpdates {
            get; set;
        }
        /// <summary>
        /// User membership updates in this group by user ID.
        /// </summary>
        /// <value>Uint, Membership type</value>
        [JsonProperty("membershipUpdatesByUserId")]
        public IDictionary<uint, MembershipType> MembershipUpdatesByUserId {
            get; set;
        }
        /// <summary>
        /// Additional roles which have a membership to this group.
        /// </summary>
        /// <value>Role ID list</value>
        [JsonProperty("additionalMembershipTeamRoleIds")]
        public IList<uint> AdditionalMembershipRoles {
            get; set;
        }
        /// <summary>
        /// Additional roles which can see this group.
        /// </summary>
        /// <value>Role ID list</value>
        [JsonProperty("additionalVisibilityTeamRoleIds")]
        public IList<uint> AdditionalVisibilityRoles {
            get; set;
        }
        /// <summary>
        /// A role which has a membership to this group.
        /// </summary>
        /// <value>Role ID</value>
        [JsonProperty("membershipTeamRoleId", Required = Required.AllowNull)]
        public uint? MembershipRole {
            get; set;
        }
        /// <summary>
        /// A role to which this group is visible.
        /// </summary>
        /// <value>Role ID</value>
        [JsonProperty("visibilityTeamRoleId", Required = Required.AllowNull)]
        public uint? VisibilityRole {
            get; set;
        }
        /// <summary>
        /// Priority of this group current user has set to.
        /// </summary>
        /// <value>User custom priority</value>
        [JsonProperty("userPriority")]
        public uint? UserPriority {
            get; set;
        }
        /// <summary>
        /// If this group is a main group in a team.
        /// </summary>
        /// <value>Main group</value>
        [JsonProperty("isBase", Required = Required.Always)]
        public bool IsBase {
            get; set;
        }
        /// <summary>
        /// Whether or not objects are equal.
        /// </summary>
        /// <param name="obj">Equals to</param>
        /// <returns>If it's equal to other object</returns>
        public override bool Equals(object obj) {
            if(obj is Group gr) return gr.TeamId == TeamId && gr.Id == Id;
            else return false;
        }
        /// <summary>
        /// Whether or not category are equal.
        /// </summary>
        /// <param name="gr0">First group to be compared</param>
        /// <param name="gr1">Second group to be compared</param>
        /// <returns>If it's equal to other object</returns>
        public static bool operator ==(Group gr0, Group gr1) => gr0.TeamId == gr1.TeamId && gr0.Id == gr1.Id;
        /// <summary>
        /// Whether or not category are not equal.
        /// </summary>
        /// <param name="gr0">First group to be compared</param>
        /// <param name="gr1">Second group to be compared</param>
        /// <returns>If it's equal to other object</returns>
        public static bool operator !=(Group gr0, Group gr1) => !(gr0 == gr1);
        /// <summary>
        /// Gets group hashcode.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() => (TeamId.GetHashCode() + Id.GetHashCode() + 3000) / 2;
    }
}