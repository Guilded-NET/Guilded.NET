using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Guilded.Commands.Items;

/// <summary>
/// The <see cref="int.TryParse(string?, out int)" /> of various <see cref="Type">types</see>.
/// </summary>
/// <param name="raw">A string to convert</param>
/// <param name="value">The results of the parsing</param>
/// <typeparam name="T">The type of the output</typeparam>
/// <returns>Whether it was parsed</returns>
public delegate bool ConverterParser<T>([NotNullWhen(true)] string? raw, out T value);

/// <summary>
/// Represents the information about one-value command argument in <see name="CommandInfo">a command method</see>.
/// </summary>
public class CommandArgument : AbstractCommandArgument
{
    #region Properties
    /// <summary>
    /// Gets whether the value for the <see cref="CommandParamAttribute">command argument</see> does not need to be provided.
    /// </summary>
    /// <value>Whether the <see cref="CommandParamAttribute">command argument</see> value is optional</value>
    public bool IsOptional { get; }

    /// <summary>
    /// Gets the function to parse <see cref="CommandArgument">command argument's</see> provided values.
    /// </summary>
    /// <value><see cref="CommandArgument">Command argument</see> value parser</value>
    public ArgumentConverter Parser { get; }

    /// <summary>
    /// Gets whether the <see cref="CommandParamAttribute">command argument</see> takes in spaces and other splittable characters.
    /// </summary>
    /// <returns>Whether the <see cref="CommandParamAttribute">command argument</see> is rest argument</returns>
    public bool IsRest => ArgumentType == typeof(string[]) || Parameter.GetCustomAttribute<CommandRestAttribute>() is not null;

    /// <summary>
    /// Gets the <see cref="Type">type</see> of the <see cref="CommandParamAttribute">command argument</see>.
    /// </summary>
    /// <value>The <see cref="Type">type</see> of the <see cref="CommandParamAttribute">command argument</see></value>
    public Type ArgumentType { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CommandArgument" /> from a method <paramref name="parameter" />.
    /// </summary>
    /// <param name="isOptional">Whether the argument is optional</param>
    /// <param name="index">The index of the parameter in a <see cref="CommandAttribute">command</see></param>
    /// <param name="parameter">The <see cref="ParameterInfo">parameter</see> that was declared as a <see cref="CommandParamAttribute">command argument</see></param>
    /// <param name="argumentType">The type of the <see cref="CommandParamAttribute">command argument</see> <see cref="Type">type</see></param>
    /// <param name="command">The parent <see cref="CommandAttribute">command</see> of this <see cref="CommandAttribute">argument</see></param>
    public CommandArgument(bool isOptional, int index, ParameterInfo parameter, Type argumentType, Command command) : base(index, parameter, command) =>
        (IsOptional, Parser, ArgumentType) = (isOptional, GetParser(isOptional), argumentType);
    #endregion

    #region Methods
    /// <inheritdoc />
    public override bool TryGetValueFrom(RootCommandEvent rootInvokation, ref string argument, out object? value) =>
        Parser(this, rootInvokation, ref argument, out value);
    #endregion

    #region Static methods
    internal static ArgumentConverter GetParser(bool isOptional) =>
        isOptional
        ? (CommandArgument commandArg, RootCommandEvent rootInvokation, ref string argument, out object? value) =>
            {
                // Convert properly
                if (!string.IsNullOrEmpty(argument)) return rootInvokation.Configuration.ArgumentConverters[commandArg.ArgumentType](commandArg, rootInvokation, ref argument, out value);
                // `= xyz` or `null`
                // This could be minimized to just .Value, but at this point maybe RAM would suffer and it's
                // obsession over micro-optimizations
                // need to check RAM usage of the commands first
                value =
                    commandArg.Parameter.HasDefaultValue
                    ? commandArg.Parameter.DefaultValue
                    : commandArg.ArgumentType == typeof(string[])
                    ? Array.Empty<string>()
                    : null;
                return true;
            }
    : (CommandArgument commandArg, RootCommandEvent rootInvokation, ref string argument, out object? value) =>
        rootInvokation.Configuration.ArgumentConverters[commandArg.ArgumentType](commandArg, rootInvokation, ref argument, out value);

    internal static ArgumentConverter FromParser<T>(ConverterParser<T> parser)
    {
        return (CommandArgument _, RootCommandEvent rootInvokation, ref string arguments, out object? value) =>
        {
            string[] split = arguments.Split(rootInvokation.Configuration.Separators, 2, rootInvokation.Configuration.SplitOptions);

            bool good = parser(split.First(), out var parsed);
            value = parsed!;

            arguments = split.ElementAtOrDefault(1) ?? string.Empty;

            return good;
        };
    }
    #endregion
}