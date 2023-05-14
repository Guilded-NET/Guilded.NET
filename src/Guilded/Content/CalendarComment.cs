using System;
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
/// Represents a comment on a <see cref="CalendarEvent">calendar event</see>.
/// </summary>
/// <seealso cref="CalendarEvent" />
/// <seealso cref="CalendarChannel" />
/// <seealso cref="ChannelContent{TId, TServer}" />
public class CalendarEventComment : BaseComment
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="CalendarEvent">calendar event</see> where the <see cref="CalendarEventComment">calendar event comment</see> was created.
    /// </summary>
    /// <value>The identifier of the <see cref="CalendarEvent">calendar event</see> where the <see cref="CalendarEventComment">calendar event comment</see> was created</value>
    /// <seealso cref="CalendarEventComment" />
    /// <seealso cref="BaseComment.Id" />
    /// <seealso cref="BaseComment.ChannelId" />
    /// <seealso cref="BaseComment.CreatedBy" />
    public uint EventId { get; }
    #endregion

    #region Properties Events
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="CalendarEventComment">calendar event comment</see> gets edited.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="CalendarEventComment">calendar event comment</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="CalendarEventComment">calendar event comment</see> gets edited</returns>
    /// <seealso cref="Deleted" />
    public IObservable<CalendarEventCommentEvent> Updated =>
        ParentClient
            .EventCommentUpdated
            .Where(x =>
                x.ChannelId == ChannelId && x.EventId == EventId && x.Id == Id
            );

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="CalendarEventComment">calendar event comment</see> gets deleted.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="CalendarEventComment">calendar event comment</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="CalendarEventComment">calendar event comment</see> gets deleted</returns>
    /// <seealso cref="Updated" />
    public IObservable<CalendarEventCommentEvent> Deleted =>
        ParentClient
            .EventCommentDeleted
            .Where(x =>
                x.ChannelId == ChannelId && x.EventId == EventId && x.Id == Id
            )
            .Take(1);
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CalendarEventComment" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the <see cref="CalendarEventComment"></see></param>
    /// <param name="calendarEventId">The identifier of the <see cref="CalendarEvent">calendar event</see> where the <see cref="CalendarEventComment">calendar event comment</see> was created</param>
    /// <param name="channelId">The identifier of the <see cref="ServerChannel">channel</see> where the <see cref="CalendarEventComment">calendar event comment</see> was created</param>
    /// <param name="content">The full-Markdown text contents of the <see cref="CalendarEventComment">calendar event comment</see></param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> that created the <see cref="CalendarEventComment">calendar event comment</see></param>
    /// <param name="createdAt">The date when the <see cref="CalendarEventComment">calendar event comment</see> was created</param>
    /// <param name="updatedAt">The date when the <see cref="CalendarEventComment">calendar event comment</see> was edited</param>
    /// <returns>New <see cref="CalendarEventComment" /> JSON instance</returns>
    /// <seealso cref="CalendarEventComment" />
    [JsonConstructor]
    public CalendarEventComment(
        [JsonProperty(Required = Required.Always)]
        uint id,

        [JsonProperty(Required = Required.Always)]
        uint calendarEventId,

        [JsonProperty(Required = Required.Always)]
        Guid channelId,

        [JsonProperty(Required = Required.Always)]
        string content,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? updatedAt = null
    ) : base(id, channelId, content, createdBy, createdAt, updatedAt) =>
        EventId = calendarEventId;
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.CreateEventCommentAsync(Guid, uint, string)" />
    /// <param name="content">The content of the <see cref="CalendarEventComment">calendar event comment</see></param>
    public Task<CalendarEventComment> ReplyAsync(string content) =>
        ParentClient.CreateEventCommentAsync(ChannelId, EventId, content);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateEventCommentAsync(Guid, uint, uint, string)" />
    /// <param name="content">The new Markdown content of the <see cref="CalendarEventComment">calendar event comment</see></param>
    public Task<CalendarEventComment> UpdateAsync(string content) =>
        ParentClient.UpdateEventCommentAsync(ChannelId, EventId, Id, content);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteEventCommentAsync(Guid, uint, uint)" />
    public Task DeleteAsync() =>
        ParentClient.DeleteEventCommentAsync(ChannelId, EventId, Id);
    #endregion
}