using Newtonsoft.Json;

namespace Guilded.NET.Objects.Events {
    /// <summary>
    /// Guilded abstract event.
    /// </summary>
    public abstract class Event: ClientObject {
        /// <summary>
        /// Event type given by Guilded Websocket.
        /// </summary>
        /// <value></value>
        [JsonProperty("type")]
        public string EventType {
            get; set;
        }
    }
}