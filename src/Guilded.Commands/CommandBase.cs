using System.Collections.Generic;
using System.Reflection;
using Guilded.Commands.Items;

namespace Guilded.Commands;

/// <summary>
/// Represents the base for all <see cref="CommandAttribute">command types</see>.
/// </summary>
/// <example>
/// <para>The following shows an example of a command with a sub-command:</para>
/// <code language="csharp">
/// [Command]
/// public class Config : CommandBase
/// {
///     [Command]
///     public async Task Prefix(CommandEvent invokation, [CommandParam] string prefix)
///     {
///         // ...
///         await invokation.ReplyAsync($"Set prefix as '{prefix}'");
///     }
/// }
/// </code>
/// </example>
/// <seealso cref="CommandModule" />
/// <seealso cref="CommandAttribute" />
/// <seealso cref="CommandFallbackAttribute" />
public abstract class CommandBase : CommandParent
{
    /// <inheritdoc cref="ICommand{T}.Name" />
    public string Name => InstanceInfo.Name;

    /// <inheritdoc cref="ICommand{T}.Aliases" />
    public string[]? Aliases => InstanceInfo.Aliases;

    /// <inheritdoc cref="ICommand{T}.Description" />
    public string? Description => InstanceInfo.Description;

    /// <inheritdoc cref="ICommand{T}.Examples" />
    public IEnumerable<ExampleAttribute> Examples => InstanceInfo.Examples;

    /// <summary>
    /// Gets the information about the <see cref="CommandBase">command</see>.
    /// </summary>
    /// <value>Command info</value>
    /// <seealso cref="CommandBase" />
    /// <seealso cref="Examples" />
    /// <seealso cref="Description" />
    public CommandContainer InstanceInfo { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="CommandBase" />.
    /// </summary>
    /// <seealso cref="CommandBase" />
    public CommandBase() =>
        InstanceInfo = new CommandContainer(GetType(), GetType().GetCustomAttribute<CommandAttribute>()!, this);
}