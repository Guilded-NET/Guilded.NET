using System;
using System.Threading.Tasks;
using Guilded.Base.Content;
using Guilded.Base.Servers;
using Newtonsoft.Json;

namespace Guilded.Base.Events;

/// <summary>
/// Represents an event that occurs when someone creates, updates or deletes a <see cref="Content.Topic">forum topic</see>.
/// </summary>
/// <seealso cref="Content.Topic" />
/// <seealso cref="DocEvent" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="ListItemEvent" />
/// <seealso cref="CalendarEventEvent" />
/// <seealso cref="ChannelEvent" />
public class TopicEvent : BaseModel
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

    #region Properties
    /// <inheritdoc cref="ChannelContent{T, S}.ChannelId" />
    public Guid ChannelId => Topic.ChannelId;

    /// <inheritdoc cref="TitledContent.Title" />
    public string Title => Topic.Title;

    /// <inheritdoc cref="Topic.Content" />
    public string Content => Topic.Content;

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
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="TopicEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of <see cref="Server">the server</see> where the <see cref="TopicEvent">topic event</see> occurred</param>
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
    public async Task<Topic> UpdateAsync(string title, string content) =>
        await Topic.UpdateAsync(title, content).ConfigureAwait(false);

    /// <inheritdoc cref="TopicSummary.DeleteAsync" />
    public async Task DeleteAsync() =>
        await Topic.DeleteAsync().ConfigureAwait(false);

    /// <inheritdoc cref="TitledContent.AddReactionAsync(uint)" />
    public async Task AddReactionAsync(uint emoteId) =>
        await Topic.AddReactionAsync(emoteId).ConfigureAwait(false);

    /// <inheritdoc cref="TitledContent.RemoveReactionAsync(uint)" />
    public async Task RemoveReactionAsync(uint emoteId) =>
        await Topic.RemoveReactionAsync(emoteId).ConfigureAwait(false);
    #endregion
}