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
/// Represents a <see cref="Doc">document</see> <see cref="Reaction">reaction</see>.
/// </summary>
/// <seealso cref="Reaction" />
/// <seealso cref="DocCommentReaction" />
/// <seealso cref="MessageReaction" />
/// <seealso cref="DocReactionEvent" />
public class DocReaction : Reaction
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="Doc">document</see> that <see cref="User">user</see> reacted on.
    /// </summary>
    /// <value>The identifier of the <see cref="Doc">document</see> that <see cref="User">user</see> reacted on</value>
    /// <seealso cref="DocReaction" />
    /// <seealso cref="Reaction" />
    /// <seealso cref="Topic" />
    /// <seealso cref="Reaction.ChannelId" />
    /// <seealso cref="Reaction.Id" />
    public uint DocId { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of <see cref="DocReaction" /> from the specified JSON properties.
    /// </summary>
    /// <param name="emote">The <see cref="Emote">emote</see> with which the <see cref="Reaction.CreatedBy">user</see> reacted</param>
    /// <param name="createdBy">The identifier of the <see cref="User">user</see> that reacted</param>
    /// <param name="channelId">The identifier of the <see cref="ServerChannel">channel</see> where the <see cref="Doc">document</see> is</param>
    /// <param name="docId">The identifier of the <see cref="Doc">document</see> that <see cref="Reaction.CreatedBy">user</see> reacted on</param>
    /// <returns>New <see cref="DocReaction" /> JSON instance</returns>
    /// <seealso cref="DocReaction" />
    /// <seealso cref="DocReactionEvent" />
    [JsonConstructor]
    public DocReaction(
        [JsonProperty(Required = Required.Always)]
        Emote emote,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        Guid channelId,

        [JsonProperty(Required = Required.Always)]
        uint docId
    ) : base(emote, createdBy, channelId) =>
        DocId = docId;
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.AddReactionAsync(Guid, uint, uint)" />
    public override Task AddAsync() =>
        ParentClient.AddDocReactionAsync(ChannelId, DocId, Emote.Id);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveReactionAsync(Guid, uint, uint)" />
    public override Task RemoveAsync() =>
        ParentClient.RemoveDocReactionAsync(ChannelId, DocId, Emote.Id);
    #endregion
}

/// <summary>
/// Represents a <see cref="DocComment">document comment</see> <see cref="Reaction">reaction</see>.
/// </summary>
/// <seealso cref="Reaction" />
/// <seealso cref="DocReaction" />
/// <seealso cref="MessageReaction" />
/// <seealso cref="DocCommentReactionEvent" />
public class DocCommentReaction : Reaction
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="Doc">document</see> where the <see cref="DocComment">comment</see> is.
    /// </summary>
    /// <value>The <see cref="Doc">document</see> identifier of the <see cref="Reaction">reaction</see></value>
    /// <seealso cref="DocCommentReaction" />
    /// <seealso cref="DocCommentId" />
    /// <seealso cref="Reaction" />
    /// <seealso cref="Doc" />
    /// <seealso cref="Reaction.ChannelId" />
    /// <seealso cref="Reaction.Id" />
    public uint DocId { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="DocComment">document comment</see> that <see cref="User">user</see> reacted on.
    /// </summary>
    /// <value>The <see cref="DocComment">document comment</see> identifier of the <see cref="Reaction">reaction</see></value>
    /// <seealso cref="DocCommentReaction" />
    /// <seealso cref="DocId" />
    /// <seealso cref="Reaction" />
    /// <seealso cref="Doc" />
    /// <seealso cref="Reaction.ChannelId" />
    /// <seealso cref="Reaction.Id" />
    public uint DocCommentId { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of <see cref="DocReaction" /> from the specified JSON properties.
    /// </summary>
    /// <param name="emote">The <see cref="Emote">emote</see> with which the <see cref="Reaction.CreatedBy">user</see> reacted</param>
    /// <param name="createdBy">The identifier of the <see cref="User">user</see> that reacted</param>
    /// <param name="channelId">The identifier of the <see cref="ServerChannel">channel</see> where the <see cref="Doc">document</see> is</param>
    /// <param name="docId">The identifier of the <see cref="Doc">document</see> where the <see cref="DocComment">comment</see> is</param>
    /// <param name="docCommentId">The identifier of the <see cref="DocComment">document comment</see> that <see cref="Reaction.CreatedBy">user</see> reacted on</param>
    /// <returns>New <see cref="DocReaction" /> JSON instance</returns>
    /// <seealso cref="DocReaction" />
    /// <seealso cref="DocReactionEvent" />
    [JsonConstructor]
    public DocCommentReaction(
        [JsonProperty(Required = Required.Always)]
        Emote emote,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        Guid channelId,

        [JsonProperty(Required = Required.Always)]
        uint docId,

        [JsonProperty(Required = Required.Always)]
        uint docCommentId
    ) : base(emote, createdBy, channelId) =>
        (DocId, DocCommentId) = (docId, docCommentId);
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.AddDocCommentReactionAsync(Guid, uint, uint, uint)" />
    public override Task AddAsync() =>
        ParentClient.AddDocCommentReactionAsync(ChannelId, DocId, DocCommentId, Emote.Id);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveDocCommentReactionAsync(Guid, uint, uint, uint)" />
    public override Task RemoveAsync() =>
        ParentClient.RemoveDocCommentReactionAsync(ChannelId, DocId, DocCommentId, Emote.Id);
    #endregion
}
