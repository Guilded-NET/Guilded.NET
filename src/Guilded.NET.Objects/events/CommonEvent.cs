using Guilded.NET.Objects.Chat;
using Newtonsoft.Json;
using System;


namespace Guilded.NET.Objects.Events {
    /// <summary>
    /// A base for events in both teams(reaction events, message events) and DMs.
    /// </summary>
    public class CommonEvent: ClientEvent {
        /// <summary>
        /// A base for events in both teams(reaction events, message events) and DMs.
        /// </summary>
        public CommonEvent() =>
           (TeamId, CategoryId) = (null, null);
        /// <summary>
        /// ID of the channel this event appeared. Can be a DM channel or a team channel.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonProperty("channelId")]
        public Guid ChannelId {
            get; set;
        }
        /// <summary>
        /// ID of the category this reaction's channel is.
        /// </summary>
        /// <value>Category ID</value>
        [JsonProperty("channelCategoryId")]
        public uint? CategoryId {
            get; set;
        }
        /// <summary>
        /// Type of the channel.
        /// </summary>
        /// <value>Team</value>
        [JsonProperty("channelType", Required = Required.Always)]
        public ChatType ChannelType {
            get; set;
        }
        /// <summary>
        /// ID of the team this reaction is in.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty("teamId")]
        public GId TeamId {
            get; set;
        }
    }
}