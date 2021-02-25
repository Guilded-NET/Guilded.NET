using System;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Events {
    /// <summary>
    /// Message which has been deleted/pinned/unpinned.
    /// </summary>
    public class MessageEvent: ClientObject {
        /// <summary>
        /// ID of the message which was deleted/pinned/unpinned.
        /// </summary>
        /// <value>Message ID</value>
        [JsonProperty("id", Required = Required.Always)]
        public Guid Id {
            get; set;
        }
    }
}