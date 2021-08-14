using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Events
{
    /// <summary>
    /// An event that occurs once someone creates a message.
    /// </summary>
    /// <seealso cref="MessageUpdatedEvent"/>
    /// <seealso cref="MessageDeletedEvent"/>
    public class MessageCreatedEvent : MessageEvent
    {
        /// <summary>
        /// The date of when the message was created.
        /// </summary>
        /// <value>Occurred at</value>
        [JsonIgnore]
        public DateTime OccurredAt => Message.CreatedAt;
    }
}