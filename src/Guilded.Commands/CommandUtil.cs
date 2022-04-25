using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Guilded.Commands;

internal static class CommandUtil
{
    private static readonly Type[] _unknownFallbackParameterTypes = new Type[] { typeof(string), typeof(CommandEvent), typeof(IEnumerable<ICommandInfo<MemberInfo>>) },
                                   _indexParameterTypes = new Type[] { typeof(CommandEvent), typeof(IEnumerable<ICommandInfo<MemberInfo>>) };
    public static MethodInfo? GetMethodWithAttrAndParams<T>(Type type, Type[] requiredParameters) where T : Attribute =>
        type.GetMethods()
            .FirstOrDefault(method =>
            {
                if (method.GetCustomAttribute<T>() is null || !method.IsStatic)
                    return false;

                ParameterInfo[] parameters = method.GetParameters();

                // (string commandName, CommandEvent invokation)
                if (parameters.Length != requiredParameters.Length) return false;

                for (int i = 0; i < requiredParameters.Length; i++)
                    if (parameters[i].ParameterType != requiredParameters[i])
                        return false;

                // All passed
                return true;
            });
    public static MethodInfo? GetUnknownCommandFallback(Type type) =>
        GetMethodWithAttrAndParams<UnknownCommandAttribute>(type, _unknownFallbackParameterTypes);
    public static MethodInfo? GetIndexCommandFallback(Type type) =>
        GetMethodWithAttrAndParams<CommandIndexAttribute>(type, _indexParameterTypes);

    public static IEnumerable<ICommandInfo<MemberInfo>> GetCommandsOf(Type type) =>
        type.GetMembers()
            .Select(member => (member, attribute: (CommandAttribute?)member.GetCustomAttribute<CommandAttribute>()))
            // Ignore misc and instance methods
            .Where(memberInfo =>
            {
                CommandAttribute? attribute = memberInfo.attribute;
                MemberInfo member = memberInfo.member;
                MemberTypes memberType = member.MemberType;

                return
                    attribute is not null &&
                    (
                        (memberType == MemberTypes.Method && ((MethodInfo)member).IsStatic) ||
                        memberType == MemberTypes.NestedType || memberType == MemberTypes.TypeInfo
                    );
            })
            // Looks really ugly
            .Select<(MemberInfo member, CommandAttribute? attribute), ICommandInfo<MemberInfo>>(
                methodInfo =>
                {
                    MemberInfo member = methodInfo.member;
                    CommandAttribute attribute = methodInfo.attribute!;

                    if (member is MethodInfo method)
                    {
                        ParameterInfo[] parameters = method.GetParameters();

                        // (CommandEvent command, string arg0, string arg1)
                        if (!parameters.Any())
                            throw new FormatException("Expected method with [Command] attribute to have at least one parameter");

                        ParameterInfo contextParameter = parameters.First();

                        if (contextParameter.ParameterType != typeof(CommandEvent))
                            throw new FormatException("Expected method with [Command] attribute to have first parameter with CommandEvent type");

                        return new CommandInfo(method, attribute, parameters[1..]);
                    }
                    else
                    {
                        Type type = (Type)member;

                        // Allows nested types as well
                        // [Command] type within [Command] type within [Command] type...
                        IEnumerable<ICommandInfo<MemberInfo>> subCommands = GetCommandsOf(type);

                        MethodInfo? commandIndex = GetIndexCommandFallback(type),
                                    unknownCommandFallback = GetUnknownCommandFallback(type);

                        return new CommandContainerInfo(type, attribute, subCommands, commandIndex, unknownCommandFallback);
                    }
                }
            );
    /// <summary>
    /// Invokes any <paramref name="commandName">given command</paramref> from the <paramref name="commands">provided list</paramref> that were found to be found with correct parameters.
    /// </summary>
    /// <param name="commandEvent">The command invokation event that is invoking any of the specified commands</param>
    /// <param name="commandName">The name of the commands being invoked</param>
    /// <param name="commands">The commands that can be invoked</param>
    /// <param name="args">The arguments that were given to the command</param>
    /// <returns>Any command was invoked</returns>
    internal static async Task<bool> InvokeAnyCommand(CommandEvent commandEvent, string commandName, IEnumerable<ICommandInfo<MemberInfo>> commands, IEnumerable<string> args)
    {
        IEnumerable<ICommandInfo<MemberInfo>> foundCommands = commands.Where(command => command.HasName(commandName));

        // Temp
        if (!foundCommands.Any()) return false;

        bool anyCommandInvoked =
            (await Task.WhenAll(foundCommands.Select(x => x.InvokeAsync(commandEvent, args))))
                .Any(invokedProperly => invokedProperly);

        return foundCommands.Any() && anyCommandInvoked;
    }
}