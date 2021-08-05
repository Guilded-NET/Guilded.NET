using System;
using System.Linq;
using System.Drawing;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Guilded.NET.Base.Teams
{
    using Users;
    using Permissions;
    /// <summary>
    /// Guilded team/guild/server.
    /// </summary>
    public class BaseTeam : ClientObject
    {
        #region JSON properties
        
        #region Basic Info
        /// <summary>
        /// ID of the team.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty(Required = Required.Always)]
        public GId Id
        {
            get; set;
        }
        /// <summary>
        /// ID of the owner.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty(Required = Required.Always)]
        public GId OwnerId
        {
            get; set;
        }
        /// <summary>
        /// Name of the team.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty(Required = Required.Always)]
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// Team subdomain used in a unique link.
        /// </summary>
        /// <value>Subdomain</value>
        [JsonProperty(Required = Required.Always)]
        public string Subdomain
        {
            get; set;
        }
        #endregion

        #region Banners & Avatars
        /// <summary>
        /// URL of team's avatar.
        /// </summary>
        /// <value>URL</value>
        public Uri ProfilePicture
        {
            get; set;
        }
        /// <summary>
        /// URL of team's dash image.
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("teamDashImage", Required = Required.AllowNull)]
        public Uri DashImage
        {
            get; set;
        }
        /// <summary>
        /// URL of team's banner(small version).
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("homeBannerImageSm")]
        public Uri HomeBannerSmall
        {
            get; set;
        }
        /// <summary>
        /// URL of team's banner(medium version).
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("homeBannerImageMd")]
        public Uri HomeBannerMedium
        {
            get; set;
        }
        /// <summary>
        /// URL of team's banner(large version).
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("homeBannerImageLarge")]
        public Uri HomeBannerLarge
        {
            get; set;
        }
        #endregion

        #region Is...
        /// <summary>
        /// Whether the team is recruiting members or not.
        /// </summary>
        /// <value>Recruiting</value>
        [JsonProperty(Required = Required.Always)]
        public bool IsRecruiting
        {
            get; set;
        }
        /// <summary>
        /// Whether the team is verified.
        /// </summary>
        /// <value>Verified</value>
        [JsonProperty(Required = Required.Always)]
        public bool IsVerified
        {
            get; set;
        }
        /// <summary>
        /// Whether the team is public.
        /// </summary>
        /// <value>Public</value>
        [JsonProperty(Required = Required.Always)]
        public bool IsPublic
        {
            get; set;
        }
        /// <summary>
        /// Whether the team is pro verified.
        /// </summary>
        /// <value>Pro verified</value>
        [JsonProperty(Required = Required.Always)]
        public bool IsPro
        {
            get; set;
        }
        #endregion
        
        #region Discord
        /// <summary>
        /// If it should sync Discord roles.
        /// </summary>
        /// <value>Boolean</value>
        public bool AutoSyncDiscordRoles
        {
            get; set;
        }
        /// <summary>
        /// ID of the Discord server this team has imported.
        /// </summary>
        /// <value>(Nullable) Discord Server ID</value>
        [JsonProperty("discordGuildId")]
        public ulong? DiscordId
        {
            get; set;
        }
        /// <summary>
        /// Name of the Discord server this team has imported.
        /// </summary>
        /// <value>(Nullable) Discord Server Name</value>
        [JsonProperty("discordServerName")]
        public string DiscordName
        {
            get; set;
        }
        #endregion

        #region Members & Followers
        /// <summary>
        /// All roles in this team.
        /// </summary>
        /// <value>IDictionary of role id, role</value>
        [JsonProperty(Required = Required.Always)]
        public IDictionary<string, TeamRole> RolesById
        {
            get; set;
        }
        #endregion

        #region Team Features
        /// <summary>
        /// A main/home group in this team.
        /// </summary>
        /// <value>Group</value>
        [JsonProperty(Required = Required.Always)]
        public Group BaseGroup
        {
            get; set;
        }
        /// <summary>
        /// All games in this team.
        /// </summary>
        /// <value>Team game IDs</value>
        [JsonProperty(Required = Required.Always)]
        public IList<uint> Games
        {
            get; set;
        }
        #endregion

        #region Other
        /// <summary>
        /// Team's selected timezone.
        /// </summary>
        /// <value>Timezone</value>
        [JsonProperty(Required = Required.AllowNull)]
        public string Timezone
        {
            get; set;
        }
        /// <summary>
        /// Type of this team.
        /// </summary>
        /// <value>Team type</value>
        [JsonProperty(Required = Required.AllowNull)]
        public TeamType? Type
        {
            get; set;
        }
        #endregion

        #endregion

        
        /*#region Additional
        /// <summary>
        /// Gets team channels.
        /// </summary>
        /// <returns>Channel, category and thread list</returns>
        public async Task<ChannelList> GetChannelsAsync() =>
            await ParentClient.GetChannelsAsync(Id);
        /// <summary>
        /// Gets team groups.
        /// </summary>
        /// <returns>Group list</returns>
        public async Task<IList<Group>> GetGroupsAsync() =>
            await ParentClient.GetGroupsAsync(Id);
        /// <summary>
        /// Gets owner of this team as an user.
        /// </summary>
        /// <returns>Team owner</returns>
        public async Task<User> GetOwnerAsync() =>
            await ParentClient.GetUserAsync(OwnerId);
        /// <summary>
        /// Kicks a member from this team.
        /// </summary>
        /// <param name="memberId">ID of the member to kick</param>
        public async Task KickMemberAsync(GId memberId) =>
            await ParentClient.KickMemberAsync(Id, memberId);
        /// <summary>
        /// Kicks a member from this team.
        /// </summary>
        /// <param name="member">Member to kick</param>
        public async Task KickMemberAsync(TeamMember member) =>
            await ParentClient.KickMemberAsync(Id, member.Id);
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
        /// <param name="member">Member to ban</param>
        /// <param name="reason">Why user got banned</param>
        /// <param name="deleteHistoryOption">How much of the history should be deleted</param>
        public async Task BanMemberAsync(TeamMember member, string reason, uint deleteHistoryOption) =>
            await ParentClient.BanMemberAsync(Id, member.Id, reason, deleteHistoryOption);
        /// <summary>
        /// Gets all permissions(server-wide) of a specific member.
        /// </summary>
        /// <param name="member">Member to get permissions of</param>
        /// <returns>Allowed permissions</returns>
        public PermissionList GetPermissionsOf(TeamMember member) =>
            RolesById["baseRole"].Permissions + member.RoleIds?.Select(x => RolesById[x.ToString()].Permissions)?.Aggregate((a, b) => a + b);
        /// <summary>
        /// Gets a colour of a specific member.
        /// </summary>
        /// <param name="member">Member's colour</param>
        /// <returns>Colour</returns>
        public Color GetColorOf(TeamMember member)
        {
            // Gets a colour from roles
            Color roleColour = member.RoleIds
                .Select(x => RolesById[x.ToString()].Color)
                .FirstOrDefault(x => x.A != 0);
            // If role colour is empty, then return base role colour. Else, return role colour.
            return roleColour != default ? roleColour : RolesById["baseRole"].Color;
        }
        /// <summary>
        /// Get a channel with given ID in this team.
        /// </summary>
        /// <param name="channelId">ID of the channel to get</param>
        /// <returns>Channel</returns>
        public async Task<Channel> GetChannelAsync(Guid channelId) =>
            await ParentClient.GetChannelAsync(Id, channelId);
        /// <summary>
        /// Gets this user team as a normal team.
        /// </summary>
        /// <returns>Team</returns>
        public async Task<Team> AsTeamAsync() =>
            await ParentClient.GetTeamAsync(Id);
        #endregion*/

        
        #region Overrides
        /// <summary>
        /// Turns team to string.
        /// </summary>
        /// <returns>Team as a string</returns>
        public override string ToString() => $"Team[{Id}] {Name}";
        /// <summary>
        /// Whether objects are equal.
        /// </summary>
        /// <param name="obj">Equals to</param>
        /// <returns>If it's equal to other object</returns>
        public override bool Equals(object obj)
        {
            if (obj is BaseTeam team) return team.Id == Id;
            else return false;
        }
        /// <summary>
        /// Whether teams are equal.
        /// </summary>
        /// <param name="tm0">First team to be compared</param>
        /// <param name="tm1">Second team to be compared</param>
        /// <returns>If it's equal to other object</returns>
        public static bool operator ==(BaseTeam tm0, BaseTeam tm1) => tm0.Id == tm1.Id;
        /// <summary>
        /// Whether teams are not equal.
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
        #endregion
    }
}