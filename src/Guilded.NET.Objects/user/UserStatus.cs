using Newtonsoft.Json;

namespace Guilded.NET.Objects {
    using Chat;
    /// <summary>
    /// Status message of the user.
    /// </summary>
    public class UserStatus: BaseObject {
        /// <summary>
        /// Status message content. Null if there's no content.
        /// </summary>
        /// <value></value>
        [JsonProperty("content")]
        public MessageContent Content {
            get; set;
        }
        /// <summary>
        /// ID of the emote in this status.
        /// </summary>
        /// <value>Emote's ID</value>
        [JsonProperty("customReactionId")]
        public uint? EmoteId {
            get; set;
        }
        /// <summary>
        /// When it expires.
        /// </summary>
        /// <value>Milliseconds</value>
        [JsonProperty("expireInMs")]
        public ulong? ExpiresIn {
            get; set;
        }
        /// <summary>
        /// Generates user status for setting your custom status.
        /// </summary>
        /// <param name="emoteId">ID of the emote it should use</param>
        /// <param name="content">Content of the status</param>
        /// <returns>Generated status</returns>
        public UserStatus Generate(uint? emoteId = null, MessageContent content = null) =>
            new UserStatus {
                EmoteId = emoteId,
                Content = content
            };
        /// <summary>
        /// Generates user status for setting your custom status.
        /// </summary>
        /// <param name="emoteId">ID of the emote it should use</param>
        /// <param name="content">Content of the status</param>
        /// <returns>Generated status</returns>
        public UserStatus Generate(uint? emoteId = null, string content = null) {
            // Generates message content for this
            MessageContent newContent = content != null ? MessageContent.Generate(ParagraphNode.Generate(Leaf.Generate(content))) : null;
            // Generates status
            return Generate(emoteId, newContent);
        }
        /// <summary>
        /// Generates user status for setting your custom status.
        /// </summary>
        /// <param name="emote">ID of the emote it should use</param>
        /// <param name="content">Content of the status</param>
        /// <returns>Generated status</returns>
        public UserStatus Generate(ChatEmote emote = null, MessageContent content = null) =>
            Generate(emote?.Id, content);
        /// <summary>
        /// Generates user status for setting your custom status.
        /// </summary>
        /// <param name="emote">ID of the emote it should use</param>
        /// <param name="content">Content of the status</param>
        /// <returns>Generated status</returns>
        public UserStatus Generate(ChatEmote emote = null, string content = null) =>
            Generate(emote?.Id, content);
        /// <summary>
        /// Generates user status for setting your custom status.
        /// </summary>
        /// <param name="emote">ID of the emote it should use</param>
        /// <param name="content">Content of the status</param>
        /// <returns>Generated status</returns>
        public UserStatus Generate(Emote emote = null, MessageContent content = null) =>
            Generate(emote?.Id, content);
        /// <summary>
        /// Generates user status for setting your custom status.
        /// </summary>
        /// <param name="emote">ID of the emote it should use</param>
        /// <param name="content">Content of the status</param>
        /// <returns>Generated status</returns>
        public UserStatus Generate(Emote emote = null, string content = null) =>
            Generate(emote?.Id, content);
    }
}