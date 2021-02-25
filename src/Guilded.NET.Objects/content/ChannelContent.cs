using System;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Content
{
    /// <summary>
    /// A base for forum posts, media, announcements, etc..
    /// </summary>
    /// <typeparam name="T">ID type</typeparam>
    public abstract class ChannelContent<T> : ClientObject
    {
        /// <summary>
        /// ID of the content which was posted.
        /// </summary>
        /// <value>Content ID</value>
        [JsonProperty("id", Required = Required.Always)]
        public T Id
        {
            get; set;
        }
        /// <summary>
        /// ID of the team this content was posted in.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty("teamId", Required = Required.Always)]
        public GId TeamId
        {
            get; set;
        }
        /// <summary>
        /// ID of the team this content was posted in.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonProperty("channelId", Required = Required.Always)]
        public Guid ChannelId
        {
            get; set;
        }
        /// <summary>
        /// When the content were created.
        /// </summary>
        /// <value>Created at</value>
        [JsonProperty("createdAt", Required = Required.Always)]
        public DateTime CreatedAt
        {
            get; set;
        }
    }
}