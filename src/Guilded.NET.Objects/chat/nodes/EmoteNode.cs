using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Represents Guilded's emote node.
    /// </summary>
    public class EmoteNode: ContainerNode<IMessageObject> {
        public EmoteNode() {
            Object = MsgObject.Inline;
            Type = NodeType.Reaction;
        }
        /// <summary>
        /// Gets emote node data.
        /// </summary>
        /// <value>Emote</value>
        [JsonIgnore]
        public Emote Emote {
            get {
                object obj = GetDataProperty("reaction");
                // If o is null, return null
                if(obj == null) return null;
                // Convert the object to emote data
                return JObject.FromObject(obj).ToObject<Emote>();
            }
        }
        /// <summary>
        /// Turns emote to string.
        /// </summary>
        /// <returns>Emote as a string</returns>
        public override string ToString() {
            // If emote is not null
            bool notNull = Emote != null;
            // Return the string representation of the emote. If it's null, it returns <:null:0>
            return $"<:{(notNull && Emote?.Name != null ? Emote.Name : "null")}:{(notNull ? Emote.Id : 0)}>";
        }
        /// <summary>
        /// Generates emote node.
        /// </summary>
        /// <param name="emote">Emote to generate node of</param>
        /// <returns>Emote node</returns>
        public static LinkNode Generate(ChatEmote emote) =>
            new LinkNode {
                // Adds link to the link node
                Data = new Dictionary<string, object> {
                    {
                        "reaction", JObject.FromObject(emote)
                    }
                },
                // Emotes need nodes for some reason
                Nodes = new List<IMessageObject> {
                    new TextObj {
                        //Generates leaves, because emotes need to
                        Leaves = new List<Leaf> {
                           Leaf.Generate($":{emote.Name}:")
                        },
                        Object = MsgObject.Text
                    }
                }
            };
        /// <summary>
        /// Generates emote node.
        /// </summary>
        /// <param name="emote">Emote to generate node of</param>
        /// <returns>Emote node</returns>
        public static LinkNode Generate(Emote emote) =>
            new LinkNode {
                // Adds link to the link node
                Data = new Dictionary<string, object> {
                    {
                        "reaction", JObject.FromObject(ChatEmote.From(emote))
                    }
                },
                // Emotes need nodes for some reason
                Nodes = new List<IMessageObject> {
                    new TextObj {
                        //Generates leaves, because emotes need to
                        Leaves = new List<Leaf> {
                           Leaf.Generate($":{emote.Name}:")
                        },
                        Object = MsgObject.Text
                    }
                }
            };
    }
}