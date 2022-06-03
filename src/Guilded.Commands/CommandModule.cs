using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Guilded.Base.Events;

namespace Guilded.Commands;

/// <summary>
/// Defines a method that will fetch <see cref="CommandModule.Prefix">the prefix</see> of <see cref="CommandAttribute">the command</see>.
/// </summary>
/// <remarks>
/// <para>This allows context-based prefixes, for things like server-wide prefixes, group-wide prefixes or even user-specific prefixes.</para>
/// </remarks>
/// <param name="msgCreated">The message that was created</param>
/// <returns>Prefix</returns>
/// <seealso cref="CommandModule" />
public delegate Task<string> ContextPrefix(MessageEvent msgCreated);

/// <summary>
/// The module that adds commands to Guilded clients.
/// </summary>
/// <remarks>
/// <para>Adds customizable commands to selected clients.</para>
/// </remarks>
/// <seealso cref="ContextPrefix" />
public class CommandModule : CommandBase
{
    #region Static & Constants
    /// <summary>
    /// The default argument separator characters.
    /// </summary>
    /// <remarks>
    /// <para>By default, <c> </c>, <c>\t</c>, <c>\v</c>, <c>\n</c> and <c>\r</c> will be used.</para>
    /// </remarks>
    /// <value>Argument separator characters</value>
    public static readonly char[] DefaultSeparators = new char[] { ' ', '\t', '\n' };

    /// <summary>
    /// The default splitting options for command arguments.
    /// </summary>
    /// <remarks>
    /// <para>By default, it uses <see cref="StringSplitOptions.RemoveEmptyEntries" />.</para>
    /// </remarks>
    /// <value>Split options</value>
    public const StringSplitOptions DefaultSplitOptions = StringSplitOptions.RemoveEmptyEntries;
    #endregion

    #region Fields
    private AbstractGuildedClient? _subscribedClient;

    private IDisposable? _commandSubscription;
    #endregion

    #region Properties
    /// <summary>
    /// Gets the method that will be used to get prefix based on <see cref="MessageEvent">the current context</see>.
    /// </summary>
    /// <value>Context-based prefix</value>
    public ContextPrefix Prefix { get; set; }

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
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CommandModule" /> with context-based <paramref name="prefix" />.
    /// </summary>
    /// <param name="prefix">The context-based prefix method for commands</param>
    /// <param name="separators">The separators that split the command's arguments</param>
    /// <param name="splitOptions">The splitting options of the command's arguments</param>
    public CommandModule(ContextPrefix prefix, char[] separators, StringSplitOptions splitOptions = DefaultSplitOptions) =>
        (Prefix, Separators, SplitOptions) = (prefix, separators, splitOptions);

    /// <summary>
    /// Initializes a new instance of <see cref="CommandModule" /> with context-based <paramref name="prefix" />.
    /// </summary>
    /// <param name="prefix">The context-based prefix method for commands</param>
    /// <param name="splitOptions">The splitting options of the command's arguments</param>
    public CommandModule(ContextPrefix prefix, StringSplitOptions splitOptions = DefaultSplitOptions) : this(prefix, DefaultSeparators, splitOptions) { }

    /// <summary>
    /// Initializes a new instance of <see cref="CommandModule" /> with context-based <paramref name="prefix" />.
    /// </summary>
    /// <param name="prefix">The context-based prefix method for commands</param>
    /// <param name="separators">The separators that split the command's arguments</param>
    /// <param name="splitOptions">The splitting options of the command's arguments</param>
    public CommandModule(Func<MessageEvent, string> prefix, char[] separators, StringSplitOptions splitOptions = DefaultSplitOptions) : this(msgCreated => Task.FromResult(prefix(msgCreated)), separators, splitOptions) { }

    /// <summary>
    /// Initializes a new instance of <see cref="CommandModule" /> with context-based <paramref name="prefix" />.
    /// </summary>
    /// <param name="prefix">The context-based prefix method for commands</param>
    /// <param name="splitOptions">The splitting options of the command's arguments</param>
    public CommandModule(Func<MessageEvent, string> prefix, StringSplitOptions splitOptions = DefaultSplitOptions) : this(prefix, DefaultSeparators, splitOptions) { }

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
    /// Initializes a new instance of <see cref="CommandModule" /> with no prefix.
    /// </summary>
    /// <param name="splitOptions">The splitting options of the command's arguments</param>
    public CommandModule(StringSplitOptions splitOptions = DefaultSplitOptions) : this(string.Empty, splitOptions) { }
    #endregion

    #region Methods
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
                .Where(msgCreated => msgCreated.Content is not null)
                .Subscribe(async msgCreated =>
                {
                    string prefix = await Prefix(msgCreated).ConfigureAwait(false);

                    if (!msgCreated.Content!.StartsWith(prefix)) return;

                    string[] splitContent = msgCreated
                        .Content[prefix.Length..]
                        .Split(Separators, SplitOptions);

                    string commandName = splitContent.First();

                    if (string.IsNullOrEmpty(commandName)) return;

                    // First one is the name of the command
                    IEnumerable<string> args = splitContent.Skip(1);

                    RootCommandEvent context = new(msgCreated, prefix, commandName, args);

                    await InvokeCommandByNameAsync(context, commandName, splitContent).ConfigureAwait(false);
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
    #endregion
}