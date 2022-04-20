using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base.Permissions;
using Newtonsoft.Json;

namespace Guilded.Base.Content;

/// <summary>
/// Represents a message posted in a chat channel or alike.
/// </summary>
/// <remarks>
/// <para>Either an existing or a cached message. It can be found in chat, voice and stream channels, as well as threads with the same channel types as described.</para>
/// <para>This currently includes both messages of types <see cref="MessageType.Default"/> and <see cref="MessageType.System"/>, but it could be changed in the future.</para>
/// </remarks>
/// <seealso cref="Doc"/>
/// <seealso cref="ListItem{T}"/>
/// <seealso cref="ForumThread"/>
public class Message : ChannelContent<Guid, HashId?>, IUpdatableContent, IWebhookCreatable, IReactibleContent
{
    #region Static
    /// <summary>
    /// The count of how many characters there can be in <see cref="Message.Content" />.
    /// </summary>
    /// <value>Limit</value>
    public const int ContentLimit = 4000;
    #endregion

    #region JSON properties

    #region Content
    /// <summary>
    /// Gets the text contents of the message.
    /// </summary>
    /// <remarks>
    /// <para>The contents are formatted in Markdown. This includes images and videos, which are in the format of <c>![](source_url)</c>.</para>
    /// </remarks>
    /// <value>Markdown string</value>
    public string Content { get; }
    /// <summary>
    /// Gets the list of messages being replied to.
    /// </summary>
    /// <remarks>
    /// <para>The max reply limit is 5.</para>
    /// </remarks>
    /// <value>List of message IDs?</value>
    public IList<Guid>? ReplyMessageIds { get; }
    /// <summary>
    /// Gets whether the reply is private.
    /// </summary>
    /// <remarks>
    /// <para>This can only be <see langword="true"/> if <see cref="ReplyMessageIds"/> has a value or there is an user or role mention in the <see cref="Content" />.</para>
    /// </remarks>
    /// <value>Reply is private</value>
    public bool IsPrivate { get; }
    /// <summary>
    /// Gets whether the specified message is a reply
    /// </summary>
    /// <value>Message is a reply</value>
    public bool IsReply => ReplyMessageIds?.Count > 0;
    #endregion

    /// <summary>
    /// Gets the identifier of the webhook that created the message.
    /// </summary>
    /// <value>Webhook ID?</value>
    public Guid? CreatedByWebhook { get; }
    /// <summary>
    /// Gets the date of when the message was updated.
    /// </summary>
    /// <remarks>
    /// <para>Only returns the most recent update.</para>
    /// </remarks>
    /// <value>Updated at?</value>
    public DateTime? UpdatedAt { get; }
    /// <summary>
    /// Gets the type of the message.
    /// </summary>
    /// <remarks>
    /// <para>Distinguishes the messages by what content they contain.</para>
    /// </remarks>
    /// <value>Message type</value>
    public MessageType Type { get; }
    /// <summary>
    /// Gets whether the specified message is a system message.
    /// </summary>
    /// <remarks>
    /// <para>A system message is a message that is created automatically on specific events, such as renaming the channel. Usually, it's something like "User has renamed the channel from X to Y"</para>
    /// </remarks>
    /// <value>Message is a system message</value>
    public bool IsSystemMessage => Type == MessageType.System;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of <see cref="Message" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the message</param>
    /// <param name="channelId">The identifier of the channel where the message is</param>
    /// <param name="serverId">The identifier of the server where the message is</param>
    /// <param name="content">The text contents of the message</param>
    /// <param name="replyMessageIds">The list of messages being replied to</param>
    /// <param name="isPrivate">Whether the reply is private</param>
    /// <param name="createdBy">The identifier of the user that created the message</param>
    /// <param name="createdByWebhookId">The identifier of the webhook that created the message</param>
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
    /// Creates a message in the parent channel (from <see cref="ChannelContent{T, S}.ChannelId" />).
    /// </summary>
    /// <remarks>
    /// <para>The given text <paramref name="content" /> will be formatted in Markdown.</para>
    /// <para>This does not automatically include the message in the reply list.</para>
    /// </remarks>
    /// <param name="content">The text contents of the message in Markdown plain text</param>
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
    /// <returns>Created message</returns>
    public async Task<Message> CreateMessageAsync(string content) =>
        await ParentClient.CreateMessageAsync(ChannelId, content).ConfigureAwait(false);
    /// <inheritdoc cref="CreateMessageAsync(string)"/>
    /// <param name="content">The text contents of the message in Markdown plain text</param>
    /// <param name="isPrivate">Whether the mention is private</param>
    public async Task<Message> CreateMessageAsync(string content, bool isPrivate) =>
        await ParentClient.CreateMessageAsync(ChannelId, content, isPrivate).ConfigureAwait(false);
    /// <inheritdoc cref="CreateMessageAsync(string)"/>
    /// <param name="content">The text contents of the message in Markdown plain text</param>
    /// <param name="replyMessageIds">The array of all messages it is replying to(5 max)</param>
    public async Task<Message> CreateMessageAsync(string content, params Guid[] replyMessageIds) =>
        await ParentClient.CreateMessageAsync(ChannelId, content, replyMessageIds).ConfigureAwait(false);
    /// <inheritdoc cref="CreateMessageAsync(string)"/>
    /// <param name="content">The text contents of the message in Markdown plain text</param>
    /// <param name="isPrivate">Whether the reply is private</param>
    /// <param name="replyMessageIds">The array of all messages it is replying to(5 max)</param>
    public async Task<Message> CreateMessageAsync(string content, bool isPrivate, params Guid[] replyMessageIds) =>
        await ParentClient.CreateMessageAsync(ChannelId, content, isPrivate, replyMessageIds).ConfigureAwait(false);
    /// <summary>
    /// Replies to the message in the parent channel (from <see cref="ChannelContent{T, T}.ChannelId" />).
    /// </summary>
    /// <remarks>
    /// <para>The given text <paramref name="content" /> will be formatted in Markdown.</para>
    /// <para>Includes this message (<see cref="ChannelContent{T, S}.Id" /> property) in the reply list.</para>
    /// </remarks>
    /// <param name="content">The text contents of the message in Markdown plain text</param>
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
    /// <returns>Created message</returns>
    public async Task<Message> ReplyAsync(string content) =>
        await CreateMessageAsync(content, Id).ConfigureAwait(false);
    /// <inheritdoc cref="ReplyAsync(string)"/>
    /// <param name="content">The text contents of the message in Markdown plain text</param>
    /// <param name="isPrivate">Whether the reply is private</param>
    public async Task<Message> ReplyAsync(string content, bool isPrivate) =>
        await CreateMessageAsync(content, isPrivate, Id).ConfigureAwait(false);
    /// <inheritdoc cref="BaseGuildedClient.UpdateMessageAsync(Guid, Guid, string)"/>
    /// <param name="content">The text contents of the message in Markdown plain text</param>
    public async Task<Message> UpdateAsync(string content) =>
        await ParentClient.UpdateMessageAsync(ChannelId, Id, content).ConfigureAwait(false);
    /// <inheritdoc cref="BaseGuildedClient.DeleteMessageAsync(Guid, Guid)"/>
    public async Task DeleteAsync() =>
        await ParentClient.DeleteMessageAsync(ChannelId, Id).ConfigureAwait(false);
    /// <inheritdoc cref="BaseGuildedClient.AddReactionAsync(Guid, Guid, uint)"/>
    /// <param name="emoteId">The identifier of the emote to add</param>
    public async Task<Reaction> AddReactionAsync(uint emoteId) =>
        await ParentClient.AddReactionAsync(ChannelId, Id, emoteId).ConfigureAwait(false);
    // /// <inheritdoc cref="BaseGuildedClient.RemoveReactionAsync(Guid, Guid, uint)"/>
    // /// <param name="emoteId">The identifier of the emote to remove</param>
    // public async Task RemoveReactionAsync(uint emoteId) =>
    //     await ParentClient.RemoveReactionAsync(ChannelId, Id, emoteId).ConfigureAwait(false);
    #endregion
}
