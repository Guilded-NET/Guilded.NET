using System;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Events
{
    using Chat;
    /// <summary>
    /// A new information of a reply.
    /// </summary>
    public class ReplyUpdated : ClientObject
    {
        /// <summary>
        /// ID of the reply updated.
        /// </summary>
        /// <value>Reply ID</value>
        [JsonProperty("id", Required = Required.Always)]
        public uint Id
        {
            get; set;
        }
        /// <summary>
        /// When the reply was updated.
        /// </summary>
        /// <value>Updated at</value>
        [JsonProperty("editedAt", Required = Required.Always)]
        public DateTime EditedAt
        {
            get; set;
        }
        /// <summary>
        /// A new content of this reply.
        /// </summary>
        /// <value>Message content</value>
        [JsonProperty("message", Required = Required.Always)]
        public MessageContent Content
        {
            get; set;
        }
    }
}