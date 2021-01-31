using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Guilded.NET.Objects {
    using Forms;
    using Teams;
    public partial interface IGuildedClient {
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
        /// Clears all notifications in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel to clear notifications in</param>
        /// <returns>Async task</returns>
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
        /// <returns>Async task</returns>
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
        /// <returns>Async task</returns>
        Task DeleteChannelAsync(GId team, GId group, Guid channel);
        /// <summary>
        /// Deletes a channel in a specific team and group.
        /// </summary>
        /// <param name="team">Team to delete channel in</param>
        /// <param name="group">Group to delete channel in</param>
        /// <param name="channel">Channel to be deleted</param>
        void DeleteChannel(GId team, GId group, Guid channel);
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
    }
}