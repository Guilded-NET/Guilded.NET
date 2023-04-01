using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Guilded.Servers;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Content;

/// <summary>
/// Represents the contents of a <see cref="CalendarEvent">calendar event</see> for creation and updating.
/// </summary>
/// <seealso cref="CalendarEventSeriesContent" />
/// <seealso cref="Base.MessageContent" />
/// <seealso cref="CalendarEvent" />
[JsonObject(MissingMemberHandling = MissingMemberHandling.Ignore,
            ItemNullValueHandling = NullValueHandling.Ignore)]
public class CalendarEventContent
{
    #region Properties
    /// <summary>
    /// Gets or sets the name of the <see cref="CalendarEvent">calendar event</see>.
    /// </summary>
    /// <value>The name of the <see cref="CalendarEvent">calendar event</see></value>
    /// <seealso cref="CalendarEventContent" />
    /// <seealso cref="Name" />
    /// <seealso cref="Description" />
    /// <seealso cref="Location" />
    /// <seealso cref="StartsAt" />
    /// <seealso cref="Url" />
    /// <seealso cref="Color" />
    /// <seealso cref="Duration" />
    /// <seealso cref="RsvpLimit" />
    /// <seealso cref="IsPrivate" />
    /// <seealso cref="RsvpDisabled" />
    /// <seealso cref="AutofillWaitlist" />
    /// <seealso cref="IsAllDay" />
    /// <seealso cref="RoleIds" />
    /// <seealso cref="RepeatInfo" />
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the <see cref="CalendarEvent">calendar event</see>.
    /// </summary>
    /// <value>The description of the <see cref="CalendarEvent">calendar event</see></value>
    /// <seealso cref="CalendarEventContent" />
    /// <seealso cref="Name" />
    /// <seealso cref="Description" />
    /// <seealso cref="Location" />
    /// <seealso cref="StartsAt" />
    /// <seealso cref="Url" />
    /// <seealso cref="Color" />
    /// <seealso cref="Duration" />
    /// <seealso cref="RsvpLimit" />
    /// <seealso cref="IsPrivate" />
    /// <seealso cref="RsvpDisabled" />
    /// <seealso cref="AutofillWaitlist" />
    /// <seealso cref="IsAllDay" />
    /// <seealso cref="RoleIds" />
    /// <seealso cref="RepeatInfo" />
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the place where <see cref="CalendarEvent">calendar event</see> can be attended.
    /// </summary>
    /// <remarks>
    /// <para>In terms of functionality, this doesn't do much besides display it in the <see cref="CalendarEvent">calendar event</see> info.</para>
    /// </remarks>
    /// <value>The place where <see cref="CalendarEvent">calendar event</see> can be attended</value>
    /// <seealso cref="CalendarEventContent" />
    /// <seealso cref="Name" />
    /// <seealso cref="Description" />
    /// <seealso cref="Location" />
    /// <seealso cref="StartsAt" />
    /// <seealso cref="Url" />
    /// <seealso cref="Color" />
    /// <seealso cref="Duration" />
    /// <seealso cref="RsvpLimit" />
    /// <seealso cref="IsPrivate" />
    /// <seealso cref="RsvpDisabled" />
    /// <seealso cref="AutofillWaitlist" />
    /// <seealso cref="IsAllDay" />
    /// <seealso cref="RoleIds" />
    /// <seealso cref="RepeatInfo" />
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets the date when the <see cref="CalendarEvent">calendar event</see> starts.
    /// </summary>
    /// <value>The date when the <see cref="CalendarEvent">calendar event</see> starts</value>
    /// <seealso cref="CalendarEventContent" />
    /// <seealso cref="Name" />
    /// <seealso cref="Description" />
    /// <seealso cref="Location" />
    /// <seealso cref="StartsAt" />
    /// <seealso cref="Url" />
    /// <seealso cref="Color" />
    /// <seealso cref="Duration" />
    /// <seealso cref="RsvpLimit" />
    /// <seealso cref="IsPrivate" />
    /// <seealso cref="RsvpDisabled" />
    /// <seealso cref="AutofillWaitlist" />
    /// <seealso cref="IsAllDay" />
    /// <seealso cref="RoleIds" />
    /// <seealso cref="RepeatInfo" />
    public DateTime? StartsAt { get; set; }

    /// <summary>
    /// Gets or sets the URL to the <see cref="CalendarEvent">calendar event's</see> services, place or anything related.
    /// </summary>
    /// <value>The URL to the <see cref="CalendarEvent">calendar event's</see> services, place or anything related</value>
    /// <seealso cref="CalendarEventContent" />
    /// <seealso cref="Name" />
    /// <seealso cref="Description" />
    /// <seealso cref="Location" />
    /// <seealso cref="StartsAt" />
    /// <seealso cref="Url" />
    /// <seealso cref="Color" />
    /// <seealso cref="Duration" />
    /// <seealso cref="RsvpLimit" />
    /// <seealso cref="IsPrivate" />
    /// <seealso cref="RsvpDisabled" />
    /// <seealso cref="AutofillWaitlist" />
    /// <seealso cref="IsAllDay" />
    /// <seealso cref="RoleIds" />
    /// <seealso cref="RepeatInfo" />
    public Uri? Url { get; set; }

    /// <summary>
    /// Gets or sets the colour of the <see cref="CalendarEvent">calendar event</see>.
    /// </summary>
    /// <value>The colour of the <see cref="CalendarEvent">calendar event</see></value>
    /// <seealso cref="CalendarEventContent" />
    /// <seealso cref="Name" />
    /// <seealso cref="Description" />
    /// <seealso cref="Location" />
    /// <seealso cref="StartsAt" />
    /// <seealso cref="Url" />
    /// <seealso cref="Color" />
    /// <seealso cref="Duration" />
    /// <seealso cref="RsvpLimit" />
    /// <seealso cref="IsPrivate" />
    /// <seealso cref="RsvpDisabled" />
    /// <seealso cref="AutofillWaitlist" />
    /// <seealso cref="IsAllDay" />
    /// <seealso cref="RoleIds" />
    /// <seealso cref="RepeatInfo" />
    public Color? Color { get; set; }

    /// <summary>
    /// Gets or sets the duration of the <see cref="CalendarEvent">calendar event</see> in minutes.
    /// </summary>
    /// <value>The duration of the <see cref="CalendarEvent">calendar event</see> in minutes</value>
    /// <seealso cref="CalendarEventContent" />
    /// <seealso cref="Name" />
    /// <seealso cref="Description" />
    /// <seealso cref="Location" />
    /// <seealso cref="StartsAt" />
    /// <seealso cref="Url" />
    /// <seealso cref="Color" />
    /// <seealso cref="Duration" />
    /// <seealso cref="RsvpLimit" />
    /// <seealso cref="IsPrivate" />
    /// <seealso cref="RsvpDisabled" />
    /// <seealso cref="AutofillWaitlist" />
    /// <seealso cref="IsAllDay" />
    /// <seealso cref="RoleIds" />
    /// <seealso cref="RepeatInfo" />
    public uint? Duration { get; set; }

    /// <summary>
    /// Gets or sets how many <see cref="Member">members</see> can attend the <see cref="CalendarEvent">calendar event</see>.
    /// </summary>
    /// <value>How many <see cref="Member">members</see> can attend the <see cref="CalendarEvent">calendar event</see></value>
    /// <seealso cref="CalendarEventContent" />
    /// <seealso cref="Name" />
    /// <seealso cref="Description" />
    /// <seealso cref="Location" />
    /// <seealso cref="StartsAt" />
    /// <seealso cref="Url" />
    /// <seealso cref="Color" />
    /// <seealso cref="Duration" />
    /// <seealso cref="RsvpLimit" />
    /// <seealso cref="IsPrivate" />
    /// <seealso cref="RsvpDisabled" />
    /// <seealso cref="AutofillWaitlist" />
    /// <seealso cref="IsAllDay" />
    /// <seealso cref="RoleIds" />
    /// <seealso cref="RepeatInfo" />
    public uint? RsvpLimit { get; set; }

    /// <summary>
    /// Gets or sets whether the <see cref="CalendarEvent">calendar event</see> is only visible to people in the RSVP rooster.
    /// </summary>
    /// <value>Whether the <see cref="CalendarEvent">calendar event</see> is only visible to people in the RSVP rooster+</value>
    /// <seealso cref="CalendarEventContent" />
    /// <seealso cref="Name" />
    /// <seealso cref="Description" />
    /// <seealso cref="Location" />
    /// <seealso cref="StartsAt" />
    /// <seealso cref="Url" />
    /// <seealso cref="Color" />
    /// <seealso cref="Duration" />
    /// <seealso cref="RsvpLimit" />
    /// <seealso cref="IsPrivate" />
    /// <seealso cref="RsvpDisabled" />
    /// <seealso cref="AutofillWaitlist" />
    /// <seealso cref="IsAllDay" />
    /// <seealso cref="RoleIds" />
    /// <seealso cref="RepeatInfo" />
    public bool? IsPrivate { get; set; }

    /// <summary>
    /// Gets or sets whether <see cref="Member">members</see> can attend the <see cref="CalendarEvent">calendar event</see>.
    /// </summary>
    /// <value>Whether <see cref="Member">members</see> can attend the <see cref="CalendarEvent">calendar event</see></value>
    /// <seealso cref="CalendarEventContent" />
    /// <seealso cref="Name" />
    /// <seealso cref="Description" />
    /// <seealso cref="Location" />
    /// <seealso cref="StartsAt" />
    /// <seealso cref="Url" />
    /// <seealso cref="Color" />
    /// <seealso cref="Duration" />
    /// <seealso cref="RsvpLimit" />
    /// <seealso cref="IsPrivate" />
    /// <seealso cref="AutofillWaitlist" />
    /// <seealso cref="IsAllDay" />
    /// <seealso cref="RoleIds" />
    /// <seealso cref="RepeatInfo" />
    public bool? RsvpDisabled { get; set; }

    /// <summary>
    /// Gets or sets whether the <see cref="CalendarEvent">calendar event</see> lasts all day.
    /// </summary>
    /// <value>Whether the <see cref="CalendarEvent">calendar event</see> lasts all day</value>
    /// <seealso cref="CalendarEventContent" />
    /// <seealso cref="Name" />
    /// <seealso cref="Description" />
    /// <seealso cref="Location" />
    /// <seealso cref="StartsAt" />
    /// <seealso cref="Url" />
    /// <seealso cref="Color" />
    /// <seealso cref="Duration" />
    /// <seealso cref="RsvpLimit" />
    /// <seealso cref="IsPrivate" />
    /// <seealso cref="RsvpDisabled" />
    /// <seealso cref="AutofillWaitlist" />
    /// <seealso cref="RoleIds" />
    /// <seealso cref="RepeatInfo" />
    public bool? IsAllDay { get; set; }

    /// <summary>
    /// Gets or sets whether the <see cref="CalendarEvent">calendar event</see> should last all day.
    /// </summary>
    /// <remarks>
    /// <para>If <see cref="Duration">duration</see> encompasses multiple days, it will last multiple whole days.</para>
    /// </remarks>
    /// <value>Whether the <see cref="CalendarEvent">calendar event</see> should last all day</value>
    /// <seealso cref="CalendarEventContent" />
    /// <seealso cref="Name" />
    /// <seealso cref="Description" />
    /// <seealso cref="Location" />
    /// <seealso cref="StartsAt" />
    /// <seealso cref="Url" />
    /// <seealso cref="Color" />
    /// <seealso cref="Duration" />
    /// <seealso cref="RsvpLimit" />
    /// <seealso cref="IsPrivate" />
    /// <seealso cref="RsvpDisabled" />
    /// <seealso cref="IsAllDay" />
    /// <seealso cref="RoleIds" />
    /// <seealso cref="RepeatInfo" />
    public bool? AutofillWaitlist { get; set; }

    /// <summary>
    /// Gets or sets the roles that the <see cref="CalendarEvent">calendar event</see> should be restricted to.
    /// </summary>
    /// <value>The roles that the <see cref="CalendarEvent">calendar event</see> should be restricted to</value>
    /// <seealso cref="CalendarEventContent" />
    /// <seealso cref="Name" />
    /// <seealso cref="Description" />
    /// <seealso cref="Location" />
    /// <seealso cref="StartsAt" />
    /// <seealso cref="Url" />
    /// <seealso cref="Color" />
    /// <seealso cref="Duration" />
    /// <seealso cref="RsvpLimit" />
    /// <seealso cref="IsPrivate" />
    /// <seealso cref="RsvpDisabled" />
    /// <seealso cref="AutofillWaitlist" />
    /// <seealso cref="IsAllDay" />
    /// <seealso cref="RoleIds" />
    /// <seealso cref="RepeatInfo" />
    public IList<uint>? RoleIds { get; set; }

    /// <summary>
    /// Gets or sets the information about <see cref="CalendarEventSeries">calendar event repetition</see>.
    /// </summary>
    /// <value>The information about <see cref="CalendarEventSeries">calendar event repetition</see></value>
    /// <seealso cref="CalendarEventContent" />
    /// <seealso cref="CalendarEventSeriesContent.CalendarEvent" />
    /// <seealso cref="Name" />
    /// <seealso cref="Description" />
    /// <seealso cref="Location" />
    /// <seealso cref="StartsAt" />
    /// <seealso cref="Url" />
    /// <seealso cref="Color" />
    /// <seealso cref="Duration" />
    /// <seealso cref="RsvpLimit" />
    /// <seealso cref="IsPrivate" />
    /// <seealso cref="RsvpDisabled" />
    /// <seealso cref="AutofillWaitlist" />
    /// <seealso cref="IsAllDay" />
    /// <seealso cref="RoleIds" />
    /// <seealso cref="RepeatInfo" />
    public CalendarEventRepetition? RepeatInfo { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CalendarEventContent" /> from the specified JSON properties.
    /// </summary>
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
    /// <returns>New <see cref="CalendarEventContent" /> JSON instance</returns>
    /// <seealso cref="CalendarEventContent" />
    [JsonConstructor]
    public CalendarEventContent(
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? name = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? description = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? location = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? startsAt = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Uri? url = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Color? color = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        uint? duration = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        uint? rsvpLimit = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool? isPrivate = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool? rsvpDisabled = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool? autofillWaitlist = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool? isAllDay = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        IList<uint>? roleIds = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        CalendarEventRepetition? repeatInfo = null
    ) =>
        (Name, Description, Location, StartsAt, Url, Color, Duration, RsvpLimit, IsPrivate, RsvpDisabled, AutofillWaitlist, IsAllDay, RoleIds, RepeatInfo) = (name, description, location, startsAt, url, color, duration, rsvpLimit, isPrivate, rsvpDisabled, autofillWaitlist, isAllDay, roleIds, repeatInfo);

    /// <summary>
    /// Initializes a new instance of <see cref="CalendarEventContent" /> from the specified JSON properties.
    /// </summary>
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
    /// <returns>New <see cref="CalendarEventContent" /> JSON instance</returns>
    /// <seealso cref="CalendarEventContent" />
    public CalendarEventContent(
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
        CalendarEventRepetition? repeatInfo = null
    ) : this(name, description, location, startsAt, url, color, (uint?)duration?.TotalMinutes, rsvpLimit, isPrivate, rsvpDisabled, autofillWaitlist, isAllDay, roleIds, repeatInfo) { }
    #endregion
}

/// <summary>
/// Represents the contents of a <see cref="CalendarEvent">calendar event</see> for creation and updating.
/// </summary>
/// <seealso cref="CalendarEventContent" />
/// <seealso cref="Base.MessageContent" />
/// <seealso cref="CalendarEvent" />
[JsonObject(MissingMemberHandling = MissingMemberHandling.Ignore,
            ItemNullValueHandling = NullValueHandling.Ignore)]
public class CalendarEventSeriesContent : CalendarEventContent
{
    #region Properties
    /// <summary>
    /// Gets or sets from which <see cref="CalendarEvent">calendar event</see> onwards the <see cref="CalendarEventSeries">calendar event series</see> should be updated.
    /// </summary>
    /// <value>From which <see cref="CalendarEvent">calendar event</see> onwards the <see cref="CalendarEventSeries">calendar event series</see> should be updated</value>
    /// <seealso cref="CalendarEventSeriesContent" />
    /// <seealso cref="CalendarEventContent.RepeatInfo" />
    public uint? CalendarEvent { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CalendarEventContent" /> from the specified JSON properties.
    /// </summary>
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
    /// <returns>New <see cref="CalendarEventSeriesContent" /> JSON instance</returns>
    /// <seealso cref="CalendarEventSeriesContent" />
    [JsonConstructor]
    public CalendarEventSeriesContent(
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? name = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? description = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? location = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? startsAt = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Uri? url = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Color? color = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        uint? duration = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        uint? rsvpLimit = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool? isPrivate = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool? rsvpDisabled = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool? autofillWaitlist = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool? isAllDay = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        IList<uint>? roleIds = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        CalendarEventRepetition? repeatInfo = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        uint? calendarEvent = null
    ) : base(name, description, location, startsAt, url, color, duration, rsvpLimit, isPrivate, rsvpDisabled, autofillWaitlist, isAllDay, roleIds, repeatInfo) =>
        CalendarEvent = calendarEvent;

    /// <summary>
    /// Initializes a new instance of <see cref="CalendarEventContent" /> from the specified JSON properties.
    /// </summary>
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
    /// <returns>New <see cref="CalendarEventContent" /> JSON instance</returns>
    /// <seealso cref="CalendarEventContent" />
    public CalendarEventSeriesContent(
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
    ) : this(name, description, location, startsAt, url, color, (uint?)duration?.TotalMinutes, rsvpLimit, isPrivate, rsvpDisabled, autofillWaitlist, isAllDay, roleIds, repeatInfo, calendarEvent) { }
    #endregion
}
