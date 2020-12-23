using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Content {
    /// <summary>
    /// Media which was posted in the profile or in a media channel.
    /// </summary>
    public class GuildedMedia: ChannelContent<uint> {
        /// <summary>
        /// Media which was posted in the profile or in a media channel.
        /// </summary>
        public GuildedMedia() =>
            (Description, Tags, UpdatedAt) = (null, null, null);
        /// <summary>
        /// The type of the media. If it's an image or a video.
        /// </summary>
        /// <value></value>
        [JsonProperty("type", Required = Required.Always)]
        public MediaType Type {
            get; set;
        }
        /// <summary>
        /// URL of the media's source.
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("src", Required = Required.Always)]
        public Uri MediaSource {
            get; set;
        }
        /// <summary>
        /// Description of this media
        /// </summary>
        /// <value>Description</value>
        [JsonProperty("description")]
        public string Description {
            get; set;
        }
        /// <summary>
        /// Tags this media was assigned.
        /// </summary>
        /// <value>Tags</value>
        [JsonProperty("tags")]
        public IList<string> Tags {
            get; set;
        }
        /// <summary>
        /// If it should be displayed to everyone or not.
        /// </summary>
        /// <value>Visibility</value>
        [JsonProperty("visibility")]
        public string Visibility {
            get; set;
        }
        /// <summary>
        /// When the content were updated.
        /// </summary>
        /// <value>Created at</value>
        [JsonProperty("updatedAt")]
        public DateTime? UpdatedAt {
            get; set;
        }
    }
}