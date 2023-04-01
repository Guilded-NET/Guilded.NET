using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Client;
using Guilded.Events;
using Guilded.Servers;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Content;

/// <summary>
/// Represents a summary of a <see cref="Topic">forum topic</see> in a <see cref="ChannelType.Forums">forum channel</see>.
/// </summary>
/// <remarks>
/// <para>This summary does not contain the <see cref="Topic.Content">content</see> of the <see cref="Topic">forum topic</see> and only the <see cref="TitledContent.Title">title</see></para>
/// </remarks>
/// <seealso cref="Topic" />
/// <seealso cref="Doc" />
/// <seealso cref="Message" />
/// <seealso cref="Item" />
/// <seealso cref="CalendarEvent" />
public class TopicSummary : TitledContent
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="Webhook">webhook</see> that created the <see cref="TopicSummary">forum thread</see>.
    /// </summary>
    /// <value>The identifier of the <see cref="Webhook">webhook</see> that created the <see cref="TopicSummary">forum thread</see></value>
    /// <seealso cref="TopicSummary" />
    /// <seealso cref="ChannelContent{TId, TServer}.CreatedBy" />
    /// <seealso cref="ChannelContent{TId, TServer}.CreatedAt" />
    /// <seealso cref="TitledContent.UpdatedAt" />
    public Guid? CreatedByWebhook { get; }

    /// <summary>
    /// Gets the date when the <see cref="TopicSummary">forum topic</see> was bumped.
    /// </summary>
    /// <value>The date when the <see cref="TopicSummary">forum topic</see> was bumped</value>
    /// <seealso cref="TopicSummary" />
    /// <seealso cref="ChannelContent{TId, TServer}.CreatedAt" />
    /// <seealso cref="TitledContent.UpdatedAt" />
    public DateTime BumpedAt { get; }

    /// <summary>
    /// Gets whether the <see cref="TopicSummary">forum topic</see> has been pinned.
    /// </summary>
    /// <value>Whether the <see cref="TopicSummary">forum topic</see> has been pinned</value>
    /// <seealso cref="TopicSummary" />
    /// <seealso cref="IsLocked" />
    public bool IsPinned { get; }

    /// <summary>
    /// Gets whether the <see cref="TopicSummary">forum topic</see> has been locked.
    /// </summary>
    /// <value>Whether the <see cref="TopicSummary">forum topic</see> has been locked</value>
    /// <seealso cref="TopicSummary" />
    /// <seealso cref="IsLocked" />
    public bool IsLocked { get; }
    #endregion

    #region Properties Events
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="Topic">forum topic</see> gets edited.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="Topic">forum topic</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="Topic">forum topic</see> gets edited</returns>
    /// <seealso cref="Deleted" />
    public IObservable<TopicEvent> Updated =>
        ParentClient
            .TopicUpdated
            .Where(x =>
                x.ChannelId == ChannelId && x.Topic.Id == Id
            );

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="Topic">forum topic</see> gets deleted.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="Topic">forum topic</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="Topic">forum topic</see> gets deleted</returns>
    /// <seealso cref="Updated" />
    public IObservable<TopicEvent> Deleted =>
        ParentClient
            .TopicDeleted
            .Where(x =>
                x.ChannelId == ChannelId && x.Topic.Id == Id
            )
            .Take(1);

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="Topic">forum topic</see> gets locked.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="Topic">forum topic</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="Topic">forum topic</see> gets locked</returns>
    /// <seealso cref="Updated" />
    /// <seealso cref="Deleted" />
    public IObservable<TopicEvent> Locked =>
        ParentClient
            .TopicLocked
            .Where(x =>
                x.ChannelId == ChannelId && x.Topic.Id == Id
            );

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="Topic">forum topic</see> gets unlocked.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="Topic">forum topic</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="Topic">forum topic</see> gets unlocked</returns>
    /// <seealso cref="Updated" />
    /// <seealso cref="Deleted" />
    public IObservable<TopicEvent> Unlocked =>
        ParentClient
            .TopicUnlocked
            .Where(x =>
                x.ChannelId == ChannelId && x.Topic.Id == Id
            );

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="Topic">forum topic</see> gets pinned.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="Topic">forum topic</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="Topic">forum topic</see> gets pinned</returns>
    /// <seealso cref="Updated" />
    /// <seealso cref="Deleted" />
    public IObservable<TopicEvent> Pinned =>
        ParentClient
            .TopicPinned
            .Where(x =>
                x.ChannelId == ChannelId && x.Topic.Id == Id
            );

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="Topic">forum topic</see> gets unpinned.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="Topic">forum topic</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="Topic">forum topic</see> gets unpinned</returns>
    /// <seealso cref="Updated" />
    /// <seealso cref="Deleted" />
    public IObservable<TopicEvent> Unpinned =>
        ParentClient
            .TopicUnpinned
            .Where(x =>
                x.ChannelId == ChannelId && x.Topic.Id == Id
            );
    #endregion

    #region Properties Events Comment
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="TopicComment">forum topic comment</see> gets created.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="Topic">forum topic</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="TopicComment">forum topic comment</see> gets created</returns>
    /// <seealso cref="CommentUpdated" />
    /// <seealso cref="CommentDeleted" />
    public IObservable<TopicCommentEvent> CommentCreated =>
        ParentClient
            .TopicCommentUpdated
            .Where(x =>
                x.ChannelId == ChannelId && x.TopicId == Id
            );

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="TopicComment">forum topic comment</see> gets edited.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="Topic">forum topic</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="TopicComment">forum topic comment</see> gets edited</returns>
    /// <seealso cref="CommentCreated" />
    /// <seealso cref="CommentDeleted" />
    public IObservable<TopicCommentEvent> CommentUpdated =>
        ParentClient
            .TopicCommentUpdated
            .Where(x =>
                x.ChannelId == ChannelId && x.TopicId == Id
            );

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="TopicComment">forum topic comment</see> gets deleted.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="Topic">forum topic</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="TopicComment">forum topic comment</see> gets deleted</returns>
    /// <seealso cref="CommentCreated" />
    /// <seealso cref="CommentUpdated" />
    public IObservable<TopicCommentEvent> CommentDeleted =>
        ParentClient
            .TopicCommentDeleted
            .Where(x =>
                x.ChannelId == ChannelId && x.TopicId == Id
            );
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="TopicSummary" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the <see cref="TopicSummary">forum topic</see></param>
    /// <param name="channelId">The identifier of the <see cref="ForumChannel">channel</see> where the <see cref="TopicSummary">forum topic</see> is</param>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="TopicSummary">forum topic</see> is</param>
    /// <param name="title">The title of the <see cref="TopicSummary">forum topic</see></param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> that created the <see cref="TopicSummary">forum topic</see></param>
    /// <param name="createdByWebhookId">The identifier of the <see cref="Webhook">webhook</see> that created the <see cref="TopicSummary">forum topic</see></param>
    /// <param name="createdAt">The date when the <see cref="TopicSummary">forum topic</see> was created</param>
    /// <param name="bumpedAt">The date when the <see cref="TopicSummary">forum topic</see> was bumped</param>
    /// <param name="updatedAt">The date when the <see cref="TopicSummary">forum topic</see> was edited</param>
    /// <param name="isPinned">Whether the <see cref="TopicSummary">forum topic</see> has been pinned</param>
    /// <param name="isLocked">Whether the <see cref="TopicSummary">forum topic</see> has been locked</param>
    /// <returns>New <see cref="TopicSummary" /> JSON instance</returns>
    /// <seealso cref="TopicSummary" />
    [JsonConstructor]
    public TopicSummary(
        [JsonProperty(Required = Required.Always)]
        uint id,

        [JsonProperty(Required = Required.Always)]
        Guid channelId,

        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        string title,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(Required = Required.Always)]
        DateTime bumpedAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Guid? createdByWebhookId = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? updatedAt = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool isPinned = false,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool isLocked = false
    ) : base(id, channelId, serverId, title, createdBy, createdAt, updatedAt) =>
        (BumpedAt, CreatedByWebhook, IsPinned, IsLocked) = (bumpedAt, createdByWebhookId, isPinned, isLocked);
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.UpdateTopicAsync(Guid, uint, string, string)" />
    /// <param name="title">The new title of the <see cref="Topic">forum topic</see></param>
    /// <param name="content">The Markdown content of the <see cref="Topic">forum topic</see></param>
    public Task<Topic> UpdateAsync(string title, string content) =>
        ParentClient.UpdateTopicAsync(ChannelId, Id, title, content);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteTopicAsync(Guid, uint)" />
    public Task DeleteAsync() =>
        ParentClient.DeleteTopicAsync(ChannelId, Id);

    /// <inheritdoc cref="AbstractGuildedClient.CreateTopicCommentAsync(Guid, uint, string)" />
    /// <param name="content">The content of the <see cref="TopicComment">forum topic comment</see></param>
    public Task<TopicComment> CreateCommentAsync(string content) =>
        ParentClient.CreateTopicCommentAsync(ChannelId, Id, content);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateTopicCommentAsync(Guid, uint, uint, string)" />
    /// <param name="topicComment">The identifier of the <see cref="TopicComment">forum topic comment</see> to update</param>
    /// <param name="content">The new acontent of the <see cref="TopicComment">forum topic comment</see></param>
    public Task<TopicComment> UpdateCommentAsync(uint topicComment, string content) =>
        ParentClient.UpdateTopicCommentAsync(ChannelId, Id, topicComment, content);

    /// <inheritdoc cref="AbstractGuildedClient.CreateTopicCommentAsync(Guid, uint, string)" />
    /// <param name="topicComment">The identifier of the <see cref="TopicComment">forum topic comment</see> to delete</param>
    public Task DeleteCommentAsync(uint topicComment) =>
        ParentClient.DeleteTopicCommentAsync(ChannelId, Id, topicComment);
    #endregion
}

/// <summary>
/// Represents the full information of a topic/post/thread in a <see cref="ChannelType.Forums">forum channel</see>.
/// </summary>
/// <seealso cref="TopicSummary" />
/// <seealso cref="Doc" />
/// <seealso cref="Message" />
/// <seealso cref="Item" />
/// <seealso cref="CalendarEvent" />
public class Topic : TopicSummary, IContentMarkdown
{
    #region Properties
    /// <summary>
    /// Gets the text contents of the <see cref="Topic">forum topic</see>.
    /// </summary>
    /// <value>Markdown string</value>
    /// <seealso cref="Topic" />
    /// <seealso cref="Mentions" />
    /// <seealso cref="TitledContent.Title" />
    public string Content { get; }

    /// <inheritdoc />
    /// <seealso cref="Topic" />
    /// <seealso cref="Content" />
    /// <seealso cref="TitledContent.Title" />
    public Mentions? Mentions { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="Topic" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the forum thread</param>
    /// <param name="channelId">The identifier of the channel where the forum thread is</param>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the forum thread is</param>
    /// <param name="title">The title of the forum thread</param>
    /// <param name="content">The text contents of the <see cref="Topic">forum topic</see></param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> that created the forum thread</param>
    /// <param name="createdByWebhookId">The identifier of the webhook that created the forum thread</param>
    /// <param name="createdAt">The date when the forum thread was created</param>
    /// <param name="bumpedAt">The date when the <see cref="Topic">forum topic</see> was bumped</param>
    /// <param name="updatedAt">The date when the forum thread was edited</param>
    /// <param name="mentions">The <see cref="Content.Mentions">mentions</see> found in the <see cref="Content">content</see></param>
    /// <param name="isPinned">Whether the <see cref="TopicSummary">forum topic</see> has been pinned</param>
    /// <param name="isLocked">Whether the <see cref="TopicSummary">forum topic</see> has been locked</param>
    /// <returns>New <see cref="Topic" /> JSON instance</returns>
    /// <seealso cref="Topic" />
    [JsonConstructor]
    public Topic(
        [JsonProperty(Required = Required.Always)]
        uint id,

        [JsonProperty(Required = Required.Always)]
        Guid channelId,

        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        string title,

        [JsonProperty(Required = Required.Always)]
        string content,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(Required = Required.Always)]
        DateTime bumpedAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Guid? createdByWebhookId = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? updatedAt = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Mentions? mentions = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool isPinned = false,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool isLocked = false
    ) : base(id, channelId, serverId, title, createdBy, createdAt, bumpedAt, createdByWebhookId, updatedAt, isPinned, isLocked) =>
        (Content, Mentions) = (content, mentions);
    #endregion
}

