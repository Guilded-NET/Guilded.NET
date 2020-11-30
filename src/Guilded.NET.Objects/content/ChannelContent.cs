using System;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Content {
    /// <summary>
    /// A base for forum posts, media, announcements, etc..
    /// </summary>
    /// <typeparam name="T">ID type</typeparam>
    public class ChannelContent<T>: ClientObject {
        /// <summary>
        /// A base for forum posts, media, announcements, etc..
        /// </summary>
        public ChannelContent() =>
            (TeamId, ChannelId) = (null, null);
        /// <summary>
        /// ID of the content which was posted.
        /// </summary>
        /// <value>Content ID</value>
        [JsonProperty("id", Required = Required.Always)]
        public T Id {
            get; set;
        }
        /// <summary>
        /// ID of the team this content was posted in.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty("teamId")]
        public GId TeamId {
            get; set;
        }
        /// <summary>
        /// ID of the team this content was posted in.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonProperty("channelId")]
        public Guid? ChannelId {
            get; set;
        }
        /// <summary>
        /// When the content were created.
        /// </summary>
        /// <value>Created at</value>
        [JsonProperty("createdAt", Required = Required.Always)]
        public DateTime CreatedAt {
            get; set;
        }
        /// <summary>
        /// When the content were updated.
        /// </summary>
        /// <value>Created at</value>
        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt {
            get; set;
        }
    }
}