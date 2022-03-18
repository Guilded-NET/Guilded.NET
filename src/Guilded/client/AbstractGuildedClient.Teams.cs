using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Guilded.Base;

using RestSharp;

namespace Guilded;

public abstract partial class AbstractGuildedClient
{
    #region Groups
    /// <inheritdoc/>
    public override async Task AddMembershipAsync(HashId groupId, HashId memberId) =>
        await ExecuteRequestAsync(new RestRequest($"groups/{groupId}/members/{memberId}", Method.Put)).ConfigureAwait(false);
    /// <inheritdoc/>
    public override async Task RemoveMembershipAsync(HashId groupId, HashId memberId) =>
        await ExecuteRequestAsync(new RestRequest($"groups/{groupId}/members/{memberId}", Method.Delete)).ConfigureAwait(false);
    #endregion

    #region Members
    /// <inheritdoc/>
    public override async Task<IList<uint>> GetMemberRolesAsync(HashId serverId, HashId memberId) =>
        await GetResponseProperty<IList<uint>>(new RestRequest($"servers/{serverId}/members/{memberId}/roles", Method.Get), "roleIds").ConfigureAwait(false);
    /// <inheritdoc/>
    public override async Task<string> UpdateNicknameAsync(HashId serverId, HashId memberId, string nickname)
    {
        if (string.IsNullOrWhiteSpace(nickname))
            throw new ArgumentNullException(nameof(nickname));
        else if (nickname.Length > 32)
            throw new ArgumentOutOfRangeException(nameof(nickname), nickname, $"Argument {nameof(nickname)} must be 32 characters in length max");

        return await GetResponseProperty<string>(new RestRequest($"servers/{serverId}/members/{memberId}/nickname", Method.Put)
            .AddJsonBody(new
            {
                nickname
            })
        , "nickname").ConfigureAwait(false);
    }
    /// <inheritdoc/>
    public override async Task DeleteNicknameAsync(HashId serverId, HashId memberId) =>
        await ExecuteRequestAsync(new RestRequest($"servers/{serverId}/members/{memberId}/nickname", Method.Delete)).ConfigureAwait(false);
    /// <inheritdoc/>
    public override async Task AddRoleAsync(HashId serverId, HashId memberId, uint roleId) =>
        await ExecuteRequestAsync(new RestRequest($"servers/{serverId}/members/{memberId}/roles/{roleId}", Method.Put)).ConfigureAwait(false);
    /// <inheritdoc/>
    public override async Task RemoveRoleAsync(HashId serverId, HashId memberId, uint roleId) =>
        await ExecuteRequestAsync(new RestRequest($"servers/{serverId}/members/{memberId}/roles/{roleId}", Method.Delete)).ConfigureAwait(false);
    /// <inheritdoc/>
    public override async Task<long> AddXpAsync(HashId serverId, HashId memberId, long amount) =>
        await GetResponseProperty<long>(new RestRequest($"servers/{serverId}/members/{memberId}/xp", Method.Post)
            .AddJsonBody(new
            {
                amount
            })
        , "total").ConfigureAwait(false);
    /// <inheritdoc/>
    public override async Task AddXpAsync(HashId serverId, uint roleId, long amount) =>
        await GetResponseProperty<long>(new RestRequest($"servers/{serverId}/roles/{roleId}/xp", Method.Post)
            .AddJsonBody(new
            {
                amount
            })
        , "total").ConfigureAwait(false);
    #endregion
}