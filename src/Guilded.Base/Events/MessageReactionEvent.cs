using System;
using System.Threading.Tasks;
using Guilded.Base.Client;
using Guilded.Base.Content;
using Guilded.Base.Servers;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Events;

/// <summary>
/// Represents an event that occurs when someone adds or removes <see cref="Content.Reaction">a reaction</see>.
/// </summary>
/// <seealso cref="MessageEvent" />
/// <seealso cref="MessageDeletedEvent" />
/// <seealso cref="Content.Reaction" />
public class MessageReactionEvent
{
    #region Properties
    /// <summary>
    /// Gets <see cref="Content.Reaction">the received reaction</see> from the event.
    /// </summary>
    /// <value><see cref="Content.Reaction" /></value>
    /// <seealso cref="MessageReactionEvent" />
    /// <seealso cref="Emote" />
    public EventReaction Reaction { get; }

    /// <summary>
    /// Gets the identifier of <see cref="Server">the server</see> where the event occurred.
    /// </summary>
    /// <value>Server ID?</value>
    /// <seealso cref="MessageReactionEvent" />
    /// <seealso cref="Reaction" />
    public HashId? ServerId { get; set; }

    /// <inheritdoc cref="EventReaction.Emote" />
    public Emote Emote => Reaction.Emote;

    /// <inheritdoc cref="EventReaction.CreatedBy" />
    public HashId CreatedBy => Reaction.CreatedBy;

    /// <inheritdoc cref="EventReaction.ChannelId" />
    public Guid ChannelId => Reaction.ChannelId;

    /// <inheritdoc cref="EventReaction.MessageId" />
    public Guid MessageId => Reaction.MessageId;

    /// <inheritdoc cref="Emote.Id" />
    public uint Id => Emote.Id;

    /// <inheritdoc cref="Emote.Name" />
    public string Name => Emote.Name;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="MessageReactionEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="reaction"><see cref="Content.Reaction">The received reaction</see> from the event</param>
    /// <param name="serverId">The identifier of <see cref="Server">the server</see> where the event occurred</param>
    /// <returns>New <see cref="MessageReactionEvent" /> JSON instance</returns>
    /// <seealso cref="MessageReactionEvent" />
    [JsonConstructor]
    public MessageReactionEvent(
        [JsonProperty(Required = Required.Always)]
        EventReaction reaction,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? serverId
    ) =>
        (Reaction, ServerId) = (reaction, serverId);
    #endregion

    #region Methods
    /// <inheritdoc cref="BaseGuildedClient.AddReactionAsync(Guid, Guid, uint)" />
    public Task AddAsync() =>
        Reaction.AddAsync();

    /// <inheritdoc cref="BaseGuildedClient.RemoveReactionAsync(Guid, Guid, uint)" />
    public Task RemoveAsync() =>
        Reaction.RemoveAsync();
    #endregion

    /// <summary>
    /// Represents <see cref="Content.Reaction">the reaction</see> that has been added.
    /// </summary>
    /// <seealso cref="Content.Reaction" />
    /// <seealso cref="MessageReactionEvent" />
    public class EventReaction : ContentModel
    {
        #region Properties
        /// <summary>
        /// Gets <see cref="Content.Emote">the emote</see> with which <see cref="CreatedBy">the user</see> reacted.
        /// </summary>
        /// <value><see cref="Content.Emote" /></value>
        public Emote Emote { get; }

        /// <inheritdoc cref="Reaction.CreatedBy" />
        public HashId CreatedBy { get; }

        /// <summary>
        /// Gets the identifier of <see cref="ServerChannel">the channel</see> where <see cref="Message">the message</see> is.
        /// </summary>
        /// <value><see cref="ServerChannel.Id">Channel ID</see></value>
        public Guid ChannelId { get; }

        /// <summary>
        /// Gets the identifier of <see cref="Message">the message</see> that <see cref="CreatedBy">user</see> reacted on.
        /// </summary>
        /// <value><see cref="ChannelContent{TId, TServer}.Id">Message ID</see></value>
        public Guid MessageId { get; }

        /// <inheritdoc cref="Emote.Id" />
        public uint Id => Emote.Id;

        /// <inheritdoc cref="Emote.Name" />
        public string Name => Emote.Name;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="EventReaction" /> from the specified JSON properties.
        /// </summary>
        /// <param name="emote"><see cref="Content.Emote">The emote</see> with which <see cref="CreatedBy">the user</see> reacted</param>
        /// <param name="createdBy">The identifier of <see cref="User">the user</see> that reacted</param>
        /// <param name="channelId">The identifier of <see cref="ServerChannel">the channel</see> where <see cref="Message">the message</see> is</param>
        /// <param name="messageId">The identifier of <see cref="Message">the message</see> that <see cref="CreatedBy">user</see> reacted on</param>
        /// <returns>New <see cref="EventReaction" /> JSON instance</returns>
        /// <seealso cref="EventReaction" />
        /// <seealso cref="MessageReactionEvent" />
        [JsonConstructor]
        public EventReaction(
            [JsonProperty(Required = Required.Always)]
            Emote emote,

            [JsonProperty(Required = Required.Always)]
            HashId createdBy,

            [JsonProperty(Required = Required.Always)]
            Guid channelId,

            [JsonProperty(Required = Required.Always)]
            Guid messageId
        ) =>
            (Emote, CreatedBy, ChannelId, MessageId) = (emote, createdBy, channelId, messageId);
        #endregion

        #region Methods
        /// <inheritdoc cref="BaseGuildedClient.AddReactionAsync(Guid, Guid, uint)" />
        public Task AddAsync() =>
            ParentClient.AddReactionAsync(ChannelId, MessageId, Emote.Id);

        /// <inheritdoc cref="BaseGuildedClient.RemoveReactionAsync(Guid, Guid, uint)" />
        public Task RemoveAsync() =>
            ParentClient.RemoveReactionAsync(ChannelId, MessageId, Emote.Id);
        #endregion
    }
}