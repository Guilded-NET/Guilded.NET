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
    /// Gets the overriden name of the command.
    /// </summary>
    /// <remarks>
    /// <para>By default, lowercase name of the method will be used.</para>
    /// </remarks>
    /// <value>Name?</value>
    public string? Name { get; set; }
    /// <summary>
    /// Gets the alternative names of the command.
    /// </summary>
    /// <value>Array of names?</value>
    public string[]? Aliases { get; set; }
    /// <summary>
    /// Gets the description of the command.
    /// </summary>
    /// <remarks>
    /// <para>This is not used anywhere, so you can use it for command lists.</para>
    /// </remarks>
    /// <value>Text?</value>
    public string? Description { get; set; }
    /// <summary>
    /// Gets the examples of how to use the command.
    /// </summary>
    /// <remarks>
    /// <para>This is not used anywhere, so you can use it for command lists.</para>
    /// </remarks>
    /// <value>Array of text?</value>
    public string[]? Examples { get; set; }
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