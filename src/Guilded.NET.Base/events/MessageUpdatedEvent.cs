using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Events
{
    using Content;
    /// <summary>
    /// An event that occurs once someone edits a message.
    /// </summary>
    /// <remarks>
    /// <para>An event that occurs once someone updates/edits a message in the chat.</para>
    /// <para>When receiving this event, <see cref="Message.UpdatedAt"/> will always hold a value.</para>
    /// <para>In API, this event is called <c>ChatMessageUpdated</c>.</para>
    /// </remarks>
    /// <seealso cref="MessageCreatedEvent"/>
    /// <seealso cref="MessageDeletedEvent"/>
    public class MessageUpdatedEvent : MessageEvent
    {
        /// <inheritdoc cref="Message.UpdatedAt"/>
        [JsonIgnore]
        public DateTime UpdatedAt => (DateTime)Message.UpdatedAt;
    }
}