using System;
using System.Text.RegularExpressions;
using Guilded.Base;

namespace Guilded.Commands;

/// <summary>
/// Declares a parameter as a <see cref="CommandAttribute">command's</see> parameter.
/// </summary>
/// <remarks>
/// <para>Currently available parameter types are:</para>
/// <list type="bullet">
///     <item><see cref="string" /></item>
///     <item><see cref="HashId" /></item>
///     <item><see cref="Guid" /></item>
///     <item><see cref="char" /></item>
///     <item><see cref="bool" /></item>
///     <item><see cref="int" /></item>
///     <item><see cref="uint" /></item>
///     <item><see cref="long" /></item>
///     <item><see cref="ulong" /></item>
///     <item><see cref="short" /></item>
///     <item><see cref="ushort" /></item>
///     <item><see cref="byte" /></item>
///     <item><see cref="sbyte" /></item>
///     <item><see cref="float" /></item>
///     <item><see cref="double" /></item>
///     <item><see cref="decimal" /></item>
///     <item><see cref="DateTime" /></item>
///     <item><see cref="TimeSpan" /></item>
///     <item><see cref="Match" /></item>
///     <item><see cref="MatchCollection" /></item>
/// </list>
/// <para><see cref="string" /> <see cref="Array" /> is also available, but it will be seen as a rest argument.</para>
/// </remarks>
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
/// public async Task Say(CommandEvent invokation, [CommandParam("word to say")] string word) =>
///     await invokation.ReplyAsync(word);
/// </code>
/// </example>
/// <seealso cref="CommandAttribute" />
[AttributeUsage(AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
public class CommandParamAttribute : Attribute
{
    #region Properties
    /// <summary>
    /// Gets the displayed name of the <see cref="CommandParamAttribute">parameter</see>.
    /// </summary>
    /// <remarks>
    /// <para>By default, the name of the parameter is used.</para>
    /// <para>This is not used anywhere, so you can use it for <see cref="CommandParent.Commands">command lists</see>.</para>
    /// </remarks>
    /// <value>The displayed name of the <see cref="CommandParamAttribute">parameter</see></value>
    public string? Name { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Declares a <see cref="CommandParamAttribute">command parameter</see> with the display name as the parameter's name.
    /// </summary>
    public CommandParamAttribute() { }

    /// <summary>
    /// Declares a <see cref="CommandParamAttribute">command parameter</see> with the specified <paramref name="name" />.
    /// </summary>
    /// <param name="name">The displayed name of the <see cref="CommandParamAttribute">parameter</see></param>
    public CommandParamAttribute(string name) =>
        Name = name;
    #endregion
}