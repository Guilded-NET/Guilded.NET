using System;
using Websocket.Client;

namespace Guilded.Connection;

/// <summary>
/// Represents an error received from <see cref="BaseGuildedConnection.Websocket">Guilded WebSocket</see>.
/// </summary>
/// <remarks>
/// <para>This error can occur in these different ways:</para>
/// <list type="bullet">
///     <item>
///         <term>Expired last event message identifier</term>
///         <description>When the passed <c>guilded-last-message-id</c> is expired or invalid.</description>
///     </item>
/// </list>
/// <para>In API, this is a <see cref="GuildedSocketMessage">WebSocket event</see> with an opcode of <c>8</c> and no name.</para>
/// </remarks>
/// <seealso cref="BaseGuildedConnection" />
/// <seealso cref="GuildedSocketMessage" />
[Serializable]
public class GuildedSocketException : Exception
{
    #region Properties
    /// <summary>
    /// Gets the response message from <see cref="BaseGuildedConnection.Websocket">Guilded WebSocket</see>.
    /// </summary>
    /// <remarks>
    /// <para>Can be used if further information is necessary.</para>
    /// </remarks>
    /// <value>The response message from <see cref="BaseGuildedConnection.Websocket">Guilded WebSocket</see></value>
    /// <seealso cref="GuildedSocketException" />
    public ResponseMessage? Response { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new empty instance of <see cref="GuildedSocketException" />.
    /// </summary>
    public GuildedSocketException() { }

    /// <summary>
    /// Initializes a new instance of <see cref="GuildedSocketException" /> with a <paramref name="message" />.
    /// </summary>
    /// <param name="message">The message that was received from Guilded WebSocket</param>
    public GuildedSocketException(string message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of <see cref="GuildedSocketException" /> from <see cref="BaseGuildedConnection.Websocket">WebSocket</see> response message.
    /// </summary>
    /// <param name="response">The response message from Guilded WebSocket</param>
    /// <param name="message">The message that was received from Guilded WebSocket</param>
    public GuildedSocketException(ResponseMessage response, string message) : this(message) =>
        Response = response;

    /// <summary>
    /// Initializes a new instance of <see cref="GuildedSocketException" /> with an <paramref name="inner">inner exception</paramref> explaining more.
    /// </summary>
    /// <param name="message">The message that was received from Guilded WebSocket</param>
    /// <param name="inner">The inner exception of this error</param>
    public GuildedSocketException(string message, Exception inner) : base(message, inner) { }
    #endregion
}