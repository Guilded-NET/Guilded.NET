// Big yikes for the name

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Client;
using Guilded.Content;
using Guilded.Servers;
using Guilded.Users;
using Newtonsoft.Json;
using SystemColor = System.Drawing.Color;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when someone creates, updates or deletes a <see cref="CalendarEvent">calendar event</see>.
/// </summary>
/// <seealso cref="CalendarEventRsvpEvent" />
/// <seealso cref="CalendarEvent" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="ItemEvent" />
/// <seealso cref="DocEvent" />
/// <seealso cref="ChannelEvent" />
public class CalendarEventEvent : IModelHasId<uint>, IPrivatableContent, IServerBased, IChannelBased, ICreatableContent
{
    #region Properties
    /// <summary>
    /// Gets the <see cref="CalendarEvent">calendar event</see> received from the event.
    /// </summary>
    /// <value>The <see cref="CalendarEvent">calendar event</see> received from the event</value>
    /// <seealso cref="CalendarEventEvent" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="ServerId" />
    public CalendarEvent Event { get; }

    /// <inheritdoc />
    public HashId ServerId { get; }
    #endregion

    #region Properties Additional
    /// <inheritdoc cref="ChannelContent{T, S}.Id" />
    public uint Id => Event.Id;

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
    public SystemColor? Color => Event.Color;

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
    public CalendarEventCancellation? Cancellation => Event.Cancellation;

    /// <inheritdoc cref="CalendarEvent.Cancellation" />
    public HashId? CanceledBy => Event.CanceledBy;

    /// <inheritdoc cref="CalendarEvent.IsCanceled" />
    public bool IsCanceled => Event.IsCanceled;

    /// <inheritdoc cref="CalendarEvent.IsPrivate" />
    public bool IsPrivate => Event.IsPrivate;

    /// <inheritdoc cref="IHasParentClient.ParentClient" />
    public AbstractGuildedClient ParentClient => Event.ParentClient;
    #endregion

    #region Properties Event
    /// <inheritdoc cref="CalendarEvent.Updated" />
    public IObservable<CalendarEventEvent> Updated =>
        Event.Updated;

    /// <inheritdoc cref="CalendarEvent.Deleted" />
    public IObservable<CalendarEventEvent> Deleted =>
        Event.Deleted;

    /// <inheritdoc cref="CalendarEvent.RsvpUpdated" />
    public IObservable<CalendarEventRsvpEvent> RsvpUpdated =>
        Event.RsvpUpdated;

    /// <inheritdoc cref="CalendarEvent.RsvpDeleted" />
    public IObservable<CalendarEventRsvpEvent> RsvpDeleted =>
        Event.RsvpDeleted;

    /// <inheritdoc cref="CalendarEvent.RsvpManyUpdated" />
    public IObservable<CalendarEventRsvpManyEvent> RsvpManyUpdated =>
        Event.RsvpManyUpdated;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CalendarEventEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="CalendarEventEvent">calendar event event</see> occurred</param>
    /// <param name="calendarEvent"><see cref="CalendarEvent">The calendar event</see> received from the event</param>
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

    #region Methods Event
    /// <inheritdoc cref="AbstractGuildedClient.UpdateEventAsync(Guid, uint, string?, string?, string?, DateTime?, Uri?, SystemColor?, TimeSpan?, uint?, bool?, bool?, bool?, bool?, IList{uint}?)" />
    /// <param name="name">The new name of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="description">The new description of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="location">The new location of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="startsAt">The new starting date of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="url">The new URL of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="color">The new colour of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="duration">The new length/duration of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="rsvpLimit">The limit of how many <see cref="User">users</see> can be invited or attend the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="isPrivate">Whether the <see cref="CalendarEvent">calendar event</see> is now private or not private anymore</param>
    /// <param name="rsvpDisabled">Whether <see cref="Member">members</see> can attend the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="autofillWaitlist">Whether <see cref="Member">members</see> in the waitlist should be added to the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="isAllDay">Whether the <see cref="CalendarEvent">calendar event</see> lasts all day</param>
    /// <param name="roleIds">The list of identifiers of roles to restrict <see cref="CalendarEvent">calendar events</see></param>
    public Task<CalendarEvent> UpdateAsync(
        string? name = null,
        string? description = null,
        string? location = null,
        DateTime? startsAt = null,
        Uri? url = null,
        SystemColor? color = null,
        uint? duration = null,
        uint? rsvpLimit = null,
        bool? isPrivate = null,
        bool? rsvpDisabled = null,
        bool? autofillWaitlist = null,
        bool? isAllDay = null,
        IList<uint>? roleIds = null
    ) =>
        ParentClient.UpdateEventAsync(ChannelId, Id, name, description, location, startsAt, url, color, duration, rsvpLimit, isPrivate, rsvpDisabled, autofillWaitlist, isAllDay, roleIds);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateEventAsync(Guid, uint, string?, string?, string?, DateTime?, Uri?, SystemColor?, uint?, uint?, bool?, bool?, bool?, bool?, IList{uint}?)" />
    /// <param name="name">The new name of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="description">The new description of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="location">The new location of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="startsAt">The new starting date of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="url">The new URL of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="color">The new colour of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="duration">The new length/duration of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="rsvpLimit">The limit of how many <see cref="User">users</see> can be invited or attend the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="isPrivate">Whether the <see cref="CalendarEvent">calendar event</see> is now private or not private anymore</param>
    /// <param name="rsvpDisabled">Whether <see cref="Member">members</see> can attend the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="autofillWaitlist">Whether <see cref="Member">members</see> in the waitlist should be added to the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="isAllDay">Whether the <see cref="CalendarEvent">calendar event</see> lasts all day</param>
    /// <param name="roleIds">The list of identifiers of roles to restrict <see cref="CalendarEvent">calendar events</see></param>
    public Task<CalendarEvent> UpdateAsync(
        string? name = null,
        string? description = null,
        string? location = null,
        DateTime? startsAt = null,
        Uri? url = null,
        SystemColor? color = null,
        TimeSpan? duration = null,
        uint? rsvpLimit = null,
        bool? isPrivate = null,
        bool? rsvpDisabled = null,
        bool? autofillWaitlist = null,
        bool? isAllDay = null,
        IList<uint>? roleIds = null
    ) =>
        Event.UpdateAsync(name, description, location, startsAt, url, color, duration, rsvpLimit, isPrivate, rsvpDisabled, autofillWaitlist, isAllDay, roleIds);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteEventAsync(Guid, uint)" />
    public Task DeleteAsync() =>
        Event.DeleteAsync();

    /// <inheritdoc cref="AbstractGuildedClient.AddReactionAsync(Guid, uint, uint)" />
    /// <param name="emoteId">The identifier of the <see cref="Emote">emote</see> to add</param>
    public Task AddReactionAsync(uint emoteId) =>
        Event.AddReactionAsync(emoteId);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveReactionAsync(Guid, uint, uint)" />
    /// <param name="emoteId">The identifier of the <see cref="Emote">emote</see> to remove</param>
    public Task RemoveReactionAsync(uint emoteId) =>
        Event.RemoveReactionAsync(emoteId);
    #endregion

    #region Methods RSVP
    /// <inheritdoc cref="AbstractGuildedClient.GetRsvpsAsync(Guid, uint)" />
    public Task<IList<CalendarEventRsvp>> GetRsvpsAsync() =>
        Event.GetRsvpsAsync();

    /// <inheritdoc cref="AbstractGuildedClient.GetRsvpAsync(Guid, uint, HashId)" />
    /// <param name="user">The identifier of the <see cref="User">user</see> to get <see cref="CalendarEventRsvp">RSVP</see> of</param>
    public Task<CalendarEventRsvp> GetRsvpAsync(HashId user) =>
        Event.GetRsvpAsync(user);

    /// <inheritdoc cref="AbstractGuildedClient.SetRsvpAsync(Guid, uint, HashId, CalendarEventRsvpStatus)" />
    /// <param name="user">The identifier of the <see cref="User">user</see> to set <see cref="CalendarEventRsvp">RSVP</see> of</param>
    /// <param name="status">The status of the <see cref="CalendarEvent">calendar RSVP</see> to set</param>
    public Task<CalendarEventRsvp> SetRsvpAsync(HashId user, CalendarEventRsvpStatus status) =>
        Event.SetRsvpAsync(user, status);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveRsvpAsync(Guid, uint, HashId)" />
    /// <param name="user">The identifier of the <see cref="User">user</see> to remove <see cref="CalendarEventRsvp">RSVP</see> of</param>
    public Task RemoveRsvpAsync(HashId user) =>
        Event.RemoveRsvpAsync(user);
    #endregion
}