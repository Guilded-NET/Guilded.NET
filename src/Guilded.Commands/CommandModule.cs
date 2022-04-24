using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Guilded.Base.Events;

namespace Guilded.Commands;

/// <summary>
/// Defines a method to fetch prefix.
/// </summary>
/// <remarks>
/// <para>Defines a method that will be used to fetch the prefix of the command. This allows context-based prefixes, for things like server-wide prefixes, group-wide prefixes or even user-specific prefixes.</para>
/// </remarks>
/// <param name="msgCreated">The message that was created</param>
/// <returns>Prefix</returns>
/// <seealso cref="CommandModule" />
public delegate string ContextPrefix(MessageEvent msgCreated);

/// <summary>
/// The module that adds commands to Guilded clients.
/// </summary>
/// <remarks>
/// <para>Adds customizable commands to selected clients.</para>
/// </remarks>
/// <seealso cref="ContextPrefix" />
public class CommandModule
{
    /// <summary>
    /// The default argument separator characters.
    /// </summary>
    /// <remarks>
    /// <para>The default characters that will be used to split different command arguments.</para>
    /// <para>By default, <c> </c>, <c>\t</c>, <c>\v</c>, <c>\n</c> and <c>\r</c> will be used.</para>
    /// </remarks>
    /// <value>Argument separator characters</value>
    public static readonly char[] DefaultSeparators = new char[] { ' ', '\t', '\v', '\n', '\r' };
    /// <summary>
    /// The default argument splitting options.
    /// </summary>
    /// <remarks>
    /// <para>The default options that will be used to split different command arguments.</para>
    /// <para>By default, it uses <see cref="StringSplitOptions.RemoveEmptyEntries" />.</para>
    /// </remarks>
    public const StringSplitOptions DefaultSplitOptions = StringSplitOptions.RemoveEmptyEntries;

    private AbstractGuildedClient? _subscribedClient;
    private IDisposable? _commandSubscription;
    private IEnumerable<ICommandInfo<MemberInfo>> _commands = new ICommandInfo<MemberInfo>[] { };
    private MethodInfo? _unknownCommandFallback;

    /// <summary>
    /// Gets the method that will be used to get prefix based on <see cref="MessageEvent">the current context</see>.
    /// </summary>
    /// <value>Context-based prefix</value>
    public ContextPrefix GetPrefix { get; set; }
    /// <summary>
    /// Gets the characters that separate command arguments.
    /// </summary>
    /// <value>Separator characters</value>
    public char[] Separators { get; set; }
    /// <summary>
    /// Gets the splitting options that will be used while splitting command arguments.
    /// </summary>
    /// <value>Splitting options</value>
    public StringSplitOptions SplitOptions { get; set; }
    /// <summary>
    /// Initializes a new instance of <see cref="CommandModule" /> with context-based <paramref name="prefix" />.
    /// </summary>
    /// <param name="prefix">The context-based prefix method for commands</param>
    /// <param name="separators">The separators that split the command's arguments</param>
    /// <param name="splitOptions">The splitting options of the command's arguments</param>
    public CommandModule(ContextPrefix prefix, char[] separators, StringSplitOptions splitOptions = DefaultSplitOptions) =>
        (GetPrefix, Separators, SplitOptions) = (prefix, separators, splitOptions);
    /// <summary>
    /// Initializes a new instance of <see cref="CommandModule" /> with context-based <paramref name="prefix" />.
    /// </summary>
    /// <param name="prefix">The context-based prefix method for commands</param>
    /// <param name="splitOptions">The splitting options of the command's arguments</param>
    public CommandModule(ContextPrefix prefix, StringSplitOptions splitOptions = DefaultSplitOptions) : this(prefix, DefaultSeparators, splitOptions) { }
    /// <summary>
    /// Creates a new instance of <see cref="CommandModule" /> with static <paramref name="prefix" />.
    /// </summary>
    /// <param name="prefix">The context-based prefix method for commands</param>
    /// <param name="separators">The separators that split the command's arguments</param>
    /// <param name="splitOptions">The splitting options of the command's arguments</param>
    public CommandModule(string prefix, char[] separators, StringSplitOptions splitOptions = DefaultSplitOptions) : this(_ => prefix, separators, splitOptions) { }
    /// <summary>
    /// Initializes a new instance of <see cref="CommandModule" /> with static <paramref name="prefix" />.
    /// </summary>
    /// <param name="prefix">The context-based prefix method for commands</param>
    /// <param name="splitOptions">The splitting options of the command's arguments</param>
    public CommandModule(string prefix, StringSplitOptions splitOptions = DefaultSplitOptions) : this(_ => prefix, DefaultSeparators, splitOptions) { }

    /// <summary>
    /// Adds the command module to the specified <paramref name="client" /> with the given settings.
    /// </summary>
    /// <param name="client">The client to add command module to</param>
    public void AddTo(AbstractGuildedClient client)
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
                .Subscribe(async msgCreated =>
                {
                    string prefix = GetPrefix(msgCreated);

                    if (!msgCreated.Content.StartsWith(prefix)) return;

                    string[] splitContent = msgCreated
                        .Content[prefix.Length..]
                        .Split(Separators, SplitOptions);

                    string commandName = splitContent.First();

                    if (string.IsNullOrEmpty(commandName)) return;

                    // First one is the name of the command
                    IEnumerable<string> args = splitContent.Skip(1);

                    CommandEvent commandEvent = new(msgCreated, prefix, commandName, args);

                    // Invoke all commands by name and wait for the good one
                    bool commandHasInvoked = await CommandUtil.InvokeAnyCommand(commandEvent, commandName, _commands, args).ConfigureAwait(false);

                    // Only use the fallback if no command was executed; some bots may require fallback
                    if (!commandHasInvoked && _unknownCommandFallback is not null)
                        _unknownCommandFallback.Invoke(this, new object[] { commandName, commandEvent, _commands });
                });
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
    /// <summary>
    /// Adds commands from the <paramref name="type">specified type</paramref>.
    /// </summary>
    /// <param name="type">The type to fetch commands from</param>
    /// <typeparam name="T">The type of <see cref="CommandAttribute">command attribute</see> to fetch</typeparam>
    /// <returns>Current <see cref="CommandModule" /> instance</returns>
    public CommandModule IncludeCommandsFrom<T>(Type type) where T : CommandAttribute
    {
        IEnumerable<ICommandInfo<MemberInfo>> newCommands = CommandUtil.GetCommandsOf(type);

        _commands = _commands.Concat(newCommands);

        // ?xyz -> Unknown command
        MethodInfo? foundUnknownFallback = CommandUtil.GetUnknownCommandFallback(type);

        if (foundUnknownFallback is not null)
            _unknownCommandFallback = foundUnknownFallback;

        return this;
    }
    /// <inheritdoc cref="IncludeCommandsFrom{T}(Type)" />
    public void IncludeCommandsFrom(Type type) =>
        IncludeCommandsFrom<CommandAttribute>(type);
    /// <summary>
    /// Adds commands from the <typeparamref name="TCommands">specified type</typeparamref>.
    /// </summary>
    /// <typeparam name="TCommands">The type to fetch commands from</typeparam>
    /// <typeparam name="TAttribute">The type of <see cref="CommandAttribute">command attribute</see> to fetch</typeparam>
    /// <returns>Current <see cref="CommandModule" /> instance</returns>
    public void IncludeCommandsFrom<TCommands, TAttribute>() where TAttribute : CommandAttribute =>
        IncludeCommandsFrom<TAttribute>(typeof(TCommands));
    /// <inheritdoc cref="IncludeCommandsFrom{T, C}" />
    public void IncludeCommandsFrom<T>() =>
        IncludeCommandsFrom<CommandAttribute>(typeof(T));
}