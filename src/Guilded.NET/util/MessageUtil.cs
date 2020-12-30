using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Guilded.NET.Util {
    using Objects.Events;
    using Objects.Teams;
    using Objects.Content;
    using Objects.Chat;
    using Objects;
    /// <summary>
    /// Utilities for message related things.
    /// </summary>
    public static class MessageUtil {
        /// <summary>
        /// Sends a message in the same channel as the given message.
        /// </summary>
        /// <param name="message">Message event to respond to</param>
        /// <param name="response">Message response</param>
        /// <returns>Async task</returns>
        public static async Task<object> RespondAsync(this MessageCreatedEvent message, NewMessage response) =>
            await message.ParentClient.SendMessageAsync(message.ChannelId, response);
        /// <summary>
        /// Sends a message in the same channel as the given message.
        /// </summary>
        /// <param name="message">Message event to respond to</param>
        /// <param name="response">Message response</param>
        public static void Respond(this MessageCreatedEvent message, NewMessage response) =>
            message.ParentClient.SendMessage(message.ChannelId, response);
        /// <summary>
        /// Deletes this message.
        /// </summary>
        /// <param name="message">Message event to delete</param>
        /// <returns>Async task</returns>
        public static async Task<object> DeleteAsync(this MessageCreatedEvent message) =>
            await message.ParentClient.DeleteMessageAsync(message.ChannelId, message.Message.Id);
        /// <summary>
        /// Deletes this message.
        /// </summary>
        /// <param name="message">Message event to delete</param>
        public static void Delete(this MessageCreatedEvent message) =>
            message.ParentClient.DeleteMessage(message.ChannelId, message.Message.Id);
        /// <summary>
        /// Gets parent channel of this message event.
        /// </summary>
        /// <param name="message">Message to get channel from</param>
        /// <returns>Parent channel</returns>
        public static async Task<IChannel> GetChannelAsync(this MessageCreatedEvent message) =>
            await message.ParentClient.GetChannelAsync(message.TeamId, message.ChannelId);
        /// <summary>
        /// Gets parent channel of this message event.
        /// </summary>
        /// <param name="message">Message to get channel from</param>
        /// <returns>Parent channel</returns>
        public static IChannel GetChannel(this MessageCreatedEvent message) =>
            message.ParentClient.GetChannel(message.TeamId, message.ChannelId);
        /// <summary>
        /// Gets parent team of this message event.
        /// </summary>
        /// <param name="message">Message to get team from</param>
        /// <returns>Parent team</returns>
        public static async Task<Team> GetTeamAsync(this MessageCreatedEvent message) =>
            await message.ParentClient.GetTeamAsync(message.TeamId);
        /// <summary>
        /// Gets parent team of this message event.
        /// </summary>
        /// <param name="message">Message to get team from</param>
        /// <returns>Parent team</returns>
        public static Team GetTeam(this MessageCreatedEvent message) =>
            message.ParentClient.GetTeam(message.TeamId);
        /// <summary>
        /// Gets owner or author of this message.
        /// </summary>
        /// <param name="message">Message to get author from</param>
        /// <returns>Message owner</returns>
        public static async Task<User> GetAuthorAsync(this MessageCreatedEvent message) =>
            await message.ParentClient.GetUserAsync(message.Message.AuthorId);
        /// <summary>
        /// Gets owner or author of this message.
        /// </summary>
        /// <param name="message">Message to get author from</param>
        /// <returns>Message owner</returns>
        public static async Task<User> GetAuthorAsync(this Message message) =>
            await message.ParentClient.GetUserAsync(message.AuthorId);
        /// <summary>
        /// Gets owner or author of this message.
        /// </summary>
        /// <param name="message">Message to get author from</param>
        /// <returns>Message owner</returns>
        public static User GetAuthor(this MessageCreatedEvent message) =>
            message.ParentClient.GetUser(message.Message.AuthorId);
        /// <summary>
        /// Gets owner or author of this message.
        /// </summary>
        /// <param name="message">Message to get author from</param>
        /// <returns>Message owner</returns>
        public static User GetAuthor(this Message message) =>
            message.ParentClient.GetUser(message.AuthorId);
        /// <summary>
        /// Replies to a forum post.
        /// </summary>
        /// <param name="post">Post to reply to</param>
        /// <param name="content">Content to reply with</param>
        /// <returns>Async task</returns>
        public static async Task ReplyAsync(this ForumPost post, MessageContent content) =>
            await post.ParentClient.CreateForumReplyAsync((Guid)post.ChannelId, post.Id, content);
        /// <summary>
        /// Replies to a forum post.
        /// </summary>
        /// <param name="post">Post to reply to</param>
        /// <param name="content">Content to reply with</param>
        public static void Reply(this ForumPost post, MessageContent content) =>
            post.ParentClient.CreateForumReply((Guid)post.ChannelId, post.Id, content);
        /// <summary>
        /// Gets replies of a specific forum post.
        /// </summary>
        /// <param name="post">Forum post to get replies of</param>
        /// <param name="maxItems">Max amount of replies it should get</param>
        /// <param name="afterDate">After which date should it get replies</param>
        /// <returns>List of replies</returns>
        public static async Task<IList<ForumReply>> GetRepliesAsync(this ForumPost post, uint maxItems, DateTime? afterDate = null) =>
            await post.ParentClient.GetForumRepliesAsync((Guid)post.ChannelId, post.Id, maxItems, afterDate);
        /// <summary>
        /// Gets replies of a specific forum post.
        /// </summary>
        /// <param name="post">Forum post to get replies of</param>
        /// <param name="maxItems">Max amount of replies it should get</param>
        /// <param name="afterDate">After which date should it get replies</param>
        /// <returns>List of replies</returns>
        public static IList<ForumReply> GetReplies(this ForumPost post, uint maxItems, DateTime? afterDate = null) =>
            post.ParentClient.GetForumReplies((Guid)post.ChannelId, post.Id, maxItems, afterDate);
        /// <summary>
        /// If this message was posted by this client.
        /// </summary>
        /// <param name="message">Message to check</param>
        /// <returns>Client message</returns>
        public static bool IsClientMessage(this Message message) =>
            message?.ParentClient is BasicGuildedClient client && message.IsMessageOf(client?.CurrentUser);
        /// <summary>
        /// If this message was posted by this client.
        /// </summary>
        /// <param name="message">Message to check</param>
        /// <returns>Client message</returns>
        public static bool IsClientMessage(this MessageCreatedEvent message) =>
            message.Message.IsClientMessage();
        /// <summary>
        /// If this message was posted by given user
        /// </summary>
        /// <param name="message">Message to check</param>
        /// <param name="user">User to check if it's message author</param>
        /// <returns>Message by that user</returns>
        public static bool IsMessageOf(this Message message, User user) =>
            message.AuthorId == user?.Id;
        /// <summary>
        /// If this message was posted by given user.
        /// </summary>
        /// <param name="message">Message to check</param>
        /// <param name="user">User to check if it's message author</param>
        /// <returns>Message by that user</returns>
        public static bool IsMessageOf(this MessageCreatedEvent message, User user) =>
            message.Message.IsMessageOf(user);
        /// <summary>
        /// If message was updated by given user.
        /// </summary>
        /// <param name="message">Message to check</param>
        /// <param name="user">User to check if it's message update author</param>
        /// <returns>Message updated by that user</returns>
        public static bool WasUpdatedBy(this MessageUpdatedEvent message, User user) =>
            message.UpdatedBy == user?.Id;
    }
}