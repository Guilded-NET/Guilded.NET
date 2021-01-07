using System;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Events {
    /// <summary>
    /// An event which has ID of a client assigned.
    /// </summary>
    public class ClientEvent: Event {
        /// <summary>
        /// ID of the Guilded client.
        /// </summary>
        /// <value>Guilded client ID</value>
        [JsonProperty("guildedClientId", Required = Required.Always)]
        public Guid GuildedClientId {
            get; set;
        }
    }
}