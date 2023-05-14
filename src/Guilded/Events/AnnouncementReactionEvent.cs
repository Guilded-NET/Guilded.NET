using Guilded.Base;
using Guilded.Content;
using Guilded.Servers;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when someone adds or removes a <see cref="Reaction">reaction</see> on/from a <see cref="Topic">forum topic</see>.
/// </summary>
/// <seealso cref="ReactionEvent{T}" />
/// <seealso cref="AnnouncementCommentReactionEvent" />
/// <seealso cref="Reaction" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="MessageDeletedEvent" />
public class AnnouncementReactionEvent : ReactionEvent<AnnouncementReaction>
{
    #region Properties
    /// <inheritdoc cref="AnnouncementReaction.AnnouncementId" />
    public HashId AnnouncementId => Reaction.AnnouncementId;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="AnnouncementReactionEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="reaction">The received <see cref="Reaction">reaction</see> from the event</param>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="AnnouncementReactionEvent">event</see> occurred</param>
    /// <returns>New <see cref="AnnouncementReactionEvent" /> JSON instance</returns>
    /// <seealso cref="AnnouncementReactionEvent" />
    [JsonConstructor]
    public AnnouncementReactionEvent(
        [JsonProperty(Required = Required.Always)]
        AnnouncementReaction reaction,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? serverId
    ) : base(reaction, serverId) { }
    #endregion
}

/// <summary>
/// Represents an event that occurs when someone adds or removes a <see cref="Reaction">reaction</see> on/from a <see cref="AnnouncementComment">announcement comment</see>.
/// </summary>
/// <seealso cref="ReactionEvent{T}" />
/// <seealso cref="AnnouncementReactionEvent" />
/// <seealso cref="Reaction" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="MessageDeletedEvent" />
public class AnnouncementCommentReactionEvent : ReactionEvent<AnnouncementCommentReaction>
{
    #region Properties
    /// <inheritdoc cref="AnnouncementCommentReaction.AnnouncementId" />
    public HashId AnnouncementId => Reaction.AnnouncementId;

    /// <inheritdoc cref="AnnouncementCommentReaction.AnnouncementCommentId" />
    public uint AnnouncementCommentId => Reaction.AnnouncementCommentId;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="AnnouncementReactionEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="reaction">The received <see cref="Reaction">reaction</see> from the event</param>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="AnnouncementCommentReactionEvent">event</see> occurred</param>
    /// <returns>New <see cref="AnnouncementReactionEvent" /> JSON instance</returns>
    /// <seealso cref="AnnouncementReactionEvent" />
    [JsonConstructor]
    public AnnouncementCommentReactionEvent(
        [JsonProperty(Required = Required.Always)]
        AnnouncementCommentReaction reaction,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? serverId
    ) : base(reaction, serverId) { }
    #endregion
}