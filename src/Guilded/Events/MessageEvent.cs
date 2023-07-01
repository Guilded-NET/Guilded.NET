using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Base.Embeds;
using Guilded.Content;
using Guilded.Servers;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents the base for message-related events.
/// </summary>
/// <seealso cref="Content.Message" />
/// <seealso cref="MessageDeletedEvent" />
/// <seealso cref="ItemEvent" />
/// <seealso cref="DocEvent" />
/// <seealso cref="ChannelEvent" />
public abstract class MessageEvent<T> : ContentModel, IGlobalContent, IChannelBased, IPrivatableContent, IModelHasId<Guid> where T : IModelHasId<Guid>, IGlobalContent, IChannelBased, IPrivatableContent
{
    #region Properties
    /// <summary>
    /// Gets the <see cref="Content.Message">message</see> received from the event.
    /// </summary>
    /// <value>The <see cref="Content.Message">message</see> received from the event</value>
    /// <seealso cref="MessageEvent" />
    /// <seealso cref="MessageEvent{T}" />
    /// <seealso cref="ServerId" />
    public T Message { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="Server">server</see> where the event occurred.
    /// </summary>
    /// <value>The identifier of the <see cref="Server">server</see> where the event occurred</value>
    /// <seealso cref="MessageEvent" />
    /// <seealso cref="MessageEvent{T}" />
    /// <seealso cref="Message" />
    public HashId? ServerId { get; }

    /// <inheritdoc cref="ChannelContent{TId, TServer}.Id" />
    public Guid Id => Message.Id;

    /// <inheritdoc cref="ChannelContent{T, S}.ChannelId" />
    public Guid ChannelId => Message.ChannelId;

    /// <inheritdoc cref="Message.IsPrivate" />
    public bool IsPrivate => Message.IsPrivate;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="MessageEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the message event occurred</param>
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
/// Represents an event that occurs when someone creates or edits a <see cref="Message">message</see>.
/// </summary>
/// <seealso cref="MessageDeletedEvent" />
/// <seealso cref="Message" />
public class MessageEvent : MessageEvent<Message>, IReactibleContent
{
    #region Properties
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

    /// <inheritdoc cref="Message.IsSilent" />
    public bool IsSilent => Message.IsSilent;

    /// <inheritdoc cref="Message.IsSystemMessage" />
    public bool IsSystemMessage => Message.IsSystemMessage;
    #endregion

    #region Properties Events
    /// <inheritdoc cref="Message.Replied" />
    public IObservable<MessageEvent> Replied =>
        Message.Replied;

    /// <inheritdoc cref="Message.Updated" />
    public IObservable<MessageEvent> Updated =>
        Message.Updated;

    /// <inheritdoc cref="Message.Deleted" />
    public IObservable<MessageDeletedEvent> Deleted =>
        Message.Deleted;

    /// <inheritdoc cref="Message.ReactionAdded" />
    public IObservable<MessageReactionEvent> ReactionAdded =>
        Message.ReactionAdded;

    /// <inheritdoc cref="Message.ReactionRemoved" />
    public IObservable<MessageReactionEvent> ReactionRemoved =>
        Message.ReactionRemoved;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="MessageEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the message event occurred</param>
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

    #region Method CreateMessageAsync
    /// <inheritdoc cref="Message.CreateMessageAsync(MessageContent)" />
    public Task<Message> CreateMessageAsync(MessageContent message) =>
        Message.CreateMessageAsync(message);

    /// <inheritdoc cref="Message.CreateMessageAsync(string, IList{Embed}, IList{Guid}, bool, bool)" />
    public Task<Message> CreateMessageAsync(string? content = null, IList<Embed>? embeds = null, IList<Guid>? replyTo = null, bool isPrivate = false, bool isSilent = false) =>
        Message.CreateMessageAsync(content, embeds, replyTo, isPrivate, isSilent);

    /// <inheritdoc cref="Message.CreateMessageAsync(string, IList{Guid}, bool, bool, Embed[])" />
    public Task<Message> CreateMessageAsync(string? content = null, IList<Guid>? replyTo = null, bool isPrivate = false, bool isSilent = false, params Embed[] embeds) =>
        Message.CreateMessageAsync(content, replyTo, isPrivate, isSilent, embeds);

    /// <inheritdoc cref="Message.CreateMessageAsync(string, IList{Embed}, bool, bool, Guid[])" />
    public Task<Message> CreateMessageAsync(string? content = null, IList<Embed>? embeds = null, bool isPrivate = false, bool isSilent = false, params Guid[] replyTo) =>
        Message.CreateMessageAsync(content, embeds, replyTo, isPrivate, isSilent);

    /// <inheritdoc cref="Message.CreateMessageAsync(string, Embed[])" />
    public Task<Message> CreateMessageAsync(string content, params Embed[] embeds) =>
        Message.CreateMessageAsync(content, embeds);

    /// <inheritdoc cref="Message.CreateMessageAsync(Embed[])" />
    public Task<Message> CreateMessageAsync(params Embed[] embeds) =>
        Message.CreateMessageAsync(embeds);
    #endregion

    #region Method ReplyAsync
    /// <inheritdoc cref="Message.ReplyAsync(string, IList{Embed}, bool, bool)" />
    public Task<Message> ReplyAsync(string? content = null, IList<Embed>? embeds = null, bool isPrivate = false, bool isSilent = false) =>
        Message.ReplyAsync(content, embeds, isPrivate, isSilent);

    /// <inheritdoc cref="Message.ReplyAsync(string, IList{Embed}, bool, bool)" />
    public Task<Message> ReplyAsync(string? content = null, bool isPrivate = false, bool isSilent = false, params Embed[] embeds) =>
        Message.ReplyAsync(content, isPrivate, isSilent, embeds);

    /// <inheritdoc cref="Message.ReplyAsync(string, Embed[])" />
    public Task<Message> ReplyAsync(string content, params Embed[] embeds) =>
        Message.ReplyAsync(content, embeds);

    /// <inheritdoc cref="Message.ReplyAsync(Embed[])" />
    public Task<Message> ReplyAsync(params Embed[] embeds) =>
        Message.ReplyAsync(embeds);
    #endregion

    #region Methods Threads
    /// <inheritdoc cref="Message.CreateThreadAsync(string)" />
    /// <param name="name">The name of the <see cref="ServerChannel">thread</see></param>
    public Task<ChatChannel> CreateThreadAsync(string name) =>
        Message.CreateThreadAsync(name);
    #endregion

    #region Methods
    /// <inheritdoc cref="Message.UpdateAsync(string, IList{Embed})" />
    public Task<Message> UpdateAsync(string? content = null, IList<Embed>? embeds = null) =>
        Message.UpdateAsync(content, embeds);

    /// <inheritdoc cref="Message.UpdateAsync(string, Embed[])" />
    public Task<Message> UpdateAsync(string? content = null, params Embed[] embeds) =>
        Message.UpdateAsync(content, embeds);

    /// <inheritdoc cref="Message.UpdateAsync(Embed[])" />
    public Task<Message> UpdateAsync(params Embed[] embeds) =>
        Message.UpdateAsync(embeds);

    /// <inheritdoc cref="Message.DeleteAsync" />
    public Task DeleteAsync() =>
        Message.DeleteAsync();

    /// <inheritdoc cref="Message.AddReactionAsync(uint)" />
    public Task AddReactionAsync(uint emote) =>
        Message.AddReactionAsync(emote);

    /// <inheritdoc cref="Message.RemoveReactionAsync(uint)" />
    public Task RemoveReactionAsync(uint emote) =>
        Message.RemoveReactionAsync(emote);
    #endregion
}