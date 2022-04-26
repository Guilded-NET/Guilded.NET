using System;
using System.CodeDom.Compiler;
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
    /// <summary>
    /// The enumerable of command arguments.
    /// </summary>
    /// <remarks>
    /// <para>The enumerable of arguments that can be specified by users.</para>
    /// </remarks>
    /// <value>Enumerable of command arguments</value>
    public IEnumerable<CommandArgumentInfo> Arguments { get; }
    /// <summary>
    /// Initializes a new instance of <see cref="CommandInfo" /> from the <paramref name="method">command method</paramref>.
    /// </summary>
    /// <param name="method">The method that was declared as a command</param>
    /// <param name="attribute">The command attribute it was given</param>
    /// <param name="parameters">The parameters that will be used as command arguments</param>
    public CommandInfo(MethodInfo method, CommandAttribute attribute, IEnumerable<ParameterInfo> parameters) : base(attribute, method) =>
        Arguments = parameters.Select(arg => new CommandArgumentInfo(arg));

    /// <summary>
    /// Returns the enumerable of runtime method parameter values.
    /// </summary>
    /// <param name="commandEvent">The command event to add at the start of the sequence</param>
    /// <param name="arguments">The given arguments of the command</param>
    /// <returns>Enumerable of parameters</returns>
    internal object?[]? GenerateMethodParameters(CommandEvent commandEvent, IEnumerable<string> arguments)
    {
        if (arguments.Count() < Arguments.Count())
            return null;

        try
        {
            int usedArguments = 0;

            var generatedArguments =
                // (CommandEvent invokation, string arg0, int arg1)
                new object[] { commandEvent }.Concat(
                    Arguments.Select<CommandArgumentInfo, object?>((arg, argIndex) =>
                    {
                        string stringArgument = arguments.ElementAt(argIndex);

                        Type argType = arg.ArgumentType;

                        // Rest argument
                        if (argType == typeof(string[]))
                        {
                            if (argIndex + 1 != Arguments.Count())
                                throw new FormatException();

                            usedArguments = arguments.Count();

                            return arguments.Skip(argIndex).ToArray();
                        }

                        usedArguments++;

                        // Could use TryParse, but you can't do `out object` and
                        // it would require different name for every parsed item
                        return
                            argType == typeof(string)
                            ? stringArgument
                            : argType == typeof(bool)
                            ? bool.Parse(stringArgument)
                            : argType == typeof(int)
                            ? int.Parse(stringArgument)
                            : argType == typeof(long)
                            ? long.Parse(stringArgument)
                            : argType == typeof(short)
                            ? short.Parse(stringArgument)
                            : argType == typeof(sbyte)
                            ? sbyte.Parse(stringArgument)
                            : argType == typeof(uint)
                            ? uint.Parse(stringArgument)
                            : argType == typeof(ulong)
                            ? ulong.Parse(stringArgument)
                            : argType == typeof(ushort)
                            ? ushort.Parse(stringArgument)
                            : argType == typeof(byte)
                            ? byte.Parse(stringArgument)
                            : argType == typeof(float)
                            ? float.Parse(stringArgument)
                            : argType == typeof(double)
                            ? double.Parse(stringArgument)
                            : argType == typeof(decimal)
                            ? decimal.Parse(stringArgument)
                            : argType == typeof(DateTime)
                            ? DateTime.Parse(stringArgument)
                            : argType == typeof(Guid)
                            ? new Guid(stringArgument)
                            : argType == typeof(HashId)
                            ? new HashId(stringArgument)
                            : throw new FormatException($"Cannot have type {argType} as a command argument's type");
                    })
                );

            // Only if all given arguments are exhausted, it means that this method is correct
            if (usedArguments != arguments.Count())
                return null;

            return generatedArguments.ToArray();
        }
#pragma warning disable CS0168
        catch (FormatException _)
        {
            return null;
        }
#pragma warning restore CS0168
    }
    /// <inheritdoc />
    public override Task<bool> InvokeAsync(CommandEvent commandEvent, IEnumerable<string> arguments) =>
        Task.Run(() =>
        {
            var invokationArguments = GenerateMethodParameters(commandEvent, arguments);

            if (invokationArguments is null) return false;

            Member.Invoke(this, invokationArguments);

            return true;
        });
}
/// <summary>
/// Represents the information about types that were declared as <see cref="CommandAttribute">commands</see>.
/// </summary>
public class CommandContainerInfo : AbstractCommandInfo<Type>
{
    /// <summary>
    /// Gets the enumerable of all <see cref="CommandAttribute">nested commands</see> within <see cref="CommandContainerInfo">this command</see>.
    /// </summary>
    /// <value>Enumerable of commands</value>
    public IEnumerable<ICommandInfo<MemberInfo>> SubCommands { get; set; }
    /// <summary>
    /// Gets the method that will be used if no <see cref="SubCommands">sub-command</see> is specified.
    /// </summary>
    /// <value>Command fallback</value>
    public MethodInfo? CommandIndex { get; set; }
    /// <summary>
    /// Gets the method that will be used if <see cref="SubCommands">the sub-command</see> is specified, but none was found with the same name or the same parameters.
    /// </summary>
    /// <value>Command fallback</value>
    public MethodInfo? UnknownCommand { get; set; }
    /// <summary>
    /// Initializes a new instance of <see cref="CommandContainerInfo" /> from the <paramref name="type">command type</paramref>.
    /// </summary>
    /// <param name="type">The type that was declared as a command</param>
    /// <param name="attribute">The command attribute it was given</param>
    /// <param name="subCommands">Other reflection members that were declared as commands</param>
    /// <param name="commandIndex">The method that will be used if no <see cref="SubCommands">sub-command</see> is specified</param>
    /// <param name="unknownCommand">The method that will be used if <see cref="SubCommands">the sub-command</see> is specified, but none was found with the same name or the same parameters</param>
    public CommandContainerInfo(Type type, CommandAttribute attribute, IEnumerable<ICommandInfo<MemberInfo>> subCommands, MethodInfo? commandIndex, MethodInfo? unknownCommand) : base(attribute, type) =>
        (SubCommands, CommandIndex, UnknownCommand) = (subCommands, commandIndex, unknownCommand);
    /// <inheritdoc />
    public override async Task<bool> InvokeAsync(CommandEvent commandEvent, IEnumerable<string> arguments)
    {
        if (arguments.Any())
        {
            string subCommandName = arguments.First();

            bool anySubCommandInvoked = await CommandUtil.InvokeAnyCommand(commandEvent, subCommandName, SubCommands, arguments.Skip(1));

            // No command with correct parameters was found
            if (!anySubCommandInvoked)
                UnknownCommand?.Invoke(this, new object[] { subCommandName, commandEvent, SubCommands });
        }
        // No sub-command specified
        else CommandIndex?.Invoke(null, new object[] { commandEvent, SubCommands });

        return true;
    }
}