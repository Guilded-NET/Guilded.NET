namespace Guilded.Connection;

/// <summary>
/// Represents the <see cref="GuildedSocketMessage.Opcode">opcodes</see> in Guilded's WebSocket.
/// </summary>
/// <seealso cref="GuildedSocketMessage" />
/// <seealso cref="GuildedSocketException" />
public enum SocketOpcode
{
    /// <summary>
    /// The message is an event that occurred in Guilded and has associated data with it.
    /// </summary>
    /// <seealso cref="Welcome" />
    /// <seealso cref="Resume" />
    /// <seealso cref="InvalidCursor" />
    /// <seealso cref="InternalError" />
    Activity = 0,

    /// <summary>
    /// The message is a welcome handshake, which means that the WebSocket has been successfully established.
    /// </summary>
    Welcome = 1,

    /// <summary>
    /// The message is a resume message, which means that all events that were missed got returned.
    /// </summary>
    Resume = 2,

    /// <summary>
    /// The message is an error that occurred due to invalid parameters or format.
    /// </summary>
    InvalidCursor = 8,

    /// <summary>
    /// The message is an internal server error that occurred for unknown/specific reasons.
    /// </summary>
    InternalError = 9
}