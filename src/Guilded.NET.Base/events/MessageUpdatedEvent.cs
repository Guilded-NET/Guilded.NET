using System;
using System.Threading.Tasks;
using Guilded.NET.Base.Chat;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Events
{
    /// <summary>
    /// An event that occurs once someone edits a message.
    /// </summary>
    public class MessageUpdatedEvent : MessageEvent
    {
        /// <summary>
        /// The date of when the message was updated/edited.
        /// </summary>
        [JsonIgnore]
        public DateTime OccurredAt => (DateTime)Message.UpdatedAt;
    }
}