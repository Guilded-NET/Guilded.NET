using System;
using Guilded.Base;
using Guilded.Client;
using Guilded.Content;
using Guilded.Servers;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when someone creates, updates or deletes a <see cref="Content.DocComment">document comment</see>.
/// </summary>
/// <seealso cref="Content.DocComment" />
/// <seealso cref="DocEvent" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="ItemEvent" />
/// <seealso cref="CalendarEventEvent" />
/// <seealso cref="ChannelEvent" />
public class DocCommentEvent : IModelHasId<uint>, IServerBased, IChannelBased, ICreatableContent, IUpdatableContent
{
    #region Properties
    /// <summary>
    /// Gets the <see cref="Content.DocComment">document comment</see> received from the event.
    /// </summary>
    /// <value><see cref="Content.DocComment">document comment</see> from the <see cref="DocCommentEvent">event</see></value>
    /// <seealso cref="DocCommentEvent" />
    /// <seealso cref="ServerId" />
    public DocComment DocComment { get; }

    /// <inheritdoc />
    public HashId ServerId { get; }

    /// <inheritdoc cref="BaseComment.Id" />
    public uint Id => DocComment.Id;

    /// <inheritdoc cref="DocComment.DocId" />
    public uint DocId => DocComment.DocId;

    /// <inheritdoc cref="BaseComment.ChannelId" />
    public Guid ChannelId => DocComment.ChannelId;

    /// <inheritdoc cref="BaseComment.CreatedBy" />
    public HashId CreatedBy => DocComment.CreatedBy;

    /// <inheritdoc cref="BaseComment.CreatedAt" />
    public DateTime CreatedAt => DocComment.CreatedAt;

    /// <inheritdoc cref="BaseComment.UpdatedAt" />
    public DateTime? UpdatedAt => DocComment.UpdatedAt;

    /// <inheritdoc cref="IHasParentClient.ParentClient" />
    public AbstractGuildedClient ParentClient => DocComment.ParentClient;

    /// <inheritdoc cref="DocComment.Updated" />
    public IObservable<DocCommentEvent> Updated => DocComment.Updated;

    /// <inheritdoc cref="DocComment.Deleted" />
    public IObservable<DocCommentEvent> Deleted => DocComment.Deleted;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="DocCommentEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="DocCommentEvent">document comment event</see> occurred</param>
    /// <param name="docComment">The <see cref="Content.DocComment">document comment</see> received from the event</param>
    /// <returns>New <see cref="DocCommentEvent" /> JSON instance</returns>
    /// <seealso cref="DocCommentEvent" />
    [JsonConstructor]
    public DocCommentEvent(
        [JsonProperty(Required = Required.Always)]
        DocComment docComment,

        [JsonProperty(Required = Required.Always)]
        HashId serverId
    ) =>
        (ServerId, DocComment) = (serverId, docComment);
    #endregion
}