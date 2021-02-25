using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Content {
    using Chat;
    /// <summary>
    /// A document posted in the doc channel.
    /// </summary>
    public class GuildedDocument: ChannelPost<uint> {
        /// <summary>
        /// A document posted in the doc channel.
        /// </summary>
        public GuildedDocument() =>
            (Replies, ModifiedBy, ModifiedAt) = (null, null, null);
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
        [JsonProperty("content", Required = Required.Always)]
        public MessageContent Content {
            get; set;
        }
        /// <summary>
        /// When the content were updated.
        /// </summary>
        /// <value>Created at</value>
        [JsonProperty("modifiedAt")]
        public DateTime? ModifiedAt {
            get; set;
        }
        /// <summary>
        /// Who updated the document.
        /// </summary>
        /// <value>Author ID</value>
        [JsonProperty("modifiedBy")]
        public GId ModifiedBy {
            get; set;
        }
        /// <summary>
        /// If this document is public or not.
        /// </summary>
        /// <value>Public document</value>
        [JsonProperty("isPublic")]
        public bool IsPublic {
            get; set;
        }
        /// <summary>
        /// If this document is a draft document(document which is not posted yet).
        /// </summary>
        /// <value>Draft document</value>
        [JsonProperty("isDraft")]
        public bool IsDraft {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [JsonProperty("isCredentialed")]
        public bool IsCredentialed {
            get; set;
        }
        /// <summary>
        /// List of replies in this document. Null if there are 0.
        /// </summary>
        /// <value>List of document replies</value>
        [JsonProperty("replies")]
        public IList<ContentReply> Replies {
            get; set;
        }
    }
}