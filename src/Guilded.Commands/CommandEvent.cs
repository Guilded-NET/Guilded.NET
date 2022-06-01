using System.Collections.Generic;
using Guilded.Base.Content;
using Guilded.Base.Events;

namespace Guilded.Commands;

/// <summary>
/// Represents an event that occurs once someone invokes a command.
/// </summary>
/// <seealso cref="Message" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="FailedCommandEvent" />
/// <seealso cref="CommandAttribute" />
public class CommandEvent : MessageEvent
{
    #region Properties

    #region CommandEvent properties
    /// <summary>
    /// Getss the most-top command that was invoked.
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
    /// <value>Command context</value>
    public RootCommandEvent RootCommand { get; }

    /// <summary>
    /// Gets the prefix that has been used on the command.
    /// </summary>
    /// <value>Prefix</value>
    public string Prefix => RootCommand.Prefix;

    /// <summary>
    /// Gets the name of the root-level command that was used in <see cref="Message">the message</see>.
    /// </summary>
    /// <value>Name</value>
    public string RootCommandName => RootCommand.RootCommandName;

    /// <summary>
    /// Gets the enumerable of string arguments that were given to the root-level command in <see cref="Message">the message</see>.
    /// </summary>
    /// <value>Enumerable of arguments</value>
    public IEnumerable<string> RootArguments => RootCommand.RootArguments;

    /// <summary>
    /// Gets the name of that was used in the command.
    /// </summary>
    /// <value>Name</value>
    public string CommandName { get; }

    /// <summary>
    /// Gets the enumerable of string arguments that were given to the command.
    /// </summary>
    /// <value>Enumerable of arguments</value>
    public IEnumerable<string> Arguments { get; }
    #endregion

    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CommandEvent" /> from <see cref="MessageEvent">a Created <see cref="Message">message</see></see>.
    /// </summary>
    /// <param name="context">The the root-level command that was used</param>
    /// <param name="commandName">The name of the command that was used</param>
    /// <param name="arguments">The array of string arguments that were given to the command</param>
    public CommandEvent(RootCommandEvent context, string commandName, IEnumerable<string> arguments) : base(context.MessageEvent.Message, context.MessageEvent.ServerId) =>
        (RootCommand, CommandName, Arguments) = (context, commandName, arguments);
    #endregion
}