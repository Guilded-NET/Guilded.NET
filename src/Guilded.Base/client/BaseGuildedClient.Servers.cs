using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Guilded.Base.Permissions;
using Guilded.Base.Servers;
using Guilded.Base.Users;

namespace Guilded.Base;

public abstract partial class BaseGuildedClient
{
    #region Groups
    /// <summary>
    /// Adds the <paramref name="user" /> to the <paramref name="group" />.
    /// </summary>
    /// <remarks>
    /// <para>This allows the member to interact or see the specified group.</para>
    /// </remarks>
    /// <param name="group">The identifier of the parent group</param>
    /// <param name="user">The identifier of the member to add</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="GeneralPermissions.ManageGroups">Required for managing group's memberships</permission>
    public abstract Task AddMembershipAsync(HashId group, HashId user);
    /// <summary>
    /// Removes the <paramref name="user" /> from the <paramref name="group" />.
    /// </summary>
    /// <remarks>
    /// <para>This disallows the member to interact or see the specified group.</para>
    /// </remarks>
    /// <param name="group">The identifier of the parent group</param>
    /// <param name="user">The identifier of the member to remove</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="GeneralPermissions.ManageGroups">Required for managing group's memberships</permission>
    public abstract Task RemoveMembershipAsync(HashId group, HashId user);
    #endregion

    #region Members
    /// <summary>
    /// Gets the list of all <paramref name="server" /> members.
    /// </summary>
    /// <param name="server">The server to get member list of</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <returns>List of members</returns>
    public abstract Task<IList<MemberSummary<UserSummary>>> GetMembersAsync(HashId server);
    /// <summary>
    /// Gets full information about the specified <paramref name="member" /> in the <paramref name="server" />.
    /// </summary>
    /// <param name="server">The server where the member is</param>
    /// <param name="member">The identifier of the member to get</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <returns>Member</returns>
    public abstract Task<Member> GetMemberAsync(HashId server, HashId member);
    /// <summary>
    /// Gets the list of roles the <paramref name="member" /> holds in the <paramref name="server" />.
    /// </summary>
    /// <remarks>
    /// <para>No permissions are required.</para>
    /// </remarks>
    /// <param name="server">The server where to fetch user's information</param>
    /// <param name="member">The identifier of the role holder</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <returns>List of role IDs</returns>
    public abstract Task<IList<uint>> GetMemberRolesAsync(HashId server, HashId member);
    /// <summary>
    /// Changes the <paramref name="nickname" /> of the specified <paramref name="member" /> in the <paramref name="server" />.
    /// </summary>
    /// <param name="server">The server to modify member in</param>
    /// <param name="member">The identifier of the member to update</param>
    /// <param name="nickname">The new nickname of the member</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedRequestException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="CustomPermissions.ManageNicknames">Required for managing nicknames of members</permission>
    /// <permission cref="CustomPermissions.ChangeNickname">Required for changing your own nickname</permission>
    /// <returns>Nickname</returns>
    public abstract Task<string> UpdateNicknameAsync(HashId server, HashId member, string nickname);
    /// <summary>
    /// Removes the nickname of the specified <paramref name="member" /> in the <paramref name="server" />.
    /// </summary>
    /// <param name="server">The server to modify member in</param>
    /// <param name="member">The identifier of the member to update</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedRequestException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="CustomPermissions.ManageNicknames">Required for managing nicknames of members</permission>
    /// <permission cref="CustomPermissions.ChangeNickname">Required for changing your own nickname</permission>
    /// <returns>Nickname</returns>
    public abstract Task DeleteNicknameAsync(HashId server, HashId member);
    /// <summary>
    /// Adds a <paramref name="role" /> to the user.
    /// </summary>
    /// <remarks>
    /// <para>If they hold the specified <paramref name="role" />, then nothing happens.</para>
    /// </remarks>
    /// <param name="server">The server to modify member in</param>
    /// <param name="member">The identifier of the receiving user</param>
    /// <param name="role">The identifier of the role to add</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="GeneralPermissions.ManageRoles">Required for managing member's roles</permission>
    public abstract Task AddRoleAsync(HashId server, HashId member, uint role);
    /// <summary>
    /// Removes a <paramref name="role" /> from the user.
    /// </summary>
    /// <remarks>
    /// <para>If they don't hold the specified <paramref name="role" />, then nothing happens.</para>
    /// </remarks>
    /// <param name="server">The server to modify member in</param>
    /// <param name="member">The identifier of the losing user</param>
    /// <param name="role">The identifier of the role to remove</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="GeneralPermissions.ManageRoles">Required for managing member's roles</permission>
    public abstract Task RemoveRoleAsync(HashId server, HashId member, uint role);
    /// <summary>
    /// Gives <paramref name="amount">XP</paramref> to the specified <paramref name="member" />.
    /// </summary>
    /// <param name="server">The server to modify member in</param>
    /// <param name="member">The identifier of the receiving member</param>
    /// <param name="amount">The amount of XP received (values — <c>[-1000, 1000]</c>)</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <exception cref="ArgumentOutOfRangeException">When the amount of XP given exceeds the limit</exception>
    /// <permission cref="XpPermissions.ManageServerXp">Required for managing member's XP</permission>
    /// <returns>Total XP</returns>
    public abstract Task<long> AddXpAsync(HashId server, HashId member, long amount);
    /// <summary>
    /// Gives <paramref name="amount">XP</paramref> to the specified <paramref name="role">role's</paramref> members.
    /// </summary>
    /// <param name="server">The server where the role is</param>
    /// <param name="role">The identifier of the receiving role</param>
    /// <param name="amount">The amount of XP received (values — <c>[-1000, 1000]</c>)</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="XpPermissions.ManageServerXp">Required for managing member's XP</permission>
    public abstract Task AddXpAsync(HashId server, uint role, long amount);
    #endregion

    #region Server-wide Moderation
    /// <summary>
    /// Removes the specified <paramref name="member" /> from the <paramref name="server" />.
    /// </summary>
    /// <param name="server">The server to kick member from</param>
    /// <param name="member">The identifier of the member to kick</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedRequestException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="GeneralPermissions.KickBanMembers">Required for kicking the member</permission>
    public abstract Task KickMemberAsync(HashId server, HashId member);
    /// <summary>
    /// Gets the list of <paramref name="server">server's</paramref> bans.
    /// </summary>
    /// <param name="server">The server to get bans of</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedRequestException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="GeneralPermissions.KickBanMembers">Required for getting all the bans of the server</permission>
    /// <returns>List of member ban information</returns>
    public abstract Task<IList<MemberBan>> GetBansAsync(HashId server);
    /// <summary>
    /// Gets the information about the ban of the <paramref name="member" />.
    /// </summary>
    /// <param name="server">The server where the user has been banned</param>
    /// <param name="member">The identifier of the member to get ban information of</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedRequestException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="GeneralPermissions.KickBanMembers">Required for getting the specified ban</permission>
    /// <returns>Member ban information</returns>
    public abstract Task<MemberBan> GetBanAsync(HashId server, HashId member);
    /// <summary>
    /// Bans the specified <paramref name="member" />.
    /// </summary>
    /// <remarks>
    /// <para>Disallows them from joining again, until they receive an unban with <see cref="UnbanMemberAsync" /> method.</para>
    /// </remarks>
    /// <param name="server">The server to ban member from</param>
    /// <param name="member">The identifier of the member to ban</param>
    /// <param name="reason">The reason for a ban</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedRequestException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="GeneralPermissions.KickBanMembers">Required for banning the member</permission>
    /// <returns>Member ban information</returns>
    public abstract Task<MemberBan> BanMemberAsync(HashId server, HashId member, string? reason = null);
    /// <summary>
    /// Unbans the specified <paramref name="member" />.
    /// </summary>
    /// <remarks>
    /// <para>Allows them to join the server again.</para>
    /// </remarks>
    /// <param name="server">The server to unban member in</param>
    /// <param name="member">The identifier of the member to unban</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedRequestException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="GeneralPermissions.KickBanMembers">Required for removing the ban from the member</permission>
    public abstract Task UnbanMemberAsync(HashId server, HashId member);
    #endregion

    #region Webhooks
    /// <summary>
    /// Gets a list of webhooks.
    /// </summary>
    /// <remarks>
    /// <para>Gets a list of all webhooks in the specified server. If <paramref name="channelId"/> parameter is given, it gets all of the channel webhooks instead.</para>
    /// </remarks>
    /// <param name="serverId">The identifier of the server to get webhooks from</param>
    /// <param name="channelId">The identifier of the channel to get webhooks from</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedRequestException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <returns>List of webhooks</returns>
    public abstract Task<IList<Webhook>> GetWebhooksAsync(HashId serverId, Guid? channelId = null);
    /// <summary>
    /// Gets the specified <paramref name="webhook" />.
    /// </summary>
    /// <param name="server">The identifier of the server where the webhook is</param>
    /// <param name="webhook">The identifier of the webhook to get</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedRequestException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <returns>Specified webhook</returns>
    public abstract Task<Webhook> GetWebhookAsync(HashId server, Guid webhook);
    /// <summary>
    /// Creates a new webhook in the specified <paramref name="channel" />.
    /// </summary>
    /// <param name="server">The identifier of the server where the webhook will be created</param>
    /// <param name="channel">The identifier of the channel where the webhook will be created</param>
    /// <param name="name">The name of the webhook</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedRequestException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <exception cref="ArgumentNullException">The specified <paramref name="name" /> is null, empty or whitespace</exception>
    /// <permission cref="GeneralPermissions.ManageWebhooks">Required for creating webhooks</permission>
    /// <returns>Created webhook</returns>
    public abstract Task<Webhook> CreateWebhookAsync(HashId server, Guid channel, string name);
    /// <summary>
    /// Updates the specified <paramref name="webhook" /> in the specified <paramref name="server" />.
    /// </summary>
    /// <remarks>
    /// <para>Webhooks can moved between channels using '<paramref name="newChannel" />' parameter.</para>
    /// </remarks>
    /// <param name="server">The identifier of the server where the webhook is</param>
    /// <param name="webhook">The identifier of the webhook to update</param>
    /// <param name="name">The new name of the webhook</param>
    /// <param name="newChannel">The identifier of the channel where the webhook will be moved to</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedRequestException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="GeneralPermissions.ManageWebhooks">Required for updating webhooks</permission>
    /// <exception cref="ArgumentNullException">The specified <paramref name="name" /> is null, empty or whitespace</exception>
    /// <returns>Updated webhook</returns>
    public abstract Task<Webhook> UpdateWebhookAsync(HashId server, Guid webhook, string name, Guid? newChannel = null);
    /// <summary>
    /// Deletes the specified <paramref name="webhook" />.
    /// </summary>
    /// <param name="server">The identifier of the server where the webhook is</param>
    /// <param name="webhook">The identifier of the webhook to delete</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedRequestException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="GeneralPermissions.ManageWebhooks">Required for deleting webhooks</permission>
    public abstract Task DeleteWebhookAsync(HashId server, Guid webhook);
    #endregion
}