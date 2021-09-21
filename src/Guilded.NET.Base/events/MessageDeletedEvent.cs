using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Events
{
    using Chat;
    /// <summary>
    /// An event that occurs once someone deletes a message.
    /// </summary>
    /// <remarks>
    /// <para>An event that occurs once someone creates/posts a message in the chat.</para>
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
    /// <para>In API, this event is called <c>ChatMessageDeleted</c>.</para>
    /// </remarks>
    /// <seealso cref="MessageCreatedEvent"/>
    /// <seealso cref="MessageUpdatedEvent"/>
    /// <seealso cref="MessageDeleted"/>
    public class MessageDeletedEvent : MessageEvent<MessageDeleted>
    {
        /// <summary>
        /// The date of when the message was deleted.
        /// </summary>
        /// <value>Occurred at</value>
        [JsonIgnore]
        public DateTime OccurredAt => Message.DeletedAt;
    }
}