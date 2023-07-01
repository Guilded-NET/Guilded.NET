using System;

namespace Guilded.Commands;

/// <summary>
/// Declares a method or a type as a <see cref="Items.Command">command</see>.
/// </summary>
/// <remarks>
/// <para>By default, all the parameters will be seen as context parameters, unless a parameter has <see cref="CommandParamAttribute" />. Parameters with <see cref="CommandParamAttribute" /> will be declared as command's parameters.</para>
/// </remarks>
/// <example>
/// <para>Example of a command with its own subcommands.</para>
/// <para>This results in a command <c>config</c> with sub-command <c>prefix</c>. <c>prefix</c> command will not be invokable without any arguments.</para>
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
/// <para>Writing <q>!config prefix ?</q> will result in bot responding with <q>Set server's prefix to <c>?</c></q> message.</para>
/// </example>
/// <seealso cref="CommandParamAttribute" />
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Module | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class CommandAttribute : Attribute
{
    #region Properties
    /// <summary>
    /// Gets the overriden name of the <see cref="Items.Command">command</see>.
    /// </summary>
    /// <remarks>
    /// <para>By default, lowercase name of the method will be used. <c>Async</c> and <c>Command</c> will also be trimmed from the end if the name comes from the method.</para>
    /// </remarks>
    /// <value>The overriden name of the <see cref="Items.Command">command</see></value>
    public string? Name { get; set; }

    /// <summary>
    /// Gets the alternative names of the <see cref="Items.Command">command</see>.
    /// </summary>
    /// <value>The alternative names of the <see cref="Items.Command">command</see></value>
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