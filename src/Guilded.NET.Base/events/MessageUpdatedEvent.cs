using System;
using Guilded.NET.Base.Content;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Events
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
        /// <inheritdoc cref="Message.UpdatedAt"/>
        [JsonIgnore]
        public DateTime UpdatedAt => (DateTime)Message.UpdatedAt;
    }
}