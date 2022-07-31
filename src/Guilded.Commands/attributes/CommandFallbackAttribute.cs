using System;

namespace Guilded.Commands;

/// <summary>
/// Declares a method as a <see cref="CommandParent.FailedCommand">failed command</see> handler.
/// </summary>
/// <example>
/// <para>The code below demonstrates a command with unknown sub-command handling.</para>
/// <code language="csharp">
/// [Command]
/// public class Config
/// {
///     [CommandFallback(FallbackType.NoCommandFound)]
///     public async Task UnknownCommand(CommandEvent invokation) =>
///         await invokation.ReplyAsync($"Unknown sub-command `{invokation.CommandName}`");
/// }
/// </code>
/// </example>
/// <seealso cref="FallbackType" />
/// <seealso cref="CommandAttribute" />
/// <seealso cref="CommandParamAttribute" />
[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public sealed class CommandFallbackAttribute : Attribute
{
    #region Properties
    /// <summary>
    /// Gets the type of <see cref="CommandParent.FailedCommand">failed command</see> event to handle.
    /// </summary>
    /// <value>Failed command type</value>
    public FallbackType Type { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Declares a method as a failed command handler based on <paramref name="type">the given type</paramref>.
    /// </summary>
    /// <param name="type">The type of <see cref="CommandParent.FailedCommand">failed command</see> event to handle</param>
    public CommandFallbackAttribute(FallbackType type) =>
        Type = type;
    #endregion
}