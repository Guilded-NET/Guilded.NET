using System;
using System.Diagnostics.CodeAnalysis;
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
/// The function to parse provided <see cref="CommandParamAttribute">command argument</see> values.
/// </summary>
/// <param name="commandArg">The <see cref="CommandParamAttribute">command argument</see> that is being parsed</param>
/// <param name="config">The given <see cref="CommandConfiguration">configuration</see> for the <see cref="CommandAttribute">commands</see></param>
/// <param name="argument">The given unparsed <see cref="CommandParamAttribute">command argument</see></param>
/// <param name="value">The parsed value of the <see cref="CommandParamAttribute">command argument</see></param>
/// <returns>Whether the <see cref="CommandParamAttribute">command argument</see> was parsed correctly</returns>
public delegate bool ParserDelegate(CommandArgument commandArg, CommandConfiguration config, string? argument, out object? value);

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
    public ParserDelegate Parser { get; }

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
    public override bool TryGetValueFrom(CommandConfiguration config, string? argument, out object? value) =>
        Parser(this, config, argument, out value);
    #endregion

    #region Static methods
    internal static ParserDelegate GetParser(bool isOptional) =>
        isOptional
        ? (CommandArgument commandArg, CommandConfiguration config, string? argument, out object? value) =>
            {
                // Convert properly
                if (argument is not null) return config.ArgumentConverters[commandArg.ArgumentType](commandArg, config, argument, out value);
                // = xyz or `null`
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
    : (CommandArgument commandArg, CommandConfiguration config, string? argument, out object? value) =>
        config.ArgumentConverters[commandArg.ArgumentType](commandArg, config, argument!, out value);

    internal static ArgumentConverter FromParser<T>(ConverterParser<T> parser)
    {
        return (CommandArgument _, CommandConfiguration _, string raw, [NotNullWhen(true)] out object? value) =>
        {
            bool good = parser(raw, out var parsed);
            value = parsed!;
            return good;
        };
    }
    #endregion
}