using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base.Content;
using Guilded.Base.Embeds;
using Newtonsoft.Json;

namespace Guilded.Base.Events;

/// <summary>
/// Represents the base for message-related events.
/// </summary>
/// <seealso cref="Content.Message" />
/// <seealso cref="MessageDeletedEvent" />
/// <seealso cref="ListItemEvent" />
/// <seealso cref="DocEvent" />
/// <seealso cref="ChannelEvent" />
public abstract class MessageEvent<T> : BaseModel where T : BaseModel
{
    #region Properties
    /// <summary>
    /// Gets the message received from the event.
    /// </summary>
    /// <value>Message</value>
    /// <seealso cref="MessageEvent" />
    /// <seealso cref="MessageEvent{T}" />
    /// <seealso cref="ServerId" />
    public T Message { get; }

    /// <summary>
    /// Gets the identifier of the server where the event occurred.
    /// </summary>
    /// <value>Server ID?</value>
    /// <seealso cref="MessageEvent" />
    /// <seealso cref="MessageEvent{T}" />
    /// <seealso cref="Message" />
    public HashId? ServerId { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="MessageEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the server where the message event occurred</param>
    /// <param name="message">The message received from the event</param>
    /// <returns>New <see cref="MessageEvent{T}" /> JSON instance</returns>
    /// <seealso cref="MessageEvent{T}" />
    protected MessageEvent(
        HashId? serverId,

        T message
    ) =>
        (ServerId, Message) = (serverId, message);
    #endregion
}

/// <summary>
/// Represents an event that occurs when someone creates or edits <see cref="Message">a message</see>.
/// </summary>
/// <seealso cref="MessageDeletedEvent" />
/// <seealso cref="Message" />
public class MessageEvent : MessageEvent<Message>
{
    #region Properties
    /// <inheritdoc cref="ChannelContent{T, S}.ChannelId" />
    public Guid ChannelId => Message.ChannelId;

    /// <inheritdoc cref="Message.Content" />
    public string? Content => Message.Content;

    /// <inheritdoc cref="Message.ReplyMessageIds" />
    public IList<Guid>? ReplyMessageIds => Message.ReplyMessageIds;

    /// <inheritdoc cref="Message.Embeds" />
    public IList<Embed>? Embeds => Message.Embeds;

    /// <inheritdoc cref="ChannelContent{T, S}.CreatedBy" />
    public HashId CreatedBy => Message.CreatedBy;

    /// <inheritdoc cref="Message.CreatedByWebhook" />
    public Guid? CreatedByWebhook => Message.CreatedByWebhook;

    /// <inheritdoc cref="ChannelContent{T, S}.CreatedAt" />
    public DateTime CreatedAt => Message.CreatedAt;

    /// <inheritdoc cref="Message.UpdatedAt" />
    public DateTime? UpdatedAt => Message.UpdatedAt;

    /// <inheritdoc cref="Message.Type" />
    public MessageType Type => Message.Type;

    /// <inheritdoc cref="Message.IsReply" />
    public bool IsReply => Message.IsReply;

    /// <inheritdoc cref="Message.IsPrivate" />
    public bool IsPrivate => Message.IsPrivate;

    /// <inheritdoc cref="Message.IsSilent" />
    public bool IsSilent => Message.IsSilent;

    /// <inheritdoc cref="Message.IsSystemMessage" />
    public bool IsSystemMessage => Message.IsSystemMessage;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="MessageEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the server where the message event occurred</param>
    /// <param name="message">The message received from the event</param>
    /// <returns>New <see cref="MessageEvent" /> JSON instance</returns>
    /// <seealso cref="MessageEvent" />
    [JsonConstructor]
    public MessageEvent(
        [JsonProperty(Required = Required.Always)]
        Message message,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? serverId = null
    ) : base(serverId, message) { }
    #endregion

    #region Additional

    #region Method CreateMessageAsync
    /// <inheritdoc cref="Message.CreateMessageAsync(MessageContent)" />
    public async Task<Message> CreateMessageAsync(MessageContent message) =>
        await Message.CreateMessageAsync(message).ConfigureAwait(false);

    /// <inheritdoc cref="Message.CreateMessageAsync(string)" />
    public async Task<Message> CreateMessageAsync(string content) =>
        await Message.CreateMessageAsync(content).ConfigureAwait(false);

    /// <inheritdoc cref="Message.CreateMessageAsync(string, bool, bool)" />
    public async Task<Message> CreateMessageAsync(string content, bool isPrivate = false, bool isSilent = false) =>
        await Message.CreateMessageAsync(content, isPrivate, isSilent).ConfigureAwait(false);

    /// <inheritdoc cref="Message.CreateMessageAsync(string, Guid[])" />
    public async Task<Message> CreateMessageAsync(string content, params Guid[] replyMessageIds) =>
        await Message.CreateMessageAsync(content, replyMessageIds).ConfigureAwait(false);

    /// <inheritdoc cref="Message.CreateMessageAsync(string, bool, bool, Guid[])" />
    public async Task<Message> CreateMessageAsync(string content, bool isPrivate = false, bool isSilent = false, params Guid[] replyTo) =>
        await Message.CreateMessageAsync(content, isPrivate, isSilent, replyTo).ConfigureAwait(false);

    /// <inheritdoc cref="Message.CreateMessageAsync(Embed[])" />
    public async Task<Message> CreateMessageAsync(params Embed[] embeds) =>
        await Message.CreateMessageAsync(embeds).ConfigureAwait(false);

    /// <inheritdoc cref="Message.CreateMessageAsync(bool, bool, Guid[], Embed[])" />
    public async Task<Message> CreateMessageAsync(bool isPrivate = false, bool isSilent = false, Guid[]? replyTo = null, params Embed[] embeds) =>
        await Message.CreateMessageAsync(isPrivate, isSilent, replyTo, embeds).ConfigureAwait(false);

    /// <inheritdoc cref="Message.CreateMessageAsync(string, Embed[])" />
    public async Task<Message> CreateMessageAsync(string content, params Embed[] embeds) =>
        await Message.CreateMessageAsync(content, embeds).ConfigureAwait(false);

    /// <inheritdoc cref="Message.CreateMessageAsync(string, bool, bool, Guid[], Embed[])" />
    public async Task<Message> CreateMessageAsync(string content, bool isPrivate = false, bool isSilent = false, Guid[]? replyTo = null, params Embed[] embeds) =>
        await Message.CreateMessageAsync(content, isPrivate, isSilent, replyTo, embeds).ConfigureAwait(false);
    #endregion

    #region Method ReplyAsync
    /// <inheritdoc cref="Message.ReplyAsync(string)" />
    public async Task<Message> ReplyAsync(string content) =>
        await Message.ReplyAsync(content).ConfigureAwait(false);

    /// <inheritdoc cref="Message.ReplyAsync(string, bool, bool)" />
    public async Task<Message> ReplyAsync(string content, bool isPrivate = false, bool isSilent = false) =>
        await Message.ReplyAsync(content, isPrivate, isSilent).ConfigureAwait(false);

    /// <inheritdoc cref="Message.ReplyAsync(bool, bool, Embed[])" />
    public async Task<Message> ReplyAsync(bool isPrivate = false, bool isSilent = false, params Embed[] embeds) =>
        await Message.ReplyAsync(isPrivate, isSilent, embeds).ConfigureAwait(false);

    /// <inheritdoc cref="Message.ReplyAsync(Embed[])" />
    public async Task<Message> ReplyAsync(params Embed[] embeds) =>
        await Message.ReplyAsync(embeds).ConfigureAwait(false);

    /// <inheritdoc cref="Message.ReplyAsync(string, bool, bool, Embed[])" />
    public async Task<Message> ReplyAsync(string content, bool isPrivate = false, bool isSilent = false, params Embed[] embeds) =>
        await Message.ReplyAsync(content, isPrivate, isSilent, embeds).ConfigureAwait(false);
    #endregion

    /// <inheritdoc cref="Message.UpdateAsync(string)" />
    public async Task<Message> UpdateAsync(string content) =>
        await Message.UpdateAsync(content).ConfigureAwait(false);

    /// <inheritdoc cref="Message.DeleteAsync" />
    public async Task DeleteAsync() =>
        await Message.DeleteAsync().ConfigureAwait(false);

    /// <inheritdoc cref="Message.AddReactionAsync(uint)" />
    public async Task<Reaction> AddReactionAsync(uint emoteId) =>
        await Message.AddReactionAsync(emoteId).ConfigureAwait(false);

    // /// <inheritdoc cref="Message.RemoveReactionAsync(uint)" />
    // public async Task RemoveReactionAsync(uint emoteId) =>
    //     await Message.RemoveReactionAsync(emoteId).ConfigureAwait(false);
    #endregion
}