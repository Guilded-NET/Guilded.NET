using System.Threading.Tasks;

namespace Guilded.NET.Base
{
    using Permissions;
    public abstract partial class BaseGuildedClient
    {
        #region Groups
        /// <summary>
        /// Adds a member to the group.
        /// </summary>
        /// <remarks>
        /// <para>Adds a member of <paramref name="memberId"/> to the group <paramref name="groupId"/>.</para>
        /// <para>This allows member of <paramref name="memberId"/> to see and interact with the group <paramref name="groupId"/>.</para>
        /// </remarks>
        /// <param name="groupId">The identifier of the parent group</param>
        /// <param name="memberId">The identifier of the member to add</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="GeneralPermissions.ManageGroups">Required for managing group's memberships</permission>
        public abstract Task AddMembershipAsync(GId groupId, GId memberId);
        /// <summary>
        /// Removes a member from the group.
        /// </summary>
        /// <remarks>
        /// <para>Removes a member of <paramref name="memberId"/> from the group <paramref name="groupId"/>.</para>
        /// <para>This disallows member of <paramref name="memberId"/> to interact or see the group <paramref name="groupId"/></para>
        /// </remarks>
        /// <param name="groupId">The identifier of the parent group</param>
        /// <param name="memberId">The identifier of the member to remove</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="GeneralPermissions.ManageGroups">Required for managing group's memberships</permission>
        public abstract Task RemoveMembershipAsync(GId groupId, GId memberId);
        #endregion

        #region Roles
        /// <summary>
        /// Attaches amount of XP required to a role.
        /// </summary>
        /// <remarks>
        /// <para>Sets how much <paramref name="amount"/> of XP is necessary for role of
        /// <paramref name="roleId"/> to be given.</para>
        /// </remarks>
        /// <param name="roleId">The identifier of the editing role</param>
        /// <param name="amount">The amount XP needed</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="GeneralPermissions.ManageRoles">Required for managing roles</permission>
        public abstract Task AttachRoleLevelAsync(uint roleId, long amount);
        #endregion

        #region Members
        /// <summary>
        /// Updates member's nickname.
        /// </summary>
        /// <remarks>
        /// <para>Changes given member's nickname to set <paramref name="nickname"/> parameter.</para>
        /// </remarks>
        /// <param name="memberId">The identifier of the member to update</param>
        /// <param name="nickname">The new nickname of the member</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedRequestException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="CustomPermissions.ManageNicknames">Required for managing nicknames of members</permission>
        /// <permission cref="CustomPermissions.ChangeNickname">Required for changing your own nickname</permission>
        /// <returns>Nickname</returns>
        public abstract Task<string> UpdateNicknameAsync(GId memberId, string nickname);
        /// <summary>
        /// Deletes member's nickname.
        /// </summary>
        /// <remarks>
        /// <para>Removes given member's nickname.</para>
        /// </remarks>
        /// <param name="memberId">The identifier of the member to update</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedRequestException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="CustomPermissions.ManageNicknames">Required for managing nicknames of members</permission>
        /// <permission cref="CustomPermissions.ChangeNickname">Required for changing your own nickname</permission>
        /// <returns>Nickname</returns>
        public abstract Task DeleteNicknameAsync(GId memberId);
        /// <summary>
        /// Adds a role to the given user.
        /// </summary>
        /// <remarks>
        /// <para>Gives a member of <paramref name="memberId"/> the role of <paramref name="roleId"/> if permissions are met.</para>
        /// <para>If they hold the role of <paramref name="roleId"/>, then nothing happens.</para>
        /// </remarks>
        /// <param name="memberId">The identifier of the receiving user</param>
        /// <param name="roleId">The identifier of the role to add</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="GeneralPermissions.ManageRoles">Required for managing member's roles</permission>
        public abstract Task AddRoleAsync(GId memberId, uint roleId);
        /// <summary>
        /// Removes a role from the given user.
        /// </summary>
        /// <remarks>
        /// <para>Removes a role of <paramref name="roleId"/> from the member of <paramref name="memberId"/> if permissions are met.</para>
        /// <para>If they don't hold a role of <paramref name="roleId"/>, then nothing happens.</para>
        /// </remarks>
        /// <param name="memberId">The identifier of the losing user</param>
        /// <param name="roleId">The identifier of the role to remove</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="GeneralPermissions.ManageRoles">Required for managing member's roles</permission>
        public abstract Task RemoveRoleAsync(GId memberId, uint roleId);
        /// <summary>
        /// Adds XP to the given user.
        /// </summary>
        /// <remarks>
        /// <para>Gives <paramref name="amount"/> of XP to member of <paramref name="memberId"/>.</para>
        /// <para>The minimum XP amount is <c>-1000</c> and maximum is <c>1000</c>.</para>
        /// </remarks>
        /// <param name="memberId">The identifier of the receiving member</param>
        /// <param name="amount">The amount of XP received from -1000 to 1000</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <exception cref="System.ArgumentOutOfRangeException">When the amount of XP given exceeds the limit</exception>
        /// <permission cref="XPPermissions.ManageServerXP">Required for managing member's XP</permission>
        /// <returns>Total XP</returns>
        public abstract Task<long> AddXpAsync(GId memberId, short amount);
        #endregion

        /*/// <summary>
        /// Gets metadata of given route.
        /// </summary>
        /// <param name="route">Route/path to certain thing</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Metadata</returns>
        Task<Metadata> GetMetadataAsync(string route);
        

        #region Overview
        /// <summary>
        /// Gets an overview page of a team.
        /// </summary>
        /// <param name="teamId">Team to get overview of</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Team overview page</returns>
        Task<TeamOverview> GetOverviewAsync(GId teamId);
        #endregion


        #region Team
        /// <summary>
        /// Joins a specific team.
        /// </summary>
        /// <param name="teamId">Team to join</param>
        /// <exception cref="GuildedException"/>
        Task JoinTeamAsync(GId teamId);
        /// <summary>
        /// Leaves a specific team.
        /// </summary>
        /// <param name="teamId">ID of the team to leave</param>
        /// <exception cref="GuildedException"/>
        Task LeaveTeamAsync(GId teamId);
        /// <summary>
        /// Gets team with given ID.
        /// </summary>
        /// <param name="id">ID of the team to get</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Team</returns>
        Task<Team> GetTeamAsync(GId id);
        /// <summary>
        /// Gets team info without member and webhook list. Use <see cref="GetMembersAsync"/> to get member list instead.
        /// </summary>
        /// <param name="id">ID of the team to get information of</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Team information</returns>
        Task<Team> GetTeamInfoAsync(GId id);
        #endregion

        
        #region Members
        /// <summary>
        /// Gets a list of all members, webhooks and bots in a team.
        /// </summary>
        /// <param name="teamId">ID of the team to get member list in</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Member, webhook and bot list</returns>
        Task<MemberList> GetMembersAsync(GId teamId);
        /// <summary>
        /// Gets details about given members.
        /// </summary>
        /// <param name="teamId">ID of the team to get member details from</param>
        /// <param name="userIds">ID of the users to get details of</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Dictionary of user ID, Details</returns>
        Task<IDictionary<GId, MemberDetails>> GetMemberDetailsAsync(GId teamId, params GId[] userIds);
        /// <summary>
        /// Gets details about given webhooks.
        /// </summary>
        /// <param name="teamId">ID of the team where webhooks are</param>
        /// <param name="webhookIds">ID of the webhooks to get details of</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Dictionary of webhook ID, Details</returns>
        Task<IDictionary<Guid, WebhookDetails>> GetWebhookDetailsAsync(GId teamId, params Guid[] webhookIds);
        /// <summary>
        /// Sets a new nickname for a member.
        /// </summary>
        /// <param name="teamId">ID of the team to change nickname in</param>
        /// <param name="memberId">ID of the member to change nickname of</param>
        /// <param name="nickname">A new nickname to set</param>
        /// <exception cref="GuildedException"/>
        Task SetNicknameAsync(GId teamId, GId memberId, string nickname);
        /// <summary>
        /// Sets new XP count of a specific user.
        /// </summary>
        /// <param name="teamId">ID of the team member is in</param>
        /// <param name="memberId">ID of the member to set XP of</param>
        /// <param name="amount">Amount of XP to set</param>
        /// <exception cref="GuildedException"/>
        Task SetXpAsync(GId teamId, GId memberId, long amount);
        /// <summary>
        /// Kicks a member from a server.
        /// </summary>
        /// <param name="teamId">ID of the team to kick from</param>
        /// <param name="memberId">ID of the member to kick</param>
        /// <exception cref="GuildedException"/>
        Task KickMemberAsync(GId teamId, GId memberId);
        /// <summary>
        /// Bans a member from a team.
        /// </summary>
        /// <param name="teamId">ID of the team to ban from</param>
        /// <param name="memberId">ID of the member to ban</param>
        /// <param name="reason">Reason for banning this user</param>
        /// <param name="deleteHistoryOption">Either 7(for 1 week) or 24(for 1 day)</param>
        /// <exception cref="GuildedException"/>
        Task BanMemberAsync(GId teamId, GId memberId, string reason, uint deleteHistoryOption);
        /// <summary>
        /// Unbans a member in a team.
        /// </summary>
        /// <param name="teamId">ID of the team to unban in</param>
        /// <param name="memberId">ID of the member to unban</param>
        /// <exception cref="GuildedException"/>
        Task UnbanMemberAsync(GId teamId, GId memberId);
        #endregion

        
        #region Groups
        /// <summary>
        /// Gets a group by ID.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <param name="groupId">ID of the groupp</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Group</returns>
        Task<Group> GetGroupAsync(GId teamId, GId groupId);
        /// <summary>
        /// List of groups in given team.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <exception cref="GuildedException"/>
        /// <returns>List of groups</returns>
        Task<IList<Group>> GetGroupsAsync(GId teamId);
        #endregion

        
        #region Channels
        /// <summary>
        /// Gets a channel by ID.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <param name="channelId">ID of the channel</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Channel</returns>
        Task<Channel> GetChannelAsync(GId teamId, Guid channelId);
        /// <summary>
        /// Clears all notifications in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel to clear notifications in</param>
        /// <exception cref="GuildedException"/>
        Task ClearNotificationsAsync(Guid channelId);
        /// <summary>
        /// Creates a new channel in a specific team and group.
        /// </summary>
        /// <param name="teamId">Team to create channel in</param>
        /// <param name="groupId">Group to create channel in</param>
        /// <param name="type">Channel type</param>
        /// <param name="public">If channel should be public</param>
        /// <param name="name">Name of the channel</param>
        /// <exception cref="GuildedException"/>
        Task CreateChannelAsync(GId teamId, GId groupId, ChannelType type, bool @public, string name);
        /// <summary>
        /// Deletes a channel in a specific team and group.
        /// </summary>
        /// <param name="teamId">Team to delete channel in</param>
        /// <param name="groupId">Group to delete channel in</param>
        /// <param name="channelId">Channel to be deleted</param>
        /// <exception cref="GuildedException"/>
        Task DeleteChannelAsync(GId teamId, GId groupId, Guid channelId);
        /// <summary>
        /// List of channels and categories in given team.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Channel list</returns>
        Task<ChannelList> GetChannelsAsync(GId teamId);
        #endregion

        
        #region Ordering
        /// <summary>
        /// Reorders channels by given channel ID array.
        /// </summary>
        /// <param name="teamId">ID of the team channels are in</param>
        /// <param name="categoryId">ID of the category channels are in</param>
        /// <param name="channelList">Channels to reorder</param>
        /// <exception cref="GuildedException"/>
        Task ReorderChannelsAsync(GId teamId, uint? categoryId, params Guid[] channelList);
        /// <summary>
        /// Reorders categories by given category ID array.
        /// </summary>
        /// <param name="teamId">ID of the team categirues are in</param>
        /// <param name="categoryList">Categories to reorder</param>
        /// <exception cref="GuildedException"/>
        Task ReorderCategoriesAsync(GId teamId, params uint[] categoryList);
        /// <summary>
        /// Assigns a channel to a specific category.
        /// </summary>
        /// <param name="teamId">ID of the team where channel is in</param>
        /// <param name="categoryId">Category where channel should be in</param>
        /// <param name="channelId">ID of the channel to move</param>
        /// <param name="shouldRoleSync">If role permissions should be synced with category</param>
        /// <exception cref="GuildedException"/>
        Task AssignToCategoryAsync(GId teamId, uint categoryId, Guid channelId, bool shouldRoleSync);
        /// <summary>
        /// Unassigns a channel from a category.
        /// </summary>
        /// <param name="teamId">ID of the team where channel is in</param>
        /// <param name="channelId">ID of the channel to remove from category</param>
        /// <exception cref="GuildedException"/>
        Task UnassignFromCategoryAsync(GId teamId, Guid channelId);
        #endregion

        
        #region Permissions
        /// <summary>
        /// Adds a role to a channel.
        /// </summary>
        /// <param name="teamId">ID of the team where channel is in</param>
        /// <param name="channelId">ID of the channel to add role in</param>
        /// <param name="roleId">ID of the role to add</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Updated channel</returns>
        Task<Channel> AddChannelRoleAsync(GId teamId, Guid channelId, uint roleId);
        /// <summary>
        /// Removes a role from a channel.
        /// </summary>
        /// <param name="teamId">ID of the team where channel is in</param>
        /// <param name="channelId">ID of the channel to remove role in</param>
        /// <param name="roleId">ID of the role to remove</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Updated channel</returns>
        Task<Channel> RemoveChannelRoleAsync(GId teamId, Guid channelId, uint roleId);
        /// <summary>
        /// Adds a role to a category.
        /// </summary>
        /// <param name="teamId">ID of the team where category is in</param>
        /// <param name="categoryId">ID of the category to add role in</param>
        /// <param name="roleId">ID of the role to add</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Updated category</returns>
        Task<Category> AddCategoryRoleAsync(GId teamId, uint categoryId, uint roleId);
        /// <summary>
        /// Removes a role from a category.
        /// </summary>
        /// <param name="teamId">ID of the team where category is in</param>
        /// <param name="categoryId">ID of the category to remove role in</param>
        /// <param name="roleId">ID of the role to remove</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Updated category</returns>
        Task<Category> RemoveCategoryRoleAsync(GId teamId, uint categoryId, uint roleId);
        /// <summary>
        /// Adds a user permission to a channel.
        /// </summary>
        /// <param name="teamId">ID of the team where channel is in</param>
        /// <param name="channelId">ID of the channel to add user in</param>
        /// <param name="userId">ID of the user to add</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Updated channel</returns>
        Task<Channel> AddChannelUserAsync(GId teamId, Guid channelId, GId userId);
        /// <summary>
        /// Removes a user permission from a channel.
        /// </summary>
        /// <param name="teamId">ID of the team where channel is in</param>
        /// <param name="channelId">ID of the channel to remove user permission in</param>
        /// <param name="userId">ID of the user to remove permissions of</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Updated channel</returns>
        Task<Channel> RemoveChannelUserAsync(GId teamId, Guid channelId, GId userId);
        /// <summary>
        /// Adds a user permission to a category.
        /// </summary>
        /// <param name="teamId">ID of the team where category is in</param>
        /// <param name="categoryId">ID of the category to add user in</param>
        /// <param name="userId">ID of the user to add</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Updated category</returns>
        Task<Category> AddCategoryUserAsync(GId teamId, uint categoryId, GId userId);
        /// <summary>
        /// Removes a user permission from a category.
        /// </summary>
        /// <param name="teamId">ID of the team where category is in</param>
        /// <param name="categoryId">ID of the category to remove user permission in</param>
        /// <param name="userId">ID of the user to remove permissions of</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Updated category</returns>
        Task<Category> RemoveCategoryUserAsync(GId teamId, uint categoryId, GId userId);
        #endregion

        
        #region Forms & Polls
        /// <summary>
        /// Creates a form for form node.
        /// </summary>
        /// <param name="form">Form to create</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Form ID</returns>
        Task<uint> CreateFormAsync(BasicGuildedForm form);
        /// <summary>
        /// Gets a form or a poll by form ID.
        /// </summary>
        /// <param name="formId">ID of the form to get</param>
        /// <exception cref="GuildedException"/>
        /// <returns>A form and a form response</returns>
        Task<FormData> GetFormAsync(uint formId);
        /// <summary>
        /// Submits a form response.
        /// </summary>
        /// <param name="formId">Form ID it is responding to</param>
        /// <param name="response">Response to submit</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Response ID</returns>
        Task<uint> PostFormResponseAsync(uint formId, BasicFormResponse response);
        #endregion*/
    }
}