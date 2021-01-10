using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Guilded.NET.Objects.Teams {
    using Chat;
    /// <summary>
    /// Represents Guilded channel.
    /// </summary>
    public class Channel: TeamChatChannel {
        /// <summary>
        /// Represents Guilded channel.
        /// </summary>
        public Channel() =>
            ChannelCategoryId = null;
        /// <summary>
        /// A description/topic of this channel.
        /// </summary>
        /// <value>Description</value>
        [JsonProperty("description", Required = Required.AllowNull)]
        public string Description {
            get; set;
        }
        /// <summary>
        /// ID of the category this channel is in.
        /// </summary>
        /// <value>Nullable Channel ID</value>
        [JsonProperty("channelCategoryId", Required = Required.AllowNull)]
        public uint? ChannelCategoryId {
            get; set;
        }

        //=========================//
        //    Additional
        //=========================//

        /// <summary>
        /// Creates a channel mention based on a given channel.
        /// </summary>
        /// <returns>Channel mention</returns>
        public ChannelMention CreateMention() =>
            ChannelMention.Generate(this);

        //=========================//
        //    Overrides
        //=========================//

        /// <summary>
        /// Turns channel to string.
        /// </summary>
        /// <returns>Channel as a string</returns>
        public override string ToString() => $"Channel({Id})";
    }
}