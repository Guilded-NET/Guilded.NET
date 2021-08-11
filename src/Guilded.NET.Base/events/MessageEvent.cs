using System;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Events
{
    using Chat;
    using Users;
    /// <summary>
    /// The base for message-related events.
    /// </summary>
    public class MessageEvent<T> : BaseObject, ITeamEvent where T : BaseMessage
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
        
        #region Properties
        /// <summary>
        /// The identifier of the channel where the message was posted.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonIgnore]
        public Guid ChannelId => Message.ChannelId;
        #endregion

        #region Additional
        /// <summary>
        /// Creates a new message in same channel as a response.
        /// </summary>
        /// <example>
        /// <code>
        /// await message.RespondAsync(new MessageContent(
        ///     new BlockQuote(message.ToString()),
        ///     new Paragraph("Done!")    
        /// ));
        /// </code>
        /// </example>
        /// <param name="content">The contents of the message in rich text markup</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <returns>Message posted</returns>
        public async Task<Message> RespondAsync(MessageContent content) =>
            await Message.RespondAsync(content);
        /// <summary>
        /// Creates a new message in same channel as a response.
        /// </summary>
        /// <example>
        /// <code>
        /// await message.RespondAsync("Hello!");
        /// </code>
        /// </example>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="ArgumentNullException">When the given content only consists of whitespace or is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">When the contents of the message are above the message limit of 4000 characters</exception>
        /// <returns>Message posted</returns>
        public async Task<Message> RespondAsync(string content) =>
            await Message.RespondAsync(content);
        /// <summary>
        /// Creates a new message in same channel as a response.
        /// </summary>
        /// <example>
        /// <code>
        /// await message.RespondAsync("Result: {0}", result);
        /// </code>
        /// </example>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="ArgumentNullException">When the given content only consists of whitespace or is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">When the contents of the message are above the message limit of 4000 characters</exception>
        /// <returns>Message posted</returns>
        public async Task<Message> RespondAsync(string format, params object[] args) =>
            await RespondAsync(string.Format(format, args));
        /// <summary>
        /// Creates a new message in same channel as a response.
        /// </summary>
        /// <example>
        /// <code>
        /// await message.RespondAsync(cultureInfo, "Current date: {0}", DateTime.Now);
        /// </code>
        /// </example>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="ArgumentNullException">When the given content only consists of whitespace or is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">When the contents of the message are above the message limit of 4000 characters</exception>
        /// <returns>Message posted</returns>
        public async Task<Message> RespondAsync(IFormatProvider provider, string format, params object[] args) =>
            await RespondAsync(string.Format(provider, format, args));
        /// <summary>
        /// Creates a new message in same channel as a response.
        /// </summary>
        /// <example>
        /// <code>
        /// await message.RespondAsync(DateTime.Now);
        /// </code>
        /// </example>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="ArgumentNullException">When the given content only consists of whitespace or is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">When the contents of the message are above the message limit of 4000 characters</exception>
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
        #region Properties
        /// <summary>
        /// Gets whether the message was posted by a bot or webhook.
        /// </summary>
        /// <returns>Created by bot</returns>
        [JsonIgnore]
        public bool ByBot => Message.ByBot;
        /// <summary>
        /// The contents of this message as a Markdown string.
        /// </summary>
        /// <value>Content</value>
        [JsonIgnore]
        public string Content => Message.Content;
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
        #endregion

        #region Additional
        /// <summary>
        /// Gets whether this message was posted by the given user.
        /// </summary>
        /// <param name="user">The potential author of the message</param>
        /// <returns>Message by user</returns>
        public bool Of(BaseUser user) =>
            Message.Of(user);
        /// <summary>
        /// Updates the contents of the message.
        /// </summary>
        /// <example>
        /// <code>
        /// await message.UpdateMessageAsync(new MessageContent("Edited message"));
        /// </code>
        /// </example>
        /// <param name="content">The new content of the message in rich text markup</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <returns>Message edited</returns>
        public async Task<Message> UpdateMessageAsync(MessageContent content) =>
            await Message.UpdateMessageAsync(content);
        /// <summary>
        /// Updates the contents of the message.
        /// </summary>
        /// <example>
        /// <code>
        /// await message.UpdateMessageAsync("Edited message");
        /// </code>
        /// </example>
        /// <param name="content">The new content of the message in Markdown plain text</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="ArgumentNullException">When the given content only consists of whitespace or is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">When the contents of the message are above the message limit of 4000 characters</exception>
        /// <returns>Message edited</returns>
        public async Task<Message> UpdateMessageAsync(string content) =>
            await Message.UpdateMessageAsync(content);
        /// <summary>
        /// Updates the contents of the message.
        /// </summary>
        /// <example>
        /// <code>
        /// await message.UpdateMessageAsync("Results: {0}", result);
        /// </code>
        /// </example>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="ArgumentNullException">When the given content only consists of whitespace or is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">When the contents of the message are above the message limit of 4000 characters</exception>
        /// <returns>Message edited</returns>
        public async Task<Message> UpdateMessageAsync(string format, params object[] args) =>
            await UpdateMessageAsync(string.Format(format, args));
        /// <summary>
        /// Updates the contents of the message.
        /// </summary>
        /// <example>
        /// <code>
        /// await message.UpdateMessageAsync(cultureInfo, "Current date: {0}", DateTime.Now);
        /// </code>
        /// </example>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="ArgumentNullException">When the given content only consists of whitespace or is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">When the contents of the message are above the message limit of 4000 characters</exception>
        /// <returns>Message edited</returns>
        public async Task<Message> UpdateMessageAsync(IFormatProvider provider, string format, params object[] args) =>
            await UpdateMessageAsync(string.Format(provider, format, args));
        /// <summary>
        /// Updates the contents of the message.
        /// </summary>
        /// <example>
        /// <code>
        /// await message.UpdateMessageAsync(result);
        /// </code>
        /// </example>
        /// <param name="content">The new content of the message in Markdown plain text</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="ArgumentNullException">When the given content only consists of whitespace or is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">When the contents of the message are above the message limit of 4000 characters</exception>
        /// <returns>Message edited</returns>
        public async Task<Message> UpdateMessageAsync(object content) =>
            await UpdateMessageAsync(content.ToString());
        /// <summary>
        /// Deletes this message.
        /// </summary>
        /// <example>
        /// <code>
        /// await message.DeleteMessageAsync();
        /// </code>
        /// </example>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        public async Task DeleteMessageAsync() =>
            await Message.DeleteMessageAsync();
        /// <summary>
        /// Adds a reaction to this message.
        /// </summary>
        /// <example>
        /// <code>
        /// await message.AddReactionAsync(90002569);
        /// </code>
        /// </example>
        /// <param name="emoteId">ID of the emote to add</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <returns>Reaction added</returns>
        public async Task<Reaction> AddReactionAsync(uint emoteId) =>
            await Message.AddReactionAsync(emoteId);
        /// <summary>
        /// Removes a reaction from this message.
        /// </summary>
        /// <example>
        /// <code>
        /// await message.RemoveReactionAsync(90002569);
        /// </code>
        /// </example>
        /// <param name="emoteId">ID of the emote to remove</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        public async Task RemoveReactionAsync(uint emoteId) =>
            await Message.RemoveReactionAsync(emoteId);
        #endregion
    }
}