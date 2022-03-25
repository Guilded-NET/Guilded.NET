using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using Guilded.Base;
using Guilded.Base.Servers;
using Guilded.Base.Users;
using RestSharp;

namespace Guilded;

public abstract partial class AbstractGuildedClient
{
    #region Groups
    /// <inheritdoc/>
    public override async Task AddMembershipAsync(HashId groupId, HashId userId) =>
        await ExecuteRequestAsync(new RestRequest($"groups/{groupId}/members/{userId}", Method.Put)).ConfigureAwait(false);
    /// <inheritdoc/>
    public override async Task RemoveMembershipAsync(HashId groupId, HashId userId) =>
        await ExecuteRequestAsync(new RestRequest($"groups/{groupId}/members/{userId}", Method.Delete)).ConfigureAwait(false);
    #endregion

    #region Members
    /// <inheritdoc/>
    public override async Task<IList<MemberSummary<UserSummary>>> GetMembersAsync(HashId serverId) =>
        await GetResponseProperty<IList<MemberSummary<UserSummary>>>(new RestRequest($"servers/{serverId}/members", Method.Get), "members").ConfigureAwait(false);
    /// <inheritdoc/>
    public override async Task<Member> GetMemberAsync(HashId serverId, HashId userId) =>
        await GetResponseProperty<Member>(new RestRequest($"servers/{serverId}/members/{userId}", Method.Get), "member").ConfigureAwait(false);
    /// <inheritdoc/>
    public override async Task<IList<uint>> GetMemberRolesAsync(HashId serverId, HashId userId) =>
        await GetResponseProperty<IList<uint>>(new RestRequest($"servers/{serverId}/members/{userId}/roles", Method.Get), "roleIds").ConfigureAwait(false);
    /// <inheritdoc/>
    public override async Task<string> UpdateNicknameAsync(HashId serverId, HashId userId, string nickname)
    {
        if (string.IsNullOrWhiteSpace(nickname))
            throw new ArgumentNullException(nameof(nickname));
        else if (nickname.Length > 32)
            throw new ArgumentOutOfRangeException(nameof(nickname), nickname, $"Argument {nameof(nickname)} must be 32 characters in length max");

        return await GetResponseProperty<string>(new RestRequest($"servers/{serverId}/members/{userId}/nickname", Method.Put)
            .AddJsonBody(new
            {
                nickname
            })
        , "nickname").ConfigureAwait(false);
    }
    /// <inheritdoc/>
    public override async Task DeleteNicknameAsync(HashId serverId, HashId userId) =>
        await ExecuteRequestAsync(new RestRequest($"servers/{serverId}/members/{userId}/nickname", Method.Delete)).ConfigureAwait(false);
    /// <inheritdoc/>
    public override async Task AddRoleAsync(HashId serverId, HashId userId, uint roleId) =>
        await ExecuteRequestAsync(new RestRequest($"servers/{serverId}/members/{userId}/roles/{roleId}", Method.Put)).ConfigureAwait(false);
    /// <inheritdoc/>
    public override async Task RemoveRoleAsync(HashId serverId, HashId userId, uint roleId) =>
        await ExecuteRequestAsync(new RestRequest($"servers/{serverId}/members/{userId}/roles/{roleId}", Method.Delete)).ConfigureAwait(false);
    /// <inheritdoc/>
    public override async Task<long> AddXpAsync(HashId serverId, HashId userId, long amount) =>
        await GetResponseProperty<long>(new RestRequest($"servers/{serverId}/members/{userId}/xp", Method.Post)
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

    #region Server-wide Moderation
    /// <inheritdoc/>
    public override async Task KickMemberAsync(HashId serverId, HashId userId) =>
        await ExecuteRequestAsync(new RestRequest($"servers/{serverId}/members/{userId}", Method.Delete)).ConfigureAwait(false);
    /// <inheritdoc/>
    public override async Task<IList<MemberBan>> GetBansAsync(HashId serverId) =>
        await GetResponseProperty<IList<MemberBan>>(new RestRequest($"servers/{serverId}/bans", Method.Get), "serverMemberBans").ConfigureAwait(false);
    /// <inheritdoc/>
    public override async Task<MemberBan> GetBanAsync(HashId serverId, HashId userId) =>
        await GetResponseProperty<MemberBan>(new RestRequest($"servers/{serverId}/bans/{userId}", Method.Get), "serverMemberBan").ConfigureAwait(false);
    /// <inheritdoc/>
    public override async Task<MemberBan> BanMemberAsync(HashId serverId, HashId userId, string? reason = null) =>
        await GetResponseProperty<MemberBan>(new RestRequest($"servers/{serverId}/bans/{userId}", Method.Post)
            .AddJsonBody(new
            {
                reason
            })
        , "serverMemberBan").ConfigureAwait(false);
    /// <inheritdoc/>
    public override async Task UnbanMemberAsync(HashId serverId, HashId userId) =>
        await ExecuteRequestAsync(new RestRequest($"servers/{serverId}/bans/{userId}", Method.Delete)).ConfigureAwait(false);
    #endregion
}