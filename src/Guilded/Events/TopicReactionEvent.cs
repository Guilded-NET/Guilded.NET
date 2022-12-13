using Guilded.Base;
using Guilded.Content;
using Guilded.Servers;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when someone adds or removes a <see cref="Reaction">reaction</see> on/from a <see cref="Topic">forum topic</see>.
/// </summary>
/// <seealso cref="ReactionEvent{T}" />
/// <seealso cref="TopicReactionEvent" />
/// <seealso cref="Reaction" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="MessageDeletedEvent" />
public class TopicReactionEvent : ReactionEvent<TopicReaction>
{
    #region Properties
    /// <inheritdoc cref="TopicReaction.TopicId" />
    public uint TopicId => Reaction.TopicId;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="TopicReactionEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="reaction">The received <see cref="Reaction">reaction</see> from the event</param>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="TopicReactionEvent">event</see> occurred</param>
    /// <returns>New <see cref="TopicReactionEvent" /> JSON instance</returns>
    /// <seealso cref="TopicReactionEvent" />
    [JsonConstructor]
    public TopicReactionEvent(
        [JsonProperty(Required = Required.Always)]
        TopicReaction reaction,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? serverId
    ) : base(reaction, serverId) { }
    #endregion
}

/// <summary>
/// Represents an event that occurs when someone adds or removes a <see cref="Reaction">reaction</see> on/from a <see cref="TopicComment">forum topic comment</see>.
/// </summary>
/// <seealso cref="ReactionEvent{T}" />
/// <seealso cref="TopicReactionEvent" />
/// <seealso cref="Reaction" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="MessageDeletedEvent" />
public class TopicCommentReactionEvent : ReactionEvent<TopicCommentReaction>
{
    #region Properties
    /// <inheritdoc cref="TopicCommentReaction.TopicId" />
    public uint TopicId => Reaction.TopicId;

    /// <inheritdoc cref="TopicCommentReaction.TopicCommentId" />
    public uint TopicCommentId => Reaction.TopicCommentId;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="TopicReactionEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="reaction">The received <see cref="Reaction">reaction</see> from the event</param>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="TopicCommentReactionEvent">event</see> occurred</param>
    /// <returns>New <see cref="TopicReactionEvent" /> JSON instance</returns>
    /// <seealso cref="TopicReactionEvent" />
    [JsonConstructor]
    public TopicCommentReactionEvent(
        [JsonProperty(Required = Required.Always)]
        TopicCommentReaction reaction,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? serverId
    ) : base(reaction, serverId) { }
    #endregion
}