using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Guilded.Base.Client;
using Guilded.Base.Content;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Servers;

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
    /// <param name="parentId">The identifier of the parent <see cref="ServerChannel">channel</see> of the <see cref="ServerChannel">channel</see></param>
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
        Guid? parentId = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        uint? categoryId = null
    ) : base(id, groupId, serverId, type, name, createdBy, createdAt, updatedAt, archivedBy, archivedAt, topic, parentId, categoryId) { }
    #endregion

    #region Methods Events
    /// <inheritdoc cref="BaseGuildedClient.GetEventsAsync(Guid, uint?, DateTime?)" />
    /// <param name="limit">The limit of how many <see cref="CalendarEvent">calendar events</see> to get (default — <c>25</c>, values — <c>(0, 100]</c>)</param>
    /// <param name="before">The max limit of the creation date of fetched <see cref="CalendarEvent">calendar events</see></param>
    public Task<IList<CalendarEvent>> GetEventsAsync(uint? limit = null, DateTime? before = null) =>
        ParentClient.GetEventsAsync(Id, limit, before);

    /// <inheritdoc cref="BaseGuildedClient.GetEventAsync(Guid, uint)" />
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> to get</param>
    public Task<CalendarEvent> GetEventAsync(uint calendarEvent) =>
        ParentClient.GetEventAsync(Id, calendarEvent);

    /// <inheritdoc cref="BaseGuildedClient.GetEventsAsync(Guid, uint?, DateTime?)" />
    /// <param name="name">The title of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="description">The description of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="location">The physical or non-physical location of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="url">The URL to <see cref="CalendarEvent">the calendar event's</see> services, place or anything related</param>
    /// <param name="color">The colour of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="duration">The duration of the <see cref="CalendarEvent">calendar event</see> in minutes</param>
    /// <param name="rsvpLimit">The limit of how many <see cref="Users.User">users</see> can be invited or attend the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="isPrivate">Whether the <see cref="CalendarEvent">calendar event</see> is private</param>
    /// <param name="startsAt">The date when the <see cref="CalendarEvent">calendar event</see> starts</param>
    public Task<CalendarEvent> CreateEventAsync(string name, string? description = null, string? location = null, DateTime? startsAt = null, Uri? url = null, Color? color = null, uint? duration = null, uint? rsvpLimit = null, bool isPrivate = false) =>
        ParentClient.CreateEventAsync(Id, name, description, location, startsAt, url, color, duration, rsvpLimit, isPrivate);

    /// <inheritdoc cref="CreateEventAsync(string, string, string, DateTime?, Uri?, Color?, uint?, uint?, bool)" />
    public Task<CalendarEvent> CreateEventAsync(string name, string? description = null, string? location = null, DateTime? startsAt = null, Uri? url = null, Color? color = null, TimeSpan? duration = null, uint? rsvpLimit = null, bool isPrivate = false) =>
        CreateEventAsync(name, description, location, startsAt, url, color, (uint?)duration?.TotalMinutes, rsvpLimit, isPrivate);

    /// <inheritdoc cref="BaseGuildedClient.UpdateEventAsync(Guid, uint, string?, string?, string?, DateTime?, Uri?, Color?, uint?, bool?)" />
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> to update/edit</param>
    /// <param name="name">The new name of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="description">The new description of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="location">The new location of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="startsAt">The new starting date of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="url">The new URL of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="color">The new colour of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="duration">The new length/duration of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="isPrivate">Whether the <see cref="CalendarEvent">calendar event</see> is now private or not private anymore</param>
    public Task<CalendarEvent> UpdateEventAsync(uint calendarEvent, string? name = null, string? description = null, string? location = null, DateTime? startsAt = null, Uri? url = null, Color? color = null, uint? duration = null, bool? isPrivate = null) =>
        ParentClient.UpdateEventAsync(Id, calendarEvent, name, description, location, startsAt, url, color, duration, isPrivate);

    /// <inheritdoc cref="UpdateEventAsync(uint, string, string, string, DateTime?, Uri?, Color?, uint?, bool?)" />
    public Task<CalendarEvent> UpdateEventAsync(uint calendarEvent, string? name = null, string? description = null, string? location = null, DateTime? startsAt = null, Uri? url = null, Color? color = null, TimeSpan? duration = null, bool? isPrivate = null) =>
        UpdateEventAsync(calendarEvent, name, description, location, startsAt, url, color, (uint?)duration?.TotalMinutes, isPrivate);

    /// <inheritdoc cref="BaseGuildedClient.DeleteEventAsync(Guid, uint)" />
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> to delete</param>
    public Task DeleteEventAsync(uint calendarEvent) =>
        ParentClient.DeleteEventAsync(Id, calendarEvent);
    #endregion

    #region Methods Rsvp
    /// <inheritdoc cref="BaseGuildedClient.GetRsvpsAsync(Guid, uint)" />
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> to get <see cref="CalendarRsvp">RSVPs</see> of</param>
    public Task<IList<CalendarRsvp>> GetRsvpsAsync(uint calendarEvent) =>
        ParentClient.GetRsvpsAsync(Id, calendarEvent);

    /// <inheritdoc cref="BaseGuildedClient.GetRsvpAsync(Guid, uint, HashId)" />
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> where the <see cref="CalendarRsvp">RSVP</see> is</param>
    /// <param name="user">The identifier of <see cref="Users.User">the user</see> to get <see cref="CalendarRsvp">RSVP</see> of</param>
    public Task<CalendarRsvp> GetRsvpAsync(uint calendarEvent, HashId user) =>
        ParentClient.GetRsvpAsync(Id, calendarEvent, user);

    /// <inheritdoc cref="BaseGuildedClient.SetRsvpAsync(Guid, uint, HashId, CalendarRsvpStatus)" />
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> where the <see cref="CalendarRsvp">RSVP</see> is</param>
    /// <param name="user">The identifier of <see cref="Users.User">the user</see> to set <see cref="CalendarRsvp">RSVP</see> of</param>
    /// <param name="status">The status of <see cref="CalendarEvent">the RSVP</see> to set</param>
    public Task<CalendarRsvp> SetRsvpAsync(uint calendarEvent, HashId user, CalendarRsvpStatus status) =>
        ParentClient.SetRsvpAsync(Id, calendarEvent, user, status);

    /// <inheritdoc cref="BaseGuildedClient.RemoveRsvpAsync(Guid, uint, HashId)" />
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> where <see cref="CalendarRsvp">the RSVP</see> is</param>
    /// <param name="user">The identifier of <see cref="Users.User">the user</see> to remove <see cref="CalendarRsvp">RSVP</see> of</param>
    public Task RemoveRsvpAsync(uint calendarEvent, HashId user) =>
        ParentClient.RemoveRsvpAsync(Id, calendarEvent, user);
    #endregion
}