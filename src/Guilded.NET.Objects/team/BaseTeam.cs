using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace Guilded.NET.Objects.Teams {
    /// <summary>
    /// Guilded team/guild/server.
    /// </summary>
    public class BaseTeam: ClientObject {
        //=======================//
        //   Team IDs
        //=======================//
        
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
        //=======================//
        //   Name and Info
        //=======================//
        
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
        //=======================//
        //   Icons & Banners
        //=======================//
        
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
        /// URL of team's banner(small version).
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("homeBannerImageSm")]
        public Uri HomeBannerSmall {
            get; set;
        }
        /// <summary>
        /// URL of team's banner(medium version).
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("homeBannerImageMd")]
        public Uri HomeBannerMedium {
            get; set;
        }
        /// <summary>
        /// URL of team's banner(large version).
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("homeBannerImageLarge")]
        public Uri HomeBannerLarge {
            get; set;
        }
        //=======================//
        //   Is...
        //=======================//

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
        /// Whether or not the team is pro(has paid?).
        /// </summary>
        /// <value>Boolean</value>
        [JsonProperty("isPro", Required = Required.Always)]
        public bool IsPro {
            get; set;
        }
        //=======================//
        //   Discord
        //=======================//

        /// <summary>
        /// If it should sync Discord roles.
        /// </summary>
        /// <value>Boolean</value>
        [JsonProperty("autoSyncDiscordRoles", Required = Required.Always)]
        public bool AutoSyncDiscordRoles {
            get; set;
        }
        /// <summary>
        /// ID of the Discord server this team has imported.
        /// </summary>
        /// <value>(Nullable) Discord Server ID</value>
        [JsonProperty("discordGuildId", Required = Required.AllowNull)]
        public ulong? DiscordId {
            get; set;
        }
        /// <summary>
        /// Name of the Discord server this team has imported.
        /// </summary>
        /// <value>(Nullable) Discord Server Name</value>
        [JsonProperty("discordServerName", Required = Required.AllowNull)]
        public string DiscordName {
            get; set;
        }
        //=======================//
        //   Members & Followers
        //=======================//

        /// <summary>
        /// All roles in this team.
        /// </summary>
        /// <value>IDictionary of role id, role</value>
        [JsonProperty("rolesById", Required = Required.Always)]
        public IDictionary<string, TeamRole> RolesById {
            get; set;
        }
        //======================//
        //   Team features
        //=======================//

        /// <summary>
        /// A main/home group in this team.
        /// </summary>
        /// <value>Group</value>
        [JsonProperty("baseGroup", Required = Required.Always)]
        public Group BaseGroup {
            get; set;
        }
        /// <summary>
        /// All games in this team.
        /// </summary>
        /// <value>Team game IDs</value>
        [JsonProperty("games", Required = Required.Always)]
        public IList<uint> Games {
            get; set;
        }
        //======================//
        //   Other
        //=======================//
        
        /// <summary>
        /// Team's selected timezone.
        /// </summary>
        /// <value>Timezone</value>
        [JsonProperty("timezone", Required = Required.AllowNull)]
        public string Timezone {
            get; set;
        }
        /// <summary>
        /// Type of this team.
        /// </summary>
        /// <value>Team type</value>
        [JsonProperty("type", Required = Required.AllowNull)]
        public TeamType? Type {
            get; set;
        }

        //=====================//
        //   Additional
        //=====================//

        /// <summary>
        /// Gets team channels.
        /// </summary>
        /// <returns>Channel, category and thread list</returns>
        public async Task<Channels> GetChannelsAsync() =>
            await ParentClient.GetChannelsAsync(Id);
        /// <summary>
        /// Gets team channels.
        /// </summary>
        /// <returns>Channel, category and thread list</returns>
        public Channels GetChannels() =>
            ParentClient.GetChannels(Id);
        /// <summary>
        /// Gets team groups.
        /// </summary>
        /// <returns>Group list</returns>
        public async Task<IList<Group>> GetGroupsAsync() =>
            await ParentClient.GetGroupsAsync(Id);
        /// <summary>
        /// Gets team groups.
        /// </summary>
        /// <returns>Group list</returns>
        public IList<Group> GetGroups() =>
            ParentClient.GetGroups(Id);
        /// <summary>
        /// Gets owner of this team as an user.
        /// </summary>
        /// <returns>Team owner</returns>
        public async Task<User> GetOwnerAsync() =>
            await ParentClient.GetUserAsync(OwnerId);
        /// <summary>
        /// Gets owner of this team as an user.
        /// </summary>
        /// <returns>Team owner</returns>
        public User GetOwner() =>
            ParentClient.GetUser(OwnerId);
        /// <summary>
        /// Kicks a member from this team.
        /// </summary>
        /// <param name="memberId">ID of the member to kick</param>
        public async Task KickMemberAsync(GId memberId) =>
            await ParentClient.KickMemberAsync(Id, memberId);
        /// <summary>
        /// Kicks a member from this team.
        /// </summary>
        /// <param name="memberId">ID of the member to kick</param>
        public void KickMember(GId memberId) =>
            ParentClient.KickMember(Id, memberId);
        /// <summary>
        /// Kicks a member from this team.
        /// </summary>
        /// <param name="member">Member to kick</param>
        public async Task KickMemberAsync(TeamMember member) =>
            await ParentClient.KickMemberAsync(Id, member.Id);
        /// <summary>
        /// Kicks a member from this team.
        /// </summary>
        /// <param name="member">Member to kick</param>
        public void KickMember(TeamMember member) =>
            ParentClient.KickMember(Id, member.Id);
        /// <summary>
        /// Kicks a member from this team.
        /// </summary>
        /// <param name="memberId">ID of the member to kick</param>
        /// <param name="reason">Why user got banned</param>
        /// <param name="deleteHistoryOption">How much of the history should be deleted</param>
        public async Task BanMemberAsync(GId memberId, string reason, uint deleteHistoryOption) =>
            await ParentClient.BanMemberAsync(Id, memberId, reason, deleteHistoryOption);
        /// <summary>
        /// Kicks a member from this team.
        /// </summary>
        /// <param name="memberId">ID of the member to kick</param>
        /// <param name="reason">Why user got banned</param>
        /// <param name="deleteHistoryOption">How much of the history should be deleted</param>
        public void BanMember(GId memberId, string reason, uint deleteHistoryOption) =>
            ParentClient.BanMember(Id, memberId, reason, deleteHistoryOption);
        /// <summary>
        /// Kicks a member from this team.
        /// </summary>
        /// <param name="member">Member to ban</param>
        /// <param name="reason">Why user got banned</param>
        /// <param name="deleteHistoryOption">How much of the history should be deleted</param>
        public async Task BanMemberAsync(TeamMember member, string reason, uint deleteHistoryOption) =>
            await ParentClient.BanMemberAsync(Id, member.Id, reason, deleteHistoryOption);
        /// <summary>
        /// Kicks a member from this team.
        /// </summary>
        /// <param name="member">Member to ban</param>
        /// <param name="reason">Why user got banned</param>
        /// <param name="deleteHistoryOption">How much of the history should be deleted</param>
        public void BanMember(TeamMember member, string reason, uint deleteHistoryOption) =>
            ParentClient.BanMember(Id, member.Id, reason, deleteHistoryOption);
        
        //=====================//
        //   Overrides
        //=====================//

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
            if(obj is BaseTeam team) return team.Id == Id;
            else return false;
        }
        /// <summary>
        /// Whether or not teams are equal.
        /// </summary>
        /// <param name="tm0">First team to be compared</param>
        /// <param name="tm1">Second team to be compared</param>
        /// <returns>If it's equal to other object</returns>
        public static bool operator ==(BaseTeam tm0, BaseTeam tm1) => tm0.Id == tm1.Id;
        /// <summary>
        /// Whether or not teams are not equal.
        /// </summary>
        /// <param name="tm0">First team to be compared</param>
        /// <param name="tm1">Second team to be compared</param>
        /// <returns>If it's not equal to other object</returns>
        public static bool operator !=(BaseTeam tm0, BaseTeam tm1) => !(tm0 == tm1);
        /// <summary>
        /// Gets team hashcode.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() => Id.GetHashCode() + 300;
    }
}