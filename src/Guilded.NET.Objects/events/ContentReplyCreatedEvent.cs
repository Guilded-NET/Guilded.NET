using System;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Events
{
    using Teams;
    using Content;
    /// <summary>
    /// When a reply to a forum post, document, media or announcement appears.
    /// </summary>
    /// <value>TEAM_CHANNEL_CONTENT_REPLY_CREATED</value>
    public class ContentReplyCreatedEvent : TeamEvent
    {
        /// <summary>
        /// A type of the channel where the reply appeared.
        /// </summary>
        /// <value>Channel type</value>
        [JsonProperty("contentType", Required = Required.Always)]
        public ChannelType ContentType
        {
            get; set;
        }
        /// <summary>
        /// When the content appeared.
        /// </summary>
        /// <value>Created at</value>
        [JsonProperty("createdAt", Required = Required.Always)]
        public DateTime CreatedAt
        {
            get; set;
        }
        /// <summary>
        /// Who created this content reply.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("createdBy", Required = Required.Always)]
        public GId CreatedBy
        {
            get; set;
        }
        /// <summary>
        /// A reply which was created.
        /// </summary>
        /// <value>Reply</value>
        [JsonProperty("reply", Required = Required.Always)]
        public ChannelReply Reply
        {
            get; set;
        }
        /// <summary>
        /// ID of the content, where reply was created in.
        /// </summary>
        /// <value>GId/uint</value>
        [JsonProperty("contentId", Required = Required.Always)]
        public string ContentId
        {
            get; set;
        }
    }
}