// Big yikes for the name

using System;
using System.Drawing;
using System.Threading.Tasks;
using Guilded.Base.Content;
using Guilded.Base.Servers;
using Newtonsoft.Json;

namespace Guilded.Base.Events;

/// <summary>
/// Represents an event that occurs when someone creates, updates or deletes <see cref="Content.CalendarEvent">a calendar event</see>.
/// </summary>
/// <seealso cref="Content.CalendarEvent" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="ListItemEvent" />
/// <seealso cref="DocEvent" />
/// <seealso cref="ChannelEvent" />
public class CalendarEventEvent : BaseModel, IServerEvent
{
    #region Properties
    /// <summary>
    /// Gets <see cref="Content.CalendarEvent">the calendar event</see> received from the event.
    /// </summary>
    /// <value><see cref="Content.CalendarEvent">Calendar event</see></value>
    /// <seealso cref="CalendarEventEvent" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="ServerId" />
    public CalendarEvent CalendarEvent { get; }

    /// <inheritdoc />
    public HashId ServerId { get; }
    #endregion

    #region Properties
    /// <inheritdoc cref="ChannelContent{T, S}.ChannelId" />
    public Guid ChannelId => CalendarEvent.ChannelId;

    /// <inheritdoc cref="CalendarEvent.Name" />
    public string Name => CalendarEvent.Name;

    /// <inheritdoc cref="CalendarEvent.Description" />
    public string? Description => CalendarEvent.Description;

    /// <inheritdoc cref="CalendarEvent.Mentions" />
    public Mentions? Mentions => CalendarEvent.Mentions;

    /// <inheritdoc cref="CalendarEvent.Description" />
    public string? Location => CalendarEvent.Location;

    /// <inheritdoc cref="CalendarEvent.Url" />
    public Uri? Url => CalendarEvent.Url;

    /// <inheritdoc cref="CalendarEvent.Color" />
    public Color? Color => CalendarEvent.Color;

    /// <inheritdoc cref="CalendarEvent.StartsAt" />
    public DateTime StartsAt => CalendarEvent.StartsAt;

    /// <inheritdoc cref="CalendarEvent.Duration" />
    public TimeSpan? Duration => CalendarEvent.Duration;

    /// <inheritdoc cref="CalendarEvent.EndsAt" />
    public DateTime? EndsAt => CalendarEvent.EndsAt;

    /// <inheritdoc cref="ChannelContent{T, S}.CreatedBy" />
    public HashId CreatedBy => CalendarEvent.CreatedBy;

    /// <inheritdoc cref="ChannelContent{T, S}.CreatedAt" />
    public DateTime CreatedAt => CalendarEvent.CreatedAt;

    /// <inheritdoc cref="CalendarEvent.Cancellation" />
    public CalendarCancellation? Cancellation => CalendarEvent.Cancellation;

    /// <inheritdoc cref="CalendarEvent.Cancellation" />
    public HashId? CanceledBy => CalendarEvent.CanceledBy;

    /// <inheritdoc cref="CalendarEvent.IsCanceled" />
    public bool IsCanceled => CalendarEvent.IsCanceled;

    /// <inheritdoc cref="CalendarEvent.IsPrivate" />
    public bool IsPrivate => CalendarEvent.IsPrivate;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CalendarEventEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of <see cref="Server">the server</see> where the doc event occurred</param>
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
        (ServerId, CalendarEvent) = (serverId, calendarEvent);
    #endregion

    #region Methods
    /// <inheritdoc cref="CalendarEvent.UpdateAsync(string?, string?, string?, DateTime?, Uri?, Color?, uint?, bool?)" />
    public async Task<CalendarEvent> UpdateAsync(string? name = null, string? description = null, string? location = null, DateTime? startsAt = null, Uri? url = null, Color? color = null, uint? duration = null, bool? isPrivate = null) =>
        await CalendarEvent.UpdateAsync(name, description, location, startsAt, url, color, duration, isPrivate).ConfigureAwait(false);

    /// <inheritdoc cref="CalendarEvent.UpdateAsync(string?, string?, string?, DateTime?, Uri?, Color?, TimeSpan?, bool?)" />
    public async Task<CalendarEvent> UpdateAsync(string? name = null, string? description = null, string? location = null, DateTime? startsAt = null, Uri? url = null, Color? color = null, TimeSpan? duration = null, bool? isPrivate = null) =>
        await CalendarEvent.UpdateAsync(name, description, location, startsAt, url, color, duration, isPrivate).ConfigureAwait(false);

    /// <inheritdoc cref="CalendarEvent.DeleteAsync" />
    public async Task DeleteAsync() =>
        await CalendarEvent.DeleteAsync().ConfigureAwait(false);

    /// <inheritdoc cref="CalendarEvent.AddReactionAsync(uint)" />
    public async Task AddReactionAsync(uint emoteId) =>
        await CalendarEvent.AddReactionAsync(emoteId).ConfigureAwait(false);

    /// <inheritdoc cref="CalendarEvent.RemoveReactionAsync(uint)" />
    public async Task RemoveReactionAsync(uint emoteId) =>
        await CalendarEvent.RemoveReactionAsync(emoteId).ConfigureAwait(false);
    #endregion
}