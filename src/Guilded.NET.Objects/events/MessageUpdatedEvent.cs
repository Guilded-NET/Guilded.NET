using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Guilded.NET.Objects.Events {
    using Teams;
    /// <summary>
    /// Event when message gets updated.
    /// </summary>
    public class MessageUpdatedEvent: CommonEvent {
        /// <summary>
        /// Type of the content.
        /// </summary>
        /// <value>Content type</value>
        [JsonProperty("contentType")]
        public ChannelType ContentType {
            get; set;
        }
        /// <summary>
        /// Message which has been updated.
        /// </summary>
        /// <value>Message updated</value>
        [JsonProperty("message", Required = Required.Always)]
        public MessageUpdated Message {
            get; set;
        }
        /// <summary>
        /// Who updated this message.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("updatedBy", Required = Required.Always)]
        public GId UpdatedBy {
            get; set;
        }
        /// <summary>
        /// ID of the message.
        /// </summary>
        /// <value>Message ID</value>
        [JsonProperty("contentId")]
        public Guid ContentId {
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
        /// Gets author of this message's update.
        /// </summary>
        /// <returns>Message update author</returns>
        public async Task<User> GetAuthorAsync() =>
            await ParentClient.GetUserAsync(UpdatedBy);
        /// <summary>
        /// Gets author of this message's update.
        /// </summary>
        /// <returns>Message update author</returns>
        public User GetAuthor() =>
            ParentClient.GetUser(UpdatedBy);
        /// <summary>
        /// If message was updated by given user.
        /// </summary>
        /// <param name="user">User to check if it's message update author</param>
        /// <returns>Message updated by that user</returns>
        public bool WasUpdatedBy(User user) =>
            UpdatedBy == user?.Id;
    }
}