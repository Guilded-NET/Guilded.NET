using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Events
{
    /// <summary>
    /// An event that occurs once someone creates a message.
    /// </summary>
    /// <remarks>
    /// <para>An event that occurs once someone creates/posts a message in the chat.</para>
    /// <para>When receiving this event, <see cref="Chat.Message.UpdatedAt"/> will never hold a value.</para>
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
    /// <para>In API, this event is called <c>ChatMessageCreated</c>.</para>
    /// </remarks>
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