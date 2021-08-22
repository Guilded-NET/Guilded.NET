using System;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Chat
{
    using System.ComponentModel;
    using Users;
    /// <summary>
    /// A message posted in the chat.
    /// </summary>
    /// <seealso cref="MessageContent"/>
    /// <seealso cref="Paragraph"/>
    /// <seealso cref="ContainerNode{T}"/>
    /// <seealso cref="Node"/>
    /// <seealso cref="Events.MessageCreatedEvent"/>
    /// <seealso cref="Events.MessageUpdatedEvent"/>
    public class Message : BaseMessage
    {
        #region JSON properties
        /// <summary>
        /// The contents of this message as a Markdown string.
        /// </summary>
        /// <value>Content</value>
        [JsonProperty(Required = Required.Always)]
        public string Content
        {
            get; set;
        }
        /// <summary>
        /// The identifier of the author of this message.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty(Required = Required.Always)]
        public GId CreatedBy
        {
            get; set;
        }
        /// <summary>
        /// The identifier of the webhook that posted this message.
        /// </summary>
        /// <value>Webhook ID?</value>
        [JsonProperty("createdByWebhookId")]
        public Guid? CreatedByWebhook
        {
            get; set;
        }
        /// <summary>
        /// The identifier of the bot that posted this message.
        /// </summary>
        /// <value>Bot ID?</value>
        [JsonProperty("createdByBotId")]
        public Guid? CreatedByBot
        {
            get; set;
        }
        /// <summary>
        /// The date of when this message was posted.
        /// </summary>
        /// <value>Created at</value>
        [JsonProperty(Required = Required.Always)]
        public DateTime CreatedAt
        {
            get; set;
        }
        /// <summary>
        /// The date of when this message was edited.
        /// </summary>
        /// <value>Updated at?</value>
        public DateTime? UpdatedAt
        {
            get; set;
        }
        /// <summary>
        /// The type of the message it is.
        /// </summary>
        /// <value>Message type</value>
        [DefaultValue(MessageType.Default)]
        public MessageType Type
        {
            get; set;
        }
        // /// <summary>
        // /// Whether this message is pinned or not.
        // /// </summary>
        // /// <value>Pinned</value>
        // public bool IsPinned
        // {
        //     get; set;
        // }
        // /// <summary>
        // /// The identifier of the user who pinned this message.
        // /// </summary>
        // /// <value>User ID?</value>
        // public GId? PinnedBy
        // {
        //     get; set;
        // }
        // /// <summary>
        // /// A list of all reactions on this message.
        // /// </summary>
        // /// <value>Reactions</value>
        // [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        // public IList<Reaction> Reactions
        // {
        //     get; set;
        // } = new List<Reaction>();
        #endregion

        #region Properties
        /// <summary>
        /// Gets whether the message was created by a bot or webhook.
        /// </summary>
        /// <returns>Created by bot</returns>
        [JsonIgnore]
        public bool ByBot => !(CreatedByBot is null) && !(CreatedByWebhook is null);
        #endregion

        #region Additional
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
            await ParentClient.UpdateMessageAsync(ChannelId, Id, content);
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
            await ParentClient.UpdateMessageAsync(ChannelId, Id, content);
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
            await UpdateMessageAsync(content);
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
            await ParentClient.DeleteMessageAsync(ChannelId, Id);
        /// <summary>
        /// Add a reaction to this message.
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
            await ParentClient.AddReactionAsync(ChannelId, Id, emoteId);
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
            await ParentClient.RemoveReactionAsync(ChannelId, Id, emoteId);
        /// <summary>
        /// Gets whether this message was posted by <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The potential author of this message</param>
        /// <returns>Message by <paramref name="user"/></returns>
        public bool Of(BaseUser user) =>
            CreatedBy == user?.Id;
        #endregion

        #region Overrides
        /// <summary>
        /// Converts the content of this message to Markdown representation of it.
        /// </summary>
        /// <returns>Content as string</returns>
        public override string ToString() =>
            Content;
        #endregion
    }
}
