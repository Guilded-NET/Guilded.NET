using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Base.Embeds;
using Guilded.Client;
using Guilded.Events;
using Guilded.Servers;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Content;

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
/// <seealso cref="Item" />
/// <seealso cref="Topic" />
public class Message :
    ChannelContent<Guid, HashId?>, IUpdatableContent, IWebhookCreated, IReactibleContent,
    IContentBlockMarkdown, IGlobalContent, IPrivatableContent
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
    /// The count of how many <see cref="Embeds">embeds</see> there can be in a <see cref="Message">message</see>.
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

    #region Properties Content
    /// <summary>
    /// Gets the identifier of the group where the <see cref="Message">message</see> is.
    /// </summary>
    /// <value>The identifier of the group where the <see cref="Message">message</see> is</value>
    /// <seealso cref="Message" />
    /// <seealso cref="ChannelContent{TId, TServer}.Id" />
    /// <seealso cref="ChannelContent{TId, TServer}.ServerId" />
    public HashId? GroupId { get; }

    /// <summary>
    /// Gets the text contents of the <see cref="Message">message</see>.
    /// </summary>
    /// <remarks>
    /// <para>The contents are formatted in Markdown. This includes images and videos, which are in the format of <c>![](source_url)</c>.</para>
    /// </remarks>
    /// <value>The text contents of the <see cref="Message">message</see></value>
    /// <seealso cref="Message" />
    /// <seealso cref="IsPrivate" />
    /// <seealso cref="ReplyMessageIds" />
    /// <seealso cref="Embeds" />
    /// <seealso cref="Type" />
    public string? Content { get; }

    /// <summary>
    /// Gets the list of <see cref="Message">messages</see> that the current <see cref="Message">message</see> is replying to.
    /// </summary>
    /// <remarks>
    /// <para>The max reply limit is 5.</para>
    /// </remarks>
    /// <value>The list of <see cref="Message">messages</see> that the current <see cref="Message">message</see> is replying to</value>
    /// <seealso cref="Message" />
    /// <seealso cref="IsPrivate" />
    /// <seealso cref="IsReply" />
    public IList<Guid>? ReplyMessageIds { get; }

    /// <summary>
    /// Gets the list of links that will not be <see cref="Embed">embeded</see> in the <see cref="Message">message</see>.
    /// </summary>
    /// <value>The list of links that will not be <see cref="Embed">embeded</see> in the <see cref="Message">message</see></value>
    /// <seealso cref="Message" />
    /// <seealso cref="IsPrivate" />
    /// <seealso cref="IsReply" />
    public ISet<Uri>? HiddenUrls { get; }

    /// <summary>
    /// Gets the list of <see cref="Embed">custom embeds</see> that are part of the <see cref="Message">message's</see> contents.
    /// </summary>
    /// <remarks>
    /// <para>The max <see cref="Embed">embed</see> limit as of now is <see cref="EmbedLimit">1</see>.</para>
    /// </remarks>
    /// <value>The list of <see cref="Embed">custom embeds</see> that are part of the <see cref="Message">message's</see> contents</value>
    /// <seealso cref="Message" />
    /// <seealso cref="IsPrivate" />
    /// <seealso cref="ReplyMessageIds" />
    /// <seealso cref="Content" />
    public IList<Embed>? Embeds { get; }

    /// <summary>
    /// Gets the <see cref="Mentions">mentions</see> found in the <see cref="Content">content</see>.
    /// </summary>
    /// <value>The <see cref="Mentions">mentions</see> found in the <see cref="Content">content</see></value>
    public Mentions? Mentions { get; }

    /// <summary>
    /// Gets whether the reply or mention is private.
    /// </summary>
    /// <remarks>
    /// <para>This can only be <see langword="true" /> if <see cref="ReplyMessageIds" /> has a value or there is an user or role mention in the <see cref="Content" />.</para>
    /// </remarks>
    /// <value>Whether the reply or mention is private</value>
    /// <seealso cref="Message" />
    /// <seealso cref="IsSilent" />
    /// <seealso cref="IsReply" />
    /// <seealso cref="ReplyMessageIds" />
    /// <seealso cref="Content" />
    /// <seealso cref="Embeds" />
    public bool IsPrivate { get; }

    /// <summary>
    /// Gets whether the reply or mention is silent and doesn't ping any <see cref="User">user</see>.
    /// </summary>
    /// <remarks>
    /// <para>This can only be <see langword="true" /> if <see cref="ReplyMessageIds" /> has a value or there is an user or role mention in the <see cref="Content" />.</para>
    /// </remarks>
    /// <value>Whether the reply or mention is silent and doesn't ping any <see cref="User">user</see></value>
    /// <seealso cref="Message" />
    /// <seealso cref="IsPrivate" />
    /// <seealso cref="IsReply" />
    /// <seealso cref="ReplyMessageIds" />
    /// <seealso cref="Content" />
    /// <seealso cref="Embeds" />
    public bool IsSilent { get; }

    /// <summary>
    /// Gets whether the <see cref="Message">message</see> is <see cref="ReplyMessageIds">a reply</see> to another message.
    /// </summary>
    /// <value>Whether the <see cref="Message">message</see> is <see cref="ReplyMessageIds">a reply</see> to another message</value>
    /// <seealso cref="Message" />
    /// <seealso cref="ReplyMessageIds" />
    /// <seealso cref="IsPrivate" />
    /// <seealso cref="IsSilent" />
    [MemberNotNullWhen(true, nameof(ReplyMessageIds))]
    public bool IsReply => ReplyMessageIds?.Count > 0;
    #endregion

    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="Webhook">webhook</see> that created the <see cref="Message">message</see>.
    /// </summary>
    /// <value>The identifier of the <see cref="Webhook">webhook</see> that created the <see cref="Message">message</see></value>
    /// <seealso cref="Message" />
    /// <seealso cref="ChannelContent{TId, TServer}.CreatedBy" />
    /// <seealso cref="ChannelContent{TId, TServer}.CreatedAt" />
    /// <seealso cref="UpdatedAt" />
    public Guid? CreatedByWebhook { get; }

    /// <summary>
    /// Gets the date when the <see cref="Message">message</see> was edited.
    /// </summary>
    /// <remarks>
    /// <para>Only returns the most recent update.</para>
    /// </remarks>
    /// <value>The date when the <see cref="Message">message</see> was edited</value>
    /// <seealso cref="Message" />
    /// <seealso cref="ChannelContent{TId, TServer}.CreatedAt" />
    /// <seealso cref="ChannelContent{TId, TServer}.CreatedBy" />
    /// <seealso cref="CreatedByWebhook" />
    public DateTime? UpdatedAt { get; }

    /// <summary>
    /// Gets the type of the <see cref="Message">message</see>.
    /// </summary>
    /// <remarks>
    /// <para>Distinguishes the <see cref="Message">message</see> by what content they contain.</para>
    /// </remarks>
    /// <value>The type of the <see cref="Message">message</see></value>
    /// <seealso cref="Message" />
    /// <seealso cref="Content" />
    /// <seealso cref="Embeds" />
    /// <seealso cref="IsSystemMessage" />
    public MessageType Type { get; }

    /// <summary>
    /// Gets whether the <see cref="Message">message</see> is a <see cref="MessageType.System">system message</see>.
    /// </summary>
    /// <remarks>
    /// <para>A <see cref="MessageType.System">system message</see> is a <see cref="Message">message</see> that is created automatically on specific events, such as renaming a <see cref="ServerChannel">channel</see>. Usually, it's something like "User has renamed the channel from X to Y"</para>
    /// </remarks>
    /// <value>Whether the <see cref="Message">message</see> is a <see cref="MessageType.System">system message</see></value>
    /// <seealso cref="Message" />
    /// <seealso cref="Content" />
    /// <seealso cref="Embeds" />
    /// <seealso cref="Type" />
    public bool IsSystemMessage => Type == MessageType.System;
    #endregion

    #region Properties Events
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a reaction is added to the <see cref="Message">message</see>.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="Message">message</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when a reaction is added to the <see cref="Message">message</see></returns>
    /// <seealso cref="ReactionRemoved" />
    /// <seealso cref="Replied" />
    /// <seealso cref="Updated" />
    /// <seealso cref="Deleted" />
    public IObservable<MessageReactionEvent> ReactionAdded =>
        ParentClient
            .MessageReactionAdded
            .Where(x =>
                x.ChannelId == ChannelId && x.MessageId == Id
            );

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a reaction is removed from the <see cref="Message">message</see>.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="Message">message</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when a reaction is removed from the <see cref="Message">message</see></returns>
    /// <seealso cref="ReactionAdded" />
    /// <seealso cref="Replied" />
    /// <seealso cref="Updated" />
    /// <seealso cref="Deleted" />
    public IObservable<MessageReactionEvent> ReactionRemoved =>
        ParentClient
            .MessageReactionRemoved
            .Where(x =>
                x.ChannelId == ChannelId && x.MessageId == Id
            );

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a reply <see cref="Message">message</see> gets created to the message.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="Message">message</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when a reply <see cref="Message">message</see> gets created to the message</returns>
    /// <seealso cref="ReactionAdded" />
    /// <seealso cref="ReactionRemoved" />
    /// <seealso cref="Updated" />
    /// <seealso cref="Deleted" />
    public IObservable<MessageEvent> Replied =>
        ParentClient
            .MessageCreated
            .Where(x =>
                x.ChannelId == ChannelId && (x.ReplyMessageIds?.Contains(Id) ?? false)
            );

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="Message">message</see> gets edited.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="Message">message</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="Message">message</see> gets edited</returns>
    /// <seealso cref="ReactionAdded" />
    /// <seealso cref="ReactionRemoved" />
    /// <seealso cref="Replied" />
    /// <seealso cref="Deleted" />
    public IObservable<MessageEvent> Updated =>
        ParentClient
            .MessageUpdated
            .Where(x =>
                x.ChannelId == ChannelId && x.Message.Id == Id
            );

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="Message">message</see> gets deleted.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="Message">message</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="Message">message</see> gets deleted</returns>
    /// <seealso cref="ReactionAdded" />
    /// <seealso cref="ReactionRemoved" />
    /// <seealso cref="Updated" />
    /// <seealso cref="Deleted" />
    public IObservable<MessageDeletedEvent> Deleted =>
        ParentClient
            .MessageDeleted
            .Where(x =>
                x.ChannelId == ChannelId && x.Message.Id == Id
            )
            .Take(1);
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of <see cref="Message" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the <see cref="Message">message</see></param>
    /// <param name="channelId">The identifier of the <see cref="ServerChannel">channel</see> where the <see cref="Message">message</see> is</param>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="Message">message</see> is</param>
    /// <param name="content">The text contents of the <see cref="Message">message</see></param>
    /// <param name="replyMessageIds">The list of <see cref="Message">messages</see> that the current <see cref="Message">message</see> is replying to</param>
    /// <param name="hiddenLinkPreviewUrls">The list of links that will not be <see cref="Embed">embeded</see> in the <see cref="Message">message</see></param>
    /// <param name="embeds">The list of <see cref="Embed">custom embeds</see> that are part of the <see cref="Message">message's</see> contents</param>
    /// <param name="isPrivate">Whether the reply or mention is private</param>
    /// <param name="isSilent">Whether the reply or mention is silent and doesn't ping any user</param>
    /// <param name="mentions">The <see cref="Mentions">mentions</see> found in the <see cref="Content">content</see></param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> that created the message</param>
    /// <param name="createdByWebhookId">The identifier of the <see cref="Webhook">webhook</see> that created the <see cref="Message">message</see></param>
    /// <param name="createdAt">The date when the <see cref="Message">message</see> was created</param>
    /// <param name="updatedAt">The date when the <see cref="Message">message</see> was edited</param>
    /// <param name="type">The type of the <see cref="Message">message</see></param>
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
        ISet<Uri>? hiddenLinkPreviewUrls = null,

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
        (Content, ReplyMessageIds, HiddenUrls, Embeds, IsPrivate, IsSilent, Mentions, CreatedByWebhook, UpdatedAt, Type) = (content, replyMessageIds, hiddenLinkPreviewUrls, embeds, isPrivate, isSilent, mentions, createdByWebhookId, updatedAt, type);
    #endregion

    #region Methods CreateMessageAsync
    /// <summary>
    /// Creates a message in the parent channel (from <see cref="ChannelContent{T, S}.ChannelId" />).
    /// </summary>
    /// <param name="message">The <see cref="Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <exception cref="ArgumentNullException">When the <see cref="MessageContent.Content">content</see> only consists of whitespace or is <see langword="null" /> and <see cref="MessageContent.Embeds">embeds</see> are also null or its array is empty</exception>
    /// <exception cref="ArgumentOutOfRangeException">When the <see cref="MessageContent.Content" /> is above the message limit of 4000 characters</exception>
    /// <permission cref="Permission.GetMessages">Required for reading all channel and thread messages</permission>
    /// <permission cref="Permission.CreateMessages">Required for sending a message in a channel</permission>
    /// <permission cref="Permission.CreateThreadMessages">Required for sending a message in a thread</permission>
    /// <returns>The <see cref="Message">message</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Message> CreateMessageAsync(MessageContent message) =>
        ParentClient.CreateMessageAsync(ChannelId, message);

    /// <inheritdoc cref="CreateMessageAsync(MessageContent)" />
    /// <remarks>
    /// <para>The text <paramref name="content" /> will be formatted in Markdown.</para>
    /// </remarks>
    /// <param name="content">The <see cref="Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="replyTo">The list of all <see cref="Message">messages</see> it is replying to (max — <c>5</c>)</param>
    /// <param name="hiddenUrls">The <see cref="Uri">URLs</see> that will not have <see cref="Embed">link embeds</see></param>
    /// <param name="isPrivate">Whether the mention is private</param>
    /// <param name="isSilent">Whether the mention is silent and does not ping anyone</param>
    /// <param name="embeds">The list of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task<Message> CreateMessageAsync(string? content = null, IList<Embed>? embeds = null, IList<Guid>? replyTo = null, ISet<Uri>? hiddenUrls = null, bool isPrivate = false, bool isSilent = false) =>
        ParentClient.CreateMessageAsync(ChannelId, content, embeds, replyTo, hiddenUrls, isPrivate, isSilent);

    /// <inheritdoc cref="CreateMessageAsync(MessageContent)" />
    /// <remarks>
    /// <para>The text <paramref name="content" /> will be formatted in Markdown.</para>
    /// </remarks>
    /// <param name="content">The <see cref="Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="replyTo">The list of all <see cref="Message">messages</see> it is replying to (max — <c>5</c>)</param>
    /// <param name="hiddenUrls">The <see cref="Uri">URLs</see> that will not have <see cref="Embed">link embeds</see></param>
    /// <param name="isPrivate">Whether the mention is private</param>
    /// <param name="isSilent">Whether the mention is silent and does not ping anyone</param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task<Message> CreateMessageAsync(string? content = null, IList<Guid>? replyTo = null, ISet<Uri>? hiddenUrls = null, bool isPrivate = false, bool isSilent = false, params Embed[] embeds) =>
        ParentClient.CreateMessageAsync(ChannelId, content, embeds, replyTo, hiddenUrls, isPrivate, isSilent);

    /// <inheritdoc cref="CreateMessageAsync(MessageContent)" />
    /// <remarks>
    /// <para>The text <paramref name="content" /> will be formatted in Markdown.</para>
    /// </remarks>
    /// <param name="content">The <see cref="Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="replyTo">The list of all <see cref="Message">messages</see> it is replying to (max — <c>5</c>)</param>
    /// <param name="hiddenUrls">The <see cref="Uri">URLs</see> that will not have <see cref="Embed">link embeds</see></param>
    /// <param name="isPrivate">Whether the reply is private</param>
    /// <param name="isSilent">Whether the reply is silent and does not ping anyone</param>
    /// <param name="embeds">The list of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task<Message> CreateMessageAsync(string? content = null, IList<Embed>? embeds = null, ISet<Uri>? hiddenUrls = null, bool isPrivate = false, bool isSilent = false, params Guid[] replyTo) =>
        ParentClient.CreateMessageAsync(ChannelId, content, embeds, replyTo, hiddenUrls, isPrivate, isSilent);

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

    #region Methods ReplyAsync
    /// <summary>
    /// Replies to the message in the parent channel (from <see cref="ChannelContent{T, T}.ChannelId" />).
    /// </summary>
    /// <remarks>
    /// <para>The given text <paramref name="content" /> will be formatted in Markdown.</para>
    /// <para>Includes this message (<see cref="ChannelContent{T, S}.Id" /> property) in the reply list.</para>
    /// </remarks>
    /// <param name="content">The <see cref="Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="hiddenUrls">The <see cref="Uri">URLs</see> that will not have <see cref="Embed">link embeds</see></param>
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
    /// <permission cref="Permission.GetMessages" />
    /// <permission cref="Permission.CreateMessages">Required when sending a <see cref="Message">message</see> in <see cref="ServerChannel">a top-most channel</see></permission>
    /// <permission cref="Permission.CreateThreadMessages">Required when sending a <see cref="Message">message</see> in <see cref="ServerChannel">a thread</see></permission>
    /// <permission cref="Permission.CreatePrivateMessages">Required when sending a <see cref="Message">message</see> that is set as <see cref="IsPrivate">private</see></permission>
    /// <permission cref="Permission.CreateMessageMedia">Required when sending a <see cref="Message">message</see> that contains an image or a video</permission>
    /// <returns>The <see cref="Message">message</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Message> ReplyAsync(string? content = null, IList<Embed>? embeds = null, ISet<Uri>? hiddenUrls = null, bool isPrivate = false, bool isSilent = true) =>
        CreateMessageAsync(content, embeds, new Guid[] { Id }, hiddenUrls, isPrivate, isSilent);

    /// <inheritdoc cref="ReplyAsync(string, IList{Embed}, ISet{Uri}, bool, bool)" />
    /// <param name="content">The <see cref="Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="isPrivate">Whether the mention is private</param>
    /// <param name="isSilent">Whether the mention is silent and does not ping anyone</param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task<Message> ReplyAsync(string? content = null, bool isPrivate = false, bool isSilent = true, params Embed[] embeds) =>
        CreateMessageAsync(content, embeds, new Guid[] { Id }, null, isPrivate, isSilent);

    /// <inheritdoc cref="ReplyAsync(string, IList{Embed}, ISet{Uri}, bool, bool)" />
    /// <param name="content">The <see cref="Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="hiddenUrls">The <see cref="Uri">URLs</see> that will not have <see cref="Embed">link embeds</see></param>
    /// <param name="isPrivate">Whether the mention is private</param>
    /// <param name="isSilent">Whether the mention is silent and does not ping anyone</param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task<Message> ReplyAsync(string? content = null, ISet<Uri>? hiddenUrls = null, bool isPrivate = false, bool isSilent = true, params Embed[] embeds) =>
        CreateMessageAsync(content, embeds, new Guid[] { Id }, hiddenUrls, isPrivate, isSilent);

    /// <inheritdoc cref="ReplyAsync(string, IList{Embed}, ISet{Uri}, bool, bool)" />
    /// <param name="content">The <see cref="Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task<Message> ReplyAsync(string content, params Embed[] embeds) =>
        CreateMessageAsync(content, embeds, new Guid[] { Id });

    /// <inheritdoc cref="ReplyAsync(string, IList{Embed}, ISet{Uri}, bool, bool)" />
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task<Message> ReplyAsync(params Embed[] embeds) =>
        CreateMessageAsync(null, embeds, replyTo: new Guid[] { Id });
    #endregion

    #region Methods UpdateAsync
    /// <inheritdoc cref="AbstractGuildedClient.UpdateMessageAsync(Guid, Guid, MessageContent)" />
    /// <param name="content">The <see cref="MessageContent">new contents</see> of the <see cref="Message">message</see></param>
    public Task<Message> UpdateAsync(MessageContent content) =>
        ParentClient.UpdateMessageAsync(ChannelId, Id, content);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateMessageAsync(Guid, Guid, string, IList{Embed}, ISet{Uri})" />
    /// <param name="content">The <see cref="Content">new text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="embeds">The new <see cref="Embed">custom embeds</see> of the <see cref="Message">message</see> in Markdown (max — <c>1</c>)</param>
    /// <param name="hiddenUrls">The new <see cref="Uri">URLs</see> that will not have <see cref="Embed">link embeds</see></param>
    public Task<Message> UpdateAsync(string? content = null, IList<Embed>? embeds = null, ISet<Uri>? hiddenUrls = null) =>
        ParentClient.UpdateMessageAsync(ChannelId, Id, content, embeds, hiddenUrls);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateMessageAsync(Guid, Guid, string, ISet{Uri}?, Embed[])" />
    /// <param name="content">The <see cref="Content">new text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="embeds">The new <see cref="Embed">custom embeds</see> of the <see cref="Message">message</see> in Markdown (max — <c>1</c>)</param>
    public Task<Message> UpdateAsync(string? content = null, params Embed[] embeds) =>
        ParentClient.UpdateMessageAsync(ChannelId, Id, content, embeds);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateMessageAsync(Guid, Guid, string, ISet{Uri}?, Embed[])" />
    /// <param name="content">The <see cref="Content">new text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="hiddenUrls">The new <see cref="Uri">URLs</see> that will not have <see cref="Embed">link embeds</see></param>
    /// <param name="embeds">The new <see cref="Embed">custom embeds</see> of the <see cref="Message">message</see> in Markdown (max — <c>1</c>)</param>
    public Task<Message> UpdateAsync(string? content = null, ISet<Uri>? hiddenUrls = null, params Embed[] embeds) =>
        ParentClient.UpdateMessageAsync(ChannelId, Id, content, hiddenUrls, embeds);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateMessageAsync(Guid, Guid, string, Embed[])" />
    /// <param name="embeds">The new <see cref="Embed">custom embeds</see> of the <see cref="Message">message</see> in Markdown (max — <c>1</c>)</param>
    public Task<Message> UpdateAsync(params Embed[] embeds) =>
        ParentClient.UpdateMessageAsync(ChannelId, Id, embeds);
    #endregion

    #region Methods Threads
    /// <summary>
    /// Creates a thread from the <see cref="Message">message</see>.
    /// </summary>
    /// <remarks>
    /// <para>Includes this message as a parent of the thread.</para>
    /// </remarks>
    /// <param name="name">The name of the <see cref="ServerChannel">thread</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <exception cref="InvalidOperationException">When the <see cref="Message">message</see> is in direct messages</exception>
    /// <permission cref="Permission.GetMessages" />
    /// <permission cref="Permission.CreateThreads" />
    /// <returns>The <see cref="ChatChannel">thread</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public async Task<ChatChannel> CreateThreadAsync(string name) =>
        ServerId is null
        ? throw new InvalidOperationException("Cannot create a thread in direct messages")
        : (ChatChannel)await ParentClient.CreateChannelAsync((HashId)ServerId, name, message: Id);
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.DeleteMessageAsync(Guid, Guid)" />
    public Task DeleteAsync() =>
        ParentClient.DeleteMessageAsync(ChannelId, Id);

    /// <inheritdoc cref="AbstractGuildedClient.PinMessageAsync(Guid, Guid)" />
    public Task PinAsync() =>
        ParentClient.PinMessageAsync(ChannelId, Id);

    /// <inheritdoc cref="AbstractGuildedClient.UnpinMessageAsync(Guid, Guid)" />
    public Task UnpinAsync() =>
        ParentClient.UnpinMessageAsync(ChannelId, Id);

    /// <inheritdoc cref="AbstractGuildedClient.AddMessageReactionAsync(Guid, Guid, uint)" />
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to add</param>
    public Task AddReactionAsync(uint emote) =>
        ParentClient.AddMessageReactionAsync(ChannelId, Id, emote);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveMessageReactionAsync(Guid, Guid, uint)" />
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to remove</param>
    public Task RemoveReactionAsync(uint emote) =>
        ParentClient.RemoveMessageReactionAsync(ChannelId, Id, emote);
    #endregion
}
