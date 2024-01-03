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
public class FailedCommandEvent(RootCommandEvent context, string commandName, string arguments, FallbackType type) : CommandEvent(context, commandName, arguments)
{
    #region Properties
    /// <summary>
    /// Gets the type of the error that occurred.
    /// </summary>
    /// <value>The type of the error that occurred</value>
    public FallbackType FailType { get; } = type;
    #endregion
}

/// <summary>
/// Represents an event that occurs when one of the <see cref="CommandParamAttribute">arguments</see> is incorrect for all <see cref="CommandAttribute">commands</see>.
/// </summary>
/// <seealso cref="Message" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="CommandEvent" />
/// <seealso cref="CommandAttribute" />
public class BadCommandArgumentEvent(RootCommandEvent context, string commandName, string arguments, IList<AbstractCommandArgument> badArguments) : FailedCommandEvent(context, commandName, arguments, FallbackType.BadArguments)
{
    #region Properties
    /// <summary>
    /// Gets the <see cref="CommandParamAttribute">arguments</see> of each <see cref="CommandAttribute">command</see> by the same name that were incorrect.
    /// </summary>
    /// <value>The <see cref="CommandParamAttribute">arguments</see> of each <see cref="CommandAttribute">command</see> by the same name that were incorrect</value>
    public IList<AbstractCommandArgument> BadArguments { get; } = badArguments;
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