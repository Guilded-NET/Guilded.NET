using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Guilded.Base;

namespace Guilded.Commands;

/// <summary>
/// Represents the information about methods that were declared as <see cref="CommandAttribute">commands</see>.
/// </summary>
public class Command : AbstractCommand<MethodInfo>
{
    #region Static
    private static readonly Type[] s_allowedTypes = new Type[]
    {
        typeof(string), typeof(bool), typeof(int), typeof(long),
        typeof(short), typeof(sbyte), typeof(uint), typeof(ulong),
        typeof(ushort), typeof(byte), typeof(float), typeof(double),
        typeof(decimal), typeof(char), typeof(DateTime), typeof(TimeSpan),
        typeof(Guid), typeof(HashId),
        typeof(string[]), typeof(Match), typeof(MatchCollection)
    };

    private static readonly Type[] s_allowedRestTypes = new Type[]
    {
        typeof(string[]), typeof(string)
    };
    #endregion

    #region Properties
    /// <summary>
    /// Gets the sequence of <see cref="CommandArgument">command argument</see> that can be specified by users.
    /// </summary>
    /// <value>Sequence of <see cref="CommandArgument">command argument</see></value>
    public CommandArgument[] Arguments { get; }

    /// <summary>
    /// Gets whether there is a <see cref="CommandArgument.IsRest">command rest argument</see> for the command.
    /// </summary>
    /// <value><see cref="CommandAttribute">Command</see> has <see cref="CommandArgument.IsRest">rest argument</see></value>
    public bool HasRestArgument { get; private set; }

    /// <summary>
    /// Gets the count of total mandatory <see cref="CommandArgument">command arguments</see>.
    /// </summary>
    /// <value>Count of required <see cref="CommandArgument">command arguments</see></value>
    public int RequiredCount { get; private set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="Command" /> from the command <paramref name="method" />.
    /// </summary>
    /// <param name="method">The method that was declared as a command</param>
    /// <param name="attribute">The command attribute it was given</param>
    /// <param name="parameters">The parameters that will be used as command arguments</param>
    public Command(MethodInfo method, CommandAttribute attribute, IEnumerable<ParameterInfo> parameters) : base(attribute, method)
    {
        HasRestArgument = false;

        bool defaultInList = false;

        Arguments = parameters.Select<ParameterInfo, CommandArgument>((arg, argIndex) =>
        {
            // Honestly, I don't know what I am doing. I don't want to do repetitive ifs,
            // especially since it can be a bit slower (see branchless programming). But it
            // still looks bad
            bool hasRestAttribute = arg.GetCustomAttribute<CommandRestAttribute>() is not null;

            if (arg.ParameterType == typeof(string[]) || arg.GetCustomAttribute<CommandRestAttribute>() is not null)
            {
                // We could probably do it backwards, but eh, better errors?
                if (!s_allowedRestTypes.Contains(arg.ParameterType)) throw new InvalidOperationException("You can only have string or string array types as command rest arguments.");
                else if (argIndex + 1 != parameters.Count()) throw new InvalidOperationException("String array or rest command argument can only be the last command parameters");
                else HasRestArgument = true;
            }

            Type? nullableType = Nullable.GetUnderlyingType(arg.ParameterType);

            bool isDefaultable = nullableType is not null || arg.HasDefaultValue;

            // Prevent (uint? x, uint y)
            if (defaultInList)
            {
                if (!isDefaultable) throw new InvalidOperationException("Command cannot contain a non-nullable argument after a nullable argument type");
            }
            else
            {
                // Whether to increment required count
                if (isDefaultable) defaultInList = true;
                else RequiredCount++;
            }

            // uint? and uint to be treated the same
            Type argumentType = nullableType ?? arg.ParameterType;

            if (!s_allowedTypes.Contains(argumentType))
                throw new InvalidOperationException($"Cannot have a command argument of type {arg.ParameterType}");

            return new CommandArgument(isDefaultable, argIndex, arg, argumentType, this);
        }).ToArray();
    }
    #endregion

    #region Methods
    internal bool HasCorrectCount(int count) =>
        HasRestArgument ? count >= RequiredCount : count >= RequiredCount && count <= Arguments.Length;

    internal bool GenerateMethodParameters(CommandConfiguration config, IEnumerable<string> arguments, [NotNullWhen(true)] out List<object?> parsed, [NotNullWhen(false)] out AbstractCommandArgument? badArgument)
    {
        parsed = new();
        badArgument = null;

        int i = 0;
        foreach (AbstractCommandArgument arg in Arguments)
        {
            // Find bad argument
            if (!arg.TryGetValueFrom(config, arguments.ElementAtOrDefault(i), out object? value))
            {
                badArgument = arg;
                return false;
            }

            // Add good argument
            parsed.Add(value);
            i++;
        }
        return true;
    }

    /// <summary>
    /// Invokes the command.
    /// </summary>
    /// <param name="parent">The parent command of this command</param>
    /// <param name="commandEvent">The command event that invoked the command</param>
    /// <param name="arguments">The arguments that have been used to invoke the command</param>
    /// <returns>Whether the command was properly invoked</returns>
    public Task InvokeAsync(CommandParent parent, CommandEvent commandEvent, IEnumerable<object?> arguments) =>
        Task.Run(() => Member.Invoke(parent, new object[] { commandEvent }.Concat(arguments.ToArray()).ToArray()));
    #endregion
}
/// <summary>
/// Represents the information about types that were declared as <see cref="CommandAttribute">commands</see>.
/// </summary>
public class CommandContainer : AbstractCommand<Type>
{
    #region Properties

    /// <summary>
    /// Gets the created instance of <see cref="CommandAttribute">the command</see> type for this command.
    /// </summary>
    /// <value>Command instance</value>
    public CommandParent Instance { get; }

#nullable disable
    /// <inheritdoc cref="CommandParent.Commands" />
    public IEnumerable<ICommand<MemberInfo>> SubCommands => Instance.Commands;
#nullable enable
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CommandContainer" /> from the command <paramref name="type" />.
    /// </summary>
    /// <param name="type">The type that was declared as a command</param>
    /// <param name="attribute">The command attribute it was given</param>
    /// <param name="instance">Other reflection members that were declared as commands</param>
    public CommandContainer(Type type, CommandAttribute attribute, CommandParent instance) : base(attribute, type) =>
        Instance = instance;
    #endregion
}