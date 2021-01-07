using Newtonsoft.Json;

namespace Guilded.NET.Objects.Events {
    /// <summary>
    /// A user in an event.
    /// </summary>
    public class UserEvent: ClientObject {
        /// <summary>
        /// ID of a user for the event.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("id", Required = Required.Always)]
        public GId Id {
            get; set;
        }
    }
}