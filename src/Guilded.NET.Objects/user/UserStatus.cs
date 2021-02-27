using Newtonsoft.Json;

namespace Guilded.NET.Objects.Users
{
    using Chat;
    /// <summary>
    /// Status message of the user.
    /// </summary>
    public class UserStatus : BaseObject
    {
        /// <summary>
        /// Status message content. Null if there's no content.
        /// </summary>
        /// <value></value>
        [JsonProperty("content")]
        public MessageContent Content
        {
            get; set;
        }
        /// <summary>
        /// ID of the emote in this status.
        /// </summary>
        /// <value>Emote's ID</value>
        [JsonProperty("customReactionId")]
        public uint? EmoteId
        {
            get; set;
        }
        /// <summary>
        /// When it expires.
        /// </summary>
        /// <value>Milliseconds</value>
        [JsonProperty("expireInMs")]
        public ulong? ExpiresIn
        {
            get; set;
        }
        /// <summary>
        /// Generates user status for setting your custom status.
        /// </summary>
        /// <param name="emoteId">ID of the emote it should use</param>
        /// <param name="content">Content of the status</param>
        /// <param name="expiresIn">When it should expire in milliseconds</param>
        /// <returns>Generated status</returns>
        public static UserStatus Generate(uint? emoteId = null, MessageContent content = null, ulong? expiresIn = null) =>
            new UserStatus
            {
                EmoteId = emoteId,
                Content = content,
                ExpiresIn = expiresIn
            };
        /// <summary>
        /// Generates user status for setting your custom status.
        /// </summary>
        /// <param name="emote">ID of the emote it should use</param>
        /// <param name="content">Content of the status</param>
        /// <param name="expiresIn">When it should expire in milliseconds</param>
        /// <returns>Generated status</returns>
        public static UserStatus Generate(ChatEmote emote, MessageContent content = null, ulong? expiresIn = null) =>
            Generate(emote.Id, content, expiresIn);
    }
}