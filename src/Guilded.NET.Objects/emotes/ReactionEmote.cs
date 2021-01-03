using Newtonsoft.Json;
using System.Collections.Generic;

namespace Guilded.NET.Objects {
    /// <summary>
    /// Emote used in a reaction.
    /// </summary>
    public class ReactionEmote: ClientObject {
        /// <summary>
        /// All of the people who reacted to using this reaction.
        /// </summary>
        /// <value>Authors</value>
        [JsonProperty("reactedUsers", Required = Required.Always)]
        public IList<GId> ReactedUsers {
            get; set;
        }
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