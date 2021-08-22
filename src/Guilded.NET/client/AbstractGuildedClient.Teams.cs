using System;
using System.Threading.Tasks;

using RestSharp;

namespace Guilded.NET
{
    using Base;
    using Base.Permissions;
    /// <summary>
    /// A base for all Guilded clients.
    /// </summary>
    public abstract partial class AbstractGuildedClient
    {
        #region Members
        /// <summary>
        /// Adds a role to the given user.
        /// </summary>
        /// <example>
        /// <code>
        /// await client.AddRoleAsync(message.CreatedBy, 100000000);
        /// </code>
        /// </example>
        /// <param name="memberId">The identifier of the receiving user</param>
        /// <param name="roleId">The identifier of the role to add</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the member of identifier <paramref name="memberId"/> has not been found</exception>
        /// <permission cref="GeneralPermissions.ManageRoles">Required for managing member's roles</permission>
        public override async Task AddRoleAsync(GId memberId, uint roleId) =>
            await ExecuteRequest($"members/{memberId}/roles/{roleId}", Method.PUT);
        /// <summary>
        /// Removes a role from the given user.
        /// </summary>
        /// <example>
        /// <code>
        /// await client.RemoveRoleAsync(message.CreatedBy, 100000000);
        /// </code>
        /// </example>
        /// <param name="memberId">The identifier of the losing user</param>
        /// <param name="roleId">The identifier of the role to remove</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the member of identifier <paramref name="memberId"/> has not been found</exception>
        /// <permission cref="GeneralPermissions.ManageRoles">Required for managing member's roles</permission>
        public override async Task RemoveRoleAsync(GId memberId, uint roleId) =>
            await ExecuteRequest($"members/{memberId}/roles/{roleId}", Method.DELETE);
        /// <summary>
        /// Adds XP to the given user.
        /// </summary>
        /// <example>
        /// <code>
        /// await client.AddXpAsync(message.CreatedBy, 10);
        /// </code>
        /// </example>
        /// <param name="memberId">The identifier of the receiving member</param>
        /// <param name="xpAmount">The amount of XP received from -1000 to 1000</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the member of identifier <paramref name="memberId"/> has not been found</exception>
        /// <exception cref="ArgumentOutOfRangeException">When the amount of XP given exceeds the limit</exception>
        /// <permission cref="XPPermissions.ManageServerXP">Required for managing member's XP</permission>
        /// <returns>Total XP</returns>
        public override async Task<long> AddXpAsync(GId memberId, short xpAmount)
        {
            // Checks if it's not too much or too little
            if (xpAmount > 1000 || xpAmount < -1000)
                throw new ArgumentOutOfRangeException($"Expected {nameof(xpAmount)} to be between 1000 and -1000, but got {xpAmount} instead");
            // Gives XP to the user
            return await GetObject<long>($"members/{memberId}/xp", Method.POST, "total", new
            {
                amount = xpAmount
            });
        }
        #endregion

        #region Roles
        /// <summary>
        /// Attaches amount of XP required to a role.
        /// </summary>
        /// <example>
        /// <code>
        /// await client.AttachRoleLevelAsync(1000000000, 2048);
        /// </code>
        /// </example>
        /// <param name="roleId">The identifier of the editing role</param>
        /// <param name="amount">The amount XP added</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the role of identifier <paramref name="roleId"/> has not been found</exception>
        /// <permission cref="GeneralPermissions.ManageRoles">Required for managing roles</permission>
        public override async Task AttachRoleLevelAsync(uint roleId, long amount) =>
            await ExecuteRequest($"roles/{roleId}/xp", Method.POST, new
            {
                amount
            });
        #endregion

        #region Groups
        /// <summary>
        /// Adds a member to the group.
        /// </summary>
        /// <example>
        /// <code>
        /// await client.AddMembershipAsync(group.Id, message.CreatedBy);
        /// </code>
        /// </example>
        /// <param name="groupId">The identifier of the parent group</param>
        /// <param name="memberId">The identifier of the member to add</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the group <paramref name="groupId"/>, the member <paramref name="memberId"/> or both have not been found</exception>
        /// <permission cref="GeneralPermissions.ManageGroups">Required for managing group's memberships</permission>
        public override async Task AddMembershipAsync(GId groupId, GId memberId) =>
            await ExecuteRequest($"groups/{groupId}/members/{memberId}", Method.PUT);
        /// <summary>
        /// Removes a member from the group.
        /// </summary>
        /// <example>
        /// <code>
        /// await client.RemoveMembershipAsync(group.Id, message.CreatedBy);
        /// </code>
        /// </example>
        /// <param name="groupId">The identifier of the parent group</param>
        /// <param name="memberId">The identifier of the member to remove</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the group <paramref name="groupId"/>, the member <paramref name="memberId"/> or both have not been found</exception>
        /// <permission cref="GeneralPermissions.ManageGroups">Required for managing group's memberships</permission>
        public override async Task RemoveMembershipAsync(GId groupId, GId memberId) =>
            await ExecuteRequest($"groups/{groupId}/members/{memberId}", Method.DELETE);
        #endregion
    }
}