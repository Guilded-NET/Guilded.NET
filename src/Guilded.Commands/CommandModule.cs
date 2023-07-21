using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Guilded.Client;
using Guilded.Events;

namespace Guilded.Commands;

/// <summary>
/// Represents the module that adds <see cref="CommandAttribute">commands</see> to <see cref="AbstractGuildedClient">Guilded clients</see>.
/// </summary>
/// <seealso cref="CommandParent" />
/// <seealso cref="CommandAttribute" />
/// <seealso cref="CommandFallbackAttribute" />
public abstract class CommandModule : CommandParent
{
    #region Fields
    private AbstractGuildedClient? _subscribedClient;

    private IDisposable? _commandSubscription;
    #endregion

    #region Methods
    private async Task<bool> HandleCommandAsync(MessageEvent msgCreated, string prefix, CommandConfiguration config, object? additionalContext)
    {
        // Unprefix the content and split it into command name and arguments (spaces and everything included)
        // Arguments may not need to be split by space in some cases (like when we have rest args)
        string[] splitContent = msgCreated
            .Content![prefix.Length..]
            .Split(config.Separators, 2, config.SplitOptions);

        string? commandName = splitContent.FirstOrDefault();

        // The prefix by itself doesn't mean anything, as people may put something like `?` by itself
        // Maybe in the future, command modules may have indexes. In those cases, that can be invoked
        // Can be useful as an alternative to help commands, but if someone sets prefix as `?`
        // it can mean that confused people are granted help (up to bot developer)
        if (string.IsNullOrEmpty(commandName))
            return false;

        string? nullableArgs = splitContent.ElementAtOrDefault(1);
        string args = nullableArgs ?? string.Empty;

        // FIXME: Legacy code: we still need argument count
        int totalArgCount = nullableArgs?.Split(config.Separators, config.SplitOptions).Length ?? 0;

        // This stays permanent; no matter which command we invoke
        RootCommandEvent context = new(msgCreated, config, prefix, commandName, args, additionalContext);

        return await InvokeCommandByNameAsync(context, commandName, args, totalArgCount).ConfigureAwait(false);
    }

    /// <summary>
    /// Checks if any <see cref="CommandAttribute">commands</see> are called in the message and invokes them.
    /// </summary>
    /// <param name="msgCreated">The supposed command message</param>
    /// <param name="prefixes">The current available prefixes usable for the command</param>
    /// <param name="config">The configuration of client's commands</param>
    /// <param name="additionalContext">The additional context for the commands</param>
    /// <returns>Whether any <see cref="CommandAttribute">command</see> has been invoked</returns>
    public virtual async Task<bool> DoCommandsAsync(MessageEvent msgCreated, IEnumerable<string> prefixes, CommandConfiguration config, object? additionalContext)
    {
        string? foundPrefix = prefixes.FirstOrDefault(prefix => msgCreated.Content!.StartsWith(prefix));

        if (foundPrefix is null)
            return false;

        return await HandleCommandAsync(msgCreated, foundPrefix, config, additionalContext);
    }

    /// <summary>
    /// Checks if any <see cref="CommandAttribute">commands</see> are called in the message and invokes them.
    /// </summary>
    /// <param name="msgCreated">The supposed command message</param>
    /// <param name="prefix">The current available prefixes usable for the command</param>
    /// <param name="config">The configuration of client's commands</param>
    /// <param name="additionalContext">The additional context for the commands</param>
    /// <returns>Whether any <see cref="CommandAttribute">command</see> has been invoked</returns>
    public virtual async Task<bool> DoCommandsAsync(MessageEvent msgCreated, string prefix, CommandConfiguration config, object? additionalContext)
    {
        if (!msgCreated.Content!.StartsWith(prefix, StringComparison.InvariantCulture))
            return false;

        return await HandleCommandAsync(msgCreated, prefix, config, additionalContext);
    }

    /// <summary>
    /// Checks if any <see cref="CommandAttribute">commands</see> are called in the message and invokes them.
    /// </summary>
    /// <param name="msgCreated">The supposed command message</param>
    /// <param name="config">The configuration of client's commands</param>
    /// <returns>Whether any <see cref="CommandAttribute">command</see> has been invoked</returns>
    public virtual Task<bool> DoCommandsAsync(MessageEvent msgCreated, CommandConfiguration config) =>
        DoCommandsAsync(msgCreated, config.Prefix, config, null);

    /// <summary>
    /// Adds the command module to the specified <paramref name="client" /> with the given settings.
    /// </summary>
    /// <param name="client">The client to add command module to</param>
    /// <param name="config">The configuration of the client's commands</param>
    public void AddTo(AbstractGuildedClient client, CommandConfiguration config)
    {
        if (_subscribedClient == client)
            throw new InvalidOperationException("Cannot add the same command module to the client");
        // If we remove the command module from the client, we don't need to have it subscribed
        else if (_subscribedClient is not null)
            _commandSubscription!.Dispose();

        // Command invokation detection
        _commandSubscription =
            client
                .MessageCreated
                .Where(msgCreated => msgCreated.Content is not null)
                .Subscribe(async msgCreated => await DoCommandsAsync(msgCreated, config).ConfigureAwait(false));
        _subscribedClient = client;
    }

    /// <summary>
    /// Removes the command module from the subscribed client.
    /// </summary>
    public void Remove()
    {
        if (_subscribedClient is null)
            throw new InvalidOperationException("Command module is not attached to any client and cannot be unsubscribed");

        _commandSubscription!.Dispose();
        _subscribedClient = null;
    }
    #endregion
}