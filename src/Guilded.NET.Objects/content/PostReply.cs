using System;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Content
{
    /// <summary>
    /// A reply to a profile post.
    /// </summary>
    public class PostReply : Reply
    {
        /// <summary>
        /// A name of the author.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty("name", Required = Required.Always)]
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// A profile picture of the author.
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("profilePicture", Required = Required.Always)]
        public Uri Avatar
        {
            get; set;
        }
        /// <summary>
        /// ID of the user this profile is of.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("userId", Required = Required.Always)]
        public GId UserId
        {
            get; set;
        }
    }
}