using Newtonsoft.Json;

namespace Guilded.NET.Base.Events
{
    /// <summary>
    /// Event that occurs when client passes last event message identifier.
    /// </summary>
    /// <remarks>
    /// <para><see cref="ResumeEvent"/> only occurs if last event message identifier is passed to the WebSocket in <c>guilded-last-message-id</c> header. This event is only received after all events are given to the client and normal events are being received again.</para>
    /// <para>In API, this event has no name but has an event opcode of <c>2</c>.</para>
    /// </remarks>
    /// <seealso cref="WelcomeEvent"/>
    /// <seealso cref="GuildedWebsocketException"/>
    /// <seealso cref="BaseGuildedClient.InitWebsocket(string, System.Uri)"/>
    public class ResumeEvent : BaseObject
    {
        #region JSON properties
        /// <summary>
        /// The identifier of the last received event.
        /// </summary>
        /// <remarks>
        /// <para>Gets the identifier of the last received event message that was passed in <c>guilded-last-message-id</c>.</para>
        /// <para>You can get the identifier of the event message by using <see cref="GuildedSocketMessage.MessageId"/> property from events.</para>
        /// </remarks>
        /// <value>Event message ID</value>
        public string MessageId
        {
            get; set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="ResumeEvent"/>. This is currently only used in deserialization.
        /// </summary>
        /// <param name="s">The identifier of the last received message</param>
        [JsonConstructor]
        public ResumeEvent(
            [JsonProperty(Required = Required.Always)]
            string s
        ) =>
            MessageId = s;
        #endregion
    }
}