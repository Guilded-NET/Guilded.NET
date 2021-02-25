using System;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Chat
{
    using Teams;
    /// <summary>
    /// A message sent by the system.
    /// </summary>
    public class SystemMessage: ContainerNode<IMessageObject> {
        /// <summary>
        /// A message sent by the system.
        /// </summary>
        public SystemMessage() =>
            Type = NodeType.SystemMessage;
        /// <summary>
        /// Type of the system message.
        /// </summary>
        /// <value>System message type</value>
        [JsonIgnore]
        public SystemMessageType MessageType {
            get => GetDataProperty<SystemMessageType>("type");
        }
        /// <summary>
        /// Who did the action.
        /// </summary>
        /// <value>User ID</value>
        [JsonIgnore]
        public GId CreatedBy {
            get => GetDataProperty<GId>("createdBy");
        }
        /// <summary>
        /// Owner of the action. (Threads created,)
        /// </summary>
        /// <value>User ID</value>
        [JsonIgnore]
        public GId OwnerId {
            get => GetDataProperty<GId>("ownerId");
        }
        /// <summary>
        /// A link of a message thread originates from. (Threads created,)
        /// </summary>
        /// <value>Message URL</value>
        [JsonIgnore]
        public Uri OriginatingUrl {
            get => GetDataProperty<Uri>("originatingUrl");
        }
        /// <summary>
        /// Type of the channel a thread is originating from. (Threads created,)
        /// </summary>
        /// <value>Origin channel type</value>
        [JsonIgnore]
        public ChannelType OriginatingContentType {
            get => GetDataProperty<ChannelType>("originatingContentType");
        }
        /// <summary>
        /// Old name of the channel. (Channels renamed)
        /// </summary>
        /// <value>Name</value>
        [JsonIgnore]
        public string OldName {
            get => GetDataProperty<string>("oldName");
        }
        /// <summary>
        /// New name of the channel. (Channels renamed)
        /// </summary>
        /// <value>Name</value>
        [JsonIgnore]
        public string NewName {
            get => GetDataProperty<string>("newName");
        }
        /// <summary>
        /// ID of the user which was removed from the DM group.
        /// </summary>
        /// <value>User ID</value>
        [JsonIgnore]
        public GId UserId {
            get => GetDataProperty<GId>("userId");
        }
    }
}