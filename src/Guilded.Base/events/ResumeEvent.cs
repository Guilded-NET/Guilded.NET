using Newtonsoft.Json;

namespace Guilded.Base.Events;

/// <summary>
/// Represents an event with an event opcode of <c>2</c> that occurs when the client passes last event message identifier.
/// </summary>
/// <seealso cref="WelcomeEvent"/>
/// <seealso cref="GuildedWebsocketException"/>
/// <seealso cref="BaseGuildedClient.LastMessageId"/>
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
    public string MessageId { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ResumeEvent"/> from the specified JSON properties.
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