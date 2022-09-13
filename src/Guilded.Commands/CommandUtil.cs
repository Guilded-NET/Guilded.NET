using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using Guilded.Commands.Items;

namespace Guilded.Commands;

internal static class CommandUtil
{
    #region Methods
    public static void SubscribeToFailedCommands(CommandParent command, Type type)
    {
        Type commandEventType = typeof(CommandEvent);

        foreach (MethodInfo method in type.GetMethods())
        {
            CommandFallbackAttribute? attr = method.GetCustomAttribute<CommandFallbackAttribute>();

            if (attr is null || method.IsStatic) continue;

            ParameterInfo[] parameters = method.GetParameters();

            // (CommandEvent invokation)
            if (parameters.Length != 1 || !(parameters.First().ParameterType == commandEventType || parameters.First().ParameterType.IsSubclassOf(commandEventType)))
                continue;

            command
                .FailedCommand
                .Where(failedCommand =>
                    failedCommand.FailType == attr.Type
                )
                .Subscribe(failedCommand =>
                    method.Invoke(command, new object[] { failedCommand })
                );
        }
    }

    public static IEnumerable<ICommand<MemberInfo>> GetCommandsOf(Type type) =>
        type.GetMembers()
            .Select(member => (member, attribute: member.GetCustomAttribute<CommandAttribute>()))
            // Ignore misc and instance methods
            .Where(memberInfo =>
            {
                CommandAttribute? attribute = memberInfo.attribute;
                MemberInfo member = memberInfo.member;
                MemberTypes memberType = member.MemberType;

                return
                    attribute is not null &&
                    (
                        (memberType == MemberTypes.Method && !((MethodInfo)member).IsAbstract) ||
                        (memberType == MemberTypes.NestedType && !((TypeInfo)member).IsAbstract)
                    );
            })
            // Looks really ugly
            .Select<(MemberInfo member, CommandAttribute? attribute), ICommand<MemberInfo>>(
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

                        return new Command(method, attribute, parameters[1..]);
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

                                    return constructor.IsPublic && !parameters.Any();
                                });

                        if (invokableConstructor is null)
                            throw new MemberAccessException($"Could not find public constructor with no parameters in type {type}");

                        CommandBase instance = (CommandBase)invokableConstructor.Invoke(Array.Empty<object>());

                        SubscribeToFailedCommands(instance, type);

                        return instance.InstanceInfo;
                    }
                }
            );
    #endregion
}