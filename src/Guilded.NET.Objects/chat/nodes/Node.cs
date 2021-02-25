using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Objects.Chat
{
    /// <summary>
    /// Represents message node.
    /// </summary>
    public abstract class Node: ClientObject, IMessageObject {
        /// <summary>
        /// Represents message node.
        /// </summary>
        protected Node() =>
            Data = JObject.Parse("{}");
        /// <summary>
        /// Object of the node.
        /// </summary>
        /// <value>Node object</value>
        [JsonProperty("object", Required = Required.Always)]
        public MsgObject Object {
            get; set;
        }
        /// <summary>
        /// Type of the node.
        /// </summary>
        /// <value>Node type</value>
        [JsonProperty("type", Required = Required.Always)]
        public NodeType Type {
            get; set;
        }
        /// <summary>
        /// Data of this node.
        /// </summary>
        /// <value>Node data</value>
        [JsonProperty("data")]
        public JObject Data {
            get; set;
        }
        /// <summary>
        /// Gets a property from <see cref="Data"/> and checks if it exists.
        /// </summary>
        /// <param name="property">Property to get from data</param>
        /// <returns>Property in data</returns>
        protected T GetDataProperty<T>(string property) {
            // If Data is null, return null
            if(Data == null) return default;
            // If data does not contain that key, return null
            else if(!Data.ContainsKey(property)) return default;
            // Else, return that property
            else return Data[property].ToObject<T>(ParentClient.GuildedSerializer);
        }
    }
}