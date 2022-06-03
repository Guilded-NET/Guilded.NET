using Guilded.Base.Servers;

namespace Guilded.Base.Events;

/// <summary>
/// Represents the interface for server-based events.
/// </summary>
/// <seealso cref="MemberJoinedEvent" />
/// <seealso cref="ChannelEvent" />
public interface IServerEvent
{
    #region Properties
    /// <summary>
    /// Gets the identifier of <see cref="Server">the server</see> where the event occurred.
    /// </summary>
    /// <value>Server ID</value>
    /// <seealso cref="IServerEvent" />
    /// <seealso cref="MessageEvent{T}.ServerId" />
    HashId ServerId { get; }
    #endregion
}