using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Events
{
    /// <summary>
    /// An event that occurs once someone edits a message.
    /// </summary>
    /// <remarks>
    /// <para>An event that occurs once someone updates/edits a message in the chat.</para>
    /// <para>When receiving this event, <see cref="Chat.Message.UpdatedAt"/> will always hold a value.</para>
    /// <para>This event can occur in:</para>
    /// <list type="bullet">
    ///     <item>
    ///         <description>Chat channels</description>
    ///     </item>
    ///     <item>
    ///         <description>Voice channels</description>
    ///     </item>
    ///     <item>
    ///         <description>Stream channels</description>
    ///     </item>
    ///     <item>
    ///         <description>Direct message channels</description>
    ///     </item>
    /// </list>
    /// <para>In API, this event is called <c>ChatMessageUpdated</c>.</para>
    /// </remarks>
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