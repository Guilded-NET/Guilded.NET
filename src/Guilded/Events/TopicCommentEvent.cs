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
public class TopicCommentEvent : IModelHasId<uint>, IServerBased, ICreatableContent, IUpdatableContent
{
    #region Properties
    /// <summary>
    /// Gets the <see cref="Content.TopicComment">forum topic comment</see> received from the event.
    /// </summary>
    /// <value><see cref="Content.TopicComment">forum topic comment</see> from the <see cref="TopicCommentEvent">event</see></value>
    /// <seealso cref="TopicCommentEvent" />
    /// <seealso cref="ServerId" />
    public TopicComment TopicComment { get; }

    /// <inheritdoc />
    public HashId ServerId { get; }

    /// <inheritdoc cref="TopicComment.Id" />
    public uint Id => TopicComment.Id;

    /// <inheritdoc cref="TopicComment.TopicId" />
    public uint TopicId => TopicComment.TopicId;

    /// <inheritdoc cref="TopicComment.CreatedBy" />
    public HashId CreatedBy => TopicComment.CreatedBy;

    /// <inheritdoc cref="TopicComment.CreatedAt" />
    public DateTime CreatedAt => TopicComment.CreatedAt;

    /// <inheritdoc cref="TopicComment.UpdatedAt" />
    public DateTime? UpdatedAt => TopicComment.UpdatedAt;

    /// <inheritdoc cref="IHasParentClient.ParentClient" />
    public AbstractGuildedClient ParentClient => TopicComment.ParentClient;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="TopicCommentEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="TopicEvent">topic event</see> occurred</param>
    /// <param name="forumTopicComment">The <see cref="Content.TopicComment">forum topic comment</see> received from the event</param>
    /// <returns>New <see cref="TopicEvent" /> JSON instance</returns>
    /// <seealso cref="TopicCommentEvent" />
    [JsonConstructor]
    public TopicCommentEvent(
        [JsonProperty(Required = Required.Always)]
        TopicComment forumTopicComment,

        [JsonProperty(Required = Required.Always)]
        HashId serverId
    ) =>
        (ServerId, TopicComment) = (serverId, forumTopicComment);
    #endregion
}