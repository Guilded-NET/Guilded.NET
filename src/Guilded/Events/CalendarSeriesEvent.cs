using Guilded.Base;
using Guilded.Client;
using Guilded.Content;
using Guilded.Servers;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when someone updates or deletes a <see cref="CalendarEventSeries">calendar event series</see>.
/// </summary>
/// <seealso cref="CalendarEventSeries" /> 
/// <seealso cref="CalendarEventEvent" /> 
public class CalendarEventSeriesEvent : IServerBased
{
    #region Properties
    /// <inheritdoc cref="CalendarEventSeries.ServerId" />
    public HashId ServerId { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="CalendarEvent">calendar event</see> that the <see cref="CalendarEventSeries">series</see> started at.
    /// </summary>
    /// <value>The identifier of the <see cref="CalendarEvent">calendar event</see> that the <see cref="CalendarEventSeries">series</see> started at</value>
    /// <seealso cref="CalendarEventSeriesEvent" />
    /// <seealso cref="ServerId" />
    /// <seealso cref="EventSeries" />
    public uint? EventId { get; }

    /// <summary>
    /// Gets the <see cref="CalendarEventSeries">calendar event series</see> received from the <see cref="CalendarEventSeriesEvent">event</see>.
    /// </summary>
    /// <value>The <see cref="CalendarEventSeries">calendar event series</see> received from the <see cref="CalendarEventSeriesEvent">event</see></value>
    /// <seealso cref="CalendarEventSeriesEvent" />
    /// <seealso cref="ServerId" />
    /// <seealso cref="EventId" />
    public CalendarEventSeries EventSeries { get; }

    /// <inheritdoc cref="IHasParentClient.ParentClient" />
    public AbstractGuildedClient ParentClient => EventSeries.ParentClient;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CalendarEventSeriesEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="CalendarEventSeries">calendar event series</see> are</param>
    /// <param name="calendarEventId">The identifier of the <see cref="CalendarEvent">calendar event</see> that the <see cref="CalendarEventSeries">series</see> started at</param>
    /// <param name="calendarEventSeries">The <see cref="CalendarEventSeries">calendar event series</see> received from the <see cref="CalendarEventSeriesEvent">event</see></param>
    /// <returns>New <see cref="CalendarEventSeriesEvent" /> JSON instance</returns>
    /// <seealso cref="CalendarEventSeriesEvent" />
    [JsonConstructor]
    public CalendarEventSeriesEvent(
        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        CalendarEventSeries calendarEventSeries,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        uint? calendarEventId
    ) =>
        (ServerId, EventSeries, EventId) = (serverId, calendarEventSeries, calendarEventId);
    #endregion
}