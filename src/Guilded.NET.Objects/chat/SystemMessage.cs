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
        public SystemMessage() =>
            (Type, Object) = (NodeType.SystemMessage, MsgObject.Block);
        /// <summary>
        /// Type of the system message.
        /// </summary>
        /// <value>System message type</value>
        [JsonIgnore]
        public string MessageType {
            get => GetDataProperty<string>("type");
        }
        /// <summary>
        /// Who did the action.
        /// </summary>
        /// <value>User ID</value>
        [JsonIgnore]
        public GId CreatedBy {
            get => GetDataProperty<GId>("createdBy");
        }

    }
}