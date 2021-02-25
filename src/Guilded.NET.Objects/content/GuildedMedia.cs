using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Content {
    /// <summary>
    /// Media which was posted in the profile or in a media channel.
    /// </summary>
    public class GuildedMedia: ChannelPost<uint> {
        /// <summary>
        /// Media which was posted in the profile or in a media channel.
        /// </summary>
        public GuildedMedia() =>
            (Description, Tags, UpdatedAt) = (null, null, null);
        /// <inheritdoc/>
        [JsonProperty("title", Required = Required.Always)]
        public string Title {
            get; set;
        }
        /// <inheritdoc/>
        [JsonProperty("type", Required = Required.Always)]
        public MediaType Type {
            get; set;
        }
        /// <inheritdoc/>
        [JsonProperty("src", Required = Required.Always)]
        public Uri MediaSource {
            get; set;
        }
        /// <inheritdoc/>
        [JsonProperty("description")]
        public string Description {
            get; set;
        }
        /// <inheritdoc/>
        [JsonProperty("tags")]
        public IList<string> Tags {
            get; set;
        }
        /// <summary>
        /// When this media was updated last time.
        /// </summary>
        /// <value>Updated at</value>
        [JsonProperty("updatedAt")]
        public DateTime? UpdatedAt {
            get; set;
        }
        /// <inheritdoc/>
        [JsonProperty("reactions", Required = Required.AllowNull)]
        public IList<Reaction> Reactions {
            get; set;
        }
        /// <inheritdoc/>
        [JsonProperty("srcThumbnail", Required = Required.AllowNull)]
        public Uri ThumbnailSource {
            get; set;
        }
    }
}