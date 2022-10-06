using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base.Client;
using Guilded.Base.Embeds;
using Guilded.Base.Permissions;
using Guilded.Base.Servers;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Content;

/// <summary>
/// Represents a message posted in <see cref="ChannelType.Chat">a document channel</see> or alike.
/// </summary>
/// <remarks>
/// <para>Either an existing or a cached message. It can be found in chat, voice and stream channels, as well as threads with the same channel types as described.</para>
/// <para>This currently includes both messages of types <see cref="MessageType.Default" /> and <see cref="MessageType.System" />, but it could be changed in the future.</para>
/// </remarks>
/// <seealso cref="MessageContent" />
/// <seealso cref="MessageType" />
/// <seealso cref="Doc" />
/// <seealso cref="ListItem" />
/// <seealso cref="Topic" />
public class Message :
    ChannelContent<Guid, HashId?>, IUpdatableContent, IWebhookCreatable, IReactibleContent,
    IContentBlockMarkdown, IGlobalContent
{
    #region Constants
    /// <summary>
    /// The count of how many <see cref="char">characters</see> there can be in <see cref="Content">message's content</see>.
    /// </summary>
    /// <value>Limit</value>
    /// <seealso cref="Message" />
    /// <seealso cref="Content" />
    /// <seealso cref="ReplyLimit" />
    /// <seealso cref="EmbedLimit" />
    public const short TextLimit = 4000;

    /// <summary>
    /// The count of how many <see cref="Embeds">embeds</see> there can be in <see cref="Message">a message</see>.
    /// </summary>
    /// <value>Limit</value>
    /// <seealso cref="Message" />
    /// <seealso cref="Embeds" />
    /// <seealso cref="ReplyLimit" />
    /// <seealso cref="TextLimit" />
    public const short EmbedLimit = 1;

    /// <summary>
    /// The count of how many <see cref="Message">messages</see> can be replied to per <see cref="Message">message</see>.
    /// </summary>
    /// <value>Limit</value>
    /// <seealso cref="Message" />
    /// <seealso cref="ReplyMessageIds" />
    /// <seealso cref="EmbedLimit" />
    /// <seealso cref="TextLimit" />
    public const short ReplyLimit = 5;
    #endregion

    #region Properties

    #region Content
    /// <summary>
    /// Gets the text contents of <see cref="Message">the message</see>.
    /// </summary>
    /// <remarks>
    /// <para>The contents are formatted in Markdown. This includes images and videos, which are in the format of <c>![](source_url)</c>.</para>
    /// </remarks>
    /// <value>Markdown string?</value>
    /// <seealso cref="Message" />
    /// <seealso cref="IsPrivate" />
    /// <seealso cref="ReplyMessageIds" />
    /// <seealso cref="Embeds" />
    /// <seealso cref="Type" />
    public string? Content { get; }

    /// <summary>
    /// Gets the list of <see cref="Message">messages</see> being replied to.
    /// </summary>
    /// <remarks>
    /// <para>The max reply limit is 5.</para>
    /// </remarks>
    /// <value>List of <see cref="ChannelContent{TId, TServer}.Id">message IDs</see>?</value>
    /// <seealso cref="Message" />
    /// <seealso cref="IsPrivate" />
    /// <seealso cref="IsReply" />
    public IList<Guid>? ReplyMessageIds { get; }

    /// <summary>
    /// Gets the list of <see cref="Embed">custom embeds</see> that <see cref="Message">the message</see> contains.
    /// </summary>
    /// <remarks>
    /// <para>The max <see cref="Embed">embed</see> limit as of now is <see cref="EmbedLimit">1</see>.</para>
    /// </remarks>
    /// <value>List of <see cref="Embeds" />?</value>
    /// <seealso cref="Message" />
    /// <seealso cref="IsPrivate" />
    /// <seealso cref="ReplyMessageIds" />
    /// <seealso cref="Content" />
    public IList<Embed>? Embeds { get; }

    /// <summary>
    /// Gets <see cref="Mentions">the mentions</see> found in <see cref="Content">the content</see>.
    /// </summary>
    /// <value><see cref="Mentions" />?</value>
    public Mentions? Mentions { get; }

    /// <summary>
    /// Gets whether <see cref="IsReply">the reply</see> or mention is private.
    /// </summary>
    /// <remarks>
    /// <para>This can only be <see langword="true" /> if <see cref="ReplyMessageIds" /> has a value or there is an user or role mention in the <see cref="Content" />.</para>
    /// </remarks>
    /// <value><see cref="Message" /> is private</value>
    /// <seealso cref="Message" />
    /// <seealso cref="IsSilent" />
    /// <seealso cref="IsReply" />
    /// <seealso cref="ReplyMessageIds" />
    /// <seealso cref="Content" />
    /// <seealso cref="Embeds" />
    public bool IsPrivate { get; }

    /// <summary>
    /// Gets whether <see cref="IsReply">the reply</see> or mention is silent and doesn't ping any user.
    /// </summary>
    /// <remarks>
    /// <para>This can only be <see langword="true" /> if <see cref="ReplyMessageIds" /> has a value or there is an user or role mention in the <see cref="Content" />.</para>
    /// </remarks>
    /// <value><see cref="Message" /> is silent</value>
    /// <seealso cref="Message" />
    /// <seealso cref="IsPrivate" />
    /// <seealso cref="IsReply" />
    /// <seealso cref="ReplyMessageIds" />
    /// <seealso cref="Content" />
    /// <seealso cref="Embeds" />
    public bool IsSilent { get; }

    /// <summary>
    /// Gets whether <see cref="Message">the message</see> is <see cref="ReplyMessageIds">a reply</see> to another message.
    /// </summary>
    /// <value><see cref="Message" /> is <see cref="ReplyMessageIds">a reply</see></value>
    /// <seealso cref="Message" />
    /// <seealso cref="ReplyMessageIds" />
    /// <seealso cref="IsPrivate" />
    /// <seealso cref="IsSilent" />
    public bool IsReply => ReplyMessageIds?.Count > 0;
    #endregion

    /// <summary>
    /// Gets the identifier of <see cref="Webhook">the webhook</see> that created the message.
    /// </summary>
    /// <value><see cref="Webhook.Id">Webhook ID</see>?</value>
    /// <seealso cref="Message" />
    /// <seealso cref="ChannelContent{TId, TServer}.CreatedBy" />
    /// <seealso cref="ChannelContent{TId, TServer}.CreatedAt" />
    /// <seealso cref="UpdatedAt" />
    public Guid? CreatedByWebhook { get; }

    /// <summary>
    /// Gets the date when <see cref="Message">the message</see> was edited.
    /// </summary>
    /// <remarks>
    /// <para>Only returns the most recent update.</para>
    /// </remarks>
    /// <value>Date?</value>
    /// <seealso cref="Message" />
    /// <seealso cref="ChannelContent{TId, TServer}.CreatedAt" />
    /// <seealso cref="ChannelContent{TId, TServer}.CreatedBy" />
    /// <seealso cref="CreatedByWebhook" />
    public DateTime? UpdatedAt { get; }

    /// <summary>
    /// Gets the type of <see cref="Message">the message</see>.
    /// </summary>
    /// <remarks>
    /// <para>Distinguishes <see cref="Message">the message</see> by what content they contain.</para>
    /// </remarks>
    /// <value><see cref="MessageType">Message type</see></value>
    /// <seealso cref="Message" />
    /// <seealso cref="Content" />
    /// <seealso cref="Embeds" />
    /// <seealso cref="IsSystemMessage" />
    public MessageType Type { get; }

    /// <summary>
    /// Gets whether <see cref="Message">the message</see> is <see cref="MessageType.System">a system message</see>.
    /// </summary>
    /// <remarks>
    /// <para>A <see cref="MessageType.System">a system message</see> is a message that is created automatically on specific events, such as renaming the channel. Usually, it's something like "User has renamed the channel from X to Y"</para>
    /// </remarks>
    /// <value><see cref="Message" /> is <see cref="MessageType.System">a system message</see></value>
    /// <seealso cref="Message" />
    /// <seealso cref="Content" />
    /// <seealso cref="Embeds" />
    /// <seealso cref="Type" />
    public bool IsSystemMessage => Type == MessageType.System;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of <see cref="Message" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the message</param>
    /// <param name="channelId">The identifier of the channel where the message is</param>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the message is</param>
    /// <param name="content">The text contents of the message</param>
    /// <param name="replyMessageIds">Gets the list of <see cref="Message">messages</see> being replied to</param>
    /// <param name="embeds">Gets the list of <see cref="Embed">custom embeds</see> that this message contains</param>
    /// <param name="isPrivate">Whether <see cref="IsReply">the reply</see> or mention is private</param>
    /// <param name="isSilent">Whether <see cref="IsReply">the reply</see> or mention is silent</param>
    /// <param name="mentions"><see cref="Mentions">The mentions</see> found in <see cref="Content">the content</see></param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> that created the message</param>
    /// <param name="createdByWebhookId">The identifier of <see cref="Servers.Webhook">the webhook</see> that created the message</param>
    /// <param name="createdAt">the date when the message was created</param>
    /// <param name="updatedAt">the date when the message was edited</param>
    /// <param name="type">The type of the message</param>
    /// <returns>New <see cref="Message" /> JSON instance</returns>
    /// <seealso cref="Message" />
    [JsonConstructor]
    public Message(
        [JsonProperty(Required = Required.Always)]
        Guid id,

        [JsonProperty(Required = Required.Always)]
        Guid channelId,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(Required = Required.Always)]
        MessageType type,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? serverId = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        IList<Guid>? replyMessageIds = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? content = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        IList<Embed>? embeds = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool isPrivate = false,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool isSilent = false,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Mentions? mentions = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Guid? createdByWebhookId = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? updatedAt = null
    ) : base(id, channelId, serverId, createdBy, createdAt) =>
        (Content, ReplyMessageIds, Embeds, IsPrivate, IsSilent, Mentions, CreatedByWebhook, UpdatedAt, Type) = (content, replyMessageIds, embeds, isPrivate, isSilent, mentions, createdByWebhookId, updatedAt, type);
    #endregion

    #region Methods

    #region Method CreateMessageAsync
    /// <summary>
    /// Creates a message in the parent channel (from <see cref="ChannelContent{T, S}.ChannelId" />).
    /// </summary>
    /// <param name="message">The <see cref="Content">text contents</see> of <see cref="Message">the message</see> in Markdown (max — <c>4000</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <exception cref="ArgumentNullException">When the <see cref="MessageContent.Content">content</see> only consists of whitespace or is <see langword="null" /> and <see cref="MessageContent.Embeds">embeds</see> are also null or its array is empty</exception>
    /// <exception cref="ArgumentOutOfRangeException">When the <see cref="MessageContent.Content" /> is above the message limit of 4000 characters</exception>
    /// <permission cref="ChatPermissions.GetMessage">Required for reading all channel and thread messages</permission>
    /// <permission cref="ChatPermissions.CreateMessage">Required for sending a message in a channel</permission>
    /// <permission cref="ChatPermissions.CreateThreadMessage">Required for sending a message in a thread</permission>
    /// <returns>The <see cref="Message">message</see> that was created by the <see cref="BaseGuildedClient">client</see></returns>
    public Task<Message> CreateMessageAsync(MessageContent message) =>
        ParentClient.CreateMessageAsync(ChannelId, message);

    /// <inheritdoc cref="CreateMessageAsync(MessageContent)" />
    /// <remarks>
    /// <para>The text <paramref name="content" /> will be formatted in Markdown.</para>
    /// </remarks>
    /// <param name="content">The <see cref="Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="replyTo">The list of all <see cref="Message">messages</see> it is replying to (max — <c>5</c>)</param>
    /// <param name="isPrivate">Whether the mention is private</param>
    /// <param name="isSilent">Whether the mention is silent and does not ping anyone</param>
    /// <param name="embeds">The list of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task<Message> CreateMessageAsync(string? content = null, IList<Embed>? embeds = null, IList<Guid>? replyTo = null, bool isPrivate = false, bool isSilent = false) =>
        ParentClient.CreateMessageAsync(ChannelId, content, embeds, replyTo, isPrivate, isSilent);

    /// <inheritdoc cref="CreateMessageAsync(MessageContent)" />
    /// <remarks>
    /// <para>The text <paramref name="content" /> will be formatted in Markdown.</para>
    /// </remarks>
    /// <param name="content">The <see cref="Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="replyTo">The list of all <see cref="Message">messages</see> it is replying to (max — <c>5</c>)</param>
    /// <param name="isPrivate">Whether the mention is private</param>
    /// <param name="isSilent">Whether the mention is silent and does not ping anyone</param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task<Message> CreateMessageAsync(string? content = null, IList<Guid>? replyTo = null, bool isPrivate = false, bool isSilent = false, params Embed[] embeds) =>
        ParentClient.CreateMessageAsync(ChannelId, content, embeds, replyTo, isPrivate, isSilent);

    /// <inheritdoc cref="CreateMessageAsync(MessageContent)" />
    /// <remarks>
    /// <para>The text <paramref name="content" /> will be formatted in Markdown.</para>
    /// </remarks>
    /// <param name="content">The <see cref="Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="replyTo">The list of all <see cref="Message">messages</see> it is replying to (max — <c>5</c>)</param>
    /// <param name="isPrivate">Whether the reply is private</param>
    /// <param name="isSilent">Whether the reply is silent and does not ping anyone</param>
    /// <param name="embeds">The list of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task<Message> CreateMessageAsync(string? content = null, IList<Embed>? embeds = null, bool isPrivate = false, bool isSilent = false, params Guid[] replyTo) =>
        ParentClient.CreateMessageAsync(ChannelId, content, embeds, replyTo, isPrivate, isSilent);

    /// <inheritdoc cref="CreateMessageAsync(MessageContent)" />
    /// <remarks>
    /// <para>The text <paramref name="content" /> will be formatted in Markdown.</para>
    /// </remarks>
    /// <param name="content">The <see cref="Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task<Message> CreateMessageAsync(string content, params Embed[] embeds) =>
        ParentClient.CreateMessageAsync(ChannelId, content, embeds);

    /// <inheritdoc cref="CreateMessageAsync(MessageContent)" />
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task<Message> CreateMessageAsync(params Embed[] embeds) =>
        ParentClient.CreateMessageAsync(ChannelId, embeds);
    #endregion

    #region Method ReplyAsync
    /// <summary>
    /// Replies to the message in the parent channel (from <see cref="ChannelContent{T, T}.ChannelId" />).
    /// </summary>
    /// <remarks>
    /// <para>The given text <paramref name="content" /> will be formatted in Markdown.</para>
    /// <para>Includes this message (<see cref="ChannelContent{T, S}.Id" /> property) in the reply list.</para>
    /// </remarks>
    /// <param name="content">The <see cref="Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="isPrivate">Whether the mention is private</param>
    /// <param name="isSilent">Whether the mention is silent and does not ping anyone</param>
    /// <param name="embeds">The list of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <exception cref="ArgumentNullException">When the <paramref name="content" /> only consists of whitespace or is <see langword="nu" /></exception>
    /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="content" /> is above the message limit of 4000 characters</exception>
    /// <permission cref="ChatPermissions.GetMessage" />
    /// <permission cref="ChatPermissions.CreateMessage">Required when sending <see cref="Message">a message</see> in <see cref="ServerChannel">a top-most channel</see></permission>
    /// <permission cref="ChatPermissions.CreateThreadMessage">Required when sending <see cref="Message">a message</see> in <see cref="ServerChannel">a thread</see></permission>
    /// <permission cref="ChatPermissions.CreatePrivateMessage">Required when sending <see cref="Message">a message</see> that is set as <see cref="IsPrivate">private</see></permission>
    /// <permission cref="ChatPermissions.AddMedia">Required when sending <see cref="Message">a message</see> that contains an image or a video</permission>
    /// <returns>The <see cref="Message">message</see> that was created by the <see cref="BaseGuildedClient">client</see></returns>
    public Task<Message> ReplyAsync(string? content = null, IList<Embed>? embeds = null, bool isPrivate = false, bool isSilent = true) =>
        CreateMessageAsync(content, embeds, new Guid[] { Id }, isPrivate, isSilent);

    /// <inheritdoc cref="ReplyAsync(string, IList{Embed}, bool, bool)" />
    /// <param name="content">The <see cref="Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="isPrivate">Whether the mention is private</param>
    /// <param name="isSilent">Whether the mention is silent and does not ping anyone</param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task<Message> ReplyAsync(string? content = null, bool isPrivate = false, bool isSilent = true, params Embed[] embeds) =>
        CreateMessageAsync(content, embeds, new Guid[] { Id }, isPrivate, isSilent);

    /// <inheritdoc cref="ReplyAsync(string, IList{Embed}, bool, bool)" />
    /// <param name="content">The <see cref="Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task<Message> ReplyAsync(string content, params Embed[] embeds) =>
        CreateMessageAsync(content, embeds, new Guid[] { Id });

    /// <inheritdoc cref="ReplyAsync(string, IList{Embed}, bool, bool)" />
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task<Message> ReplyAsync(params Embed[] embeds) =>
        CreateMessageAsync(null, embeds, replyTo: new Guid[] { Id });
    #endregion

    #region Method UpdateAsync
    /// <inheritdoc cref="BaseGuildedClient.UpdateMessageAsync(Guid, Guid, MessageContent)" />
    /// <param name="content">The <see cref="MessageContent">new contents</see> of <see cref="Message">the message</see></param>
    public Task<Message> UpdateAsync(MessageContent content) =>
        ParentClient.UpdateMessageAsync(ChannelId, Id, content);

    /// <inheritdoc cref="BaseGuildedClient.UpdateMessageAsync(Guid, Guid, string, Embed[])" />
    /// <param name="content">The <see cref="Content">new text contents</see> of <see cref="Message">the message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="embeds">The new <see cref="Embed">custom embeds</see> of the <see cref="Message">message</see> in Markdown (max — <c>1</c>)</param>
    public Task<Message> UpdateAsync(string? content = null, IList<Embed>? embeds = null) =>
        ParentClient.UpdateMessageAsync(ChannelId, Id, content, embeds);

    /// <inheritdoc cref="BaseGuildedClient.UpdateMessageAsync(Guid, Guid, string, Embed[])" />
    /// <param name="content">The <see cref="Content">new text contents</see> of <see cref="Message">the message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="embeds">The new <see cref="Embed">custom embeds</see> of the <see cref="Message">message</see> in Markdown (max — <c>1</c>)</param>
    public Task<Message> UpdateAsync(string? content = null, params Embed[] embeds) =>
        ParentClient.UpdateMessageAsync(ChannelId, Id, content, embeds);

    /// <inheritdoc cref="BaseGuildedClient.UpdateMessageAsync(Guid, Guid, string, Embed[])" />
    /// <param name="embeds">The new <see cref="Embed">custom embeds</see> of the <see cref="Message">message</see> in Markdown (max — <c>1</c>)</param>
    public Task<Message> UpdateAsync(params Embed[] embeds) =>
        ParentClient.UpdateMessageAsync(ChannelId, Id, embeds);
    #endregion

    /// <inheritdoc cref="BaseGuildedClient.DeleteMessageAsync(Guid, Guid)" />
    public Task DeleteAsync() =>
        ParentClient.DeleteMessageAsync(ChannelId, Id);

    /// <inheritdoc cref="BaseGuildedClient.AddReactionAsync(Guid, Guid, uint)" />
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to add</param>
    public Task AddReactionAsync(uint emote) =>
        ParentClient.AddReactionAsync(ChannelId, Id, emote);

    /// <inheritdoc cref="BaseGuildedClient.RemoveReactionAsync(Guid, Guid, uint)" />
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to remove</param>
    public Task RemoveReactionAsync(uint emote) =>
        ParentClient.RemoveReactionAsync(ChannelId, Id, emote);
    #endregion
}
