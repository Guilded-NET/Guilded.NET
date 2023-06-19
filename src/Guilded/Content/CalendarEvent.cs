using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Base.Json;
using Guilded.Client;
using Guilded.Events;
using Guilded.Servers;
using Guilded.Users;
using Newtonsoft.Json;
using SystemColor = System.Drawing.Color;

namespace Guilded.Content;

/// <summary>
/// Represents a calendar event in a <see cref="ChannelType.Calendar">calendar channel</see>.
/// </summary>
/// <seealso cref="CalendarEventCancellation" />
/// <seealso cref="CalendarEventRsvp" />
/// <seealso cref="Topic" />
/// <seealso cref="Doc" />
/// <seealso cref="ItemBase{T}" />
/// <seealso cref="Message" />
public class CalendarEvent : ChannelContent<uint, HashId>, IReactibleContent, IServerBased, IPrivatableContent
{
    #region Properties Content
    /// <summary>
    /// Gets the title of the <see cref="CalendarEvent">calendar event</see>.
    /// </summary>
    /// <remarks>
    /// <para>This does not have any Markdown formatting and will not contain <c>\n</c> or other line breaking characters.</para>
    /// </remarks>
    /// <value>The title of the <see cref="CalendarEvent">calendar event</see></value>
    /// <seealso cref="CalendarEvent" />
    /// <seealso cref="Description" />
    /// <seealso cref="Location" />
    public string Name { get; }

    /// <summary>
    /// Gets the description of the <see cref="CalendarEvent">calendar event</see>.
    /// </summary>
    /// <remarks>
    /// <para>The contents are formatted in Markdown. This includes images and videos, which are in the format of <c>![](source_url)</c>.</para>
    /// </remarks>
    /// <value>The description of the <see cref="CalendarEvent">calendar event</see></value>
    /// <seealso cref="CalendarEvent" />
    /// <seealso cref="Name" />
    /// <seealso cref="Mentions" />
    /// <seealso cref="Location" />
    public string? Description { get; }

    /// <summary>
    /// Gets the <see cref="Content.Mentions">mentions</see> found in the <see cref="Description">description</see>.
    /// </summary>
    /// <value>The <see cref="Content.Mentions">mentions</see> found in the <see cref="Description">description</see></value>
    /// <seealso cref="CalendarEvent" />
    /// <seealso cref="Description" />
    /// <seealso cref="Name" />
    /// <seealso cref="Location" />
    public Mentions? Mentions { get; }

    /// <summary>
    /// Gets the place where <see cref="CalendarEvent">calendar event</see> can be attended.
    /// </summary>
    /// <remarks>
    /// <para>In terms of functionality, this doesn't do much besides display it in the <see cref="CalendarEvent">calendar event</see> info.</para>
    /// </remarks>
    /// <value>The place where <see cref="CalendarEvent">calendar event</see> can be attended</value>
    /// <seealso cref="CalendarEvent" />
    /// <seealso cref="Name" />
    /// <seealso cref="Description" />
    /// <seealso cref="Url" />
    /// <seealso cref="Color" />
    public string? Location { get; }

    /// <summary>
    /// Gets the URL to the <see cref="CalendarEvent">calendar event's</see> services, place or anything related.
    /// </summary>
    /// <value>The URL to the <see cref="CalendarEvent">calendar event's</see> services, place or anything related</value>
    /// <seealso cref="CalendarEvent" />
    /// <seealso cref="Name" />
    /// <seealso cref="Description" />
    /// <seealso cref="Location" />
    /// <seealso cref="Color" />
    public Uri? Url { get; }

    /// <summary>
    /// Gets the colour of the <see cref="CalendarEvent">calendar event</see>.
    /// </summary>
    /// <value>The colour of the <see cref="CalendarEvent">calendar event</see></value>
    /// <seealso cref="CalendarEvent" />
    /// <seealso cref="Url" />
    /// <seealso cref="Location" />
    /// <seealso cref="Name" />
    /// <seealso cref="Description" />
    [JsonConverter(typeof(DecimalColorConverter))]
    public SystemColor? Color { get; }
    #endregion

    #region Properties Time
    /// <summary>
    /// Gets the date when the <see cref="CalendarEvent">calendar event</see> starts.
    /// </summary>
    /// <value>The date when the <see cref="CalendarEvent">calendar event</see> starts</value>
    /// <seealso cref="CalendarEvent" />
    /// <seealso cref="ChannelContent{T, S}.CreatedAt" />
    /// <seealso cref="ChannelContent{T, S}.CreatedBy" />
    /// <seealso cref="Duration" />
    /// <seealso cref="EndsAt" />
    /// <seealso cref="Repeats" />
    public DateTime StartsAt { get; }

    /// <summary>
    /// Gets the duration of the <see cref="CalendarEvent">calendar event</see> in minutes.
    /// </summary>
    /// <value>The duration of the <see cref="CalendarEvent">calendar event</see> in minutes</value>
    /// <seealso cref="CalendarEvent" />
    /// <seealso cref="ChannelContent{T, S}.CreatedAt" />
    /// <seealso cref="ChannelContent{T, S}.CreatedBy" />
    /// <seealso cref="StartsAt" />
    /// <seealso cref="EndsAt" />
    /// <seealso cref="Repeats" />
    public TimeSpan? Duration { get; }

    /// <summary>
    /// Gets whether the <see cref="CalendarEvent">calendar event</see> has more than one instance similar to itself.
    /// </summary>
    /// <value>Whether the <see cref="CalendarEvent">calendar event</see> has more than one instance similar to itself</value>
    /// <seealso cref="CalendarEvent" />
    /// <seealso cref="StartsAt" />
    /// <seealso cref="Duration" />
    /// <seealso cref="EndsAt" />
    /// <seealso cref="IsPrivate" />
    /// <seealso cref="IsCanceled" />
    public bool Repeats { get; }

    /// <summary>
    /// Gets the identifier of the repetition of the <see cref="CalendarEvent">calendar event</see>.
    /// </summary>
    /// <value>The identifier of the repetition of the <see cref="CalendarEvent">calendar event</see></value>
    /// <seealso cref="CalendarEvent" />
    /// <seealso cref="InSeries" />
    /// <seealso cref="StartsAt" />
    /// <seealso cref="Duration" />
    /// <seealso cref="EndsAt" />
    /// <seealso cref="Repeats" />
    public Guid? SeriesId { get; }

    /// <summary>
    /// Gets the date when the <see cref="CalendarEvent">calendar event</see> starts.
    /// </summary>
    /// <value>Date</value>
    /// <seealso cref="CalendarEvent" />
    /// <seealso cref="ChannelContent{T, S}.CreatedAt" />
    /// <seealso cref="ChannelContent{T, S}.CreatedBy" />
    /// <seealso cref="StartsAt" />
    /// <seealso cref="Duration" />
    /// <seealso cref="Repeats" />
    public DateTime? EndsAt => Duration is null ? null : (StartsAt + Duration);
    #endregion

    #region Properties
    /// <summary>
    /// Gets whether the <see cref="CalendarEvent">calendar event</see> was set as private.
    /// </summary>
    /// <value>Whether the <see cref="CalendarEvent">calendar event</see> was set as private</value>
    /// <seealso cref="CalendarEvent" />
    /// <seealso cref="IsCanceled" />
    /// <seealso cref="Repeats" />
    public bool IsPrivate { get; }

    /// <summary>
    /// Gets the limit of how many <see cref="Member">members</see> can attend the <see cref="CalendarEvent">calendar event</see>.
    /// </summary>
    /// <value>The limit of how many <see cref="Member">members</see> can attend the <see cref="CalendarEvent">calendar event</see></value>
    /// <seealso cref="CalendarEvent" />
    /// <seealso cref="CalendarEventRsvp" />
    public uint? RsvpLimit { get; }

    /// <summary>
    /// Gets whether <see cref="Member">members</see> can attend the <see cref="CalendarEvent">calendar event</see>.
    /// </summary>
    /// <value>Whether <see cref="Member">members</see> can attend the <see cref="CalendarEvent">calendar event</see></value>
    /// <seealso cref="CalendarEvent" />
    /// <seealso cref="CalendarEventRsvp" />
    public bool RsvpDisabled { get; }

    /// <summary>
    /// Gets whether <see cref="Member">members</see> in the waitlist should be added to the <see cref="CalendarEvent">calendar event</see>.
    /// </summary>
    /// <value>Whether <see cref="Member">members</see> in the waitlist should be added to the <see cref="CalendarEvent">calendar event</see></value>
    /// <seealso cref="CalendarEvent" />
    /// <seealso cref="CalendarEventRsvp" />
    public bool AutofillWaitlist { get; }

    /// <summary>
    /// Gets the information about the <see cref="CalendarEvent">calendar event's</see> cancellation.
    /// </summary>
    /// <value>The information about the <see cref="CalendarEvent">calendar event's</see> cancellation</value>
    public CalendarEventCancellation? Cancellation { get; }

    /// <summary>
    /// Gets whether the <see cref="CalendarEvent">calendar event</see> was cancelled.
    /// </summary>
    /// <value>Whether the <see cref="CalendarEvent">calendar event</see> was cancelled</value>
    /// <seealso cref="CalendarEvent" />
    /// <seealso cref="Cancellation" />
    /// <seealso cref="IsPrivate" />
    /// <seealso cref="InSeries" />
    [MemberNotNullWhen(true, nameof(Cancellation), nameof(CanceledBy))]
    public bool IsCanceled => Cancellation is not null;

    /// <inheritdoc cref="CalendarEventCancellation.CreatedBy" />
    public HashId? CanceledBy => Cancellation?.CreatedBy;
    #endregion

    #region Properties Additional
    /// <summary>
    /// Gets whether the <see cref="CalendarEvent">calendar event</see> is in any kind of <see cref="CalendarEventSeries">calendar event series</see>.
    /// </summary>
    /// <value>Whether the <see cref="CalendarEvent">calendar event</see> is in any kind of <see cref="CalendarEventSeries">calendar event series</see></value>
    /// <seealso cref="CalendarEvent" />
    /// <seealso cref="SeriesId" />
    /// <seealso cref="IsCanceled" />
    /// <seealso cref="IsPrivate" />
    [MemberNotNullWhen(true, nameof(SeriesId))]
    public bool InSeries => SeriesId is not null;
    #endregion

    #region Properties Events
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="CalendarEvent">calendar event</see> gets edited.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="CalendarEvent">calendar event</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="CalendarEvent">calendar event</see> gets edited</returns>
    /// <seealso cref="Deleted" />
    public IObservable<CalendarEventEvent> Updated =>
        ParentClient
            .EventUpdated
            .Where(x =>
                x.ChannelId == ChannelId && x.Event.Id == Id
            );

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="CalendarEvent">calendar event</see> gets deleted.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="CalendarEvent">calendar event</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="CalendarEvent">calendar event</see> gets deleted</returns>
    /// <seealso cref="Updated" />
    public IObservable<CalendarEventEvent> Deleted =>
        ParentClient
            .EventDeleted
            .Where(x =>
                x.ChannelId == ChannelId && x.Event.Id == Id
            )
            .Take(1);

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when <see cref="CalendarEvent">calendar event's</see> <see cref="CalendarEventRsvp">RSVP</see> gets added/edited.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="CalendarEvent">calendar event</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="CalendarEvent">calendar event's</see> <see cref="CalendarEventRsvp">RSVP</see> gets added/edited</returns>
    /// <seealso cref="Updated" />
    /// <seealso cref="Deleted" />
    /// <seealso cref="RsvpDeleted" />
    /// <seealso cref="RsvpManyUpdated" />
    public IObservable<CalendarEventRsvpEvent> RsvpUpdated =>
        ParentClient
            .EventRsvpUpdated
            .Where(x =>
                x.ChannelId == ChannelId && x.EventId == Id
            );

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="CalendarEvent">calendar event's</see> <see cref="CalendarEventRsvp">RSVP</see> gets deleted.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="CalendarEvent">calendar event</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="CalendarEvent">calendar event's</see> <see cref="CalendarEventRsvp">RSVP</see> gets deleted</returns>
    /// <seealso cref="Updated" />
    /// <seealso cref="Deleted" />
    /// <seealso cref="RsvpUpdated" />
    /// <seealso cref="RsvpManyUpdated" />
    public IObservable<CalendarEventRsvpEvent> RsvpDeleted =>
        ParentClient
            .EventRsvpDeleted
            .Where(x =>
                x.ChannelId == ChannelId && x.EventId == Id
            );

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="CalendarEvent">calendar event's</see> multiple <see cref="CalendarEventRsvp">RSVPs</see> gets added/edited.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="CalendarEvent">calendar event</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="CalendarEvent">calendar event's</see> multiple <see cref="CalendarEventRsvp">RSVPs</see> gets added/edited</returns>
    /// <seealso cref="Updated" />
    /// <seealso cref="Deleted" />
    /// <seealso cref="RsvpUpdated" />
    /// <seealso cref="RsvpDeleted" />
    public IObservable<CalendarEventRsvpManyEvent> RsvpManyUpdated =>
        ParentClient
            .EventRsvpManyUpdated
            .Where(x =>
                x.ChannelId == ChannelId && x.EventId == Id
            );
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CalendarEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the channel content</param>
    /// <param name="channelId">The identifier of the channel where the channel content are</param>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the channel content are</param>
    /// <param name="name">The title of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> that created the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="createdAt">The date when the <see cref="CalendarEvent">calendar event</see> were created</param>
    /// <param name="startsAt">The date when the <see cref="CalendarEvent">calendar event</see> starts</param>
    /// <param name="description">The description of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="mentions"><see cref="Content.Mentions">The mentions</see> found in the <see cref="Description">description</see></param>
    /// <param name="location">The physical or non-physical location of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="url">The URL to the <see cref="CalendarEvent">calendar event's</see> services, place or anything related</param>
    /// <param name="color">The colour of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="duration">The duration of the <see cref="CalendarEvent">calendar event</see> in minutes</param>
    /// <param name="repeats">Whether the <see cref="CalendarEvent">calendar event</see> has more than one instance similar to itself</param>
    /// <param name="seriesId">The identifier of the repetition of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="isPrivate">Whether the <see cref="CalendarEvent">calendar event</see> was set as private</param>
    /// <param name="rsvpDisabled">Whether <see cref="Member">members</see> can attend the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="autofillWaitlist">Whether <see cref="Member">members</see> in the waitlist should be added to the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="rsvpLimit">The limit of how many <see cref="Member">members</see> can attend the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="cancellation">The information about the <see cref="CalendarEvent">calendar event's</see> cancellation</param>
    /// <returns>New <see cref="CalendarEvent" /> JSON instance</returns>
    /// <seealso cref="CalendarEvent" />
    [JsonConstructor]
    public CalendarEvent(
        [JsonProperty(Required = Required.Always)]
        uint id,

        [JsonProperty(Required = Required.Always)]
        Guid channelId,

        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        string name,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(Required = Required.Always)]
        DateTime startsAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool repeats = false,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Guid? seriesId = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        uint? duration = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? description = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Mentions? mentions = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? location = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Uri? url = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        SystemColor? color = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        uint? rsvpLimit = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool isPrivate = false,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool rsvpDisabled = false,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool autofillWaitlist = false,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        CalendarEventCancellation? cancellation = null
    ) : base(id, channelId, serverId, createdBy, createdAt) =>
        (Name, Description, Mentions, Location, Url, Color, StartsAt, Duration, Repeats, SeriesId, IsPrivate, RsvpLimit, RsvpDisabled, AutofillWaitlist, Cancellation) = (name, description, mentions, location, url, color, startsAt, duration is not null ? TimeSpan.FromMinutes((double)duration) : null, repeats, seriesId, isPrivate, rsvpLimit, rsvpDisabled, autofillWaitlist, cancellation);
    #endregion

    #region Methods Event
    /// <inheritdoc cref="AbstractGuildedClient.UpdateEventAsync(Guid, uint, CalendarEventContent)" />
    /// <param name="calendarEventContent">The new contents of the <see cref="CalendarEvent">calendar event</see> that is being updated</param>
    public Task<CalendarEvent> UpdateAsync(CalendarEventContent calendarEventContent) =>
        ParentClient.UpdateEventAsync(ChannelId, Id, calendarEventContent);

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
        ParentClient.DeleteEventAsync(ChannelId, Id);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateEventSeriesAsync(Guid, Guid, CalendarEventSeriesContent)" />
    /// <param name="calendarEventSeriesContent">The new contents of all the <see cref="CalendarEvent">calendar events</see> in the <see cref="CalendarEventSeries">series</see> or other informations</param>
    public Task UpdateSeriesAsync(CalendarEventSeriesContent calendarEventSeriesContent) =>
        InSeries ? ParentClient.UpdateEventSeriesAsync(ChannelId, (Guid)SeriesId, calendarEventSeriesContent) : throw new InvalidOperationException("Cannot update events in the calendar series that don't exist. CalendarEvent.SeriesId is null.");

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
        InSeries ? ParentClient.DeleteEventSeriesAsync(ChannelId, SeriesId.Value, deleteAfterCalendarEvent ? Id : null) : throw new InvalidOperationException("Cannot delete events in the calendar series that don't exist. CalendarEvent.SeriesId is null.");

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
        ParentClient.GetEventRsvpsAsync(ChannelId, Id);

    /// <inheritdoc cref="AbstractGuildedClient.GetRsvpAsync(Guid, uint, HashId)" />
    /// <param name="user">The identifier of the <see cref="User">user</see> to get <see cref="CalendarEventRsvp">RSVP</see> of</param>
    public Task<CalendarEventRsvp> GetRsvpAsync(HashId user) =>
        ParentClient.GetEventRsvpAsync(ChannelId, Id, user);

    /// <inheritdoc cref="AbstractGuildedClient.SetRsvpAsync(Guid, uint, HashId, CalendarEventRsvpStatus)" />
    /// <param name="user">The identifier of the <see cref="User">user</see> to set <see cref="CalendarEventRsvp">RSVP</see> of</param>
    /// <param name="status">The status of the <see cref="CalendarEvent">calendar RSVP</see> to set</param>
    public Task<CalendarEventRsvp> SetRsvpAsync(HashId user, CalendarEventRsvpStatus status) =>
        ParentClient.SetEventRsvpAsync(ChannelId, Id, user, status);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveRsvpAsync(Guid, uint, HashId)" />
    /// <param name="user">The identifier of the <see cref="User">user</see> to remove <see cref="CalendarEventRsvp">RSVP</see> of</param>
    public Task RemoveRsvpAsync(HashId user) =>
        ParentClient.RemoveEventRsvpAsync(ChannelId, Id, user);
    #endregion

    #region Methods Comments
    /// <inheritdoc cref="AbstractGuildedClient.CreateEventCommentAsync(Guid, uint, string)" />
    /// <param name="content">The content of the <see cref="CalendarEventComment">calendar event comment</see></param>
    public Task<CalendarEventComment> CreateCommentAsync(string content) =>
        ParentClient.CreateEventCommentAsync(ChannelId, Id, content);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateEventCommentAsync(Guid, uint, uint, string)" />
    /// <param name="comment">The identifier of the <see cref="CalendarEventComment">calendar event comment</see> to update</param>
    /// <param name="content">The new acontent of the <see cref="CalendarEventComment">calendar event comment</see></param>
    public Task<CalendarEventComment> UpdateCommentAsync(uint comment, string content) =>
        ParentClient.UpdateEventCommentAsync(ChannelId, Id, comment, content);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteEventCommentAsync(Guid, uint, uint)" />
    /// <param name="comment">The identifier of the <see cref="CalendarEventComment">calendar event comment</see> to delete</param>
    public Task DeleteCommentAsync(uint comment) =>
        ParentClient.DeleteEventCommentAsync(ChannelId, Id, comment);
    #endregion
}

/// <summary>
/// Represents the cancellation of a <see cref="CalendarEvent">calendar event</see>.
/// </summary>
/// <seealso cref="CalendarEvent" />
/// <seealso cref="ItemNote" />
/// <seealso cref="ItemNoteSummary" />
public class CalendarEventCancellation
{
    #region Properties
    /// <summary>
    /// Gets the reason why the <see cref="CalendarEvent">calendar event</see> was cancelled.
    /// </summary>
    /// <value>String</value>
    /// <seealso cref="CalendarEventCancellation" />
    /// <seealso cref="CalendarEvent" />
    /// <seealso cref="CreatedBy" />
    public string? Description { get; }

    /// <summary>
    /// Gets the identifier of <see cref="User">user</see> that cancelled the <see cref="CalendarEvent">calendar event</see>.
    /// </summary>
    /// <value><see cref="UserSummary.Id">User ID</see></value>
    /// <seealso cref="CalendarEventCancellation" />
    /// <seealso cref="CalendarEvent" />
    /// <seealso cref="Description" />
    /// <seealso cref="ChannelContent{TId, TServer}.CreatedBy" />
    public HashId CreatedBy { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CalendarEventCancellation" /> from the specified JSON properties.
    /// </summary>
    /// <param name="description">The reason why the <see cref="CalendarEvent">calendar event</see> was cancelled</param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> that cancelled the <see cref="CalendarEvent">calendar event</see></param>
    /// <returns>New <see cref="CalendarEventCancellation" /> JSON instance</returns>
    /// <seealso cref="CalendarEventCancellation" />
    /// <seealso cref="CalendarEvent" />
    [JsonConstructor]
    public CalendarEventCancellation(
        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? description = null
    ) =>
        (Description, CreatedBy) = (description, createdBy);
    #endregion
}
