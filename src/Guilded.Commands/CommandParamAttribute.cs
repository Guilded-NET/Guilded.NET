using System;

namespace Guilded.Commands;

/// <summary>
/// Declares a parameter as a <see cref="CommandAttribute">command's</see> parameter.
/// </summary>
/// <remarks>
/// <para>Any parameter that does not have this parameter will be seen as a context parameter.</para>
/// </remarks>
/// <seealso cref="CommandAttribute" />
[AttributeUsage(AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
public class CommandParamAttribute : Attribute
{
    /// <summary>
    /// Gets the displayed name of the parameter.
    /// </summary>
    /// <remarks>
    /// <para>By default, the name of the parameter is used.</para>
    /// <para>This is not used anywhere for now, so you can use it for help commands or usage.</para>
    /// </remarks>
    /// <value>Display name?</value>
    public string? DisplayName { get; set; }
    /// <summary>
    /// Gets whether the parameter is optional.
    /// </summary>
    /// <remarks>
    /// <para>Declares the argument as optional. By default, it looks whether the parameter is nullable.</para>
    /// </remarks>
    /// <value>Optional</value>
    public bool? Optional { get; set; }
    /// <summary>
    /// Declares a command parameter with the display name as the parameter's name.
    /// </summary>
    public CommandParamAttribute() { }
    /// <summary>
    /// Declares a command parameter with the specified <paramref name="name">name</paramref>.
    /// </summary>
    /// <param name="name">The displayed name of the parameter</param>
    public CommandParamAttribute(string name) =>
        DisplayName = name;
}