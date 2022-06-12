using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Guilded.Base;

namespace Guilded.Commands;

/// <summary>
/// Represents the information about methods that were declared as <see cref="CommandAttribute">commands</see>.
/// </summary>
public class CommandInfo : AbstractCommandInfo<MethodInfo>
{
    #region Static
    private static readonly Type[] s_allowedTypes = new Type[]
    {
        typeof(string), typeof(bool), typeof(int), typeof(long),
        typeof(short), typeof(sbyte), typeof(uint), typeof(ulong),
        typeof(ushort), typeof(byte), typeof(float), typeof(double),
        typeof(decimal), typeof(char), typeof(DateTime), typeof(TimeSpan),
        typeof(Guid), typeof(HashId)
    };
    #endregion

    #region Properties
    /// <summary>
    /// Gets the enumerable of command arguments that can be specified by users.
    /// </summary>
    /// <value>Enumerable of command arguments</value>
    public AbstractCommandArgument[] Arguments { get; }

    /// <summary>
    /// Gets whether there is a rest argument for the command.
    /// </summary>
    /// <value>Command has rest argument</value>
    public bool HasRestArgument { get; private set; } = false;

    /// <summary>
    /// Gets whether there is a rest argument for the command.
    /// </summary>
    /// <value>Command has rest argument</value>
    public int RequiredCount { get; private set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CommandInfo" /> from the <paramref name="method">command method</paramref>.
    /// </summary>
    /// <param name="method">The method that was declared as a command</param>
    /// <param name="attribute">The command attribute it was given</param>
    /// <param name="parameters">The parameters that will be used as command arguments</param>
    public CommandInfo(MethodInfo method, CommandAttribute attribute, IEnumerable<ParameterInfo> parameters) : base(attribute, method)
    {
        bool defaultInList = false;

        Arguments = parameters.Select<ParameterInfo, AbstractCommandArgument>((arg, argIndex) =>
        {
            // Honestly, I don't know what I am doing. I don't want to do repetitive ifs,
            // especially since it can be a bit slower (see branchless programming). But it
            // still looks bad
            // REVIEW, FIXME: Code below
            if (arg.ParameterType == typeof(string[]))
                // We could probably do it backwards, but eh, better errors?
                if (argIndex + 1 != parameters.Count())
                {
                    throw new InvalidOperationException("String array can only be the last command parameter");
                }
                else
                {
                    HasRestArgument = true;
                    return new CommandRestInfo(arg);
                }

            Type? nullableType = Nullable.GetUnderlyingType(arg.ParameterType);

            bool isDefaultable = nullableType is not null || arg.HasDefaultValue;

            // Prevent (uint? x, uint y)
            if (defaultInList)
            {
                if (!isDefaultable)
                    throw new InvalidOperationException($"Command cannot contain a non-nullable argument after a nullable argument type");
            }
            else
            {
                // Whether to increment required count
                if (isDefaultable)
                    defaultInList = true;
                else RequiredCount++;
            }

            // uint? and uint to be treated the same
            Type argumentType = nullableType ?? arg.ParameterType;

            if (!s_allowedTypes.Contains(argumentType))
                throw new InvalidOperationException($"Cannot have a command argument of type {arg.ParameterType}");

            return isDefaultable ? new CommandOptionalArgumentInfo(arg, argumentType) : new CommandArgumentInfo(arg);
        }).ToArray();
    }
    #endregion

    #region Methods
    internal bool HasCorrectCount(int count) =>
        HasRestArgument ? count >= RequiredCount : count >= RequiredCount && count <= Arguments.Count();

    /// <summary>
    /// Returns the enumerable of runtime method parameter values.
    /// </summary>
    /// <param name="arguments">The given arguments of the command</param>
    /// <returns>Enumerable of parameters</returns>
    internal IEnumerable<object?> GenerateMethodParameters(IEnumerable<string> arguments) =>
        // (CommandEvent invokation, string arg0, int arg1)
        Arguments.Select((arg, argIndex) => arg.GetValueFrom(arguments, argIndex));

    /// <summary>
    /// Invokes the command.
    /// </summary>
    /// <param name="parent">The parent command of this command</param>
    /// <param name="commandEvent">The command event that invoked the command</param>
    /// <param name="arguments">The arguments that have been used to invoke the command</param>
    /// <returns>Whether the command was properly invoked</returns>
    public Task InvokeAsync(CommandBase parent, CommandEvent commandEvent, IEnumerable<object?> arguments) =>
        Task.Run(() => Member.Invoke(parent, new object[] { commandEvent }.Concat(arguments.ToArray()).ToArray()));
    #endregion
}
/// <summary>
/// Represents the information about types that were declared as <see cref="CommandAttribute">commands</see>.
/// </summary>
public class CommandContainerInfo : AbstractCommandInfo<Type>
{
    #region Properties
    /// <summary>
    /// Gets the created instance of <see cref="CommandAttribute">the command</see> type for this command.
    /// </summary>
    /// <value>Command instance</value>
    public CommandBase Instance { get; }

    /// <inheritdoc cref="CommandBase.Commands" />
    public IEnumerable<ICommandInfo<MemberInfo>> SubCommands => Instance.Commands;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CommandContainerInfo" /> from the <paramref name="type">command type</paramref>.
    /// </summary>
    /// <param name="type">The type that was declared as a command</param>
    /// <param name="attribute">The command attribute it was given</param>
    /// <param name="instance">Other reflection members that were declared as commands</param>
    public CommandContainerInfo(Type type, CommandAttribute attribute, CommandBase instance) : base(attribute, type) =>
        Instance = instance;
    #endregion
}