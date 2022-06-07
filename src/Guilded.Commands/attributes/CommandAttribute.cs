using System;

namespace Guilded.Commands;

/// <summary>
/// Declares a method or a type as a command.
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
    #region Properties
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
    #endregion

    #region Constructors
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
    #endregion
}