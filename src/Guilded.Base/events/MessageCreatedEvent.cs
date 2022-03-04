using Guilded.Base.Content;
using Newtonsoft.Json;

namespace Guilded.Base.Events
{
    /// <summary>
    /// An event that occurs once someone creates a message.
    /// </summary>
    /// <remarks>
    /// <para>An event of the name <c>ChatMessageCreated</c> and opcode <c>0</c> that occurs once someone creates/posts a message in the chat. When receiving this event, <see cref="Content.Message.UpdatedAt"/> will never hold a value.</para>
    /// </remarks>
    /// <seealso cref="MessageUpdatedEvent"/>
    /// <seealso cref="MessageDeletedEvent"/>
    /// <seealso cref="Message"/>
    public class MessageCreatedEvent : MessageEvent
    {
        /// <summary>
        /// Creates a new instance of <see cref="MessageCreatedEvent"/>. This is currently only used in deserialization.
        /// </summary>
        /// <param name="serverId">The identifier of the server where the message was created</param>
        /// <param name="message">The message that has been created</param>
        [JsonConstructor]
        public MessageCreatedEvent(
            [JsonProperty(Required = Required.Always)]
            HashId serverId,

            [JsonProperty(Required = Required.Always)]
            Message message
        ) : base(serverId, message) { }
    }
}