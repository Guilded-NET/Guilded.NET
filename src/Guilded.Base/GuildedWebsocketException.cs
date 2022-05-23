using System;
using Websocket.Client;

namespace Guilded.Base;

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
/// <seealso cref="Events.WelcomeEvent" />
/// <seealso cref="Events.ResumeEvent" />
/// <seealso cref="GuildedException" />
/// <seealso cref="GuildedAuthorizationException" />
/// <seealso cref="GuildedPermissionException" />
/// <seealso cref="GuildedRequestException" />
/// <seealso cref="GuildedResourceException" />
[Serializable]
public class GuildedWebsocketException : Exception
{
    #region Properties
    /// <summary>
    /// Gets the response message from Guilded WebSocket.
    /// </summary>
    /// <remarks>
    /// <para>Can be used if further information is necessary.</para>
    /// </remarks>
    /// <value>WebSocket response</value>
    /// <seealso cref="GuildedWebsocketException" />
    /// <seealso cref="GuildedException" />
    /// <seealso cref="Events.WelcomeEvent" />
    /// <seealso cref="Events.ResumeEvent" />
    public ResponseMessage? Response { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new empty instance of <see cref="GuildedWebsocketException" />.
    /// </summary>
    public GuildedWebsocketException() { }

    /// <summary>
    /// Initializes a new instance of <see cref="GuildedWebsocketException" /> with a <paramref name="message" />.
    /// </summary>
    /// <param name="message">The message that was received from Guilded Websocket</param>
    public GuildedWebsocketException(string message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of <see cref="GuildedWebsocketException" /> from WebSocket <paramref name="message">response message</paramref>.
    /// </summary>
    /// <param name="response">The response message from Guilded WebSocket</param>
    /// <param name="message">The message that was received from Guilded Websocket</param>
    public GuildedWebsocketException(ResponseMessage response, string message) : this(message) =>
        Response = response;

    /// <summary>
    /// Initializes a new instance of <see cref="GuildedWebsocketException" /> with an <paramref name="inner">inner exception</paramref> explaining more.
    /// </summary>
    /// <param name="message">The message that was received from Guilded Websocket</param>
    /// <param name="inner">The inner exception of this error</param>
    public GuildedWebsocketException(string message, Exception inner) : base(message, inner) { }
    #endregion
}