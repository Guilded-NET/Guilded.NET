using System;
using System.ComponentModel;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Content
{
    using Chat;
    using Users;
    /// <summary>
    /// A reply to anything.
    /// </summary>
    public abstract class Reply : ClientObject
    {
        /// <summary>
        /// ID of the reply.
        /// </summary>
        /// <value>Reply ID</value>
        [JsonProperty(Required = Required.Always)]
        public uint Id
        {
            get; set;
        }
        /// <summary>
        /// The content of the reply.
        /// </summary>
        /// <remarks>
        /// Whole message content of this reply. This is only null if it's a profile post reply.
        /// </remarks>
        /// <value>Reply message?</value>
        public MessageContent Message
        {
            get; set;
        }
        /// <summary>
        /// When the reply was created.
        /// </summary>
        /// <value>Created at</value>
        [JsonProperty(Required = Required.Always)]
        public DateTime CreatedAt
        {
            get; set;
        }
        /// <summary>
        /// When the reply was edited.
        /// </summary>
        /// <value>Edited at</value>
        [JsonProperty(Required = Required.AllowNull)]
        public DateTime? EditedAt
        {
            get; set;
        }
        /// <summary>
        /// Who created the reply.
        /// </summary>
        /// <value>Author</value>
        [JsonProperty(Required = Required.Always)]
        public GId CreatedBy
        {
            get; set;
        }
        /// <summary>
        /// Who created this content reply.
        /// </summary>
        /// <value>Reply owner</value>
        public BaseUser CreatedByInfo
        {
            get; set;
        }
        /*/// <summary>
        /// Gets author of this reply.
        /// </summary>
        /// <returns>User</returns>
        public async Task<User> GetAuthorAsync() =>
            await ParentClient.GetUserAsync(CreatedBy);
        /// <summary>
        /// Generates a reply node for this reply.
        /// </summary>
        /// <param name="type">Type of the header to generate - normal or quote reply</param>
        /// <returns>Reply header node</returns>
        public ReplyHeader GenerateReplyHeader(ReplyHeaderType type = ReplyHeaderType.Reply) =>
            ReplyHeader.Generate(Id, CreatedBy, type);*/
        // TODO: Generate quote
    }
}