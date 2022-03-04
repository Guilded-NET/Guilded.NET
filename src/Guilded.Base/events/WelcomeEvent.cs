using Newtonsoft.Json;

namespace Guilded.Base.Events
{
    /// <summary>
    /// An event that is received once WebSocket is initiated.
    /// </summary>
    /// <remarks>
    /// <para>This event is received once WebSocket (re)connects to Guilded.</para>
    /// <para><see cref="WelcomeEvent"/> can be used to ensure that WebSocket has connected to Guilded or that the events from Guilded are being received.</para>
    /// <para>This event has no name in API but has an event opcode of <c>1</c>.</para>
    /// </remarks>
    /// <seealso cref="ResumeEvent"/>
    /// <seealso cref="GuildedWebsocketException"/>
    public class WelcomeEvent : BaseObject
    {
        #region JSON properties
        /// <summary>
        /// The duration between heartbeats.
        /// </summary>
        /// <remarks>
        /// <para>The duration between each heartbeat in milliseconds. The value is usually <c>22500</c>.</para>
        /// </remarks>
        /// <value>Milliseconds</value>
        public int HeartbeatInterval { get; }
        /// <summary>
        /// The identifier of the last event sent.
        /// </summary>
        /// <remarks>
        /// <para>The identifier of the last message that was received before this event.</para>
        /// </remarks>
        /// <value>Event ID?</value>
        public string? LastMessageId { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="WelcomeEvent"/>. This is currently only used in deserialization.
        /// </summary>
        /// <param name="heartbeatIntervalMs">The duration between heartbeats</param>
        /// <param name="lastMessageId">The identifier of the last event sent</param>
        [JsonConstructor]
        public WelcomeEvent(
            [JsonProperty(Required = Required.Always)]
            int heartbeatIntervalMs,

            string? lastMessageId
        ) =>
            (HeartbeatInterval, LastMessageId) = (heartbeatIntervalMs, lastMessageId);
        #endregion
    }
}