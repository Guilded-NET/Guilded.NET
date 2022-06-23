using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base.Permissions;
using Guilded.Base.Servers;
using Guilded.Base.Users;

namespace Guilded.Base;

public abstract partial class BaseGuildedClient
{
    #region Methods

    #region Groups
    /// <summary>
    /// Adds <paramref name="user">the member</paramref> to <paramref name="group">the group</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>This allows <paramref name="user">the member</paramref> to interact or see the specified group.</para>
    /// </remarks>
    /// <param name="group">The identifier of the parent group</param>
    /// <param name="user">The identifier of <see cref="Member">the member</see> to add</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="GeneralPermissions.ManageGroups" />
    public abstract Task AddMembershipAsync(HashId group, HashId user);

    /// <summary>
    /// Removes <paramref name="user">the member</paramref> from <paramref name="group">the group</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>This disallows <paramref name="user">the member</paramref> to interact or see the specified group.</para>
    /// </remarks>
    /// <param name="group">The identifier of the parent group</param>
    /// <param name="user">The identifier of <see cref="Member">the member</see> to remove</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="GeneralPermissions.ManageGroups" />
    public abstract Task RemoveMembershipAsync(HashId group, HashId user);
    #endregion

    #region Members
    /// <summary>
    /// Gets the list of all <paramref name="server" /> <see cref="Member">members</see>.
    /// </summary>
    /// <param name="server">The server to get <see cref="Member">member</see> list of</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>List of <see cref="Member">members</see></returns>
    public abstract Task<IList<MemberSummary>> GetMembersAsync(HashId server);

    /// <summary>
    /// Gets full information about the <paramref name="member">specified member</paramref>.
    /// </summary>
    /// <param name="server">The server where <see cref="Member">the member</see> is</param>
    /// <param name="member">The identifier of <see cref="Member">the member</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns><paramref name="member">Specified member</paramref></returns>
    public abstract Task<Member> GetMemberAsync(HashId server, HashId member);

    /// <summary>
    /// Gets the list of roles <paramref name="member">the specified member</paramref> holds.
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
    /// Changes <see cref="Member.Nickname">the nickname</see> of the <paramref name="member">specified member</paramref>.
    /// </summary>
    /// <param name="server">The server to modify member in</param>
    /// <param name="member">The identifier of the member to update</param>
    /// <param name="nickname">The new nickname of the member</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="CustomPermissions.ManageNicknames">Required when deleting nicknames of <see cref="Member">other members</see></permission>
    /// <permission cref="CustomPermissions.ChangeNickname">Required when deleting <see cref="BaseGuildedClient">the client's</see> own nickname</permission>
    /// <returns>Updated <see cref="Member.Nickname">nickname</see></returns>
    public abstract Task<string> UpdateNicknameAsync(HashId server, HashId member, string nickname);

    /// <summary>
    /// Removes <see cref="Member.Nickname">the nickname</see> of the <paramref name="member">specified member</paramref>.
    /// </summary>
    /// <param name="server">The server to modify <see cref="Member">member</see> in</param>
    /// <param name="member">The identifier of <see cref="Member">the member</see> to update</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="CustomPermissions.ManageNicknames">Required when changing nicknames of <see cref="Member">other members</see></permission>
    /// <permission cref="CustomPermissions.ChangeNickname">Required when changing <see cref="BaseGuildedClient">the client's</see> own nickname</permission>
    public abstract Task DeleteNicknameAsync(HashId server, HashId member);

    /// <summary>
    /// Adds a <paramref name="role" /> to <see cref="User">the user</see>.
    /// </summary>
    /// <remarks>
    /// <para>If they hold the <paramref name="role">specified role</paramref>, then nothing happens.</para>
    /// </remarks>
    /// <param name="server">The server to modify <see cref="Member">member</see> in</param>
    /// <param name="member">The identifier of <see cref="Member">the receiving member</see></param>
    /// <param name="role">The identifier of the role to add</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="GeneralPermissions.ManageRoles" />
    public abstract Task AddRoleAsync(HashId server, HashId member, uint role);

    /// <summary>
    /// Removes <paramref name="role">the specified role</paramref> from <see cref="User">the user</see>.
    /// </summary>
    /// <remarks>
    /// <para>If they don't hold the <paramref name="role">specified role</paramref>, then nothing happens.</para>
    /// </remarks>
    /// <param name="server">The server to modify <see cref="Member">member</see> in</param>
    /// <param name="member">The identifier of <see cref="Member">the losing member</see></param>
    /// <param name="role">The identifier of the role to remove</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="GeneralPermissions.ManageRoles" />
    public abstract Task RemoveRoleAsync(HashId server, HashId member, uint role);

    /// <summary>
    /// Gives <paramref name="amount">XP</paramref> to the specified <paramref name="member" />.
    /// </summary>
    /// <param name="server">The server to modify <see cref="Member">member</see> in</param>
    /// <param name="member">The identifier of <see cref="Member">the receiving member</see></param>
    /// <param name="amount">The amount of XP received (values — <c>[-1000, 1000]</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <exception cref="ArgumentOutOfRangeException">When the amount of XP given exceeds the limit</exception>
    /// <permission cref="XpPermissions.ManageServerXp" />
    /// <returns>Total XP</returns>
    public abstract Task<long> AddXpAsync(HashId server, HashId member, long amount);

    /// <summary>
    /// Gives <paramref name="amount">XP</paramref> to the <paramref name="role">specified role's</paramref> members.
    /// </summary>
    /// <param name="server">The server where the role is</param>
    /// <param name="role">The identifier of the receiving role</param>
    /// <param name="amount">The amount of XP received (values — <c>[-1000, 1000]</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="XpPermissions.ManageServerXp" />
    public abstract Task AddXpAsync(HashId server, uint role, long amount);
    #endregion

    #region Server-wide Moderation
    /// <summary>
    /// Removes the <paramref name="member">specified member</paramref> from the <paramref name="server" />.
    /// </summary>
    /// <param name="server">The server to kick <see cref="Member">the member</see> from</param>
    /// <param name="member">The identifier of <see cref="Member">the member</see> to kick</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="GeneralPermissions.KickBanMembers" />
    public abstract Task RemoveMemberAsync(HashId server, HashId member);

    /// <summary>
    /// Gets the list of <paramref name="server">server's</paramref> bans.
    /// </summary>
    /// <param name="server">The server to get bans of</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="GeneralPermissions.KickBanMembers" />
    /// <returns>List of <see cref="MemberBan">Member ban information</see></returns>
    public abstract Task<IList<MemberBan>> GetBansAsync(HashId server);

    /// <summary>
    /// Gets <see cref="MemberBan">the information</see> about the ban of <paramref name="member">the member</paramref>.
    /// </summary>
    /// <param name="server">The server where <see cref="User">the user</see> has been banned</param>
    /// <param name="member">The identifier of <see cref="Member">the member</see> to get ban information of</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="GeneralPermissions.KickBanMembers" />
    /// <returns><paramref name="member">Specified member's</paramref> ban</returns>
    public abstract Task<MemberBan> GetBanAsync(HashId server, HashId member);

    /// <summary>
    /// Bans the <paramref name="member">specified member</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>Disallows them from joining again, until they receive an unban with <see cref="RemoveMemberBanAsync" /> method.</para>
    /// </remarks>
    /// <param name="server">The server to ban member from</param>
    /// <param name="member">The identifier of <see cref="Member">the member</see> to ban</param>
    /// <param name="reason">The reason for a ban</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="GeneralPermissions.KickBanMembers" />
    /// <returns>Created <see cref="MemberBan">member's ban</see></returns>
    public abstract Task<MemberBan> AddMemberBanAsync(HashId server, HashId member, string? reason = null);

    /// <summary>
    /// Unbans the <paramref name="member">specified member</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>Allows them to join <see cref="Server">the server</see> again.</para>
    /// </remarks>
    /// <param name="server">The server to unban <see cref="Member">member</see> in</param>
    /// <param name="member">The identifier of <see cref="Member">the member</see> to unban</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="GeneralPermissions.KickBanMembers" />
    public abstract Task RemoveMemberBanAsync(HashId server, HashId member);

    /// <inheritdoc cref="RemoveMemberAsync(HashId, HashId)" />
    [Obsolete("Use `RemoveMemberAsync` (this is for consistency reason)")]
    public async Task KickMemberAsync(HashId server, HashId member) =>
        await RemoveMemberAsync(server, member).ConfigureAwait(false);

    /// <inheritdoc cref="AddMemberBanAsync(HashId, HashId, string)" />
    [Obsolete("Use `AddMemberBanAsync` (this is for consistency reason)")]
    public async Task BanMemberAsync(HashId server, HashId member, string? reason = null) =>
        await AddMemberBanAsync(server, member, reason).ConfigureAwait(false);

    /// <inheritdoc cref="RemoveMemberBanAsync(HashId, HashId)" />
    [Obsolete("Use `RemoveMemberBanAsync` (this is for consistency reason)")]
    public async Task UnbanMemberAsync(HashId server, HashId member) =>
        await RemoveMemberBanAsync(server, member).ConfigureAwait(false);
    #endregion

    #region Webhooks
    /// <summary>
    /// Gets a list of <see cref="Webhook">webhooks</see>.
    /// </summary>
    /// <remarks>
    /// <para>If <paramref name="channel" /> parameter is given, it gets all of <see cref="Webhook">the channel webhooks</see> instead.</para>
    /// </remarks>
    /// <param name="server">The identifier of <see cref="Server">the server</see> to get <see cref="Webhook">webhooks</see> from</param>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the channel</see> to get webhooks from</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>List of <see cref="Webhook">webhooks</see></returns>
    public abstract Task<IList<Webhook>> GetWebhooksAsync(HashId server, Guid? channel = null);

    /// <summary>
    /// Gets the <paramref name="webhook">specified webhook</paramref>.
    /// </summary>
    /// <param name="server">The identifier of <see cref="Server">the server</see> <see cref="Webhook">the webhook</see> is</param>
    /// <param name="webhook">The identifier of <see cref="Webhook">the webhook</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns><paramref name="webhook">Specified webhook</paramref></returns>
    public abstract Task<Webhook> GetWebhookAsync(HashId server, Guid webhook);

    /// <summary>
    /// Creates a <see cref="Webhook">new webhook</see> in the <paramref name="channel">specified channel</paramref>.
    /// </summary>
    /// <param name="server">The identifier of <see cref="Server">the server</see> where <see cref="Webhook">the webhook</see> will be created</param>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the channel</see> where <see cref="Webhook">the webhook</see> will be created</param>
    /// <param name="name">The name of <see cref="Webhook">the webhook</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <exception cref="ArgumentNullException">The specified <paramref name="name" /> is null, empty or whitespace</exception>
    /// <permission cref="GeneralPermissions.ManageWebhooks" />
    /// <returns>Created <see cref="Webhook">webhook</see></returns>
    public abstract Task<Webhook> CreateWebhookAsync(HashId server, Guid channel, string name);

    /// <summary>
    /// Edits the <paramref name="webhook">specified webhook</paramref>.
    /// </summary>
    /// <remarks>
    /// <para><see cref="Webhook" /> can moved between <see cref="ServerChannel">channels</see> using '<paramref name="newChannel" />' parameter.</para>
    /// </remarks>
    /// <param name="server">The identifier of <see cref="Server">the server</see> where <see cref="Webhook">the webhook</see> is</param>
    /// <param name="webhook">The identifier of <see cref="Webhook">the webhook</see> to update</param>
    /// <param name="name">The new name of <see cref="Webhook">the webhook</see></param>
    /// <param name="newChannel">The identifier of <see cref="ServerChannel">the channel</see> where <see cref="Webhook">the webhook</see> will be moved to</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="GeneralPermissions.ManageWebhooks" />
    /// <exception cref="ArgumentNullException">The specified <paramref name="name" /> is null, empty or whitespace</exception>
    /// <returns>Updated <see cref="Webhook">webhook</see></returns>
    public abstract Task<Webhook> UpdateWebhookAsync(HashId server, Guid webhook, string name, Guid? newChannel = null);

    /// <summary>
    /// Deletes the <paramref name="webhook">specified webhook</paramref>.
    /// </summary>
    /// <param name="server">The identifier of <see cref="Server">the server</see> where <see cref="Webhook">the webhook</see> is</param>
    /// <param name="webhook">The identifier of <see cref="Webhook">the webhook</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="GeneralPermissions.ManageWebhooks" />
    public abstract Task DeleteWebhookAsync(HashId server, Guid webhook);
    #endregion

    #region Channels
    /// <summary>
    /// Gets the <paramref name="channel">specified webhook</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the channel</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns><paramref name="channel">Specified channel</paramref></returns>
    public abstract Task<ServerChannel> GetChannelAsync(Guid channel);

    /// <summary>
    /// Creates a new channel in the <paramref name="server">specified server</paramref>.
    /// </summary>
    /// <param name="server">The identifier of <see cref="Server">the server</see> where <see cref="ServerChannel">the channel</see> will be created</param>
    /// <param name="name">The name of <see cref="ServerChannel">the channel</see> (max — <c>100</c>)</param>
    /// <param name="type">The type of the content that <see cref="ServerChannel">the channel</see> will hold</param>
    /// <param name="topic">The topic describing what <see cref="ServerChannel">the channel</see> is about (max — <c>512</c>)</param>
    /// <param name="group">The identifier of the group where <see cref="ServerChannel">the channel</see> will be created</param>
    /// <param name="category">The identifier of the category where <see cref="ServerChannel">the channel</see> will be created</param>
    /// <param name="isPublic">Whether the contents of the channel are publicly viewable</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <exception cref="ArgumentNullException">The specified <paramref name="name" /> is null, empty or whitespace</exception>
    /// <permission cref="GeneralPermissions.ManageChannels" />
    /// <returns>Created <see cref="ServerChannel">channel</see></returns>
    public abstract Task<ServerChannel> CreateChannelAsync(HashId server, string name, ChannelType type = ChannelType.Chat, string? topic = null, HashId? group = null, uint? category = null, bool? isPublic = null);

    /// <summary>
    /// Updates the <paramref name="channel">specified channel</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the channel</see> to update</param>
    /// <param name="name">A new name of <see cref="ServerChannel">the channel</see> (max — <c>100</c>)</param>
    /// <param name="topic">A new topic describing what <see cref="ServerChannel">the channel</see> is about (max — <c>512</c>)</param>
    /// <param name="isPublic">Whether the contents of the channel are publicly viewable</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="GeneralPermissions.ManageChannels" />
    /// <returns>Updated <paramref name="channel" /></returns>
    public abstract Task<ServerChannel> UpdateChannelAsync(Guid channel, string? name = null, string? topic = null, bool? isPublic = null);

    /// <summary>
    /// Deletes the <paramref name="channel">specified channel</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the channel to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="GeneralPermissions.ManageChannels" />
    public abstract Task DeleteChannelAsync(Guid channel);
    #endregion

    #endregion
}