using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Client;
using Guilded.Content;
using Guilded.Servers;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when someone adds, removes or edits a <see cref="Content.CalendarEventRsvp">RSVP</see> of a <see cref="CalendarEvent">calendar event</see>.
/// </summary>
/// <seealso cref="CalendarEventEvent" />
/// <seealso cref="CalendarEvent" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="ItemEvent" />
/// <seealso cref="DocEvent" />
/// <seealso cref="ChannelEvent" />
public class CalendarEventRsvpEvent : IChannelBased, IServerBased, ICreatableContent, IUpdatableContent
{
    #region Properties
    /// <summary>
    /// Gets <see cref="Content.CalendarEventRsvp">RSVP</see> of a <see cref="CalendarEvent">calendar event</see> received from the event.
    /// </summary>
    /// <value><see cref="Content.CalendarEventRsvp">Calendar event RSVP</see></value>
    /// <seealso cref="CalendarEventEvent" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="ServerId" />
    public CalendarEventRsvp Rsvp { get; }

    /// <inheritdoc cref="Rsvp" />
    [Obsolete($"Use `{nameof(Rsvp)}` instead")]
    public CalendarEventRsvp CalendarRsvp => Rsvp;

    /// <inheritdoc />
    public HashId ServerId { get; }
    #endregion

    #region Properties Additional
    /// <inheritdoc cref="CalendarEventRsvp.ChannelId" />
    public Guid ChannelId => Rsvp.ChannelId;

    /// <inheritdoc cref="CalendarEventRsvp.EventId" />
    public uint EventId => Rsvp.EventId;

    /// <inheritdoc cref="CalendarEventRsvp.EventId" />
    [Obsolete($"Use `{nameof(EventId)}` instead")]
    public uint CalendarEventId => EventId;

    /// <inheritdoc cref="CalendarEventRsvp.UserId" />
    public HashId UserId => Rsvp.UserId;

    /// <inheritdoc cref="CalendarEventRsvp.Status" />
    public CalendarEventRsvpStatus Status => Rsvp.Status;

    /// <inheritdoc cref="ChannelContent{T, S}.CreatedBy" />
    public HashId CreatedBy => Rsvp.CreatedBy;

    /// <inheritdoc cref="ChannelContent{T, S}.CreatedAt" />
    public DateTime CreatedAt => Rsvp.CreatedAt;

    /// <inheritdoc cref="CalendarEventRsvp.UpdatedBy" />
    public HashId? UpdatedBy => Rsvp.UpdatedBy;

    /// <inheritdoc cref="CalendarEventRsvp.UpdatedAt" />
    public DateTime? UpdatedAt => Rsvp.UpdatedAt;

    /// <inheritdoc cref="IHasParentClient.ParentClient" />
    public AbstractGuildedClient ParentClient => Rsvp.ParentClient;
    #endregion

    #region Properties Event
    /// <inheritdoc cref="CalendarEventRsvp.Updated" />
    public IObservable<CalendarEventRsvpEvent> Updated =>
        Rsvp.Updated;

    /// <inheritdoc cref="CalendarEventRsvp.Deleted" />
    public IObservable<CalendarEventRsvpEvent> Deleted =>
        Rsvp.Deleted;

    /// <inheritdoc cref="CalendarEventRsvp.ManyUpdated" />
    public IObservable<CalendarEventRsvpManyEvent> ManyUpdated =>
        Rsvp.ManyUpdated;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CalendarEventEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="CalendarEventRsvpEvent">calendar RSVP event</see> occurred</param>
    /// <param name="calendarEventRsvp">The <see cref="CalendarEvent">calendar event</see> received from the event</param>
    /// <returns>New <see cref="CalendarEventEvent" /> JSON instance</returns>
    /// <seealso cref="CalendarEventEvent" />
    [JsonConstructor]
    public CalendarEventRsvpEvent(
        [JsonProperty(Required = Required.Always)]
        CalendarEventRsvp calendarEventRsvp,

        [JsonProperty(Required = Required.Always)]
        HashId serverId
    ) =>
        (ServerId, Rsvp) = (serverId, calendarEventRsvp);
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.SetRsvpAsync(Guid, uint, HashId, CalendarEventRsvpStatus)" />
    /// <param name="status">The new status of the <see cref="CalendarEvent">calendar RSVP</see></param>
    public Task<CalendarEventRsvp> SetAsync(CalendarEventRsvpStatus status) =>
        Rsvp.SetAsync(status);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveRsvpAsync(Guid, uint, HashId)" />
    public Task RemoveAsync() =>
        Rsvp.RemoveAsync();
    #endregion
}