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
/// Represents a <see cref="Announcement">announcement</see> <see cref="Reaction">reaction</see>.
/// </summary>
/// <seealso cref="Reaction" />
/// <seealso cref="AnnouncementCommentReaction" />
/// <seealso cref="AnnouncementReaction" />
/// <seealso cref="AnnouncementReactionEvent" />
public class AnnouncementReaction : Reaction
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="Announcement">announcement</see> that <see cref="User">user</see> reacted on.
    /// </summary>
    /// <value>The identifier of the <see cref="Announcement">announcement</see> that <see cref="User">user</see> reacted on</value>
    /// <seealso cref="AnnouncementReaction" />
    /// <seealso cref="Reaction" />
    /// <seealso cref="Announcement" />
    /// <seealso cref="Reaction.ChannelId" />
    /// <seealso cref="Reaction.Id" />
    public HashId AnnouncementId { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of <see cref="AnnouncementReaction" /> from the specified JSON properties.
    /// </summary>
    /// <param name="emote">The <see cref="Emote">emote</see> with which the <see cref="Reaction.CreatedBy">user</see> reacted</param>
    /// <param name="createdBy">The identifier of the <see cref="User">user</see> that reacted</param>
    /// <param name="channelId">The identifier of the <see cref="ServerChannel">channel</see> where the <see cref="Announcement">announcement</see> is</param>
    /// <param name="announcementId">The identifier of the <see cref="Announcement">announcement</see> that <see cref="Reaction.CreatedBy">user</see> reacted on</param>
    /// <returns>New <see cref="AnnouncementReaction" /> JSON instance</returns>
    /// <seealso cref="AnnouncementReaction" />
    /// <seealso cref="AnnouncementReactionEvent" />
    [JsonConstructor]
    public AnnouncementReaction(
        [JsonProperty(Required = Required.Always)]
        Emote emote,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        Guid channelId,

        [JsonProperty(Required = Required.Always)]
        HashId announcementId
    ) : base(emote, createdBy, channelId) =>
        AnnouncementId = announcementId;
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.AddAnnouncementReactionAsync(Guid, HashId, uint)" />
    public override Task AddAsync() =>
        ParentClient.AddAnnouncementReactionAsync(ChannelId, AnnouncementId, Emote.Id);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveAnnouncementReactionAsync(Guid, HashId, uint)" />
    public override Task RemoveAsync() =>
        ParentClient.RemoveAnnouncementReactionAsync(ChannelId, AnnouncementId, Emote.Id);
    #endregion
}

/// <summary>
/// Represents a <see cref="AnnouncementComment">announcement comment</see> <see cref="Reaction">reaction</see>.
/// </summary>
/// <seealso cref="Reaction" />
/// <seealso cref="AnnouncementReaction" />
/// <seealso cref="MessageReaction" />
/// <seealso cref="AnnouncementCommentReactionEvent" />
public class AnnouncementCommentReaction : Reaction
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="Announcement">announcement</see> where the <see cref="AnnouncementComment">comment</see> is.
    /// </summary>
    /// <value>The <see cref="Announcement">announcement</see> identifier of the <see cref="Reaction">reaction</see></value>
    /// <seealso cref="AnnouncementCommentReaction" />
    /// <seealso cref="AnnouncementCommentId" />
    /// <seealso cref="Reaction" />
    /// <seealso cref="Announcement" />
    /// <seealso cref="Reaction.ChannelId" />
    /// <seealso cref="Reaction.Id" />
    public HashId AnnouncementId { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="AnnouncementComment">announcement comment</see> that <see cref="User">user</see> reacted on.
    /// </summary>
    /// <value>The <see cref="AnnouncementComment">announcement comment</see> identifier of the <see cref="Reaction">reaction</see></value>
    /// <seealso cref="AnnouncementCommentReaction" />
    /// <seealso cref="AnnouncementId" />
    /// <seealso cref="Reaction" />
    /// <seealso cref="Announcement" />
    /// <seealso cref="Reaction.ChannelId" />
    /// <seealso cref="Reaction.Id" />
    public uint AnnouncementCommentId { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of <see cref="AnnouncementReaction" /> from the specified JSON properties.
    /// </summary>
    /// <param name="emote">The <see cref="Emote">emote</see> with which the <see cref="Reaction.CreatedBy">user</see> reacted</param>
    /// <param name="createdBy">The identifier of the <see cref="User">user</see> that reacted</param>
    /// <param name="channelId">The identifier of the <see cref="ServerChannel">channel</see> where the <see cref="Announcement">announcement</see> is</param>
    /// <param name="announcementId">The identifier of the <see cref="Announcement">announcement</see> where the <see cref="AnnouncementComment">comment</see> is</param>
    /// <param name="announcementCommentId">The identifier of the <see cref="AnnouncementComment">announcement comment</see> that <see cref="Reaction.CreatedBy">user</see> reacted on</param>
    /// <returns>New <see cref="AnnouncementReaction" /> JSON instance</returns>
    /// <seealso cref="AnnouncementReaction" />
    /// <seealso cref="AnnouncementReactionEvent" />
    [JsonConstructor]
    public AnnouncementCommentReaction(
        [JsonProperty(Required = Required.Always)]
        Emote emote,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        Guid channelId,

        [JsonProperty(Required = Required.Always)]
        HashId announcementId,

        [JsonProperty(Required = Required.Always)]
        uint announcementCommentId
    ) : base(emote, createdBy, channelId) =>
        (AnnouncementId, AnnouncementCommentId) = (announcementId, announcementCommentId);
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.AddAnnouncementCommentReactionAsync(Guid, HashId, uint, uint)" />
    public override Task AddAsync() =>
        ParentClient.AddAnnouncementCommentReactionAsync(ChannelId, AnnouncementId, AnnouncementCommentId, Emote.Id);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveAnnouncementCommentReactionAsync(Guid, HashId, uint, uint)" />
    public override Task RemoveAsync() =>
        ParentClient.RemoveAnnouncementCommentReactionAsync(ChannelId, AnnouncementId, AnnouncementCommentId, Emote.Id);
    #endregion
}
