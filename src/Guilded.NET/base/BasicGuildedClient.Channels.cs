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
        static readonly Dictionary<ChannelType, string> ContentTypes = new Dictionary<ChannelType, string> {
            {ChannelType.Document, "doc"},
            {ChannelType.Media, "team_media"},
            {ChannelType.Event, "event"},
            {ChannelType.Forum, "forum"},
            {ChannelType.Announcement, "announcement"}
        };

        //=======================//
        //   Chat
        //=======================//

        /// <summary>
        /// Sends a message to the specific channel.
        /// </summary>
        /// <param name="channel">ID of the channel</param>
        /// <param name="message">Message</param>
        /// <returns>Async task</returns>
        public async Task<object> SendMessageAsync(Guid channel, NewMessage message) =>
            await ExecuteRequest(new Endpoint($"channels/{channel}/messages", Method.POST), new JsonBody(JsonConvert.SerializeObject(message, Converters)));
        /// <summary>
        /// Sends a message into the chat. Sync version of <see cref="SendMessageAsync"/>.
        /// </summary>
        /// <param name="channel">ID of the channel</param>
        /// <param name="message">Message to post</param>
        public void SendMessage(Guid channel, NewMessage message) =>
            SendMessageAsync(channel, message).GetAwaiter().GetResult();
        /// <summary>
        /// Edits message of the bot posted in the chat.
        /// </summary>
        /// <param name="channel">ID of the channel</param>
        /// <param name="messageId">ID of the message to edit</param>
        /// <param name="content">New content of the message</param>
        /// <returns>Response</returns>
        public async Task<object> EditMessageAsync(Guid channel, Guid messageId, MessageContent content) =>
            await ExecuteRequest(new Endpoint($"channels/{channel}/messages/{messageId}", Method.PUT), new JsonBody(new { content }, Converters));
        /// <summary>
        /// Edits message of the bot posted in the chat. Sync version of <see cref="EditMessageAsync"/>.
        /// </summary>
        /// <param name="channel">ID of the channel</param>
        /// <param name="messageId">ID of the message to edit</param>
        /// <param name="content">New content of the message</param>
        public void EditMessage(Guid channel, Guid messageId, MessageContent content) =>
            EditMessageAsync(channel, messageId, content).GetAwaiter().GetResult();
        /// <summary>
        /// Deletes a message posted in the chat.
        /// </summary>
        /// <param name="channel">ID of the channel</param>
        /// <param name="messageId">ID of the message to delete</param>
        /// <returns>Response</returns>
        public async Task<object> DeleteMessageAsync(Guid channel, Guid messageId) =>
            await ExecuteRequest(new Endpoint($"channels/{channel}/messages/{messageId}", Method.DELETE));
        /// <summary>
        /// Deletes a message posted in the chat. Sync version of <see cref="DeleteMessageAsync"/>.
        /// </summary>
        /// <param name="channel">ID of the channel</param>
        /// <param name="messageId">ID of the message to delete</param>
        public void DeleteMessage(Guid channel, Guid messageId) =>
            DeleteMessageAsync(channel, messageId).GetAwaiter().GetResult();
        /// <summary>
        /// Gets a message in a specific channel.
        /// </summary>
        /// <param name="subdomain">Subdomain of a team where that message and channel of that message is</param>
        /// <param name="groupId">ID of the group where that channel is in</param>
        /// <param name="channelId">ID of the channel where that message is</param>
        /// <param name="messageId">ID of message it should get</param>
        /// <returns>Message</returns>
        public async Task<Message> GetMessageAsync(string subdomain, GId groupId, Guid channelId, Guid messageId) =>
            (await FromObject(new Endpoint($"content/route/metadata?route=/{subdomain}/groups/{groupId}/channels/{channelId}/chat?messageId={messageId}", Method.GET)))["metadata"]["message"].ToObject<Message>(GuildedSerializer);
        /// <summary>
        /// Gets a message in a specific channel.
        /// </summary>
        /// <param name="subdomain">Subdomain of a team where that message and channel of that message is</param>
        /// <param name="groupId">ID of the group where that channel is in</param>
        /// <param name="channelId">ID of the channel where that message is</param>
        /// <param name="messageId">ID of message it should get</param>
        /// <returns>Message</returns>
        public Message GetMessage(string subdomain, GId groupId, Guid channelId, Guid messageId) =>
            GetMessageAsync(subdomain, groupId, channelId, messageId).GetAwaiter().GetResult();

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
        public async Task<IList<ForumPost>> GetForumPostsAsync(Guid channelId, uint? maxItems = 1000, DateTime? beforeDate = null) =>
            await FromObject<IList<ForumPost>>(new Endpoint($"channels/{channelId}/forums?maxItems={maxItems}&beforeDate={beforeDate ?? DateTime.Now}", Method.GET), "threads");
        /// <summary>
        /// Gets forum posts from a specific forum channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="beforeDate">Before what date it should get posts</param>
        /// <param name="maxItems">How many forum posts it should get</param>
        /// <returns>Forum post list</returns>
        public IList<ForumPost> GetForumPosts(Guid channelId, uint? maxItems = 1000, DateTime? beforeDate = null) =>
            GetForumPostsAsync(channelId, maxItems, beforeDate).GetAwaiter().GetResult();
        /// <summary>
        /// Gets forum posts from a specific forum channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="postId">ID of the post it should get replies from</param>
        /// <param name="maxItems">How many forum posts it should get</param>
        /// <param name="afterDate">After what date it should get posts</param>
        /// <returns>Forum reply list</returns>
        public async Task<IList<ForumReply>> GetForumRepliesAsync(Guid channelId, uint postId, uint? maxItems = 10, DateTime? afterDate = null) =>
            await FromObject<IList<ForumReply>>(new Endpoint($"channels/{channelId}/forums/{postId}/replies?maxItems={maxItems}&afterDate={afterDate ?? DateTime.Now}", Method.GET), "threadReplies");
        /// <summary>
        /// Gets forum replies from a forum post.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="postId">ID of the post it should get replies from</param>
        /// <param name="maxItems">How many forum posts it should get</param>
        /// <param name="afterDate">After what date it should get posts</param>
        /// <returns>Forum reply list</returns>
        public IList<ForumReply> GetForumReplies(Guid channelId, uint postId, uint? maxItems = 10, DateTime? afterDate = null) =>
            GetForumRepliesAsync(channelId, postId, maxItems, afterDate).GetAwaiter().GetResult();
        /// <summary>
        /// Create a forum post in a forum channel.
        /// </summary>
        /// <param name="channelId">ID of the forum channel</param>
        /// <param name="title">A title of the forum post</param>
        /// <param name="message">Content of the forum post</param>
        /// <returns>Async task</returns>
        public async Task CreateForumPostAsync(Guid channelId, string title, MessageContent message) =>
            await ExecuteRequest(new Endpoint($"channels/{channelId}/forums", Method.POST), new JsonBody(new {
                threadId = RandomId.Next(1000000000, int.MaxValue),
                title,
                message
            }, Converters));
        /// <summary>
        /// Create a forum post in a forum channel.
        /// </summary>
        /// <param name="channelId">ID of the forum channel</param>
        /// <param name="title">A title of the forum post</param>
        /// <param name="message">Content of the forum post</param>
        public void CreateForumPost(Guid channelId, string title, MessageContent message) =>
            CreateForumPostAsync(channelId, title, message).GetAwaiter().GetResult();
        /// <summary>
        /// Deletes a forum post in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel where the post is in</param>
        /// <param name="postId">ID of the post to delete</param>
        public async Task DeleteForumPostAsync(Guid channelId, uint postId) =>
            await ExecuteRequest(new Endpoint($"channels/{channelId}/forums/{postId}", Method.DELETE));
        /// <summary>
        /// Deletes a forum post in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel where the post is in</param>
        /// <param name="postId">ID of the post to delete</param>
        public void DeleteForumPost(Guid channelId, uint postId) =>
            DeleteForumPostAsync(channelId, postId).GetAwaiter().GetResult();
        /// <summary>
        /// Edits a forum post in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel post is in</param>
        /// <param name="postId">ID of the post</param>
        /// <param name="title">New title of this post</param>
        /// <param name="message">New content of the post</param>
        public async Task EditForumPostAsync(Guid channelId, uint postId, string title, MessageContent message) =>
            await ExecuteRequest(new Endpoint($"channels/{channelId}/forums/{postId}", Method.PUT), new JsonBody(new {
                threadId = postId,
                title,
                message
            }, Converters));
        /// <summary>
        /// Edits a forum post in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel post is in</param>
        /// <param name="postId">ID of the post</param>
        /// <param name="title">New title of this post</param>
        /// <param name="message">New content of the post</param>
        public void EditForumPost(Guid channelId, uint postId, string title, MessageContent message) =>
            EditForumPostAsync(channelId, postId, title, message).GetAwaiter().GetResult();
        /// <summary>
        /// Replies to a forum post.
        /// </summary>
        /// <param name="channelId">ID of the forum channel</param>
        /// <param name="postId">ID of the post it should reply to</param>
        /// <param name="message">Content of the forum post</param>
        /// <returns>Async task</returns>
        public async Task CreateForumReplyAsync(Guid channelId, uint postId, MessageContent message) =>
            await ExecuteRequest(new Endpoint($"channels/{channelId}/forums/{postId}/replies", Method.POST), new JsonBody(new {
                id = RandomId.Next(1000000000, int.MaxValue),
                message
            }, Converters));
        /// <summary>
        /// Replies to a forum post.
        /// </summary>
        /// <param name="channelId">ID of the forum channel</param>
        /// <param name="postId">ID of the post it should reply to</param>
        /// <param name="message">Content of the forum post</param>
        public void CreateForumReply(Guid channelId, uint postId, MessageContent message) =>
            CreateForumReplyAsync(channelId, postId, message).GetAwaiter().GetResult();
        /// <summary>
        /// Deletes a forum reply/comment.
        /// </summary>
        /// <param name="channelId">ID of the channel where the post is in</param>
        /// <param name="postId">A forum post where reply should be deleted</param>
        /// <param name="replyId">A reply of a forum post which should be deleted</param>
        public async Task DeleteForumReplyAsync(Guid channelId, uint postId, ulong replyId) =>
            await ExecuteRequest(new Endpoint($"channels/{channelId}/forums/{postId}/replies/{replyId}", Method.DELETE));
        /// <summary>
        /// Deletes a forum reply/comment.
        /// </summary>
        /// <param name="channelId">ID of the channel where the post is in</param>
        /// <param name="postId">A forum post where reply should be deleted</param>
        /// <param name="replyId">A reply of a forum post which should be deleted</param>
        public void DeleteForumReply(Guid channelId, uint postId, ulong replyId) =>
            DeleteForumReplyAsync(channelId, postId, replyId).GetAwaiter().GetResult();
        /// <summary>
        /// Edits a forum reply.
        /// </summary>
        /// <param name="channelId">ID of the channel where forum post is in</param>
        /// <param name="postId">ID of the post to edit reply in</param>
        /// <param name="replyId">Reply to edit contents of</param>
        /// <param name="content">New content which will replace the old content</param>
        public async Task EditForumReplyAsync(Guid channelId, uint postId, ulong replyId, MessageContent content) =>
            await ExecuteRequest(new Endpoint($"channels/{channelId}/forums/{postId}/replies/{replyId}", Method.PUT), new JsonBody($"{{\"message\": {content.Serialize(GuildedSerializer)}}}"));
        /// <summary>
        /// Edits a forum reply.
        /// </summary>
        /// <param name="channelId">ID of the channel where forum post is in</param>
        /// <param name="postId">ID of the post to edit reply in</param>
        /// <param name="replyId">Reply to edit contents of</param>
        /// <param name="content">New content which will replace the old content</param>
        public void EditForumReply(Guid channelId, uint postId, ulong replyId, MessageContent content) =>
            EditForumReplyAsync(channelId, postId, replyId, content).GetAwaiter().GetResult();

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
        public async Task<IList<GuildedDocument>> GetDocumentsAsync(Guid channelId, uint? maxItems = 50, DateTime? beforeDate = null) =>
            await FromArray<GuildedDocument>(new Endpoint($"channels/{channelId}/docs?maxItems={maxItems}&beforeDate={beforeDate ?? DateTime.Now}", Method.GET));
        /// <summary>
        /// Gets all documents within a specific channel with given max count and before given date.
        /// </summary>
        /// <param name="channelId">ID of channel to fetch documents from</param>
        /// <param name="maxItems">Amount of documents to get</param>
        /// <param name="beforeDate">Date before which it should get documents</param>
        /// <returns>List of documents</returns>
        public IList<GuildedDocument> GetDocuments(Guid channelId, uint? maxItems = 50, DateTime? beforeDate = null) =>
            GetDocumentsAsync(channelId, maxItems, beforeDate).GetAwaiter().GetResult();
        /// <summary>
        /// Gets a specific document in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of channel to fetch documents from</param>
        /// <param name="docId">ID of the document</param>
        /// <returns>Document</returns>
        public async Task<GuildedDocument> GetDocumentAsync(Guid channelId, uint docId) =>
            await FromObject<GuildedDocument>(new Endpoint($"channels/{channelId}/docs/{docId}", Method.GET));
        /// <summary>
        /// Gets a specific document in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of channel to fetch documents from</param>
        /// <param name="docId">ID of the document</param>
        /// <returns>Document</returns>
        public GuildedDocument GetDocument(Guid channelId, uint docId) =>
            GetDocumentAsync(channelId, docId).GetAwaiter().GetResult();
        
        //=======================//
        //   Media
        //=======================//

        /// <summary>
        /// Gets all medias within a specific channel with given max count and before given date.
        /// </summary>
        /// <param name="channelId">ID of channel to fetch media from</param>
        /// <returns>List of media posts</returns>
        public async Task<IList<GuildedMedia>> GetMediaAsync(Guid channelId) =>
            await FromArray<GuildedMedia>(new Endpoint($"channels/{channelId}/media", Method.GET));
        /// <summary>
        /// Gets all medias within a specific channel with given max count and before given date.
        /// </summary>
        /// <param name="channelId">ID of channel to fetch media from</param>
        /// <returns>List of media posts</returns>
        public IList<GuildedMedia> GetMedia(Guid channelId) =>
            GetMediaAsync(channelId).GetAwaiter().GetResult();
        /// <summary>
        /// Gets all comments in a given media post.
        /// </summary>
        /// <param name="mediaId">ID of the media post</param>
        /// <returns>List of content replies</returns>
        public async Task<IList<ContentReply>> GetMediaRepliesAsync(uint mediaId) =>
            await FromArray<ContentReply>(new Endpoint($"content/team_media/{mediaId}/replies", Method.GET));
        /// <summary>
        /// Gets all comments in a given media post.
        /// </summary>
        /// <param name="mediaId">ID of the media post</param>
        /// <returns>List of content replies</returns>
        public IList<ContentReply> GetMediaReplies(uint mediaId) =>
            GetMediaRepliesAsync(mediaId).GetAwaiter().GetResult();

        //=======================//
        //   Events
        //=======================//

        /// <summary>
        /// Gets given amount of events from a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="maxItems">How many events it should get</param>
        /// <param name="endDate">At which date it should end</param>
        /// <param name="startDate">At which date it should start</param>
        /// <returns>List of calendar events</returns>
        public async Task<IList<CalendarEvent>> GetEventsAsync(Guid channelId, uint? maxItems = 250, DateTime? endDate = null, DateTime? startDate = null) =>
            await FromObject<IList<CalendarEvent>>(new Endpoint($"channels/{channelId}/events?endDate={endDate ?? DateTime.Now}maxItems={maxItems}&startDate={startDate ?? DateTime.Now}", Method.GET), "events");
        /// <summary>
        /// Gets given amount of events from a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="maxItems">How many events it should get</param>
        /// <param name="endDate">At which date it should end</param>
        /// <param name="startDate">At which date it should start</param>
        /// <returns>List of calendar events</returns>
        public IList<CalendarEvent> GetEvents(Guid channelId, uint? maxItems = 250, DateTime? endDate = null, DateTime? startDate = null) =>
            GetEventsAsync(channelId, maxItems, endDate ?? DateTime.Today + TimeSpan.FromDays(30), startDate ?? DateTime.Today - TimeSpan.FromDays(30)).GetAwaiter().GetResult();

        //=======================//
        //   Schedules
        //=======================//

        /// <summary>
        /// Get availabilities in a schedule channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <returns>List of availabilities</returns>
        public async Task<IList<Availability>> GetSchedulesAsync(Guid channelId) =>
            await FromArray<Availability>(new Endpoint($"channels/{channelId}/availability", Method.GET));
        /// <summary>
        /// Get availabilities in a schedule channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <returns>List of availabilities</returns>
        public IList<Availability> GetSchedules(Guid channelId) =>
            GetSchedulesAsync(channelId).GetAwaiter().GetResult();
        
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
        public async Task<IList<Announcement>> GetAnnouncementsAsync(Guid channelId, uint? maxItems = 10, DateTime? beforeDate = null) =>
            await FromObject<IList<Announcement>>(new Endpoint($"channels/{channelId}/announcements?maxItems={maxItems}&beforeDate={beforeDate ?? DateTime.Now}", Method.GET), "");
        /// <summary>
        /// Gets a list of announcements in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="maxItems">How many announcements it should get</param>
        /// <param name="beforeDate">Before which date it should get announcements</param>
        /// <returns>List of announcements</returns>
        public IList<Announcement> GetAnnouncements(Guid channelId, uint? maxItems = 10, DateTime? beforeDate = null) =>
            GetAnnouncementsAsync(channelId, maxItems, beforeDate).GetAwaiter().GetResult();
        /// <summary>
        /// Gets a list of pinned announcements in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <returns>List of announcements</returns>
        public async Task<IList<Announcement>> GetPinnedAnnouncementsAsync(Guid channelId) =>
            await FromObject<IList<Announcement>>(new Endpoint($"channels/{channelId}/announcements/pinned", Method.GET), "announcements");
        /// <summary>
        /// Gets a list of pinned announcements in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <returns>List of announcements</returns>
        public IList<Announcement> GetPinnedAnnouncements(Guid channelId) =>
            GetPinnedAnnouncementsAsync(channelId).GetAwaiter().GetResult();

        //=======================//
        //   List
        //=======================//

        /// <summary>
        /// Gets list items in a given channel.
        /// </summary>
        /// <param name="channelId">Channel ID</param>
        /// <returns>List of list items</returns>
        public async Task<IList<ListItem>> GetListItemsAsync(Guid channelId) =>
            await FromArray<ListItem>(new Endpoint($"channels/{channelId}/listitems", Method.GET));
        /// <summary>
        /// Gets list items in a given channel.
        /// </summary>
        /// <param name="channelId">Channel ID</param>
        /// <returns>List of list items</returns>
        public IList<ListItem> GetListItems(Guid channelId) =>
            GetListItemsAsync(channelId).GetAwaiter().GetResult();
        /// <summary>
        /// Creates a new list item.
        /// </summary>
        /// <param name="channelId">ID of the channel to add a list item in</param>
        /// <param name="title">Title content of this list item</param>
        /// <param name="priority">Order of this list item</param>
        /// <param name="parentId">ID of the parent</param>
        /// <param name="note">Note of this list item</param>
        public async Task CreateListItemAsync(Guid channelId, MessageContent title, long priority = 0, Guid? parentId = null, MessageContent note = null) {
            // Creates a new object for creating list item
            var obj = new {
                id = Guid.NewGuid(),
                message = title,
                priority,
                parentId,
                note
            };
            // Sends it to Guilded
            await ExecuteRequest(new Endpoint($"channels/{channelId}/listitems?notifyAllClients=undefined", Method.PUT), new JsonBody(obj, Converters));
        }
        /// <summary>
        /// Creates a new list item.
        /// </summary>
        /// <param name="channelId">ID of the channel to add a list item in</param>
        /// <param name="title">Title content of this list item</param>
        /// <param name="priority">Order of this list item</param>
        /// <param name="parentId">ID of the parent</param>
        /// <param name="note">Note of this list item</param>
        public void CreateListItem(Guid channelId, MessageContent title, long priority = 0, Guid? parentId = null, MessageContent note = null) =>
            CreateListItemAsync(channelId, title, priority, parentId, note).GetAwaiter().GetResult();
        /// <summary>
        /// Deletes a list item.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="itemId">ID of the item</param>
        public async Task DeleteListItemAsync(Guid channelId, Guid itemId) =>
            await ExecuteRequest(new Endpoint($"channels/{channelId}/listitems/{itemId}", Method.DELETE));
        /// <summary>
        /// Deletes a list item.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="itemId">ID of the item</param>
        public void DeleteListItem(Guid channelId, Guid itemId) =>
            DeleteListItemAsync(channelId, itemId).GetAwaiter().GetResult();

        //=======================//
        //   Multiple
        //=======================//

        /// <summary>
        /// Gets all comments in a given document or media.
        /// </summary>
        /// <param name="contentId">ID of content</param>
        /// <param name="type">Type of the channel</param>
        /// <returns>List of content replies</returns>
        public async Task<IList<ContentReply>> GetContentRepliesAsync(uint contentId, ChannelType type) =>
            await FromArray<ContentReply>(new Endpoint($"content/{ContentTypes[type]}/{contentId}/replies", Method.GET));
        /// <summary>
        /// Gets all comments in a given document or media.
        /// </summary>
        /// <param name="contentId">ID of content</param>
        /// <param name="type">Type of the channel</param>
        /// <returns>List of content replies</returns>
        public IList<ContentReply> GetContentReplies(uint contentId, ChannelType type) =>
            GetContentRepliesAsync(contentId, type).GetAwaiter().GetResult();
        /// <summary>
        /// Deletes a document or a media reply.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <param name="contentId">ID of the content</param>
        /// <param name="replyId">ID of the reply to delete</param>
        /// <param name="type">Channel's type</param>
        public async Task DeleteContentReplyAsync(GId teamId, uint contentId, ulong replyId, ChannelType type) =>
            await ExecuteRequest(new Endpoint($"content/{ContentTypes[type]}/{contentId}/replies/{replyId}", Method.DELETE), new JsonBody(new {
                teamId
            }));
        /// <summary>
        /// Deletes a document or a media reply.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <param name="contentId">ID of the content</param>
        /// <param name="replyId">ID of the reply to delete</param>
        /// <param name="type">Channel's type</param>
        public void DeleteContentReply(GId teamId, uint contentId, ulong replyId, ChannelType type) =>
            DeleteContentReplyAsync(teamId, contentId, replyId, type).GetAwaiter().GetResult();
    }
}