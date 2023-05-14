using System;
using Guilded.Base;
using Guilded.Client;
using Guilded.Content;
using Guilded.Events;
using Guilded.Servers;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when someone creates, updates or deletes a <see cref="Content.AnnouncementComment">document comment</see>.
/// </summary>
/// <seealso cref="Content.AnnouncementComment" />
/// <seealso cref="DocEvent" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="ItemEvent" />
/// <seealso cref="CalendarEventEvent" />
/// <seealso cref="ChannelEvent" />
public class AnnouncementCommentEvent : IModelHasId<uint>, IServerBased, IChannelBased, ICreatableContent, IUpdatableContent
{
    #region Properties
    /// <summary>
    /// Gets the <see cref="Content.AnnouncementComment">announcement comment</see> received from the event.
    /// </summary>
    /// <value><see cref="Content.AnnouncementComment">announcement comment</see> from the <see cref="AnnouncementCommentEvent">event</see></value>
    /// <seealso cref="AnnouncementCommentEvent" />
    /// <seealso cref="ServerId" />
    public AnnouncementComment AnnouncementComment { get; }

    /// <inheritdoc />
    public HashId ServerId { get; }

    /// <inheritdoc cref="BaseComment.Id" />
    public uint Id => AnnouncementComment.Id;

    /// <inheritdoc cref="AnnouncementComment.AnnouncementId" />
    public HashId AnnouncementId => AnnouncementComment.AnnouncementId;

    /// <inheritdoc cref="BaseComment.ChannelId" />
    public Guid ChannelId => AnnouncementComment.ChannelId;

    /// <inheritdoc cref="BaseComment.CreatedBy" />
    public HashId CreatedBy => AnnouncementComment.CreatedBy;

    /// <inheritdoc cref="BaseComment.CreatedAt" />
    public DateTime CreatedAt => AnnouncementComment.CreatedAt;

    /// <inheritdoc cref="BaseComment.UpdatedAt" />
    public DateTime? UpdatedAt => AnnouncementComment.UpdatedAt;

    /// <inheritdoc cref="IHasParentClient.ParentClient" />
    public AbstractGuildedClient ParentClient => AnnouncementComment.ParentClient;

    /// <inheritdoc cref="AnnouncementComment.Updated" />
    public IObservable<AnnouncementCommentEvent> Updated => AnnouncementComment.Updated;

    /// <inheritdoc cref="AnnouncementComment.Deleted" />
    public IObservable<AnnouncementCommentEvent> Deleted => AnnouncementComment.Deleted;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="AnnouncementCommentEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="AnnouncementCommentEvent">announcement comment event</see> occurred</param>
    /// <param name="announcementComment">The <see cref="Content.AnnouncementComment">announcement comment</see> received from the event</param>
    /// <returns>New <see cref="AnnouncementEvent" /> JSON instance</returns>
    /// <seealso cref="AnnouncementCommentEvent" />
    [JsonConstructor]
    public AnnouncementCommentEvent(
        [JsonProperty(Required = Required.Always)]
        AnnouncementComment announcementComment,

        [JsonProperty(Required = Required.Always)]
        HashId serverId
    ) =>
        (ServerId, AnnouncementComment) = (serverId, announcementComment);
    #endregion
}