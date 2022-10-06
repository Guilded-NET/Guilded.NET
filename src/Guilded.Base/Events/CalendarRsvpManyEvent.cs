using System;
using System.Collections.Generic;
using System.Linq;
using Guilded.Base.Content;
using Guilded.Base.Servers;
using Newtonsoft.Json;

namespace Guilded.Base.Events;

/// <summary>
/// Represents an event that occurs when someone adds, removes or edits multiple <see cref="Content.CalendarRsvp">RSVPs</see> of a <see cref="CalendarEvent">calendar event</see>.
/// </summary>
/// <seealso cref="CalendarRsvpEvent" />
/// <seealso cref="CalendarEventEvent" />
/// <seealso cref="CalendarEvent" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="ListItemEvent" />
/// <seealso cref="DocEvent" />
/// <seealso cref="ChannelEvent" />
public class CalendarRsvpManyEvent
{
    #region Properties
    /// <summary>
    /// Gets the list of <see cref="CalendarRsvp">RSVPs</see> of a <see cref="CalendarEvent">calendar event</see> received from the event.
    /// </summary>
    /// <value>List of <see cref="CalendarRsvp">Calendar event RSVPs</see></value>
    /// <seealso cref="CalendarEventEvent" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="ServerId" />
    public IList<CalendarRsvp> CalendarRsvps { get; }

    /// <inheritdoc />
    public HashId ServerId { get; }
    #endregion

    #region Properties
    /// <summary>
    /// Gets the first <see cref="CalendarRsvp">RSVP</see> in a <see cref="CalendarEvent">calendar event</see>.
    /// </summary>
    /// <returns>First <see cref="CalendarRsvp">RSVP</see></returns>
    /// <seealso cref="Last" />
    /// <seealso cref="Count" />
    public CalendarRsvp First => CalendarRsvps.First();

    /// <summary>
    /// Gets the last <see cref="CalendarRsvp">RSVP</see> in a <see cref="CalendarEvent">calendar event</see>.
    /// </summary>
    /// <returns>Last <see cref="CalendarRsvp">RSVP</see></returns>
    /// <seealso cref="First" />
    /// <seealso cref="Count" />
    public CalendarRsvp Last => CalendarRsvps.Last();

    /// <summary>
    /// Gets the count of how many <see cref="CalendarRsvp">RSVPs</see> have been changed.
    /// </summary>
    /// <returns>Count of <see cref="CalendarRsvp">RSVPs</see></returns>
    /// <seealso cref="First" />
    /// <seealso cref="Last" />
    public int Count => CalendarRsvps.Count;

    /// <inheritdoc cref="CalendarRsvp.ChannelId" />
    public Guid ChannelId => First.ChannelId;

    /// <inheritdoc cref="CalendarRsvp.CalendarEventId" />
    public uint CalendarEventId => First.CalendarEventId;

    /// <inheritdoc cref="CalendarRsvp.UserId" />
    public HashId UserId => First.UserId;

    /// <inheritdoc cref="CalendarRsvp.Status" />
    public CalendarRsvpStatus Status => First.Status;

    /// <inheritdoc cref="ChannelContent{T, S}.CreatedBy" />
    public HashId CreatedBy => First.CreatedBy;

    /// <inheritdoc cref="ChannelContent{T, S}.CreatedAt" />
    public DateTime CreatedAt => First.CreatedAt;

    /// <inheritdoc cref="CalendarRsvp.UpdatedBy" />
    public HashId? UpdatedBy => First.UpdatedBy;

    /// <inheritdoc cref="CalendarRsvp.UpdatedAt" />
    public DateTime? UpdatedAt => First.UpdatedAt;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CalendarEventEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="CalendarRsvpManyEvent">calendar RSVP event</see> occurred</param>
    /// <param name="calendarEventRsvps">The list of <see cref="CalendarRsvp">RSVPs</see> of a <see cref="CalendarEvent">calendar event</see> received from the event</param>
    /// <returns>New <see cref="CalendarEventEvent" /> JSON instance</returns>
    /// <seealso cref="CalendarEventEvent" />
    [JsonConstructor]
    public CalendarRsvpManyEvent(
        [JsonProperty(Required = Required.Always)]
        IList<CalendarRsvp> calendarEventRsvps,

        [JsonProperty(Required = Required.Always)]
        HashId serverId
    ) =>
        (ServerId, CalendarRsvps) = (serverId, calendarEventRsvps);
    #endregion
}