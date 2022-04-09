using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base.Permissions;
using Newtonsoft.Json;

namespace Guilded.Base.Content;

/// <summary>
/// A message posted in the chat.
/// </summary>
/// <remarks>
/// <para>An existing/a cached message that can be found in a chat. This can be found in chat channels, voice channels, stream channels and their equivalent threads.</para>
/// <para>This currently includes both messages of types <see cref="MessageType.Default"/> and <see cref="MessageType.System"/>, but it could be changed in the future.</para>
/// </remarks>
/// <seealso cref="ListItem{T}"/>
/// <seealso cref="ForumThread"/>
public class Message : ChannelContent<Guid, HashId?>, IUpdatableContent, IWebhookCreatable, IReactibleContent
{
    #region Static
    /// <summary>
    /// The limit of the message content
    /// </summary>
    /// <remarks>
    /// <para>The count of how many max characters there can be in a message.</para>
    /// </remarks>
    /// <value>4000</value>
    public const int ContentLimit = 4000;
    #endregion

    #region JSON properties

    #region Content
    /// <summary>
    /// The contents of the message.
    /// </summary>
    /// <remarks>
    /// <para>The contents of the message in Markdown format.</para>
    /// <para>This includes images and videos, which are in the format of <c>![](source_url)</c>.</para>
    /// </remarks>
    /// <value>Markdown string</value>
    public string Content { get; }
    /// <summary>
    /// The list of messages being replied to.
    /// </summary>
    /// <remarks>
    /// <para>Specifies which messages were replied to in this message. The max reply limit is 5.</para>
    /// </remarks>
    /// <value>List of message IDs?</value>
    public IList<Guid>? ReplyMessageIds { get; }
    /// <summary>
    /// Whether the reply is private.
    /// </summary>
    /// <remarks>
    /// <para>Specifies whether the reply is private or not.</para>
    /// <para>This can only be <see langword="true"/> if <see cref="ReplyMessageIds"/> has a value.</para>
    /// </remarks>
    /// <value>Reply is private</value>
    public bool IsPrivate { get; }
    /// <summary>
    /// Whether the specified message is a reply
    /// </summary>
    /// <remarks>
    /// <para>Checks whether the message is a reply.</para>
    /// </remarks>
    /// <value>Message is a reply</value>
    [JsonIgnore]
    public bool IsReply => ReplyMessageIds?.Count > 0;
    #endregion

    /// <summary>
    /// The identifier of the webhook creator of the message.
    /// </summary>
    /// <remarks>
    /// <para>The identifier of the webhook that created this message.</para>
    /// </remarks>
    /// <value>Webhook ID?</value>
    public Guid? CreatedByWebhook { get; }
    /// <summary>
    /// The date of when the message was updated.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="DateTime"/> of when the message was updated/edited. Only returns the most recent update.</para>
    /// </remarks>
    /// <value>Updated at?</value>
    public DateTime? UpdatedAt { get; }
    /// <summary>
    /// The type of the message.
    /// </summary>
    /// <remarks>
    /// <para>Allows message to be determined as a <see cref="MessageType.Default"/> or <see cref="MessageType.System"/>.</para>
    /// </remarks>
    /// <value>Message type</value>
    public MessageType Type { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Creates a new instance of <see cref="Message"/> with provided details.
    /// </summary>
    /// <param name="id">The identifier of the message</param>
    /// <param name="channelId">The identifier of the channel where the message is</param>
    /// <param name="serverId">The identifier of the server where the message is</param>
    /// <param name="content">The contents of the message</param>
    /// <param name="replyMessageIds">The list of messages being replied to</param>
    /// <param name="isPrivate">Whether the reply is private</param>
    /// <param name="createdBy">The identifier of the user creator of the message</param>
    /// <param name="createdByWebhookId">The identifier of the webhook creator of the message</param>
    /// <param name="createdAt">The date of when the message was created</param>
    /// <param name="updatedAt">The date of when the message was updated</param>
    /// <param name="type">The type of the message</param>
    [JsonConstructor]
    public Message(
        [JsonProperty(Required = Required.Always)]
        Guid id,

        [JsonProperty(Required = Required.Always)]
        Guid channelId,

        [JsonProperty]
        HashId? serverId,

        [JsonProperty(Required = Required.Always)]
        string content,

        [JsonProperty]
        IList<Guid>? replyMessageIds,

        [JsonProperty]
        bool isPrivate,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty]
        Guid? createdByWebhookId,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty]
        DateTime? updatedAt,

        [JsonProperty(Required = Required.Always)]
        MessageType type
    ) : base(id, channelId, serverId, createdBy, createdAt) =>
        (Content, ReplyMessageIds, IsPrivate, CreatedByWebhook, UpdatedAt, Type) = (content, replyMessageIds, isPrivate, createdByWebhookId, updatedAt, type);
    #endregion

    #region Additional
    /// <summary>
    /// Creates a message in a chat.
    /// </summary>
    /// <remarks>
    /// <para>Creates a new message in the channel of identifier <see cref="ChannelContent{T, S}.ChannelId"/> where the message is.</para>
    /// <para>This does not automatically include the message in the reply list.</para>
    /// </remarks>
    /// <param name="content">The contents of the message in Markdown plain text</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedRequestException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <exception cref="ArgumentNullException">When the <paramref name="content"/> only consists of whitespace or is <see langword="null"/></exception>
    /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="content"/> is above the message limit of 4000 characters</exception>
    /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
    /// <permission cref="ChatPermissions.SendMessages">Required for sending a message in a channel</permission>
    /// <permission cref="ChatPermissions.SendThreadMessages">Required for sending a message in a thread</permission>
    /// <returns>Message created</returns>
    public async Task<Message> CreateMessageAsync(string content) =>
        await ParentClient.CreateMessageAsync(ChannelId, content).ConfigureAwait(false);
    /// <inheritdoc cref="CreateMessageAsync(string)"/>
    /// <param name="content">The contents of the message in Markdown plain text</param>
    /// <param name="replyMessageIds">The array of all messages it is replying to(5 max)</param>
    public async Task<Message> CreateMessageAsync(string content, params Guid[] replyMessageIds) =>
        await ParentClient.CreateMessageAsync(ChannelId, content, replyMessageIds).ConfigureAwait(false);
    /// <inheritdoc cref="CreateMessageAsync(string)"/>
    /// <param name="content">The contents of the message in Markdown plain text</param>
    /// <param name="isPrivate">Whether the reply is private</param>
    /// <param name="replyMessageIds">The array of all messages it is replying to(5 max)</param>
    public async Task<Message> CreateMessageAsync(string content, bool isPrivate, params Guid[] replyMessageIds) =>
        await ParentClient.CreateMessageAsync(ChannelId, content, isPrivate, replyMessageIds).ConfigureAwait(false);
    /// <summary>
    /// Replies to the message in the chat.
    /// </summary>
    /// <remarks>
    /// <para>Creates a new message in the channel of identifier <see cref="ChannelContent{T, S}.ChannelId"/> where the message is.</para>
    /// <para>Includes the message in the reply list.</para>
    /// </remarks>
    /// <param name="content">The contents of the message in Markdown plain text</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedRequestException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <exception cref="ArgumentNullException">When the <paramref name="content"/> only consists of whitespace or is <see langword="null"/></exception>
    /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="content"/> is above the message limit of 4000 characters</exception>
    /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
    /// <permission cref="ChatPermissions.SendMessages">Required for sending a message in a channel</permission>
    /// <permission cref="ChatPermissions.SendThreadMessages">Required for sending a message in a thread</permission>
    /// <returns>Message created</returns>
    public async Task<Message> ReplyAsync(string content) =>
        await CreateMessageAsync(content, Id).ConfigureAwait(false);
    /// <inheritdoc cref="ReplyAsync(string)"/>
    /// <param name="content">The contents of the message in Markdown plain text</param>
    /// <param name="isPrivate">Whether the reply is private</param>
    public async Task<Message> ReplyAsync(string content, bool isPrivate) =>
        await CreateMessageAsync(content, isPrivate, Id).ConfigureAwait(false);
    /// <inheritdoc cref="BaseGuildedClient.UpdateMessageAsync(Guid, Guid, string)"/>
    /// <param name="content">The contents of the message in Markdown plain text</param>
    public async Task<Message> UpdateAsync(string content) =>
        await ParentClient.UpdateMessageAsync(ChannelId, Id, content).ConfigureAwait(false);
    /// <inheritdoc cref="BaseGuildedClient.DeleteMessageAsync(Guid, Guid)"/>
    public async Task DeleteAsync() =>
        await ParentClient.DeleteMessageAsync(ChannelId, Id).ConfigureAwait(false);
    /// <inheritdoc cref="BaseGuildedClient.AddReactionAsync(Guid, Guid, uint)"/>
    /// <param name="emoteId">The identifier of the emote to add</param>
    public async Task<Reaction> AddReactionAsync(uint emoteId) =>
        await ParentClient.AddReactionAsync(ChannelId, Id, emoteId).ConfigureAwait(false);
    /// <inheritdoc cref="BaseGuildedClient.RemoveReactionAsync(Guid, Guid, uint)"/>
    /// <param name="emoteId">The identifier of the emote to remove</param>
    public async Task RemoveReactionAsync(uint emoteId) =>
        await ParentClient.RemoveReactionAsync(ChannelId, Id, emoteId).ConfigureAwait(false);
    #endregion
}
