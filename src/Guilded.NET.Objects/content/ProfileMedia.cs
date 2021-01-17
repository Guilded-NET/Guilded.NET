using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Guilded.NET.Objects.Content {
    /// <summary>
    /// A media posted in a profile.
    /// </summary>
    public class ProfileMedia: ClientObject, IMedia {
        /// <inheritdoc/>
        [JsonProperty("id", Required = Required.Always)]
        public uint Id {
            get; set;
        }
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
        /// <inheritdoc/>
        [JsonProperty("reactions", Required = Required.Always)]
        public IList<Reaction> Reactions {
            get; set;
        }
        /// <summary>
        /// ID of the profile user this media was posted in.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("userId", Required = Required.Always)]
        public GId UserId {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [JsonProperty("showInBanner", Required = Required.Always)]
        public bool ShowInBanner {
            get; set;
        }
        /// <inheritdoc/>
        [JsonProperty("srcThumbnail", Required = Required.AllowNull)]
        public Uri ThumbnailSource {
            get; set;
        }
    }
}