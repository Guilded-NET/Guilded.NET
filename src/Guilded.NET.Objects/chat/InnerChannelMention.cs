using Newtonsoft.Json;
using System;

namespace Guilded.NET.Objects.Chat {
    using Teams;
    /// <summary>
    /// Data of channel mention.
    /// </summary>
    public class ChannelMentionData: BaseObject, IMention {
        /// <inheritdoc/>
        [JsonProperty("matcher")]
        public string Matcher {
            get; set;
        }
        /// <inheritdoc/>
        [JsonProperty("name", Required = Required.Always)]
        public string Name {
            get; set;
        }
        /// <summary>
        /// ID of the channel mentioned.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonProperty("id", Required = Required.Always)]
        public Guid ChannelId {
            get; set;
        }
        /// <summary>
        /// Generates channel mention.
        /// </summary>
        /// <param name="channel">Channel to mention</param>
        /// <returns>Channel mention data</returns>
        public static ChannelMentionData Generate(Channel channel) =>
            new ChannelMentionData {
                Matcher = "#" + channel.Name.ToLower(),
                Name = channel.Name,
                ChannelId = channel.Id
            };
    }
}