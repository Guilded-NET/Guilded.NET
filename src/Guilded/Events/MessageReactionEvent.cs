using System;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Client;
using Guilded.Content;
using Guilded.Servers;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when someone adds or removes a <see cref="Reaction">reaction</see>.
/// </summary>
/// <seealso cref="ReactionEvent{T}" />
/// <seealso cref="TopicReactionEvent" />
/// <seealso cref="Reaction" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="MessageDeletedEvent" />
public class MessageReactionEvent : ReactionEvent<MessageReaction>
{
    #region Properties
    /// <inheritdoc cref="MessageReaction.MessageId" />
    public Guid MessageId => Reaction.MessageId;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="MessageReactionEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="reaction">The received <see cref="Reaction">reaction</see> from the event</param>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the event occurred</param>
    /// <returns>New <see cref="MessageReactionEvent" /> JSON instance</returns>
    /// <seealso cref="MessageReactionEvent" />
    [JsonConstructor]
    public MessageReactionEvent(
        [JsonProperty(Required = Required.Always)]
        MessageReaction reaction,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? serverId
    ) : base(reaction, serverId) { }
    #endregion
}