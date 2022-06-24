using System;

namespace Guilded.Commands;

/// <summary>
/// Defines an example for a command.
/// </summary>
/// <example>
/// <para>The following example demonstrates a command with 3 examples attached to it. The last command example uses the command's alias <c>plus</c>.</para>
/// <code language="csharp">
/// [Command(Aliases = new string[] { "plus" })]
/// [Example("2 + 2")]
/// [Example("-2 + 5")]
/// [Example("plus", "-2 + 5")]
/// public async Task Add(CommandEvent invokation, [CommandParam] int x, [CommandParam] int y) =>
///     await invokation.ReplyAsync($"{x} + {y} = {x + y}");
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public sealed class ExampleAttribute : Attribute
{
    #region Properties
    /// <summary>
    /// Gets the example of <see cref="CommandAttribute">command's</see> usage.
    /// </summary>
    /// <value>Markdown string</value>
    public string Content { get; }

    /// <summary>
    /// Gets <see cref="CommandAttribute.Name">the name</see> or <see cref="CommandAttribute.Aliases">the alias</see> of <see cref="CommandAttribute">the command</see> in the example.
    /// </summary>
    /// <value>Name</value>
    public string? Name { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Defines an example for <see cref="CommandAttribute">a command</see>.
    /// </summary>
    /// <param name="content">The description of <see cref="CommandAttribute">command</see></param>
    public ExampleAttribute(string content) =>
        Content = content;

    /// <summary>
    /// Defines an example for <see cref="CommandAttribute">a command</see>.
    /// </summary>
    /// <param name="name"><see cref="CommandAttribute.Name">The name</see> or <see cref="CommandAttribute.Aliases">the alias</see> of <see cref="CommandAttribute">the command</see> in the example</param>
    /// <param name="content">The description of <see cref="CommandAttribute">command</see></param>
    public ExampleAttribute(string name, string content) : this(content) =>
        Name = name;
    #endregion
}