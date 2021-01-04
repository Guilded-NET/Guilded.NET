using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// A header which tells to what forum/document/media reply is replying to.
    /// </summary>
    public class ReplyHeader: ContainerNode<IMessageObject> {
        /// <summary>
        /// A header which tells to what forum/document/media reply is replying to.
        /// </summary>
        public ReplyHeader() =>
            Type = NodeType.ReplyHeader;
        /// <summary>
        /// ID of the user it is replying to.
        /// </summary>
        /// <value>User ID</value>
        [JsonIgnore]
        public GId ReplyingTo {
            get => GetDataProperty<GId>("createdBy");
        }
        /// <summary>
        /// ID of the post it is replying to.
        /// </summary>
        /// <value>Post ID</value>
        [JsonIgnore]
        public ulong? PostId {
            get => GetDataProperty<ulong>("postId");
        }
        /// <summary>
        /// Type of the reply header.
        /// </summary>
        /// <value>Reply header type</value>
        [JsonIgnore]
        public string ReplyType {
            get => GetDataProperty<string>("type");
        }
        /// <summary>
        /// Turns reply header to string.
        /// </summary>
        /// <returns>Reply header as string</returns>
        public override string ToString() =>
            $"<- Post {PostId ?? 0} of {ReplyingTo}";
        /// <summary>
        /// Generates a reply header.
        /// </summary>
        /// <param name="postId">ID of the post it is replying to</param>
        /// <param name="author">ID of the post author it is replying to</param>
        /// <param name="type">Type of te reply header</param>
        /// <returns>Reply header</returns>
        public static ReplyHeader Generate(ulong postId, GId author, ReplyHeaderType type) =>
            new ReplyHeader {
                Data = JObject.FromObject(new {
                    postId,
                    createdBy = author.ToString(),
                    type = type == ReplyHeaderType.BlockQuote ? "block-quote" : "reply"
                })
            };
    }
}