using System;

namespace Guilded.Commands;

/// <summary>
/// Declares a string parameter as a <see cref="CommandAttribute">command's</see> rest argument.
/// </summary>
/// <remarks>
/// <para>This will get everything, including spaces, as an argument.</para>
/// </remarks>
/// <example>
/// <para>The following is an example of <see cref="CommandRestAttribute" /> usage:</para>
/// <code language="csharp">
/// [Command]
/// public async Task Say(CommandEvent invokation, [CommandParam("text to say"), CommandRest] string text) =>
///     await invokation.ReplyAsync(text);
/// </code>
/// </example>
/// <seealso cref="CommandAttribute" />
[AttributeUsage(AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
public class CommandRestAttribute : Attribute
{
    #region Constructors
    /// <summary>
    /// Declares a string parameter as a <see cref="CommandAttribute">command's</see> rest argument.
    /// </summary>
    public CommandRestAttribute() { }
    #endregion
}