using System;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Client;
using Guilded.Connection;
using Guilded.Content;
using Guilded.Servers;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when someone adds or removes a <see cref="Content.Reaction">reaction</see>.
/// </summary>
/// <seealso cref="MessageReactionEvent" />
/// <seealso cref="TopicReactionEvent" />
/// <seealso cref="Content.Reaction" />
public class ReactionEvent<T> : IModelHasId<uint>, IGlobalContent, IChannelBased where T : Reaction
{
    #region Properties
    /// <summary>
    /// Gets the received <see cref="Content.Reaction">reaction</see> from the <see cref="GuildedSocketMessage">event</see>.
    /// </summary>
    /// <value>The <see cref="Content.Reaction">reaction</see> from the <see cref="GuildedSocketMessage">event</see></value>
    /// <seealso cref="ReactionEvent{T}" />
    /// <seealso cref="Emote" />
    public T Reaction { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="Server">server</see> where the <see cref="GuildedSocketMessage">event</see> occurred.
    /// </summary>
    /// <value>The <see cref="Server">server</see> identifier of the <see cref="Reaction">reaction</see></value>
    /// <seealso cref="ReactionEvent{T}" />
    /// <seealso cref="Reaction" />
    public HashId? ServerId { get; set; }
    #endregion

    #region Properties Additional
    /// <inheritdoc cref="Reaction.Emote" />
    public Emote Emote => Reaction.Emote;

    /// <inheritdoc cref="Reaction.CreatedBy" />
    public HashId CreatedBy => Reaction.CreatedBy;

    /// <inheritdoc cref="Reaction.ChannelId" />
    public Guid ChannelId => Reaction.ChannelId;

    /// <inheritdoc cref="Emote.Id" />
    public uint Id => Emote.Id;

    /// <inheritdoc cref="Emote.Name" />
    public string Name => Emote.Name;

    /// <inheritdoc cref="IHasParentClient.ParentClient" />
    public AbstractGuildedClient ParentClient => Reaction.ParentClient;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ReactionEvent{T}" /> from the specified JSON properties.
    /// </summary>
    /// <param name="reaction">The received <see cref="Content.Reaction">reaction</see> from the event</param>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the event occurred</param>
    /// <returns>New <see cref="MessageReactionEvent" /> JSON instance</returns>
    /// <seealso cref="ReactionEvent{T}" />
    /// <seealso cref="MessageReactionEvent" />
    /// <seealso cref="TopicReactionEvent" />
    [JsonConstructor]
    public ReactionEvent(
        [JsonProperty(Required = Required.Always)]
        T reaction,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? serverId
    ) =>
        (Reaction, ServerId) = (reaction, serverId);
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.AddReactionAsync(Guid, Guid, uint)" />
    public Task AddAsync() =>
        Reaction.AddAsync();

    /// <inheritdoc cref="AbstractGuildedClient.RemoveReactionAsync(Guid, Guid, uint)" />
    public Task RemoveAsync() =>
        Reaction.RemoveAsync();
    #endregion
}