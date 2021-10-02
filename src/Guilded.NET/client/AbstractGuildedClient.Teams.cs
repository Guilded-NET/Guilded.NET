using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using RestSharp;

namespace Guilded.NET
{
    using Base;
    using Base.Permissions;
    public abstract partial class AbstractGuildedClient
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
        public override async Task AddMembershipAsync(GId groupId, GId memberId) =>
            await ExecuteRequest($"groups/{groupId}/members/{memberId}", Method.PUT).ConfigureAwait(false);
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
        public override async Task RemoveMembershipAsync(GId groupId, GId memberId) =>
            await ExecuteRequest($"groups/{groupId}/members/{memberId}", Method.DELETE).ConfigureAwait(false);
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
        public override async Task<IList<uint>> GetMemberRolesAsync(GId memberId) =>
            await GetObject<IList<uint>>($"members/{memberId}/roles", Method.GET, key: "roleIds").ConfigureAwait(false);
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
        /// <exception cref="ArgumentNullException">When <paramref name="nickname"/> is <see langword="null"/>, empty or full of whitespace</exception>
        /// <exception cref="ArgumentOutOfRangeException">When <paramref name="nickname"/> is above 32 characters</exception>
        /// <permission cref="CustomPermissions.ManageNicknames">Required for managing nicknames of members</permission>
        /// <permission cref="CustomPermissions.ChangeNickname">Required for changing your own nickname</permission>
        /// <returns>Nickname</returns>
        public override async Task<string> UpdateNicknameAsync(GId memberId, string nickname)
        {
            if(string.IsNullOrWhiteSpace(nickname))
                throw new ArgumentNullException(nameof(nickname));
            else if(nickname.Length > 32)
                throw new ArgumentOutOfRangeException(nameof(nickname), nickname, $"Argument {nameof(nickname)} must be 32 characters in length max");

            return await GetObject<string>($"members/{memberId}/nickname", Method.PUT, key: "nickname", new
            {
                nickname
            }).ConfigureAwait(false);
        }
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
        public override async Task DeleteNicknameAsync(GId memberId) =>
            await ExecuteRequest($"members/{memberId}/nickname", Method.DELETE).ConfigureAwait(false);
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
        public override async Task AddRoleAsync(GId memberId, uint roleId) =>
            await ExecuteRequest($"members/{memberId}/roles/{roleId}", Method.PUT).ConfigureAwait(false);
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
        public override async Task RemoveRoleAsync(GId memberId, uint roleId) =>
            await ExecuteRequest($"members/{memberId}/roles/{roleId}", Method.DELETE).ConfigureAwait(false);
        /// <summary>
        /// Adds XP to the given user.
        /// </summary>
        /// <remarks>
        /// <para>Gives the <paramref name="amount"/> of XP to the given member.</para>
        /// </remarks>
        /// <param name="memberId">The identifier of the receiving member</param>
        /// <param name="amount">The amount of XP received</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="XpPermissions.ManageServerXp">Required for managing member's XP</permission>
        /// <returns>Total XP</returns>
        public override async Task<long> AddXpAsync(GId memberId, long amount) =>
            await GetObject<long>($"members/{memberId}/xp", Method.POST, "total", new
            {
                amount
            }).ConfigureAwait(false);
        /// <summary>
        /// Adds XP to the given role.
        /// </summary>
        /// <remarks>
        /// <para>Gives the <paramref name="amount"/> of XP to all role holders.</para>
        /// </remarks>
        /// <param name="roleId">The identifier of the receiving role</param>
        /// <param name="amount">The amount of XP received</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="XpPermissions.ManageServerXp">Required for managing member's XP</permission>
        public override async Task AddXpAsync(uint roleId, long amount) =>
            await GetObject<long>($"roles/{roleId}/xp", Method.POST, "total", new
            {
                amount
            }).ConfigureAwait(false);
        #endregion
    }
}