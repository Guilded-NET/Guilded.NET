using System.Collections.Generic;
using Guilded.Base.Events;

namespace Guilded.Commands;

/// <summary>
/// Represents the information about the root/original command.
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
/// <seealso cref="CommandEvent" />
/// <seealso cref="FailedCommandEvent" />
public struct RootCommandEvent
{
    #region Properties
    /// <summary>
    /// Gets the prefix that was fetched for the command.
    /// </summary>
    /// <value>Prefix</value>
    public string Prefix { get; }

    /// <summary>
    /// Gets the name of the original command that was used.
    /// </summary>
    /// <value>Name</value>
    public string RootCommandName { get; }

    /// <summary>
    /// Gets the given arguments to the original command.
    /// </summary>
    /// <value>Arguments</value>
    public IEnumerable<string> RootArguments { get; }

    /// <summary>
    /// Gets the message event that invoked the command.
    /// </summary>
    /// <value>Message event</value>
    public MessageEvent MessageEvent { get; }

    /// <summary>
    /// Gets any additional context that were passed manually by the bot developer (you).
    /// </summary>
    /// <value>Any object</value>
    public object? AdditionalContext { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="RootCommandEvent" />.
    /// </summary>
    /// <param name="messageEvent">The message event that invoked the command</param>
    /// <param name="prefix">The prefix that was fetched for the command</param>
    /// <param name="commandName">The name of the original command that was used</param>
    /// <param name="arguments">The given arguments to the original command</param>
    /// <param name="additionalContext">The additional context that were passed</param>
    public RootCommandEvent(MessageEvent messageEvent, string prefix, string commandName, IEnumerable<string> arguments, object? additionalContext) =>
        (MessageEvent, Prefix, RootCommandName, RootArguments, AdditionalContext) = (messageEvent, prefix, commandName, arguments, additionalContext);
    #endregion
}