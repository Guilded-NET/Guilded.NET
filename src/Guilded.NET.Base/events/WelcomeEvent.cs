using Newtonsoft.Json;

namespace Guilded.NET.Base.Events
{
    /// <summary>
    /// An event that is received once WebSocket is initiated.
    /// </summary>
    /// <seealso cref="XpAddedEvent"/>
    public class WelcomeEvent : BaseObject
    {
        #region JSON properties
        /// <summary>
        /// The duration of time between each heartbeat.
        /// </summary>
        /// <value>Milliseconds</value>
        [JsonProperty("heartbeatIntervalMs", Required = Required.Always)]
        public int HeartbeatInterval
        {
            get; set;
        }
        /// <summary>
        /// The identifier of the last event sent.
        /// </summary>
        /// <value>Event ID?</value>
        public string LastMessageId
        {
            get; set;
        }
        #endregion
    }
}