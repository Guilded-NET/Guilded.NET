using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Guilded.Base;

namespace Guilded.Commands;

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
    #region Static
    // FIXME: Cast int.Parse, bool.Parse, etc. to Func<string, object> somehow or something like that
    // I hate this
    internal static readonly Dictionary<Type, ConverterDelegate> _converters =
        new()
        {
            { typeof(string),   (string x, out object y) => { y = x; return true; } },
            { typeof(int),      FromParser<int>(int.TryParse) },
            { typeof(bool),     FromParser<bool>(bool.TryParse) },
            { typeof(Guid),     FromParser<Guid>(Guid.TryParse) },
            { typeof(HashId),   FromParser<HashId>(HashId.TryParse) },
            { typeof(uint),     FromParser<uint>(uint.TryParse) },
            { typeof(long),     FromParser<long>(long.TryParse) },
            { typeof(ulong),    FromParser<ulong>(ulong.TryParse) },
            { typeof(float),    FromParser<float>(float.TryParse) },
            { typeof(short),    FromParser<short>(short.TryParse) },
            { typeof(ushort),   FromParser<ushort>(ushort.TryParse) },
            { typeof(byte),     FromParser<byte>(byte.TryParse) },
            { typeof(sbyte),    FromParser<sbyte>(sbyte.TryParse) },
            { typeof(double),   FromParser<double>(double.TryParse) },
            { typeof(decimal),  FromParser<decimal>(decimal.TryParse) },
            { typeof(char),     FromParser<char>(char.TryParse) },
            { typeof(DateTime), FromParser<DateTime>(DateTime.TryParse) },
            { typeof(TimeSpan), FromParser<TimeSpan>(TimeSpan.TryParse) }
        };
    #endregion

    #region Properties
    /// <summary>
    /// Gets the converter to convert string values to the <see cref="AbstractCommandArgument.ArgumentType">argument's type</see>.
    /// </summary>
    /// <value>String to Object Converter</value>
    internal ConverterDelegate Converter { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CommandArgument" /> from a <paramref name="parameter">method parameter</paramref>.
    /// </summary>
    /// <param name="index">The index of the parameter in a command </param>
    /// <param name="parameter">The <see cref="ParameterInfo">parameter</see> that was declared as a <see cref="CommandParamAttribute">command argument</see></param>
    /// <param name="command">The parent <see cref="CommandAttribute">command</see> of this <see cref="CommandAttribute">argument</see></param>
    public CommandArgument(int index, ParameterInfo parameter, Command command) : base(index, parameter, command) =>
        Converter = GetParametersParser(parameter.ParameterType);
    #endregion

    #region Methods
    /// <inheritdoc />
    public override bool TryGetValueFrom(IEnumerable<string> arguments, int index, out object? value) =>
        Converter(arguments.ElementAt(index), out value);
    #endregion

    #region Static methods
    internal static ConverterDelegate GetParametersParser(Type parameterType)
    {
        if (!_converters.ContainsKey(parameterType))
            throw new FormatException($"Cannot have type {parameterType} as a command argument's type");

        return _converters[parameterType];
    }

    private static ConverterDelegate FromParser<T>(ConverterParser<T> parser)
    {
        return (string raw, out object value) =>
        {
            bool good = parser(raw, out var parsed);
            value = parsed!;
            return good;
        };
    }
    #endregion
}

/// <summary>
/// Represents the information about one-value command argument in <see name="CommandInfo">a command method</see>.
/// </summary>
public class CommandOptionalArgument : AbstractCommandArgument
{
    #region Properties
    /// <summary>
    /// Gets the converter to convert string values to the <see cref="AbstractCommandArgument.ArgumentType">argument's type</see>.
    /// </summary>
    /// <value>String to Object Converter</value>
    internal ConverterDelegate Converter { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CommandArgument" /> from a <paramref name="parameter">method parameter</paramref>.
    /// </summary>
    /// <param name="index">The index of the parameter in a command </param>
    /// <param name="parameter">The <see cref="ParameterInfo">parameter</see> that was declared as a <see cref="CommandParamAttribute">command argument</see></param>
    /// <param name="type"></param>
    /// <param name="command">The parent <see cref="CommandAttribute">command</see> of this <see cref="CommandAttribute">argument</see></param>
    public CommandOptionalArgument(int index, ParameterInfo parameter, Type type, Command command) : base(index, parameter, command) =>
        Converter = CommandArgument.GetParametersParser(type);
    #endregion

    #region Methods
    /// <inheritdoc />
    public override bool TryGetValueFrom(IEnumerable<string> arguments, int index, out object? value)
    {
        // Convert properly
        if (arguments.Count() > index) return Converter(arguments.ElementAt(index), out value);
        // = xyz or `null`
        // This could be minimized to just .Value, but at this point maybe RAM would suffer and it's
        // obsession over micro-optimizations
        // need to check RAM usage of the commands first
        value = Parameter.HasDefaultValue ? Parameter.DefaultValue : null;
        return true;
    }
    #endregion
}

/// <summary>
/// Represents the information about array command argument in <see name="CommandInfo">a command method</see>.
/// </summary>
public class CommandRest : AbstractCommandArgument
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CommandRest" /> from a <paramref name="parameter">method parameter</paramref>.
    /// </summary>
    /// <param name="index">The index of the parameter in a command </param>
    /// <param name="parameter">The <see cref="ParameterInfo">parameter</see> that was declared as a <see cref="CommandParamAttribute">command argument</see></param>
    /// <param name="command">The parent <see cref="CommandAttribute">command</see> of this <see cref="CommandAttribute">argument</see></param>
    public CommandRest(int index, ParameterInfo parameter, Command command) : base(index, parameter, command) { }
    #endregion

    #region Method
    /// <inheritdoc />
    public override bool TryGetValueFrom(IEnumerable<string> arguments, int index, out object value)
    {
        value = arguments.Skip(index).ToArray();
        return true;
    }
    #endregion
}