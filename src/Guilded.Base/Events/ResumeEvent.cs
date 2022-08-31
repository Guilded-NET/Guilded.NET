using Newtonsoft.Json;

namespace Guilded.Base.Events;

/// <summary>
/// Represents an event that occurs once all missed events were <see cref="BaseGuildedClient.LastMessageId">resumed</see>.
/// </summary>
/// <seealso cref="WelcomeEvent" />
/// <seealso cref="GuildedSocketMessage" />
/// <seealso cref="GuildedWebsocketException" />
/// <seealso cref="BaseGuildedClient.LastMessageId" />
public class ResumeEvent
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the last received event.
    /// </summary>
    /// <remarks>
    /// <para>This is the identifier of the event message that was passed in <c>guilded-last-message-id</c>.</para>
    /// <para>You can get the identifier of the event message by using <see cref="GuildedSocketMessage.MessageId" /> property from events.</para>
    /// </remarks>
    /// <value>Event message ID</value>
    public string MessageId { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ResumeEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="s">The identifier of the last received message</param>
    /// <returns>New <see cref="ResumeEvent" /> JSON instance</returns>
    /// <seealso cref="ResumeEvent" />
    [JsonConstructor]
    public ResumeEvent(
        [JsonProperty(Required = Required.Always)]
        string s
    ) =>
        MessageId = s;
    #endregion
}