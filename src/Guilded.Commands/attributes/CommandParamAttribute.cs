using System;

namespace Guilded.Commands;

/// <summary>
/// Declares a parameter as a <see cref="CommandAttribute">command's</see> parameter.
/// </summary>
/// <example>
/// <para>Here's an example of a parameter being declared as a command parameter:</para>
/// <code language="csharp">
/// [Command(Aliases = new string[] { "plus" })]
/// public async Task Add(CommandEvent invokation, [CommandParam] int x, [CommandParam] int y) =>
///     await invokation.ReplyAsync($"{x} + {y} = {x + y}");
/// </code>
/// <para>The following showcases a command parameter with an overriden name:</para>
/// <code language="csharp">
/// [Command]
/// public async Task Say(CommandEvent invokation, [CommandParam("text to say")] params string[] args) =>
///     await invokation.ReplyAsync(string.Join(" ", args));
/// </code>
/// </example>
/// <seealso cref="CommandAttribute" />
[AttributeUsage(AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
public class CommandParamAttribute : Attribute
{
    #region Properties
    /// <summary>
    /// Gets the displayed name of the parameter.
    /// </summary>
    /// <remarks>
    /// <para>By default, the name of the parameter is used.</para>
    /// <para>This is not used anywhere, so you can use it for command lists.</para>
    /// </remarks>
    /// <value>Name?</value>
    public string? Name { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Declares a command parameter with the display name as the parameter's name.
    /// </summary>
    public CommandParamAttribute() { }

    /// <summary>
    /// Declares a command parameter with the specified <paramref name="name">name</paramref>.
    /// </summary>
    /// <param name="name">The displayed name of the parameter</param>
    public CommandParamAttribute(string name) =>
        Name = name;
    #endregion
}