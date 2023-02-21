using System;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Client;
using Guilded.Events;
using Guilded.Servers;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Content;

/// <summary>
/// Represents a <see cref="CalendarEvent">calendar event</see> <see cref="Reaction">reaction</see>.
/// </summary>
/// <seealso cref="Reaction" />
/// <seealso cref="TopicCommentReaction" />
/// <seealso cref="MessageReaction" />
/// <seealso cref="TopicReactionEvent" />
public class CalendarEventReaction : Reaction
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="CalendarEvent">calendar event</see> that <see cref="User">user</see> reacted on.
    /// </summary>
    /// <value>The <see cref="CalendarEvent">calendar event</see> identifier of the <see cref="Reaction">reaction</see></value>
    /// <seealso cref="CalendarEventReaction" />
    /// <seealso cref="Reaction" />
    /// <seealso cref="CalendarEvent" />
    /// <seealso cref="Reaction.ChannelId" />
    /// <seealso cref="Reaction.Id" />
    public uint EventId { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of <see cref="TopicReaction" /> from the specified JSON properties.
    /// </summary>
    /// <param name="emote">The <see cref="Emote">emote</see> with which the <see cref="Reaction.CreatedBy">user</see> reacted</param>
    /// <param name="createdBy">The identifier of the <see cref="User">user</see> that reacted</param>
    /// <param name="channelId">The identifier of the <see cref="ServerChannel">channel</see> where the <see cref="CalendarEvent">calendar event</see> is</param>
    /// <param name="calendarEventId">The identifier of the <see cref="CalendarEvent">calendar event</see> that <see cref="Reaction.CreatedBy">user</see> reacted on</param>
    /// <returns>New <see cref="TopicReaction" /> JSON instance</returns>
    /// <seealso cref="TopicReaction" />
    /// <seealso cref="TopicReactionEvent" />
    [JsonConstructor]
    public CalendarEventReaction(
        [JsonProperty(Required = Required.Always)]
        Emote emote,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        Guid channelId,

        [JsonProperty(Required = Required.Always)]
        uint calendarEventId
    ) : base(emote, createdBy, channelId) =>
        EventId = calendarEventId;
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.AddEventReactionAsync(Guid, uint, uint)" />
    public override Task AddAsync() =>
        ParentClient.AddEventReactionAsync(ChannelId, EventId, Emote.Id);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveEventReactionAsync(Guid, uint, uint)" />
    public override Task RemoveAsync() =>
        ParentClient.RemoveEventReactionAsync(ChannelId, EventId, Emote.Id);
    #endregion
}

/// <summary>
/// Represents a <see cref="CalendarEventComment">calendar event comment</see> <see cref="Reaction">reaction</see>.
/// </summary>
/// <seealso cref="Reaction" />
/// <seealso cref="CalendarEventReaction" />
/// <seealso cref="MessageReaction" />
/// <seealso cref="TopicCommentReactionEvent" />
public class CalendarEventCommentReaction : Reaction
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="CalendarEvent">calendar event</see> where the <see cref="TopicComment">comment</see> is.
    /// </summary>
    /// <value>The identifier of the <see cref="CalendarEvent">calendar event</see> where the <see cref="TopicComment">comment</see> is</value>
    /// <seealso cref="CalendarEventCommentReaction" />
    /// <seealso cref="EventCommentId" />
    /// <seealso cref="Reaction" />
    /// <seealso cref="CalendarEvent" />
    /// <seealso cref="Reaction.ChannelId" />
    /// <seealso cref="Reaction.Id" />
    public uint EventId { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="CalendarEventComment">calendar event comment</see> that <see cref="User">user</see> reacted on.
    /// </summary>
    /// <value>Te identifier of the <see cref="CalendarEventComment">calendar event comment</see> that <see cref="User">user</see> reacted on</value>
    /// <seealso cref="CalendarEventCommentReaction" />
    /// <seealso cref="EventId" />
    /// <seealso cref="Reaction" />
    /// <seealso cref="Topic" />
    /// <seealso cref="Reaction.ChannelId" />
    /// <seealso cref="Reaction.Id" />
    public uint EventCommentId { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of <see cref="TopicReaction" /> from the specified JSON properties.
    /// </summary>
    /// <param name="emote">The <see cref="Emote">emote</see> with which the <see cref="Reaction.CreatedBy">user</see> reacted</param>
    /// <param name="createdBy">The identifier of the <see cref="User">user</see> that reacted</param>
    /// <param name="channelId">The identifier of the <see cref="ServerChannel">channel</see> where the <see cref="CalendarEvent">calendar event</see> is</param>
    /// <param name="calendarEventId">The identifier of the <see cref="CalendarEvent">calendar event</see> where the <see cref="TopicComment">comment</see> is</param>
    /// <param name="calendarEventCommentId">The identifier of the <see cref="CalendarEventComment">calendar event comment</see> that <see cref="Reaction.CreatedBy">user</see> reacted on</param>
    /// <returns>New <see cref="TopicReaction" /> JSON instance</returns>
    /// <seealso cref="TopicReaction" />
    /// <seealso cref="TopicReactionEvent" />
    [JsonConstructor]
    public CalendarEventCommentReaction(
        [JsonProperty(Required = Required.Always)]
        Emote emote,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        Guid channelId,

        [JsonProperty(Required = Required.Always)]
        uint calendarEventId,

        [JsonProperty(Required = Required.Always)]
        uint calendarEventCommentId
    ) : base(emote, createdBy, channelId) =>
        (EventId, EventCommentId) = (calendarEventId, calendarEventCommentId);
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.AddEventCommentReactionAsync(Guid, uint, uint, uint)" />
    public override Task AddAsync() =>
        ParentClient.AddEventCommentReactionAsync(ChannelId, EventId, EventCommentId, Emote.Id);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveTopicCommentReactionAsync(Guid, uint, uint, uint)" />
    public override Task RemoveAsync() =>
        ParentClient.RemoveEventCommentReactionAsync(ChannelId, EventId, EventCommentId, Emote.Id);
    #endregion
}
