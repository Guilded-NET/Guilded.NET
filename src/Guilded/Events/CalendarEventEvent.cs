// Big yikes for the name

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

    /// <inheritdoc cref="CalendarEvent.SeriesId" />
    public Guid? SeriesId => Event.SeriesId;

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
    [MemberNotNullWhen(true, nameof(Cancellation), nameof(CanceledBy))]
    public bool IsCanceled => Event.IsCanceled;

    /// <inheritdoc cref="CalendarEvent.IsPrivate" />
    public bool IsPrivate => Event.IsPrivate;

    /// <inheritdoc cref="CalendarEvent.Repeats" />
    public bool Repeats => Event.Repeats;

    /// <inheritdoc cref="CalendarEvent.AutofillWaitlist" />
    public bool AutofillWaitlist => Event.AutofillWaitlist;

    /// <inheritdoc cref="CalendarEvent.RsvpDisabled" />
    public bool RsvpDisabled => Event.RsvpDisabled;

    /// <inheritdoc cref="CalendarEvent.RsvpLimit" />
    public uint? RsvpLimit => Event.RsvpLimit;

    /// <inheritdoc cref="CalendarEvent.RsvpDisabled" />
    [MemberNotNullWhen(true, nameof(SeriesId))]
    public bool InSeries => Event.InSeries;

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
    /// <inheritdoc cref="AbstractGuildedClient.UpdateEventAsync(Guid, uint, CalendarEventContent)" />
    /// <param name="calendarEventContent">The new contents of the <see cref="CalendarEvent">calendar event</see> that is being updated</param>
    public Task<CalendarEvent> UpdateAsync(CalendarEventContent calendarEventContent) =>
        Event.UpdateAsync(calendarEventContent);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateEventAsync(Guid, uint, CalendarEventContent)" />
    /// <param name="name">The new name of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="description">The new description of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="location">The new location of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="startsAt">The new starting date of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="url">The new URL of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="color">The new colour of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="duration">The new length/duration of the <see cref="CalendarEvent">calendar event</see> in minutes</param>
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
        UpdateAsync(new CalendarEventContent(name, description, location, startsAt, url, color, duration, rsvpLimit, isPrivate, rsvpDisabled, autofillWaitlist, isAllDay, roleIds));

    /// <inheritdoc cref="AbstractGuildedClient.UpdateEventAsync(Guid, uint, CalendarEventContent)" />
    /// <param name="name">The new name of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="description">The new description of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="location">The new location of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="startsAt">The new starting date of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="url">The new URL of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="color">The new colour of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="duration">The new length/duration of the <see cref="CalendarEvent">calendar event</see> in minutes</param>
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
        UpdateAsync(name, description, location, startsAt, url, color, (uint?)duration?.TotalMinutes, rsvpLimit, isPrivate, rsvpDisabled, autofillWaitlist, isAllDay, roleIds);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteEventAsync(Guid, uint)" />
    public Task DeleteAsync() =>
        Event.DeleteAsync();

    /// <inheritdoc cref="AbstractGuildedClient.UpdateEventSeriesAsync(Guid, Guid, CalendarEventSeriesContent)" />
    /// <param name="calendarEventSeriesContent">The new contents of all the <see cref="CalendarEvent">calendar events</see> in the <see cref="CalendarEventSeries">series</see> or other informations</param>
    public Task UpdateSeriesAsync(CalendarEventSeriesContent calendarEventSeriesContent) =>
        Event.UpdateSeriesAsync(calendarEventSeriesContent);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateEventSeriesAsync(Guid, Guid, CalendarEventSeriesContent)" />
    /// <param name="name">The title of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="description">The description of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="location">The physical or non-physical location of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="startsAt">The date when the <see cref="CalendarEvent">calendar event</see> starts</param>
    /// <param name="url">The URL to the <see cref="CalendarEvent">calendar event's</see> services, place or anything related</param>
    /// <param name="color">The colour of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="duration">The duration of the <see cref="CalendarEvent">calendar event</see> in minutes</param>
    /// <param name="rsvpLimit">The limit of how many <see cref="User">users</see> can be invited or attend the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="isPrivate">Whether the <see cref="CalendarEvent">calendar event</see> is private</param>
    /// <param name="rsvpDisabled">Whether <see cref="Member">members</see> can attend the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="autofillWaitlist">Whether <see cref="Member">members</see> in the waitlist should be added to the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="isAllDay">Whether the <see cref="CalendarEvent">calendar event</see> lasts all day</param>
    /// <param name="roleIds">The list of identifiers of roles to restrict <see cref="CalendarEvent">calendar events</see></param>
    /// <param name="repeatInfo">The information about <see cref="CalendarEventSeries">calendar event repetition</see></param>
    /// <param name="updateAfterCalendarEvent">Whether all the other <see cref="CalendarEvent">calendar events</see> in the <see cref="CalendarEventSeries">calendar event series</see> after the current <see cref="CalendarEvent">calendar events</see> should be updated</param>
    public Task UpdateSeriesAsync(
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
        IList<uint>? roleIds = null,
        CalendarEventRepetition? repeatInfo = null,
        bool updateAfterCalendarEvent = false
    ) =>
        UpdateSeriesAsync(new CalendarEventSeriesContent(name, description, location, startsAt, url, color, duration, rsvpLimit, isPrivate, rsvpDisabled, autofillWaitlist, isAllDay, roleIds, repeatInfo, updateAfterCalendarEvent ? Id : null));

    /// <inheritdoc cref="AbstractGuildedClient.UpdateEventSeriesAsync(Guid, Guid, CalendarEventSeriesContent)" />
    /// <param name="calendarEventSeries">The identifier of the <see cref="CalendarEventSeries">calendar event series</see> to update/edit <see cref="CalendarEvent">calendar events</see> in </param>
    /// <param name="name">The title of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="description">The description of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="location">The physical or non-physical location of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="startsAt">The date when the <see cref="CalendarEvent">calendar event</see> starts</param>
    /// <param name="url">The URL to the <see cref="CalendarEvent">calendar event's</see> services, place or anything related</param>
    /// <param name="color">The colour of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="duration">The duration of the <see cref="CalendarEvent">calendar event</see> in minutes</param>
    /// <param name="rsvpLimit">The limit of how many <see cref="User">users</see> can be invited or attend the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="isPrivate">Whether the <see cref="CalendarEvent">calendar event</see> is private</param>
    /// <param name="rsvpDisabled">Whether <see cref="Member">members</see> can attend the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="autofillWaitlist">Whether <see cref="Member">members</see> in the waitlist should be added to the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="isAllDay">Whether the <see cref="CalendarEvent">calendar event</see> lasts all day</param>
    /// <param name="roleIds">The list of identifiers of roles to restrict <see cref="CalendarEvent">calendar events</see></param>
    /// <param name="repeatInfo">The information about <see cref="CalendarEventSeries">calendar event repetition</see></param>
    /// <param name="updateAfterCalendarEvent">Whether all the other <see cref="CalendarEvent">calendar events</see> in the <see cref="CalendarEventSeries">calendar event series</see> after the current <see cref="CalendarEvent">calendar events</see> should be updated</param>
    public Task UpdateSeriesAsync(
        Guid calendarEventSeries,
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
        IList<uint>? roleIds = null,
        CalendarEventRepetition? repeatInfo = null,
        bool updateAfterCalendarEvent = false
    ) =>
        UpdateSeriesAsync(calendarEventSeries, name, description, location, startsAt, url, color, duration, rsvpLimit, isPrivate, rsvpDisabled, autofillWaitlist, isAllDay, roleIds, repeatInfo, updateAfterCalendarEvent);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteEventSeriesAsync(Guid, Guid, uint?)" />
    /// <param name="deleteAfterCalendarEvent">Whether all the other <see cref="CalendarEvent">calendar events</see> in the <see cref="CalendarEventSeries">calendar event series</see> after the current <see cref="CalendarEvent">calendar events</see> should be deleted</param>
    public Task DeleteSeriesAsync(bool deleteAfterCalendarEvent = false) =>
        Event.DeleteSeriesAsync(deleteAfterCalendarEvent);

    /// <inheritdoc cref="AbstractGuildedClient.AddReactionAsync(Guid, uint, uint)" />
    /// <param name="emoteId">The identifier of the <see cref="Emote">emote</see> to add</param>
    public Task AddReactionAsync(uint emoteId) =>
        ParentClient.AddReactionAsync(ChannelId, Id, emoteId);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveReactionAsync(Guid, uint, uint)" />
    /// <param name="emoteId">The identifier of the <see cref="Emote">emote</see> to remove</param>
    public Task RemoveReactionAsync(uint emoteId) =>
        ParentClient.RemoveReactionAsync(ChannelId, Id, emoteId);
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

    #region Methods Comments
    /// <inheritdoc cref="AbstractGuildedClient.CreateTopicCommentAsync(Guid, uint, string)" />
    /// <param name="content">The content of the <see cref="CalendarEventComment">calendar event comment</see></param>
    public Task<CalendarEventComment> CreateCommentAsync(string content) =>
        Event.CreateCommentAsync(content);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateTopicCommentAsync(Guid, uint, uint, string)" />
    /// <param name="comment">The identifier of the <see cref="CalendarEventComment">calendar event comment</see> to update</param>
    /// <param name="content">The new acontent of the <see cref="CalendarEventComment">calendar event comment</see></param>
    public Task<CalendarEventComment> UpdateCommentAsync(uint comment, string content) =>
        Event.UpdateCommentAsync(comment, content);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteTopicCommentAsync(Guid, uint, uint)" />
    /// <param name="comment">The identifier of the <see cref="TopicComment">forum topic comment</see> to delete</param>
    public Task DeleteCommentAsync(uint comment) =>
        Event.DeleteCommentAsync(comment);
    #endregion
}