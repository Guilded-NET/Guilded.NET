using Newtonsoft.Json;

namespace Guilded.NET.Objects.Events
{
    using Teams;
    /// <summary>
    /// When a reply to a forum post, document, media or announcement gets updated.
    /// </summary>
    /// <value>TEAM_CHANNEL_CONTENT_REPLY_UPDATED</value>
    public class ContentReplyUpdatedEvent : TeamEvent
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
        /// Who updated this content reply.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("updatedBy", Required = Required.Always)]
        public GId UpdatedBy
        {
            get; set;
        }
        /// <summary>
        /// A reply which was updated.
        /// </summary>
        /// <value>Reply</value>
        [JsonProperty("reply", Required = Required.Always)]
        public ReplyUpdated Reply
        {
            get; set;
        }
        /// <summary>
        /// ID of the content, where reply was updated in.
        /// </summary>
        /// <value>GId/uint</value>
        [JsonProperty("contentId", Required = Required.Always)]
        public string ContentId
        {
            get; set;
        }
    }
}