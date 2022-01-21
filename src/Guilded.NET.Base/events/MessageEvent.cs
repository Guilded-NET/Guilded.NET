using System;
using System.Threading.Tasks;
using Guilded.NET.Base.Content;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Events
{
    /// <summary>
    /// The base for message-related events.
    /// </summary>
    /// <seealso cref="Message"/>
    /// <seealso cref="MessageCreatedEvent"/>
    /// <seealso cref="MessageUpdatedEvent"/>
    /// <seealso cref="MessageDeletedEvent"/>
    public class MessageEvent<T> : BaseObject where T : BaseObject
    {
        #region JSON properties
        /// <summary>
        /// The message received from the event.
        /// </summary>
        /// <value>Message</value>
        public T Message
        {
            get; set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="MessageEvent"/>. This is currently only used in deserialization.
        /// </summary>
        /// <param name="message">The message received from the event</param>
        public MessageEvent(
            T message
        ) =>
            Message = message;
        #endregion
    }
    /// <summary>
    /// The base for message-related events.
    /// </summary>
    /// <seealso cref="Message"/>
    /// <seealso cref="MessageCreatedEvent"/>
    /// <seealso cref="MessageUpdatedEvent"/>
    /// <seealso cref="MessageDeletedEvent"/>
    public class MessageEvent : MessageEvent<Message>
    {
        #region Properties
        /// <inheritdoc cref="ChannelContent{T, S}.ChannelId"/>
        [JsonIgnore]
        public Guid ChannelId => Message.ChannelId;
        /// <inheritdoc cref="ChannelContent{T, S}.ServerId"/>
        [JsonIgnore]
        public HashId? ServerId => Message.ServerId;
        /// <inheritdoc cref="Message.Content"/>
        [JsonIgnore]
        public string Content => Message.Content;
        /// <inheritdoc cref="ChannelContent{T, S}.CreatedBy"/>
        [JsonIgnore]
        public HashId CreatedBy => Message.CreatedBy;
        /// <inheritdoc cref="Message.CreatedByWebhook"/>
        [JsonIgnore]
        public Guid? CreatedByWebhook => Message.CreatedByWebhook;
        /// <inheritdoc cref="ChannelContent{T, S}.CreatedAt"/>
        [JsonIgnore]
        public DateTime CreatedAt => Message.CreatedAt;
        /// <inheritdoc cref="Message.Type"/>
        [JsonIgnore]
        public MessageType Type => Message.Type;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="MessageEvent"/>. This is currently only used in deserialization.
        /// </summary>
        /// <param name="message">The message received from the event</param>
        [JsonConstructor]
        public MessageEvent(
            [JsonProperty(Required = Required.Always)]
            Message message
        ) : base(message) { }
        #endregion

        #region Additional
        /// <inheritdoc cref="Message.CreateMessageAsync(string)"/>
        public async Task<Message> CreateMessageAsync(string content) =>
            await Message.CreateMessageAsync(content).ConfigureAwait(false);
        /// <inheritdoc cref="Message.CreateMessageAsync(string, Guid[])"/>
        public async Task<Message> CreateMessageAsync(string content, params Guid[] replyMessageIds) =>
            await Message.CreateMessageAsync(content, replyMessageIds).ConfigureAwait(false);
        /// <inheritdoc cref="Message.CreateMessageAsync(string, bool, Guid[])"/>
        public async Task<Message> CreateMessageAsync(string content, bool isPrivate, params Guid[] replyMessageIds) =>
            await Message.CreateMessageAsync(content, isPrivate, replyMessageIds).ConfigureAwait(false);
        /// <inheritdoc cref="Message.ReplyAsync(string)"/>
        public async Task<Message> ReplyAsync(string content) =>
            await Message.ReplyAsync(content).ConfigureAwait(false);
        /// <inheritdoc cref="Message.ReplyAsync(string, bool)"/>
        public async Task<Message> ReplyAsync(string content, bool isPrivate) =>
            await Message.ReplyAsync(content, isPrivate).ConfigureAwait(false);
        /// <inheritdoc cref="Message.UpdateMessageAsync(string)"/>
        public async Task<Message> UpdateMessageAsync(string content) =>
            await Message.UpdateMessageAsync(content).ConfigureAwait(false);
        /// <inheritdoc cref="Message.DeleteMessageAsync"/>
        public async Task DeleteMessageAsync() =>
            await Message.DeleteMessageAsync().ConfigureAwait(false);
        /// <inheritdoc cref="Message.AddReactionAsync(uint)"/>
        public async Task<Reaction> AddReactionAsync(uint emoteId) =>
            await Message.AddReactionAsync(emoteId).ConfigureAwait(false);
        /// <inheritdoc cref="Message.RemoveReactionAsync(uint)"/>
        public async Task RemoveReactionAsync(uint emoteId) =>
            await Message.RemoveReactionAsync(emoteId).ConfigureAwait(false);
        #endregion
    }
}