using Guilded.NET.Objects.Chat;
using Newtonsoft.Json;
using System;


namespace Guilded.NET.Objects.Events {
    /// <summary>
    /// A base for events in teams(reaction events, message events).
    /// </summary>
    /// <typeparam name="T">Child type</typeparam>
    public class TeamEvent: Event {
        /// <summary>
        /// A base for events in teams(reaction events, message events).
        /// </summary>
        public TeamEvent() =>
           GuildedClientId = null;
        /// <summary>
        /// ID of the Guilded client.
        /// </summary>
        /// <value>Guilded client ID</value>
        [JsonProperty("guildedClientId")]
        public Guid? GuildedClientId {
            get; set;
        }
        /// <summary>
        /// ID of the channel this reaction's message is in.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonProperty("channelId", Required = Required.Always)]
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
        [JsonProperty("channelType")]
        public ChatType ChannelType {
            get; set;
        }
        /// <summary>
        /// ID of the team this reaction is in.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty("teamId", NullValueHandling = NullValueHandling.Ignore)]
        public GId TeamId {
            get; set;
        } = null;
    }
}