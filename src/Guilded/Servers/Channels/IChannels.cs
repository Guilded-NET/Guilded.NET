using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Base.Embeds;
using Guilded.Client;
using Guilded.Content;

namespace Guilded.Servers;

/// <summary>
/// Represents a navigatable item that contains content.
/// </summary>
public interface IChannel : IHasParentClient
{
    /// <summary>
    /// Gets the identifier of the <see cref="ServerChannel">channel</see>.
    /// </summary>
    /// <value><see cref="Id">Channel ID</see></value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="ServerChannel.ParentId" />
    /// <seealso cref="ServerChannel.CategoryId" />
    /// <seealso cref="ServerChannel.GroupId" />
    /// <seealso cref="ServerChannel.ServerId" />
    public Guid Id { get; }
}

/// <summary>
/// Represents methods for <see cref="ServerChannel">channels</see> of <see cref="ChannelType.Chat">chat</see> type.
/// </summary>
public interface IChatChannel : IChannel
{
    /// <inheritdoc cref="AbstractGuildedClient.GetMessagesAsync(Guid, bool, uint?, DateTime?, DateTime?)" />
    /// <param name="includePrivate">Whether to get private replies or not</param>
    /// <param name="limit">The limit of how many messages to get (default — <c>50</c>, min — <c>1</c>, max — <c>100</c>)</param>
    /// <param name="before">The max limit of the creation date of fetched messages</param>
    /// <param name="after">The min limit of the creation date of fetched messages</param>
    public Task<IList<Message>> GetMessagesAsync(bool includePrivate = false, uint? limit = null, DateTime? before = null, DateTime? after = null) =>
        ParentClient.GetMessagesAsync(Id, includePrivate, limit, before, after);

    /// <inheritdoc cref="AbstractGuildedClient.GetMessageAsync(Guid, Guid)" />
    /// <param name="message">The identifier of the message it should get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    public Task<Message> GetMessageAsync(Guid message) =>
        ParentClient.GetMessageAsync(Id, message);

    /// <inheritdoc cref="AbstractGuildedClient.CreateMessageAsync(Guid, MessageContent)" />
    /// <param name="message">The message to send</param>
    public Task<Message> CreateMessageAsync(MessageContent message) =>
        ParentClient.CreateMessageAsync(Id, message);

    /// <inheritdoc cref="AbstractGuildedClient.CreateMessageAsync(Guid, string?, IList{Embed}?, IList{Guid}?, bool, bool)" />
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="embeds">The list of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    /// <param name="replyTo">The list of all <see cref="Message">messages</see> it is replying to (max — <c>5</c>)</param>
    /// <param name="isPrivate">Whether the mention is private</param>
    /// <param name="isSilent">Whether the mention is silent and does not ping anyone</param>
    public Task<Message> CreateMessageAsync(string? content = null, IList<Embed>? embeds = null, IList<Guid>? replyTo = null, bool isPrivate = false, bool isSilent = false) =>
        CreateMessageAsync(new MessageContent(content, embeds, replyTo, isPrivate, isSilent));

    /// <inheritdoc cref="AbstractGuildedClient.CreateMessageAsync(Guid, string?, IList{Embed}?, IList{Guid}?, bool, bool)" />
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="replyTo">The list of all <see cref="Message">messages</see> it is replying to (max — <c>5</c>)</param>
    /// <param name="isPrivate">Whether the mention is private</param>
    /// <param name="isSilent">Whether the mention is silent and does not ping anyone</param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task<Message> CreateMessageAsync(string? content = null, IList<Guid>? replyTo = null, bool isPrivate = false, bool isSilent = false, params Embed[] embeds) =>
        CreateMessageAsync(new MessageContent(content, embeds, replyTo, isPrivate, isSilent));

    /// <inheritdoc cref="AbstractGuildedClient.CreateMessageAsync(Guid, string?, IList{Embed}?, IList{Guid}?, bool, bool)" />
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="embeds">The list of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    /// <param name="isPrivate">Whether the mention is private</param>
    /// <param name="isSilent">Whether the mention is silent and does not ping anyone</param>
    /// <param name="replyTo">The array of all <see cref="Message">messages</see> it is replying to (max — <c>5</c>)</param>
    public Task<Message> CreateMessageAsync(string? content = null, IList<Embed>? embeds = null, bool isPrivate = false, bool isSilent = false, params Guid[] replyTo) =>
        CreateMessageAsync(new MessageContent(content, embeds, replyTo, isPrivate, isSilent));

    /// <inheritdoc cref="AbstractGuildedClient.CreateMessageAsync(Guid, string?, IList{Embed}?, IList{Guid}?, bool, bool)" />
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="embeds">The list of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task<Message> CreateMessageAsync(string content, params Embed[] embeds) =>
        CreateMessageAsync(new MessageContent(content, embeds));

    /// <inheritdoc cref="AbstractGuildedClient.CreateMessageAsync(Guid, string?, IList{Embed}?, IList{Guid}?, bool, bool)" />
    /// <param name="embeds">The list of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task<Message> CreateMessageAsync(params Embed[] embeds) =>
        CreateMessageAsync(new MessageContent(embeds));

    /// <inheritdoc cref="AbstractGuildedClient.UpdateMessageAsync(Guid, Guid, MessageContent)" />
    /// <param name="message">The identifier of the <see cref="Message">message</see> to edit</param>
    /// <param name="content">The <see cref="MessageContent">new contents</see> of the <see cref="Message">message</see></param>
    public Task<Message> UpdateMessageAsync(Guid message, MessageContent content) =>
        ParentClient.UpdateMessageAsync(Id, message, content);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateMessageAsync(Guid, Guid, MessageContent)" />
    /// <param name="message">The identifier of the <see cref="Message">message</see> to edit</param>
    /// <param name="content">The new <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="embeds">The new <see cref="Embed">custom embeds</see> of the <see cref="Message">message</see> in Markdown (max — <c>1</c>)</param>
    public Task<Message> UpdateMessageAsync(Guid message, string? content = null, IList<Embed>? embeds = null) =>
        UpdateMessageAsync(message, new MessageContent(content, embeds));

    /// <inheritdoc cref="AbstractGuildedClient.UpdateMessageAsync(Guid, Guid, MessageContent)" />
    /// <param name="message">The identifier of the <see cref="Message">message</see> to edit</param>
    /// <param name="content">The new <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="embeds">The new <see cref="Embed">custom embeds</see> of the <see cref="Message">message</see> in Markdown (max — <c>1</c>)</param>
    public Task<Message> UpdateMessageAsync(Guid message, string? content = null, params Embed[] embeds) =>
        UpdateMessageAsync(message, new MessageContent(content, embeds));

    /// <inheritdoc cref="AbstractGuildedClient.UpdateMessageAsync(Guid, Guid, MessageContent)" />
    /// <param name="message">The identifier of the <see cref="Message">message</see> to edit</param>
    /// <param name="embeds">The new <see cref="Embed">custom embeds</see> of the <see cref="Message">message</see> in Markdown (max — <c>1</c>)</param>
    public Task<Message> UpdateMessageAsync(Guid message, params Embed[] embeds) =>
        UpdateMessageAsync(message, new MessageContent(null, embeds));

    /// <inheritdoc cref="AbstractGuildedClient.DeleteMessageAsync(Guid, Guid)" />
    /// <param name="message">The identifier of the <see cref="Message">message</see> to delete</param>
    public Task DeleteMessageAsync(Guid message) =>
        ParentClient.DeleteMessageAsync(Id, message);

    /// <inheritdoc cref="AbstractGuildedClient.AddReactionAsync(Guid, Guid, uint)" />
    /// <param name="message">The identifier of the <see cref="Message">message</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to add</param>
    public Task AddReactionAsync(Guid message, uint emote) =>
        ParentClient.AddReactionAsync(Id, message, emote);

    /// <inheritdoc cref="AbstractGuildedClient.AddReactionAsync(Guid, Guid, uint)" />
    /// <param name="message">The identifier of the <see cref="Message">message</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to add</param>
    public Task AddReactionAsync(Guid message, Emote emote) =>
        AddReactionAsync(message, emote.Id);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveReactionAsync(Guid, Guid, uint)" />
    /// <param name="message">The identifier of the <see cref="Message">message</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to remove</param>
    public Task RemoveReactionAsync(Guid message, uint emote) =>
        ParentClient.RemoveReactionAsync(Id, message, emote);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveReactionAsync(Guid, Guid, uint)" />
    /// <param name="message">The identifier of the <see cref="Message">message</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to remove</param>
    public Task RemoveReactionAsync(Guid message, Emote emote) =>
        RemoveReactionAsync(message, emote.Id);
}

/// <summary>
/// Represents methods for <see cref="ServerChannel">channels</see> for common <see cref="ChannelType">channel types</see>.
/// </summary>
public interface IContentChannel : IChannel, IHasParentClient
{
    /// <inheritdoc cref="AbstractGuildedClient.AddReactionAsync(Guid, uint, uint)" />
    /// <param name="content">The identifier of the <see cref="ChannelContent{TId, TServer}">content</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to add</param>
    public Task AddReactionAsync(uint content, uint emote) =>
        ParentClient.AddReactionAsync(Id, content, emote);

    /// <inheritdoc cref="AbstractGuildedClient.AddReactionAsync(Guid, uint, uint)" />
    /// <param name="content">The identifier of the <see cref="ChannelContent{TId, TServer}">content</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to add</param>
    public Task AddReactionAsync(uint content, Emote emote) =>
        AddReactionAsync(content, emote.Id);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveReactionAsync(Guid, uint, uint)" />
    /// <param name="content">The identifier of the <see cref="ChannelContent{TId, TServer}">content</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to remove</param>
    public Task RemoveReactionAsync(uint content, uint emote) =>
        ParentClient.RemoveReactionAsync(Id, content, emote);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveReactionAsync(Guid, uint, uint)" />
    /// <param name="content">The identifier of the <see cref="ChannelContent{TId, TServer}">content</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to remove</param>
    public Task RemoveReactionAsync(uint content, Emote emote) =>
        RemoveReactionAsync(content, emote.Id);
}