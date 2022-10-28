using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Client;
using Guilded.Events;
using Guilded.Servers;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Content;

/// <summary>
/// Represents <see cref="User">user's</see> invitation to or <see cref="User">user's</see> status on a <see cref="CalendarEvent">calendar event</see>.
/// </summary>
/// <seealso cref="CalendarEvent" />
/// <seealso cref="CalendarCancellation" />
/// <seealso cref="User" />
/// <seealso cref="Member" />
public class CalendarRsvp : ContentModel, ICreatableContent, IUpdatableContent, IServerBased, IChannelBased
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="User">user</see> whose RSVP it is.
    /// </summary>
    /// <value><see cref="UserSummary.Id">User ID</see></value>
    /// <seealso cref="CalendarRsvp" />
    /// <seealso cref="EventId" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="ServerId" />
    public HashId UserId { get; }

    /// <summary>
    /// Gets the status of the <see cref="UserId">user's</see> <see cref="CalendarRsvp">RSVP</see>.
    /// </summary>
    /// <value><see cref="CalendarRsvpStatus">RSVP Status</see></value>
    /// <seealso cref="CalendarRsvp" />
    /// <seealso cref="UserId" />
    /// <seealso cref="EventId" />
    public CalendarRsvpStatus Status { get; }

    /// <summary>
    /// Gets the identifier of the parent <see cref="CalendarEvent">calendar event</see>.
    /// </summary>
    /// <value><see cref="ChannelContent{TId, TServer}.Id">Calendar event ID</see></value>
    /// <seealso cref="CalendarRsvp" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="ServerId" />
    /// <seealso cref="UserId" />
    public uint EventId { get; }

    /// <inheritdoc cref="EventId" />
    [Obsolete($"Use `{nameof(EventId)}` instead")]
    public uint CalendarEventId => EventId;

    /// <summary>
    /// Gets the identifier of the parent <see cref="CalendarChannel">channel</see> where the <see cref="CalendarEvent">calendar event</see> is.
    /// </summary>
    /// <value><see cref="ServerChannel.Id">Channel ID</see></value>
    /// <seealso cref="CalendarRsvp" />
    /// <seealso cref="ServerId" />
    /// <seealso cref="EventId" />
    /// <seealso cref="UserId" />
    public Guid ChannelId { get; }

    /// <summary>
    /// Gets the identifier of the parent <see cref="Server">server</see> where the parent <see cref="CalendarEvent">calendar event</see> is.
    /// </summary>
    /// <value><see cref="Server.Id">Server ID</see></value>
    /// <seealso cref="CalendarRsvp" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="EventId" />
    /// <seealso cref="UserId" />
    public HashId ServerId { get; }

    /// <summary>
    /// Gets the identifier of <see cref="Member">the member</see> who created the <see cref="CalendarRsvp">calendar RSVP</see>.
    /// </summary>
    /// <value><see cref="UserSummary.Id">User ID</see></value>
    /// <seealso cref="CalendarRsvp" />
    /// <seealso cref="CreatedAt" />
    /// <seealso cref="UpdatedBy" />
    /// <seealso cref="UpdatedAt" />
    public HashId CreatedBy { get; }

    /// <summary>
    /// Gets the date when the <see cref="CalendarRsvp">calendar RSVP</see> was created.
    /// </summary>
    /// <value>Date</value>
    /// <seealso cref="CalendarRsvp" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="UpdatedAt" />
    /// <seealso cref="UpdatedBy" />
    public DateTime CreatedAt { get; }

    /// <summary>
    /// Gets the identifier of <see cref="Member">the member</see> who updated the <see cref="CalendarRsvp">calendar RSVP</see>.
    /// </summary>
    /// <remarks>
    /// <para>Only includes the <see cref="User">user</see> who updated the <see cref="CalendarRsvp">calendar RSVP</see> most recently.</para>
    /// </remarks>
    /// <value><see cref="UserSummary.Id">User ID</see>?</value>
    /// <seealso cref="CalendarRsvp" />
    /// <seealso cref="UpdatedAt" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="CreatedAt" />
    public HashId? UpdatedBy { get; }

    /// <summary>
    /// Gets the date when the <see cref="CalendarRsvp">calendar RSVP</see> was updated.
    /// </summary>
    /// <remarks>
    /// <para>Only includes date when the <see cref="CalendarRsvp">calendar RSVP</see> was updated most recently.</para>
    /// </remarks>
    /// <value>Date</value>
    /// <seealso cref="CalendarRsvp" />
    /// <seealso cref="CreatedAt" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="UpdatedBy" />
    public DateTime? UpdatedAt { get; }
    #endregion

    #region Properties Events
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="CalendarEvent">calendar event's</see> <see cref="CalendarRsvp">RSVP</see> gets edited.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for the parent <see cref="CalendarEvent">calendar event</see> and <see cref="User">author</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="CalendarEvent">calendar event's</see> <see cref="CalendarRsvp">RSVP</see> gets edited</returns>
    /// <seealso cref="Updated" />
    /// <seealso cref="Deleted" />
    /// <seealso cref="ManyUpdated" />
    public IObservable<CalendarRsvpEvent> Updated =>
        ParentClient
            .RsvpUpdated
            .Where(x =>
                x.ChannelId == ChannelId && x.EventId == EventId && x.CreatedBy == CreatedBy
            );

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="CalendarEvent">calendar event's</see> <see cref="CalendarRsvp">RSVP</see> gets deleted.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="CalendarEvent">calendar event</see> and <see cref="User">author</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="CalendarEvent">calendar event's</see> <see cref="CalendarRsvp">RSVP</see> gets deleted</returns>
    /// <seealso cref="Updated" />
    /// <seealso cref="ManyUpdated" />
    public IObservable<CalendarRsvpEvent> Deleted =>
        ParentClient
            .RsvpDeleted
            .Where(x =>
                x.ChannelId == ChannelId && x.EventId == EventId && x.CreatedBy == CreatedBy
            );

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when <see cref="CalendarEvent">calendar event's</see> multiple <see cref="CalendarRsvp">RSVPs</see> gets added/edited.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="CalendarEvent">calendar event</see> specifically and <see cref="CalendarRsvpManyEvent">multiple RSVP event</see> that contains this <see cref="User">author</see>.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="CalendarEvent">calendar event's</see> multiple <see cref="CalendarRsvp">RSVPs</see> gets added/edited</returns>
    /// <seealso cref="Updated" />
    /// <seealso cref="Deleted" />
    public IObservable<CalendarRsvpManyEvent> ManyUpdated =>
        ParentClient
            .RsvpManyUpdated
            .Where(x =>
                x.ChannelId == ChannelId && x.EventId == EventId && x.Rsvps.Any(x => UserId == x.UserId)
            );
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
        (UserId, EventId, ChannelId, ServerId, Status, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt) = (userId, calendarEventId, channelId, serverId, status, createdBy, createdAt, updatedBy, updatedAt);
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.SetRsvpAsync(Guid, uint, HashId, CalendarRsvpStatus)" />
    /// <param name="status">The new status of the <see cref="CalendarEvent">calendar event RSVP</see></param>
    public Task<CalendarRsvp> SetAsync(CalendarRsvpStatus status) =>
        ParentClient.SetRsvpAsync(ChannelId, EventId, UserId, status);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveRsvpAsync(Guid, uint, HashId)" />
    public Task RemoveAsync() =>
        ParentClient.RemoveRsvpAsync(ChannelId, EventId, UserId);
    #endregion
}