using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Guilded.NET.Objects {
    using Forms;
    using Teams;
    public partial interface IGuildedClient {
        //=======================//
        //   Overview
        //=======================//

        /// <summary>
        /// Gets an overview page of a team.
        /// </summary>
        /// <param name="teamId">Team to get overview of</param>
        /// <returns>Team overview page</returns>
        Task<TeamOverview> GetOverviewAsync(GId teamId);
        /// <summary>
        /// Gets an overview page of a team.
        /// </summary>
        /// <param name="teamId">Team to get overview of</param>
        /// <returns>Team overview page</returns>
        TeamOverview GetOverview(GId teamId);

        //=======================//
        //   Teams
        //=======================//

        /// <summary>
        /// Joins a specific team.
        /// </summary>
        /// <param name="team">Team to join</param>
        /// <returns>Async task</returns>
        Task JoinTeamAsync(GId team);
        /// <summary>
        /// Joins a specific team.
        /// </summary>
        /// <param name="team">Team to join</param>
        void JoinTeam(GId team);
        /// <summary>
        /// Leaves a specific team.
        /// </summary>
        /// <param name="team">Team to leave</param>
        /// <returns>Async task</returns>
        Task LeaveTeamAsync(GId team);
        /// <summary>
        /// Leaves a specific team.
        /// </summary>
        /// <param name="team">Team to leave</param>
        void LeaveTeam(GId team);
        /// <summary>
        /// Gets team with given ID.
        /// </summary>
        /// <param name="id">Team ID</param>
        /// <returns>Team</returns>
        Task<Team> GetTeamAsync(GId id);
        /// <summary>
        /// Gets team with given ID.
        /// </summary>
        /// <param name="id">Team ID</param>
        /// <returns>Team</returns>
        Team GetTeam(GId id);

        //=======================//
        //   Members
        //=======================//

        /// <summary>
        /// Sets a new nickname for a member.
        /// </summary>
        /// <param name="teamId">ID of the team to change nickname in</param>
        /// <param name="memberId">ID of the member to change nickname of</param>
        /// <param name="nickname">A new nickname to set</param>
        Task SetNicknameAsync(GId teamId, GId memberId, string nickname);
        /// <summary>
        /// Sets a new nickname for a member.
        /// </summary>
        /// <param name="teamId">ID of the team to change nickname in</param>
        /// <param name="memberId">ID of the member to change nickname of</param>
        /// <param name="nickname">A new nickname to set</param>
        void SetNickname(GId teamId, GId memberId, string nickname);
        /// <summary>
        /// Kicks a member from a server.
        /// </summary>
        /// <param name="teamId">ID of the team to kick from</param>
        /// <param name="memberId">ID of the member to kick</param>
        Task KickMemberAsync(GId teamId, GId memberId);
        /// <summary>
        /// Kicks a member from a team.
        /// </summary>
        /// <param name="teamId">ID of the team to kick from</param>
        /// <param name="memberId">ID of the member to kick</param>
        void KickMember(GId teamId, GId memberId);
        /// <summary>
        /// Bans a member from a team.
        /// </summary>
        /// <param name="teamId">ID of the team to ban from</param>
        /// <param name="memberId">ID of the member to ban</param>
        /// <param name="reason">Reason for banning this user</param>
        /// <param name="deleteHistoryOption">Either 7(for 1 week) or 24(for 1 day)</param>
        Task BanMemberAsync(GId teamId, GId memberId, string reason, uint deleteHistoryOption);
        /// <summary>
        /// Bans a member from a team.
        /// </summary>
        /// <param name="teamId">ID of the team to ban from</param>
        /// <param name="memberId">ID of the member to ban</param>
        /// <param name="reason">Reason for banning this user</param>
        /// <param name="deleteHistoryOption">Either 7(for 1 week) or 24(for 1 day)</param>
        void BanMember(GId teamId, GId memberId, string reason, uint deleteHistoryOption);
        /// <summary>
        /// Unbans a member in a team.
        /// </summary>
        /// <param name="teamId">ID of the team to unban in</param>
        /// <param name="memberId">ID of the member to unban</param>
        Task UnbanMemberAsync(GId teamId, GId memberId);
        /// <summary>
        /// Unbans a member in a team.
        /// </summary>
        /// <param name="teamId">ID of the team to unban in</param>
        /// <param name="memberId">ID of the member to unban</param>
        void UnbanMember(GId teamId, GId memberId);
        /// <summary>
        /// Gets member with given ID.
        /// </summary>
        /// <param name="team">Team ID</param>
        /// <param name="user">User ID</param>
        /// <returns>Member</returns>
        Task<TeamMember> GetMemberAsync(GId team, GId user);
        /// <summary>
        /// Gets member with given ID.
        /// </summary>
        /// <param name="team">Team ID</param>
        /// <param name="user">User ID</param>
        /// <returns>Member</returns>
        TeamMember GetMember(GId team, GId user);

        //=======================//
        //   Groups
        //=======================//

        /// <summary>
        /// Gets a group by ID.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <param name="groupId">ID of the groupp</param>
        /// <returns>Group</returns>
        Task<Group> GetGroupAsync(GId teamId, GId groupId);
        /// <summary>
        /// Gets a group by ID.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <param name="groupId">ID of the groupp</param>
        /// <returns>Group</returns>
        Group GetGroup(GId teamId, GId groupId);
        /// <summary>
        /// List of groups in given team.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <returns>List of groups</returns>
        Task<IList<Group>> GetGroupsAsync(GId teamId);
        /// <summary>
        /// List of groups in given team.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <returns>List of groups</returns>
        IList<Group> GetGroups(GId teamId);

        //=======================//
        //   Channels
        //=======================//

        /// <summary>
        /// Gets a channel by ID.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <param name="channelId">ID of the channel</param>
        /// <returns>Channel</returns>
        Task<Channel> GetChannelAsync(GId teamId, Guid channelId);
        /// <summary>
        /// Gets a channel by ID.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <param name="channelId">ID of the channel</param>
        /// <returns>Channel</returns>
        Channel GetChannel(GId teamId, Guid channelId);
        /// <summary>
        /// Clears all notifications in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel to clear notifications in</param>
        Task ClearNotificationsAsync(Guid channelId);
        /// <summary>
        /// Clears all notifications in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel to clear notifications in</param>
        void ClearNotifications(Guid channelId);
        /// <summary>
        /// Creates a new channel in a specific team and group.
        /// </summary>
        /// <param name="team">Team to create channel in</param>
        /// <param name="group">Group to create channel in</param>
        /// <param name="type">Channel type</param>
        /// <param name="public">If channel should be public</param>
        /// <param name="name">Name of the channel</param>
        Task CreateChannelAsync(GId team, GId group, ChannelType type, bool @public, string name);
        /// <summary>
        /// Creates a new channel in a specific team and group.
        /// </summary>
        /// <param name="team">Team to create channel in</param>
        /// <param name="group">Group to create channel in</param>
        /// <param name="type">Channel type</param>
        /// <param name="public">If channel should be public</param>
        /// <param name="name">Name of the channel</param>
        void CreateChannel(GId team, GId group, ChannelType type, bool @public, string name);
        /// <summary>
        /// Deletes a channel in a specific team and group.
        /// </summary>
        /// <param name="team">Team to delete channel in</param>
        /// <param name="group">Group to delete channel in</param>
        /// <param name="channel">Channel to be deleted</param>
        Task DeleteChannelAsync(GId team, GId group, Guid channel);
        /// <summary>
        /// Deletes a channel in a specific team and group.
        /// </summary>
        /// <param name="team">Team to delete channel in</param>
        /// <param name="group">Group to delete channel in</param>
        /// <param name="channel">Channel to be deleted</param>
        void DeleteChannel(GId team, GId group, Guid channel);
        /// <summary>
        /// List of channels and categories in given team.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <returns>Channel list</returns>
        Task<Channels> GetChannelsAsync(GId teamId);
        /// <summary>
        /// List of channels and categories in given team.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <returns>Channel list</returns>
        Channels GetChannels(GId teamId);

        //=================================//
        //   Channel & category ordering
        //=================================//

        /// <summary>
        /// Reorders channels by given channel ID array.
        /// </summary>
        /// <param name="teamId">ID of the team channels are in</param>
        /// <param name="categoryId">ID of the category channels are in</param>
        /// <param name="channelList">Channels to reorder</param>
        Task ReorderChannelsAsync(GId teamId, uint? categoryId, params Guid[] channelList);
        /// <summary>
        /// Reorders channels by given channel ID array.
        /// </summary>
        /// <param name="teamId">ID of the team channels are in</param>
        /// <param name="categoryId">ID of the category channels are in</param>
        /// <param name="channelList">Channels to reorder</param>
        void ReorderChannels(GId teamId, uint? categoryId, params Guid[] channelList);
        /// <summary>
        /// Reorders categories by given category ID array.
        /// </summary>
        /// <param name="teamId">ID of the team categirues are in</param>
        /// <param name="categoryList">Categories to reorder</param>
        Task ReorderCategoriesAsync(GId teamId, params uint[] categoryList);
        /// <summary>
        /// Reorders categories by given category ID array.
        /// </summary>
        /// <param name="teamId">ID of the team categories are in</param>
        /// <param name="categoryList">Chategories to reorder</param>
        void ReorderCategories(GId teamId, params uint[] categoryList);
        /// <summary>
        /// Assigns a channel to a specific category.
        /// </summary>
        /// <param name="teamId">ID of the team where channel is in</param>
        /// <param name="categoryId">Category where channel should be in</param>
        /// <param name="channelId">ID of the channel to move</param>
        /// <param name="shouldRoleSync">If role permissions should be synced with category</param>
        Task AssignToCategoryAsync(GId teamId, uint categoryId, Guid channelId, bool shouldRoleSync);
        /// <summary>
        /// Assigns a channel to a specific category.
        /// </summary>
        /// <param name="teamId">ID of the team where channel is in</param>
        /// <param name="categoryId">Category where channel should be in</param>
        /// <param name="channelId">ID of the channel to move</param>
        /// <param name="shouldRoleSync">If role permissions should be synced with category</param>
        void AssignToCategory(GId teamId, uint categoryId, Guid channelId, bool shouldRoleSync);
        /// <summary>
        /// Unassigns a channel from a category.
        /// </summary>
        /// <param name="teamId">ID of the team where channel is in</param>
        /// <param name="channelId">ID of the channel to remove from category</param>
        Task UnassignFromCategoryAsync(GId teamId, Guid channelId);
        /// <summary>
        /// Unassigns a channel from a category.
        /// </summary>
        /// <param name="teamId">ID of the team where channel is in</param>
        /// <param name="channelId">ID of the channel to remove from category</param>
        void UnassignFromCategory(GId teamId, Guid channelId);

        //==================================//
        //   Channel & category permissions
        //==================================//

        /// <summary>
        /// Adds a role to a channel.
        /// </summary>
        /// <param name="teamId">ID of the team where channel is in</param>
        /// <param name="channelId">ID of the channel to add role in</param>
        /// <param name="roleId">ID of the role to add</param>
        /// <returns>Updated channel</returns>
        Task<Channel> AddChannelRoleAsync(GId teamId, Guid channelId, uint roleId);
        /// <summary>
        /// Adds a role to a channel.
        /// </summary>
        /// <param name="teamId">ID of the team where channel is in</param>
        /// <param name="channelId">ID of the channel to add role in</param>
        /// <param name="roleId">ID of the role to add</param>
        /// <returns>Updated channel</returns>
        Channel AddChannelRole(GId teamId, Guid channelId, uint roleId);
        /// <summary>
        /// Removes a role from a channel.
        /// </summary>
        /// <param name="teamId">ID of the team where channel is in</param>
        /// <param name="channelId">ID of the channel to remove role in</param>
        /// <param name="roleId">ID of the role to remove</param>
        /// <returns>Updated channel</returns>
        Task<Channel> RemoveChannelRoleAsync(GId teamId, Guid channelId, uint roleId);
        /// <summary>
        /// Removes a role from a channel.
        /// </summary>
        /// <param name="teamId">ID of the team where channel is in</param>
        /// <param name="channelId">ID of the channel to remove role in</param>
        /// <param name="roleId">ID of the role to remove</param>
        /// <returns>Updated channel</returns>
        Channel RemoveChannelRole(GId teamId, Guid channelId, uint roleId);
        /// <summary>
        /// Adds a role to a category.
        /// </summary>
        /// <param name="teamId">ID of the team where category is in</param>
        /// <param name="categoryId">ID of the category to add role in</param>
        /// <param name="roleId">ID of the role to add</param>
        /// <returns>Updated category</returns>
        Task<Category> AddCategoryRoleAsync(GId teamId, uint categoryId, uint roleId);
        /// <summary>
        /// Adds a role to a category.
        /// </summary>
        /// <param name="teamId">ID of the team where category is in</param>
        /// <param name="categoryId">ID of the category to add role in</param>
        /// <param name="roleId">ID of the role to add</param>
        /// <returns>Updated category</returns>
        Category AddCategoryRole(GId teamId, uint categoryId, uint roleId);
        /// <summary>
        /// Removes a role from a category.
        /// </summary>
        /// <param name="teamId">ID of the team where category is in</param>
        /// <param name="categoryId">ID of the category to remove role in</param>
        /// <param name="roleId">ID of the role to remove</param>
        /// <returns>Updated category</returns>
        Task<Category> RemoveCategoryRoleAsync(GId teamId, uint categoryId, uint roleId);
        /// <summary>
        /// Removes a role from a category.
        /// </summary>
        /// <param name="teamId">ID of the team where category is in</param>
        /// <param name="categoryId">ID of the category to remove role in</param>
        /// <param name="roleId">ID of the role to remove</param>
        /// <returns>Updated category</returns>
        Category RemoveCategoryRole(GId teamId, uint categoryId, uint roleId);
        /// <summary>
        /// Adds a user permission to a channel.
        /// </summary>
        /// <param name="teamId">ID of the team where channel is in</param>
        /// <param name="channelId">ID of the channel to add user in</param>
        /// <param name="userId">ID of the user to add</param>
        /// <returns>Updated channel</returns>
        Task<Channel> AddChannelUserAsync(GId teamId, Guid channelId, GId userId);
        /// <summary>
        /// Adds a user permission to a channel.
        /// </summary>
        /// <param name="teamId">ID of the team where channel is in</param>
        /// <param name="channelId">ID of the channel to add user in</param>
        /// <param name="userId">ID of the user to add</param>
        /// <returns>Updated channel</returns>
        Channel AddChannelUser(GId teamId, Guid channelId, GId userId);
        /// <summary>
        /// Removes a user permission from a channel.
        /// </summary>
        /// <param name="teamId">ID of the team where channel is in</param>
        /// <param name="channelId">ID of the channel to remove user permission in</param>
        /// <param name="userId">ID of the user to remove permissions of</param>
        /// <returns>Updated channel</returns>
        Task<Channel> RemoveChannelUserAsync(GId teamId, Guid channelId, GId userId);
        /// <summary>
        /// Removes a user permission from a channel.
        /// </summary>
        /// <param name="teamId">ID of the team where channel is in</param>
        /// <param name="channelId">ID of the channel to remove user permission in</param>
        /// <param name="userId">ID of the user to remove permissions of</param>
        /// <returns>Updated channel</returns>
        Channel RemoveChannelUser(GId teamId, Guid channelId, GId userId);
        /// <summary>
        /// Adds a user permission to a category.
        /// </summary>
        /// <param name="teamId">ID of the team where category is in</param>
        /// <param name="categoryId">ID of the category to add user in</param>
        /// <param name="userId">ID of the user to add</param>
        /// <returns>Updated category</returns>
        Task<Category> AddCategoryUserAsync(GId teamId, uint categoryId, GId userId);
        /// <summary>
        /// Adds a user permission to a category.
        /// </summary>
        /// <param name="teamId">ID of the team where category is in</param>
        /// <param name="categoryId">ID of the category to add user in</param>
        /// <param name="userId">ID of the user to add</param>
        /// <returns>Updated category</returns>
        Category AddCategoryUser(GId teamId, uint categoryId, GId userId);
        /// <summary>
        /// Removes a user permission from a category.
        /// </summary>
        /// <param name="teamId">ID of the team where category is in</param>
        /// <param name="categoryId">ID of the category to remove user permission in</param>
        /// <param name="userId">ID of the user to remove permissions of</param>
        /// <returns>Updated category</returns>
        Task<Category> RemoveCategoryUserAsync(GId teamId, uint categoryId, GId userId);
        /// <summary>
        /// Removes a user permission from a category.
        /// </summary>
        /// <param name="teamId">ID of the team where category is in</param>
        /// <param name="categoryId">ID of the category to remove user permission in</param>
        /// <param name="userId">ID of the user to remove permissions of</param>
        /// <returns>Updated category</returns>
        Category RemoveCategoryUser(GId teamId, uint categoryId, GId userId);

        //=======================//
        //   Forms
        //=======================//

        /// <summary>
        /// Creates a form for form node.
        /// </summary>
        /// <param name="form">Form to create</param>
        /// <returns>Form ID</returns>
        Task<uint> CreateFormAsync(BasicGuildedForm form);
        /// <summary>
        /// Creates a form for form node.
        /// </summary>
        /// <param name="form">Form to create</param>
        /// <returns>Form ID</returns>
        uint CreateForm(BasicGuildedForm form);
        /// <summary>
        /// Gets a form or a poll by form ID.
        /// </summary>
        /// <param name="formId">ID of the form to get</param>
        /// <returns>A form and a form response</returns>
        Task<FormData> GetFormAsync(uint formId);
        /// <summary>
        /// Gets a form or a poll by form ID.
        /// </summary>
        /// <param name="formId">ID of the form to get</param>
        /// <returns>A form and a form response</returns>
        FormData GetForm(uint formId);
        /// <summary>
        /// Submits a form response.
        /// </summary>
        /// <param name="formId">Form ID it is responding to</param>
        /// <param name="response">Response to submit</param>
        /// <returns>Response ID</returns>
        Task<uint> PostFormResponseAsync(uint formId, BasicFormResponse response);
        /// <summary>
        /// Submits a form response.
        /// </summary>
        /// <param name="formId">Form ID it is responding to</param>
        /// <param name="response">Response to submit</param>
        /// <returns>Response ID</returns>
        uint PostFormResponse(uint formId, BasicFormResponse response);
    }
}