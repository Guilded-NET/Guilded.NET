using System;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Events
{
    using Chat;
    /// <summary>
    /// Message posted in chat which has been updated.
    /// </summary>
    public class MessageUpdated : BaseObject, IMessage
    {
        /// <summary>
        /// ID of the message.
        /// </summary>
        /// <value>Message ID</value>
        [JsonProperty("messageId", Required = Required.Always)]
        public Guid Id
        {
            get; set;
        }
        /// <summary>
        /// Content of the message.
        /// </summary>
        /// <value>Message content</value>
        [JsonProperty("content", Required = Required.Always)]
        public MessageContent Content
        {
            get; set;
        }
        /// <summary>
        /// I don't even know what this is.
        /// </summary>
        /// <value>Something</value>
        [JsonProperty("editedAt", Required = Required.Always)]
        public DateTime EditedAt
        {
            get; set;
        }
    }
}