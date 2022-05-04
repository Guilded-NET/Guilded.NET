using Guilded.Base;
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
public class FailedCommandEvent : BaseObject
{
    /// <summary>
    /// Gets a command event that tried to invoke a command.
    /// </summary>
    /// <value>Command event</value>
    public CommandEvent Invokation { get; set; }
    /// <summary>
    /// Gets the type of the event that occurred.
    /// </summary>
    /// <value>Event type</value>
    public FallbackType Type { get; set; }
    /// <summary>
    /// Initializes a new instance of <see cref="FailedCommandEvent" />.
    /// </summary>
    /// <param name="commandEvent">A command event that tried to invoke a command</param>
    /// <param name="type">The type of the event that occurred</param>
    public FailedCommandEvent(CommandEvent commandEvent, FallbackType type) =>
        (Invokation, Type) = (commandEvent, type);
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