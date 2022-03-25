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
    /// Adds a member to the group.
    /// </summary>
    /// <remarks>
    /// <para>Adds the specified member to the group.</para>
    /// <para>This allows the member to see and interact with the specified group.</para>
    /// </remarks>
    /// <param name="groupId">The identifier of the parent group</param>
    /// <param name="userId">The identifier of the member to add</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="GeneralPermissions.ManageGroups">Required for managing group's memberships</permission>
    public abstract Task AddMembershipAsync(HashId groupId, HashId userId);
    /// <summary>
    /// Removes a member from the group.
    /// </summary>
    /// <remarks>
    /// <para>Removes the specified member from the group.</para>
    /// <para>This disallows the member to interact or see the specified group.</para>
    /// </remarks>
    /// <param name="groupId">The identifier of the parent group</param>
    /// <param name="userId">The identifier of the member to remove</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="GeneralPermissions.ManageGroups">Required for managing group's memberships</permission>
    public abstract Task RemoveMembershipAsync(HashId groupId, HashId userId);
    #endregion

    #region Members
    /// <summary>
    /// Gets server's members.
    /// </summary>
    /// <remarks>
    /// <para>Gets the list of all server members.</para>
    /// </remarks>
    /// <param name="serverId">The server to get member list of</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <returns>List of members</returns>
    public abstract Task<IList<MemberSummary<UserSummary>>> GetMembersAsync(HashId serverId);
    /// <summary>
    /// Gets the specified member.
    /// </summary>
    /// <remarks>
    /// <para>Gets full information about the specified member.</para>
    /// </remarks>
    /// <param name="serverId">The server where the member is</param>
    /// <param name="userId">The identifier of the member to get</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <returns>Member</returns>
    public abstract Task<Member> GetMemberAsync(HashId serverId, HashId userId);
    /// <summary>
    /// Gets the member's roles.
    /// </summary>
    /// <remarks>
    /// <para>Gets the specified member's role ID list. No permissions are required.</para>
    /// </remarks>
    /// <param name="serverId">The server where to fetch user's information</param>
    /// <param name="userId">The identifier of the role holder</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <returns>List of role IDs</returns>
    public abstract Task<IList<uint>> GetMemberRolesAsync(HashId serverId, HashId userId);
    /// <summary>
    /// Updates the member's nickname.
    /// </summary>
    /// <remarks>
    /// <para>Changes the specified member's nickname. New nickname will be set as <paramref name="nickname"/> parameter.</para>
    /// </remarks>
    /// <param name="serverId">The server to modify member in</param>
    /// <param name="userId">The identifier of the member to update</param>
    /// <param name="nickname">The new nickname of the member</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedRequestException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="CustomPermissions.ManageNicknames">Required for managing nicknames of members</permission>
    /// <permission cref="CustomPermissions.ChangeNickname">Required for changing your own nickname</permission>
    /// <returns>Nickname</returns>
    public abstract Task<string> UpdateNicknameAsync(HashId serverId, HashId userId, string nickname);
    /// <summary>
    /// Deletes member's nickname.
    /// </summary>
    /// <remarks>
    /// <para>Removes the specified member's nickname.</para>
    /// </remarks>
    /// <param name="serverId">The server to modify member in</param>
    /// <param name="userId">The identifier of the member to update</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedRequestException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="CustomPermissions.ManageNicknames">Required for managing nicknames of members</permission>
    /// <permission cref="CustomPermissions.ChangeNickname">Required for changing your own nickname</permission>
    /// <returns>Nickname</returns>
    public abstract Task DeleteNicknameAsync(HashId serverId, HashId userId);
    /// <summary>
    /// Adds a role to the user.
    /// </summary>
    /// <remarks>
    /// <para>Gives the specified role to the member.</para>
    /// <para>If they hold the specified role, then nothing happens.</para>
    /// </remarks>
    /// <param name="serverId">The server to modify member in</param>
    /// <param name="userId">The identifier of the receiving user</param>
    /// <param name="roleId">The identifier of the role to add</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="GeneralPermissions.ManageRoles">Required for managing member's roles</permission>
    public abstract Task AddRoleAsync(HashId serverId, HashId userId, uint roleId);
    /// <summary>
    /// Removes a role from the user.
    /// </summary>
    /// <remarks>
    /// <para>Removes the specified role from the given member.</para>
    /// <para>If they don't hold the specified role, then nothing happens.</para>
    /// </remarks>
    /// <param name="serverId">The server to modify member in</param>
    /// <param name="userId">The identifier of the losing user</param>
    /// <param name="roleId">The identifier of the role to remove</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="GeneralPermissions.ManageRoles">Required for managing member's roles</permission>
    public abstract Task RemoveRoleAsync(HashId serverId, HashId userId, uint roleId);
    /// <summary>
    /// Adds XP to the user.
    /// </summary>
    /// <remarks>
    /// <para>Gives the specified <paramref name="amount"/> of XP to the member.</para>
    /// </remarks>
    /// <param name="serverId">The server to modify member in</param>
    /// <param name="userId">The identifier of the receiving member</param>
    /// <param name="amount">The amount of XP received (values — <c>[-1000, 1000]</c>)</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <exception cref="System.ArgumentOutOfRangeException">When the amount of XP given exceeds the limit</exception>
    /// <permission cref="XpPermissions.ManageServerXp">Required for managing member's XP</permission>
    /// <returns>Total XP</returns>
    public abstract Task<long> AddXpAsync(HashId serverId, HashId userId, long amount);
    /// <summary>
    /// Adds XP to the role.
    /// </summary>
    /// <remarks>
    /// <para>Gives the specified <paramref name="amount"/> of XP to the role's members.</para>
    /// </remarks>
    /// <param name="serverId">The server where the role is</param>
    /// <param name="roleId">The identifier of the receiving role</param>
    /// <param name="amount">The amount of XP received (values — <c>[-1000, 1000]</c>)</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="XpPermissions.ManageServerXp">Required for managing member's XP</permission>
    public abstract Task AddXpAsync(HashId serverId, uint roleId, long amount);
    #endregion

    #region Server-wide Moderation
    /// <summary>
    /// Kicks the specified member.
    /// </summary>
    /// <remarks>
    /// <para>Removes the member from the server.</para>
    /// </remarks>
    /// <param name="serverId">The server to kick member from</param>
    /// <param name="userId">The identifier of the member to kick</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedRequestException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="GeneralPermissions.KickBanMembers">Required for kicking the member</permission>
    public abstract Task KickMemberAsync(HashId serverId, HashId userId);
    /// <summary>
    /// Gets server's bans.
    /// </summary>
    /// <remarks>
    /// <para>Gets the list of server's bans.</para>
    /// </remarks>
    /// <param name="serverId">The server to get bans of</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedRequestException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="GeneralPermissions.KickBanMembers">Required for getting all the bans of the server</permission>
    /// <returns>List of member ban information</returns>
    public abstract Task<IList<MemberBan>> GetBansAsync(HashId serverId);
    /// <summary>
    /// Gets the ban of the member.
    /// </summary>
    /// <remarks>
    /// <para>Gets the information about the member's ban.</para>
    /// </remarks>
    /// <param name="serverId">The server where the user has been banned</param>
    /// <param name="userId">The identifier of the member to get ban information of</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedRequestException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="GeneralPermissions.KickBanMembers">Required for getting the specified ban</permission>
    /// <returns>Member ban information</returns>
    public abstract Task<MemberBan> GetBanAsync(HashId serverId, HashId userId);
    /// <summary>
    /// Bans the specified member.
    /// </summary>
    /// <remarks>
    /// <para>Removes the member from the server and disallows them from joining again, until they receive an unban with <see cref="UnbanMemberAsync" /> method.</para>
    /// </remarks>
    /// <param name="serverId">The server to ban member from</param>
    /// <param name="userId">The identifier of the member to ban</param>
    /// <param name="reason">The reason for a ban</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedRequestException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="GeneralPermissions.KickBanMembers">Required for banning the member</permission>
    /// <returns>Member ban information</returns>
    public abstract Task<MemberBan> BanMemberAsync(HashId serverId, HashId userId, string? reason = null);
    /// <summary>
    /// Unbans the specified member.
    /// </summary>
    /// <remarks>
    /// <para>Removes the ban from the specified member. Allows them to join the server again.</para>
    /// </remarks>
    /// <param name="serverId">The server to unban member in</param>
    /// <param name="userId">The identifier of the member to unban</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedRequestException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="GeneralPermissions.KickBanMembers">Required for removing the ban from the member</permission>
    public abstract Task UnbanMemberAsync(HashId serverId, HashId userId);
    #endregion
}