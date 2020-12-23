using System;
using Guilded.NET.Objects.Content;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Content {
    /// <summary>
    /// A base for forum posts, announcements and documents.
    /// </summary>
    /// <typeparam name="T">ID type</typeparam>
    public class ChannelPost<T>: ChannelContent<T> {
        /// <summary>
        /// A base for forum posts, announcements and documents.
        /// </summary>
        public ChannelPost() =>
            CreatedByBotId = null;
        /// <summary>
        /// Title of the post.
        /// </summary>
        /// <value>Title</value>
        [JsonProperty("title", Required = Required.Always)]
        public string Title {
            get; set;
        }
        /// <summary>
        /// If it should be displayed to everyone or not.
        /// </summary>
        /// <value>Visibility</value>
        [JsonProperty("visibility", Required = Required.Always)]
        public string Visibility {
            get; set;
        }
        /// <summary>
        /// Who created this post.
        /// </summary>
        /// <value>Author ID</value>
        [JsonProperty("createdBy", Required = Required.Always)]
        public GId CreatedBy {
            get; set;
        }
        /// <summary>
        /// Bot which created this post.
        /// </summary>
        /// <value>Bot ID</value>
        [JsonProperty("createdByBotId")]
        public Guid? CreatedByBotId {
            get; set;
        }
    }
}