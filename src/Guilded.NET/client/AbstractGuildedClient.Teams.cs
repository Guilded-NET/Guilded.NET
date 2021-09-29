using System;
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
        public override async Task AddMembershipAsync(GId groupId, GId memberId) =>
            await ExecuteRequest($"groups/{groupId}/members/{memberId}", Method.PUT).ConfigureAwait(false);
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
        public override async Task RemoveMembershipAsync(GId groupId, GId memberId) =>
            await ExecuteRequest($"groups/{groupId}/members/{memberId}", Method.DELETE).ConfigureAwait(false);
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
        public override async Task SetRoleLevelAsync(uint roleId, long amount) =>
            await ExecuteRequest($"roles/{roleId}/xp", Method.POST, new
            {
                amount
            }).ConfigureAwait(false);
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
        /// <returns>Nickname</returns>
        public override async Task DeleteNicknameAsync(GId memberId) =>
            await ExecuteRequest($"members/{memberId}/nickname", Method.DELETE).ConfigureAwait(false);
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
        public override async Task AddRoleAsync(GId memberId, uint roleId) =>
            await ExecuteRequest($"members/{memberId}/roles/{roleId}", Method.PUT).ConfigureAwait(false);
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
        public override async Task RemoveRoleAsync(GId memberId, uint roleId) =>
            await ExecuteRequest($"members/{memberId}/roles/{roleId}", Method.DELETE).ConfigureAwait(false);
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
        /// <exception cref="ArgumentOutOfRangeException">When the amount of XP given exceeds the limit</exception>
        /// <permission cref="XpPermissions.ManageServerXp">Required for managing member's XP</permission>
        /// <returns>Total XP</returns>
        public override async Task<long> AddXpAsync(GId memberId, short amount)
        {
            if (amount > 1000 || amount < -1000)
                throw new ArgumentOutOfRangeException($"Expected {nameof(amount)} to be between 1000 and -1000, but got {amount} instead");

            return await GetObject<long>($"members/{memberId}/xp", Method.POST, "total", new
            {
                amount
            }).ConfigureAwait(false);
        }
        #endregion
    }
}