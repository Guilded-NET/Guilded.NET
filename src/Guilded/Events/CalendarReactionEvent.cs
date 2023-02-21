using Guilded.Base;
using Guilded.Content;
using Guilded.Servers;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when someone adds or removes a <see cref="Reaction">reaction</see> on/from a <see cref="Topic">forum topic</see>.
/// </summary>
/// <seealso cref="ReactionEvent{T}" />
/// <seealso cref="CalendarEventCommentReactionEvent" />
/// <seealso cref="Reaction" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="MessageDeletedEvent" />
public class CalendarEventReactionEvent : ReactionEvent<CalendarEventReaction>
{
    #region Properties
    /// <inheritdoc cref="CalendarEventReaction.EventId" />
    public uint EventId => Reaction.EventId;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CalendarEventReactionEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="reaction">The received <see cref="Reaction">reaction</see> from the event</param>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="CalendarEventReactionEvent">event</see> occurred</param>
    /// <returns>New <see cref="CalendarEventReactionEvent" /> JSON instance</returns>
    /// <seealso cref="CalendarEventReactionEvent" />
    [JsonConstructor]
    public CalendarEventReactionEvent(
        [JsonProperty(Required = Required.Always)]
        CalendarEventReaction reaction,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? serverId
    ) : base(reaction, serverId) { }
    #endregion
}

/// <summary>
/// Represents an event that occurs when someone adds or removes a <see cref="Reaction">reaction</see> on/from a <see cref="CalendarEventComment">calendar event comment</see>.
/// </summary>
/// <seealso cref="ReactionEvent{T}" />
/// <seealso cref="CalendarEventReactionEvent" />
/// <seealso cref="Reaction" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="MessageDeletedEvent" />
public class CalendarEventCommentReactionEvent : ReactionEvent<CalendarEventCommentReaction>
{
    #region Properties
    /// <inheritdoc cref="CalendarEventCommentReaction.EventId" />
    public uint TopicId => Reaction.EventId;

    /// <inheritdoc cref="CalendarEventCommentReaction.EventCommentId" />
    public uint EventCommentId => Reaction.EventCommentId;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CalendarEventCommentReactionEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="reaction">The received <see cref="Reaction">reaction</see> from the event</param>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="TopicCommentReactionEvent">event</see> occurred</param>
    /// <returns>New <see cref="CalendarEventCommentReactionEvent" /> JSON instance</returns>
    /// <seealso cref="CalendarEventCommentReactionEvent" />
    [JsonConstructor]
    public CalendarEventCommentReactionEvent(
        [JsonProperty(Required = Required.Always)]
        CalendarEventCommentReaction reaction,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? serverId
    ) : base(reaction, serverId) { }
    #endregion
}