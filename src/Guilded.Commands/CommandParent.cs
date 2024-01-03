using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reflection;
using System.Threading.Tasks;
using Guilded.Commands.Items;
using Guilded.Commands.Utils;
using Guilded.Servers;

namespace Guilded.Commands;

/// <summary>
/// Represents an abstract type that holds commands.
/// </summary>
/// <seealso cref="CommandBase" />
/// <seealso cref="CommandModule" />
public abstract class CommandParent
{
    #region Field
    private readonly Subject<FailedCommandEvent> onFailedCommand = new();
    #endregion

    #region Properties
    /// <summary>
    /// Gets the list of <see cref="CommandAttribute">commands or sub-commands</see> of this command.
    /// </summary>
    /// <value>The list of <see cref="CommandAttribute">commands or sub-commands</see> of this command</value>
    /// <seealso cref="CommandParent" />
    /// <seealso cref="CommandLookup" />
    /// <seealso cref="CommandNames" />
    public IEnumerable<ICommand<MemberInfo>> Commands { get; protected internal set; }

    /// <summary>
    /// Gets the lookup of <see cref="Commands">commands or sub-commands</see> based on their <see cref="ICommand{T}.Name">name</see>.
    /// </summary>
    /// <example>
    /// <para>Since the keys of the lookup are always going to be <see cref="Commands">command</see> names and they are going to be unique, this can be used in help commands by getting all the keys in the lookup:</para>
    /// <code>
    /// var commandList = CommandLookup.Select(group => group.Key);
    /// </code>
    /// <para>This will not give you any duplicate command names. This is what <see cref="CommandNames" /> is for, so it's recommended to use <see cref="CommandNames" /> when you need to get list of unique command names.</para>
    /// </example>
    /// <returns>The lookup of <see cref="Commands">commands or sub-commands</see> based on their <see cref="ICommand{T}.Name">name</see></returns>
    /// <seealso cref="CommandParent" />
    /// <seealso cref="Commands" />
    /// <seealso cref="CommandNames" />
    public ILookup<string, ICommand<MemberInfo>> CommandLookup => Commands.ToLookup(command => command.Name);

    /// <summary>
    /// Gets the list of the <see cref="CommandAttribute.Name">names</see> of all <see cref="Commands">commands or sub-commands</see>.
    /// </summary>
    /// <returns>Te list of the <see cref="CommandAttribute.Name">names</see> of all <see cref="Commands">commands or sub-commands</see></returns>
    /// <seealso cref="CommandParent" />
    /// <seealso cref="CommandLookup" />
    /// <seealso cref="Commands" />
    public IEnumerable<string> CommandNames => CommandLookup.Select(x => x.Key);

    /// <summary>
    /// Gets the event for failed command invokation.
    /// </summary>
    /// <returns>Observable</returns>
    /// <seealso cref="CommandParent" />
    public IObservable<FailedCommandEvent> FailedCommand => onFailedCommand.AsObservable();
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CommandParent" />.
    /// </summary>
    /// <seealso cref="CommandParent" />
    protected CommandParent() =>
        // Because it sucks having to use constructors everywhere
        // Allows nested types as well
        // [Command] type within [Command] type within [Command] type...
        // CommandInfo first, in-case there are there are method-based commands that match the arguments
        Commands = CommandUtil.GetCommandsOf(GetType()).OrderBy(command => command is CommandContainer);
    #endregion

    #region Methods Invokation
    /// <summary>
    /// Invokes any of the command's <see cref="Commands">sub-commands</see>.
    /// </summary>
    /// <param name="usedBaseName">The specified name of this command</param>
    /// <param name="context">The information about the original command</param>
    /// <param name="arguments">The arguments given to this command</param>
    /// <param name="count">The count of split arguments in the command used</param>
    public async Task<bool> InvokeAsync(string usedBaseName, RootCommandEvent context, string arguments, int count)
    {
        if (string.IsNullOrEmpty(arguments))
        {
            // Command index
            onFailedCommand.OnNext(new FailedCommandEvent(context, usedBaseName, arguments, FallbackType.Unspecified));
            return false;
        }

        string[] split = arguments.Split(context.Configuration.Separators, 2, context.Configuration.SplitOptions);

        string subCommandName = split.First(), subCommandArgs = split.ElementAtOrDefault(1) ?? string.Empty;

        return await InvokeCommandByNameAsync(context, subCommandName, subCommandArgs, count - 1).ConfigureAwait(false);
    }

    /// <summary>
    /// Filters out <see cref="Commands">commands</see> that do not have the specified <paramref name="name" />.
    /// </summary>
    /// <param name="name">The name of the commands to get</param>
    /// <returns>The <see cref="Commands">commands</see> that fit the provided <paramref name="name" /></returns>
    public IEnumerable<ICommand<MemberInfo>> FilterCommandsByName(string name) =>
        Commands.Where(command => command.HasName(name));

    /// <summary>
    /// Filters <see cref="Commands">commands</see> and invokes any commands that were found. If none are found, <see cref="FailedCommand">failed command event</see> is invoked.
    /// </summary>
    /// <param name="rootInvokation">The information about the original command</param>
    /// <param name="commandName">The name of the current command used</param>
    /// <param name="arguments">The arguments of the current command used</param>
    /// <param name="count">The count of split arguments in the command used</param>
    protected async Task<bool> InvokeCommandByNameAsync(RootCommandEvent rootInvokation, string commandName, string arguments, int count)
    {
        IEnumerable<ICommand<MemberInfo>> commandsByName = FilterCommandsByName(commandName);

        // Can't find it
        if (!commandsByName.Any())
        {
            onFailedCommand.OnNext(new FailedCommandEvent(rootInvokation, commandName, arguments, FallbackType.NoCommandFound));
            return false;
        }

        // Filter by parameters and names
        IEnumerable<ICommand<MemberInfo>> filteredSubCommands =
            commandsByName
                .Where(command =>
                    command is CommandContainer commandContainer || ((Command)command).HasCorrectCount(count)
                );

        if (!filteredSubCommands.Any())
        {
            onFailedCommand.OnNext(new FailedCommandEvent(rootInvokation, commandName, arguments, FallbackType.BadArgumentCount));
            return false;
        }

        List<AbstractCommandArgument> badArguments = [];

        // Fill in required count of members, channels, roles for arguments from message mentions
        var maxFetchRequired = CommandArgumentUtil.GetMaxNumbersOfFetchableArguments(filteredSubCommands);

        await rootInvokation.FetchRequiredArgumentsAsync(maxFetchRequired);

        // Wait for one of them to be correctly invoked
        foreach (ICommand<MemberInfo> filteredCommand in filteredSubCommands)
        {
            if (filteredCommand is CommandContainer commandContainer)
            {
                await InvokeCommandAsync(commandContainer, rootInvokation, commandName, arguments, count).ConfigureAwait(false);
                return true;
            }
            else
            {
                Command command = (Command)filteredCommand;

                if (command.GenerateMethodParameters(rootInvokation, arguments, out List<object?> commandArgs, out AbstractCommandArgument? badArgument))
                {
                    await InvokeCommandAsync(command, rootInvokation, commandName, arguments, commandArgs).ConfigureAwait(false);
                    return true;
                }
                else
                {
                    badArguments.Add(badArgument);
                }
            }
        }

        onFailedCommand.OnNext(new BadCommandArgumentEvent(rootInvokation, commandName, arguments, badArguments));

        return false;
    }

    /// <summary>
    /// Invokes the provided <paramref name="command" /> as a child of <see cref="CommandParent">this command base</see>.
    /// </summary>
    /// <param name="command">The <see cref="CommandAttribute">command</see> to invoke</param>
    /// <param name="rootInvokation">The unnested first-most command that has been invoked</param>
    /// <param name="commandName">The used name of the invoking <paramref name="command" /></param>
    /// <param name="rawArguments">The unparsed arguments that were given to the command</param>
    /// <param name="arguments">The parsed arguments that were given to the command</param>
    protected virtual Task InvokeCommandAsync(Command command, RootCommandEvent rootInvokation, string commandName, string rawArguments, IEnumerable<object?> arguments)
    {
        CommandEvent commandInvokation = new(rootInvokation, commandName, rawArguments);

        return command.InvokeAsync(this, commandInvokation, arguments);
    }

    /// <summary>
    /// Invokes the provided <paramref name="command" /> as a child of <see cref="CommandParent">this command base</see>.
    /// </summary>
    /// <param name="command">The <see cref="CommandAttribute">command</see> to invoke</param>
    /// <param name="rootInvokation">The unnested first-most command that has been invoked</param>
    /// <param name="commandName">The used name of the invoking <paramref name="command" /></param>
    /// <param name="arguments">The unparsed arguments and sub-command names that were given to the command</param>
    /// <param name="count">The count of split arguments in the command used</param>
    protected virtual Task InvokeCommandAsync(CommandContainer command, RootCommandEvent rootInvokation, string commandName, string arguments, int count) =>
        command.Instance.InvokeAsync(commandName, rootInvokation, arguments, count);
    #endregion
}