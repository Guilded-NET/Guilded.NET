using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Chat
{
    using Users;
    /// <summary>
    /// A message posted in the chat.
    /// </summary>
    public class Message : BaseMessage
    {
        #region JSON properties
        /// <summary>
        /// The content of this message as a Markdown string.
        /// </summary>
        /// <value>Content?</value>
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
        /// Whether this message is pinned or not.
        /// </summary>
        /// <value>Pinned</value>
        [DefaultValue(false)]
        public bool IsPinned
        {
            get; set;
        }
        /// <summary>
        /// The identifier of the user who pinned this message.
        /// </summary>
        /// <value>User ID?</value>
        public GId? PinnedBy
        {
            get; set;
        }
        /// <summary>
        /// A list of all reactions on this message.
        /// </summary>
        /// <value>Reactions</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IList<Reaction> Reactions
        {
            get; set;
        } = new List<Reaction>();
        #endregion

        #region Additional
        /// <summary>
        /// Edits message of the bot posted in the chat.
        /// </summary>
        /// <param name="content">The new content of the message in rich text markup</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Message edited</returns>
        public async Task<Message> EditMessageAsync(MessageContent content) =>
            await ParentClient.EditMessageAsync(ChannelId, Id, content);
        /// <summary>
        /// Edits message of the bot posted in the chat.
        /// </summary>
        /// <param name="content">The new content of the message in Markdown plain text</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Message edited</returns>
        public async Task<Message> EditMessageAsync(string content) =>
            await ParentClient.EditMessageAsync(ChannelId, Id, content);
        /// <summary>
        /// Edits message of the bot posted in the chat.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Message edited</returns>
        public async Task<Message> EditMessageAsync(string format, params object[] args) =>
            await EditMessageAsync(string.Format(format, args));
        /// <summary>
        /// Edits message of the bot posted in the chat.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Message edited</returns>
        public async Task<Message> EditMessageAsync(IFormatProvider provider, string format, params object[] args) =>
            await EditMessageAsync(string.Format(provider, format, args));
        /// <summary>
        /// Edits message of the bot posted in the chat.
        /// </summary>
        /// <param name="content">The new content of the message in Markdown plain text</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Message edited</returns>
        public async Task<Message> EditMessageAsync(object content) =>
            await EditMessageAsync(content);
        /// <summary>
        /// Deletes this message.
        /// </summary>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        public async Task DeleteMessageAsync() =>
            await ParentClient.DeleteMessageAsync(ChannelId, Id);
        /// <summary>
        /// Add a reaction to this message.
        /// </summary>
        /// <param name="emoteId">ID of the emote to add</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Reaction added</returns>
        public async Task<Reaction> AddReactionAsync(uint emoteId) =>
            await ParentClient.AddReactionAsync(ChannelId, Id, emoteId);
        /// <summary>
        /// Removes a reaction from this message.
        /// </summary>
        /// <param name="emoteId">ID of the emote to remove</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        public async Task RemoveReactionAsync(uint emoteId) =>
            await ParentClient.RemoveReactionAsync(ChannelId, Id, emoteId);
        /// <summary>
        /// Gets whether this message was posted by the given user.
        /// </summary>
        /// <param name="user">User to check</param>
        /// <returns>Message by that user</returns>
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
