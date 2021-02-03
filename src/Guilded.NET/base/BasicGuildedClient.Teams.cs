using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using RestSharp;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Guilded.NET {
    using API;
    using Objects;
    using Objects.Chat;
    using Objects.Teams;
    using Objects.Forms;
    using Objects.Content;

    /// <summary>
    /// Logged-in user in Guilded.
    /// </summary>
    public abstract partial class BasicGuildedClient: IGuildedClient {
        /// <summary>
        /// Gets team with given ID.
        /// </summary>
        /// <param name="id">Team ID</param>
        /// <returns>Team</returns>
        public async Task<Team> GetTeamAsync(GId id) =>
            await FromObject<Team>(new Endpoint($"/teams/{id}", Method.GET), "team");
        /// <summary>
        /// Gets team with given ID. Sync version of <see cref="GetTeamAsync"/>.
        /// </summary>
        /// <param name="id">Team ID</param>
        /// <returns>Team</returns>
        public Team GetTeam(GId id) =>
            GetTeamAsync(id).GetAwaiter().GetResult();
                /// <summary>
        /// Joins a specific team.
        /// </summary>
        /// <param name="team">Team to join</param>
        /// <returns>Async task</returns>
        public async Task JoinTeamAsync(GId team) =>
            await ExecuteRequest(new Endpoint($"teams/{team}/members/{Me.User.Id}/join", Method.PUT), new JsonBody("{}"));
        /// <summary>
        /// Joins a specific team. Sync version of <see cref="JoinTeamAsync"/>.
        /// </summary>
        /// <param name="team">Team to join</param>
        public void JoinTeam(GId team) =>
            JoinTeamAsync(team).GetAwaiter().GetResult();
        /// <summary>
        /// Leaves a specific team.
        /// </summary>
        /// <param name="team">Team to leave</param>
        /// <returns>Async task</returns>
        public async Task LeaveTeamAsync(GId team) =>
            await ExecuteRequest(new Endpoint($"teams/{team}/members/{Me.User.Id}", Method.DELETE));
        /// <summary>
        /// Leaves a specific team. Sync version of <see cref="JoinTeamAsync"/>.
        /// </summary>
        /// <param name="team">Team to leave</param>
        public void LeaveTeam(GId team) =>
            LeaveTeamAsync(team).GetAwaiter().GetResult();
        /// <summary>
        /// List of groups in given team.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <returns>List of groups</returns>
        public async Task<IList<Group>> GetGroupsAsync(GId teamId) =>
            await FromObject<IList<Group>>(new Endpoint($"/teams/{teamId}/groups", Method.GET), "groups");
        /// <summary>
        /// List of groups in given team. Sync version of <see cref="GetGroupsAsync"/>.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <returns>List of groups</returns>
        public IList<Group> GetGroups(GId teamId) =>
            GetGroupsAsync(teamId).GetAwaiter().GetResult();
        /// <summary>
        /// Gets a group by ID.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <param name="groupId">ID of the groupp</param>
        /// <returns>Group</returns>
        public async Task<Group> GetGroupAsync(GId teamId, GId groupId) =>
            await FromObject<Group>(new Endpoint($"teams/{teamId}/groups/{groupId}", Method.GET), "group");
        /// <summary>
        /// Gets a group by ID.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <param name="groupId">ID of the groupp</param>
        /// <returns>Group</returns>
        public Group GetGroup(GId teamId, GId groupId) =>
            GetGroupAsync(teamId, groupId).GetAwaiter().GetResult();
        /// <summary>
        /// List of channels and categories in given team.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <returns>Channel list</returns>
        public async Task<Channels> GetChannelsAsync(GId teamId) =>
            await FromObject<Channels>(new Endpoint($"/teams/{teamId}/channels", Method.GET));
        /// <summary>
        /// List of channels and categories in given team. Sync version of <see cref="GetChannelsAsync"/>.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <returns>Channel list</returns>
        public Channels GetChannels(GId teamId) =>
            GetChannelsAsync(teamId).GetAwaiter().GetResult();
        /// <summary>
        /// Gets a channel by ID.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <param name="channelId">ID of the channel</param>
        /// <returns>Channel</returns>
        public async Task<Channel> GetChannelAsync(GId teamId, Guid channelId) =>
            await FromObject<Channel>(new Endpoint($"teams/{teamId}/channels/{channelId}", Method.GET), "channel");
        /// <summary>
        /// Gets a channel by ID.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <param name="channelId">ID of the channel</param>
        /// <returns>Channel</returns>
        public Channel GetChannel(GId teamId, Guid channelId) =>
            GetChannelAsync(teamId, channelId).GetAwaiter().GetResult();
        /// <summary>
        /// Creates a new channel in a specific team and group. Sync version of <see cref="CreateChannelAsync"/>.
        /// </summary>
        /// <param name="team">Team to create channel in</param>
        /// <param name="group">Group to create channel in</param>
        /// <param name="type">Channel type</param>
        /// <param name="public">If channel should be public</param>
        /// <param name="name">Name which should be assigned to the channel</param>
        public async Task CreateChannelAsync(GId team, GId group, ChannelType type, bool @public, string name) =>
            await ExecuteRequest(new Endpoint($"teams/{team}/groups/{group}/channels", Method.POST), new JsonBody(new {
                name,
                contentType = type,
                isPublic = @public
            }, Converters));
        /// <summary>
        /// Creates a new channel in a specific team and group. Sync version of <see cref="CreateChannelAsync"/>.
        /// </summary>
        /// <param name="team">Team to create channel in</param>
        /// <param name="group">Group to create channel in</param>
        /// <param name="type">Channel type</param>
        /// <param name="public">If channel should be public</param>
        /// <param name="name">Name which should be assigned to the channel</param>
        public void CreateChannel(GId team, GId group, ChannelType type, bool @public, string name) =>
            CreateChannelAsync(team, group, type, @public, name).GetAwaiter().GetResult();
        /// <summary>
        /// Deletes a channel in a specific team and group.
        /// </summary>
        /// <param name="team">Team to delete channel in</param>
        /// <param name="group">Group to delete channel in</param>
        /// <param name="channel">Channel to be deleted</param>
        /// <returns>Async task</returns>
        public async Task DeleteChannelAsync(GId team, GId group, Guid channel) =>
            await ExecuteRequest(new Endpoint($"teams/{team}/groups/{group}/channels/{channel}", Method.DELETE));
        /// <summary>
        /// Deletes a channel in a specific team and group. Sync version of <see cref="DeleteChannelAsync"/>.
        /// </summary>
        /// <param name="team">Team to delete channel in</param>
        /// <param name="group">Group to delete channel in</param>
        /// <param name="channel">Channel to be deleted</param>
        public void DeleteChannel(GId team, GId group, Guid channel) =>
            DeleteChannelAsync(team, group, channel).GetAwaiter().GetResult();
        /// <summary>
        /// Gets member with given ID.
        /// </summary>
        /// <param name="team">Team ID</param>
        /// <param name="user">User ID</param>
        /// <returns>Member</returns>
        public async Task<TeamMember> GetMemberAsync(GId team, GId user) =>
            (await GetTeamAsync(team)).Members.First(x => x.Id == user);
        /// <summary>
        /// Gets member with given ID. Sync version of <see cref="GetMemberAsync"/>.
        /// </summary>
        /// <param name="team">Team ID</param>
        /// <param name="user">User ID</param>
        /// <returns>Member</returns>
        public TeamMember GetMember(GId team, GId user) =>
            GetMemberAsync(team, user).GetAwaiter().GetResult();
        /// <summary>
        /// Creates a form for form node.
        /// </summary>
        /// <param name="form">Form to create</param>
        /// <returns>Form ID</returns>
        public async Task<uint> CreateFormAsync(BasicGuildedForm form) =>
            await FromObject<uint>(new Endpoint($"content/custom_forms", Method.PUT), "customFormId", new JsonBody(form, Converters));
        /// <summary>
        /// Creates a form for form node.
        /// </summary>
        /// <param name="form">Form to create</param>
        /// <returns>Form ID</returns>
        public uint CreateForm(BasicGuildedForm form) =>
            CreateFormAsync(form).GetAwaiter().GetResult();
        /// <summary>
        /// Gets a form or a poll by form ID.
        /// </summary>
        /// <param name="formId">ID of the form to get</param>
        /// <returns>A form and a form response</returns>
        public async Task<FormData> GetFormAsync(uint formId) =>
            await FromObject<FormData>(new Endpoint($"content/custom_forms/{formId}", Method.GET));
        /// <summary>
        /// Gets a form or a poll by form ID.
        /// </summary>
        /// <param name="formId">ID of the form to get</param>
        /// <returns>A form and a form response</returns>
        public FormData GetForm(uint formId) =>
            GetFormAsync(formId).GetAwaiter().GetResult();
        /// <summary>
        /// Submits a form response.
        /// </summary>
        /// <param name="formId">Form ID it is responding to</param>
        /// <param name="response">Response to submit</param>
        /// <returns>Response ID</returns>
        public async Task<uint> PostFormResponseAsync(uint formId, BasicFormResponse response) =>
            await FromObject<uint>(new Endpoint($"content/custom_forms/{formId}/responses", Method.PUT), "customFormResponseId", new JsonBody(response, Converters));
        /// <summary>
        /// Submits a form response.
        /// </summary>
        /// <param name="formId">Form ID it is responding to</param>
        /// <param name="response">Response to submit</param>
        /// <returns>Response ID</returns>
        public uint PostFormResponse(uint formId, BasicFormResponse response) =>
            PostFormResponseAsync(formId, response).GetAwaiter().GetResult();
        /// <summary>
        /// Gets an overview page of a team.
        /// </summary>
        /// <param name="teamId">Team to get overview of</param>
        /// <returns>Team overview page</returns>
        public async Task<TeamOverview> GetOverviewAsync(GId teamId) =>
            await FromObject<TeamOverview>(new Endpoint($"teams/{teamId}/overview", Method.GET));
        /// <summary>
        /// Gets an overview page of a team.
        /// </summary>
        /// <param name="teamId">Team to get overview of</param>
        /// <returns>Team overview page</returns>
        public TeamOverview GetOverview(GId teamId) =>
            GetOverviewAsync(teamId).GetAwaiter().GetResult();
        /// <summary>
        /// Gets a list of announcements in a team.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <param name="maxItems">How many announcements it should get</param>
        /// <param name="beforeDate">Before which date it should get announcements</param>
        /// <returns>List of announcements</returns>
        public async Task<IList<Announcement>> GetAnnouncementsAsync(GId teamId, uint? maxItems = 10, DateTime? beforeDate = null) =>
            await FromObject<IList<Announcement>>(new Endpoint($"teams/{teamId}/announcements?maxItems={maxItems}&beforeDate={beforeDate ?? DateTime.Now}", Method.GET), "");
        /// <summary>
        /// Gets a list of announcements in a team.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <param name="maxItems">How many announcements it should get</param>
        /// <param name="beforeDate">Before which date it should get announcements</param>
        /// <returns>List of announcements</returns>
        public IList<Announcement> GetAnnouncements(GId teamId, uint? maxItems = 10, DateTime? beforeDate = null) =>
            GetAnnouncementsAsync(teamId, maxItems, beforeDate).GetAwaiter().GetResult();
        /// <summary>
        /// Gets all comments in a given announcement.
        /// </summary>
        /// <param name="announcementId">ID of the announcement</param>
        /// <returns>List of content replies</returns>
        public async Task<IList<AnnouncementReply>> GetAnnouncementRepliesAsync(GId announcementId) =>
            await FromArray<AnnouncementReply>(new Endpoint($"content/announcement/{announcementId}/replies", Method.GET));
        /// <summary>
        /// Gets all comments in a given announcement.
        /// </summary>
        /// <param name="announcementId">ID of the announcement</param>
        /// <returns>List of content replies</returns>
        public IList<AnnouncementReply> GetAnnouncementReplies(GId announcementId) =>
            GetAnnouncementRepliesAsync(announcementId).GetAwaiter().GetResult();
                /// <summary>
        /// Gets a list of pinned announcements in a team.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <returns>List of announcements</returns>
        public async Task<IList<Announcement>> GetPinnedAnnouncementsAsync(GId teamId) =>
            await FromObject<IList<Announcement>>(new Endpoint($"teams/{teamId}/announcements/pinned", Method.GET), "announcements");
        /// <summary>
        /// Gets a list of pinned announcements in a team.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <returns>List of announcements</returns>
        public IList<Announcement> GetPinnedAnnouncements(GId teamId) =>
            GetPinnedAnnouncementsAsync(teamId).GetAwaiter().GetResult();
        /// <summary>
        /// Sets a new nickname for a member.
        /// </summary>
        /// <param name="teamId">ID of the team to change nickname in</param>
        /// <param name="memberId">ID of the member to change nickname of</param>
        /// <param name="nickname">A new nickname to set</param>
        public async Task SetNicknameAsync(GId teamId, GId memberId, string nickname) =>
            await ExecuteRequest(new Endpoint($"teams/{teamId}/members/{memberId}/nickname", Method.PUT), new JsonBody(JsonConvert.SerializeObject(new { nickname })));
        /// <summary>
        /// Sets a new nickname for a member.
        /// </summary>
        /// <param name="teamId">ID of the team to change nickname in</param>
        /// <param name="memberId">ID of the member to change nickname of</param>
        /// <param name="nickname">A new nickname to set</param>
        public void SetNickname(GId teamId, GId memberId, string nickname) =>
            SetNicknameAsync(teamId, memberId, nickname).GetAwaiter().GetResult();
        /// <summary>
        /// Kicks a member from a server.
        /// </summary>
        /// <param name="teamId">ID of the team to kick from</param>
        /// <param name="memberId">ID of the member to kick</param>
        public async Task KickMemberAsync(GId teamId, GId memberId) =>
            await ExecuteRequest(new Endpoint($"teams/{teamId}/members/{memberId}", Method.DELETE));
        /// <summary>
        /// Kicks a member from a server.
        /// </summary>
        /// <param name="teamId">ID of the team to kick from</param>
        /// <param name="memberId">ID of the member to kick</param>
        public void KickMember(GId teamId, GId memberId) =>
            KickMemberAsync(teamId, memberId).GetAwaiter().GetResult();
        /// <summary>
        /// Bans a member from a server.
        /// </summary>
        /// <param name="teamId">ID of the team to ban from</param>
        /// <param name="memberId">ID of the member to ban</param>
        /// <param name="reason">Reason for banning this user</param>
        /// <param name="deleteHistoryOption">Either 7(for 1 week) or 24(for 1 day)</param>
        public async Task BanMemberAsync(GId teamId, GId memberId, string reason, uint deleteHistoryOption) =>
            await ExecuteRequest(new Endpoint($"teams/{teamId}/members/{memberId}/ban", Method.DELETE), new JsonBody(JsonConvert.SerializeObject(
                new {
                    deleteHistoryOption,
                    teamId,
                    memberId,
                    reason,
                    afterDate = DateTime.Now
                }
            )));
        /// <summary>
        /// Bans a member from a server.
        /// </summary>
        /// <param name="teamId">ID of the team to ban from</param>
        /// <param name="memberId">ID of the member to ban</param>
        /// <param name="reason">Reason for banning this user</param>
        /// <param name="deleteHistoryOption">Either 7(for 1 week) or 24(for 1 day)</param>
        public void BanMember(GId teamId, GId memberId, string reason, uint deleteHistoryOption) =>
            BanMemberAsync(teamId, memberId, reason, deleteHistoryOption).GetAwaiter().GetResult();
        /// <summary>
        /// Unbans a member in a team.
        /// </summary>
        /// <param name="teamId">ID of the team to unban in</param>
        /// <param name="memberId">ID of the member to unban</param>
        public async Task UnbanMemberAsync(GId teamId, GId memberId) =>
            await ExecuteRequest(new Endpoint($"teams/{teamId}/members/{memberId}/ban", Method.PUT), new JsonBody(JsonConvert.SerializeObject(
                new {
                    teamId,
                    memberId
                }
            )));
        /// <summary>
        /// Unbans a member in a team.
        /// </summary>
        /// <param name="teamId">ID of the team to unban in</param>
        /// <param name="memberId">ID of the member to unban</param>
        public void UnbanMember(GId teamId, GId memberId) =>
            UnbanMemberAsync(teamId, memberId).GetAwaiter().GetResult();
    }
}