using Newtonsoft.Json;

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
        /// Turns reply header to string.
        /// </summary>
        /// <returns>Reply header as string</returns>
        public override string ToString() =>
            $"-> {PostId ?? 0}";
    }
}