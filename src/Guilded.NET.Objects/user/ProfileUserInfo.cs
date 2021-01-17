using Newtonsoft.Json;
using System;

namespace Guilded.NET.Objects {
    /// <summary>
    /// An info about the user who posted a post or a reply in other user's/their profile.
    /// </summary>
    public class ProfileUserInfo: ClientObject {
        /// <summary>
        /// ID of this user.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("id", Required = Required.Always)]
        public GId Id {
            get; set;
        }
        /// <summary>
        /// Name of this user.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty("name", Required = Required.Always)]
        public string Name {
            get; set;
        }
        /// <summary>
        /// An avatar/a profile picture this user has.
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("profilePicture", Required = Required.Always)]
        public Uri Avatar {
            get; set;
        }
        /// <summary>
        /// A URL to this user's banner.
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("profileBannerBlur", Required = Required.Always)]
        public Uri Banner {
            get; set;
        }
    }
}