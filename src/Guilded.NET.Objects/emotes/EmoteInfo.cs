using Newtonsoft.Json;

namespace Guilded.NET.Objects {
    /// <summary>
    /// An information about an emote in emote node.
    /// </summary>
    public class EmoteInfo: ClientObject {
        /// <summary>
        /// ID of the emote.
        /// </summary>
        /// <value>Emote ID</value>
        [JsonProperty("id", Required = Required.Always)]
        public uint Id {
            get; set;
        }
        /// <summary>
        /// ID of the emote.
        /// </summary>
        /// <value>Emote ID</value>
        [JsonProperty("customReactionId", Required = Required.Always)]
        public uint CustomEmoteId {
            get; set;
        }
        /// <summary>
        /// Emote's ID, name and image URL.
        /// </summary>
        /// <value>Chat emote</value>
        [JsonProperty("customReaction", Required = Required.Always)]
        public ChatEmote CustomEmote {
            get; set;
        }
        /// <summary>
        /// Generates an emote info.
        /// </summary>
        /// <param name="emote">Emote to generate emote info from</param>
        /// <returns>Emote info</returns>
        public static EmoteInfo Generate(ChatEmote emote) =>
            new EmoteInfo {
                Id = emote.Id,
                CustomEmoteId = emote.Id,
                CustomEmote = emote
            };
    }
}