using Newtonsoft.Json;

namespace Guilded.NET.Base.Events
{
    using Users;
    using Chat;
    using System;

    /// <summary>
    /// An event that occurs once someone posts a message.
    /// </summary>
    public class MessageCreatedEvent : MessageEvent
    {
        /// <summary>
        /// The date of when the message was created.
        /// </summary>
        [JsonIgnore]
        public DateTime OccurredAt => Message.CreatedAt;
    }
}