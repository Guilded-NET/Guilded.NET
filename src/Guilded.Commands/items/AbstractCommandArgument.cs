using System;
using System.Collections.Generic;
using System.Reflection;

namespace Guilded.Commands;

/// <summary>
/// Represents the information about any command argument in <see name="CommandInfo">a command method</see>.
/// </summary>
public abstract class AbstractCommandArgument
{
    #region Properties
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
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="AbstractCommandArgument" /> from a <paramref name="parameter">method parameter</paramref>.
    /// </summary>
    /// <param name="parameter">The parameter that was declared as a command parameter</param>
    protected AbstractCommandArgument(ParameterInfo parameter) =>
        (Attribute, Parameter) = (
            parameter.GetCustomAttribute<CommandParamAttribute>(),
            parameter
        );
    #endregion

    #region Methods
    /// <summary>
    /// Gets the value for <see cref="AbstractCommandArgument">the argument</see> of <see cref="CommandEvent.Arguments">the provided invokation arguments</see> and current index.
    /// </summary>
    /// <param name="arguments"><see cref="CommandEvent.Arguments">The arguments</see> fetched from <see cref="CommandEvent">the command invokation</see></param>
    /// <param name="index">The index this argument is at</param>
    /// <returns>Value</returns>
    public abstract object? GetValueFrom(IEnumerable<string> arguments, int index);
    #endregion
}