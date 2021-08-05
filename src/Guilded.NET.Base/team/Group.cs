using System;
using System.ComponentModel;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Teams
{
    /// <summary>
    /// Represents Guilded team group/subserver.
    /// </summary>
    public class Group : ClientObject
    {
        #region JSON properties

        #region Basic Info
        /// <summary>
        /// ID of this group.
        /// </summary>
        /// <value>Group ID</value>
        [JsonProperty(Required = Required.Always)]
        public GId Id
        {
            get; set;
        }
        /// <summary>
        /// Name of the group.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty(Required = Required.Always)]
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// A description of a team.
        /// </summary>
        /// <value>Nullable string</value>
        [JsonProperty(Required = Required.AllowNull)]
        public string Description
        {
            get; set;
        }
        /// <summary>
        /// A type of this group.
        /// </summary>
        /// <value>Group type</value>
        [JsonProperty(Required = Required.Always)]
        public GroupType Type
        {
            get; set;
        }
        /// <summary>
        /// Icon of this group.
        /// </summary>
        /// <value>URL</value>
        [JsonProperty(Required = Required.AllowNull)]
        public Uri Avatar
        {
            get; set;
        }
        /// <summary>
        /// Banner of this group. Currently unused by Guilded. Potential feature?
        /// </summary>
        /// <value>URL</value>
        [JsonProperty(Required = Required.AllowNull)]
        public Uri Banner
        {
            get; set;
        }
        /// <summary>
        /// ID of the team this group is in.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty(Required = Required.Always)]
        public GId TeamId
        {
            get; set;
        }
        /// <summary>
        /// ID of the game in this group.
        /// </summary>
        /// <value>Game ID</value>
        [JsonProperty(Required = Required.AllowNull)]
        public uint? GameId
        {
            get; set;
        }
        /// <summary>
        /// Priority of the group.
        /// </summary>
        /// <value>Priority</value>
        [JsonProperty(Required = Required.AllowNull)]
        public long? Priority
        {
            get; set;
        }
        /// <summary>
        /// Priority of this group current user has set to.
        /// </summary>
        /// <value>User custom priority?</value>
        public uint? UserPriority
        {
            get; set;
        }
        /// <summary>
        /// If this group is a main group in a team.
        /// </summary>
        /// <value>Main group</value>
        [JsonProperty(Required = Required.Always)]
        public bool IsBase
        {
            get; set;
        }
        #endregion

        #region Actions & Dates
        /// <summary>
        /// When the group was created.
        /// </summary>
        /// <value>Created at</value>
        [JsonProperty(Required = Required.AllowNull)]
        public DateTime? CreatedAt
        {
            get; set;
        }
        /// <summary>
        /// ID of the user who created this group.
        /// </summary>
        /// <value>Created by</value>
        [JsonProperty(Required = Required.AllowNull)]
        public GId CreatedBy
        {
            get; set;
        }
        /// <summary>
        /// When the group was updated/edited.
        /// </summary>
        /// <value>Updated at</value>
        [JsonProperty(Required = Required.AllowNull)]
        public DateTime? UpdatedAt
        {
            get; set;
        }
        /// <summary>
        /// ID of the user who edited/updated this group.
        /// </summary>
        /// <value>Updated by</value>
        [JsonProperty(Required = Required.AllowNull)]
        public GId UpdatedBy
        {
            get; set;
        }
        /// <summary>
        /// When the group was deleted.
        /// </summary>
        /// <value>Deleted at?</value>
        [JsonProperty(Required = Required.AllowNull)]
        public DateTime? DeletedAt
        {
            get; set;
        }
        /// <summary>
        /// When the group was archived.
        /// </summary>
        /// <value>Archived at?</value>
        [JsonProperty(Required = Required.AllowNull)]
        public DateTime? ArchivedAt
        {
            get; set;
        }
        /// <summary>
        /// ID of the user who archived this group.
        /// </summary>
        /// <value>Archived by</value>
        [JsonProperty(Required = Required.AllowNull)]
        public GId ArchivedBy
        {
            get; set;
        }
        #endregion

        #region Membership & Roles
        /// <summary>
        /// User membership updates in this group.
        /// </summary>
        /// <value>List of Membership Updates</value>
        public IList<Membership> MembershipUpdates
        {
            get; set;
        } = new List<Membership>();
        /// <summary>
        /// User membership updates in this group by user ID.
        /// </summary>
        /// <value>Uint, Membership type</value>
        public IDictionary<uint, MembershipType> MembershipUpdatesByUserId
        {
            get; set;
        } = new Dictionary<uint, MembershipType>();
        /// <summary>
        /// Additional roles which have a membership to this group.
        /// </summary>
        /// <value>Role ID list</value>
        [JsonProperty("additionalMembershipTeamRoleIds")]
        [DefaultValue(new uint[] { })]
        public IList<uint> AdditionalMembershipRoles
        {
            get; set;
        }
        /// <summary>
        /// Additional roles which can see this group.
        /// </summary>
        /// <value>Role ID list</value>
        [JsonProperty("additionalVisibilityTeamRoleIds")]
        [DefaultValue(new uint[] { })]
        public IList<uint> AdditionalVisibilityRoles
        {
            get; set;
        }
        /// <summary>
        /// A role that has a membership to this group.
        /// </summary>
        /// <value>Role ID</value>
        [JsonProperty("membershipTeamRoleId", Required = Required.AllowNull)]
        public uint? MembershipRole
        {
            get; set;
        }
        /// <summary>
        /// A role to which this group is visible.
        /// </summary>
        /// <value>Role ID</value>
        [JsonProperty("visibilityTeamRoleId", Required = Required.AllowNull)]
        public uint? VisibilityRole
        {
            get; set;
        }
        #endregion

        #endregion

        
        #region Additional
        /// <summary>
        /// If this group is deleted.
        /// </summary>
        /// <value>Group deleted</value>
        [JsonIgnore]
        public bool IsDeleted => !(DeletedAt is null);
        /// <summary>
        /// If this group is archived.
        /// </summary>
        /// <value>Group archived</value>
        [JsonIgnore]
        public bool IsArchived => !(ArchivedAt is null);
        /// <summary>
        /// If this group was updated
        /// </summary>
        /// <value>Group was updated</value>
        [JsonIgnore]
        public bool WasUpdated => !(UpdatedAt is null);
        #endregion


        #region Overrides
        /// <summary>
        /// Whether objects are equal.
        /// </summary>
        /// <param name="obj">Equals to</param>
        /// <returns>If it's equal to other object</returns>
        public override bool Equals(object obj) =>
            obj is Group gr && gr.TeamId == TeamId && gr.Id == Id;
        /// <summary>
        /// Whether category are equal.
        /// </summary>
        /// <param name="gr0">First group to be compared</param>
        /// <param name="gr1">Second group to be compared</param>
        /// <returns>If it's equal to other object</returns>
        public static bool operator ==(Group gr0, Group gr1) => gr0.TeamId == gr1.TeamId && gr0.Id == gr1.Id;
        /// <summary>
        /// Whether category are not equal.
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
        #endregion
    }
}