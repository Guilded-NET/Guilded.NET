using System;

namespace Guilded.Commands;

/// <summary>
/// Declares a method as a command.
/// </summary>
/// <remarks>
/// <para>By default, all the parameters will be seen as context parameters, unless a parameter has <see cref="CommandParamAttribute" />. Parameters with <see cref="CommandParamAttribute" /> will be declared as command's parameters.</para>
/// </remarks>
/// <example>
/// <para>Example of a command with its own subcommands:</para>
/// <code lang="csharp">
/// [Command]
/// public static class ConfigCommand
/// {
///     [Command]
///     public static async Task Prefix(CommandEvent invokation, [CommandParam] string prefix)
///     {
///         await invokation.ReplyAsync($"Set server's prefix to `{prefix}`");
///         // ...
///     }
/// }
/// </code>
/// </example>
/// <seealso cref="CommandParamAttribute" />
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Module | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class CommandAttribute : Attribute
{
    /// <summary>
    /// The name of the command.
    /// </summary>
    /// <remarks>
    /// <para>Overrides the name of the command. By default, lowercase name of the method will be used.</para>
    /// </remarks>
    /// <value>Name?</value>
    public string? Name { get; set; }
    /// <summary>
    /// The alternative names of the command.
    /// </summary>
    /// <remarks>
    /// <para>The aliases of the commands that can be used as the alternative names of the command.</para>
    /// </remarks>
    /// <value>Aliases?</value>
    public string[]? Aliases { get; set; }
    /// <summary>
    /// Declares a method as a command with no aliases.
    /// </summary>
    public CommandAttribute() { }
    /// <summary>
    /// Declares a method as a command with given <paramref name="name" />
    /// </summary>
    /// <param name="name">The overriden name of the command</param>
    public CommandAttribute(string name) =>
        Name = name;
}