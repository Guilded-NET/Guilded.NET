using System;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using Guilded.NET.Base.Chat;
using Guilded.NET.Base.Users;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Events
{
    /// <summary>
    /// The base for message-related events.
    /// </summary>
    public class MessageEvent<T> : BaseObject, IChannelEvent where T : BaseMessage
    {
        #region JSON properties
        /// <summary>
        /// The message received from the event.
        /// </summary>
        /// <value>Message</value>
        [JsonProperty(Required = Required.Always)]
        public T Message
        {
            get; set;
        }
        #endregion
        
        #region Additional
        /// <summary>
        /// The identifier of the channel where the message was posted.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonIgnore]
        public Guid ChannelId => Message.ChannelId;
        /// <summary>
        /// Sends a new message in the same channel as a response to this message.
        /// </summary>
        /// <param name="content">The contents of the message in rich text markup</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Message posted</returns>
        public async Task<Message> RespondAsync(MessageContent content) =>
            await Message.RespondAsync(content);
        /// <summary>
        /// Sends a new message in the same channel as a response to this message.
        /// </summary>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Message posted</returns>
        public async Task<Message> RespondAsync(string content) =>
            await Message.RespondAsync(content);
        /// <summary>
        /// Sends a new message in the same channel as a response to this message.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Message posted</returns>
        public async Task<Message> RespondAsync(string format, params object[] args) =>
            await RespondAsync(string.Format(format, args));
        /// <summary>
        /// Sends a new message in the same channel as a response to this message.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Message posted</returns>
        public async Task<Message> RespondAsync(IFormatProvider provider, string format, params object[] args) =>
            await RespondAsync(string.Format(provider, format, args));
        /// <summary>
        /// Sends a new message in the same channel as a response to this message.
        /// </summary>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Message posted</returns>
        public async Task<Message> RespondAsync(object content) =>
            await RespondAsync(content.ToString());
        #endregion
    }
    /// <summary>
    /// The base for message-related events.
    /// </summary>
    public class MessageEvent : MessageEvent<Message>
    {
        #region Additional
        /// <summary>
        /// The identifier of the author of this message.
        /// </summary>
        /// <value>User ID</value>
        [JsonIgnore]
        public GId CreatedBy => Message.CreatedBy;
        /// <summary>
        /// The identifier of the webhook that posted this message.
        /// </summary>
        /// <value>Webhook ID?</value>
        [JsonIgnore]
        public Guid? CreatedByWebhook => Message.CreatedByWebhook;
        /// <summary>
        /// The identifier of the bot that posted this message.
        /// </summary>
        /// <value>Bot ID?</value>
        [JsonIgnore]
        public Guid? CreatedByBot => Message.CreatedByBot;
        /// <summary>
        /// Gets whether this message was posted by the given user.
        /// </summary>
        /// <param name="user">User to check</param>
        /// <returns>Message by that user</returns>
        public bool Of(BaseUser user) =>
            Message.Of(user);
        /// <summary>
        /// Edits the content of this message.
        /// </summary>
        /// <param name="content">The new content of the message in rich text markup</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Message edited</returns>
        public async Task<Message> EditMessageAsync(MessageContent content) =>
            await Message.EditMessageAsync(content);
        /// <summary>
        /// Edits the content of this message.
        /// </summary>
        /// <param name="content">The new content of the message in Markdown plain text</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Message edited</returns>
        public async Task<Message> EditMessageAsync(string content) =>
            await Message.EditMessageAsync(content);
        /// <summary>
        /// Edits the content of this message.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Message edited</returns>
        public async Task<Message> EditMessageAsync(string format, params object[] args) =>
            await EditMessageAsync(string.Format(format, args));
        /// <summary>
        /// Edits the content of this message.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Message edited</returns>
        public async Task<Message> EditMessageAsync(IFormatProvider provider, string format, params object[] args) =>
            await EditMessageAsync(string.Format(provider, format, args));
        /// <summary>
        /// Edits the content of this message.
        /// </summary>
        /// <param name="content">The new content of the message in Markdown plain text</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Message edited</returns>
        public async Task<Message> EditMessageAsync(object content) =>
            await EditMessageAsync(content.ToString());
        /// <summary>
        /// Deletes this message.
        /// </summary>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        public async Task DeleteMessageAsync() =>
            await Message.DeleteMessageAsync();
        /// <summary>
        /// Adds a reaction to this message.
        /// </summary>
        /// <param name="emoteId">ID of the emote to add</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Reaction added</returns>
        public async Task<Reaction> AddReactionAsync(uint emoteId) =>
            await Message.AddReactionAsync(emoteId);
        /// <summary>
        /// Removes a reaction from this message.
        /// </summary>
        /// <param name="emoteId">ID of the emote to remove</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        public async Task RemoveReactionAsync(uint emoteId) =>
            await Message.RemoveReactionAsync(emoteId);
        #endregion
    }
}