using System;
using System.Reflection;

namespace Guilded.Commands;

/// <summary>
/// Represents the information about command argument in <see name="CommandInfo">a command method</see>.
/// </summary>
public class CommandArgumentInfo
{
    /// <summary>
    /// Gets the type of <see cref="Parameter">the parameter</see>.
    /// </summary>
    /// <value>Type</value>
    public Type ArgumentType => Parameter.ParameterType;
    /// <summary>
    /// Gets the parameter that was declared in the method.
    /// </summary>
    /// <value>Reflection parameter</value>
    public ParameterInfo Parameter { get; set; }
    /// <summary>
    /// Gets <see cref="CommandParamAttribute">the attribute</see> that was used to declare <see cref="CommandAttribute">the command parameter</see>.
    /// </summary>
    /// <value>Command param attribute</value>
    public CommandParamAttribute? Attribute { get; set; }
    /// <summary>
    /// Gets the displayed name of <see cref="CommandParamAttribute.Name">the command argument</see>.
    /// </summary>
    /// <value>Name</value>
    public string Name => Attribute?.Name ?? Parameter.Name ?? "";
    /// <summary>
    /// Initializes a new instance of <see cref="CommandArgumentInfo" /> from a <paramref name="parameter">method parameter</paramref>.
    /// </summary>
    /// <param name="parameter">The parameter that was declared as a command parameter</param>
    public CommandArgumentInfo(ParameterInfo parameter) =>
        (Attribute, Parameter) = (
            parameter.GetCustomAttribute<CommandParamAttribute>(),
            parameter
        );
}