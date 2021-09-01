using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Teams
{
    using Permissions;
    /// <summary>
    /// Interface for team channels and categories.
    /// </summary>
    /// <typeparam name="T">Type of channel's ID</typeparam>
    public abstract class TeamChannel<T> : BaseChannel<T>
    {
        #region JSON properties
        /// <summary>
        /// Priority/sort index of this channel.
        /// </summary>
        /// <value>Priority</value>
        [JsonProperty(Required = Required.AllowNull)]
        public long? Priority
        {
            get; set;
        }
        /// <summary>
        /// Name of this channel.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty(Required = Required.Always)]
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// Permissions of the roles in this channel.
        /// </summary>
        /// <value>Role Permissions</value>
        [JsonProperty("rolesById", Required = Required.Always)]
        public IDictionary<string, ChannelPermission> RolePermissions
        {
            get; set;
        }
        /// <summary>
        /// Permissions of the users in this channel.
        /// </summary>
        /// <value>User Permissions</value>
        [JsonProperty(Required = Required.AllowNull)]
        public IList<UserPermission> UserPermissions
        {
            get; set;
        }
        /// <summary>
        /// ID of team this channel is in.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty(Required = Required.Always)]
        public GId TeamId
        {
            get; set;
        }
        /// <summary>
        /// ID of the group this channel is in.
        /// </summary>
        /// <value>Group ID</value>
        [JsonProperty(Required = Required.Always)]
        public GId GroupId
        {
            get; set;
        }
        /// <summary>
        /// ID of the category this channel is in.
        /// </summary>
        /// <value>Channel ID?</value>
        [JsonProperty("channelCategoryId")]
        public uint? CategoryId
        {
            get; set;
        }
        #endregion

        /*#region Additional
        /// <summary>
        /// Gets the team of a channel.
        /// </summary>
        /// <returns>Team</returns>
        public async Task<Team> GetTeamAsync() =>
            await ParentClient.GetTeamAsync(TeamId);
        /// <summary>
        /// Gets parent group of this channel.
        /// </summary>
        /// <returns>Group</returns>
        public async Task<Group> GetGroupAsync() =>
            await ParentClient.GetGroupAsync(TeamId, GroupId);
        /// <summary>
        /// Gets category of this channel.
        /// </summary>
        /// <returns>Category?</returns>
        public async Task<Category> GetCategoryAsync() =>
            CategoryId is null
            ? default
            : (await ParentClient.GetChannelsAsync(TeamId)).Categories.FirstOrDefault(category => category.Id == CategoryId);
        /// <summary>
        /// Gets all team and channel permissions, then adds them all up.
        /// </summary>
        /// <param name="member">Member to get all permissions of</param>
        /// <param name="team">Team this channel is in</param>
        /// <returns>Allowed permissions</returns>
        public PermissionList GetFullPermissionsOf(BaseTeam team, TeamMember member)
        {
            // All of the permissions this user has in a team
            PermissionList teamPerms = team.GetPermissionsOf(member);
            // Gets all role permissions this user has
            IEnumerable<ChannelPermission> rolePerms =
                RolePermissions
                    .Where(perm => (uint.TryParse(perm.Key, out uint y) && member.RoleIds.Contains(y)) || perm.Key == "baseRole")
                    .Select(perm => perm.Value);
            // Gets user permissions
            UserPermission userPerms = UserPermissions?.FirstOrDefault(perm => perm.UserId == member?.Id);
            // Adds up all user permissions and role permissions
            return teamPerms
                - OptionalAddition(rolePerms.Select(permList => permList.DenyPermissions))
                - userPerms?.DenyPermissions
                + OptionalAddition(rolePerms.Select(permList => permList.AllowPermissions))
                + userPerms?.AllowPermissions;
        }
        /// <summary>
        /// Gets all team and channel permissions, then adds them all up.
        /// </summary>
        /// <param name="member">Member to get all permissions of</param>
        /// <returns>Allowed permissions</returns>
        public async Task<PermissionList> GetFullPermissionsOf(TeamMember member) =>
            GetFullPermissionsOf(await GetTeamAsync(), member);
        #endregion*/

        #region Utilities
        // /// <summary>
        // /// Optionally aggregates permission list
        // /// </summary>
        // /// <param name="perms">Permission list to aggregate</param>
        // /// <returns>Aggregated permission list</returns>
        //private static PermissionList OptionalAddition(IEnumerable<PermissionList> perms) =>
        //    perms.Count() > 1 ? perms.Aggregate((perm0, perm1) => perm0 + perm1) : perms.FirstOrDefault();
        #endregion

        #region Overrides
        /// <summary>
        /// Whether objects are equal.
        /// </summary>
        /// <param name="obj">Equals to</param>
        /// <returns>If it's equal to other object</returns>
        public override bool Equals(object obj) =>
            obj is TeamChannel<T> ch && ch.TeamId == TeamId && Equals(ch.Id, Id);
        /// <summary>
        /// Whether channels are equal.
        /// </summary>
        /// <param name="ch0">First channel to be compared</param>
        /// <param name="ch1">Second channel to be compared</param>
        /// <returns>If it's equal to other object</returns>
        public static bool operator ==(TeamChannel<T> ch0, TeamChannel<T> ch1) =>
            ch0.TeamId == ch1.TeamId && Equals(ch0.Id, ch1.Id);
        /// <summary>
        /// Whether channels are not equal.
        /// </summary>
        /// <param name="ch0">First channel to be compared</param>
        /// <param name="ch1">Second channel to be compared</param>
        /// <returns>If it's not equal to other object</returns>
        public static bool operator !=(TeamChannel<T> ch0, TeamChannel<T> ch1) =>
            !(ch0 == ch1);
        /// <summary>
        /// Gets channel hashcode.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() =>
            (TeamId.GetHashCode() + Id.GetHashCode() + 2000) / 2;
        #endregion
    }
}