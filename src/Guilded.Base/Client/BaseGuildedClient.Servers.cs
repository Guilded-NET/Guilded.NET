using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base.Permissions;
using Guilded.Base.Servers;
using Guilded.Base.Users;

namespace Guilded.Base.Client;

public abstract partial class BaseGuildedClient
{
    #region Methods

    #region Methods Servers specifically
    /// <summary>
    /// Gets the specified <see cref="Server">server</see>.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> to get</param>
    /// <returns>The <see cref="Server">server</see> that was specified in the arguments</returns>
    public abstract Task<Server> GetServerAsync(HashId server);
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
    public abstract Task AddMembershipAsync(HashId group, HashId member);

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
    public abstract Task RemoveMembershipAsync(HashId group, HashId member);
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
    public abstract Task<IList<MemberSummary>> GetMembersAsync(HashId server);

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
    public abstract Task<Member> GetMemberAsync(HashId server, HashId member);

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
    public abstract Task<IList<uint>> GetMemberRolesAsync(HashId server, HashId member);

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
    /// <permission cref="CustomPermissions.ManageSelfNickname">Required when deleting <see cref="BaseGuildedClient">the client's</see> own nickname</permission>
    /// <returns>Updated <see cref="Member.Nickname">nickname</see></returns>
    public abstract Task<string> SetNicknameAsync(HashId server, HashId member, string nickname);

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
    /// <permission cref="CustomPermissions.ManageSelfNickname">Required when changing <see cref="BaseGuildedClient">the client's</see> own nickname</permission>
    public abstract Task RemoveNicknameAsync(HashId server, HashId member);

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
    /// <param name="member">The identifier of <see cref="Member">the receiving member</see></param>
    /// <param name="role">The identifier of the role to add</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="GeneralPermissions.ManageRole" />
    public abstract Task AddMemberRoleAsync(HashId server, HashId member, uint role);

    /// <summary>
    /// Removes the specified <paramref name="role" /> from the <see cref="User">user</see>.
    /// </summary>
    /// <remarks>
    /// <para>If they don't hold the specified <paramref name="role" />, then nothing happens.</para>
    /// </remarks>
    /// <param name="server">The server to modify <see cref="Member">member</see> in</param>
    /// <param name="member">The identifier of <see cref="Member">the losing member</see></param>
    /// <param name="role">The identifier of the role to remove</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="GeneralPermissions.ManageRole" />
    public abstract Task RemoveMemberRoleAsync(HashId server, HashId member, uint role);

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
    public abstract Task<long> AddXpAsync(HashId server, HashId member, short amount);

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
    public abstract Task<long> SetXpAsync(HashId server, HashId member, long total);

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
    public abstract Task AddXpAsync(HashId server, uint role, short amount);
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
    public abstract Task RemoveMemberAsync(HashId server, HashId member);

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
    public abstract Task<IList<MemberBan>> GetMemberBansAsync(HashId server);

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
    public abstract Task<MemberBan> GetMemberBanAsync(HashId server, HashId member);

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
    public abstract Task<MemberBan> AddMemberBanAsync(HashId server, HashId member, string? reason = null);

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
    public abstract Task RemoveMemberBanAsync(HashId server, HashId member);
    #endregion

    #region Methods Webhooks
    /// <summary>
    /// Gets a list of <see cref="Webhook">webhooks</see>.
    /// </summary>
    /// <remarks>
    /// <para>If <paramref name="channel" /> parameter is given, it gets all of <see cref="Webhook">the channel webhooks</see> instead.</para>
    /// </remarks>
    /// <param name="server">The identifier of the <see cref="Server">server</see> to get <see cref="Webhook">webhooks</see> from</param>
    /// <param name="channel">The identifier of the <see cref="ServerChannel">channel</see> to get webhooks from</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The list of fetched <see cref="Webhook">webhooks</see> in the specified <paramref name="channel" /></returns>
    public abstract Task<IList<Webhook>> GetWebhooksAsync(HashId server, Guid? channel = null);

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
    public abstract Task<Webhook> GetWebhookAsync(HashId server, Guid webhook);

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
    /// <returns>The <see cref="Webhook">webhook</see> that was created by the <see cref="BaseGuildedClient">client</see></returns>
    public abstract Task<Webhook> CreateWebhookAsync(HashId server, Guid channel, string name);

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
    /// <returns>The <see cref="Webhook">webhook</see> that was updated by the <see cref="BaseGuildedClient">client</see></returns>
    public abstract Task<Webhook> UpdateWebhookAsync(HashId server, Guid webhook, string name, Guid? newChannel = null);

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
    public abstract Task DeleteWebhookAsync(HashId server, Guid webhook);
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
    public abstract Task<ServerChannel> GetChannelAsync(Guid channel);

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
    /// <returns>The <see cref="ServerChannel">channel</see> that was created by the <see cref="BaseGuildedClient">client</see></returns>
    public abstract Task<ServerChannel> CreateChannelAsync(HashId server, string name, ChannelType type = ChannelType.Chat, string? topic = null, HashId? group = null, uint? category = null, bool? isPublic = null);

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
    /// <returns>The <see cref="ServerChannel">channel</see> that was updated by the <see cref="BaseGuildedClient">client</see></returns>
    public abstract Task<ServerChannel> UpdateChannelAsync(Guid channel, string? name = null, string? topic = null, bool? isPublic = null);

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
    public abstract Task DeleteChannelAsync(Guid channel);
    #endregion

    #endregion
}