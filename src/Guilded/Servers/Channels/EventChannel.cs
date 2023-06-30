using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Client;
using Guilded.Content;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Servers;

/// <summary>
/// Represents a <see cref="ChannelType.List">list</see>-type channel.
/// </summary>
/// <seealso cref="ServerChannel" />
/// <seealso cref="SchedulingChannel" />
/// <seealso cref="AnnouncementChannel" />
/// <seealso cref="MediaChannel" />
/// <seealso cref="DocChannel" />
/// <seealso cref="ForumChannel" />
/// <seealso cref="ChatChannel" />
/// <seealso cref="VoiceChannel" />
/// <seealso cref="StreamChannel" />
/// <seealso cref="ListChannel" />
/// <seealso cref="ChannelType" />
/// <seealso cref="Member" />
/// <seealso cref="Webhook" />
public class CalendarChannel : ServerChannel
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CalendarChannel" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the <see cref="ServerChannel">channel</see></param>
    /// <param name="groupId">The identifier of the parent group of the <see cref="ServerChannel">channel</see></param>
    /// <param name="serverId">The identifier of the parent <see cref="Server">server</see> of the <see cref="ServerChannel">channel</see></param>
    /// <param name="type">The type of content <see cref="ServerChannel">channel</see> holds</param>
    /// <param name="name">The name of the <see cref="ServerChannel">channel</see></param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> that created the <see cref="ServerChannel">channel</see></param>
    /// <param name="createdAt">The date when the <see cref="ServerChannel">channel</see> was created</param>
    /// <param name="updatedAt">The date when the <see cref="ServerChannel">channel</see> was edited</param>
    /// <param name="archivedBy">The identifier of <see cref="User">user</see> that archived the <see cref="ServerChannel">channel</see></param>
    /// <param name="archivedAt">The date when the <see cref="ServerChannel">channel</see> was archived</param>
    /// <param name="topic">The topic describing what the <see cref="ServerChannel">channel</see> is about</param>
    /// <param name="rootId">The identifier of the ancestor channel of the <see cref="ServerChannel">channel</see> that exist at the <see cref="Group">group</see> level</param>
    /// <param name="parentId">The identifier of the parent <see cref="ServerChannel">channel</see> of the <see cref="ServerChannel">channel</see></param>
    /// <param name="messageId">The identifier of the <see cref="Message">message</see> that hosts the thread</param>
    /// <param name="categoryId">The identifier of the parent category of the <see cref="ServerChannel">channel</see></param>
    /// <returns>New <see cref="CalendarChannel" /> JSON instance</returns>
    /// <seealso cref="CalendarChannel" />
    [JsonConstructor]
    public CalendarChannel(
        [JsonProperty(Required = Required.Always)]
        Guid id,

        [JsonProperty(Required = Required.Always)]
        HashId groupId,

        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        ChannelType type,

        [JsonProperty(Required = Required.Always)]
        string name,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? updatedAt = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? archivedBy = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? archivedAt = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? topic = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Guid? rootId = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Guid? parentId = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Guid? messageId = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        uint? categoryId = null
    ) : base(id, groupId, serverId, type, name, createdBy, createdAt, updatedAt, archivedBy, archivedAt, topic, rootId, parentId, messageId, categoryId) { }
    #endregion

    #region Methods Events
    /// <inheritdoc cref="AbstractGuildedClient.GetEventsAsync(Guid, uint?, DateTime?)" />
    /// <param name="limit">The limit of how many <see cref="CalendarEvent">calendar events</see> to get (default — <c>25</c>, values — <c>(0, 100]</c>)</param>
    /// <param name="before">The max limit of the creation date of fetched <see cref="CalendarEvent">calendar events</see></param>
    public Task<IList<CalendarEvent>> GetEventsAsync(uint? limit = null, DateTime? before = null) =>
        ParentClient.GetEventsAsync(Id, limit, before);

    /// <inheritdoc cref="AbstractGuildedClient.GetEventAsync(Guid, uint)" />
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> to get</param>
    public Task<CalendarEvent> GetEventAsync(uint calendarEvent) =>
        ParentClient.GetEventAsync(Id, calendarEvent);

    /// <inheritdoc cref="AbstractGuildedClient.CreateEventAsync(Guid, string, string?, string?, DateTime?, Uri?, Color?, TimeSpan?, uint?, bool, bool, bool, bool, IList{uint}?, CalendarEventRepetition?)" />
    /// <param name="calendarEvent">The creation information about the <see cref="CalendarEvent">calendar event</see> being created</param>
    public Task<CalendarEvent> CreateEventAsync(CalendarEventContent calendarEvent) =>
        ParentClient.CreateEventAsync(Id, calendarEvent);

    /// <inheritdoc cref="AbstractGuildedClient.CreateEventAsync(Guid, string, string?, string?, DateTime?, Uri?, Color?, TimeSpan?, uint?, bool, bool, bool, bool, IList{uint}?, CalendarEventRepetition?)" />
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
        CreateEventAsync(new CalendarEventContent(name, description, location, startsAt, url, color, duration, rsvpLimit, isPrivate, rsvpDisabled, autofillWaitlist, isAllDay, roleIds, repeatInfo));

    /// <inheritdoc cref="AbstractGuildedClient.CreateEventAsync(Guid, string, string?, string?, DateTime?, Uri?, Color?, TimeSpan?, uint?, bool, bool, bool, bool, IList{uint}?, CalendarEventRepetition?)" />
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
        CreateEventAsync(name, description, location, startsAt, url, color, (uint?)duration?.TotalMinutes, rsvpLimit, isPrivate, rsvpDisabled, autofillWaitlist, isAllDay, roleIds, repeatInfo);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateEventAsync(Guid, uint, CalendarEventContent)" />
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> to update/edit</param>
    /// <param name="calendarEventContent">The new contents of the <see cref="CalendarEvent">calendar event</see> that is being updated</param>
    public Task<CalendarEvent> UpdateEventAsync(uint calendarEvent, CalendarEventContent calendarEventContent) =>
        ParentClient.UpdateEventAsync(Id, calendarEvent, calendarEventContent);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateEventAsync(Guid, uint, CalendarEventContent)" />
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
        UpdateEventAsync(calendarEvent, new CalendarEventContent(name, description, location, startsAt, url, color, duration, rsvpLimit, isPrivate, rsvpDisabled, autofillWaitlist, isAllDay, roleIds));

    /// <inheritdoc cref="AbstractGuildedClient.UpdateEventAsync(Guid, uint, CalendarEventContent)" />
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
        UpdateEventAsync(calendarEvent, name, description, location, startsAt, url, color, (uint?)duration?.TotalMinutes, rsvpLimit, isPrivate, rsvpDisabled, autofillWaitlist, isAllDay, roleIds);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteEventAsync(Guid, uint)" />
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> to delete</param>
    public Task DeleteEventAsync(uint calendarEvent) =>
        ParentClient.DeleteEventAsync(Id, calendarEvent);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateEventSeriesAsync(Guid, Guid, CalendarEventSeriesContent)" />
    /// <param name="calendarEventSeries">The identifier of the <see cref="CalendarEventSeries">calendar event series</see> to update/edit <see cref="CalendarEvent">calendar events</see> in</param>
    /// <param name="calendarEventSeriesContent">The new contents of all the <see cref="CalendarEvent">calendar events</see> in the <see cref="CalendarEventSeries">series</see> or other informations</param>
    public Task UpdateEventSeriesAsync(Guid calendarEventSeries, CalendarEventSeriesContent calendarEventSeriesContent) =>
        ParentClient.UpdateEventSeriesAsync(Id, calendarEventSeries, calendarEventSeriesContent);

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
    /// <param name="calendarEvent">From which <see cref="CalendarEvent">calendar event</see> onwards the <see cref="CalendarEventSeries">calendar event series</see> should be updated</param>
    public Task UpdateEventSeriesAsync(
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
        UpdateEventSeriesAsync(calendarEventSeries, new CalendarEventSeriesContent(name, description, location, startsAt, url, color, duration, rsvpLimit, isPrivate, rsvpDisabled, autofillWaitlist, isAllDay, roleIds, repeatInfo, calendarEvent));

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
    /// <param name="calendarEvent">From which <see cref="CalendarEvent">calendar event</see> onwards the <see cref="CalendarEventSeries">calendar event series</see> should be updated</param>
    public Task UpdateEventSeriesAsync(
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
        UpdateEventSeriesAsync(calendarEventSeries, name, description, location, startsAt, url, color, duration, rsvpLimit, isPrivate, rsvpDisabled, autofillWaitlist, isAllDay, roleIds, repeatInfo, calendarEvent);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteEventSeriesAsync(Guid, Guid, uint?)" />
    /// <param name="calendarEventSeries">The identifier of the <see cref="CalendarEventSeries">calendar event series</see> to update/edit <see cref="CalendarEvent">calendar events</see> in </param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> after which all other <see cref="CalendarEvent">calendar events</see> in the <see cref="CalendarEventSeries">series</see> should be deleted</param>
    public Task DeleteEventSeriesAsync(Guid calendarEventSeries, uint? calendarEvent = null) =>
        ParentClient.DeleteEventSeriesAsync(Id, calendarEventSeries, calendarEvent);
    #endregion

    #region Methods Rsvp
    /// <inheritdoc cref="AbstractGuildedClient.GetRsvpsAsync(Guid, uint)" />
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> to get <see cref="CalendarEventRsvp">RSVPs</see> of</param>
    public Task<IList<CalendarEventRsvp>> GetRsvpsAsync(uint calendarEvent) =>
        ParentClient.GetEventRsvpsAsync(Id, calendarEvent);

    /// <inheritdoc cref="AbstractGuildedClient.GetRsvpAsync(Guid, uint, HashId)" />
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> where the <see cref="CalendarEventRsvp">RSVP</see> is</param>
    /// <param name="user">The identifier of the <see cref="User">user</see> to get <see cref="CalendarEventRsvp">RSVP</see> of</param>
    public Task<CalendarEventRsvp> GetRsvpAsync(uint calendarEvent, HashId user) =>
        ParentClient.GetEventRsvpAsync(Id, calendarEvent, user);

    /// <inheritdoc cref="AbstractGuildedClient.SetRsvpAsync(Guid, uint, HashId, CalendarEventRsvpStatus)" />
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> where the <see cref="CalendarEventRsvp">RSVP</see> is</param>
    /// <param name="user">The identifier of the <see cref="User">user</see> to set <see cref="CalendarEventRsvp">RSVP</see> of</param>
    /// <param name="status">The status of the <see cref="CalendarEvent">calendar RSVP</see> to set</param>
    public Task<CalendarEventRsvp> SetRsvpAsync(uint calendarEvent, HashId user, CalendarEventRsvpStatus status) =>
        ParentClient.SetEventRsvpAsync(Id, calendarEvent, user, status);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveRsvpAsync(Guid, uint, HashId)" />
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> where the <see cref="CalendarEventRsvp">calendar event RSVP</see> is</param>
    /// <param name="user">The identifier of the <see cref="User">user</see> to remove <see cref="CalendarEventRsvp">RSVP</see> of</param>
    public Task RemoveRsvpAsync(uint calendarEvent, HashId user) =>
        ParentClient.RemoveEventRsvpAsync(Id, calendarEvent, user);
    #endregion
}