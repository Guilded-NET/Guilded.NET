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
    /// Gets the <see cref="Content.Topic">topic</see> received from the event.
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

    /// <inheritdoc cref="TitledContent.Title" />
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

    /// <inheritdoc cref="TitledContent.UpdatedAt" />
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
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="TopicEvent">topic event</see> occurred</param>
    /// <param name="forumTopic">The <see cref="Content.Topic">topic</see> received from the event</param>
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
    /// <inheritdoc cref="TopicSummary.UpdateAsync(string, string)" />
    public Task<Topic> UpdateAsync(string title, string content) =>
        Topic.UpdateAsync(title, content);

    /// <inheritdoc cref="TopicSummary.DeleteAsync" />
    public Task DeleteAsync() =>
        Topic.DeleteAsync();

    /// <inheritdoc cref="TitledContent.AddReactionAsync(uint)" />
    public Task AddReactionAsync(uint emoteId) =>
        Topic.AddReactionAsync(emoteId);

    /// <inheritdoc cref="TitledContent.RemoveReactionAsync(uint)" />
    public Task RemoveReactionAsync(uint emoteId) =>
        Topic.RemoveReactionAsync(emoteId);
    #endregion
}