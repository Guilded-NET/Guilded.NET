using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// Header that tells to what the comment is replying.
    /// </summary>
    public class ReplyHeader : ContainerNode<TextContainer, ReplyHeader>
    {
        #region Properties
        /// <summary>
        /// ID of the user it is replying to.
        /// </summary>
        /// <value>User ID?</value>
        [JsonIgnore]
        public GId? ReplyingTo => Data.CreatedBy;
        /// <summary>
        /// ID of the post it is replying to.
        /// </summary>
        /// <value>Post ID?</value>
        [JsonIgnore]
        public uint? PostId => Data.PostId;
        /// <summary>
        /// Type of the reply header.
        /// </summary>
        /// <value>Reply header type</value>
        [JsonIgnore]
        public ReplyHeaderType ReplyType => Data.Type == "block-quote" ? ReplyHeaderType.BlockQuote : ReplyHeaderType.Reply;
        #endregion

        #region Constructors
        /// <summary>
        /// Header that tells to what the comment is replying.
        /// </summary>
        public ReplyHeader() : base(NodeType.ReplyHeader, ElementType.Block, new TextContainer("")) { }
        /// <summary>
        /// Header that tells to what the comment is replying.
        /// </summary>
        /// <param name="postId">ID of the post it is replying to</param>
        /// <param name="createdBy">ID of the post author it is replying to</param>
        /// <param name="type">Type of the reply header</param>
        public ReplyHeader(uint postId, GId createdBy, ReplyHeaderType type = ReplyHeaderType.Reply) : this() =>
            (Data.PostId, Data.CreatedBy, Data.Type) = (postId, createdBy, type == ReplyHeaderType.BlockQuote ? "block-quote" : "reply");
        #endregion

        #region Overrides
        /// <summary>
        /// Converts reply to its string equivalent.
        /// </summary>
        /// <returns>Reply as string</returns>
        public override string ToString() =>
            $"<- Post {PostId ?? 0} of {ReplyingTo}\n";
        #endregion
    }
}