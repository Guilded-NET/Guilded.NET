using System.Collections.Generic;

namespace Guilded.NET {
    using Objects.Events;
    using Objects;
    /// <summary>
    /// Delegate for Guilded.NET commands.
    /// </summary>
    /// <param name="client">Client currently being used</param>
    /// <param name="messageCreated">Message creation event</param>
    /// <param name="command">Command name</param>
    /// <param name="arguments">Arguments of the command</param>
    public delegate void CommandMethod(IGuildedClient client, MessageCreatedEvent messageCreated, string command, IList<string> arguments);
}