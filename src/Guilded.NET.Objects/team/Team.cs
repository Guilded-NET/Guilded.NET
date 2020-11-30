using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;
using System.Linq;
using Guilded.NET.Objects.Converters;

namespace Guilded.NET.Objects.Teams {
    /// <summary>
    /// Guilded team/guild/server.
    /// </summary>
    public class Team: ClientObject {
        /// <summary>
        /// ID of the team.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty("id", Required = Required.Always)]
        public GId Id {
            get; set;
        }
        /// <summary>
        /// ID of the owner.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("ownerId", Required = Required.Always)]
        public GId OwnerId {
            get; set;
        }
        /// <summary>
        /// Name of the team.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty("name", Required = Required.Always)]
        public string Name {
            get; set;
        }
        /// <summary>
        /// Team subdomain used in a unique link.
        /// </summary>
        /// <value>Subdomain</value>
        [JsonProperty("subdomain", Required = Required.Always)]
        public string Subdomain {
            get; set;
        }
        /// <summary>
        /// URL of team's avatar.
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("profilePicture")]
        public Uri Avatar {
            get; set;
        }
        /// <summary>
        /// URL of team's dash image.
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("teamDashImage", Required = Required.AllowNull)]
        public Uri DashImage {
            get; set;
        }
        /// <summary>
        /// Large version of team's banner.
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("homeBannerImageLg", Required = Required.AllowNull)]
        public Uri HomeBannerLarge {
            get; set;
        }
        /// <summary>
        /// Team's banner image.
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("homeBannerImageMd", Required = Required.AllowNull)]
        public string HomeBannerMid {
            get; set;
        }
        /// <summary>
        /// Small version of team's banner.
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("homeBannerImageSm", Required = Required.AllowNull)]
        public Uri HomeBannerSmall {
            get; set;
        }
        /// <summary>
        /// Team's selected timezone.
        /// </summary>
        /// <value>Timezone</value>
        [JsonProperty("timezone", Required = Required.Always)]
        public string Timezone {
            get; set;
        }
        /// <summary>
        /// Whether the team is recruiting or not.
        /// </summary>
        /// <value>Boolean</value>
        [JsonProperty("isRecruiting", Required = Required.Always)]
        public bool IsRecruiting {
            get; set;
        }
        /// <summary>
        /// Whether or not the team is verified.
        /// </summary>
        /// <value>Boolean</value>
        [JsonProperty("isVerified", Required = Required.Always)]
        public bool IsVerified {
            get; set;
        }
        /// <summary>
        /// Whether or not the team is public.
        /// </summary>
        /// <value>Boolean</value>
        [JsonProperty("isPublic", Required = Required.Always)]
        public bool IsPublic {
            get; set;
        }
        /// <summary>
        /// If it should always show team home.
        /// </summary>
        /// <value>Boolean</value>
        [JsonProperty("alwaysShowTeamHome", Required = Required.Always)]
        public bool AlwaysShowTeamHome {
            get; set;
        }
        /// <summary>
        /// Whether or not the team is pro(has paid?).
        /// </summary>
        /// <value>Boolean</value>
        [JsonProperty("isPro", Required = Required.Always)]
        public bool IsPro {
            get; set;
        }
        /// <summary>
        /// If it should sync Discord roles.
        /// </summary>
        /// <value>Boolean</value>
        [JsonProperty("autoSyncDiscordRoles", Required = Required.AllowNull)]
        public bool? AutoSyncDiscordRoles {
            get; set;
        }
        /// <summary>
        /// Type of the team.
        /// </summary>
        /// <value>CamelCase enum value</value>
        //[JsonProperty("type")]
        //public TeamType Type {
        //    get; set;
        //}
        /// <summary>
        /// Count of members in the team.
        /// </summary>
        /// <value>Count</value>
        [JsonProperty("memberCount", Required = Required.AllowNull)]
        public uint? MemberCount {
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
        /// Count of followers in the team.
        /// </summary>
        /// <value>Count</value>
        [JsonProperty("followerCount", Required = Required.Always)]
        public uint FollowerCount {
            get; set;
        }
        /// <summary>
        /// If the team was favourited by the current user.
        /// </summary>
        /// <value>Boolean</value>
        [JsonProperty("isFavorite", Required = Required.Always)]
        public bool IsFavorite {
            get; set;
        }
        /// <summary>
        /// Whether or not this user can invite members to this team.
        /// </summary>
        /// <value>Boolean</value>
        [JsonProperty("canInviteMembers", Required = Required.Always)]
        public bool CanInviteMembers {
            get; set;
        }
        /// <summary>
        /// Whether or not this user can update this team.
        /// </summary>
        /// <value>Boolean</value>
        [JsonProperty("canUpdateTeam", Required = Required.Always)]
        public bool CanUpdateTeam {
            get; set;
        }
        /// <summary>
        /// Whether or not this user can manage tournaments.
        /// </summary>
        /// <value>Boolean</value>
        [JsonProperty("canManageTournaments", Required = Required.Always)]
        public bool CanManageTournaments {
            get; set;
        }
        /// <summary>
        /// Whether or not this client user is a member of this team.
        /// </summary>
        /// <value>Boolean</value>
        [JsonProperty("viewerIsMember", Required = Required.Always)]
        public bool ViewerIsMember {
            get; set;
        }
        /// <summary>
        /// Whether or not this user is an admin in this team.
        /// </summary>
        /// <value></value>
        [JsonProperty("isAdmin", Required = Required.Always)]
        public bool IsAdmin {
            get; set;
        }
        /// <summary>
        /// List of role IDs in this team.
        /// </summary>
        /// <value>List of IDs</value>
        [JsonProperty("roleIds", Required = Required.Always)]
        public IList<ulong> Roles {
            get; set;
        }
        /// <summary>
        /// All roles in this team.
        /// </summary>
        /// <value>IDictionary of role id, role</value>
        [JsonProperty("rolesById", Required = Required.Always)]
        public IDictionary<string, TeamRole> RolesById {
            get; set;
        }
        /// <summary>
        /// Turns team to string.
        /// </summary>
        /// <returns>Team as a string</returns>
        public override string ToString() => $"Team({Id})";
        /// <summary>
        /// Whether or not objects are equal.
        /// </summary>
        /// <param name="obj">Equals to</param>
        /// <returns>If it's equal to other object</returns>
        public override bool Equals(object obj) {
            if(obj is Team team) return team.Id == Id;
            else return false;
        }
        /// <summary>
        /// Whether or not teams are equal.
        /// </summary>
        /// <param name="tm0">First team to be compared</param>
        /// <param name="tm1">Second team to be compared</param>
        /// <returns>If it's equal to other object</returns>
        public static bool operator ==(Team tm0, Team tm1) => tm0.Id == tm1.Id;
        /// <summary>
        /// Whether or not teams are not equal.
        /// </summary>
        /// <param name="tm0">First team to be compared</param>
        /// <param name="tm1">Second team to be compared</param>
        /// <returns>If it's not equal to other object</returns>
        public static bool operator !=(Team tm0, Team tm1) => !(tm0 == tm1);
        /// <summary>
        /// Gets team hashcode.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() => Id.GetHashCode() + 300;
    }
}