using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Guilded.NET.Objects
{
    using Content;
    using Teams;
    using Chat;
    /// <summary>
    /// Represents any Guilded client.
    /// </summary>
    public partial interface IGuildedClient
    {
        /// <summary>
        /// Serializer used to (de)serialize JSON given by Guilded or made for Guilded.
        /// </summary>
        /// <value>Serializer</value>
        JsonSerializer GuildedSerializer
        {
            get; set;
        }

        //=======================//
        //   Chat
        //=======================//

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

        //=======================//
        //   Forums
        //=======================//

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
        /// Deletes a forum post in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel where the post is in</param>
        /// <param name="postId">ID of the post to delete</param>
        Task DeleteForumPostAsync(Guid channelId, uint postId);
        /// <summary>
        /// Deletes a forum post in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel where the post is in</param>
        /// <param name="postId">ID of the post to delete</param>
        void DeleteForumPost(Guid channelId, uint postId);
        /// <summary>
        /// Edits a forum post in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel post is in</param>
        /// <param name="postId">ID of the post</param>
        /// <param name="title">New title of this post</param>
        /// <param name="message">New content of the post</param>
        Task EditForumPostAsync(Guid channelId, uint postId, string title, MessageContent message);
        /// <summary>
        /// Edits a forum post in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel post is in</param>
        /// <param name="postId">ID of the post</param>
        /// <param name="title">New title of this post</param>
        /// <param name="message">New content of the post</param>
        void EditForumPost(Guid channelId, uint postId, string title, MessageContent message);
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
        /// Deletes a forum reply/comment.
        /// </summary>
        /// <param name="channelId">ID of the channel where the post is in</param>
        /// <param name="postId">A forum post where reply should be deleted</param>
        /// <param name="replyId">A reply of a forum post which should be deleted</param>
        Task DeleteForumReplyAsync(Guid channelId, uint postId, uint replyId);
        /// <summary>
        /// Deletes a forum reply/comment.
        /// </summary>
        /// <param name="channelId">ID of the channel where the post is in</param>
        /// <param name="postId">A forum post where reply should be deleted</param>
        /// <param name="replyId">A reply of a forum post which should be deleted</param>
        void DeleteForumReply(Guid channelId, uint postId, uint replyId);
        /// <summary>
        /// Edits a forum reply.
        /// </summary>
        /// <param name="channelId">ID of the channel where forum post is in</param>
        /// <param name="postId">ID of the post to edit reply in</param>
        /// <param name="replyId">Reply to edit contents of</param>
        /// <param name="content">New content which will replace the old content</param>
        Task EditForumReplyAsync(Guid channelId, uint postId, uint replyId, MessageContent content);
        /// <summary>
        /// Edits a forum reply.
        /// </summary>
        /// <param name="channelId">ID of the channel where forum post is in</param>
        /// <param name="postId">ID of the post to edit reply in</param>
        /// <param name="replyId">Reply to edit contents of</param>
        /// <param name="content">New content which will replace the old content</param>
        void EditForumReply(Guid channelId, uint postId, uint replyId, MessageContent content);

        //=======================//
        //   Documents
        //=======================//

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

        //=======================//
        //   Media
        //=======================//

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

        //=======================//
        //   Calendar
        //=======================//

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

        //=======================//
        //   Scheduling
        //=======================//

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
        /// Creates a schedule availability.
        /// </summary>
        /// <param name="channelId">ID of the channel where to create a schedule</param>
        /// <param name="startDate">Start date of this availability</param>
        /// <param name="endDate">End date of this availability</param>
        /// <returns>Created schedule availability</returns>
        Task<IList<Availability>> CreateScheduleAsync(Guid channelId, DateTime startDate, DateTime endDate);
        /// <summary>
        /// Creates a schedule availability.
        /// </summary>
        /// <param name="channelId">ID of the channel where to create a schedule</param>
        /// <param name="startDate">Start date of this availability</param>
        /// <param name="endDate">End date of this availability</param>
        /// <returns>Created schedule availability</returns>
        IList<Availability> CreateSchedule(Guid channelId, DateTime startDate, DateTime endDate);
        /// <summary>
        /// Edits a schedule availability.
        /// </summary>
        /// <param name="channelId">ID of the channel where an availability is</param>
        /// <param name="availabilityId">ID of schedule availability to edit</param>
        /// <param name="startDate">Start date of this availability</param>
        /// <param name="endDate">End date of this availability</param>
        /// <returns>Edited schedule availability</returns>
        Task<IList<Availability>> EditScheduleAsync(Guid channelId, uint availabilityId, DateTime startDate, DateTime endDate);
        /// <summary>
        /// Edits a schedule availability.
        /// </summary>
        /// <param name="channelId">ID of the channel where to create a schedule</param>
        /// <param name="availabilityId">ID of schedule availability to edit</param>
        /// <param name="startDate">Start date of this availability</param>
        /// <param name="endDate">End date of this availability</param>
        /// <returns>Edited schedule availability</returns>
        IList<Availability> EditSchedule(Guid channelId, uint availabilityId, DateTime startDate, DateTime endDate);
        /// <summary>
        /// Deletes a schedule availability.
        /// </summary>
        /// <param name="channelId">ID of the channel where an availability is</param>
        /// <param name="availabilityId">ID of schedule availability to edit</param>
        Task DeleteScheduleAsync(Guid channelId, uint availabilityId);
        /// <summary>
        /// Deletes a schedule availability.
        /// </summary>
        /// <param name="channelId">ID of the channel where an availability is</param>
        /// <param name="availabilityId">ID of schedule availability to edit</param>
        void DeleteSchedule(Guid channelId, uint availabilityId);

        //=======================//
        //   Announcements
        //=======================//

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
        /// Gets all comments in a given announcement.
        /// </summary>
        /// <param name="announcementId">ID of the announcement</param>
        /// <returns>List of content replies</returns>
        Task<IList<AnnouncementReply>> GetAnnouncementRepliesAsync(GId announcementId);
        /// <summary>
        /// Gets all comments in a given announcement.
        /// </summary>
        /// <param name="announcementId">ID of the announcement</param>
        /// <returns>List of content replies</returns>
        IList<AnnouncementReply> GetAnnouncementReplies(GId announcementId);
        /// <summary>
        /// Deletes an announcement reply.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <param name="contentId">ID of the content</param>
        /// <param name="replyId">ID of the reply to delete</param>
        Task DeleteAnnouncementReplyAsync(GId teamId, GId contentId, uint replyId);
        /// <summary>
        /// Deletes an announcement reply.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <param name="contentId">ID of the content</param>
        /// <param name="replyId">ID of the reply to delete</param>
        void DeleteAnnouncementReply(GId teamId, GId contentId, uint replyId);
        /// <summary>
        /// Edits announcement reply's message.
        /// </summary>
        /// <param name="contentId">ID of the content reply is in</param>
        /// <param name="replyId">ID of the reply to edit</param>
        /// <param name="message">New message content to replace with</param>
        Task EditAnnouncementReplyAsync(GId contentId, uint replyId, MessageContent message);
        /// <summary>
        /// Edits announcement reply's message.
        /// </summary>
        /// <param name="contentId">ID of the content reply is in</param>
        /// <param name="replyId">ID of the reply to edit</param>
        /// <param name="message">New message content to replace with</param>
        void EditAnnouncementReply(GId contentId, uint replyId, MessageContent message);
        /// <summary>
        /// Creates and posts a new announcement.
        /// </summary>
        /// <param name="teamId">ID of the team to create announcement in</param>
        /// <param name="channelId">ID of the channel to create announcement in</param>
        /// <param name="title">Title of the announcement</param>
        /// <param name="content">Content of the announcement</param>
        /// <param name="dontSendNotifications">If it should not send a notification to everyone</param>
        /// <param name="gameId">ID of the group's game</param>
        /// <returns>Created announcement</returns>
        Task<Announcement> PostAnnouncementAsync(GId teamId, Guid channelId, string title, MessageContent content, bool dontSendNotifications, uint? gameId);
        /// <summary>
        /// Creates and posts a new announcement.
        /// </summary>
        /// <param name="teamId">ID of the team to create announcement in</param>
        /// <param name="channelId">ID of the channel to create announcement in</param>
        /// <param name="title">Title of the announcement</param>
        /// <param name="content">Content of the announcement</param>
        /// <param name="dontSendNotifications">If it should not send a notification to everyone</param>
        /// <param name="gameId">ID of the group's game</param>
        /// <returns>Created announcement</returns>
        Announcement PostAnnouncement(GId teamId, Guid channelId, string title, MessageContent content, bool dontSendNotifications = false, uint? gameId = null);
        /// <summary>
        /// Pins or unpins an announcement.
        /// </summary>
        /// <param name="channelId">ID of the channel where announcement is in</param>
        /// <param name="announcementId">ID of the announcement to (un)pin</param>
        /// <param name="isPinned">True - pin announcement, false - unpin announcement</param>
        Task PinAnnouncementAsync(Guid channelId, GId announcementId, bool isPinned = true);
        /// <summary>
        /// Pins or unpins an announcement.
        /// </summary>
        /// <param name="channelId">ID of the channel where announcement is in</param>
        /// <param name="announcementId">ID of the announcement to (un)pin</param>
        /// <param name="isPinned">True - pin announcement, false - unpin announcement</param>
        void PinAnnouncement(Guid channelId, GId announcementId, bool isPinned = true);
        /// <summary>
        /// Updates/edits an announcement.
        /// </summary>
        /// <param name="teamId">ID of the team where announcement is</param>
        /// <param name="channelId">ID of the channel where announcement is</param>
        /// <param name="announcementId">ID of the announcement to edit</param>
        /// <param name="title">New title</param>
        /// <param name="content">New content</param>
        /// <returns>ID of edited/updated announcement</returns>
        Task<GId> UpdateAnnouncementAsync(GId teamId, Guid channelId, GId announcementId, string title, MessageContent content);
        /// <summary>
        /// Updates/edits an announcement.
        /// </summary>
        /// <param name="teamId">ID of the team where announcement is</param>
        /// <param name="channelId">ID of the channel where announcement is</param>
        /// <param name="announcementId">ID of the announcement to edit</param>
        /// <param name="title">New title</param>
        /// <param name="content">New content</param>
        /// <returns>ID of edited/updated announcement</returns>
        GId UpdateAnnouncement(GId teamId, Guid channelId, GId announcementId, string title, MessageContent content);
        /// <summary>
        /// Deletes an announcement.
        /// </summary>
        /// <param name="channelId">ID of the channel where announcement is</param>
        /// <param name="announcementId">ID of the announcement to delete</param>
        /// <returns>Deleted announcement</returns>
        Task<Announcement> DeleteAnnouncementAsync(Guid channelId, GId announcementId);
        /// <summary>
        /// Deletes an announcement.
        /// </summary>
        /// <param name="channelId">ID of the channel where announcement is</param>
        /// <param name="announcementId">ID of the announcement to delete</param>
        /// <returns>Deleted announcement</returns>
        Announcement DeleteAnnouncement(Guid channelId, GId announcementId);

        //=======================//
        //   List
        //=======================//

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
        /// Edits a list item.
        /// </summary>
        /// <param name="channelId">ID of the channel where list item is</param>
        /// <param name="itemId">List item to edit</param>
        /// <param name="content">New list item content/message/title(null if you only need to edit a note)</param>
        /// <param name="note">New list item note(null if you only need to edit content)</param>
        Task EditListItemAsync(Guid channelId, Guid itemId, MessageContent content, MessageContent note);
        /// <summary>
        /// Edits a list item.
        /// </summary>
        /// <param name="channelId">ID of the channel where list item is</param>
        /// <param name="itemId">List item to edit</param>
        /// <param name="content">New list item content/message/title(null if you only need to edit a note)</param>
        /// <param name="note">New list item note(null if you only need to edit content)</param>
        void EditListItem(Guid channelId, Guid itemId, MessageContent content, MessageContent note);
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

        //=======================//
        //   Multiple
        //=======================//

        /// <summary>
        /// Gets all comments in a given document or media.
        /// </summary>
        /// <param name="contentId">ID of content</param>
        /// <param name="type">Type of the channel</param>
        /// <returns>List of content replies</returns>
        Task<IList<ContentReply>> GetContentRepliesAsync(uint contentId, ChannelType type);
        /// <summary>
        /// Gets all comments in a given document or media.
        /// </summary>
        /// <param name="contentId">ID of content</param>
        /// <param name="type">Type of the channel</param>
        /// <returns>List of content replies</returns>
        IList<ContentReply> GetContentReplies(uint contentId, ChannelType type);
        /// <summary>
        /// Deletes a document or a media reply.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <param name="contentId">ID of the content</param>
        /// <param name="replyId">ID of the reply to delete</param>
        /// <param name="type">Channel's type</param>
        Task DeleteContentReplyAsync(GId teamId, uint contentId, uint replyId, ChannelType type);
        /// <summary>
        /// Deletes a document or a media reply.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <param name="contentId">ID of the content</param>
        /// <param name="replyId">ID of the reply to delete</param>
        /// <param name="type">Channel's type</param>
        void DeleteContentReply(GId teamId, uint contentId, uint replyId, ChannelType type);
        /// <summary>
        /// Edits content reply's message.
        /// </summary>
        /// <param name="contentId">ID of the content reply is in</param>
        /// <param name="replyId">ID of the reply to edit</param>
        /// <param name="type">Type of the channel this reply is in</param>
        /// <param name="message">New message content to replace with</param>
        Task EditContentReplyAsync(uint contentId, uint replyId, ChannelType type, MessageContent message);
        /// <summary>
        /// Edits content reply's message.
        /// </summary>
        /// <param name="contentId">ID of the content reply is in</param>
        /// <param name="replyId">ID of the reply to edit</param>
        /// <param name="type">Type of the channel this reply is in</param>
        /// <param name="message">New message content to replace with</param>
        void EditContentReply(uint contentId, uint replyId, ChannelType type, MessageContent message);
    }
}