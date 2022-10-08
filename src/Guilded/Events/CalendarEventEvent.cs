// Big yikes for the name

using System;
using System.Drawing;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Content;
using Guilded.Servers;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when someone creates, updates or deletes <see cref="Content.CalendarEvent">a calendar event</see>.
/// </summary>
/// <seealso cref="CalendarRsvpEvent" />
/// <seealso cref="Content.CalendarEvent" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="ListItemEvent" />
/// <seealso cref="DocEvent" />
/// <seealso cref="ChannelEvent" />
public class CalendarEventEvent
{
    #region Properties
    /// <summary>
    /// Gets <see cref="Content.CalendarEvent">the calendar event</see> received from the event.
    /// </summary>
    /// <value><see cref="Content.CalendarEvent">Calendar event</see></value>
    /// <seealso cref="CalendarEventEvent" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="ServerId" />
    public CalendarEvent Event { get; }

    /// <inheritdoc />
    public HashId ServerId { get; }
    #endregion

    #region Properties Additional
    /// <inheritdoc cref="ChannelContent{T, S}.ChannelId" />
    public Guid ChannelId => Event.ChannelId;

    /// <inheritdoc cref="CalendarEvent.Name" />
    public string Name => Event.Name;

    /// <inheritdoc cref="CalendarEvent.Description" />
    public string? Description => Event.Description;

    /// <inheritdoc cref="CalendarEvent.Mentions" />
    public Mentions? Mentions => Event.Mentions;

    /// <inheritdoc cref="CalendarEvent.Description" />
    public string? Location => Event.Location;

    /// <inheritdoc cref="CalendarEvent.Url" />
    public Uri? Url => Event.Url;

    /// <inheritdoc cref="CalendarEvent.Color" />
    public Color? Color => Event.Color;

    /// <inheritdoc cref="CalendarEvent.StartsAt" />
    public DateTime StartsAt => Event.StartsAt;

    /// <inheritdoc cref="CalendarEvent.Duration" />
    public TimeSpan? Duration => Event.Duration;

    /// <inheritdoc cref="CalendarEvent.EndsAt" />
    public DateTime? EndsAt => Event.EndsAt;

    /// <inheritdoc cref="ChannelContent{T, S}.CreatedBy" />
    public HashId CreatedBy => Event.CreatedBy;

    /// <inheritdoc cref="ChannelContent{T, S}.CreatedAt" />
    public DateTime CreatedAt => Event.CreatedAt;

    /// <inheritdoc cref="CalendarEvent.Cancellation" />
    public CalendarCancellation? Cancellation => Event.Cancellation;

    /// <inheritdoc cref="CalendarEvent.Cancellation" />
    public HashId? CanceledBy => Event.CanceledBy;

    /// <inheritdoc cref="CalendarEvent.IsCanceled" />
    public bool IsCanceled => Event.IsCanceled;

    /// <inheritdoc cref="CalendarEvent.IsPrivate" />
    public bool IsPrivate => Event.IsPrivate;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CalendarEventEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="CalendarEventEvent">calendar event event</see> occurred</param>
    /// <param name="calendarEvent"><see cref="Content.CalendarEvent">The calendar event</see> received from the event</param>
    /// <returns>New <see cref="CalendarEventEvent" /> JSON instance</returns>
    /// <seealso cref="CalendarEventEvent" />
    [JsonConstructor]
    public CalendarEventEvent(
        [JsonProperty(Required = Required.Always)]
        CalendarEvent calendarEvent,

        [JsonProperty(Required = Required.Always)]
        HashId serverId
    ) =>
        (ServerId, Event) = (serverId, calendarEvent);
    #endregion

    #region Methods
    /// <inheritdoc cref="CalendarEvent.UpdateAsync(string?, string?, string?, DateTime?, Uri?, Color?, uint?, bool?)" />
    public Task<CalendarEvent> UpdateAsync(string? name = null, string? description = null, string? location = null, DateTime? startsAt = null, Uri? url = null, Color? color = null, uint? duration = null, bool? isPrivate = null) =>
        Event.UpdateAsync(name, description, location, startsAt, url, color, duration, isPrivate);

    /// <inheritdoc cref="CalendarEvent.UpdateAsync(string?, string?, string?, DateTime?, Uri?, Color?, TimeSpan?, bool?)" />
    public Task<CalendarEvent> UpdateAsync(string? name = null, string? description = null, string? location = null, DateTime? startsAt = null, Uri? url = null, Color? color = null, TimeSpan? duration = null, bool? isPrivate = null) =>
        Event.UpdateAsync(name, description, location, startsAt, url, color, duration, isPrivate);

    /// <inheritdoc cref="CalendarEvent.DeleteAsync" />
    public Task DeleteAsync() =>
        Event.DeleteAsync();

    /// <inheritdoc cref="CalendarEvent.AddReactionAsync(uint)" />
    public Task AddReactionAsync(uint emoteId) =>
        Event.AddReactionAsync(emoteId);

    /// <inheritdoc cref="CalendarEvent.RemoveReactionAsync(uint)" />
    public Task RemoveReactionAsync(uint emoteId) =>
        Event.RemoveReactionAsync(emoteId);
    #endregion
}