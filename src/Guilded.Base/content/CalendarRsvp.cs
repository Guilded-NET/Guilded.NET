using System;
using System.Threading.Tasks;
using Guilded.Base.Servers;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Content;

/// <summary>
/// Represents <see cref="User">user's</see> invitation to or <see cref="User">user's</see> status on <see cref="CalendarEvent">a calendar event</see>.
/// </summary>
/// <seealso cref="CalendarEvent" />
/// <seealso cref="CalendarCancellation" />
/// <seealso cref="User" />
/// <seealso cref="Member" />
public class CalendarRsvp : ContentModel, ICreatableContent, IUpdatableContent, IServerBased, IChannelBased
{
    #region Properties
    /// <summary>
    /// Gets the identifier of <see cref="User">the user</see> whose RSVP it is.
    /// </summary>
    /// <value><see cref="UserSummary.Id">User ID</see></value>
    /// <seealso cref="CalendarRsvp" />
    /// <seealso cref="CalendarEventId" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="ServerId" />
    public HashId UserId { get; }

    /// <summary>
    /// Gets the status of the <see cref="UserId">user's</see> <see cref="CalendarRsvp">RSVP</see>.
    /// </summary>
    /// <value><see cref="CalendarRsvpStatus">RSVP Status</see></value>
    /// <seealso cref="CalendarRsvp" />
    /// <seealso cref="UserId" />
    /// <seealso cref="CalendarEventId" />
    public CalendarRsvpStatus Status { get; }

    /// <summary>
    /// Gets the identifier of <see cref="CalendarEvent">the parent calendar event</see>.
    /// </summary>
    /// <value><see cref="ChannelContent{TId, TServer}.Id">Calendar event ID</see></value>
    /// <seealso cref="CalendarRsvp" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="ServerId" />
    /// <seealso cref="UserId" />
    public uint CalendarEventId { get; }

    /// <summary>
    /// Gets the identifier of <see cref="ServerChannel">the parent channel</see> where <see cref="CalendarEventId">the calendar event</see> is.
    /// </summary>
    /// <value><see cref="ServerChannel.Id">Channel ID</see></value>
    /// <seealso cref="CalendarRsvp" />
    /// <seealso cref="ServerId" />
    /// <seealso cref="CalendarEventId" />
    /// <seealso cref="UserId" />
    public Guid ChannelId { get; }

    /// <summary>
    /// Gets the identifier of <see cref="Server">the parent server</see> where <see cref="CalendarEventId">the calendar event</see> is.
    /// </summary>
    /// <value><see cref="Server.Id">Server ID</see></value>
    /// <seealso cref="CalendarRsvp" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="CalendarEventId" />
    /// <seealso cref="UserId" />
    public HashId ServerId { get; }

    /// <summary>
    /// Gets the identifier of <see cref="Member">the member</see> who created <see cref="CalendarRsvp">the RSVP</see>.
    /// </summary>
    /// <value><see cref="UserSummary.Id">User ID</see></value>
    /// <seealso cref="CalendarRsvp" />
    /// <seealso cref="CreatedAt" />
    /// <seealso cref="UpdatedBy" />
    /// <seealso cref="UpdatedAt" />
    public HashId CreatedBy { get; }

    /// <summary>
    /// Gets the date when <see cref="CalendarRsvp">the RSVP</see> was created.
    /// </summary>
    /// <value>Date</value>
    /// <seealso cref="CalendarRsvp" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="UpdatedAt" />
    /// <seealso cref="UpdatedBy" />
    public DateTime CreatedAt { get; }

    /// <summary>
    /// Gets the identifier of <see cref="Member">the member</see> who updated <see cref="CalendarRsvp">the RSVP</see>.
    /// </summary>
    /// <remarks>
    /// <para>Only includes <see cref="User">the user</see> who updated <see cref="CalendarRsvp">the RSVP</see> most recently.</para>
    /// </remarks>
    /// <value><see cref="UserSummary.Id">User ID</see>?</value>
    /// <seealso cref="CalendarRsvp" />
    /// <seealso cref="UpdatedAt" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="CreatedAt" />
    public HashId? UpdatedBy { get; }

    /// <summary>
    /// Gets the date when <see cref="CalendarRsvp">the RSVP</see> was updated.
    /// </summary>
    /// <remarks>
    /// <para>Only includes date when <see cref="CalendarRsvp">the RSVP</see> was updated most recently.</para>
    /// </remarks>
    /// <value>Date</value>
    /// <seealso cref="CalendarRsvp" />
    /// <seealso cref="CreatedAt" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="UpdatedBy" />
    public DateTime? UpdatedAt { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CalendarRsvp" /> from the specified JSON properties.
    /// </summary>
    /// <returns>New <see cref="CalendarRsvp" /> JSON instance</returns>
    /// <seealso cref="CalendarRsvp" />
    [JsonConstructor]
    public CalendarRsvp(
        [JsonProperty(Required = Required.Always)]
        uint calendarEventId,

        [JsonProperty(Required = Required.Always)]
        HashId userId,

        [JsonProperty(Required = Required.Always)]
        Guid channelId,

        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        CalendarRsvpStatus status,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? updatedBy = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? updatedAt = null
    ) =>
        (UserId, CalendarEventId, ChannelId, ServerId, Status, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt) = (userId, calendarEventId, channelId, serverId, status, createdBy, createdAt, updatedBy, updatedAt);
    #endregion

    #region Methods
    /// <inheritdoc cref="BaseGuildedClient.SetRsvpAsync(Guid, uint, HashId, CalendarRsvpStatus)" />
    /// <param name="status">The new status of <see cref="CalendarEvent">the RSVP</see></param>
    public Task<CalendarRsvp> SetAsync(CalendarRsvpStatus status) =>
        ParentClient.SetRsvpAsync(ChannelId, CalendarEventId, UserId, status);

    /// <inheritdoc cref="BaseGuildedClient.RemoveRsvpAsync(Guid, uint, HashId)" />
    public Task RemoveAsync() =>
        ParentClient.RemoveRsvpAsync(ChannelId, CalendarEventId, UserId);
    #endregion
}