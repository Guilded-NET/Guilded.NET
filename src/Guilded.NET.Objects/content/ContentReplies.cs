using System;
using System.Collections.Generic;
using Guilded.NET.Objects.Chat;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Content {
    /// <summary>
    /// A reply to a Guilded document or media.
    /// </summary>
    public class ContentReply: ClientObject {
        /// <summary>
        /// A reply to a Guilded document or media.
        /// </summary>
        public ContentReply() =>
            EditedAt = null;
        /// <summary>
        /// ID of the reply.
        /// </summary>
        /// <value></value>
        [JsonProperty("id", Required = Required.Always)]
        public ulong Id {
            get; set;
        }
        /// <summary>
        /// The content of the reply.
        /// </summary>
        /// <value>Reply message</value>
        [JsonProperty("message", Required = Required.Always)]
        public MessageContent Message {
            get; set;
        }
        /// <summary>
        /// Reactions in this reply.
        /// </summary>
        /// <value>Reactions</value>
        [JsonProperty("reactions", Required = Required.AllowNull)]
        public IList<ReactionEmote> Reactions {
            get; set;
        }
        /// <summary>
        /// When the reply was created.
        /// </summary>
        /// <value>Created at</value>
        [JsonProperty("createdAt", Required = Required.Always)]
        public DateTime CreatedAt {
            get; set;
        }
        /// <summary>
        /// When the reply was edited.
        /// </summary>
        /// <value>Edited at</value>
        [JsonProperty("editedAt", Required = Required.AllowNull)]
        public DateTime? EditedAt {
            get; set;
        }
        /// <summary>
        /// Who created the reply.
        /// </summary>
        /// <value>Author</value>
        [JsonProperty("createdBy", Required = Required.Always)]
        public GId CreatedBy {
            get; set;
        }
        /// <summary>
        /// ID of the document/media this reply was posted under.
        /// </summary>
        /// <value>Document/media ID</value>
        [JsonProperty("contentId", Required = Required.Always)]
        public uint ContentId {
            get; set;
        }
    }
}