using System;

namespace Guilded.Commands;

/// <summary>
/// Defines a description for a command.
/// </summary>
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
    /// Defines a description for <see cref="CommandAttribute">a command</see> with <paramref name="text">the provided text</paramref>.
    /// </summary>
    /// <param name="text">The description of <see cref="CommandAttribute">command</see></param>
    public DescriptionAttribute(string text) =>
        Text = text;
    #endregion
}