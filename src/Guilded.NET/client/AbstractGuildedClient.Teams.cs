using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using RestSharp;

namespace Guilded.NET
{
    using Base;
    public abstract partial class AbstractGuildedClient
    {
        #region Groups
        /// <inheritdoc/>
        public override async Task AddMembershipAsync(GId groupId, GId memberId) =>
            await ExecuteRequestAsync(new RestRequest($"groups/{groupId}/members/{memberId}", Method.PUT)).ConfigureAwait(false);
        /// <inheritdoc/>
        public override async Task RemoveMembershipAsync(GId groupId, GId memberId) =>
            await ExecuteRequestAsync(new RestRequest($"groups/{groupId}/members/{memberId}", Method.DELETE)).ConfigureAwait(false);
        #endregion

        #region Members
        /// <inheritdoc/>
        public override async Task<IList<uint>> GetMemberRolesAsync(GId memberId) =>
            await GetObject<IList<uint>>(new RestRequest($"members/{memberId}/roles", Method.GET), "roleIds").ConfigureAwait(false);
        /// <inheritdoc/>
        public override async Task<string> UpdateNicknameAsync(GId memberId, string nickname)
        {
            if(string.IsNullOrWhiteSpace(nickname))
                throw new ArgumentNullException(nameof(nickname));
            else if(nickname.Length > 32)
                throw new ArgumentOutOfRangeException(nameof(nickname), nickname, $"Argument {nameof(nickname)} must be 32 characters in length max");

            return await GetObject<string>(new RestRequest($"members/{memberId}/nickname", Method.PUT)
                .AddJsonBody(new
                {
                    nickname
                })
            , "nickname").ConfigureAwait(false);
        }
        /// <inheritdoc/>
        public override async Task DeleteNicknameAsync(GId memberId) =>
            await ExecuteRequestAsync(new RestRequest($"members/{memberId}/nickname", Method.DELETE)).ConfigureAwait(false);
        /// <inheritdoc/>
        public override async Task AddRoleAsync(GId memberId, uint roleId) =>
            await ExecuteRequestAsync(new RestRequest($"members/{memberId}/roles/{roleId}", Method.PUT)).ConfigureAwait(false);
        /// <inheritdoc/>
        public override async Task RemoveRoleAsync(GId memberId, uint roleId) =>
            await ExecuteRequestAsync(new RestRequest($"members/{memberId}/roles/{roleId}", Method.DELETE)).ConfigureAwait(false);
        /// <inheritdoc/>
        public override async Task<long> AddXpAsync(GId memberId, long amount) =>
            await GetObject<long>(new RestRequest($"members/{memberId}/xp", Method.POST)
                .AddJsonBody(new
                {
                    amount
                })
            , "total").ConfigureAwait(false);
        /// <inheritdoc/>
        public override async Task AddXpAsync(uint roleId, long amount) =>
            await GetObject<long>(new RestRequest($"roles/{roleId}/xp", Method.POST)
                .AddJsonBody(new
                {
                    amount
                })
            , "total").ConfigureAwait(false);
        #endregion
    }
}