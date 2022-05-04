using System;

namespace Guilded.Commands;

/// <summary>
/// Declares a method as a <see cref="CommandBase.FailedCommand">failed command</see> handler.
/// </summary>
[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public sealed class CommandFallbackAttribute : Attribute
{
    /// <summary>
    /// Gets the type of <see cref="CommandBase.FailedCommand">failed command</see> event to handle.
    /// </summary>
    /// <value>Failed command type</value>
    public FallbackType Type { get; set; }
    /// <summary>
    /// Declares a method as a failed command handler based on <paramref name="type">the given type</paramref>.
    /// </summary>
    /// <param name="type">The type of <see cref="CommandBase.FailedCommand">failed command</see> event to handle</param>
    public CommandFallbackAttribute(FallbackType type) =>
        Type = type;
}