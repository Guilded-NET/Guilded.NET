using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;

namespace Guilded.Commands;

internal static class CommandUtil
{
    public static void SubscribeToFailedCommands(CommandBase command, Type type)
    {
        Type commandEventType = typeof(CommandEvent);

        foreach (MethodInfo method in type.GetMethods())
        {
            CommandFallbackAttribute? attr = method.GetCustomAttribute<CommandFallbackAttribute>();

            if (attr is null || method.IsStatic) continue;

            ParameterInfo[] parameters = method.GetParameters();

            // (CommandEvent invokation)
            if (parameters.Length != 1 || parameters.First().ParameterType != commandEventType)
                continue;

            command
                .FailedCommand
                .Where(failedCommand =>
                    failedCommand.Type == attr.Type
                )
                .Subscribe(failedCommand =>
                    method.Invoke(command, new object[] { failedCommand })
                );
        }
    }

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
                        (memberType == MemberTypes.Method && !((MethodInfo)member).IsStatic) ||
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

                        if (!type.IsSubclassOf(typeof(CommandBase)))
                            throw new NotSupportedException($"Cannot declare type as a command when it's not a subclass of {nameof(CommandBase)}.");

                        ConstructorInfo? invokableConstructor =
                            type.GetConstructors()
                                .FirstOrDefault(constructor =>
                                {
                                    ParameterInfo[] parameters = constructor.GetParameters();

                                    return constructor.IsPublic && parameters.Count() == 1 && parameters.First().ParameterType == typeof(IEnumerable<ICommandInfo<MemberInfo>>);
                                });

                        if (invokableConstructor is null)
                            throw new MemberAccessException($"Could not find public constructor with parameter {nameof(IEnumerable<ICommandInfo<MethodInfo>>)} in type {type}");

                        // Allows nested types as well
                        // [Command] type within [Command] type within [Command] type...
                        IEnumerable<ICommandInfo<MemberInfo>> subCommands = GetCommandsOf(type);

                        CommandBase instance = (CommandBase)invokableConstructor.Invoke(new object[] { subCommands });

                        SubscribeToFailedCommands(instance, type);

                        return new CommandContainerInfo(type, attribute, instance);
                    }
                }
            );
}