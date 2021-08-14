using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Base.Events
{
    /// <summary>
    /// Event that was received from the websocket.
    /// </summary>
    /// <seealso cref="MessageCreatedEvent"/>
    /// <seealso cref="MessageUpdatedEvent"/>
    /// <seealso cref="MessageDeletedEvent"/>
    /// <seealso cref="XpAddedEvent"/>
    /// <seealso cref="WelcomeEvent"/>
    public class GuildedEvent : ClientObject
    {
        #region JSON properties
        /// <summary>
        /// An operation code that tells about the event, which was received.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        ///     <listheader>
        ///         <term>Opcode</term>
        ///         <description>Description</description>
        ///     </listheader>
        ///     <item>
        ///         <term><c>0</c></term>
        ///         <description>An event with data associated.</description>
        ///     </item>
        ///     <item>
        ///         <term><c>1</c></term>
        ///         <description>An event that occurs once WebSocket connection is established.</description>
        ///     </item>
        ///     <item>
        ///         <term><c>2</c></term>
        ///         <description>An event that occurs once connection is re-established with passed last event.</description>
        ///     </item>
        ///     <item>
        ///         <term><c>8</c></term>
        ///         <description>Unknown</description>
        ///     </item>
        ///     <item>
        ///         <term><c>9</c></term>
        ///         <description>Unknown</description>
        ///     </item>
        /// </list>
        /// </remarks>
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