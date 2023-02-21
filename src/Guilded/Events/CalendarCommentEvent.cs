using System;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Client;
using Guilded.Content;
using Guilded.Servers;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when someone adds, removes or edits a <see cref="EventComment">comment</see> of a <see cref="CalendarEvent">calendar event</see>.
/// </summary>
/// <seealso cref="EventComment" />
/// <seealso cref="CalendarEventEvent" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="ItemEvent" />
/// <seealso cref="DocEvent" />
/// <seealso cref="ChannelEvent" />
public class CalendarEventCommentEvent : IChannelBased, IServerBased, ICreatableContent, IUpdatableContent
{
    #region Properties
    /// <summary>
    /// Gets <see cref="EventComment">calendar event comment</see> received from the event.
    /// </summary>
    /// <value><see cref="EventComment">Calendar event comment</see></value>
    /// <seealso cref="CalendarEventEvent" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="ServerId" />
    public CalendarEventComment EventComment { get; }

    /// <inheritdoc />
    public HashId ServerId { get; }
    #endregion

    #region Properties Additional
    /// <inheritdoc cref="BaseComment.ChannelId" />
    public Guid ChannelId => EventComment.ChannelId;

    /// <inheritdoc cref="CalendarEventComment.EventId" />
    public uint EventId => EventComment.EventId;

    /// <inheritdoc cref="BaseComment.Id" />
    public uint Id => EventComment.Id;

    /// <inheritdoc cref="BaseComment.Content" />
    public string Content => EventComment.Content;

    /// <inheritdoc cref="BaseComment.CreatedBy" />
    public HashId CreatedBy => EventComment.CreatedBy;

    /// <inheritdoc cref="BaseComment.CreatedAt" />
    public DateTime CreatedAt => EventComment.CreatedAt;

    /// <inheritdoc cref="BaseComment.UpdatedAt" />
    public DateTime? UpdatedAt => EventComment.UpdatedAt;

    /// <inheritdoc cref="IHasParentClient.ParentClient" />
    public AbstractGuildedClient ParentClient => EventComment.ParentClient;
    #endregion

    #region Properties Event
    /// <inheritdoc cref="CalendarEventComment.Updated" />
    public IObservable<CalendarEventCommentEvent> Updated =>
        EventComment.Updated;

    /// <inheritdoc cref="CalendarEventComment.Deleted" />
    public IObservable<CalendarEventCommentEvent> Deleted =>
        EventComment.Deleted;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CalendarEventEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="CalendarEventRsvpEvent">calendar RSVP event</see> occurred</param>
    /// <param name="calendarEventComment">The <see cref="CalendarEvent">calendar event</see> received from the event</param>
    /// <returns>New <see cref="CalendarEventEvent" /> JSON instance</returns>
    /// <seealso cref="CalendarEventEvent" />
    [JsonConstructor]
    public CalendarEventCommentEvent(
        [JsonProperty(Required = Required.Always)]
        CalendarEventComment calendarEventComment,

        [JsonProperty(Required = Required.Always)]
        HashId serverId
    ) =>
        (ServerId, EventComment) = (serverId, calendarEventComment);
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.CreateEventCommentAsync(Guid, uint, string)" />
    /// <param name="content">The content of the <see cref="EventComment">calendar event comment</see></param>
    public Task<CalendarEventComment> ReplyAsync(string content) =>
        EventComment.ReplyAsync(content);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateEventCommentAsync(Guid, uint, uint, string)" />
    /// <param name="content">The new Markdown content of the <see cref="EventComment">calendar event comment</see></param>
    public Task<CalendarEventComment> UpdateAsync(string content) =>
        EventComment.UpdateAsync(content);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteEventCommentAsync(Guid, uint, uint)" />
    public Task DeleteAsync() =>
        EventComment.DeleteAsync();
    #endregion
}