using System.Threading.Tasks;
using System;

namespace Guilded.NET.Util {
    using Objects.Teams;
    using Objects.Chat;
    using Objects;
    /// <summary>
    /// Utilities for channel related things.
    /// </summary>
    public static class ChannelId {
        /// <summary>
        /// Sends a message to the specific channel.
        /// </summary>
        /// <param name="channel">Channel to send to</param>
        /// <param name="message">Message itself</param>
        public static async Task SendMessageAsync(this Channel channel, NewMessage message) =>
            await channel.ParentClient.SendMessageAsync(channel.Id, message);
        /// <summary>
        /// Sends a message to the specific channel.
        /// </summary>
        /// <param name="channel">Channel to send to</param>
        /// <param name="message">Message itself</param>
        public static void SendMessage(this Channel channel, NewMessage message) =>
            channel.ParentClient.SendMessage(channel.Id, message);
        /// <summary>
        /// Sends a message to the specific channel.
        /// </summary>
        /// <param name="channel">Channel to send to</param>
        /// <param name="messageId">Message itself</param>
        public static async Task<Message> GetMessageAsync(this Channel channel, Guid messageId) =>
            await channel.ParentClient.GetMessageAsync(channel, messageId);
        /// <summary>
        /// Sends a message to the specific channel.
        /// </summary>
        /// <param name="channel">Channel to send to</param>
        /// <param name="messageId">Message itself</param>
        public static Message GetMessage(this Channel channel, Guid messageId) =>
            channel.ParentClient.GetMessage(channel, messageId);
        /// <summary>
        /// Deletes a message posted in the chat.
        /// </summary>
        /// <param name="channel">Channel to delete message in</param>
        /// <param name="messageId">ID of the message to delete</param>
        /// <returns>Response</returns>
        public static async Task<object> DeleteMessageAsync(this Channel channel, Guid messageId) =>
            await channel.ParentClient.DeleteMessageAsync(channel.Id, messageId);
        /// <summary>
        /// Deletes a message posted in the chat.
        /// </summary>
        /// <param name="channel">Channel to delete message in</param>
        /// <param name="messageId">ID of the message to delete</param>
        public static void DeleteMessage(this Channel channel, Guid messageId) =>
            channel.ParentClient.DeleteMessage(channel.Id, messageId);
        /// <summary>
        /// Edits message of the bot posted in the chat.
        /// </summary>
        /// <param name="channel">Channel to edit message in</param>
        /// <param name="messageId">ID of the message to edit</param>
        /// <param name="content">New content of the message</param>
        /// <returns>Response</returns>
        public static async Task<object> EditMessageAsync(this Channel channel, Guid messageId, MessageContent content) =>
            await channel.ParentClient.EditMessageAsync(channel.Id, messageId, content);
        /// <summary>
        /// Edits message of the bot posted in the chat.
        /// </summary>
        /// <param name="channel">Channel to edit message in</param>
        /// <param name="messageId">ID of the message to edit</param>
        /// <param name="content">New content of the message</param>
        public static void EditMessage(this Channel channel, Guid messageId, MessageContent content) =>
            channel.ParentClient.EditMessage(channel.Id, messageId, content);
        /// <summary>
        /// Sends a message to the specific channel.
        /// </summary>
        /// <param name="channel">Channel to send to</param>
        /// <param name="message">Message itself</param>
        public static async Task SendMessageAsync(this DMChannel channel, NewMessage message) =>
            await channel.ParentClient.SendMessageAsync(channel.Id, message);
        /// <summary>
        /// Sends a message to the specific channel.
        /// </summary>
        /// <param name="channel">Channel to send to</param>
        /// <param name="message">Message itself</param>
        public static void SendMessage(this DMChannel channel, NewMessage message) =>
            channel.ParentClient.SendMessage(channel.Id, message);
        /// <summary>
        /// Deletes a message posted in the chat.
        /// </summary>
        /// <param name="channel">Channel to delete message in</param>
        /// <param name="messageId">ID of the message to delete</param>
        /// <returns>Response</returns>
        public static async Task<object> DeleteMessageAsync(this DMChannel channel, Guid messageId) =>
            await channel.ParentClient.DeleteMessageAsync(channel.Id, messageId);
        /// <summary>
        /// Deletes a message posted in the chat.
        /// </summary>
        /// <param name="channel">Channel to delete message in</param>
        /// <param name="messageId">ID of the message to delete</param>
        public static void DeleteMessage(this DMChannel channel, Guid messageId) =>
            channel.ParentClient.DeleteMessage(channel.Id, messageId);
        /// <summary>
        /// Edits message of the bot posted in the chat.
        /// </summary>
        /// <param name="channel">Channel to edit message in</param>
        /// <param name="messageId">ID of the message to edit</param>
        /// <param name="content">New content of the message</param>
        /// <returns>Response</returns>
        public static async Task<object> EditMessageAsync(this DMChannel channel, Guid messageId, MessageContent content) =>
            await channel.ParentClient.EditMessageAsync(channel.Id, messageId, content);
        /// <summary>
        /// Edits message of the bot posted in the chat.
        /// </summary>
        /// <param name="channel">Channel to edit message in</param>
        /// <param name="messageId">ID of the message to edit</param>
        /// <param name="content">New content of the message</param>
        public static void EditMessage(this DMChannel channel, Guid messageId, MessageContent content) =>
            channel.ParentClient.EditMessage(channel.Id, messageId, content);
        /// <summary>
        /// Sends a message to the specific channel.
        /// </summary>
        /// <param name="channel">Channel to send to</param>
        /// <param name="message">Message itself</param>
        public static async Task SendMessageAsync(this ThreadChannel channel, NewMessage message) =>
            await channel.ParentClient.SendMessageAsync(channel.Id, message);
        /// <summary>
        /// Sends a message to the specific channel.
        /// </summary>
        /// <param name="channel">Channel to send to</param>
        /// <param name="message">Message itself</param>
        public static void SendMessage(this ThreadChannel channel, NewMessage message) =>
            channel.ParentClient.SendMessage(channel.Id, message);
        /// <summary>
        /// Deletes a message posted in the chat.
        /// </summary>
        /// <param name="channel">Channel to delete message in</param>
        /// <param name="messageId">ID of the message to delete</param>
        /// <returns>Response</returns>
        public static async Task<object> DeleteMessageAsync(this ThreadChannel channel, Guid messageId) =>
            await channel.ParentClient.DeleteMessageAsync(channel.Id, messageId);
        /// <summary>
        /// Deletes a message posted in the chat.
        /// </summary>
        /// <param name="channel">Channel to delete message in</param>
        /// <param name="messageId">ID of the message to delete</param>
        public static void DeleteMessage(this ThreadChannel channel, Guid messageId) =>
            channel.ParentClient.DeleteMessage(channel.Id, messageId);
        /// <summary>
        /// Edits message of the bot posted in the chat.
        /// </summary>
        /// <param name="channel">Channel to edit message in</param>
        /// <param name="messageId">ID of the message to edit</param>
        /// <param name="content">New content of the message</param>
        /// <returns>Response</returns>
        public static async Task<object> EditMessageAsync(this ThreadChannel channel, Guid messageId, MessageContent content) =>
            await channel.ParentClient.EditMessageAsync(channel.Id, messageId, content);
        /// <summary>
        /// Edits message of the bot posted in the chat.
        /// </summary>
        /// <param name="channel">Channel to edit message in</param>
        /// <param name="messageId">ID of the message to edit</param>
        /// <param name="content">New content of the message</param>
        public static void EditMessage(this ThreadChannel channel, Guid messageId, MessageContent content) =>
            channel.ParentClient.EditMessage(channel.Id, messageId, content);
        /// <summary>
        /// Gets the team of a channel.
        /// </summary>
        /// <param name="channel">Channel to get team of</param>
        /// <returns>Team</returns>
        public static async Task<Team> GetTeamAsync(this Channel channel) =>
            await channel.ParentClient.GetTeamAsync(channel.TeamId);
        /// <summary>
        /// Gets the team of a channel.
        /// </summary>
        /// <param name="channel">Channel to get team of</param>
        /// <returns>Team</returns>
        public static Team GetTeam(this Channel channel) =>
            channel.ParentClient.GetTeam(channel.TeamId);
        /// <summary>
        /// Gets the team of a thread.
        /// </summary>
        /// <param name="thread">Thread to get team of</param>
        /// <returns>Team</returns>
        public static async Task<Team> GetTeamAsync(this ThreadChannel thread) =>
            await thread.ParentClient.GetTeamAsync(thread.TeamId);
        /// <summary>
        /// Gets the team of a thread.
        /// </summary>
        /// <param name="thread">Thread to get team of</param>
        /// <returns>Team</returns>
        public static Team GetTeam(this ThreadChannel thread) =>
            thread.ParentClient.GetTeam(thread.TeamId);
        /// <summary>
        /// Gets the team of a category.
        /// </summary>
        /// <param name="category">Category to get team of</param>
        /// <returns>Team</returns>
        public static async Task<Team> GetTeamAsync(this Category category) =>
            await category.ParentClient.GetTeamAsync(category.TeamId);
        /// <summary>
        /// Gets the team of a category.
        /// </summary>
        /// <param name="category">Category to get team of</param>
        /// <returns>Team</returns>
        public static Team GetTeam(this Category category) =>
            category.ParentClient.GetTeam(category.TeamId);
        /// <summary>
        /// Creates a channel mention based on a given channel.
        /// </summary>
        /// <param name="channel">Channel to mention</param>
        /// <returns>Channel mention</returns>
        public static ChannelMention CreateMention(this Channel channel) =>
            ChannelMention.Generate(channel);
    }
}