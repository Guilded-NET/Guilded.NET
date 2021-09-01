using System;
using Newtonsoft.Json;

namespace Guilded.NET.Base
{
    /// <summary>
    /// The information about an emote in emote node.
    /// </summary>
    /// <seealso cref="Emote"/>
    /// <seealso cref="Chat.ChatEmote"/>
    public class EmoteInfo : ClientObject
    {
        #region JSON properties
        /// <summary>
        /// The identifier of the emote.
        /// </summary>
        /// <value>Emote ID</value>
        [JsonProperty(Required = Required.Always)]
        public uint Id
        {
            get; set;
        }
        /// <summary>
        /// The identifier of the emote.
        /// </summary>
        /// <value>Emote ID</value>
        [JsonProperty("customReactionId", Required = Required.Always)]
        public uint CustomEmoteId
        {
            get; set;
        }
        /// <summary>
        /// The emote that has been used.
        /// </summary>
        /// <value>Chat emote</value>
        [JsonProperty("customReaction", Required = Required.Always)]
        public BaseEmote CustomEmote
        {
            get; set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// The information about an emote in emote node.
        /// </summary>
        /// <param name="emote">The emote to use</param>
        public EmoteInfo(BaseEmote emote) =>
            (Id, CustomEmoteId, CustomEmote) = (emote.Id, emote.Id, emote);
        #endregion

        #region Overrides
        /// <summary>
        /// Returns whether this and <paramref name="obj"/> are equal to each other.
        /// </summary>
        /// <param name="obj">Another object to compare</param>
        /// <returns>Are equal</returns>
        public override bool Equals(object obj) =>
            obj is EmoteInfo emote && Id == emote.Id;
        /// <summary>
        /// Gets a hashcode of this object.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() =>
            HashCode.Combine(Id, CustomEmote);
        #endregion
    }
}