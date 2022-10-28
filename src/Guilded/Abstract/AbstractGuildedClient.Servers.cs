using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Permissions;
using Guilded.Servers;
using Guilded.Users;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Guilded.Client;

public abstract partial class AbstractGuildedClient
{
    #region Methods Servers specifically
    /// <summary>
    /// Gets the specified <see cref="Server">server</see>.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> to get</param>
    /// <returns>The <see cref="Server">server</see> that was specified in the arguments</returns>
    public Task<Server> GetServerAsync(HashId server) =>
        GetResponsePropertyAsync<Server>(new RestRequest($"servers/{server}", Method.Get), "server");
    #endregion

    #region Methods Groups
    /// <summary>
    /// Adds the <paramref name="member" /> to the <paramref name="group" />.
    /// </summary>
    /// <remarks>
    /// <para>This allows the <paramref name="member" /> to interact or see the specified group.</para>
    /// </remarks>
    /// <param name="group">The identifier of the parent group</param>
    /// <param name="member">The identifier of the <see cref="Member">member</see> to add</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="GeneralPermissions.ManageGroup" />
    public Task AddMembershipAsync(HashId group, HashId member) =>
        ExecuteRequestAsync(new RestRequest($"groups/{group}/members/{member}", Method.Put));

    /// <summary>
    /// Removes the <paramref name="member" /> from the <paramref name="group" />.
    /// </summary>
    /// <remarks>
    /// <para>This disallows the <paramref name="member" /> to interact or see the specified group.</para>
    /// </remarks>
    /// <param name="group">The identifier of the parent group</param>
    /// <param name="member">The identifier of the <see cref="Member">member</see> to remove</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="GeneralPermissions.ManageGroup" />
    public Task RemoveMembershipAsync(HashId group, HashId member) =>
        ExecuteRequestAsync(new RestRequest($"groups/{group}/members/{member}", Method.Delete));
    #endregion

    #region Methods Members
    /// <summary>
    /// Gets the list of all <paramref name="server" /> <see cref="Member">members</see>.
    /// </summary>
    /// <param name="server">The server to get <see cref="Member">member</see> list of</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The list of fetched <see cref="Member">members</see> in the specified <paramref name="server" /></returns>
    public Task<IList<MemberSummary>> GetMembersAsync(HashId server) =>
        TransformListResponseAsync(new RestRequest($"servers/{server}/members", Method.Get), "members", x =>
        {
            // Add serverId property to them
            x.Add("serverId", JValue.CreateString(server.ToString()));
            return x.ToObject<MemberSummary>(GuildedSerializer)!;
        });

    /// <summary>
    /// Gets full information about the specified <paramref name="member" />.
    /// </summary>
    /// <param name="server">The server where the <see cref="Member">member</see> is</param>
    /// <param name="member">The identifier of the <see cref="Member">member</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns><paramref name="member">Specified member</paramref></returns>
    public Task<Member> GetMemberAsync(HashId server, HashId member) =>
        TransformResponseAsync<Member>(new RestRequest($"servers/{server}/members/{member}", Method.Get), "member", value =>
        {
            value.Add("serverId", JValue.CreateString(server.ToString()));

            return value;
        });

    /// <summary>
    /// Gets the list of roles the specified <paramref name="member" /> holds.
    /// </summary>
    /// <remarks>
    /// <para>No permissions are required.</para>
    /// </remarks>
    /// <param name="server">The server where to fetch user's information</param>
    /// <param name="member">The identifier of the role holder</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>List of role IDs</returns>
    public Task<IList<uint>> GetMemberRolesAsync(HashId server, HashId member) =>
        GetResponsePropertyAsync<IList<uint>>(new RestRequest($"servers/{server}/members/{member}/roles", Method.Get), "roleIds");

    /// <summary>
    /// Changes the <see cref="Member.Nickname">nickname</see> of the specified <paramref name="member" />.
    /// </summary>
    /// <param name="server">The server to modify member in</param>
    /// <param name="member">The identifier of the member to update</param>
    /// <param name="nickname">The new nickname of the member</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="CustomPermissions.ManageMemberNickname">Required when deleting nicknames of <see cref="Member">other members</see></permission>
    /// <permission cref="CustomPermissions.ManageSelfNickname">Required when deleting the <see cref="AbstractGuildedClient">client's</see> own nickname</permission>
    /// <returns>Updated <see cref="Member.Nickname">nickname</see></returns>
    public Task<string> SetNicknameAsync(HashId server, HashId member, string nickname) =>
        string.IsNullOrWhiteSpace(nickname)
        ? throw new ArgumentNullException(nameof(nickname))
        : nickname.Length > 32
        ? throw new ArgumentOutOfRangeException(nameof(nickname), nickname, $"Argument {nameof(nickname)} must be 32 characters in length max")
        : GetResponsePropertyAsync<string>(new RestRequest($"servers/{server}/members/{member}/nickname", Method.Put)
            .AddJsonBody(new
            {
                nickname
            })
        , "nickname");

    /// <inheritdoc cref="SetNicknameAsync(HashId, HashId, string)" />
    [Obsolete("Use `SetNicknameAsync` instead")]
    public Task<string> UpdateNicknameAsync(HashId server, HashId member, string nickname) =>
        UpdateNicknameAsync(server, member, nickname);

    /// <summary>
    /// Removes the <see cref="Member.Nickname">nickname</see> of the specified <paramref name="member" />.
    /// </summary>
    /// <param name="server">The server to modify <see cref="Member">member</see> in</param>
    /// <param name="member">The identifier of the <see cref="Member">member</see> to update</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="CustomPermissions.ManageMemberNickname">Required when changing nicknames of <see cref="Member">other members</see></permission>
    /// <permission cref="CustomPermissions.ManageSelfNickname">Required when changing the <see cref="AbstractGuildedClient">client's</see> own nickname</permission>
    public Task RemoveNicknameAsync(HashId server, HashId member) =>
        ExecuteRequestAsync(new RestRequest($"servers/{server}/members/{member}/nickname", Method.Delete));

    /// <inheritdoc cref="RemoveNicknameAsync(HashId, HashId)" />
    [Obsolete("Use `SetNicknameAsync` instead")]
    public Task DeleteNicknameAsync(HashId server, HashId member) =>
        RemoveNicknameAsync(server, member);

    /// <summary>
    /// Adds a <paramref name="role" /> to the <see cref="User">user</see>.
    /// </summary>
    /// <remarks>
    /// <para>If they hold the specified <paramref name="role" />, then nothing happens.</para>
    /// </remarks>
    /// <param name="server">The server to modify <see cref="Member">member</see> in</param>
    /// <param name="member">The identifier of the receiving <see cref="Member">member</see></param>
    /// <param name="role">The identifier of the role to add</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="GeneralPermissions.ManageRole" />
    public Task AddMemberRoleAsync(HashId server, HashId member, uint role) =>
        ExecuteRequestAsync(new RestRequest($"servers/{server}/members/{member}/roles/{role}", Method.Put));

    /// <summary>
    /// Removes the specified <paramref name="role" /> from the <see cref="User">user</see>.
    /// </summary>
    /// <remarks>
    /// <para>If they don't hold the specified <paramref name="role" />, then nothing happens.</para>
    /// </remarks>
    /// <param name="server">The server to modify <see cref="Member">member</see> in</param>
    /// <param name="member">The identifier of the losing <see cref="Member">member</see></param>
    /// <param name="role">The identifier of the role to remove</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="GeneralPermissions.ManageRole" />
    public Task RemoveMemberRoleAsync(HashId server, HashId member, uint role) =>
        ExecuteRequestAsync(new RestRequest($"servers/{server}/members/{member}/roles/{role}", Method.Delete));

    /// <summary>
    /// Gives the specified <paramref name="amount" /> of XP to the specified <paramref name="member" />.
    /// </summary>
    /// <param name="server">The server to modify <see cref="Member">member</see> in</param>
    /// <param name="member">The identifier of the receiving <see cref="Member">member</see></param>
    /// <param name="amount">The amount of XP received (values — <c>[-1000, 1000]</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <exception cref="ArgumentOutOfRangeException">When the amount of XP given exceeds the limit</exception>
    /// <permission cref="XpPermissions.ManageXp" />
    /// <returns>The total amount of XP that the <see cref="Member">member</see> has</returns>
    public Task<long> AddXpAsync(HashId server, HashId member, short amount) =>
        amount is > 1000 or < -1000
        ? throw new ArgumentOutOfRangeException(nameof(amount), amount, "Cannot add more than 1000 and less than -1000 XP")
        : GetResponsePropertyAsync<long>(new RestRequest($"servers/{server}/members/{member}/xp", Method.Post)
            .AddJsonBody(new
            {
                amount
            })
        , "total");

    /// <summary>
    /// Sets how much <paramref name="total" /> XP the specified <paramref name="member" /> will have.
    /// </summary>
    /// <param name="server">The server to modify <see cref="Member">member</see> in</param>
    /// <param name="member">The identifier of the <see cref="Member">member</see> who is being modified</param>
    /// <param name="total">The amount of XP the <see cref="Member">member</see> should have (values — <c>[-1000000000, 1000000000]</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <exception cref="ArgumentOutOfRangeException">When the amount of XP given exceeds the limit</exception>
    /// <permission cref="XpPermissions.ManageXp" />
    /// <returns>The <paramref name="total" /> amount of XP that the <see cref="Member">member</see> has</returns>
    public Task<long> SetXpAsync(HashId server, HashId member, long total) =>
        total is > 1000 or < -1000
        ? throw new ArgumentOutOfRangeException(nameof(total), total, "Cannot add more than 1000000000 and less than -1000000000 XP")
        : GetResponsePropertyAsync<long>(new RestRequest($"servers/{server}/members/{member}/xp", Method.Put)
            .AddJsonBody(new
            {
                total
            })
        , "total");

    /// <summary>
    /// Gives the specified <paramref name="amount" /> of XP to the specified <paramref name="role">role's</paramref> members.
    /// </summary>
    /// <param name="server">The server where the role is</param>
    /// <param name="role">The identifier of the receiving role</param>
    /// <param name="amount">The amount of XP received (values — <c>[-1000, 1000]</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="XpPermissions.ManageXp" />
    public Task AddXpAsync(HashId server, uint role, short amount) =>
        amount is > 1000 or < -1000
        ? throw new ArgumentOutOfRangeException(nameof(amount), amount, "Cannot add more than 1000 and less than -1000 XP")
        : ExecuteRequestAsync(new RestRequest($"servers/{server}/roles/{role}/xp", Method.Post)
            .AddJsonBody(new
            {
                amount
            })
        );
    #endregion

    #region Methods Server-wide Moderation
    /// <summary>
    /// Removes the specified <paramref name="member" /> from the <paramref name="server" />.
    /// </summary>
    /// <param name="server">The server to kick the <see cref="Member">member</see> from</param>
    /// <param name="member">The identifier of the <see cref="Member">member</see> to kick</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="GeneralPermissions.RemoveMember" />
    public Task RemoveMemberAsync(HashId server, HashId member) =>
        ExecuteRequestAsync(new RestRequest($"servers/{server}/members/{member}", Method.Delete));

    /// <summary>
    /// Gets the list of <paramref name="server">server's</paramref> bans.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> to get bans of</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="GeneralPermissions.RemoveMember" />
    /// <returns>The list of fetched <see cref="MemberBan">member bans</see> in the specified <paramref name="server" /></returns>
    public Task<IList<MemberBan>> GetMemberBansAsync(HashId server) =>
        TransformListResponseAsync(new RestRequest($"servers/{server}/bans", Method.Get), "serverMemberBans", value =>
        {
            value.Add("serverId", JValue.CreateString(server.ToString()));
            return value.ToObject<MemberBan>(GuildedSerializer)!;
        });

    /// <inheritdoc cref="GetMemberBansAsync(HashId)" />
    [Obsolete("Use `GetMemberBansAsync` instead")]
    public Task<IList<MemberBan>> GetBansAsync(HashId server) =>
        GetMemberBansAsync(server);

    /// <summary>
    /// Gets the information about the <see cref="MemberBan">ban</see> of the <paramref name="member" />.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="User">user</see> has been banned</param>
    /// <param name="member">The identifier of the <see cref="Member">member</see> to get ban information of</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="GeneralPermissions.RemoveMember" />
    /// <returns>The <see cref="MemberBan">ban</see> of the <see cref="Member">member</see> that was specified in the arguments</returns>
    public Task<MemberBan> GetMemberBanAsync(HashId server, HashId member) =>
        TransformResponseAsync<MemberBan>(new RestRequest($"servers/{server}/bans/{member}", Method.Get), "serverMemberBan", token =>
        {
            token.Add("serverId", JValue.CreateString(server.ToString()));
            return token;
        });

    /// <inheritdoc cref="GetMemberBanAsync(HashId, HashId)" />
    [Obsolete("Use `GetMemberBanAsync` instead")]
    public Task<IList<MemberBan>> GetBanAsync(HashId server) =>
        GetMemberBansAsync(server);

    /// <summary>
    /// Bans the specified <paramref name="member" />.
    /// </summary>
    /// <remarks>
    /// <para>Disallows them from joining again, until they receive an unban with <see cref="RemoveMemberBanAsync" /> method.</para>
    /// </remarks>
    /// <param name="server">The identifier of the <see cref="Server">server</see> to ban member from</param>
    /// <param name="member">The identifier of the <see cref="Member">member</see> to ban</param>
    /// <param name="reason">The reason for a ban</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="GeneralPermissions.RemoveMember" />
    /// <returns>Created <see cref="MemberBan">member's ban</see></returns>
    public Task<MemberBan> AddMemberBanAsync(HashId server, HashId member, string? reason = null) =>
        TransformResponseAsync<MemberBan>(
            new RestRequest($"servers/{server}/bans/{member}", Method.Post).AddJsonBody(new { reason }),
            "serverMemberBan",
            value =>
            {
                value.Add("serverId", JValue.CreateString(server.ToString()));
                return value;
            }
        );

    /// <summary>
    /// Unbans the specified <paramref name="member" />.
    /// </summary>
    /// <remarks>
    /// <para>Allows them to join the <see cref="Server">server</see> again.</para>
    /// </remarks>
    /// <param name="server">The identifier of the <see cref="Server">server</see> to unban <see cref="Member">member</see> in</param>
    /// <param name="member">The identifier of the <see cref="Member">member</see> to unban</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="GeneralPermissions.RemoveMember" />
    public Task RemoveMemberBanAsync(HashId server, HashId member) =>
        ExecuteRequestAsync(new RestRequest($"servers/{server}/bans/{member}", Method.Delete));
    #endregion

    #region Methods Webhooks
    /// <summary>
    /// Gets a list of <see cref="Webhook">webhooks</see>.
    /// </summary>
    /// <remarks>
    /// <para>If <paramref name="channel" /> parameter is given, it gets all of the channel <see cref="Webhook">webhooks</see> instead.</para>
    /// </remarks>
    /// <param name="server">The identifier of the <see cref="Server">server</see> to get <see cref="Webhook">webhooks</see> from</param>
    /// <param name="channel">The identifier of the <see cref="ServerChannel">channel</see> to get webhooks from</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The list of fetched <see cref="Webhook">webhooks</see> in the specified <paramref name="channel" /></returns>
    public Task<IList<Webhook>> GetWebhooksAsync(HashId server, Guid? channel = null) =>
        GetResponsePropertyAsync<IList<Webhook>>(new RestRequest($"servers/{server}/webhooks", Method.Get)
            .AddOptionalQuery("channelId", channel)
        , "webhooks");

    /// <summary>
    /// Gets the specified <paramref name="webhook" />.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> the <see cref="Webhook">webhook</see> is</param>
    /// <param name="webhook">The identifier of the <see cref="Webhook">webhook</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The <see cref="Webhook">webhook</see> that was specified in the arguments</returns>
    public Task<Webhook> GetWebhookAsync(HashId server, Guid webhook) =>
        GetResponsePropertyAsync<Webhook>(new RestRequest($"servers/{server}/webhooks/{webhook}", Method.Get), "webhook");

    /// <summary>
    /// Creates a new <see cref="Webhook">webhook</see> in the specified <paramref name="channel" />.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Webhook">webhook</see> will be created</param>
    /// <param name="channel">The identifier of the <see cref="ServerChannel">channel</see> where the <see cref="Webhook">webhook</see> will be created</param>
    /// <param name="name">The name of the <see cref="Webhook">webhook</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <exception cref="ArgumentNullException">The specified <paramref name="name" /> is null, empty or whitespace</exception>
    /// <permission cref="GeneralPermissions.ManageWebhook" />
    /// <returns>The <see cref="Webhook">webhook</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Webhook> CreateWebhookAsync(HashId server, Guid channel, string name) =>
        string.IsNullOrWhiteSpace(name)
        ? throw new ArgumentNullException(nameof(name))
        : GetResponsePropertyAsync<Webhook>(new RestRequest($"servers/{server}/webhooks", Method.Post)
            .AddJsonBody(new
            {
                name,
                channelId = channel
            })
        , "webhook");

    /// <summary>
    /// Edits the specified <paramref name="webhook" />.
    /// </summary>
    /// <remarks>
    /// <para><see cref="Webhook" /> can moved between <see cref="ServerChannel">channels</see> using '<paramref name="newChannel" />' parameter.</para>
    /// </remarks>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Webhook">webhook</see> is</param>
    /// <param name="webhook">The identifier of the <see cref="Webhook">webhook</see> to update</param>
    /// <param name="name">The new name of the <see cref="Webhook">webhook</see></param>
    /// <param name="newChannel">The identifier of the <see cref="ServerChannel">channel</see> where the <see cref="Webhook">webhook</see> will be moved to</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="GeneralPermissions.ManageWebhook" />
    /// <exception cref="ArgumentNullException">The specified <paramref name="name" /> is null, empty or whitespace</exception>
    /// <returns>The <see cref="Webhook">webhook</see> that was updated by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Webhook> UpdateWebhookAsync(HashId server, Guid webhook, string name, Guid? newChannel = null) =>
        string.IsNullOrWhiteSpace(name)
        ? throw new ArgumentNullException(nameof(name))
        : GetResponsePropertyAsync<Webhook>(new RestRequest($"servers/{server}/webhooks/{webhook}", Method.Put)
            .AddJsonBody(new
            {
                name,
                channelId = newChannel
            })
        , "webhook");

    /// <summary>
    /// Deletes the specified <paramref name="webhook" />.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Webhook">webhook</see> is</param>
    /// <param name="webhook">The identifier of the <see cref="Webhook">webhook</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="GeneralPermissions.ManageWebhook" />
    public Task DeleteWebhookAsync(HashId server, Guid webhook) =>
        ExecuteRequestAsync(new RestRequest($"servers/{server}/webhooks/{webhook}", Method.Delete));
    #endregion

    #region Methods Channels
    /// <summary>
    /// Gets the specified <paramref name="channel" />.
    /// </summary>
    /// <param name="channel">The identifier of the <see cref="ServerChannel">channel</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The <see cref="ServerChannel">channel</see> that was specified in the arguments</returns>
    public Task<ServerChannel> GetChannelAsync(Guid channel) =>
        GetResponsePropertyAsync<ServerChannel>(new RestRequest($"channels/{channel}", Method.Get), "channel");

    /// <summary>
    /// Creates a new channel in the specified <paramref name="server" />.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="ServerChannel">channel</see> will be created</param>
    /// <param name="name">The name of the <see cref="ServerChannel">channel</see> (max — <c>100</c>)</param>
    /// <param name="type">The type of the content that the <see cref="ServerChannel">channel</see> will hold</param>
    /// <param name="topic">The topic describing what the <see cref="ServerChannel">channel</see> is about (max — <c>512</c>)</param>
    /// <param name="group">The identifier of the group where the <see cref="ServerChannel">channel</see> will be created</param>
    /// <param name="category">The identifier of the category where the <see cref="ServerChannel">channel</see> will be created</param>
    /// <param name="isPublic">Whether the contents of the channel are publicly viewable</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <exception cref="ArgumentNullException">The specified <paramref name="name" /> is null, empty or whitespace</exception>
    /// <permission cref="GeneralPermissions.ManageChannel" />
    /// <returns>The <see cref="ServerChannel">channel</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<ServerChannel> CreateChannelAsync(HashId server, string name, ChannelType type = ChannelType.Chat, string? topic = null, HashId? group = null, uint? category = null, bool? isPublic = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));

        EnforceLimit(nameof(name), name, ServerChannel.NameLimit);
        EnforceLimitOnNullable(nameof(topic), topic, ServerChannel.TopicLimit);

        return GetResponsePropertyAsync<ServerChannel>(new RestRequest($"channels", Method.Post)
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
        , "channel");
    }

    /// <summary>
    /// Updates the specified <paramref name="channel" />.
    /// </summary>
    /// <param name="channel">The identifier of the <see cref="ServerChannel">channel</see> to update</param>
    /// <param name="name">A new name of the <see cref="ServerChannel">channel</see> (max — <c>100</c>)</param>
    /// <param name="topic">A new topic describing what the <see cref="ServerChannel">channel</see> is about (max — <c>512</c>)</param>
    /// <param name="isPublic">Whether the contents of the channel are publicly viewable</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="GeneralPermissions.ManageChannel" />
    /// <returns>The <see cref="ServerChannel">channel</see> that was updated by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<ServerChannel> UpdateChannelAsync(Guid channel, string? name = null, string? topic = null, bool? isPublic = null)
    {
        return GetResponsePropertyAsync<ServerChannel>(new RestRequest($"channels/{channel}", Method.Patch)
            .AddJsonBody(new
            {
                name,
                topic,
                isPublic
            })
        , "channel");
    }

    /// <summary>
    /// Deletes the specified <paramref name="channel" />.
    /// </summary>
    /// <param name="channel">The identifier of the channel to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="GeneralPermissions.ManageChannel" />
    public Task DeleteChannelAsync(Guid channel) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}", Method.Delete));
    #endregion
}