using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Base.Events
{
    /// <summary>
    /// Event that was received from the websocket.
    /// </summary>
    public class GuildedEvent : ClientObject
    {
        #region JSON properties
        /// <summary>
        /// An operation code that tells about the event, which was received.
        /// </summary>
        /// <value>Opcode</value>
        [JsonProperty("op", Required = Required.Always)]
        public uint Opcode
        {
            get; set;
        }
        /// <summary>
        /// The name of the event received.
        /// </summary>
        /// <value>Name?</value>
        [JsonProperty("t")]
        public string EventName
        {
            get; set;
        }
        /// <summary>
        /// Data associated with the event.
        /// </summary>
        /// <value>Object?</value>
        [JsonProperty("d")]
        public JObject RawData
        {
            get; set;
        }
        /// <summary>
        /// An identifier that allows the event to be replayed.
        /// </summary>
        /// <value>Event ID?</value>
        [JsonProperty("s")]
        public string MessageId
        {
            get; set;
        }
        #endregion
    }
}