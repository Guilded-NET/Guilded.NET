using Guilded.NET.Objects.Events;
using System;
using Guilded.NET.Objects.Chat;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Guilded.NET.Objects {
    using Forms;
    using Guilded.NET.Objects.Content;
    using Teams;
    /// <summary>
    /// Represents any Guilded client.
    /// </summary>
    public interface IGuildedClient {
        /// <summary>
        /// Serializer used to (de)serialize JSON given by Guilded or made for Guilded.
        /// </summary>
        /// <value>Serializer</value>
        JsonSerializer GuildedSerializer {
            get; set;
        }
        /// <summary>
        /// Event when someone posts a message in the chat.
        /// </summary>
        event EventHandler<MessageCreatedEvent> MessageCreated;
        /// <summary>
        /// Event when someone starts typing in the chat.
        /// </summary>
        event EventHandler<UserTypingEvent> UserTyping;
        /// <summary>
        /// Sends a message into the chat.
        /// </summary>
        /// <param name="channel">ID of the channel</param>
        /// <param name="message">Message to post</param>
        /// <returns>Response</returns>
        Task<object> SendMessageAsync(Guid channel, NewMessage message);
        /// <summary>
        /// Sends a message into the chat.
        /// </summary>
        /// <param name="channel">ID of the channel</param>
        /// <param name="message">Message to post</param>
        void SendMessage(Guid channel, NewMessage message);
        /// <summary>
        /// Edits message of the bot posted in the chat.
        /// </summary>
        /// <param name="channel">ID of the channel</param>
        /// <param name="messageId">ID of the message to edit</param>
        /// <param name="content">New content of the message</param>
        /// <returns>Response</returns>
        Task<object> EditMessageAsync(Guid channel, Guid messageId, MessageContent content);
        /// <summary>
        /// Edits message of the bot posted in the chat.
        /// </summary>
        /// <param name="channel">ID of the channel</param>
        /// <param name="messageId">ID of the message to edit</param>
        /// <param name="content">New content of the message</param>
        void EditMessage(Guid channel, Guid messageId, MessageContent content);
        /// <summary>
        /// Deletes a message posted in the chat.
        /// </summary>
        /// <param name="channel">ID of the channel</param>
        /// <param name="messageId">ID of the message to delete</param>
        /// <returns>Response</returns>
        Task<object> DeleteMessageAsync(Guid channel, Guid messageId);
        /// <summary>
        /// Deletes a message posted in the chat.
        /// </summary>
        /// <param name="channel">ID of the channel</param>
        /// <param name="messageId">ID of the message to delete</param>
        void DeleteMessage(Guid channel, Guid messageId);
        /// <summary>
        /// Gets user this client is using.
        /// </summary>
        /// <returns>Current User</returns>
        Task<Me> GetThisUserAsync();
        /// <summary>
        /// Gets user this client is using.
        /// </summary>
        /// <returns>Current User</returns>
        Me GetThisUser();
        /// <summary>
        /// Gets user with given ID.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>User</returns>
        Task<User> GetUserAsync(GId id);
        /// <summary>
        /// Gets user with given ID.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>User</returns>
        User GetUser(GId id);
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
        /// <summary>
        /// Gets a message in a specific channel.
        /// </summary>
        /// <param name="subdomain">Subdomain of a team where that message and channel of that message is</param>
        /// <param name="groupId">ID of the group where that channel is in</param>
        /// <param name="channelId">ID of the channel where that message is</param>
        /// <param name="messageId">ID of message it should get</param>
        /// <returns>Message</returns>
        Task<Message> GetMessageAsync(string subdomain, GId groupId, Guid channelId, Guid messageId);
        /// <summary>
        /// Gets a message in a specific channel.
        /// </summary>
        /// <param name="subdomain">Subdomain of a team where that message and channel of that message is</param>
        /// <param name="groupId">ID of the group where that channel is in</param>
        /// <param name="channelId">ID of the channel where that message is</param>
        /// <param name="messageId">ID of message it should get</param>
        /// <returns>Message</returns>
        Message GetMessage(string subdomain, GId groupId, Guid channelId, Guid messageId);
        /// <summary>
        /// Changes the name of the user.
        /// </summary>
        /// <param name="name">New name</param>
        /// <returns>Async task</returns>
        Task ChangeNameAsync(string name);
        /// <summary>
        /// Changes the name of the user.
        /// </summary>
        /// <param name="name">New name</param>
        void ChangeName(string name);
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
        /// Gets messages with a specific limit.
        /// </summary>
        /// <param name="channel">Channel to get messages in</param>
        /// <param name="limit">How many messages it should get</param>
        /// <returns>List of messages</returns>
        Task<IList<Message>> GetMessagesAsync(Guid channel, uint limit);
        /// <summary>
        /// Gets messages with a specific limit.
        /// </summary>
        /// <param name="channel">Channel to get messages in</param>
        /// <param name="limit">How many messages it should get</param>
        /// <returns>List of messages</returns>
        IList<Message> GetMessages(Guid channel, uint limit);
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
        /// Changes user's presence.
        /// </summary>
        /// <param name="presence">New presence</param>
        /// <returns>Async task</returns>
        Task<object> ChangePresenceAsync(Presence presence);
        /// <summary>
        /// Changes user's presence.
        /// </summary>
        /// <param name="presence">New presence</param>
        void ChangePresence(Presence presence);
        /// <summary>
        /// Changes user's status message and emote.
        /// </summary>
        /// <param name="status">New status</param>
        /// <returns>Async task</returns>
        Task ChangeStatusAsync(UserStatus status);
        /// <summary>
        /// Changes user's status message and emote.
        /// </summary>
        /// <param name="status">New status</param>
        void ChangeStatus(UserStatus status);
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
        /// Gets all DM channels.
        /// </summary>
        /// <returns>Channel</returns>
        Task<IList<DMChannel>> GetDMChannelsAsync();
        /// <summary>
        /// Gets all DM channels.
        /// </summary>
        /// <returns>Channel</returns>
        IList<DMChannel> GetDMChannels();
        /// <summary>
        /// Creates a new DM channel.
        /// </summary>
        /// <param name="users">What users it should add. 1 for normal DMs, 2 for DM group</param>
        /// <returns>Channel</returns>
        Task<DMChannel> CreateDMChannelAsync(params GId[] users);
        /// <summary>
        /// Creates a new DM channel.
        /// </summary>
        /// <param name="users">What users it should add. 1 for normal DMs, 2 for DM group</param>
        /// <returns>Channel</returns>
        DMChannel CreateDMChannel(params GId[] users);
        /// <summary>
        /// Gets forum posts from a specific forum channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="beforeDate">Before what date it should get posts</param>
        /// <param name="maxItems">How many forum posts it should get</param>
        /// <returns>Forum post list</returns>
        Task<IList<ForumPost>> GetForumPostsAsync(Guid channelId, uint? maxItems, DateTime? beforeDate);
        /// <summary>
        /// Gets forum posts from a specific forum channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="beforeDate">Before what date it should get posts</param>
        /// <param name="maxItems">How many forum posts it should get</param>
        /// <returns>Forum post list</returns>
        IList<ForumPost> GetForumPosts(Guid channelId, uint? maxItems, DateTime? beforeDate);
        /// <summary>
        /// Gets forum posts from a specific forum channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="postId">ID of the post it should get replies of</param>
        /// <param name="maxItems">How many forum posts it should get</param>
        /// <param name="afterDate">After what date it should get posts</param>
        /// <returns>Forum reply list</returns>
        Task<IList<ForumReply>> GetForumRepliesAsync(Guid channelId, uint postId, uint? maxItems, DateTime? afterDate);
        /// <summary>
        /// Gets forum replies from a forum post.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="postId">ID of the post it should get replies from</param>
        /// <param name="maxItems">How many forum posts it should get</param>
        /// <param name="afterDate">After what date it should get posts</param>
        /// <returns>Forum reply list</returns>
        IList<ForumReply> GetForumReplies(Guid channelId, uint postId, uint? maxItems, DateTime? afterDate);
        /// <summary>
        /// Create a forum post in a forum channel.
        /// </summary>
        /// <param name="channelId">ID of the forum channel</param>
        /// <param name="title">A title of the forum post</param>
        /// <param name="message">Content of the forum post</param>
        /// <returns>Async task</returns>
        Task CreateForumPostAsync(Guid channelId, string title, MessageContent message);
        /// <summary>
        /// Create a forum post in a forum channel.
        /// </summary>
        /// <param name="channelId">ID of the forum channel</param>
        /// <param name="title">A title of the forum post</param>
        /// <param name="message">Content of the forum post</param>
        void CreateForumPost(Guid channelId, string title, MessageContent message);
        /// <summary>
        /// Replies to a forum post.
        /// </summary>
        /// <param name="channelId">ID of the forum channel</param>
        /// <param name="postId">ID of the post it should reply to</param>
        /// <param name="message">Content of the forum post</param>
        /// <returns>Async task</returns>
        Task CreateForumReplyAsync(Guid channelId, uint postId, MessageContent message);
        /// <summary>
        /// Replies to a forum post.
        /// </summary>
        /// <param name="channelId">ID of the forum channel</param>
        /// <param name="postId">ID of the post it should reply to</param>
        /// <param name="message">Content of the forum post</param>
        void CreateForumReply(Guid channelId, uint postId, MessageContent message);
        /// <summary>
        /// Gets all documents within a specific channel with given max count and before given date.
        /// </summary>
        /// <param name="channelId">ID of channel to fetch documents from</param>
        /// <param name="maxItems">Amount of documents to get</param>
        /// <param name="beforeDate">Date before which it should get documents</param>
        /// <returns>List of documents</returns>
        Task<IList<GuildedDocument>> GetDocumentsAsync(Guid channelId, uint? maxItems, DateTime? beforeDate);
        /// <summary>
        /// Gets all documents within a specific channel with given max count and before given date.
        /// </summary>
        /// <param name="channelId">ID of channel to fetch documents from</param>
        /// <param name="maxItems">Amount of documents to get</param>
        /// <param name="beforeDate">Date before which it should get documents</param>
        /// <returns>List of documents</returns>
        IList<GuildedDocument> GetDocuments(Guid channelId, uint? maxItems, DateTime? beforeDate);
        /// <summary>
        /// Gets a specific document in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of channel to fetch documents from</param>
        /// <param name="docId">ID of the document</param>
        /// <returns>Document</returns>
        Task<GuildedDocument> GetDocumentAsync(Guid channelId, uint docId);
        /// <summary>
        /// Gets a specific document in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of channel to fetch documents from</param>
        /// <param name="docId">ID of the document</param>
        /// <returns>Document</returns>
        GuildedDocument GetDocument(Guid channelId, uint docId);
        /// <summary>
        /// Gets all medias within a specific channel with given max count and before given date.
        /// </summary>
        /// <param name="channelId">ID of channel to fetch media from</param>
        /// <returns>List of media posts</returns>
        Task<IList<GuildedMedia>> GetMediaAsync(Guid channelId);
        /// <summary>
        /// Gets all medias within a specific channel with given max count and before given date.
        /// </summary>
        /// <param name="channelId">ID of channel to fetch media from</param>
        /// <returns>List of media posts</returns>
        IList<GuildedMedia> GetMedia(Guid channelId);
        /// <summary>
        /// Gets given amount of events from a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="maxItems">How many events it should get</param>
        /// <param name="endDate">At which date it should end</param>
        /// <param name="startDate">At which date it should start</param>
        /// <returns>List of calendar events</returns>
        Task<IList<CalendarEvent>> GetEventsAsync(Guid channelId, uint? maxItems, DateTime? endDate, DateTime? startDate);
        /// <summary>
        /// Gets given amount of events from a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="maxItems">How many events it should get</param>
        /// <param name="endDate">At which date it should end</param>
        /// <param name="startDate">At which date it should start</param>
        /// <returns>List of calendar events</returns>
        IList<CalendarEvent> GetEvents(Guid channelId, uint? maxItems, DateTime? endDate, DateTime? startDate);
        /// <summary>
        /// Get availabilities in a schedule channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <returns>List of availabilities</returns>
        Task<IList<Availability>> GetSchedulesAsync(Guid channelId);
        /// <summary>
        /// Get availabilities in a schedule channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <returns>List of availabilities</returns>
        IList<Availability> GetSchedules(Guid channelId);
        /// <summary>
        /// Add a reaction to a specific message.
        /// </summary>
        /// <param name="channelId">ID of the channel where the message is in</param>
        /// <param name="messageId">ID of the message to add a reaction on</param>
        /// <param name="emoteId">ID of the emote to add</param>
        Task AddReactionAsync(Guid channelId, Guid messageId, uint emoteId);
        /// <summary>
        /// Add a reaction to a specific message.
        /// </summary>
        /// <param name="channelId">ID of the channel where the message is in</param>
        /// <param name="messageId">ID of the message to add a reaction on</param>
        /// <param name="emoteId">ID of the emote to add</param>
        void AddReaction(Guid channelId, Guid messageId, uint emoteId);
        /// <summary>
        /// Removes a reaction from a specific message.
        /// </summary>
        /// <param name="channelId">ID of the channel where the message is in</param>
        /// <param name="messageId">ID of the message to remove a reaction from</param>
        /// <param name="emoteId">ID of the emote to remove</param>
        Task RemoveReactionAsync(Guid channelId, Guid messageId, uint emoteId);
        /// <summary>
        /// Removes a reaction from a specific message.
        /// </summary>
        /// <param name="channelId">ID of the channel where the message is in</param>
        /// <param name="messageId">ID of the message to remove a reaction from</param>
        /// <param name="emoteId">ID of the emote to remove</param>
        void RemoveReaction(Guid channelId, Guid messageId, uint emoteId);
        /// <summary>
        /// Gets a list of announcements in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="maxItems">How many announcements it should get</param>
        /// <param name="beforeDate">Before which date it should get announcements</param>
        /// <returns>List of announcements</returns>
        Task<IList<Announcement>> GetAnnouncementsAsync(Guid channelId, uint? maxItems, DateTime? beforeDate);
        /// <summary>
        /// Gets a list of announcements in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="maxItems">How many announcements it should get</param>
        /// <param name="beforeDate">Before which date it should get announcements</param>
        /// <returns>List of announcements</returns>
        IList<Announcement> GetAnnouncements(Guid channelId, uint? maxItems, DateTime? beforeDate);
        /// <summary>
        /// Gets a list of announcements in a team.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <param name="maxItems">How many announcements it should get</param>
        /// <param name="beforeDate">Before which date it should get announcements</param>
        /// <returns>List of announcements</returns>
        Task<IList<Announcement>> GetAnnouncementsAsync(GId teamId, uint? maxItems, DateTime? beforeDate);
        /// <summary>
        /// Gets a list of announcements in a team.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <param name="maxItems">How many announcements it should get</param>
        /// <param name="beforeDate">Before which date it should get announcements</param>
        /// <returns>List of announcements</returns>
        IList<Announcement> GetAnnouncements(GId teamId, uint? maxItems, DateTime? beforeDate);
        /// <summary>
        /// Gets a list of pinned announcements in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <returns>List of announcements</returns>
        Task<IList<Announcement>> GetPinnedAnnouncementsAsync(Guid channelId);
        /// <summary>
        /// Gets a list of pinned announcements in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <returns>List of announcements</returns>
        IList<Announcement> GetPinnedAnnouncements(Guid channelId);
        /// <summary>
        /// Gets a list of pinned announcements in a team.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <returns>List of announcements</returns>
        Task<IList<Announcement>> GetPinnedAnnouncementsAsync(GId teamId);
        /// <summary>
        /// Gets a list of pinned announcements in a team.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <returns>List of announcements</returns>
        IList<Announcement> GetPinnedAnnouncements(GId teamId);
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
        /// Gets list items in a given channel.
        /// </summary>
        /// <param name="channelId">Channel ID</param>
        /// <returns>List of list items</returns>
        Task<IList<ListItem>> GetListItemsAsync(Guid channelId);
        /// <summary>
        /// Gets list items in a given channel.
        /// </summary>
        /// <param name="channelId">Channel ID</param>
        /// <returns>List of list items</returns>
        IList<ListItem> GetListItems(Guid channelId);
        /// <summary>
        /// Creates a new list item.
        /// </summary>
        /// <param name="channelId">ID of the channel to add a list item in</param>
        /// <param name="title">Title content of this list item</param>
        /// <param name="priority">Order of this list item</param>
        /// <param name="parentId">ID of the parent</param>
        /// <param name="note">Note of this list item</param>
        Task CreateListItemAsync(Guid channelId, MessageContent title, long priority, Guid? parentId, MessageContent note);
        /// <summary>
        /// Creates a new list item.
        /// </summary>
        /// <param name="channelId">ID of the channel to add a list item in</param>
        /// <param name="title">Title content of this list item</param>
        /// <param name="priority">Order of this list item</param>
        /// <param name="parentId">ID of the parent</param>
        /// <param name="note">Note of this list item</param>
        void CreateListItem(Guid channelId, MessageContent title, long priority, Guid? parentId, MessageContent note);
        /// <summary>
        /// Deletes a list item.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="itemId">ID of the item</param>
        Task DeleteListItemAsync(Guid channelId, Guid itemId);
        /// <summary>
        /// Deletes a list item.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="itemId">ID of the item</param>
        void DeleteListItem(Guid channelId, Guid itemId);
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
        /// Gets all comments in a given announcement.
        /// </summary>
        /// <param name="announcementId">ID of the announcement</param>
        /// <returns>List of content replies</returns>
        Task<IList<ContentReply>> GetAnnouncementRepliesAsync(GId announcementId);
        /// <summary>
        /// Gets all comments in a given announcement.
        /// </summary>
        /// <param name="announcementId">ID of the announcement</param>
        /// <returns>List of content replies</returns>
        IList<ContentReply> GetAnnouncementReplies(GId announcementId);
        /// <summary>
        /// Gets all comments in a given document.
        /// </summary>
        /// <param name="docId">ID of the document</param>
        /// <returns>List of content replies</returns>
        Task<IList<ContentReply>> GetDocRepliesAsync(uint docId);
        /// <summary>
        /// Gets all comments in a given document.
        /// </summary>
        /// <param name="docId">ID of the document</param>
        /// <returns>List of content replies</returns>
        IList<ContentReply> GetDocReplies(uint docId);
        /// <summary>
        /// Gets all comments in a given media post.
        /// </summary>
        /// <param name="mediaId">ID of the media post</param>
        /// <returns>List of content replies</returns>
        Task<IList<ContentReply>> GetMediaRepliesAsync(uint mediaId);
        /// <summary>
        /// Gets all comments in a given media post.
        /// </summary>
        /// <param name="mediaId">ID of the media post</param>
        /// <returns>List of content replies</returns>
        IList<ContentReply> GetMediaReplies(uint mediaId);
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
    }
}