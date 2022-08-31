using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base.Content;
using Guilded.Base.Embeds;
using Guilded.Base.Servers;
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
public abstract class MessageEvent<T> : ContentModel where T
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
    /// Gets the identifier of <see cref="Server">the server</see> where the event occurred.
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
    /// <param name="serverId">The identifier of <see cref="Server">the server</see> where the message event occurred</param>
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

    /// <inheritdoc cref="Message.Mentions" />
    public Mentions? Mentions => Message.Mentions;

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
    /// <param name="serverId">The identifier of <see cref="Server">the server</see> where the message event occurred</param>
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

    #region Methods

    #region Method CreateMessageAsync
    /// <inheritdoc cref="Message.CreateMessageAsync(MessageContent)" />
    public Task<Message> CreateMessageAsync(MessageContent message) =>
        Message.CreateMessageAsync(message);

    /// <inheritdoc cref="Message.CreateMessageAsync(string)" />
    public Task<Message> CreateMessageAsync(string content) =>
        Message.CreateMessageAsync(content);

    /// <inheritdoc cref="Message.CreateMessageAsync(string, bool, bool)" />
    public Task<Message> CreateMessageAsync(string content, bool isPrivate = false, bool isSilent = false) =>
        Message.CreateMessageAsync(content, isPrivate, isSilent);

    /// <inheritdoc cref="Message.CreateMessageAsync(string, Guid[])" />
    public Task<Message> CreateMessageAsync(string content, params Guid[] replyMessageIds) =>
        Message.CreateMessageAsync(content, replyMessageIds);

    /// <inheritdoc cref="Message.CreateMessageAsync(string, bool, bool, Guid[])" />
    public Task<Message> CreateMessageAsync(string content, bool isPrivate = false, bool isSilent = false, params Guid[] replyTo) =>
        Message.CreateMessageAsync(content, isPrivate, isSilent, replyTo);

    /// <inheritdoc cref="Message.CreateMessageAsync(Embed[])" />
    public Task<Message> CreateMessageAsync(params Embed[] embeds) =>
        Message.CreateMessageAsync(embeds);

    /// <inheritdoc cref="Message.CreateMessageAsync(bool, bool, Guid[], Embed[])" />
    public Task<Message> CreateMessageAsync(bool isPrivate = false, bool isSilent = false, Guid[]? replyTo = null, params Embed[] embeds) =>
        Message.CreateMessageAsync(isPrivate, isSilent, replyTo, embeds);

    /// <inheritdoc cref="Message.CreateMessageAsync(string, Embed[])" />
    public Task<Message> CreateMessageAsync(string content, params Embed[] embeds) =>
        Message.CreateMessageAsync(content, embeds);

    /// <inheritdoc cref="Message.CreateMessageAsync(string, bool, bool, Guid[], Embed[])" />
    public Task<Message> CreateMessageAsync(string content, bool isPrivate = false, bool isSilent = false, Guid[]? replyTo = null, params Embed[] embeds) =>
        Message.CreateMessageAsync(content, isPrivate, isSilent, replyTo, embeds);
    #endregion

    #region Method ReplyAsync
    /// <inheritdoc cref="Message.ReplyAsync(string)" />
    public Task<Message> ReplyAsync(string content) =>
        Message.ReplyAsync(content);

    /// <inheritdoc cref="Message.ReplyAsync(string, bool, bool)" />
    public Task<Message> ReplyAsync(string content, bool isPrivate = false, bool isSilent = false) =>
        Message.ReplyAsync(content, isPrivate, isSilent);

    /// <inheritdoc cref="Message.ReplyAsync(bool, bool, Embed[])" />
    public Task<Message> ReplyAsync(bool isPrivate = false, bool isSilent = false, params Embed[] embeds) =>
        Message.ReplyAsync(isPrivate, isSilent, embeds);

    /// <inheritdoc cref="Message.ReplyAsync(Embed[])" />
    public Task<Message> ReplyAsync(params Embed[] embeds) =>
        Message.ReplyAsync(embeds);

    /// <inheritdoc cref="Message.ReplyAsync(string, bool, bool, Embed[])" />
    public Task<Message> ReplyAsync(string content, bool isPrivate = false, bool isSilent = false, params Embed[] embeds) =>
        Message.ReplyAsync(content, isPrivate, isSilent, embeds);
    #endregion

    /// <inheritdoc cref="Message.UpdateAsync(string)" />
    public Task<Message> UpdateAsync(string content) =>
        Message.UpdateAsync(content);

    /// <inheritdoc cref="Message.DeleteAsync" />
    public Task DeleteAsync() =>
        Message.DeleteAsync();

    /// <inheritdoc cref="Message.AddReactionAsync(uint)" />
    public Task AddReactionAsync(uint emoteId) =>
        Message.AddReactionAsync(emoteId);

    /// <inheritdoc cref="Message.RemoveReactionAsync(uint)" />
    public Task RemoveReactionAsync(uint emoteId) =>
        Message.RemoveReactionAsync(emoteId);
    #endregion
}