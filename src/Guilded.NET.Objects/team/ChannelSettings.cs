using Newtonsoft.Json;

namespace Guilded.NET.Objects.Teams {
    /// <summary>
    /// All settings for a channel.
    /// </summary>
    public class ChannelSettings: BaseObject {
        /// <summary>
        /// If blogs are enabled for this announcement channel.
        /// </summary>
        /// <value>Blog enabled</value>
        [JsonProperty("isBlog")]
        public bool IsBlog {
            get; set;
        }
        /// <summary>
        /// If comments are disabled on this channel.
        /// </summary>
        /// <value>Comments disabled</value>
        [JsonProperty("disableComments")]
        public bool DisableComments {
            get; set;
        }
    }
}