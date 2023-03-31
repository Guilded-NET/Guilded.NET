using System.Collections.Generic;
using Guilded.Commands.Items;
using Guilded.Content;
using Guilded.Events;

namespace Guilded.Commands;

/// <summary>
/// Represents an event that occurs when someone incorrectly invokes a <see cref="CommandAttribute">command</see>.
/// </summary>
/// <seealso cref="Message" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="CommandEvent" />
/// <seealso cref="CommandAttribute" />
public class FailedCommandEvent : CommandEvent
{
    #region Properties
    /// <summary>
    /// Gets the type of the error that occurred.
    /// </summary>
    /// <value>The type of the error that occurred</value>
    public FallbackType FailType { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="FailedCommandEvent" />.
    /// </summary>
    /// <param name="context">The the root-level command that was used</param>
    /// <param name="commandName">The name of the command that was used</param>
    /// <param name="arguments">The array of string arguments that were given to the command</param>
    /// <param name="type">The type of the event that occurred</param>
    public FailedCommandEvent(RootCommandEvent context, string commandName, IEnumerable<string> arguments, FallbackType type) : base(context, commandName, arguments) =>
        FailType = type;
    #endregion
}

/// <summary>
/// Represents an event that occurs when one of the <see cref="CommandParamAttribute">arguments</see> is incorrect for all <see cref="CommandAttribute">commands</see>.
/// </summary>
/// <seealso cref="Message" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="CommandEvent" />
/// <seealso cref="CommandAttribute" />
public class BadCommandArgumentEvent : FailedCommandEvent
{
    #region Properties
    /// <summary>
    /// Gets the <see cref="CommandParamAttribute">arguments</see> of each <see cref="CommandAttribute">command</see> by the same name that were incorrect.
    /// </summary>
    /// <value>The <see cref="CommandParamAttribute">arguments</see> of each <see cref="CommandAttribute">command</see> by the same name that were incorrect</value>
    public IList<AbstractCommandArgument> BadArguments { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="FailedCommandEvent" />.
    /// </summary>
    /// <param name="context">The the root-level command that was used</param>
    /// <param name="commandName">The name of the command that was used</param>
    /// <param name="arguments">The array of string arguments that were given to the command</param>
    /// <param name="badArguments">The list of arguments that failed being converted/parsed</param>
    public BadCommandArgumentEvent(RootCommandEvent context, string commandName, IEnumerable<string> arguments, IList<AbstractCommandArgument> badArguments) : base(context, commandName, arguments, FallbackType.BadArguments) =>
        BadArguments = badArguments;
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
    NoCommandFound,

    /// <summary>
    /// The <see cref="FailedCommandEvent">sub-command/command</see> was found, but it had incorrect argument count and argument integrity wasn't checked.
    /// </summary>
    BadArgumentCount,

    /// <summary>
    /// The <see cref="FailedCommandEvent">sub-command/command</see> was being invoked with bad arguments.
    /// </summary>
    BadArguments,
}