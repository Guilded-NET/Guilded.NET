using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// A message sent by the system.
    /// </summary>
    public class SystemMessage: ContainerNode<IMessageObject> {
        /// <summary>
        /// A message sent by the system.
        /// </summary>
        public SystemMessage() {
            Type = NodeType.SystemMessage;
            Object = MsgObject.Block;
        }
        /// <summary>
        /// Type of the system message.
        /// </summary>
        /// <value>System message type</value>
        [JsonIgnore]
        public string MessageType {
            get => GetDataProperty("type") as string;
        }
        /// <summary>
        /// Who did the action.
        /// </summary>
        /// <value>User ID</value>
        [JsonIgnore]
        public GId CreatedBy {
            get {
                object obj = GetDataProperty("createdBy");
                // If Data does not contain `createdBy`
                if(obj == null) return null;
                // Gets the type of the createdBy
                if(obj is string str) return GId.Parse(str);
                else if(obj is JValue value) return GId.Parse(value.Value<string>());
                else if(obj is GId id) return id;
                // If it's not none of the types above, return null
                else return null;
            }
        }

    }
}