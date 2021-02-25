using Newtonsoft.Json;

namespace Guilded.NET.Objects.Events
{
    using Teams;
    /// <summary>
    /// When a forum post, media, document, schedule, event, etc. gets deleted.
    /// </summary>
    public class ContentDeletedEvent : TeamEvent
    {
        /// <summary>
        /// Type of the channel.
        /// </summary>
        /// <value>Team</value>
        [JsonProperty("contentType", Required = Required.Always)]
        public ChannelType ContentType
        {
            get; set;
        }
        /// <summary>
        /// ID of the content. If it's an announcement(<see cref="ContentType"/> == <see cref="ChannelType.Announcement"/>), parse it as GId. If it's not, then parse it as unsigned integer.
        /// </summary>
        /// <value>Content ID</value>
        [JsonProperty("contentId")]
        public string ContentId
        {
            get; set;
        }
    }
}