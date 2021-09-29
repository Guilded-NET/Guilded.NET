using System.Threading.Tasks;
using System.Collections.Generic;

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
        /// <para>Adds the member to the group.</para>
        /// <para>This allows the member to see and interact with the given group.</para>
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
        /// <para>Removes the given member from the group.</para>
        /// <para>This disallows the member to interact or see the given group.</para>
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
        /// <para>Sets how much <paramref name="amount"/> of XP is necessary for the given role to be received.</para>
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
        /// Gets member's roles.
        /// </summary>
        /// <remarks>
        /// <para>Gets given member's role ID list.</para>
        /// <para>No permissions are required, as it is team-wide.</para>
        /// </remarks>
        /// <param name="memberId">The identifier of the role holder</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <returns>List of role IDs</returns>
        public abstract Task<IList<uint>> GetMemberRolesAsync(GId memberId);
        /// <summary>
        /// Updates member's nickname.
        /// </summary>
        /// <remarks>
        /// <para>Changes given member's nickname.</para>
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
        /// <para>Gives the given role to the member.</para>
        /// <para>If they hold the role, then nothing happens.</para>
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
        /// <para>Removes the given role from the given member.</para>
        /// <para>If they don't hold the role, then nothing happens.</para>
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
        /// <para>Gives the <paramref name="amount"/> of XP to the given member.</para>
        /// <para>The minimum XP amount is <c>-1000</c> and maximum is <c>1000</c>.</para>
        /// </remarks>
        /// <param name="memberId">The identifier of the receiving member</param>
        /// <param name="amount">The amount of XP received from -1000 to 1000</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <exception cref="System.ArgumentOutOfRangeException">When the amount of XP given exceeds the limit</exception>
        /// <permission cref="XpPermissions.ManageServerXp">Required for managing member's XP</permission>
        /// <returns>Total XP</returns>
        public abstract Task<long> AddXpAsync(GId memberId, short amount);
        #endregion
    }
}