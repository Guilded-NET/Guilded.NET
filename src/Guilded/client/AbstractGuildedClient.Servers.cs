using System;
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
    /// <inheritdoc />
    public override async Task AddMembershipAsync(HashId group, HashId member) =>
        await ExecuteRequestAsync(new RestRequest($"groups/{group}/members/{member}", Method.Put)).ConfigureAwait(false);
    /// <inheritdoc />
    public override async Task RemoveMembershipAsync(HashId group, HashId member) =>
        await ExecuteRequestAsync(new RestRequest($"groups/{group}/members/{member}", Method.Delete)).ConfigureAwait(false);
    #endregion

    #region Members
    /// <inheritdoc />
    public override async Task<IList<MemberSummary<UserSummary>>> GetMembersAsync(HashId server) =>
        await GetResponseProperty<IList<MemberSummary<UserSummary>>>(new RestRequest($"servers/{server}/members", Method.Get), "members").ConfigureAwait(false);
    /// <inheritdoc />
    public override async Task<Member> GetMemberAsync(HashId server, HashId member) =>
        await GetResponseProperty<Member>(new RestRequest($"servers/{server}/members/{member}", Method.Get), "member").ConfigureAwait(false);
    /// <inheritdoc />
    public override async Task<IList<uint>> GetMemberRolesAsync(HashId server, HashId member) =>
        await GetResponseProperty<IList<uint>>(new RestRequest($"servers/{server}/members/{member}/roles", Method.Get), "roleIds").ConfigureAwait(false);
    /// <inheritdoc />
    public override async Task<string> UpdateNicknameAsync(HashId server, HashId member, string nickname)
    {
        if (string.IsNullOrWhiteSpace(nickname))
            throw new ArgumentNullException(nameof(nickname));
        else if (nickname.Length > 32)
            throw new ArgumentOutOfRangeException(nameof(nickname), nickname, $"Argument {nameof(nickname)} must be 32 characters in length max");

        return await GetResponseProperty<string>(new RestRequest($"servers/{server}/members/{member}/nickname", Method.Put)
            .AddJsonBody(new
            {
                nickname
            })
        , "nickname").ConfigureAwait(false);
    }
    /// <inheritdoc />
    public override async Task DeleteNicknameAsync(HashId server, HashId member) =>
        await ExecuteRequestAsync(new RestRequest($"servers/{server}/members/{member}/nickname", Method.Delete)).ConfigureAwait(false);
    /// <inheritdoc />
    public override async Task AddRoleAsync(HashId server, HashId member, uint role) =>
        await ExecuteRequestAsync(new RestRequest($"servers/{server}/members/{member}/roles/{role}", Method.Put)).ConfigureAwait(false);
    /// <inheritdoc />
    public override async Task RemoveRoleAsync(HashId server, HashId member, uint role) =>
        await ExecuteRequestAsync(new RestRequest($"servers/{server}/members/{member}/roles/{role}", Method.Delete)).ConfigureAwait(false);
    /// <inheritdoc />
    public override async Task<long> AddXpAsync(HashId server, HashId member, long amount) =>
        await GetResponseProperty<long>(new RestRequest($"servers/{server}/members/{member}/xp", Method.Post)
            .AddJsonBody(new
            {
                amount
            })
        , "total").ConfigureAwait(false);
    /// <inheritdoc />
    public override async Task AddXpAsync(HashId server, uint role, long amount) =>
        await GetResponseProperty<long>(new RestRequest($"servers/{server}/roles/{role}/xp", Method.Post)
            .AddJsonBody(new
            {
                amount
            })
        , "total").ConfigureAwait(false);
    #endregion

    #region Server-wide Moderation
    /// <inheritdoc />
    public override async Task KickMemberAsync(HashId server, HashId member) =>
        await ExecuteRequestAsync(new RestRequest($"servers/{server}/members/{member}", Method.Delete)).ConfigureAwait(false);
    /// <inheritdoc />
    public override async Task<IList<MemberBan>> GetBansAsync(HashId server) =>
        await GetResponseProperty<IList<MemberBan>>(new RestRequest($"servers/{server}/bans", Method.Get), "serverMemberBans").ConfigureAwait(false);
    /// <inheritdoc />
    public override async Task<MemberBan> GetBanAsync(HashId server, HashId member) =>
        await GetResponseProperty<MemberBan>(new RestRequest($"servers/{server}/bans/{member}", Method.Get), "serverMemberBan").ConfigureAwait(false);
    /// <inheritdoc />
    public override async Task<MemberBan> BanMemberAsync(HashId server, HashId member, string? reason = null) =>
        await GetResponseProperty<MemberBan>(new RestRequest($"servers/{server}/bans/{member}", Method.Post)
            .AddJsonBody(new
            {
                reason
            })
        , "serverMemberBan").ConfigureAwait(false);
    /// <inheritdoc />
    public override async Task UnbanMemberAsync(HashId server, HashId member) =>
        await ExecuteRequestAsync(new RestRequest($"servers/{server}/bans/{member}", Method.Delete)).ConfigureAwait(false);
    #endregion

    #region Webhooks
    /// <inheritdoc />
    public override async Task<IList<Webhook>> GetWebhooksAsync(HashId server, Guid? channel = null) =>
        await GetResponseProperty<IList<Webhook>>(new RestRequest($"servers/{server}/webhooks", Method.Get)
            .AddOptionalQuery("channelId", channel)
        , "webhooks").ConfigureAwait(false);
    /// <inheritdoc />
    public override async Task<Webhook> GetWebhookAsync(HashId server, Guid webhook) =>
        await GetResponseProperty<Webhook>(new RestRequest($"servers/{server}/webhooks/{webhook}", Method.Get), "webhook").ConfigureAwait(false);
    /// <inheritdoc />
    public override async Task<Webhook> CreateWebhookAsync(HashId server, Guid channel, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));

        return await GetResponseProperty<Webhook>(new RestRequest($"servers/{server}/webhooks", Method.Post)
            .AddJsonBody(new
            {
                name,
                channelId = channel
            })
        , "webhook").ConfigureAwait(false);
    }
    /// <inheritdoc />
    public override async Task<Webhook> UpdateWebhookAsync(HashId server, Guid webhook, string name, Guid? newChannel = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));

        return await GetResponseProperty<Webhook>(new RestRequest($"servers/{server}/webhooks/{webhook}", Method.Put)
            .AddJsonBody(new
            {
                name,
                channelId = newChannel
            })
        , "webhook").ConfigureAwait(false);
    }
    /// <inheritdoc />
    public override async Task DeleteWebhookAsync(HashId server, Guid webhook) =>
        await ExecuteRequestAsync(new RestRequest($"servers/{server}/webhooks/{webhook}", Method.Delete)).ConfigureAwait(false);
    #endregion

    #region Channels
    /// <inheritdoc />
    public override async Task<ServerChannel> GetChannelAsync(Guid channel) =>
        await GetResponseProperty<ServerChannel>(new RestRequest($"channels/{channel}", Method.Get), "channel").ConfigureAwait(false);
    /// <inheritdoc />
    public override async Task<ServerChannel> CreateChannelAsync(HashId server, string name, ChannelType type = ChannelType.Chat, string? topic = null, HashId? group = null, uint? category = null, bool? isPublic = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));

        return await GetResponseProperty<ServerChannel>(new RestRequest($"channel", Method.Post)
            .AddJsonBody(new
            {
                serverId = server,
                groupId = group,
                categoryId = category,
                name,
                type,
                topic,
                isPublic
            })
        , "channel").ConfigureAwait(false);
    }
    /// <inheritdoc />
    public override async Task DeleteChannelAsync(Guid channel) =>
        await ExecuteRequestAsync(new RestRequest($"channels/{channel}", Method.Delete)).ConfigureAwait(false);
    #endregion
}