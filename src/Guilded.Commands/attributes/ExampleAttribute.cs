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
    /// Gets the example of a <see cref="CommandAttribute">command's</see> usage.
    /// </summary>
    /// <value>The example of a <see cref="CommandAttribute">command's</see> usage</value>
    public string Content { get; }

    /// <summary>
    /// Gets the <see cref="CommandAttribute.Name">name</see> or the <see cref="CommandAttribute.Aliases">alias</see> of the <see cref="CommandAttribute">command</see> in the <see cref="ExampleAttribute">example</see>.
    /// </summary>
    /// <value>The <see cref="CommandAttribute.Name">name</see> or the <see cref="CommandAttribute.Aliases">alias</see> of the <see cref="CommandAttribute">command</see> in the <see cref="ExampleAttribute">example</see></value>
    public string? Name { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Defines an example for <see cref="CommandAttribute">a command</see>.
    /// </summary>
    /// <param name="content">The example of a <see cref="CommandAttribute">command's</see> usage</param>
    public ExampleAttribute(string content) =>
        Content = content;

    /// <summary>
    /// Defines an example for <see cref="CommandAttribute">a command</see>.
    /// </summary>
    /// <param name="name">The <see cref="CommandAttribute.Name">name</see> or the <see cref="CommandAttribute.Aliases">alias</see> of the <see cref="CommandAttribute">command</see> in the <see cref="ExampleAttribute">example</see></param>
    /// <param name="content">The example of a <see cref="CommandAttribute">command's</see> usage</param>
    public ExampleAttribute(string name, string content) : this(content) =>
        Name = name;
    #endregion
}