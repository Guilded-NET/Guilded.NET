using System;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Client;
using Guilded.Content;
using Guilded.Servers;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when someone creates, updates or deletes a <see cref="Content.Topic">forum topic</see>.
/// </summary>
/// <seealso cref="Content.Topic" />
/// <seealso cref="DocEvent" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="ItemEvent" />
/// <seealso cref="CalendarEventEvent" />
/// <seealso cref="ChannelEvent" />
public class TopicEvent : IModelHasId<uint>, IServerBased, IChannelBased, ICreatableContent, IUpdatableContent
{
    #region Properties
    /// <summary>
    /// Gets the <see cref="Content.Topic">forum topic</see> received from the event.
    /// </summary>
    /// <value><see cref="Content.Topic" /></value>
    /// <seealso cref="TopicEvent" />
    /// <seealso cref="Title" />
    /// <seealso cref="ServerId" />
    public Topic Topic { get; }

    /// <inheritdoc />
    public HashId ServerId { get; }
    #endregion

    #region Properties Additional
    /// <inheritdoc cref="ChannelContent{T, S}.Id" />
    public uint Id => Topic.Id;

    /// <inheritdoc cref="ChannelContent{T, S}.ChannelId" />
    public Guid ChannelId => Topic.ChannelId;

    /// <inheritdoc cref="TitledContent{T}.Title" />
    public string Title => Topic.Title;

    /// <inheritdoc cref="Topic.Content" />
    public string Content => Topic.Content;

    /// <inheritdoc cref="Topic.Mentions" />
    public Mentions? Mentions => Topic.Mentions;

    /// <inheritdoc cref="ChannelContent{T, S}.CreatedBy" />
    public HashId CreatedBy => Topic.CreatedBy;

    /// <inheritdoc cref="TopicSummary.CreatedByWebhook" />
    public Guid? CreatedByWebhook => Topic.CreatedByWebhook;

    /// <inheritdoc cref="ChannelContent{T, S}.CreatedAt" />
    public DateTime CreatedAt => Topic.CreatedAt;

    /// <inheritdoc cref="TopicSummary.BumpedAt" />
    public DateTime BumpedAt => Topic.BumpedAt;

    /// <inheritdoc cref="TopicSummary.UpdatedAt" />
    public DateTime? UpdatedAt => Topic.UpdatedAt;

    /// <inheritdoc cref="TopicSummary.IsPinned" />
    public bool IsPinned => Topic.IsPinned;

    /// <inheritdoc cref="TopicSummary.IsLocked" />
    public bool IsLocked => Topic.IsLocked;

    /// <inheritdoc cref="IHasParentClient.ParentClient" />
    public AbstractGuildedClient ParentClient => Topic.ParentClient;
    #endregion

    #region Properties Events
    /// <inheritdoc cref="TopicSummary.Updated" />
    public IObservable<TopicEvent> Updated =>
        Topic.Updated;

    /// <inheritdoc cref="TopicSummary.Deleted" />
    public IObservable<TopicEvent> Deleted =>
        Topic.Deleted;

    /// <inheritdoc cref="TopicSummary.Locked" />
    public IObservable<TopicEvent> Locked =>
        Topic.Locked;

    /// <inheritdoc cref="TopicSummary.Unlocked" />
    public IObservable<TopicEvent> Unlocked =>
        Topic.Unlocked;

    /// <inheritdoc cref="TopicSummary.Pinned" />
    public IObservable<TopicEvent> Pinned =>
        Topic.Pinned;

    /// <inheritdoc cref="TopicSummary.Unpinned" />
    public IObservable<TopicEvent> Unpinned =>
        Topic.Unpinned;

    /// <inheritdoc cref="TopicSummary.CommentCreated" />
    public IObservable<TopicCommentEvent> CommentCreated =>
        Topic.CommentCreated;

    /// <inheritdoc cref="TopicSummary.CommentUpdated" />
    public IObservable<TopicCommentEvent> CommentUpdated =>
        Topic.CommentUpdated;

    /// <inheritdoc cref="TopicSummary.CommentDeleted" />
    public IObservable<TopicCommentEvent> CommentDeleted =>
        Topic.CommentDeleted;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="TopicEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="TopicEvent">forum topic event</see> occurred</param>
    /// <param name="forumTopic">The <see cref="Content.Topic">forum topic</see> received from the event</param>
    /// <returns>New <see cref="TopicEvent" /> JSON instance</returns>
    /// <seealso cref="TopicEvent" />
    [JsonConstructor]
    public TopicEvent(
        [JsonProperty(Required = Required.Always)]
        Topic forumTopic,

        [JsonProperty(Required = Required.Always)]
        HashId serverId
    ) =>
        (ServerId, Topic) = (serverId, forumTopic);
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.UpdateTopicAsync(Guid, uint, string, string)" />
    /// <param name="title">The new title of the <see cref="Content.Topic">forum topic</see></param>
    /// <param name="content">The Markdown content of the <see cref="Content.Topic">forum topic</see></param>
    public Task<Topic> UpdateAsync(string title, string content) =>
        Topic.UpdateAsync(title, content);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteTopicAsync(Guid, uint)" />
    public Task DeleteAsync() =>
        Topic.DeleteAsync();

    /// <inheritdoc cref="AbstractGuildedClient.AddTopicReactionAsync(Guid, uint, uint)" />
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to add</param>
    public Task AddReactionAsync(uint emote) =>
        Topic.AddReactionAsync(emote);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveTopicReactionAsync(Guid, uint, uint)" />
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to remove</param>
    public Task RemoveReactionAsync(uint emote) =>
        Topic.RemoveReactionAsync(emote);
    #endregion

    #region Methods Comments
    /// <inheritdoc cref="AbstractGuildedClient.CreateTopicCommentAsync(Guid, uint, string)" />
    /// <param name="content">The content of the <see cref="DocComment">document comment</see></param>
    public Task<TopicComment> CreateCommentAsync(string content) =>
        Topic.CreateCommentAsync(content);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateTopicCommentAsync(Guid, uint, uint, string)" />
    /// <param name="topicComment">The identifier of the <see cref="TopicComment">forum topic comment</see> to update</param>
    /// <param name="content">The new acontent of the <see cref="TopicComment">forum topic comment</see></param>
    public Task<TopicComment> UpdateCommentAsync(uint topicComment, string content) =>
        Topic.UpdateCommentAsync(topicComment, content);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteTopicCommentAsync(Guid, uint, uint)" />
    /// <param name="topicComment">The identifier of the <see cref="TopicComment">forum topic comment</see> to delete</param>
    public Task DeleteCommentAsync(uint topicComment) =>
        Topic.DeleteCommentAsync(topicComment);
    #endregion
}