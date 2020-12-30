using Newtonsoft.Json;
using System;

namespace Guilded.NET.Objects.Events {
    using Teams;
    /// <summary>
    /// When message gets pinned or unpinned.
    /// </summary>
    public class MessagePinUpdatedEvent: CommonEvent {
        /// <summary>
        /// In which channel type it ocurred.
        /// </summary>
        /// <value>Channel type</value>
        [JsonProperty("contentType", Required = Required.Always)]
        public ChannelType ContentType {
            get; set;
        }
        /// <summary>
        /// Who pinned/unpinned that message.
        /// </summary>
        /// <value>Update author</value>
        [JsonProperty("updatedBy", Required = Required.Always)]
        public GId UpdatedBy {
            get; set;
        }
        /// <summary>
        /// Message which was updated.
        /// </summary>
        /// <value>Message ID</value>
        [JsonProperty("message", Required = Required.Always)]
        public MessageEvent Message {
            get; set;
        }
    }
}