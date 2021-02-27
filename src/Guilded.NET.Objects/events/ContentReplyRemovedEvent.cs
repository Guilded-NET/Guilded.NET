using Newtonsoft.Json;

namespace Guilded.NET.Objects.Events
{
    using Teams;
    /// <summary>
    /// When a reply to a forum post, document, media or announcement gets deleted.
    /// </summary>
    /// <value>TEAM_CHANNEL_CONTENT_REPLY_DELETED</value>
    public class ContentReplyDeletedEvent : TeamEvent
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
        /// A reply which was deleted.
        /// </summary>
        /// <value>Reply ID</value>
        [JsonProperty("contentReplyId", Required = Required.Always)]
        public uint ContentReplyId
        {
            get; set;
        }
        /// <summary>
        /// ID of the content, where reply was deleted in.
        /// </summary>
        /// <value>GId/uint</value>
        [JsonProperty("contentId", Required = Required.Always)]
        public string ContentId
        {
            get; set;
        }
    }
}