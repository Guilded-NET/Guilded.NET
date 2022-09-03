using System;
using System.Collections.Generic;
using System.Reflection;

namespace Guilded.Commands;

/// <summary>
/// The type of the <see cref="CommandArgument.Converter">command argument converter</see>.
/// </summary>
/// <param name="raw">The given unparsed value of the <see cref="CommandParamAttribute">argument</see></param>
/// <param name="value">The value of the <see cref="CommandParamAttribute">argument</see></param>
/// <returns>Whether the argument was parsed</returns>
public delegate bool ConverterDelegate(string raw, out object value);

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

    /// <summary>
    /// Gets the index of the <see cref="Command">command's</see> <see cref="AbstractCommandArgument">argument</see>.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="CommandEvent" /> parameter will be ignored, so this starts from <c>0</c>.</para>
    /// </remarks>
    /// <value>Index</value>
    public int Index { get; }

    /// <summary>
    /// Gets the <see cref="Command">command</see> where the <see cref="AbstractCommandArgument">argument</see> was declared.
    /// </summary>
    /// <value>Method command</value>
    public Command ParentCommand { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="AbstractCommandArgument" /> from a method <paramref name="parameter" />.
    /// </summary>
    /// <param name="index">The index of the parameter in a command </param>
    /// <param name="parameter">The <see cref="ParameterInfo">parameter</see> that was declared as a <see cref="CommandParamAttribute">command argument</see></param>
    /// <param name="command">The parent <see cref="CommandAttribute">command</see> of this <see cref="CommandAttribute">argument</see></param>
    protected AbstractCommandArgument(int index, ParameterInfo parameter, Command command) =>
        (Index, Attribute, Parameter, ParentCommand) = (
            index,
            parameter.GetCustomAttribute<CommandParamAttribute>(),
            parameter,
            command
        );
    #endregion

    #region Methods
    /// <summary>
    /// Gets the value for <see cref="AbstractCommandArgument">the argument</see> of <see cref="CommandEvent.Arguments">the provided invokation arguments</see> and current index.
    /// </summary>
    /// <param name="arguments"><see cref="CommandEvent.Arguments">The arguments</see> fetched from <see cref="CommandEvent">the command invokation</see></param>
    /// <param name="index">The index this argument is at</param>
    /// <param name="value">The converted value</param>
    /// <returns>Properly converted/parssed</returns>
    public abstract bool TryGetValueFrom(IEnumerable<string> arguments, int index, out object? value);
    #endregion
}