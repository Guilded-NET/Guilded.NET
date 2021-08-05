using System;
using System.Threading.Tasks;

using RestSharp;

namespace Guilded.NET
{
    using Base;

    /// <summary>
    /// Logged-in user in Guilded.
    /// </summary>
    public abstract partial class BasicGuildedClient
    {
        #region Members
        /// <summary>
        /// Adds a role to the user.
        /// </summary>
        /// <param name="memberId">ID of the member to give role to</param>
        /// <param name="roleId">ID of the role to give to the member</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        public override async Task GiveRoleAsync(GId memberId, uint roleId) =>
            await ExecuteRequest($"members/{memberId}/roles/{roleId}", Method.PUT);
        /// <summary>
        /// Removes a role from the user.
        /// </summary>
        /// <param name="memberId">ID of the member to give role to</param>
        /// <param name="roleId">ID of the role to give to the member</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        public override async Task RemoveRoleAsync(GId memberId, uint roleId) =>
            await ExecuteRequest($"members/{memberId}/roles/{roleId}", Method.DELETE);
        /// <summary>
        /// Gives amount of XP to a user.
        /// </summary>
        /// <param name="userId">ID of the user to give XP to</param>
        /// <param name="xpAmount">Amount of XP to give (-1000 to 1000)</param>
        /// <exception cref="ArgumentException">When xpAmount is too big or too small</exception>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Total XP</returns>
        public override async Task<long> GiveXpAsync(GId userId, short xpAmount)
        {
            // Checks if it's not too much or too little
            if (xpAmount > 1000 || xpAmount < -1000)
                throw new ArgumentException($"Expected {nameof(xpAmount)} to be between 1000 and -1000, but got {xpAmount} instead");
            // Gives XP to the user
            return await GetObject<long>($"members/{userId}/xp", Method.POST, key: "total", new
            {
                amount = xpAmount
            });
        }
        #endregion


        #region Roles
        /// <summary>
        /// Attaches amount of XP required to a role.
        /// </summary>
        /// <param name="roleId">ID of the role to attach level to</param>
        /// <param name="amount">Amount of XP required for the role</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
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
        /// <param name="groupId">ID of the group to add member to</param>
        /// <param name="memberId">ID of the member to add</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        public override async Task AddMembershipAsync(GId groupId, GId memberId) =>
            await ExecuteRequest($"groups/{groupId}/members/{memberId}", Method.PUT);
        /// <summary>
        /// Removes a member from the group.
        /// </summary>
        /// <param name="groupId">ID of the group to remove member from</param>
        /// <param name="memberId">ID of the member to remove</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        public override async Task RemoveMembershipAsync(GId groupId, GId memberId) =>
            await ExecuteRequest($"groups/{groupId}/members/{memberId}", Method.DELETE);
        #endregion
    }
}