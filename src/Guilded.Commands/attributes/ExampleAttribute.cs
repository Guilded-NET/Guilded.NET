using System;

namespace Guilded.Commands;

/// <summary>
/// Defines an example for a command.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
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