using System;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Events
{
    using Teams;
    /// <summary>
    /// When a notification gets received.
    /// </summary>
    /// <value>CHANNEL_BADGED</value>
    public class ChannelBadgedEvent : CommonEvent
    {
        /// <summary>
        /// When this notification appeared.
        /// </summary>
        /// <value>Created at</value>
        [JsonProperty("createdAt", Required = Required.Always)]
        public DateTime CreatedAt
        {
            get; set;
        }
        /// <summary>
        /// ID of notification content.
        /// </summary>
        /// <value>Content ID</value>
        [JsonProperty("contentId", Required = Required.Always)]
        public string ContentId
        {
            get; set;
        }
        /// <summary>
        /// In which channel type the notification appeared.
        /// </summary>
        /// <value>Channel type</value>
        [JsonProperty("contentType", Required = Required.Always)]
        public ChannelType ContentType
        {
            get; set;
        }
    }
}