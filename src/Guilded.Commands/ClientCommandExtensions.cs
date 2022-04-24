using System;

namespace Guilded.Commands;

/// <summary>
/// Extensions for Guilded clients related to commands.
/// </summary>
/// <remarks>
/// <para>Adds command-related extensions to clients.</para>
/// </remarks>
/// <seealso cref="CommandModule" />
public static class ClientCommandExtensions
{
    #region AddCommands from Runtime type
    /// <summary>
    /// Adds a new command module to the client.
    /// </summary>
    /// <param name="client">The client to add command module to</param>
    /// <param name="type">The type to fetch commands from</param>
    /// <param name="prefix">The context-based command prefix</param>
    /// <param name="separators">The command argument separator characters</param>
    /// <param name="splitOptions">The command argument splitting options</param>
    /// <returns>Guilded client</returns>
    public static AbstractGuildedClient AddCommandsFromType(this AbstractGuildedClient client, Type type, ContextPrefix prefix, char[] separators, StringSplitOptions splitOptions = CommandModule.DefaultSplitOptions)
    {
        CommandModule commands = new(prefix, separators, splitOptions);

        commands.IncludeCommandsFrom(type);

        commands.AddTo(client);

        return client;
    }
    /// <inheritdoc cref="AddCommandsFromType(AbstractGuildedClient, Type, ContextPrefix, char[], StringSplitOptions)" />
    public static AbstractGuildedClient AddCommandsFromType(this AbstractGuildedClient client, Type type, ContextPrefix prefix, StringSplitOptions splitOptions = CommandModule.DefaultSplitOptions) =>
        AddCommandsFromType(client, type, prefix, CommandModule.DefaultSeparators, splitOptions);
    /// <summary>
    /// Adds a new command module to the client.
    /// </summary>
    /// <param name="client">The client to add command module to</param>
    /// <param name="type">The type to fetch commands from</param>
    /// <param name="prefix">The static command prefix</param>
    /// <param name="separators">The command argument separator characters</param>
    /// <param name="splitOptions">The command argument splitting options</param>
    /// <returns>Guilded client</returns>
    public static AbstractGuildedClient AddCommandsFromType(this AbstractGuildedClient client, Type type, string prefix, char[] separators, StringSplitOptions splitOptions = CommandModule.DefaultSplitOptions) =>
        AddCommandsFromType(client, type, _ => prefix, separators, splitOptions);
    /// <inheritdoc cref="AddCommandsFromType(AbstractGuildedClient, Type, string, StringSplitOptions)" />
    public static AbstractGuildedClient AddCommandsFromType(this AbstractGuildedClient client, Type type, string prefix, StringSplitOptions splitOptions = CommandModule.DefaultSplitOptions) =>
        AddCommandsFromType(client, type, prefix, CommandModule.DefaultSeparators, splitOptions);
    #endregion

    #region AddCommands from generic type
    /// <summary>
    /// Adds a new command module to the client.
    /// </summary>
    /// <param name="client">The client to add command module to</param>
    /// <param name="prefix">The context-based command prefix</param>
    /// <param name="separators">The command argument separator characters</param>
    /// <param name="splitOptions">The command argument splitting options</param>
    /// <typeparam name="T">The type to fetch commands from</typeparam>
    /// <returns>Guilded client</returns>
    public static AbstractGuildedClient AddCommandsFromType<T>(this AbstractGuildedClient client, ContextPrefix prefix, char[] separators, StringSplitOptions splitOptions = CommandModule.DefaultSplitOptions) =>
        AddCommandsFromType(client, typeof(T), prefix, separators, splitOptions);
    /// <inheritdoc cref="AddCommandsFromType{TCommands}(AbstractGuildedClient, ContextPrefix, char[], StringSplitOptions)" />
    public static AbstractGuildedClient AddCommandsFromType<TCommands>(this AbstractGuildedClient client, ContextPrefix prefix, StringSplitOptions splitOptions = CommandModule.DefaultSplitOptions) =>
        AddCommandsFromType<TCommands>(client, prefix, CommandModule.DefaultSeparators, splitOptions);
    /// <summary>
    /// Adds a new command module to the client.
    /// </summary>
    /// <param name="client">The client to add command module to</param>
    /// <param name="prefix">The static command prefix</param>
    /// <param name="separators">The command argument separator characters</param>
    /// <param name="splitOptions">The command argument splitting options</param>
    /// <typeparam name="T">The type to fetch commands from</typeparam>
    /// <returns>Guilded client</returns>
    public static AbstractGuildedClient AddCommandsFromType<T>(this AbstractGuildedClient client, string prefix, char[] separators, StringSplitOptions splitOptions = CommandModule.DefaultSplitOptions) =>
        AddCommandsFromType<T>(client, _ => prefix, separators, splitOptions);
    /// <inheritdoc cref="AddCommandsFromType{T}(AbstractGuildedClient, string, char[], StringSplitOptions)" />
    public static AbstractGuildedClient AddCommandsFromType<T>(this AbstractGuildedClient client, string prefix, StringSplitOptions splitOptions = CommandModule.DefaultSplitOptions) =>
        AddCommandsFromType<T>(client, prefix, CommandModule.DefaultSeparators, splitOptions);
    #endregion
}