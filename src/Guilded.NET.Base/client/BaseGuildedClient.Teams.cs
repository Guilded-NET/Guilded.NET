using System.Threading.Tasks;

namespace Guilded.NET.Base
{
    using Permissions;
    public abstract partial class BaseGuildedClient
    {
        #region Groups
        /// <summary>
        /// Adds a member to the group.
        /// </summary>
        /// <remarks>
        /// <para>Adds a member of <paramref name="memberId"/> to the group <paramref name="groupId"/>.</para>
        /// <para>This allows member of <paramref name="memberId"/> to see and interact with the group <paramref name="groupId"/>.</para>
        /// </remarks>
        /// <param name="groupId">The identifier of the parent group</param>
        /// <param name="memberId">The identifier of the member to add</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="GeneralPermissions.ManageGroups">Required for managing group's memberships</permission>
        public abstract Task AddMembershipAsync(GId groupId, GId memberId);
        /// <summary>
        /// Removes a member from the group.
        /// </summary>
        /// <remarks>
        /// <para>Removes a member of <paramref name="memberId"/> from the group <paramref name="groupId"/>.</para>
        /// <para>This disallows member of <paramref name="memberId"/> to interact or see the group <paramref name="groupId"/></para>
        /// </remarks>
        /// <param name="groupId">The identifier of the parent group</param>
        /// <param name="memberId">The identifier of the member to remove</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="GeneralPermissions.ManageGroups">Required for managing group's memberships</permission>
        public abstract Task RemoveMembershipAsync(GId groupId, GId memberId);
        #endregion

        #region Roles
        /// <summary>
        /// Attaches amount of XP required to a role.
        /// </summary>
        /// <remarks>
        /// <para>Sets how much <paramref name="amount"/> of XP is necessary for role of
        /// <paramref name="roleId"/> to be given.</para>
        /// </remarks>
        /// <param name="roleId">The identifier of the editing role</param>
        /// <param name="amount">The amount XP needed</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="GeneralPermissions.ManageRoles">Required for managing roles</permission>
        public abstract Task SetRoleLevelAsync(uint roleId, long amount);
        #endregion

        #region Members
        /// <summary>
        /// Updates member's nickname.
        /// </summary>
        /// <remarks>
        /// <para>Changes given member's nickname to set <paramref name="nickname"/> parameter.</para>
        /// </remarks>
        /// <param name="memberId">The identifier of the member to update</param>
        /// <param name="nickname">The new nickname of the member</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedRequestException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="CustomPermissions.ManageNicknames">Required for managing nicknames of members</permission>
        /// <permission cref="CustomPermissions.ChangeNickname">Required for changing your own nickname</permission>
        /// <returns>Nickname</returns>
        public abstract Task<string> UpdateNicknameAsync(GId memberId, string nickname);
        /// <summary>
        /// Deletes member's nickname.
        /// </summary>
        /// <remarks>
        /// <para>Removes given member's nickname.</para>
        /// </remarks>
        /// <param name="memberId">The identifier of the member to update</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedRequestException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="CustomPermissions.ManageNicknames">Required for managing nicknames of members</permission>
        /// <permission cref="CustomPermissions.ChangeNickname">Required for changing your own nickname</permission>
        /// <returns>Nickname</returns>
        public abstract Task DeleteNicknameAsync(GId memberId);
        /// <summary>
        /// Adds a role to the given user.
        /// </summary>
        /// <remarks>
        /// <para>Gives a member of <paramref name="memberId"/> the role of <paramref name="roleId"/> if permissions are met.</para>
        /// <para>If they hold the role of <paramref name="roleId"/>, then nothing happens.</para>
        /// </remarks>
        /// <param name="memberId">The identifier of the receiving user</param>
        /// <param name="roleId">The identifier of the role to add</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="GeneralPermissions.ManageRoles">Required for managing member's roles</permission>
        public abstract Task AddRoleAsync(GId memberId, uint roleId);
        /// <summary>
        /// Removes a role from the given user.
        /// </summary>
        /// <remarks>
        /// <para>Removes a role of <paramref name="roleId"/> from the member of <paramref name="memberId"/> if permissions are met.</para>
        /// <para>If they don't hold a role of <paramref name="roleId"/>, then nothing happens.</para>
        /// </remarks>
        /// <param name="memberId">The identifier of the losing user</param>
        /// <param name="roleId">The identifier of the role to remove</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="GeneralPermissions.ManageRoles">Required for managing member's roles</permission>
        public abstract Task RemoveRoleAsync(GId memberId, uint roleId);
        /// <summary>
        /// Adds XP to the given user.
        /// </summary>
        /// <remarks>
        /// <para>Gives <paramref name="amount"/> of XP to member of <paramref name="memberId"/>.</para>
        /// <para>The minimum XP amount is <c>-1000</c> and maximum is <c>1000</c>.</para>
        /// </remarks>
        /// <param name="memberId">The identifier of the receiving member</param>
        /// <param name="amount">The amount of XP received from -1000 to 1000</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <exception cref="System.ArgumentOutOfRangeException">When the amount of XP given exceeds the limit</exception>
        /// <permission cref="XPPermissions.ManageServerXP">Required for managing member's XP</permission>
        /// <returns>Total XP</returns>
        public abstract Task<long> AddXpAsync(GId memberId, short amount);
        #endregion
    }
}