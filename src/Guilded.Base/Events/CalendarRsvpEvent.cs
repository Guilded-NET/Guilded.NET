using System;
using System.Threading.Tasks;
using Guilded.Base.Client;
using Guilded.Base.Content;
using Guilded.Base.Servers;
using Newtonsoft.Json;

namespace Guilded.Base.Events;

/// <summary>
/// Represents an event that occurs when someone adds, removes or edits a <see cref="Content.CalendarRsvp">RSVP</see> of a <see cref="CalendarEvent">calendar event</see>.
/// </summary>
/// <seealso cref="CalendarEventEvent" />
/// <seealso cref="CalendarEvent" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="ListItemEvent" />
/// <seealso cref="DocEvent" />
/// <seealso cref="ChannelEvent" />
public class CalendarRsvpEvent
{
    #region Properties
    /// <summary>
    /// Gets <see cref="Content.CalendarRsvp">RSVP</see> of a <see cref="CalendarEvent">calendar event</see> received from the event.
    /// </summary>
    /// <value><see cref="Content.CalendarRsvp">Calendar event RSVP</see></value>
    /// <seealso cref="CalendarEventEvent" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="ServerId" />
    public CalendarRsvp CalendarRsvp { get; }

    /// <inheritdoc />
    public HashId ServerId { get; }
    #endregion

    #region Properties
    /// <inheritdoc cref="CalendarRsvp.ChannelId" />
    public Guid ChannelId => CalendarRsvp.ChannelId;

    /// <inheritdoc cref="CalendarRsvp.CalendarEventId" />
    public uint CalendarEventId => CalendarRsvp.CalendarEventId;

    /// <inheritdoc cref="CalendarRsvp.UserId" />
    public HashId UserId => CalendarRsvp.UserId;

    /// <inheritdoc cref="CalendarRsvp.Status" />
    public CalendarRsvpStatus Status => CalendarRsvp.Status;

    /// <inheritdoc cref="ChannelContent{T, S}.CreatedBy" />
    public HashId CreatedBy => CalendarRsvp.CreatedBy;

    /// <inheritdoc cref="ChannelContent{T, S}.CreatedAt" />
    public DateTime CreatedAt => CalendarRsvp.CreatedAt;

    /// <inheritdoc cref="CalendarRsvp.UpdatedBy" />
    public HashId? UpdatedBy => CalendarRsvp.UpdatedBy;

    /// <inheritdoc cref="CalendarRsvp.UpdatedAt" />
    public DateTime? UpdatedAt => CalendarRsvp.UpdatedAt;
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
        (ServerId, CalendarRsvp) = (serverId, calendarEventRsvp);
    #endregion

    #region Methods
    /// <inheritdoc cref="BaseGuildedClient.SetRsvpAsync(Guid, uint, HashId, CalendarRsvpStatus)" />
    /// <param name="status">The new status of <see cref="CalendarEvent">the RSVP</see></param>
    public Task<CalendarRsvp> SetAsync(CalendarRsvpStatus status) =>
        CalendarRsvp.SetAsync(status);

    /// <inheritdoc cref="BaseGuildedClient.RemoveRsvpAsync(Guid, uint, HashId)" />
    public Task RemoveAsync() =>
        CalendarRsvp.RemoveAsync();
    #endregion
}