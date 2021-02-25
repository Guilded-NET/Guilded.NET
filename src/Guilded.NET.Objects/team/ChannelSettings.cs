using Newtonsoft.Json;

namespace Guilded.NET.Objects.Teams
{
    /// <summary>
    /// All settings for a channel.
    /// </summary>
    public class ChannelSettings : BaseObject
    {
        /// <summary>
        /// If blogs are enabled for this announcement channel.
        /// </summary>
        /// <value>Blog enabled</value>
        [JsonProperty("isBlog")]
        public bool IsBlog
        {
            get; set;
        }
        /// <summary>
        /// If comments are disabled on this channel.
        /// </summary>
        /// <value>Comments disabled</value>
        [JsonProperty("disableComments")]
        public bool DisableComments
        {
            get; set;
        }
        /// <summary>
        /// Bitrate in a voice channel.
        /// </summary>
        /// <value>Bitrate</value>
        [JsonProperty("voiceBitrate")]
        public uint VoiceBitrate
        {
            get; set;
        }
        /// <summary>
        /// A region of the server this voice channel is using.
        /// </summary>
        /// <value>Voice region</value>
        [JsonProperty("channelRegion")]
        public VoiceRegion ChannelRegion
        {
            get; set;
        }
    }
}