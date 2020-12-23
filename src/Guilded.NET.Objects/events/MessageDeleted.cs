using Newtonsoft.Json;
using System;

namespace Guilded.NET.Objects.Events {
    /// <summary>
    /// Message which has been deleted.
    /// </summary>
    public class MessageDeleted: BaseObject {
        /// <summary>
        /// ID of the message deleted.
        /// </summary>
        /// <value>Message ID</value>
        [JsonProperty("id", Required = Required.Always)]
        public Guid Id {
            get; set;
        }
    }
}