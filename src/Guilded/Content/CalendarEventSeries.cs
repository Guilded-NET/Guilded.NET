using System;
using Guilded.Base;
using Guilded.Servers;

namespace Guilded.Content;

/// <summary>
/// Represents series of repeated <see cref="CalendarEvent">calendar events</see>.
/// </summary>
/// <seealso cref="CalendarEvent" />
/// <seealso cref="CalendarEventRepetition" />
/// <seealso cref="CalendarEventInterval" />
public class CalendarEventSeries : ContentModel, IModelHasId<Guid>, IChannelBased, IServerBased
{
    /// <summary>
    /// Gets the identifier of the <see cref="Server">server</see> where the <see cref="CalendarEventSeries">calendar event series</see> are.
    /// </summary>
    /// <value>The identifier of the <see cref="Server">server</see> where the <see cref="CalendarEventSeries">calendar event series</see> are</value>
    /// <seealso cref="CalendarEventSeries" />
    /// <seealso cref="Id" />
    /// <seealso cref="ChannelId" />
    public HashId ServerId { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="CalendarChannel">channel</see> where the <see cref="CalendarEventSeries">calendar event series</see> are.
    /// </summary>
    /// <value>The identifier of the <see cref="CalendarChannel">channel</see> where the <see cref="CalendarEventSeries">calendar event series</see> are</value>
    /// <seealso cref="CalendarEventSeries" />
    /// <seealso cref="Id" />
    /// <seealso cref="ServerId" />
    public Guid ChannelId { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="CalendarEventSeries">calendar event series</see>.
    /// </summary>
    /// <value>The identifier of the <see cref="CalendarEventSeries">calendar event series</see></value>
    /// <seealso cref="CalendarEventSeries" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="ServerId" />
    public Guid Id { get; }
}