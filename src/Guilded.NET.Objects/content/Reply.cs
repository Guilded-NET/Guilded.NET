using System;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Content
{
    using Chat;
    /// <summary>
    /// A reply to anything.
    /// </summary>
    public abstract class Reply : ClientObject
    {
        /// <summary>
        /// ID of the reply.
        /// </summary>
        /// <value></value>
        [JsonProperty("id", Required = Required.Always)]
        public ulong Id
        {
            get; set;
        }
        /// <summary>
        /// The content of the reply.
        /// </summary>
        /// <value>Reply message</value>
        [JsonProperty("message", Required = Required.Always)]
        public MessageContent Message
        {
            get; set;
        }
        /// <summary>
        /// When the reply was created.
        /// </summary>
        /// <value>Created at</value>
        [JsonProperty("createdAt", Required = Required.Always)]
        public DateTime CreatedAt
        {
            get; set;
        }
        /// <summary>
        /// When the reply was edited.
        /// </summary>
        /// <value>Edited at</value>
        [JsonProperty("editedAt", Required = Required.AllowNull)]
        public DateTime? EditedAt
        {
            get; set;
        }
        /// <summary>
        /// Who created the reply.
        /// </summary>
        /// <value>Author</value>
        [JsonProperty("createdBy", Required = Required.Always)]
        public GId CreatedBy
        {
            get; set;
        }
    }
}