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
/// Represents a <see cref="Topic">forum topic</see> <see cref="Reaction">reaction</see>.
/// </summary>
/// <seealso cref="Reaction" />
/// <seealso cref="TopicCommentReaction" />
/// <seealso cref="MessageReaction" />
/// <seealso cref="TopicReactionEvent" />
public class TopicReaction : Reaction
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="Topic">forum topic</see> that <see cref="User">user</see> reacted on.
    /// </summary>
    /// <value>The identifier of the <see cref="Topic">forum topic</see> that <see cref="User">user</see> reacted on</value>
    /// <seealso cref="TopicReaction" />
    /// <seealso cref="Reaction" />
    /// <seealso cref="Topic" />
    /// <seealso cref="Reaction.ChannelId" />
    /// <seealso cref="Reaction.Id" />
    public uint TopicId { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of <see cref="TopicReaction" /> from the specified JSON properties.
    /// </summary>
    /// <param name="emote">The <see cref="Emote">emote</see> with which the <see cref="Reaction.CreatedBy">user</see> reacted</param>
    /// <param name="createdBy">The identifier of the <see cref="User">user</see> that reacted</param>
    /// <param name="channelId">The identifier of the <see cref="ServerChannel">channel</see> where the <see cref="Topic">forum topic</see> is</param>
    /// <param name="forumTopicId">The identifier of the <see cref="Topic">forum topic</see> that <see cref="Reaction.CreatedBy">user</see> reacted on</param>
    /// <returns>New <see cref="TopicReaction" /> JSON instance</returns>
    /// <seealso cref="TopicReaction" />
    /// <seealso cref="TopicReactionEvent" />
    [JsonConstructor]
    public TopicReaction(
        [JsonProperty(Required = Required.Always)]
        Emote emote,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        Guid channelId,

        [JsonProperty(Required = Required.Always)]
        uint forumTopicId
    ) : base(emote, createdBy, channelId) =>
        TopicId = forumTopicId;
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.AddReactionAsync(Guid, uint, uint)" />
    public override Task AddAsync() =>
        ParentClient.AddTopicReactionAsync(ChannelId, TopicId, Emote.Id);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveReactionAsync(Guid, uint, uint)" />
    public override Task RemoveAsync() =>
        ParentClient.RemoveTopicReactionAsync(ChannelId, TopicId, Emote.Id);
    #endregion
}

/// <summary>
/// Represents a <see cref="TopicComment">forum topic comment</see> <see cref="Reaction">reaction</see>.
/// </summary>
/// <seealso cref="Reaction" />
/// <seealso cref="TopicReaction" />
/// <seealso cref="MessageReaction" />
/// <seealso cref="TopicCommentReactionEvent" />
public class TopicCommentReaction : Reaction
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="Topic">forum topic</see> where the <see cref="TopicComment">comment</see> is.
    /// </summary>
    /// <value>The <see cref="Topic">forum topic</see> identifier of the <see cref="Reaction">reaction</see></value>
    /// <seealso cref="TopicCommentReaction" />
    /// <seealso cref="TopicCommentId" />
    /// <seealso cref="Reaction" />
    /// <seealso cref="Topic" />
    /// <seealso cref="Reaction.ChannelId" />
    /// <seealso cref="Reaction.Id" />
    public uint TopicId { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="TopicComment">forum topic comment</see> that <see cref="User">user</see> reacted on.
    /// </summary>
    /// <value>The <see cref="TopicComment">forum topic comment</see> identifier of the <see cref="Reaction">reaction</see></value>
    /// <seealso cref="TopicCommentReaction" />
    /// <seealso cref="TopicId" />
    /// <seealso cref="Reaction" />
    /// <seealso cref="Topic" />
    /// <seealso cref="Reaction.ChannelId" />
    /// <seealso cref="Reaction.Id" />
    public uint TopicCommentId { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of <see cref="TopicReaction" /> from the specified JSON properties.
    /// </summary>
    /// <param name="emote">The <see cref="Emote">emote</see> with which the <see cref="Reaction.CreatedBy">user</see> reacted</param>
    /// <param name="createdBy">The identifier of the <see cref="User">user</see> that reacted</param>
    /// <param name="channelId">The identifier of the <see cref="ServerChannel">channel</see> where the <see cref="Topic">forum topic</see> is</param>
    /// <param name="forumTopicId">The identifier of the <see cref="Topic">forum topic</see> where the <see cref="TopicComment">comment</see> is</param>
    /// <param name="forumTopicCommentId">The identifier of the <see cref="TopicComment">forum topic comment</see> that <see cref="Reaction.CreatedBy">user</see> reacted on</param>
    /// <returns>New <see cref="TopicReaction" /> JSON instance</returns>
    /// <seealso cref="TopicReaction" />
    /// <seealso cref="TopicReactionEvent" />
    [JsonConstructor]
    public TopicCommentReaction(
        [JsonProperty(Required = Required.Always)]
        Emote emote,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        Guid channelId,

        [JsonProperty(Required = Required.Always)]
        uint forumTopicId,

        [JsonProperty(Required = Required.Always)]
        uint forumTopicCommentId
    ) : base(emote, createdBy, channelId) =>
        (TopicId, TopicCommentId) = (forumTopicId, forumTopicCommentId);
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.AddTopicCommentReactionAsync(Guid, uint, uint, uint)" />
    public override Task AddAsync() =>
        ParentClient.AddTopicCommentReactionAsync(ChannelId, TopicId, TopicCommentId, Emote.Id);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveTopicCommentReactionAsync(Guid, uint, uint, uint)" />
    public override Task RemoveAsync() =>
        ParentClient.RemoveTopicCommentReactionAsync(ChannelId, TopicId, TopicCommentId, Emote.Id);
    #endregion
}
