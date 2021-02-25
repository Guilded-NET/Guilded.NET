using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Objects {
    /// <summary>
    /// Emote used in a reaction.
    /// </summary>
    public class Reaction: ClientObject {
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
        /// <summary>
        /// Checks if object is equal to this reaction.
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>Equal</returns>
        public override bool Equals(object obj) =>
            obj is Reaction reaction && EmoteId == reaction.EmoteId && reaction.ReactedUsers == ReactedUsers;
        /// <summary>
        /// Gets a hashcode of this reaction.
        /// </summary>
        /// <returns>Hashcode</returns>
        public override int GetHashCode() =>
            HashCode.Combine(EmoteId);
    }
}