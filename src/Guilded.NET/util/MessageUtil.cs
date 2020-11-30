using System.Threading.Tasks;

namespace Guilded.NET.Util {
    using Guilded.NET.Objects.Events;
    using Guilded.NET.Objects.Teams;
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
    }
}