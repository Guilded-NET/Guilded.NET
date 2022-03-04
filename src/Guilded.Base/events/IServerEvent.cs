namespace Guilded.Base.Events
{
    /// <summary>
    /// Interface for events based in servers.
    /// </summary>
    public interface IServerEvent
    {
        /// <summary>
        /// The identifier of the server where the event occurred.
        /// </summary>
        /// <value>Server ID</value>
        HashId ServerId { get; }
    }
}