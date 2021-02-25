using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Objects.Chat
{
    /// <summary>
    /// Represents Guilded's emote node.
    /// </summary>
    public class EmoteNode : ContainerNode<IMessageObject>
    {
        /// <summary>
        /// Represents Guilded's emote node.
        /// </summary>
        public EmoteNode() =>
            Type = NodeType.Reaction;
        /// <summary>
        /// Gets emote node data.
        /// </summary>
        /// <value>Emote info</value>
        [JsonIgnore]
        public EmoteInfo Emote
        {
            get => GetDataProperty<EmoteInfo>("reaction");
        }
        /// <summary>
        /// Turns emote to string.
        /// </summary>
        /// <returns>Emote as a string</returns>
        public override string ToString() =>
            // Return the string representation of the emote. If it's null, it returns <:null:0>
            $"<:{Emote?.CustomEmote?.Name ?? "null"}:{Emote?.Id ?? 0}>";
        /// <summary>
        /// Generates emote node.
        /// </summary>
        /// <param name="reaction">Emote to generate node of</param>
        /// <returns>Emote node</returns>
        public static LinkNode Generate(EmoteInfo reaction) =>
            new LinkNode
            {
                // Adds link to the link node
                Data = JObject.FromObject(new { reaction }),
                // Emotes need nodes for some reason
                Nodes = new List<IMessageObject> {
                    TextObj.GenerateText($":{reaction?.CustomEmote?.Name}:")
                }
            };
        /// <summary>
        /// Generates emote node.
        /// </summary>
        /// <param name="reaction">Emote to generate node of</param>
        /// <returns>Emote node</returns>
        public static LinkNode Generate(ChatEmote reaction) =>
            Generate(EmoteInfo.Generate(reaction));
    }
}