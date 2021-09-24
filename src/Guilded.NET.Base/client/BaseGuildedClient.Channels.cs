using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Guilded.NET.Base
{
    using Content;
    using Permissions;
    public abstract partial class BaseGuildedClient
    {
        // #region Webhook
        // /// <summary>
        // /// Creates a webhook in a given channel.
        // /// </summary>
        // /// <param name="channelId">The identifier of the parent channel</param>
        // /// <param name="name">The name of the webhook</param>
        // /// <exception cref="GuildedException"/>
        // /// <permission cref="GeneralPermissions.ManageWebhooks">Required for managing webhooks</permission>
        // /// <returns>Created webhook</returns>
        // public abstract Task<Webhook> CreateWebhookAsync(Guid channelId, string name);
        // /// <summary>
        // /// Updates webhook's name or profile picture.
        // /// </summary>
        // /// <param name="channelId">The identifier of the parent channel</param>
        // /// <param name="webhookId">The identifier of the webhook to update</param>
        // /// <param name="name">A new name of the webhook</param>
        // /// <param name="avatar">Profile picture/icon of the webhook</param>
        // /// <exception cref="GuildedException"/>
        // /// <permission cref="GeneralPermissions.ManageWebhooks">Required for managing webhooks</permission>
        // /// <returns>Updated webhook</returns>
        // public abstract Task<Webhook> UpdateWebhookAsync(Guid channelId, Guid webhookId, string name, Uri avatar);
        // /// <summary>
        // /// Deletes a webhook.
        // /// </summary>
        // /// <param name="webhookId">The identifier of the webhook to delete</param>
        // /// <exception cref="GuildedException"/>
        // /// <permission cref="GeneralPermissions.ManageWebhooks">Required for managing webhooks</permission>
        // /// <returns>Deleted webhook</returns>
        // public abstract Task<Webhook> DeleteWebhookAsync(Guid webhookId);
        // /// <summary>
        // /// Posts a message using a webhook.
        // /// </summary>
        // /// <param name="webhookId">The identifier of the webhook</param>
        // /// <param name="token">Token of this webhook</param>
        // /// <param name="content">Message to send using the webhook</param>
        // /// <param name="embeds">An array of embeds to send</param>
        // /// <exception cref="GuildedException"/>
        // public abstract Task ExecuteWebhookAsync(Guid webhookId, string token, string content = null, params Embed[] embeds);
        // #endregion

        #region Chat channels
        /// <summary>
        /// Gets messages with a specific limit.
        /// </summary>
        /// <remarks>
        /// <para>Gets a list of messages in a specified channel of identifier <paramref name="channelId"/>.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="limit">How many messages it should get</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <returns>List of messages</returns>
        public abstract Task<IList<Message>> GetMessagesAsync(Guid channelId, uint limit = 50);
        /// <summary>
        /// Gets a message in a specific channel.
        /// </summary>
        /// <remarks>
        /// <para>Gets a specified message with an identifier <paramref name="messageId"/>.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="messageId">The identifier of message it should get</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <returns>Message</returns>
        public abstract Task<Message> GetMessageAsync(Guid channelId, Guid messageId);
        /// <summary>
        /// Creates a message in a chat.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new message with <paramref name="content"/> formatted in Markdown.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <exception cref="ArgumentNullException">When the <paramref name="content"/> only consists of whitespace or is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="content"/> is above the message limit of 4000 characters</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for sending a message in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for sending a message in a thread</permission>
        /// <returns>Message created</returns>
        public abstract Task<Message> CreateMessageAsync(Guid channelId, string content);
        /// <inheritdoc cref="CreateMessageAsync(Guid, string)"/>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <param name="replyMessageIds">The array of all messages it is replying to(5 max)</param>
        public abstract Task<Message> CreateMessageAsync(Guid channelId, string content, params Guid[] replyMessageIds);
        /// <inheritdoc cref="CreateMessageAsync(Guid, string)"/>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <param name="isPrivate">Whether the reply is private</param>
        /// <param name="replyMessageIds">The array of all messages it is replying to(5 max)</param>
        public abstract Task<Message> CreateMessageAsync(Guid channelId, string content, bool isPrivate, params Guid[] replyMessageIds);
        /// <summary>
        /// Updates the contents of the message.
        /// </summary>
        /// <remarks>
        /// <para>Edits the message <paramref name="messageId"/>, if the specified message is from the client. This does not work if the client is not the creator of the message.</para>
        /// <para>The <paramref name="content"/> will be formatted in Markdown.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="messageId">The identifier of the message to edit</param>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <exception cref="ArgumentNullException">When the <paramref name="content"/> only consists of whitespace or is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="content"/> is above the message limit of 4000 characters</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for editing your own messages posted in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for editing your own messages posted in a thread</permission>
        /// <returns>Message updated</returns>
        public abstract Task<Message> UpdateMessageAsync(Guid channelId, Guid messageId, string content);
        /// <summary>
        /// Deletes a specified message.
        /// </summary>
        /// <remarks>
        /// <para>Removes the message of identifier <paramref name="messageId"/>, whether it be from the client or another user.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="messageId">The identifier of the message to delete</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.ManageMessages">Required for deleting messages made by others</permission>
        public abstract Task DeleteMessageAsync(Guid channelId, Guid messageId);
        /// <summary>
        /// Adds a reaction to a message.
        /// </summary>
        /// <remarks>
        /// <para>Adds a reaction of identifier <paramref name="emoteId"/> to <paramref name="messageId"/>.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="messageId">The identifier of the message to add a reaction on</param>
        /// <param name="emoteId">The identifier of the emote to add</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="ChatPermissions.ReadMessages">Required for adding a reaction to a message you see</permission>
        /// <returns>Reaction added</returns>
        public abstract Task<Reaction> AddReactionAsync(Guid channelId, Guid messageId, uint emoteId);
        /// <summary>
        /// Removes a reaction from a message.
        /// </summary>
        /// <remarks>
        /// <para>Remove a reaction of identifier <paramref name="emoteId"/> from <paramref name="messageId"/>.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="messageId">The identifier of the message to remove a reaction from</param>
        /// <param name="emoteId">The identifier of the emote to remove</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="ChatPermissions.ReadMessages">Required for removing a reaction from a message you see</permission>
        public abstract Task RemoveReactionAsync(Guid channelId, Guid messageId, uint emoteId);
        #endregion

        /*#region Threads
        /// <summary>
        /// Creates a thread as a response to a message.
        /// </summary>
        /// <param name="channelId">The identifier of the channel to create thread in</param>
        /// <param name="name">Name of the thread</param>
        /// <param name="message">Message to respond to</param>
        /// <param name="response">Content of the response</param>
        /// <param name="contentType">Type of the channel where thread should be created in</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Thread created</returns>
        Task<ThreadChannel> CreateThreadAsync(Guid channelId, string name, Message message, MessageContent response, ChannelType contentType = ChannelType.Chat);
        /// <summary>
        /// Leaves a thread and no longer receives notifications from it.
        /// </summary>
        /// <param name="threadId">The identifier of the thread to leave</param>
        /// <exception cref="GuildedException"/>
        Task LeaveThreadAsync(Guid threadId);
        #endregion*/

        #region Forum channels
        /// <summary>
        /// Creates a forum thread.
        /// </summary>
        /// <remarks>
        /// <para>Creates a forum thread/post in forums.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="title">The title of the forum post</param>
        /// <param name="content">The content of the forum post</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="ForumPermissions.ReadForums">Required to create a forum thread in forums you can read</permission>
        /// <permission cref="ForumPermissions.CreateTopics">Required to create forum threads</permission>
        /// <returns>Forum thread created</returns>
        public abstract Task<ForumThread> CreateForumThreadAsync(Guid channelId, string title, string content);
        #endregion        

        /*
        /// <summary>
        /// Gets forum posts from a specific forum channel.
        /// </summary>
        /// <param name="channelId">The identifier of the channel</param>
        /// <param name="beforeDate">Before what date it should get posts</param>
        /// <param name="maxItems">How many forum posts it should get</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Forum post list</returns>
        Task<IList<ForumPost>> GetForumPostsAsync(Guid channelId, uint maxItems = 1000, DateTime? beforeDate = null);
        /// <summary>
        /// Edits a forum post in a specific channel.
        /// </summary>
        /// <param name="channelId">The identifier of the channel post is in</param>
        /// <param name="postId">The identifier of the post</param>
        /// <param name="title">New title of this post</param>
        /// <param name="message">New content of the post</param>
        /// <exception cref="GuildedException"/>
        Task EditForumPostAsync(Guid channelId, uint postId, string title, MessageContent message);
        /// <summary>
        /// Deletes a forum post in a specific channel.
        /// </summary>
        /// <param name="channelId">The identifier of the channel where the post is in</param>
        /// <param name="postId">The identifier of the post to delete</param>
        /// <exception cref="GuildedException"/>
        Task DeleteForumPostAsync(Guid channelId, uint postId);
        /// <summary>
        /// Gets replies from a forum post.
        /// </summary>
        /// <param name="channelId">The identifier of the channel</param>
        /// <param name="postId">The identifier of the post it should get replies of</param>
        /// <param name="maxItems">How many forum posts it should get</param>
        /// <param name="afterDate">After what date it should get posts</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Forum reply list</returns>
        Task<IList<ForumReply>> GetForumRepliesAsync(Guid channelId, uint postId, uint maxItems = 10, DateTime? afterDate = null);
        /// <summary>
        /// Replies to a forum post.
        /// </summary>
        /// <param name="channelId">The identifier of the forum channel</param>
        /// <param name="postId">The identifier of the post it should reply to</param>
        /// <param name="message">Content of the forum post</param>
        /// <exception cref="GuildedException"/>
        Task CreateForumReplyAsync(Guid channelId, uint postId, MessageContent message);
        /// <summary>
        /// Deletes a forum reply/comment.
        /// </summary>
        /// <param name="channelId">The identifier of the channel where the post is in</param>
        /// <param name="postId">A forum post where reply should be deleted</param>
        /// <param name="replyId">A reply of a forum post that should be deleted</param>
        /// <exception cref="GuildedException"/>
        Task DeleteForumReplyAsync(Guid channelId, uint postId, uint replyId);
        /// <summary>
        /// Edits a forum reply.
        /// </summary>
        /// <param name="channelId">The identifier of the channel where forum post is in</param>
        /// <param name="postId">The identifier of the post to edit reply in</param>
        /// <param name="replyId">Reply to edit contents of</param>
        /// <param name="content">New content which will replace the old content</param>
        /// <exception cref="GuildedException"/>
        Task EditForumReplyAsync(Guid channelId, uint postId, uint replyId, MessageContent content);
        /// <summary>
        /// Adds a reaction to a forum reply or a forum post.
        /// </summary>
        /// <param name="teamId">The identifier of the team where the post is</param>
        /// <param name="postId">The identifier of forum post or reply to react on</param>
        /// <param name="emoteId">The identifier of the emote to react with</param>
        /// <param name="isContentReply">Is it a reaction on a reply</param>
        /// <exception cref="GuildedException"/>
        Task AddForumReactionAsync(GId teamId, uint postId, uint emoteId, bool isContentReply = false);
        /// <summary>
        /// Removes a reaction from a forum reply or a forum post.
        /// </summary>
        /// <param name="teamId">The identifier of the team where the post is</param>
        /// <param name="postId">The identifier of content or reply to react on</param>
        /// <param name="emoteId">The identifier of the emote to react with</param>
        /// <param name="isContentReply">Is it a reaction on a reply</param>
        /// <exception cref="GuildedException"/>
        Task RemoveForumReactionAsync(GId teamId, uint postId, uint emoteId, bool isContentReply = false)
        #endregion

        #region Document channels
        /// <summary>
        /// Gets all documents within a specific channel with given max count and before given date.
        /// </summary>
        /// <param name="channelId">The identifier of channel to fetch documents from</param>
        /// <param name="maxItems">Amount of documents to get</param>
        /// <param name="beforeDate">Date before which it should get documents</param>
        /// <exception cref="GuildedException"/>
        /// <returns>List of documents</returns>
        Task<IList<GuildedDocument>> GetDocumentsAsync(Guid channelId, uint maxItems = 50, DateTime? beforeDate = null);
        /// <summary>
        /// Gets a specific document in a specific channel.
        /// </summary>
        /// <param name="channelId">The identifier of channel to fetch documents from</param>
        /// <param name="docId">The identifier of the document</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Document</returns>
        Task<GuildedDocument> GetDocumentAsync(Guid channelId, uint docId);
        /// <summary>
        /// Updates already existing document.
        /// </summary>
        /// <param name="doc">Document to update</param>
        /// <param name="title">New title of the document</param>
        /// <param name="content">New message of the document</param>
        /// <param name="isDraft">Whether it is a draft or not</param>
        /// <returns>Updated document</returns>
        Task<GuildedDocument> UpdateDocumentAsync(GuildedDocument doc, string title = null, MessageContent content = null, bool? isDraft = null);
        /// <summary>
        /// Deletes a document from doc channel.
        /// </summary>
        /// <param name="channelId">The identifier of the channel where the document is in</param>
        /// <param name="docId">The identifier of the document to delete</param>
        Task DeleteDocumentAsync(Guid channelId, uint docId);
        #endregion


        #region Media channels
        /// <summary>
        /// Gets all media within a specific channel with given max count and before given date.
        /// </summary>
        /// <param name="channelId">The identifier of channel to fetch media from</param>
        /// <param name="pageSize">Limit of media to get</param>
        /// <exception cref="GuildedException"/>
        /// <returns>List of media posts</returns>
        Task<IList<GuildedMedia>> GetMediaAsync(Guid channelId, uint pageSize = 40);
        /// <summary>
        /// Gets all comments in a given media post.
        /// </summary>
        /// <param name="mediaId">The identifier of the media post</param>
        /// <exception cref="GuildedException"/>
        /// <returns>List of content replies</returns>
        Task<IList<ContentReply>> GetMediaRepliesAsync(uint mediaId);
        /// <summary>
        /// Posts new media post.
        /// </summary>
        /// <param name="teamId">The identifier of the team where the channel is</param>
        /// <param name="channelId">The identifier of the channel where to post it</param>
        /// <param name="src">Source URL</param>
        /// <param name="title">Title of the media</param>
        /// <param name="description">Description of the media</param>
        /// <param name="type">Type of the media: image or video</param>
        /// <param name="gameId">The identifier of the game of the group</param>
        /// <param name="tags">Tags associated with the media</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Media posted</returns>
        Task<GuildedMedia> PostMediaAsync(GId teamId, Guid channelId, Uri src, string title, string description = "", MediaType type = MediaType.Image, uint? gameId = null, params string[] tags);
        /// <summary>
        /// Deletes media post.
        /// </summary>
        /// <param name="channelId">The identifier of the channel where the media is</param>
        /// <param name="mediaId">The identifier of the media to delete</param>
        /// <exception cref="GuildedException"/>
        Task DeleteMediaAsync(Guid channelId, uint mediaId);
        #endregion


        #region Calendar channels
        /// <summary>
        /// Gets given amount of events from a specific channel.
        /// </summary>
        /// <param name="channelId">The identifier of the channel</param>
        /// <param name="startDate">At which date it should start</param>
        /// <param name="endDate">At which date it should end</param>
        /// <param name="maxItems">How many events it should get</param>
        /// <exception cref="GuildedException"/>
        /// <returns>List of calendar events</returns>
        Task<IList<CalendarEvent>> GetEventsAsync(Guid channelId, DateTime startDate, DateTime endDate, uint maxItems = 250);
        #endregion

        
        #region Scheduling channels
        /// <summary>
        /// Get availabilities in a schedule channel.
        /// </summary>
        /// <param name="channelId">The identifier of the channel</param>
        /// <exception cref="GuildedException"/>
        /// <returns>List of availabilities</returns>
        Task<IList<Availability>> GetSchedulesAsync(Guid channelId);
        /// <summary>
        /// Creates a schedule availability.
        /// </summary>
        /// <param name="channelId">The identifier of the channel where to create a schedule</param>
        /// <param name="startDate">Start date of this availability</param>
        /// <param name="endDate">End date of this availability</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Created schedule availability</returns>
        Task<IList<Availability>> CreateScheduleAsync(Guid channelId, DateTime startDate, DateTime endDate);
        /// <summary>
        /// Edits a schedule availability.
        /// </summary>
        /// <param name="channelId">The identifier of the channel where an availability is</param>
        /// <param name="availabilityId">The identifier of schedule availability to edit</param>
        /// <param name="startDate">Start date of this availability</param>
        /// <param name="endDate">End date of this availability</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Edited schedule availability</returns>
        Task<IList<Availability>> EditScheduleAsync(Guid channelId, uint availabilityId, DateTime startDate, DateTime endDate);
        /// <summary>
        /// Deletes a schedule availability.
        /// </summary>
        /// <param name="channelId">The identifier of the channel where an availability is</param>
        /// <param name="availabilityId">The identifier of schedule availability to edit</param>
        /// <exception cref="GuildedException"/>
        Task DeleteScheduleAsync(Guid channelId, uint availabilityId);
        #endregion

        #region Announcements channels
        /// <summary>
        /// Gets a list of announcements in a specific channel.
        /// </summary>
        /// <param name="channelId">The identifier of the channel</param>
        /// <param name="maxItems">How many announcements it should get</param>
        /// <param name="beforeDate">Before which date it should get announcements</param>
        /// <exception cref="GuildedException"/>
        /// <returns>List of announcements</returns>
        Task<IList<Announcement>> GetAnnouncementsAsync(Guid channelId, uint maxItems = 10, DateTime? beforeDate = null);
        /// <summary>
        /// Gets a list of announcements in a team.
        /// </summary>
        /// <param name="teamId">The identifier of the team</param>
        /// <param name="maxItems">How many announcements it should get</param>
        /// <param name="beforeDate">Before which date it should get announcements</param>
        /// <exception cref="GuildedException"/>
        /// <returns>List of announcements</returns>
        Task<IList<Announcement>> GetAnnouncementsAsync(GId teamId, uint maxItems, DateTime? beforeDate);
        /// <summary>
        /// Gets a list of pinned announcements in a specific channel.
        /// </summary>
        /// <param name="channelId">The identifier of the channel</param>
        /// <returns>List of announcements</returns>
        Task<IList<Announcement>> GetPinnedAnnouncementsAsync(Guid channelId);
        /// <summary>
        /// Gets a list of pinned announcements in a team.
        /// </summary>
        /// <param name="teamId">The identifier of the team</param>
        /// <exception cref="GuildedException"/>
        /// <returns>List of announcements</returns>
        Task<IList<Announcement>> GetPinnedAnnouncementsAsync(GId teamId);
        /// <summary>
        /// Creates and posts a new announcement.
        /// </summary>
        /// <param name="teamId">The identifier of the team to create announcement in</param>
        /// <param name="channelId">The identifier of the channel to create announcement in</param>
        /// <param name="title">Title of the announcement</param>
        /// <param name="content">Content of the announcement</param>
        /// <param name="dontSendNotifications">If it should not send a notification to everyone</param>
        /// <param name="gameId">The identifier of the group's game</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Created announcement</returns>
        Task<Announcement> PostAnnouncementAsync(GId teamId, Guid channelId, string title, MessageContent content, bool dontSendNotifications = true, uint? gameId = null);
        /// <summary>
        /// Pins or unpins an announcement.
        /// </summary>
        /// <param name="channelId">The identifier of the channel where announcement is in</param>
        /// <param name="announcementId">The identifier of the announcement to (un)pin</param>
        /// <param name="isPinned">True - pin announcement, false - unpin announcement</param>
        /// <exception cref="GuildedException"/>
        Task PinAnnouncementAsync(Guid channelId, GId announcementId, bool isPinned = true);
        /// <summary>
        /// Updates/edits an announcement.
        /// </summary>
        /// <param name="teamId">The identifier of the team where announcement is</param>
        /// <param name="channelId">The identifier of the channel where announcement is</param>
        /// <param name="announcementId">The identifier of the announcement to edit</param>
        /// <param name="title">New title</param>
        /// <param name="content">New content</param>
        /// <exception cref="GuildedException"/>
        /// <returns>The identifier of edited/updated announcement</returns>
        Task<GId> UpdateAnnouncementAsync(GId teamId, Guid channelId, GId announcementId, string title, MessageContent content);
        /// <summary>
        /// Deletes an announcement.
        /// </summary>
        /// <param name="channelId">The identifier of the channel where announcement is</param>
        /// <param name="announcementId">The identifier of the announcement to delete</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Deleted announcement</returns>
        Task<Announcement> DeleteAnnouncementAsync(Guid channelId, GId announcementId);
        #endregion*/

        #region List channels
        /// <summary>
        /// Creates a list item.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new list item in list/task channel.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="message">The title content of this list item</param>
        /// <param name="note">The note of this list item</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="ListPermissions.ViewListItems">Required to create a list item in list channel you can view</permission>
        /// <permission cref="ListPermissions.CreateListItem">Required to create list items</permission>
        /// <returns>List item created</returns>
        public abstract Task<ListItem> CreateListItemAsync(Guid channelId, string message, string note = null);
        #endregion

        #region Content
        /// <summary>
        /// Adds a reaction to the content.
        /// </summary>
        /// <remarks>
        /// <para>Adds a reaction of identifier <paramref name="emoteId"/> to content of identifier <paramref name="contentId"/>.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="contentId">The identifier of the content to add a reaction on</param>
        /// <param name="emoteId">The identifier of the emote to add</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="DocPermissions.ViewDocs">Required for adding a reaction to a document you see</permission>
        /// <permission cref="MediaPermissions.SeeMedia">Required for adding a reaction to a media post you see</permission>
        /// <permission cref="ForumPermissions.ReadForums">Required for adding a reaction to a forum thread you see</permission>
        /// <permission cref="CalendarPermissions.ViewEvents">Required for adding a reaction to a calendar event you see</permission>
        /// <returns>Reaction added</returns>
        public abstract Task<Reaction> AddReactionAsync(Guid channelId, uint contentId, uint emoteId);
        /// <summary>
        /// Removes a reaction from the content.
        /// </summary>
        /// <remarks>
        /// <para>Remove a reaction of identifier <paramref name="emoteId"/> from content of identifier <paramref name="contentId"/>.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="contentId">The identifier of the content to remove a reaction from</param>
        /// <param name="emoteId">The identifier of the emote to remove</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="DocPermissions.ViewDocs">Required for removing a reaction from a document you see</permission>
        /// <permission cref="MediaPermissions.SeeMedia">Required for removing a reaction from a media post you see</permission>
        /// <permission cref="ForumPermissions.ReadForums">Required for removing a reaction from a forum thread you see</permission>
        /// <permission cref="CalendarPermissions.ViewEvents">Required for removing a reaction from a calendar event you see</permission>
        public abstract Task RemoveReactionAsync(Guid channelId, uint contentId, uint emoteId);
        #endregion
    }
}