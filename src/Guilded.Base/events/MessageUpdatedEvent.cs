using System;
using Guilded.Base.Content;
using Newtonsoft.Json;

namespace Guilded.Base.Events
{
    /// <summary>
    /// An event that occurs once someone edits a message.
    /// </summary>
    /// <remarks>
    /// <para>An event of the name <c>ChatMessageUpdated</c> and opcode <c>0</c> that occurs once someone updates/edits a message in the chat.</para>
    /// <para>When receiving this event, <see cref="Message.UpdatedAt"/> will always hold a value.</para>
    /// </remarks>
    /// <seealso cref="MessageCreatedEvent"/>
    /// <seealso cref="MessageDeletedEvent"/>
    /// <seealso cref="Message"/>
    public class MessageUpdatedEvent : MessageEvent
    {
        #region Properties
        /// <inheritdoc cref="Message.UpdatedAt"/>
        public DateTime UpdatedAt => (DateTime)Message.UpdatedAt!;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="MessageUpdatedEvent"/>. This is currently only used in deserialization.
        /// </summary>
        /// <param name="serverId">The identifier of the server where the message was updated</param>
        /// <param name="message">The message that has been updated</param>
        [JsonConstructor]
        public MessageUpdatedEvent(
            [JsonProperty(Required = Required.Always)]
            HashId serverId,

            [JsonProperty(Required = Required.Always)]
            Message message
        ) : base(serverId, message) { }
        #endregion
    }
}