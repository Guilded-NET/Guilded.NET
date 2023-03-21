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
/// Represents a <see cref="ChannelContent{T, S}">content</see> reaction.
/// </summary>
/// <seealso cref="Message" />
/// <seealso cref="Doc" />
/// <seealso cref="Topic" />
public abstract class Reaction : ContentModel, IModelHasId<uint>, IChannelBased
{
    #region Properties
    /// <summary>
    /// Gets the <see cref="Content.Emote">emote</see> with which the <see cref="CreatedBy">user</see> reacted.
    /// </summary>
    /// <value>The <see cref="Content.Emote">emote</see> of the <see cref="Reaction">reaction</see></value>
    /// <seealso cref="Reaction" />
    /// <seealso cref="Content.Emote" />
    /// <seealso cref="Id" />
    public Emote Emote { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="User">user</see> that reacted.
    /// </summary>
    /// <remarks>
    /// <para>If a <see cref="Webhook">webhook</see> created this reaction, the value of this property will be <c>Ann6LewA</c>.</para>
    /// </remarks>
    /// <value>The <see cref="User">creator</see> identifier of the <see cref="Reaction">reaction</see></value>
    /// <seealso cref="Reaction" />
    /// <seealso cref="Id" />
    /// <seealso cref="ChannelId" />
    public HashId CreatedBy { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="ServerChannel">channel</see> where the <see cref="Message">message</see> is.
    /// </summary>
    /// <value>The <see cref="ServerChannel">channel</see> identifier of the <see cref="Reaction">reaction</see></value>
    /// <seealso cref="Reaction" />
    /// <seealso cref="Id" />
    /// <seealso cref="CreatedBy" />
    public Guid ChannelId { get; }

    /// <inheritdoc cref="Emote.Id" />
    public uint Id => Emote.Id;

    /// <inheritdoc cref="Emote.Name" />
    public string Name => Emote.Name;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="Reaction" /> from the specified JSON properties.
    /// </summary>
    /// <param name="emote">The <see cref="Content.Emote">emote</see> with which the <see cref="CreatedBy">user</see> reacted</param>
    /// <param name="createdBy">The identifier of the <see cref="User">user</see> that reacted</param>
    /// <param name="channelId">The identifier of the <see cref="ServerChannel">channel</see> where the <see cref="ChannelContent{TId, TServer}">channel content</see> are</param>
    /// <returns>New <see cref="Reaction" /> JSON instance</returns>
    /// <seealso cref="Reaction" />
    /// <seealso cref="MessageReaction" />
    /// <seealso cref="TopicReaction" />
    protected Reaction(Emote emote, HashId createdBy, Guid channelId) =>
        (Emote, CreatedBy, ChannelId) = (emote, createdBy, channelId);
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.AddReactionAsync(Guid, Guid, uint)" />
    public abstract Task AddAsync();

    /// <inheritdoc cref="AbstractGuildedClient.RemoveReactionAsync(Guid, Guid, uint)" />
    public abstract Task RemoveAsync();
    #endregion
}

/// <summary>
/// Represents a <see cref="Message">message</see> <see cref="Reaction">reaction</see>.
/// </summary>
/// <seealso cref="Reaction" />
/// <seealso cref="TopicReaction" />
/// <seealso cref="TopicCommentReaction" />
/// <seealso cref="MessageReactionEvent" />
public class MessageReaction : Reaction
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="Message">message</see> that <see cref="User">user</see> reacted on.
    /// </summary>
    /// <value>The <see cref="Message">message</see> identifier of the <see cref="Reaction">reaction</see></value>
    /// <seealso cref="MessageReaction" />
    /// <seealso cref="Reaction" />
    /// <seealso cref="Message" />
    /// <seealso cref="Reaction.ChannelId" />
    /// <seealso cref="Reaction.Id" />
    public Guid MessageId { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of <see cref="MessageReaction" /> from the specified JSON properties.
    /// </summary>
    /// <param name="emote">The <see cref="Emote">emote</see> with which the <see cref="Reaction.CreatedBy">user</see> reacted</param>
    /// <param name="createdBy">The identifier of the <see cref="User">user</see> that reacted</param>
    /// <param name="channelId">The identifier of the <see cref="ServerChannel">channel</see> where the <see cref="Message">message</see> is</param>
    /// <param name="messageId">The identifier of the <see cref="Message">message</see> that <see cref="Reaction.CreatedBy">user</see> reacted on</param>
    /// <returns>New <see cref="MessageReaction" /> JSON instance</returns>
    /// <seealso cref="MessageReaction" />
    /// <seealso cref="MessageReactionEvent" />
    [JsonConstructor]
    public MessageReaction(
        [JsonProperty(Required = Required.Always)]
        Emote emote,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        Guid channelId,

        [JsonProperty(Required = Required.Always)]
        Guid messageId
    ) : base(emote, createdBy, channelId) =>
        MessageId = messageId;
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.AddReactionAsync(Guid, Guid, uint)" />
    public override Task AddAsync() =>
        ParentClient.AddReactionAsync(ChannelId, MessageId, Emote.Id);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveReactionAsync(Guid, Guid, uint)" />
    public override Task RemoveAsync() =>
        ParentClient.RemoveReactionAsync(ChannelId, MessageId, Emote.Id);
    #endregion
}
