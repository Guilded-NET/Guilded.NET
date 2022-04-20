namespace Guilded.Base.Events;

/// <summary>
/// Represents the interface for server-based events.
/// </summary>
public interface IServerEvent
{
    /// <summary>
    /// Gets the identifier of the server where the event occurred.
    /// </summary>
    /// <value>Server ID</value>
    HashId ServerId { get; }
}