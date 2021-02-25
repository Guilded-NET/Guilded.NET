using System;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Content
{
    /// <summary>
    /// A reply to a forum post.
    /// </summary>
    public class ForumReply : ContentReply
    {
        /// <summary>
        /// To what it is replying. If it's not replying to anyone, it gives ID of the post.
        /// </summary>
        /// <value>Post/Reply ID</value>
        [JsonProperty("repliesTo", Required = Required.Always)]
        public ulong RepliesTo
        {
            get; set;
        }
        /// <summary>
        /// What bot created the reply.
        /// </summary>
        /// <value>Bot ID</value>
        [JsonProperty("createdByBotId", Required = Required.AllowNull)]
        public Guid? CreatedByBotId
        {
            get; set;
        }
    }
}