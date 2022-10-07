using System;
using Websocket.Client;

namespace Guilded.Connection;

/// <summary>
/// Represents an error received from Guilded WebSocket.
/// </summary>
/// <remarks>
/// <para>This error can occur in these different ways:</para>
/// <list type="bullet">
///     <item>
///         <term>Expired last event message identifier</term>
///         <description>When the passed <c>guilded-last-message-id</c> is expired or invalid.</description>
///     </item>
/// </list>
/// <para>In API, this is a WebSocket event with an opcode of <c>8</c> and no name.</para>
/// </remarks>
/// <seealso cref="GuildedSocketMessage" />
[Serializable]
public class GuildedSocketException : Exception
{
    #region Properties
    /// <summary>
    /// Gets the response message from Guilded WebSocket.
    /// </summary>
    /// <remarks>
    /// <para>Can be used if further information is necessary.</para>
    /// </remarks>
    /// <value>WebSocket response</value>
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
    /// Initializes a new instance of <see cref="GuildedSocketException" /> from WebSocket response <paramref name="message" />.
    /// </summary>
    /// <param name="response">The response message from Guilded WebSocket</param>
    /// <param name="message">The message that was received from Guilded WebSocket</param>
    public GuildedSocketException(ResponseMessage response, string message) : this(message) =>
        Response = response;

    /// <summary>
    /// Initializes a new instance of <see cref="GuildedSocketException" /> with an <paramref name="inner" /> exception explaining more.
    /// </summary>
    /// <param name="message">The message that was received from Guilded WebSocket</param>
    /// <param name="inner">The inner exception of this error</param>
    public GuildedSocketException(string message, Exception inner) : base(message, inner) { }
    #endregion
}