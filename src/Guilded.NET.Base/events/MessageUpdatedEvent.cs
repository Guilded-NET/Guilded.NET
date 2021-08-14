using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Events
{
    /// <summary>
    /// An event that occurs once someone edits a message.
    /// </summary>
    /// <seealso cref="MessageCreatedEvent"/>
    /// <seealso cref="MessageDeletedEvent"/>
    public class MessageUpdatedEvent : MessageEvent
    {
        /// <summary>
        /// The date of when the message was edited.
        /// </summary>
        /// <value>Occurred at</value>
        [JsonIgnore]
        public DateTime OccurredAt => (DateTime)Message.UpdatedAt;
    }
}