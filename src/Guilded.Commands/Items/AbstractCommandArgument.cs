using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Guilded.Commands.Items;

/// <summary>
/// The type of the <see cref="CommandConfiguration.ArgumentConverters">command argument converter</see>.
/// </summary>
/// <param name="argument">The <see cref="CommandParamAttribute">command argument</see> that is being used for conversion</param>
/// <param name="config">The given <see cref="CommandConfiguration">configuration</see> for the <see cref="CommandAttribute">commands</see></param>
/// <param name="raw">The given unparsed value of the <see cref="CommandParamAttribute">argument</see></param>
/// <param name="value">The value of the <see cref="CommandParamAttribute">argument</see></param>
/// <returns>Whether the argument was parsed</returns>
public delegate bool ArgumentConverter(CommandArgument argument, CommandConfiguration config, string raw, [NotNullWhen(true)] out object? value);

/// <summary>
/// Represents the information about any command argument in <see name="CommandInfo">a command method</see>.
/// </summary>
public abstract class AbstractCommandArgument
{
    #region Properties
    /// <summary>
    /// Gets the parameter that was declared in the <see cref="AbstractCommand{T}.Member">command method</see>.
    /// </summary>
    /// <value>The parameter that was declared in the <see cref="AbstractCommand{T}.Member">command method</see></value>
    public ParameterInfo Parameter { get; set; }

    /// <summary>
    /// Gets the <see cref="CommandParamAttribute">attribute</see> that was used to declare a <see cref="CommandAttribute">command's</see> parameter.
    /// </summary>
    /// <value>The <see cref="CommandParamAttribute">attribute</see> that was used to declare a <see cref="CommandAttribute">command's</see> parameter</value>
    public CommandParamAttribute? Attribute { get; set; }

    /// <summary>
    /// Gets the displayed <see cref="CommandParamAttribute.Name">name</see> of the <see cref="CommandParamAttribute">command argument</see>.
    /// </summary>
    /// <value>The displayed <see cref="CommandParamAttribute.Name">name</see> of the <see cref="CommandParamAttribute">command argument</see></value>
    public string Name => Attribute?.Name ?? Parameter.Name ?? "";

    /// <summary>
    /// Gets the index of the <see cref="Command">command's</see> <see cref="AbstractCommandArgument">command argument</see>.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="CommandEvent" /> parameter will be ignored, so this starts from <c>0</c>.</para>
    /// </remarks>
    /// <value>The index of the <see cref="Command">command's</see> <see cref="AbstractCommandArgument">command argument</see></value>
    public int Index { get; }

    /// <summary>
    /// Gets the <see cref="Command">command</see> where the <see cref="AbstractCommandArgument">command argument</see> was declared.
    /// </summary>
    /// <value>The <see cref="Command">command</see> where the <see cref="AbstractCommandArgument">command argument</see> was declared</value>
    public Command ParentCommand { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="AbstractCommandArgument" /> from a method <paramref name="parameter" />.
    /// </summary>
    /// <param name="index">The index of the <see cref="Command">command's</see> <see cref="AbstractCommandArgument">command argument</see></param>
    /// <param name="parameter">The <see cref="Command">command</see> where the <see cref="AbstractCommandArgument">command argument</see> was declared</param>
    /// <param name="command">The <see cref="Command">command</see> where the <see cref="AbstractCommandArgument">command argument</see> was declared</param>
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
    /// Sets the converted <paramref name="value" /> and returns whether it was successful in setting the <paramref name="value" />.
    /// </summary>
    /// <param name="configuration">The <see cref="CommandConfiguration">configuration</see> used for <see cref="CommandModule">commands</see></param>
    /// <param name="argument">The convertable <see cref="CommandEvent.Arguments">argument</see> fetched from the <see cref="CommandEvent">command invokation</see></param>
    /// <param name="value">The converted value</param>
    /// <returns>Whether it was successful in setting the <paramref name="value" /></returns>
    public abstract bool TryGetValueFrom(CommandConfiguration configuration, string? argument, out object? value);
    #endregion
}