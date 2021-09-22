using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Events
{
    using Chat;
    using Permissions;
    /// <summary>
    /// The base for message-related events.
    /// </summary>
    /// <seealso cref="Message"/>
    /// <seealso cref="MessageCreatedEvent"/>
    /// <seealso cref="MessageUpdatedEvent"/>
    /// <seealso cref="MessageDeletedEvent"/>
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
        /// <inheritdoc cref="BaseMessage.ChannelId"/>
        [JsonIgnore]
        public Guid ChannelId => Message.ChannelId;
        #endregion

        #region Additional
        /// <inheritdoc cref="BaseMessage.RespondAsync(MessageContent)"/>
        public async Task<Message> RespondAsync(MessageContent content) =>
            await Message.RespondAsync(content).ConfigureAwait(false);
        /// <inheritdoc cref="BaseMessage.RespondAsync(string)"/>
        public async Task<Message> RespondAsync(string content) =>
            await Message.RespondAsync(content).ConfigureAwait(false);
        /// <inheritdoc cref="BaseMessage.RespondAsync(string, object)"/>
        public async Task<Message> RespondAsync(string format, object arg0) =>
            await RespondAsync(string.Format(format, arg0)).ConfigureAwait(false);
        /// <inheritdoc cref="BaseMessage.RespondAsync(string, object, object)"/>
        public async Task<Message> RespondAsync(string format, object arg0, object arg1) =>
            await RespondAsync(string.Format(format, arg0, arg1)).ConfigureAwait(false);
        /// <inheritdoc cref="BaseMessage.RespondAsync(string, object, object, object)"/>
        public async Task<Message> RespondAsync(string format, object arg0, object arg1, object arg2) =>
            await RespondAsync(string.Format(format, arg0, arg1, arg2)).ConfigureAwait(false);
        /// <inheritdoc cref="BaseMessage.RespondAsync(string, object[])"/>
        public async Task<Message> RespondAsync(string format, params object[] args) =>
            await RespondAsync(string.Format(format, args)).ConfigureAwait(false);
        /// <inheritdoc cref="BaseMessage.RespondAsync(IFormatProvider, string, object)"/>
        public async Task<Message> RespondAsync(IFormatProvider provider, string format, object arg0) =>
            await RespondAsync(string.Format(provider, format, arg0)).ConfigureAwait(false);
        /// <inheritdoc cref="BaseMessage.RespondAsync(IFormatProvider, string, object, object)"/>
        public async Task<Message> RespondAsync(IFormatProvider provider, string format, object arg0, object arg1) =>
            await RespondAsync(string.Format(provider, format, arg0, arg1)).ConfigureAwait(false);
        /// <inheritdoc cref="BaseMessage.RespondAsync(IFormatProvider, string, object, object, object)"/>
        public async Task<Message> RespondAsync(IFormatProvider provider, string format, object arg0, object arg1, object arg2) =>
            await RespondAsync(string.Format(provider, format, arg0, arg1, arg2)).ConfigureAwait(false);
        /// <inheritdoc cref="BaseMessage.RespondAsync(IFormatProvider, string, object[])"/>
        public async Task<Message> RespondAsync(IFormatProvider provider, string format, params object[] args) =>
            await RespondAsync(string.Format(provider, format, args)).ConfigureAwait(false);
        /// <inheritdoc cref="BaseMessage.RespondAsync(object)"/>
        public async Task<Message> RespondAsync(object content) =>
            await RespondAsync(content.ToString()).ConfigureAwait(false);
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
        /// <inheritdoc cref="Message.ByBot"/>
        [JsonIgnore]
        public bool ByBot => Message.ByBot;
        /// <inheritdoc cref="Message.Content"/>
        [JsonIgnore]
        public string Content => Message.Content;
        /// <inheritdoc cref="Message.CreatedBy"/>
        [JsonIgnore]
        public GId CreatedBy => Message.CreatedBy;
        /// <inheritdoc cref="Message.CreatedByWebhook"/>
        [JsonIgnore]
        public Guid? CreatedByWebhook => Message.CreatedByWebhook;
        /// <inheritdoc cref="Message.CreatedByBot"/>
        [JsonIgnore]
        public Guid? CreatedByBot => Message.CreatedByBot;
        /// <inheritdoc cref="Message.Type"/>
        [JsonIgnore]
        public MessageType Type => Message.Type;
        #endregion

        #region Additional
        /// <inheritdoc cref="Message.UpdateMessageAsync(MessageContent)"/>
        public async Task<Message> UpdateMessageAsync(MessageContent content) =>
            await Message.UpdateMessageAsync(content).ConfigureAwait(false);
        /// <inheritdoc cref="Message.UpdateMessageAsync(string)"/>
        public async Task<Message> UpdateMessageAsync(string content) =>
            await Message.UpdateMessageAsync(content).ConfigureAwait(false);
        /// <inheritdoc cref="Message.UpdateMessageAsync(string, object)"/>
        public async Task<Message> UpdateMessageAsync(string format, object arg0) =>
            await UpdateMessageAsync(string.Format(format, arg0)).ConfigureAwait(false);
        /// <inheritdoc cref="Message.UpdateMessageAsync(string, object, object)"/>
        public async Task<Message> UpdateMessageAsync(string format, object arg0, object arg1) =>
            await UpdateMessageAsync(string.Format(format, arg0, arg1)).ConfigureAwait(false);
        /// <inheritdoc cref="Message.UpdateMessageAsync(string, object, object, object)"/>
        public async Task<Message> UpdateMessageAsync(string format, object arg0, object arg1, object arg2) =>
            await UpdateMessageAsync(string.Format(format, arg0, arg1, arg2)).ConfigureAwait(false);
        /// <inheritdoc cref="Message.UpdateMessageAsync(string, object[])"/>
        public async Task<Message> UpdateMessageAsync(string format, params object[] args) =>
            await UpdateMessageAsync(string.Format(format, args)).ConfigureAwait(false);
        /// <inheritdoc cref="Message.UpdateMessageAsync(IFormatProvider, string, object)"/>
        public async Task<Message> UpdateMessageAsync(IFormatProvider provider, string format, object arg0) =>
            await UpdateMessageAsync(string.Format(provider, format, arg0)).ConfigureAwait(false);
        /// <inheritdoc cref="Message.UpdateMessageAsync(IFormatProvider, string, object, object)"/>
        public async Task<Message> UpdateMessageAsync(IFormatProvider provider, string format, object arg0, object arg1) =>
            await UpdateMessageAsync(string.Format(provider, format, arg0, arg1)).ConfigureAwait(false);
        /// <inheritdoc cref="Message.UpdateMessageAsync(IFormatProvider, string, object, object, object)"/>
        public async Task<Message> UpdateMessageAsync(IFormatProvider provider, string format, object arg0, object arg1, object arg2) =>
            await UpdateMessageAsync(string.Format(provider, format, arg0, arg1, arg2)).ConfigureAwait(false);
        /// <inheritdoc cref="Message.UpdateMessageAsync(IFormatProvider, string, object[])"/>
        public async Task<Message> UpdateMessageAsync(IFormatProvider provider, string format, params object[] args) =>
            await UpdateMessageAsync(string.Format(provider, format, args)).ConfigureAwait(false);
        /// <inheritdoc cref="Message.UpdateMessageAsync(object)"/>
        public async Task<Message> UpdateMessageAsync(object content) =>
            await UpdateMessageAsync(content).ConfigureAwait(false);
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