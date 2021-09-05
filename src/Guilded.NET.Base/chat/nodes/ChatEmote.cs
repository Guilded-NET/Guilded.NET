using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// A small inline image that represents an emotion or a meme.
    /// </summary>
    /// <remarks>
    /// Any global emote/emoji, even the ones you don't have in emote list.
    /// </remarks>
    /// <seealso cref="Image"/>
    public class ChatEmote : ContainerNode<TextContainer, ChatEmote>
    {
        #region Properties
        /// <summary>
        /// The information about the emote used.
        /// </summary>
        /// <value>Emote info?</value>
        [JsonIgnore]
        public EmoteInfo Emote => Data.Emote;
        #endregion

        #region Constructors
        /// <summary>
        /// A small inline image that represents an emotion or a meme.
        /// </summary>
        /// <param name="emote">The info of the emote to use</param>
        public ChatEmote(EmoteInfo emote) : base(NodeType.Reaction, ElementType.Inline, new TextContainer($":${emote?.CustomEmote?.Name}:")) =>
            Data.Emote = emote;
        /// <summary>
        /// A small inline image that represents an emotion or a meme.
        /// </summary>
        /// <param name="emote">The emote to use</param>
        public ChatEmote(BaseEmote emote) : this(new EmoteInfo(emote)) { }
        #endregion

        #region Overrides
        /// <summary>
        /// Converts emote to its string equivalent.
        /// </summary>
        /// <returns>Emote as string</returns>
        public override string ToString() =>
            $":{Emote?.Id ?? 0}:";
        #endregion
    }
}