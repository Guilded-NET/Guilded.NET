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
    #endregion

    #region Methods IsXyz
    // Check if reflection type is correct enough to warrant trying to create a command out of it
    private static bool TypeIsCommandType(MemberInfo memberInfo, MemberTypes memberType) =>
        (memberType == MemberTypes.TypeInfo || memberType == MemberTypes.NestedType)
        && ((TypeInfo)memberInfo).IsClass
        && !((TypeInfo)memberInfo).IsAbstract;

    // Check if reflection method is correct enough to warrant trying to create a command out of it
    private static bool MethodIsCommandMethod(MemberInfo member, MemberTypes memberType) =>
        memberType == MemberTypes.Method && !((MethodInfo)member).IsAbstract;
    #endregion

    #region Methods Create command
    // Creates ICommandInfo<Type> / CommandContainer instance from the given type
    // This assumes all the attribute and what kind of type it is checks are already done
    private static CommandContainer CreateCommandContainer(Type type)
    {
        // Should extend/base/inherit from CommandBase
        if (!type.IsSubclassOf(typeof(CommandBase)))
            throw new NotSupportedException($"Cannot declare type as a command when it's not a subclass of {nameof(CommandBase)}.");

        // Find public constructor that has no parameters
        ConstructorInfo invokableConstructor =
            type
                .GetConstructors()
                .FirstOrDefault(constructor =>
                {
                    ParameterInfo[] parameters = constructor.GetParameters();

                    return constructor.IsPublic && !parameters.Any();
                })
                ?? throw new MemberAccessException($"Could not find public constructor with no parameters in type {type}");

        // Create instance of it and let it all handle other stuff
        CommandBase instance = (CommandBase)invokableConstructor.Invoke([]);

        // Additional methods like not enough arguments, etc.
        SubscribeToFailedCommands(instance, type);

        return instance.InstanceInfo;
    }

    // Creates ICommandInfo<MethodInfo> / Command instance
    // This assumes that all the checks of what kind of method it is are already done
    public static Command CreateCommandFunction(MethodInfo method, CommandAttribute attribute)
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
    #endregion

    #region Methods Get commands from
    // Find members that only have an attribute and are types of members that we are asking for them to be
    private static IEnumerable<(T member, CommandAttribute attribute)> FindCorrectMembers<T>(T[] members, Func<MemberInfo, MemberTypes, bool> isMemberCorrect)
        where T : MemberInfo =>
        members
            .Select(member => (member, attribute: member.GetCustomAttribute<CommandAttribute>()))
            .Where(memberInfo =>
            {
                CommandAttribute? attribute = memberInfo.attribute;
                MemberInfo member = memberInfo.member;
                MemberTypes memberType = member.MemberType;

                return attribute is not null && isMemberCorrect(member, memberType);
            })
            // Because forced to
            .Select((tuple) => (tuple.member, tuple.attribute!));

    public static IEnumerable<CommandContainer> GetCommandsOf(Assembly assembly) =>
        FindCorrectMembers(assembly.GetTypes(), TypeIsCommandType)
            .Select((tuple) =>
                CreateCommandContainer(tuple.member)
            );

    public static IEnumerable<ICommand<MemberInfo>> GetCommandsOf(Type type) =>
        // Get methods and classes of the type, then check if they have attributes and are classes or methods
        FindCorrectMembers(type.GetMembers(), (member, memberType) =>
            MethodIsCommandMethod(member, memberType) ||
            TypeIsCommandType(member, memberType)
        )
            // Looks really ugly
            .Select<(MemberInfo member, CommandAttribute attribute), ICommand<MemberInfo>>(memberInfo =>
            {
                MemberInfo member = memberInfo.member;
                CommandAttribute attribute = memberInfo.attribute;

                if (member is MethodInfo method) return CreateCommandFunction(method, attribute);
                else return CreateCommandContainer((Type)member);
            });
    #endregion
}