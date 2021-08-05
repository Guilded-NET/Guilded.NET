using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Content
{
    /// <summary>
    /// A reply to a profile post.
    /// </summary>
    public class PostReply : Reply
    {
        /// <summary>
        /// ID of the profile post this reply is under.
        /// </summary>
        /// <value>Post ID</value>
        [JsonProperty(Required = Required.Always)]
        public uint ContentId
        {
            get; set;
        }
        /// <summary>
        /// ID of the user under whose profile this reply is.
        /// </summary>
        /// <value>Profile user ID</value>
        [JsonProperty(Required = Required.Always)]
        public GId UserId
        {
            get; set;
        }
    }
}