using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Content;
using Guilded.Events;
using Guilded.Permissions;
using Guilded.Servers;
using Guilded.Users;
using RestSharp;

namespace Guilded.Client;

public abstract partial class AbstractGuildedClient
{
    #region Properties Calendar channels > Events
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a new <see cref="CalendarEvent">calendar event</see> is posted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="EventUpdated" />
    /// <seealso cref="EventDeleted" />
    public IObservable<CalendarEventEvent> EventCreated => ((IEventInfo<CalendarEventEvent>)GuildedEvents["CalendarEventCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="CalendarEvent">calendar event</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="EventCreated" />
    /// <seealso cref="EventDeleted" />
    public IObservable<CalendarEventEvent> EventUpdated => ((IEventInfo<CalendarEventEvent>)GuildedEvents["CalendarEventUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="CalendarEvent">calendar event</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="EventCreated" />
    /// <seealso cref="EventUpdated" />
    public IObservable<CalendarEventEvent> EventDeleted => ((IEventInfo<CalendarEventEvent>)GuildedEvents["CalendarEventDeleted"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="CalendarEventReaction">reaction</see> is added to a <see cref="CalendarEvent">calendar event</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventReactionCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="EventCreated" />
    /// <seealso cref="EventUpdated" />
    /// <seealso cref="EventDeleted" />
    /// <seealso cref="EventReactionRemoved" />
    public IObservable<CalendarEventReactionEvent> EventReactionAdded => ((IEventInfo<CalendarEventReactionEvent>)GuildedEvents["CalendarEventReactionCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="CalendarEventReaction">reaction</see> is added to a <see cref="CalendarEvent">calendar event</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventReactionDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="EventCreated" />
    /// <seealso cref="EventUpdated" />
    /// <seealso cref="EventDeleted" />
    /// <seealso cref="EventReactionAdded" />
    public IObservable<CalendarEventReactionEvent> EventReactionRemoved => ((IEventInfo<CalendarEventReactionEvent>)GuildedEvents["CalendarEventReactionDeleted"]).Observable;
    #endregion

    #region Properties Calendar channels > RSVPs
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="CalendarEventRsvp">RSVP</see> of a <see cref="CalendarEvent">calendar event</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventRsvpUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="EventRsvpManyUpdated" />
    /// <seealso cref="EventRsvpDeleted" />
    /// <seealso cref="EventUpdated" />
    /// <seealso cref="EventDeleted" />
    public IObservable<CalendarEventRsvpEvent> EventRsvpUpdated => ((IEventInfo<CalendarEventRsvpEvent>)GuildedEvents["CalendarEventRsvpUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when multiple <see cref="CalendarEventRsvp">RSVPs</see> of a <see cref="CalendarEvent">calendar event</see> are edited or created.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventRsvpManyUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="EventRsvpUpdated" />
    /// <seealso cref="EventRsvpDeleted" />
    /// <seealso cref="EventCreated" />
    /// <seealso cref="EventDeleted" />
    public IObservable<CalendarEventRsvpManyEvent> EventRsvpManyUpdated => ((IEventInfo<CalendarEventRsvpManyEvent>)GuildedEvents["CalendarEventRsvpManyUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="CalendarEventRsvp">RSVP</see> of a <see cref="CalendarEvent">calendar event</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventRsvpDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="EventRsvpUpdated" />
    /// <seealso cref="EventRsvpManyUpdated" />
    /// <seealso cref="EventCreated" />
    /// <seealso cref="EventUpdated" />
    public IObservable<CalendarEventRsvpEvent> EventRsvpDeleted => ((IEventInfo<CalendarEventRsvpEvent>)GuildedEvents["CalendarEventRsvpDeleted"]).Observable;
    #endregion

    #region Properties Calendar channels > Series
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="CalendarEventSeries">calendar event series</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventSeriesUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="EventSeriesDeleted" />
    public IObservable<CalendarEventSeriesEvent> EventSeriesUpdated => ((IEventInfo<CalendarEventSeriesEvent>)GuildedEvents["CalendarEventSeriesUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="CalendarEventSeries">calendar event series</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventSeriesDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="EventSeriesUpdated" />
    public IObservable<CalendarEventSeriesEvent> EventSeriesDeleted => ((IEventInfo<CalendarEventSeriesEvent>)GuildedEvents["CalendarEventSeriesDeleted"]).Observable;
    #endregion

    #region Properties Calendar channels > Comments
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a new <see cref="CalendarEventComment">calendar event comment</see> is posted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventCommentCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="EventCommentUpdated" />
    /// <seealso cref="EventCommentDeleted" />
    /// <seealso cref="EventCommentReactionAdded" />
    /// <seealso cref="EventCommentReactionRemoved" />
    public IObservable<CalendarEventCommentEvent> EventCommentCreated => ((IEventInfo<CalendarEventCommentEvent>)GuildedEvents["CalendarEventCommentCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="CalendarEventComment">calendar event comment</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventCommentUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="EventCommentCreated" />
    /// <seealso cref="EventCommentDeleted" />
    /// <seealso cref="EventCommentReactionAdded" />
    /// <seealso cref="EventCommentReactionRemoved" />
    public IObservable<CalendarEventCommentEvent> EventCommentUpdated => ((IEventInfo<CalendarEventCommentEvent>)GuildedEvents["CalendarEventCommentUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="CalendarEventComment">calendar event comment</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventCommentDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="EventCommentCreated" />
    /// <seealso cref="EventCommentUpdated" />
    /// <seealso cref="EventCommentReactionAdded" />
    /// <seealso cref="EventCommentReactionRemoved" />
    public IObservable<CalendarEventCommentEvent> EventCommentDeleted => ((IEventInfo<CalendarEventCommentEvent>)GuildedEvents["CalendarEventCommentDeleted"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="CalendarEventCommentReaction">reaction</see> is added to a <see cref="CalendarEventComment">calendar event comment</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventCommentReactionCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="EventCommentCreated" />
    /// <seealso cref="EventCommentUpdated" />
    /// <seealso cref="EventCommentDeleted" />
    /// <seealso cref="EventCommentReactionRemoved" />
    public IObservable<CalendarEventCommentReactionEvent> EventCommentReactionAdded => ((IEventInfo<CalendarEventCommentReactionEvent>)GuildedEvents["CalendarEventCommentReactionCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="CalendarEventCommentReaction">reaction</see> is added to a <see cref="CalendarEventComment">calendar event comment</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CalendarEventCommentReactionDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="EventCommentCreated" />
    /// <seealso cref="EventCommentUpdated" />
    /// <seealso cref="EventCommentDeleted" />
    /// <seealso cref="EventCommentReactionAdded" />
    public IObservable<CalendarEventCommentReactionEvent> EventCommentReactionRemoved => ((IEventInfo<CalendarEventCommentReactionEvent>)GuildedEvents["CalendarEventCommentReactionDeleted"]).Observable;
    #endregion

    #region Methods Calendar channels > Events
    /// <summary>
    /// Gets a list of <see cref="CalendarEvent">calendar events</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="limit">The limit of how many <see cref="CalendarEvent">calendar events</see> to get (default — <c>25</c>, values — <c>(0, 100]</c>)</param>
    /// <param name="before">The max limit of the creation date of fetched <see cref="CalendarEvent">calendar events</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetEvents" />
    /// <returns>The list of fetched <see cref="CalendarEvent">calendar events</see> in the specified <paramref name="channel" /></returns>
    public Task<IList<CalendarEvent>> GetEventsAsync(Guid channel, uint? limit = null, DateTime? before = null) =>
        GetResponsePropertyAsync<IList<CalendarEvent>>(
            new RestRequest($"channels/{channel}/events", Method.Get)
                .AddOptionalQuery("limit", limit, encode: false)
                .AddOptionalQuery("before", before)
        , "calendarEvents");

    /// <summary>
    /// Gets the specified <paramref name="calendarEvent">calendar event</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetEvents" />
    /// <returns>The <see cref="CalendarEvent">calendar event</see> that was specified in the arguments</returns>
    public Task<CalendarEvent> GetEventAsync(Guid channel, uint calendarEvent) =>
        GetResponsePropertyAsync<CalendarEvent>(new RestRequest($"channels/{channel}/events/{calendarEvent}", Method.Get), "calendarEvent");

    /// <summary>
    /// Creates a new <see cref="CalendarEvent">calendar event</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The creation information about the <see cref="CalendarEvent">calendar event</see> being created</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetEvents" />
    /// <permission cref="Permission.CreateEvents" />
    /// <permission cref="Permission.UseEveryoneMention">Required when adding an <c>@everyone</c> or a <c>@here</c> mention to the <see cref="CalendarEvent.Description">calendar event's description</see></permission>
    /// <returns>The <see cref="CalendarEvent">calendar event</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<CalendarEvent> CreateEventAsync(Guid channel, CalendarEventContent calendarEvent)
    {
        if (calendarEvent is null)
            throw new ArgumentNullException(nameof(calendarEvent));
        // Either ignoring the non-existence of `?` or `CalendarEventContent` was used
        else if (string.IsNullOrWhiteSpace(calendarEvent.Name))
            throw new ArgumentNullException(nameof(calendarEvent), $"{nameof(calendarEvent)} cannot have a null, empty or whitespace-only name.");
        // Either null or non-empty values are allowed
        else if (calendarEvent.Description is not null && string.IsNullOrWhiteSpace(calendarEvent.Description))
            throw new ArgumentNullException(nameof(calendarEvent), $"{nameof(calendarEvent)} cannot have an empty or whitespace-only description. Set it to null if you don't want a description.");
        else if (calendarEvent.Location is not null && string.IsNullOrWhiteSpace(calendarEvent.Location))
            throw new ArgumentNullException(nameof(calendarEvent), $"{nameof(calendarEvent)} cannot have an empty or whitespace-only location. Set it to null if you don't want a location.");
        else if (calendarEvent.Duration == 0)
            throw new ArgumentNullException(nameof(calendarEvent), $"{nameof(calendarEvent)} cannot have a 0 minute duration.");

        return GetResponsePropertyAsync<CalendarEvent>(new RestRequest($"channels/{channel}/events", Method.Post).AddJsonBody(calendarEvent), "calendarEvent");
    }

    /// <summary>
    /// Creates a new <see cref="CalendarEvent">calendar event</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
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
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetEvents" />
    /// <permission cref="Permission.CreateEvents" />
    /// <permission cref="Permission.UseEveryoneMention">Required when adding an <c>@everyone</c> or a <c>@here</c> mention to the <see cref="CalendarEvent.Description">calendar event's description</see></permission>
    /// <returns>The <see cref="CalendarEvent">calendar event</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<CalendarEvent> CreateEventAsync(
        Guid channel,
        string name,
        string? description = null,
        string? location = null,
        DateTime? startsAt = null,
        Uri? url = null,
        Color? color = null,
        uint? duration = null,
        uint? rsvpLimit = null,
        bool isPrivate = false,
        bool rsvpDisabled = false,
        bool autofillWaitlist = false,
        bool isAllDay = false,
        IList<uint>? roleIds = null,
        CalendarEventRepetition? repeatInfo = null
    ) =>
        CreateEventAsync(channel, new CalendarEventContent(name, description, location, startsAt, url, color, duration, rsvpLimit, isPrivate, rsvpDisabled, autofillWaitlist, isAllDay, roleIds, repeatInfo));

    /// <inheritdoc cref="CreateEventAsync(Guid, string, string?, string?, DateTime?, Uri?, Color?, uint?, uint?, bool, bool, bool, bool, IList{uint}?, CalendarEventRepetition)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
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
    public Task<CalendarEvent> CreateEventAsync(
        Guid channel,
        string name,
        string? description = null,
        string? location = null,
        DateTime? startsAt = null,
        Uri? url = null,
        Color? color = null,
        TimeSpan? duration = null,
        uint? rsvpLimit = null,
        bool isPrivate = false,
        bool rsvpDisabled = false,
        bool autofillWaitlist = false,
        bool isAllDay = false,
        IList<uint>? roleIds = null,
        CalendarEventRepetition? repeatInfo = null
    ) =>
        CreateEventAsync(channel, name, description, location, startsAt, url, color, (uint?)duration?.TotalMinutes, rsvpLimit, isPrivate, rsvpDisabled, autofillWaitlist, isAllDay, roleIds, repeatInfo);

    /// <summary>
    /// Edits the specified <paramref name="calendarEvent">calendar event</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> to update/edit</param>
    /// <param name="calendarEventContent">The new contents of the <see cref="CalendarEvent">calendar event</see> that is being updated</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetEvents" />
    /// <permission cref="Permission.UpdateEvents">Required when editing <see cref="CalendarEvent">calendar events</see> that the <see cref="AbstractGuildedClient">client</see> doesn't own</permission>
    /// <permission cref="Permission.UseEveryoneMention">Required when adding an <c>@everyone</c> or a <c>@here</c> mention to the <see cref="CalendarEvent.Description">calendar event's description</see></permission>
    /// <returns>The <see cref="CalendarEvent">calendar event</see> that was updated by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<CalendarEvent> UpdateEventAsync(Guid channel, uint calendarEvent, CalendarEventContent calendarEventContent) =>
        GetResponsePropertyAsync<CalendarEvent>(new RestRequest($"channels/{channel}/events/{calendarEvent}", Method.Patch).AddJsonBody(calendarEventContent), "calendarEvent");

    /// <inheritdoc cref="UpdateEventAsync(Guid, uint, CalendarEventContent)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> to update/edit</param>
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
    public Task<CalendarEvent> UpdateEventAsync(
        Guid channel,
        uint calendarEvent,
        string? name = null,
        string? description = null,
        string? location = null,
        DateTime? startsAt = null,
        Uri? url = null,
        Color? color = null,
        uint? duration = null,
        uint? rsvpLimit = null,
        bool? isPrivate = null,
        bool? rsvpDisabled = null,
        bool? autofillWaitlist = null,
        bool? isAllDay = null,
        IList<uint>? roleIds = null
    ) =>
        UpdateEventAsync(channel, calendarEvent, new CalendarEventContent(name, description, location, startsAt, url, color, duration, rsvpLimit, isPrivate, rsvpDisabled, autofillWaitlist, isAllDay, roleIds));

    /// <inheritdoc cref="UpdateEventAsync(Guid, uint, CalendarEventContent)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> to update/edit</param>
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
    public Task<CalendarEvent> UpdateEventAsync(
        Guid channel,
        uint calendarEvent,
        string? name = null,
        string? description = null,
        string? location = null,
        DateTime? startsAt = null,
        Uri? url = null,
        Color? color = null,
        TimeSpan? duration = null,
        uint? rsvpLimit = null,
        bool? isPrivate = null,
        bool? rsvpDisabled = null,
        bool? autofillWaitlist = null,
        bool? isAllDay = null,
        IList<uint>? roleIds = null
    ) =>
        UpdateEventAsync(channel, calendarEvent, name, description, location, startsAt, url, color, (uint?)duration?.TotalMinutes, rsvpLimit, isPrivate, rsvpDisabled, autofillWaitlist, isAllDay, roleIds);

    /// <summary>
    /// Deletes the specified <paramref name="calendarEvent">calendar event</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetEvents" />
    /// <permission cref="Permission.DeleteEvents">Required when deleting <see cref="CalendarEvent">calendar event</see> that the <see cref="AbstractGuildedClient">client</see> doesn't own</permission>
    public Task DeleteEventAsync(Guid channel, uint calendarEvent) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/events/{calendarEvent}", Method.Delete));

    /// <summary>
    /// Edits <see cref="CalendarEvent">calendar events</see> in the specified <paramref name="calendarEventSeries">calendar event series</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEventSeries">The identifier of the <see cref="CalendarEventSeries">calendar event series</see> to update/edit <see cref="CalendarEvent">calendar events</see> in</param>
    /// <param name="calendarEventSeriesContent">The new contents of all the <see cref="CalendarEvent">calendar events</see> in the <see cref="CalendarEventSeries">series</see> or other informations</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetEvents" />
    /// <permission cref="Permission.UpdateEvents">Required when editing <see cref="CalendarEvent">calendar events</see> that the <see cref="AbstractGuildedClient">client</see> doesn't own</permission>
    /// <permission cref="Permission.UseEveryoneMention">Required when adding an <c>@everyone</c> or a <c>@here</c> mention to the <see cref="CalendarEvent.Description">calendar event's description</see></permission>
    public Task UpdateEventSeriesAsync(
        Guid channel,
        Guid calendarEventSeries,
        CalendarEventSeriesContent calendarEventSeriesContent
    ) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/event_series/{calendarEventSeries}", Method.Patch).AddJsonBody(calendarEventSeriesContent));

    /// <inheritdoc cref="UpdateEventSeriesAsync(Guid, Guid, CalendarEventSeriesContent)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
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
    /// <param name="calendarEvent">From which <see cref="CalendarEvent">calendar event</see> onwards the <see cref="CalendarEventSeries">calendar event series</see> should be updated</param>
    public Task UpdateEventSeriesAsync(
        Guid channel,
        Guid calendarEventSeries,
        string? name = null,
        string? description = null,
        string? location = null,
        DateTime? startsAt = null,
        Uri? url = null,
        Color? color = null,
        uint? duration = null,
        uint? rsvpLimit = null,
        bool? isPrivate = null,
        bool? rsvpDisabled = null,
        bool? autofillWaitlist = null,
        bool? isAllDay = null,
        IList<uint>? roleIds = null,
        CalendarEventRepetition? repeatInfo = null,
        uint? calendarEvent = null
    ) =>
        UpdateEventSeriesAsync(channel, calendarEventSeries, new CalendarEventSeriesContent(name, description, location, startsAt, url, color, duration, rsvpLimit, isPrivate, rsvpDisabled, autofillWaitlist, isAllDay, roleIds, repeatInfo, calendarEvent));

    /// <inheritdoc cref="UpdateEventSeriesAsync(Guid, Guid, CalendarEventSeriesContent)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
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
    /// <param name="calendarEvent">From which <see cref="CalendarEvent">calendar event</see> onwards the <see cref="CalendarEventSeries">calendar event series</see> should be updated</param>
    public Task UpdateEventSeriesAsync(
        Guid channel,
        Guid calendarEventSeries,
        string? name = null,
        string? description = null,
        string? location = null,
        DateTime? startsAt = null,
        Uri? url = null,
        Color? color = null,
        TimeSpan? duration = null,
        uint? rsvpLimit = null,
        bool? isPrivate = null,
        bool? rsvpDisabled = null,
        bool? autofillWaitlist = null,
        bool? isAllDay = null,
        IList<uint>? roleIds = null,
        CalendarEventRepetition? repeatInfo = null,
        uint? calendarEvent = null
    ) =>
        UpdateEventSeriesAsync(channel, calendarEventSeries, name, description, location, startsAt, url, color, (uint?)duration?.TotalMinutes, rsvpLimit, isPrivate, rsvpDisabled, autofillWaitlist, isAllDay, roleIds, repeatInfo, calendarEvent);

    /// <summary>
    /// Deletes <see cref="CalendarEvent">calendar events</see> in the specified <paramref name="calendarEventSeries">calendar event series</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEventSeries">The identifier of the <see cref="CalendarEventSeries">calendar event series</see> to update/edit <see cref="CalendarEvent">calendar events</see> in </param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> after which all other <see cref="CalendarEvent">calendar events</see> in the <see cref="CalendarEventSeries">series</see> should be deleted</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetEvents" />
    /// <permission cref="Permission.DeleteEvents">Required when deleting <see cref="CalendarEvent">calendar event</see> that the <see cref="AbstractGuildedClient">client</see> doesn't own</permission>
    public Task DeleteEventSeriesAsync(Guid channel, Guid calendarEventSeries, uint? calendarEvent = null) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/event_series/{calendarEventSeries}", Method.Delete)
            .AddJsonBody(new
            {
                calendarEventId = calendarEvent
            })
        );
    #endregion
}