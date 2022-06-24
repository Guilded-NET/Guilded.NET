using System;

namespace Guilded.Commands;

/// <summary>
/// Extensions for Guilded clients related to commands.
/// </summary>
/// <seealso cref="CommandModule" />
public static class ClientCommandExtensions
{
    #region Methods
    /// <summary>
    /// Adds a command module to the client.
    /// </summary>
    /// <param name="client">The client to add command module to</param>
    /// <param name="commandModule">The command module to add to the client</param>
    /// <param name="configuration">The configuration of the client's commands to use</param>
    /// <returns><see cref="GuildedBotClient">Guilded client</see></returns>
    public static AbstractGuildedClient AddCommands(this AbstractGuildedClient client, CommandModule commandModule, CommandConfiguration configuration)
    {
        commandModule.AddTo(client, configuration);

        return client;
    }

    /// <summary>
    /// Adds a command module to the client.
    /// </summary>
    /// <param name="client">The client to add command module to</param>
    /// <param name="commandModule">The command module to add to the client</param>
    /// <param name="prefix">The prefix with which all commands should start</param>
    /// <param name="separators">The separators that split the command's arguments</param>
    /// <param name="splitOptions">The splitting options of the command's arguments</param>
    /// <returns><see cref="GuildedBotClient">Guilded client</see></returns>
    public static AbstractGuildedClient AddCommands(this AbstractGuildedClient client, CommandModule commandModule, string prefix, char[] separators, StringSplitOptions splitOptions = CommandConfiguration.DefaultSplitOptions) =>
        AddCommands(client, commandModule, new CommandConfiguration(prefix, separators, splitOptions));

    /// <summary>
    /// Adds a command module to the client.
    /// </summary>
    /// <param name="client">The client to add command module to</param>
    /// <param name="commandModule">The command module to add to the client</param>
    /// <param name="prefix">The prefix with which all commands should start</param>
    /// <param name="splitOptions">The splitting options of the command's arguments</param>
    /// <returns><see cref="GuildedBotClient">Guilded client</see></returns>
    public static AbstractGuildedClient AddCommands(this AbstractGuildedClient client, CommandModule commandModule, string prefix, StringSplitOptions splitOptions = CommandConfiguration.DefaultSplitOptions) =>
        AddCommands(client, commandModule, new CommandConfiguration(prefix, splitOptions));

    /// <summary>
    /// Adds a command module to the client.
    /// </summary>
    /// <param name="client">The client to add command module to</param>
    /// <param name="commandModule">The command module to add to the client</param>
    /// <param name="splitOptions">The splitting options of the command's arguments</param>
    /// <returns><see cref="GuildedBotClient">Guilded client</see></returns>
    public static AbstractGuildedClient AddCommands(this AbstractGuildedClient client, CommandModule commandModule, StringSplitOptions splitOptions = CommandConfiguration.DefaultSplitOptions) =>
        AddCommands(client, commandModule, new CommandConfiguration(splitOptions));
    #endregion
}