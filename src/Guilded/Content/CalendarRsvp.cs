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
/// <seealso cref="CalendarEventCancellation" />
/// <seealso cref="User" />
/// <seealso cref="Member" />
public class CalendarEventRsvp : ContentModel, ICreatableContent, IUpdatableContent, IServerBased, IChannelBased
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="User">user</see> whose <see cref="CalendarEventRsvp">RSVP</see> it is.
    /// </summary>
    /// <value>The identifier of the <see cref="User">user</see> whose <see cref="CalendarEventRsvp">RSVP</see> it is</value>
    /// <seealso cref="CalendarEventRsvp" />
    /// <seealso cref="EventId" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="ServerId" />
    public HashId UserId { get; }

    /// <summary>
    /// Gets the status of the <see cref="UserId">user's</see> <see cref="CalendarEventRsvp">RSVP</see>.
    /// </summary>
    /// <value>The status of the <see cref="UserId">user's</see> <see cref="CalendarEventRsvp">RSVP</see></value>
    /// <seealso cref="CalendarEventRsvp" />
    /// <seealso cref="UserId" />
    /// <seealso cref="EventId" />
    public CalendarEventRsvpStatus Status { get; }

    /// <summary>
    /// Gets the identifier of the parent <see cref="CalendarEvent">calendar event</see>.
    /// </summary>
    /// <value>The identifier of the parent <see cref="CalendarEvent">calendar event</see></value>
    /// <seealso cref="CalendarEventRsvp" />
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
    /// <seealso cref="CalendarEventRsvp" />
    /// <seealso cref="ServerId" />
    /// <seealso cref="EventId" />
    /// <seealso cref="UserId" />
    public Guid ChannelId { get; }

    /// <summary>
    /// Gets the identifier of the parent <see cref="Server">server</see> where the parent <see cref="CalendarEvent">calendar event</see> is.
    /// </summary>
    /// <value>The identifier of the parent <see cref="Server">server</see> where the parent <see cref="CalendarEvent">calendar event</see> is</value>
    /// <seealso cref="CalendarEventRsvp" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="EventId" />
    /// <seealso cref="UserId" />
    public HashId ServerId { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="Member">member</see> who created the <see cref="CalendarEventRsvp">calendar event RSVP</see>.
    /// </summary>
    /// <value>The identifier of the <see cref="Member">member</see> who created the <see cref="CalendarEventRsvp">calendar event RSVP</see></value>
    /// <seealso cref="CalendarEventRsvp" />
    /// <seealso cref="CreatedAt" />
    /// <seealso cref="UpdatedBy" />
    /// <seealso cref="UpdatedAt" />
    public HashId CreatedBy { get; }

    /// <summary>
    /// Gets the date when the <see cref="CalendarEventRsvp">calendar event RSVP</see> was created.
    /// </summary>
    /// <value>The date when the <see cref="CalendarEventRsvp">calendar event RSVP</see> was created</value>
    /// <seealso cref="CalendarEventRsvp" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="UpdatedAt" />
    /// <seealso cref="UpdatedBy" />
    public DateTime CreatedAt { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="Member">member</see> who updated the <see cref="CalendarEventRsvp">calendar event RSVP</see>.
    /// </summary>
    /// <remarks>
    /// <para>Only includes the <see cref="User">user</see> who updated the <see cref="CalendarEventRsvp">calendar event RSVP</see> most recently.</para>
    /// </remarks>
    /// <value>The identifier of the <see cref="Member">member</see> who updated the <see cref="CalendarEventRsvp">calendar event RSVP</see></value>
    /// <seealso cref="CalendarEventRsvp" />
    /// <seealso cref="UpdatedAt" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="CreatedAt" />
    public HashId? UpdatedBy { get; }

    /// <summary>
    /// Gets the date when the <see cref="CalendarEventRsvp">calendar event RSVP</see> was updated.
    /// </summary>
    /// <remarks>
    /// <para>Only includes date when the <see cref="CalendarEventRsvp">calendar event RSVP</see> was updated most recently.</para>
    /// </remarks>
    /// <value>The date when the <see cref="CalendarEventRsvp">calendar event RSVP</see> was updated</value>
    /// <seealso cref="CalendarEventRsvp" />
    /// <seealso cref="CreatedAt" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="UpdatedBy" />
    public DateTime? UpdatedAt { get; }
    #endregion

    #region Properties Events
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="CalendarEvent">calendar event's</see> <see cref="CalendarEventRsvp">RSVP</see> gets edited.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for the parent <see cref="CalendarEvent">calendar event</see> and <see cref="User">author</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="CalendarEvent">calendar event's</see> <see cref="CalendarEventRsvp">RSVP</see> gets edited</returns>
    /// <seealso cref="Updated" />
    /// <seealso cref="Deleted" />
    /// <seealso cref="ManyUpdated" />
    public IObservable<CalendarEventRsvpEvent> Updated =>
        ParentClient
            .EventRsvpUpdated
            .Where(x =>
                x.ChannelId == ChannelId && x.EventId == EventId && x.CreatedBy == CreatedBy
            );

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="CalendarEvent">calendar event's</see> <see cref="CalendarEventRsvp">RSVP</see> gets deleted.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="CalendarEvent">calendar event</see> and <see cref="User">author</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="CalendarEvent">calendar event's</see> <see cref="CalendarEventRsvp">RSVP</see> gets deleted</returns>
    /// <seealso cref="Updated" />
    /// <seealso cref="ManyUpdated" />
    public IObservable<CalendarEventRsvpEvent> Deleted =>
        ParentClient
            .EventRsvpDeleted
            .Where(x =>
                x.ChannelId == ChannelId && x.EventId == EventId && x.CreatedBy == CreatedBy
            );

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when <see cref="CalendarEvent">calendar event's</see> multiple <see cref="CalendarEventRsvp">RSVPs</see> gets added/edited.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="CalendarEvent">calendar event</see> specifically and <see cref="CalendarEventRsvpManyEvent">multiple RSVP event</see> that contains this <see cref="User">author</see>.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="CalendarEvent">calendar event's</see> multiple <see cref="CalendarEventRsvp">RSVPs</see> gets added/edited</returns>
    /// <seealso cref="Updated" />
    /// <seealso cref="Deleted" />
    public IObservable<CalendarEventRsvpManyEvent> ManyUpdated =>
        ParentClient
            .EventRsvpManyUpdated
            .Where(x =>
                x.ChannelId == ChannelId && x.EventId == EventId && x.Rsvps.Any(x => UserId == x.UserId)
            );
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CalendarEventRsvp" /> from the specified JSON properties.
    /// </summary>
    /// <param name="calendarEventId">The identifier of the <see cref="CalendarEvent">calendar event</see> where the <see cref="CalendarEventComment">calendar event comment</see> was created</param>
    /// <param name="userId">The identifier of the <see cref="User">user</see> whose <see cref="CalendarEventRsvp">RSVP</see> it is</param>
    /// <param name="channelId">The identifier of the <see cref="CalendarChannel">channel</see> where the <see cref="CalendarEventRsvp">calendar event RSVP</see> was created</param>
    /// <param name="serverId">The identifier of the parent <see cref="Server">server</see> where the parent <see cref="CalendarEvent">calendar event</see> is</param>
    /// <param name="status">The status of the <see cref="UserId">user's</see> <see cref="CalendarEventRsvp">RSVP</see></param>
    /// <param name="createdBy">The identifier of the <see cref="Member">member</see> who created the <see cref="CalendarEventRsvp">calendar event RSVP</see></param>
    /// <param name="createdAt">The date when the <see cref="CalendarEventRsvp">calendar event RSVP</see> was created</param>
    /// <param name="updatedBy">The identifier of the <see cref="Member">member</see> who updated the <see cref="CalendarEventRsvp">calendar event RSVP</see></param>
    /// <param name="updatedAt">The date when the <see cref="CalendarEventRsvp">calendar event RSVP</see> was updated</param>
    /// <returns>New <see cref="CalendarEventRsvp" /> JSON instance</returns>
    /// <seealso cref="CalendarEventRsvp" />
    [JsonConstructor]
    public CalendarEventRsvp(
        [JsonProperty(Required = Required.Always)]
        uint calendarEventId,

        [JsonProperty(Required = Required.Always)]
        HashId userId,

        [JsonProperty(Required = Required.Always)]
        Guid channelId,

        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        CalendarEventRsvpStatus status,

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
    /// <inheritdoc cref="AbstractGuildedClient.SetRsvpAsync(Guid, uint, HashId, CalendarEventRsvpStatus)" />
    /// <param name="status">The new status of the <see cref="CalendarEvent">calendar event RSVP</see></param>
    public Task<CalendarEventRsvp> SetAsync(CalendarEventRsvpStatus status) =>
        ParentClient.SetEventRsvpAsync(ChannelId, EventId, UserId, status);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveRsvpAsync(Guid, uint, HashId)" />
    public Task RemoveAsync() =>
        ParentClient.RemoveEventRsvpAsync(ChannelId, EventId, UserId);
    #endregion
}