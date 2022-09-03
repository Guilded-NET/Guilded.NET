using System;

namespace Guilded.Commands;

/// <summary>
/// Defines a description for a command.
/// </summary>
/// <example>
/// <para>The code below demonstrates a command with a description in C#11 preview:</para>
/// <code language="csharp">
/// [Description(
///     """
///     Makes the bot respond with `Pong!`
///     There isn't much else to it.
///     """
/// )]
/// [Command]
/// public async Task Ping(CommandEvent invokation) =>
///     await invokation.ReplyAsync("Pong!");
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class DescriptionAttribute : Attribute
{
    #region Properties
    /// <summary>
    /// Gets the text that represents <see cref="CommandAttribute">command's</see> description.
    /// </summary>
    /// <value>Text</value>
    public string Text { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Defines a description for a <see cref="CommandAttribute">command</see> with the provided <paramref name="text" />.
    /// </summary>
    /// <param name="text">The description of <see cref="CommandAttribute">command</see></param>
    public DescriptionAttribute(string text) =>
        Text = text;
    #endregion
}