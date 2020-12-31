using System;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Events {
    /// <summary>
    /// An event which has ID of a client assigned.
    /// </summary>
    public class ClientEvent: Event {
        /// <summary>
        /// An event which has ID of a client assigned.
        /// </summary>
        public ClientEvent() =>
            GuildedClientId = null;
        /// <summary>
        /// ID of the Guilded client.
        /// </summary>
        /// <value>Guilded client ID</value>
        [JsonProperty("guildedClientId")]
        public Guid? GuildedClientId {
            get; set;
        }
    }
}