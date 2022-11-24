using Guilded.Client;
using Guilded.Connection;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when WebSocket connects or reconnects.
/// </summary>
/// <remarks>
/// <para>An event with the opcode <c>1</c>.</para>
/// <para><see cref="WelcomeEvent" /> can be used to ensure that WebSocket has connected to Guilded or that the events from Guilded are being received.</para>
/// </remarks>
/// <seealso cref="ResumeEvent" />
/// <seealso cref="GuildedSocketMessage" />
/// <seealso cref="GuildedSocketException" />
public class WelcomeEvent
{
    #region Properties
    /// <summary>
    /// Gets the time duration between heartbeats in milliseconds.
    /// </summary>
    /// <remarks>
    /// <para>The value is usually <c>22500</c>.</para>
    /// </remarks>
    /// <value>Milliseconds</value>
    /// <seealso cref="WelcomeEvent" />
    /// <seealso cref="LastMessageId" />
    /// <seealso cref="User" />
    public int HeartbeatInterval { get; }

    /// <summary>
    /// Gets the identifier of the last received <see cref="GuildedSocketMessage">WebSocket message</see>.
    /// </summary>
    /// <value>The identifier of the last received <see cref="GuildedSocketMessage">WebSocket message</see></value>
    /// <seealso cref="WelcomeEvent" />
    /// <seealso cref="HeartbeatInterval" />
    /// <seealso cref="User" />
    public string? LastMessageId { get; }

    /// <summary>
    /// Gets the <see cref="Users.User">user</see> data of the <see cref="AbstractGuildedClient">client</see>.
    /// </summary>
    /// <value>The <see cref="Users.User">user</see> data of the <see cref="AbstractGuildedClient">client</see></value>
    /// <seealso cref="WelcomeEvent" />
    /// <seealso cref="LastMessageId" />
    /// <seealso cref="HeartbeatInterval" />
    public ClientUser User { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="WelcomeEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="heartbeatIntervalMs">The duration between heartbeats</param>
    /// <param name="user">The current logged in user</param>
    /// <param name="lastMessageId">The identifier of the last event sent</param>
    /// <returns>New <see cref="WelcomeEvent" /> JSON instance</returns>
    /// <seealso cref="WelcomeEvent" />
    [JsonConstructor]
    public WelcomeEvent(
        [JsonProperty(Required = Required.Always)]
        int heartbeatIntervalMs,

        [JsonProperty(Required = Required.Always)]
        ClientUser user,

        string? lastMessageId
    ) =>
        (HeartbeatInterval, User, LastMessageId) = (heartbeatIntervalMs, user, lastMessageId);
    #endregion
}