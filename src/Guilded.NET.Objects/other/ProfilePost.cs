using System;
using Guilded.NET.Objects.Chat;
using Newtonsoft.Json;

namespace Guilded.NET.Objects {
    /// <summary>
    /// A post which was posted in the profile.
    /// </summary>
    public class ProfilePost: ClientObject {
        /// <summary>
        /// ID of the post.
        /// </summary>
        /// <value>Post ID</value>
        [JsonProperty("id", Required = Required.Always)]
        public uint Id {
            get; set;
        }
        /// <summary>
        /// Title of the profile post.
        /// </summary>
        /// <value>Post's title</value>
        [JsonProperty("title", Required = Required.Always)]
        public string Title {
            get; set;
        }
        /// <summary>
        /// Message content of this post.
        /// </summary>
        /// <value>Post content</value>
        [JsonProperty("message", Required = Required.Always)]
        public MessageContent Content {
            get; set;
        }
        /// <summary>
        /// When this post was created.
        /// </summary>
        /// <value>Created at</value>
        [JsonProperty("createdAt", Required = Required.Always)]
        public DateTime CreatedAt {
            get; set;
        }
        /// <summary>
        /// When this post was bumped.
        /// </summary>
        /// <value>Bumped at</value>
        [JsonProperty("bumpedAt")]
        public DateTime BumpedAt {
            get; set;
        }
        /// <summary>
        /// When this post was edited at.
        /// </summary>
        /// <value>Edited at</value>
        [JsonProperty("editedAt", Required = Required.AllowNull)]
        public DateTime? UpdatedAt {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <value>Is Share</value>
        [JsonProperty("isShare")]
        public bool IsShare {
            get; set;
        }
        /// <summary>
        /// Who posted this post.
        /// </summary>
        /// <value>Post author</value>
        [JsonProperty("createdBy", Required = Required.Always)]
        public GId Author {
            get; set;
        }
        /// <summary>
        /// In whose profile this post was posted.
        /// </summary>
        /// <value>Profile user</value>
        [JsonProperty("userId", Required = Required.Always)]
        public GId ProfileUser {
            get; set;
        }
        /// <summary>
        /// When this post was published.
        /// </summary>
        /// <value>When published</value>
        [JsonProperty("publishedAt", Required = Required.AllowNull)]
        public DateTime? PublishedAt {
            get; set;
        }
        /// <summary>
        /// Who published that post.
        /// </summary>
        /// <value>Who published</value>
        [JsonProperty("publishedBy", Required = Required.AllowNull)]
        public GId PublishedBy {
            get; set;
        }
        /// <summary>
        /// Published post's URL.
        /// </summary>
        /// <value>Publish URL</value>
        [JsonProperty("publishUrl", Required = Required.AllowNull)]
        public Uri PublishUrl {
            get; set;
        }
        /// <summary>
        /// Who created this profile post.
        /// </summary>
        /// <value>Profile post owner</value>
        [JsonProperty("createdByInfo")]
        public ProfileUser CreatedByInfo {
            get; set;
        }
    }
}