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
public class FailedCommandEvent : CommandEvent
{
    #region Properties
    /// <summary>
    /// Gets the type of the error that occurred.
    /// </summary>
    /// <value>Event type</value>
    public FallbackType Type { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="FailedCommandEvent" />.
    /// </summary>
    /// <param name="context">The the root-level command that was used</param>
    /// <param name="commandName">The name of the command that was used</param>
    /// <param name="arguments">The array of string arguments that were given to the command</param>
    /// <param name="type">The type of the event that occurred</param>
    public FailedCommandEvent(RootCommandContext context, string commandName, IEnumerable<string> arguments, FallbackType type) : base(context, commandName, arguments) =>
        Type = type;
    #endregion
}

/// <summary>
/// Represents the type of <see cref="FailedCommandEvent">sub command failure</see>.
/// </summary>
/// <seealso cref="FailedCommandEvent" />
public enum FallbackType
{
    /// <summary>
    /// The parent command was called with the name of <see cref="FailedCommandEvent">sub-command</see> not given.
    /// </summary>
    Unspecified,
    /// <summary>
    /// The <see cref="FailedCommandEvent">sub-command/command</see> with the specified name or arguments does not exist.
    /// </summary>
    NoCommandFound
}