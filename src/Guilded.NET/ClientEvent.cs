namespace Guilded.NET
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    public delegate void ClientEventHandler(BasicGuildedClient client);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    /// <param name="eventArg"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public delegate void ClientEventHandler<T>(BasicGuildedClient client, T eventArg);
}