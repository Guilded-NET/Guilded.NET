using System.Collections.Generic;
using Guilded.Content;
using Guilded.Events;

namespace Guilded.Commands;

/// <summary>
/// Represents an event that occurs once someone invokes a command.
/// </summary>
/// <seealso cref="Message" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="FailedCommandEvent" />
/// <seealso cref="CommandAttribute" />
public class CommandEvent : MessageEvent, IHasParentClient
{
    #region Properties CommandEvent
    /// <summary>
    /// Gets the most-top <see cref="CommandAttribute">command</see> that was invoked.
    /// </summary>
    /// <example>
    /// <para>Let's say we have this command structure:</para>
    /// <code>
    /// - `config` command
    ///     - `items` command
    ///         - `add` command with arguments (string name)
    /// </code>
    /// <para>Even if we invoke <q>config items add</q> command, the root command will always remain <q>config</q>.</para>
    /// </example>
    /// <value>The most-top <see cref="CommandAttribute">command</see> that was invoked</value>
    /// <seealso cref="CommandEvent" />
    /// <seealso cref="RootCommandName" />
    /// <seealso cref="RootArguments" />
    public RootCommandEvent RootCommand { get; }

    /// <summary>
    /// Gets the prefix that has been used on the <see cref="CommandAttribute">command</see>.
    /// </summary>
    /// <value>The prefix that has been used on the <see cref="CommandAttribute">command</see></value>
    /// <seealso cref="CommandEvent" />
    /// <seealso cref="RootCommandName" />
    /// <seealso cref="RootArguments" />
    /// <seealso cref="CommandName" />
    /// <seealso cref="Arguments" />
    public string Prefix => RootCommand.Prefix;

    /// <summary>
    /// Gets the name of the root-level <see cref="CommandAttribute">command</see> that was used in the <see cref="Message">message</see>.
    /// </summary>
    /// <value>The name of the root-level <see cref="CommandAttribute">command</see> that was used in the <see cref="Message">message</see></value>
    /// <seealso cref="CommandEvent" />
    /// <seealso cref="RootArguments" />
    /// <seealso cref="Prefix" />
    /// <seealso cref="CommandName" />
    public string RootCommandName => RootCommand.CommandName;

    /// <summary>
    /// Gets the string arguments that were given to the root-level <see cref="CommandAttribute">command</see> in the <see cref="Message">message</see>.
    /// </summary>
    /// <value>The string arguments that were given to the root-level <see cref="CommandAttribute">command</see> in the <see cref="Message">message</see></value>
    /// <seealso cref="CommandEvent" />
    /// <seealso cref="RootCommandName" />
    /// <seealso cref="Prefix" />
    /// <seealso cref="Arguments" />
    public string RootArguments => RootCommand.Arguments;

    /// <summary>
    /// Gets the name of that was used in the <see cref="CommandAttribute">command</see>.
    /// </summary>
    /// <value>The name of that was used in the <see cref="CommandAttribute">command</see></value>
    /// <seealso cref="CommandEvent" />
    /// <seealso cref="Arguments" />
    /// <seealso cref="RootCommandName" />
    /// <seealso cref="Prefix" />
    public string CommandName { get; }

    /// <summary>
    /// Gets the enumerable of string arguments that were given to the <see cref="CommandAttribute">command</see>.
    /// </summary>
    /// <value>The enumerable of string arguments that were given to the <see cref="CommandAttribute">command</see></value>
    /// <seealso cref="CommandEvent" />
    /// <seealso cref="CommandName" />
    /// <seealso cref="RootArguments" />
    /// <seealso cref="Prefix" />
    public IEnumerable<string> Arguments { get; }

    /// <inheritdoc cref="RootCommandEvent.AdditionalContext" />
    public object? AdditionalContext => RootCommand.AdditionalContext;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CommandEvent" /> from <see cref="MessageEvent">a Created <see cref="Message">message</see></see>.
    /// </summary>
    /// <param name="context">The the root-level command that was used</param>
    /// <param name="commandName">The name of the command that was used</param>
    /// <param name="arguments">The array of string arguments that were given to the command</param>
    public CommandEvent(RootCommandEvent context, string commandName, IEnumerable<string> arguments) : base(context.MessageEvent.Message, context.MessageEvent.ServerId) =>
        (RootCommand, CommandName, Arguments, ParentClient) = (context, commandName, arguments, context.MessageEvent.ParentClient);
    #endregion
}