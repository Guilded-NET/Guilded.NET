using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Guilded.NET.Objects.Events {
    using Chat;
    using Teams;
    /// <summary>
    /// Event when message is posted in the chat.
    /// </summary>
    public class MessageCreatedEvent: CommonEvent {
        /// <summary>
        /// Type of the content.
        /// </summary>
        /// <value>Content type</value>
        [JsonProperty("contentType", Required = Required.Always)]
        public ChannelType ContentType {
            get; set;
        }
        /// <summary>
        /// The message which was posted.
        /// </summary>
        /// <value>Message</value>
        [JsonProperty("message", Required = Required.Always)]
        public Message Message {
            get; set;
        }

        /// <summary>
        /// Adds a reaction on a message.
        /// </summary>
        /// <param name="emoteId">ID of the emote to react with</param>
        public async Task AddReactionAsync(uint emoteId) =>
            await ParentClient.AddReactionAsync(ChannelId, Message.Id, emoteId);
        /// <summary>
        /// Adds a reaction on a message.
        /// </summary>
        /// <param name="emoteId">ID of the emote to react with</param>
        public void AddReaction(uint emoteId) =>
            ParentClient.AddReaction(ChannelId, Message.Id, emoteId);
        /// <summary>
        /// Adds a reaction on a message.
        /// </summary>
        /// <param name="emote">Emote to react with</param>
        public async Task AddReactionAsync(ChatEmote emote) =>
            await AddReactionAsync(emote.Id);
        /// <summary>
        /// Adds a reaction on a message.
        /// </summary>
        /// <param name="emote">Emote to react with</param>
        public void AddReaction(ChatEmote emote) =>
            AddReaction(emote.Id);
        /// <summary>
        /// Removes a reaction from a message
        /// </summary>
        /// <param name="emoteId">ID of the emote to unreact with</param>
        public async Task RemoveReactionAsync(uint emoteId) =>
            await ParentClient.RemoveReactionAsync(ChannelId, Message.Id, emoteId);
        /// <summary>
        /// Removes a reaction from a message
        /// </summary>
        /// <param name="emoteId">ID of the emote to unreact with</param>
        public void RemoveReaction(uint emoteId) =>
            ParentClient.RemoveReaction(ChannelId, Message.Id, emoteId);
        /// <summary>
        /// Removes a reaction from a message
        /// </summary>
        /// <param name="emote">Emote to unreact with</param>
        public async Task RemoveReactionAsync(ChatEmote emote) =>
            await RemoveReactionAsync(emote.Id);
        /// <summary>
        /// Removes a reaction from a message
        /// </summary>
        /// <param name="emote">Emote to unreact with</param>
        public void RemoveReaction(ChatEmote emote) =>
            RemoveReaction(emote.Id);
        /// <summary>
        /// Deletes this message.
        /// </summary>
        /// <returns>Async task</returns>
        public async Task<object> DeleteAsync() =>
            await ParentClient.DeleteMessageAsync(ChannelId, Message.Id);
        /// <summary>
        /// Deletes this message.
        /// </summary>
        public void Delete() =>
            ParentClient.DeleteMessage(ChannelId, Message.Id);
        /// <summary>
        /// Gets owner or author of this message.
        /// </summary>
        /// <returns>Message owner</returns>
        public async Task<User> GetAuthorAsync() =>
            await ParentClient.GetUserAsync(Message.AuthorId);
        /// <summary>
        /// Gets owner or author of this message.
        /// </summary>
        /// <returns>Message owner</returns>
        public User GetAuthor() =>
            ParentClient.GetUser(Message.AuthorId);
        /// <summary>
        /// If this message was posted by given user.
        /// </summary>
        /// <param name="user">User to check if it's message author</param>
        /// <returns>Message by that user</returns>
        public bool IsMessageOf(User user) =>
            Message.IsMessageOf(user);
    }
}