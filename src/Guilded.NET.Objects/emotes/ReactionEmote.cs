using Newtonsoft.Json;

namespace Guilded.NET.Objects {
    /// <summary>
    /// Emote used in a reaction.
    /// </summary>
    public class ReactionEmote: BaseObject {
        /// <summary>
        /// Who created the reaction.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("createdBy")]
        public GId CreatedBy {
            get; set;
        }
        /// <summary>
        /// ID of the emote being used.
        /// </summary>
        /// <value>Emote ID</value>
        [JsonProperty("customReactionId")]
        public uint EmoteId {
            get; set;
        }
        /// <summary>
        /// Emote being used.
        /// </summary>
        /// <value>Emote</value>
        [JsonProperty("customReaction")]
        public ChatEmote Emote {
            get; set;
        }
    }
}