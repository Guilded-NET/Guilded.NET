using System;
using Guilded.NET.Objects.Chat;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Content {
    /// <summary>
    /// Forum post posted in a forum channel.
    /// </summary>
    public class ForumPost: ChannelPost<uint> {
        /// <summary>
        /// Forum post posted in a forum channel.
        /// </summary>
        public ForumPost() =>
            CategoryId = null;
        /// <summary>
        /// Title of the post.
        /// </summary>
        /// <value>Title</value>
        [JsonProperty("title", Required = Required.Always)]
        public string Title {
            get; set;
        }
        /// <summary>
        /// Content of this forum post.
        /// </summary>
        /// <value>Forum post content</value>
        [JsonProperty("message", Required = Required.Always)]
        public MessageContent Message {
            get; set;
        }
        /// <summary>
        /// When the forum post was bumped.
        /// </summary>
        /// <value>Bumped at</value>
        [JsonProperty("bumpedAt", Required = Required.Always)]
        public DateTime BumpedAt {
            get; set;
        }
        /// <summary>
        /// When the forum post was edited.
        /// </summary>
        /// <value>Edited at</value>
        [JsonProperty("editedAt", Required = Required.AllowNull)]
        public DateTime? EditedAt {
            get; set;
        }
        /// <summary>
        /// If this forum post is sticky/pinned.
        /// </summary>
        /// <value>Pinned/Sticky</value>
        [JsonProperty("isSticky", Required = Required.Always)]
        public bool IsSticky {
            get; set;
        }
        [JsonProperty("isShare", Required = Required.Always)]
        public bool IsShare {
            get; set;
        }
        /// <summary>
        /// If this forum post was deleted.
        /// </summary>
        /// <value>Deleted</value>
        [JsonProperty("isDeleted", Required = Required.Always)]
        public bool IsDeleted {
            get; set;
        }
        /// <summary>
        /// If this forum was locked.
        /// </summary>
        /// <value>Locked</value>
        [JsonProperty("isLocked", Required = Required.Always)]
        public bool IsLocked {
            get; set;
        }
        /// <summary>
        /// ID of the category this forum post is in.
        /// </summary>
        /// <value>Category ID</value>
        [JsonProperty("categoryId")]
        public uint? CategoryId {
            get; set;
        }
        /// <summary>
        /// Count of how many replies are in the forum post.
        /// </summary>
        /// <value>Reply count</value>
        [JsonProperty("replyCount", Required = Required.Always)]
        public uint ReplyCount {
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