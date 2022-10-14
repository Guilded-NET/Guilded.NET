using System;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Client;
using Guilded.Content;
using Guilded.Servers;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when someone adds, removes or edits a <see cref="Content.CalendarRsvp">RSVP</see> of a <see cref="CalendarEvent">calendar event</see>.
/// </summary>
/// <seealso cref="CalendarEventEvent" />
/// <seealso cref="CalendarEvent" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="ListItemEvent" />
/// <seealso cref="DocEvent" />
/// <seealso cref="ChannelEvent" />
public class CalendarRsvpEvent : IChannelBased, IServerBased, ICreatableContent, IUpdatableContent
{
    #region Properties
    /// <summary>
    /// Gets <see cref="Content.CalendarRsvp">RSVP</see> of a <see cref="CalendarEvent">calendar event</see> received from the event.
    /// </summary>
    /// <value><see cref="Content.CalendarRsvp">Calendar event RSVP</see></value>
    /// <seealso cref="CalendarEventEvent" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="ServerId" />
    public CalendarRsvp Rsvp { get; }

    /// <inheritdoc cref="Rsvp" />
    [Obsolete($"Use `{nameof(Rsvp)}` instead")]
    public CalendarRsvp CalendarRsvp => Rsvp;

    /// <inheritdoc />
    public HashId ServerId { get; }
    #endregion

    #region Properties Additional
    /// <inheritdoc cref="CalendarRsvp.ChannelId" />
    public Guid ChannelId => Rsvp.ChannelId;

    /// <inheritdoc cref="CalendarRsvp.EventId" />
    public uint EventId => Rsvp.EventId;

    /// <inheritdoc cref="CalendarRsvp.EventId" />
    [Obsolete($"Use `{nameof(EventId)}` instead")]
    public uint CalendarEventId => EventId;

    /// <inheritdoc cref="CalendarRsvp.UserId" />
    public HashId UserId => Rsvp.UserId;

    /// <inheritdoc cref="CalendarRsvp.Status" />
    public CalendarRsvpStatus Status => Rsvp.Status;

    /// <inheritdoc cref="ChannelContent{T, S}.CreatedBy" />
    public HashId CreatedBy => Rsvp.CreatedBy;

    /// <inheritdoc cref="ChannelContent{T, S}.CreatedAt" />
    public DateTime CreatedAt => Rsvp.CreatedAt;

    /// <inheritdoc cref="CalendarRsvp.UpdatedBy" />
    public HashId? UpdatedBy => Rsvp.UpdatedBy;

    /// <inheritdoc cref="CalendarRsvp.UpdatedAt" />
    public DateTime? UpdatedAt => Rsvp.UpdatedAt;

    /// <inheritdoc cref="IHasParentClient.ParentClient" />
    public AbstractGuildedClient ParentClient => Rsvp.ParentClient;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CalendarEventEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="CalendarRsvpEvent">calendar RSVP event</see> occurred</param>
    /// <param name="calendarEventRsvp"><see cref="CalendarEvent">The calendar event</see> received from the event</param>
    /// <returns>New <see cref="CalendarEventEvent" /> JSON instance</returns>
    /// <seealso cref="CalendarEventEvent" />
    [JsonConstructor]
    public CalendarRsvpEvent(
        [JsonProperty(Required = Required.Always)]
        CalendarRsvp calendarEventRsvp,

        [JsonProperty(Required = Required.Always)]
        HashId serverId
    ) =>
        (ServerId, Rsvp) = (serverId, calendarEventRsvp);
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.SetRsvpAsync(Guid, uint, HashId, CalendarRsvpStatus)" />
    /// <param name="status">The new status of <see cref="CalendarEvent">the RSVP</see></param>
    public Task<CalendarRsvp> SetAsync(CalendarRsvpStatus status) =>
        Rsvp.SetAsync(status);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveRsvpAsync(Guid, uint, HashId)" />
    public Task RemoveAsync() =>
        Rsvp.RemoveAsync();
    #endregion
}