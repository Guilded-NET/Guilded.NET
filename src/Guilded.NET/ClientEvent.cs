namespace Guilded.NET
{
    /// <summary>
    /// The event handler of all Guilded events.
    /// </summary>
    /// <param name="client">The receiving client</param>
    public delegate void ClientEventHandler(GuildedClient client);
    /// <summary>
    /// The event handler of all Guilded events.
    /// </summary>
    /// <param name="client">The receiving client</param>
    /// <param name="eventArg">An argument from the event received</param>
    /// <typeparam name="T">The type of the argument</typeparam>
    public delegate void ClientEventHandler<T>(GuildedClient client, T eventArg);
}