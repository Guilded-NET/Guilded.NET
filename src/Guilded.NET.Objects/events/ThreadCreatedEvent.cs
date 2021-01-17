namespace Guilded.NET.Objects.Events {
    using Newtonsoft.Json;
    using Teams;
    /// <summary>
    /// Event when thread gets created. Also known as temporal channel created event.
    /// </summary>
    public class ThreadCreatedEvent: TeamEvent {
        /// <summary>
        /// Thread which was created.
        /// </summary>
        /// <value>Thread</value>
        [JsonProperty("channel")]
        public ThreadChannel Channel {
            get; set;
        }
    }
}