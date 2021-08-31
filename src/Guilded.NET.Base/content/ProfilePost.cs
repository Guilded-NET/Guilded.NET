using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Content
{
    using Chat;
    using Users;
    /// <summary>
    /// A post that was posted in the profile.
    /// </summary>
    public class ProfilePost : ClientObject
    {
        /// <summary>
        /// ID of the post.
        /// </summary>
        /// <value>Post ID</value>
        [JsonProperty(Required = Required.Always)]
        public uint Id
        {
            get; set;
        }
        /// <summary>
        /// Title of the profile post.
        /// </summary>
        /// <value>Post's title</value>
        [JsonProperty(Required = Required.Always)]
        public string Title
        {
            get; set;
        }
        /// <summary>
        /// Message content of this post.
        /// </summary>
        /// <value>Post content</value>
        [JsonProperty("message", Required = Required.Always)]
        public MessageContent Content
        {
            get; set;
        }
        /// <summary>
        /// When this post was created.
        /// </summary>
        /// <value>Created at</value>
        [JsonProperty(Required = Required.Always)]
        public DateTime CreatedAt
        {
            get; set;
        }
        /// <summary>
        /// When this post was bumped.
        /// </summary>
        /// <value>Bumped at</value>
        [JsonProperty(Required = Required.Always)]
        public DateTime BumpedAt
        {
            get; set;
        }
        /// <summary>
        /// When this post was edited at.
        /// </summary>
        /// <value>Edited at</value>
        [JsonProperty(Required = Required.AllowNull)]
        public DateTime? EditedAt
        {
            get; set;
        }
        /// <summary>
        /// Unknown.
        /// </summary>
        /// <value>Is Share</value>
        [JsonProperty(Required = Required.Always)]
        public bool IsShare
        {
            get; set;
        }
        /// <summary>
        /// Who posted this post.
        /// </summary>
        /// <value>Post author</value>
        [JsonProperty(Required = Required.Always)]
        public GId CreatedBy
        {
            get; set;
        }
        /// <summary>
        /// In whose profile this post was posted.
        /// </summary>
        /// <value>Profile user</value>
        [JsonProperty("userId", Required = Required.Always)]
        public GId ProfileUser
        {
            get; set;
        }
        /// <summary>
        /// When this post was published.
        /// </summary>
        /// <value>When published</value>
        [JsonProperty(Required = Required.AllowNull)]
        public DateTime? PublishedAt
        {
            get; set;
        }
        /// <summary>
        /// Who published that post.
        /// </summary>
        /// <value>Who published</value>
        [JsonProperty(Required = Required.AllowNull)]
        public GId PublishedBy
        {
            get; set;
        }
        /// <summary>
        /// Published post's URL.
        /// </summary>
        /// <value>Publish URL</value>
        [JsonProperty(Required = Required.AllowNull)]
        public Uri PublishUrl
        {
            get; set;
        }
        /// <summary>
        /// Who created this profile post.
        /// </summary>
        /// <value>Profile post owner</value>
        [JsonProperty(Required = Required.Always)]
        public BaseUser CreatedByInfo
        {
            get; set;
        }
        /// <summary>
        /// Reactions on this post.
        /// </summary>
        /// <value>List of reactions</value>
        [JsonProperty(Required = Required.Always)]
        public IList<Reaction> Reactions
        {
            get; set;
        }
        /// <summary>
        /// A list of replies on this profile post.
        /// </summary>
        /// <value>Post repliess</value>
        [JsonProperty(Required = Required.AllowNull)]
        public IList<PostReply> Replies
        {
            get; set;
        }
    }
}