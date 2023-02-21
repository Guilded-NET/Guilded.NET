using Guilded.Base;
using Guilded.Content;
using Guilded.Servers;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when someone adds or removes a <see cref="Reaction">reaction</see> on/from a <see cref="Topic">forum topic</see>.
/// </summary>
/// <seealso cref="ReactionEvent{T}" />
/// <seealso cref="DocCommentReactionEvent" />
/// <seealso cref="Reaction" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="MessageDeletedEvent" />
public class DocReactionEvent : ReactionEvent<DocReaction>
{
    #region Properties
    /// <inheritdoc cref="DocReaction.DocId" />
    public uint DocId => Reaction.DocId;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="DocReactionEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="reaction">The received <see cref="Reaction">reaction</see> from the event</param>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="DocReactionEvent">event</see> occurred</param>
    /// <returns>New <see cref="DocReactionEvent" /> JSON instance</returns>
    /// <seealso cref="DocReactionEvent" />
    [JsonConstructor]
    public DocReactionEvent(
        [JsonProperty(Required = Required.Always)]
        DocReaction reaction,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? serverId
    ) : base(reaction, serverId) { }
    #endregion
}

/// <summary>
/// Represents an event that occurs when someone adds or removes a <see cref="Reaction">reaction</see> on/from a <see cref="TopicComment">forum topic comment</see>.
/// </summary>
/// <seealso cref="ReactionEvent{T}" />
/// <seealso cref="DocReactionEvent" />
/// <seealso cref="Reaction" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="MessageDeletedEvent" />
public class DocCommentReactionEvent : ReactionEvent<DocCommentReaction>
{
    #region Properties
    /// <inheritdoc cref="DocCommentReaction.DocId" />
    public uint DocId => Reaction.DocId;

    /// <inheritdoc cref="DocCommentReaction.DocCommentId" />
    public uint DocCommentId => Reaction.DocCommentId;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="DocReactionEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="reaction">The received <see cref="Reaction">reaction</see> from the event</param>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="DocCommentReactionEvent">event</see> occurred</param>
    /// <returns>New <see cref="DocReactionEvent" /> JSON instance</returns>
    /// <seealso cref="DocReactionEvent" />
    [JsonConstructor]
    public DocCommentReactionEvent(
        [JsonProperty(Required = Required.Always)]
        DocCommentReaction reaction,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? serverId
    ) : base(reaction, serverId) { }
    #endregion
}