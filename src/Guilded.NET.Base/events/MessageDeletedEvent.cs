using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Events
{
    using Chat;
    /// <summary>
    /// An event that occurs once someone deletes a message.
    /// </summary>
    public class MessageDeletedEvent : MessageEvent<MessageDeleted>
    {
        /// <summary>
        /// The date of when the message was deleted.
        /// </summary>
        [JsonIgnore]
        public DateTime OccurredAt => Message.DeletedAt;
    }
}