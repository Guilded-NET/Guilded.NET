using System;
using System.Collections.Generic;
using System.Linq;
using Guilded.Base;
using Guilded.Client;
using Guilded.Content;
using Guilded.Servers;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when someone adds, removes or edits multiple <see cref="Content.CalendarEventRsvp">RSVPs</see> of a <see cref="CalendarEvent">calendar event</see>.
/// </summary>
/// <seealso cref="CalendarEventRsvpEvent" />
/// <seealso cref="CalendarEventEvent" />
/// <seealso cref="CalendarEvent" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="ItemEvent" />
/// <seealso cref="DocEvent" />
/// <seealso cref="ChannelEvent" />
public class CalendarEventRsvpManyEvent : IChannelBased, IServerBased, ICreatableContent, IUpdatableContent
{
    #region Properties
    /// <summary>
    /// Gets the list of <see cref="CalendarEventRsvp">RSVPs</see> of a <see cref="CalendarEvent">calendar event</see> received from the event.
    /// </summary>
    /// <value>List of <see cref="CalendarEventRsvp">Calendar event RSVPs</see></value>
    /// <seealso cref="CalendarEventEvent" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="ServerId" />
    public IList<CalendarEventRsvp> Rsvps { get; }

    /// <inheritdoc cref="Rsvps" />
    [Obsolete($"Use `{nameof(Rsvps)}` instead")]
    public IList<CalendarEventRsvp> CalendarRsvps => Rsvps;

    /// <inheritdoc />
    public HashId ServerId { get; }
    #endregion

    #region Properties Additional
    /// <summary>
    /// Gets the first <see cref="CalendarEventRsvp">RSVP</see> in a <see cref="CalendarEvent">calendar event</see>.
    /// </summary>
    /// <returns>First <see cref="CalendarEventRsvp">RSVP</see></returns>
    /// <seealso cref="Last" />
    /// <seealso cref="Count" />
    public CalendarEventRsvp First => Rsvps.First();

    /// <summary>
    /// Gets the last <see cref="CalendarEventRsvp">RSVP</see> in a <see cref="CalendarEvent">calendar event</see>.
    /// </summary>
    /// <returns>Last <see cref="CalendarEventRsvp">RSVP</see></returns>
    /// <seealso cref="First" />
    /// <seealso cref="Count" />
    public CalendarEventRsvp Last => Rsvps.Last();

    /// <summary>
    /// Gets the count of how many <see cref="CalendarEventRsvp">RSVPs</see> have been changed.
    /// </summary>
    /// <returns>Count of <see cref="CalendarEventRsvp">RSVPs</see></returns>
    /// <seealso cref="First" />
    /// <seealso cref="Last" />
    public int Count => Rsvps.Count;

    /// <inheritdoc cref="CalendarEventRsvp.ChannelId" />
    public Guid ChannelId => First.ChannelId;

    /// <inheritdoc cref="CalendarEventRsvp.EventId" />
    public uint EventId => First.EventId;

    /// <inheritdoc cref="CalendarEventRsvp.EventId" />
    [Obsolete($"Use `{nameof(EventId)}` instead")]
    public uint CalendarEventId => EventId;

    /// <inheritdoc cref="CalendarEventRsvp.UserId" />
    public HashId UserId => First.UserId;

    /// <inheritdoc cref="CalendarEventRsvp.Status" />
    public CalendarEventRsvpStatus Status => First.Status;

    /// <inheritdoc cref="ChannelContent{T, S}.CreatedBy" />
    public HashId CreatedBy => First.CreatedBy;

    /// <inheritdoc cref="ChannelContent{T, S}.CreatedAt" />
    public DateTime CreatedAt => First.CreatedAt;

    /// <inheritdoc cref="CalendarEventRsvp.UpdatedBy" />
    public HashId? UpdatedBy => First.UpdatedBy;

    /// <inheritdoc cref="CalendarEventRsvp.UpdatedAt" />
    public DateTime? UpdatedAt => First.UpdatedAt;

    /// <inheritdoc cref="IHasParentClient.ParentClient" />
    public AbstractGuildedClient ParentClient => First.ParentClient;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CalendarEventEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="CalendarEventRsvpManyEvent">calendar RSVP event</see> occurred</param>
    /// <param name="calendarEventRsvps">The list of <see cref="CalendarEventRsvp">RSVPs</see> of a <see cref="CalendarEvent">calendar event</see> received from the event</param>
    /// <returns>New <see cref="CalendarEventEvent" /> JSON instance</returns>
    /// <seealso cref="CalendarEventEvent" />
    [JsonConstructor]
    public CalendarEventRsvpManyEvent(
        [JsonProperty(Required = Required.Always)]
        IList<CalendarEventRsvp> calendarEventRsvps,

        [JsonProperty(Required = Required.Always)]
        HashId serverId
    ) =>
        (ServerId, Rsvps) = (serverId, calendarEventRsvps);
    #endregion
}