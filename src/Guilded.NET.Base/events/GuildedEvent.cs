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
        /// <para>Opcodes are defined as following:</para>
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
        ///         <description>An event that occurs once WebSocket-related error has been thrown</description>
        ///     </item>
        ///     <item>
        ///         <term><c>9</c></term>
        ///         <description>Unknown</description>
        ///     </item>
        /// </list>
        /// <para>If <see cref="Opcode"/> is received as <c>8</c>, <see cref="GuildedWebsocketException"/>
        /// will be received instead of a typical event.</para>
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
        /// <remarks>
        /// <para>This only has a value if <see cref="Opcode"/> is <c>0</c>.</para>
        /// <para><see cref="EventName"/> holds the name of the receiving Guilded event.</para>
        /// </remarks>
        /// <value>Name?</value>
        [JsonProperty("t")]
        public string EventName
        {
            get; set;
        }
        /// <summary>
        /// The data associated with the event.
        /// </summary>
        /// <remarks>
        /// <para>The data associated with the receiving event.</para>
        /// <para>Holds data of most events, including <see cref="WelcomeEvent"/>,
        /// <see cref="ResumeEvent"/> and <see cref="GuildedWebsocketException"/>. Only if <see cref="Opcode"/>
        /// is 9, this will be <see langword="null"/>.</para>
        /// </remarks>
        /// <value>Object?</value>
        [JsonProperty("d")]
        public JObject RawData
        {
            get; set;
        }
        /// <summary>
        /// An identifier that allows the event to be replayed.
        /// </summary>
        /// <remarks>
        /// <para>The identifier of the event message.</para>
        /// <para>This can be passed to <see cref="BaseGuildedClient.InitWebsocket(string, System.Uri)"/>,
        /// which will give all of the events that were supposed to be received after this event.</para>
        /// <para>This property only holds value if <see cref="Opcode"/> is <c>0</c>.</para>
        /// </remarks>
        /// <value>Event ID?</value>
        [JsonProperty("s")]
        public string MessageId
        {
            get; set;
        }
        #endregion
    }
}