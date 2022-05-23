using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Guilded.Commands;

/// <summary>
/// Represents the base for all <see cref="CommandAttribute">command types</see>.
/// </summary>
public class CommandBase
{
    private readonly Subject<FailedCommandEvent> onFailedCommand = new();

    #region Properties
    /// <summary>
    /// Gets the list of commands or sub-commands of this command.
    /// </summary>
    /// <value>Commands</value>
    public IEnumerable<ICommandInfo<MemberInfo>> Commands { get; protected internal set; }
    /// <summary>
    /// Gets the lookup of <see cref="Commands">commands or sub-commands</see> based on their <see cref="ICommandInfo{T}.Name">name</see>.
    /// </summary>
    /// <returns><see cref="ILookup{TKey, TElement}">Lookup</see> of <see cref="ICommandInfo{T}.Name">names</see> to <see cref="Commands">commands</see></returns>
    public ILookup<string, ICommandInfo<MemberInfo>> CommandLookup => Commands.ToLookup(command => command.Name);
    /// <summary>
    /// Gets the event for failed command invokation.
    /// </summary>
    /// <returns>Observable</returns>
    public IObservable<FailedCommandEvent> FailedCommand => onFailedCommand.AsObservable();
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CommandBase" />.
    /// </summary>
    public CommandBase() =>
        // Because it sucks having to use constructors everywhere
        // Allows nested types as well
        // [Command] type within [Command] type within [Command] type...
        // CommandContainers first for invokation optimization reasons
        Commands = CommandUtil.GetCommandsOf(GetType()).OrderByDescending(command => command is CommandContainerInfo);
    #endregion

    #region Methods

    #region Invokation
    /// <summary>
    /// Invokes any of the command's <see cref="Commands">sub-commands</see>.
    /// </summary>
    /// <param name="usedBaseName">The specified name of this command</param>
    /// <param name="context">The information about the original command</param>
    /// <param name="arguments">The arguments given to this command</param>
    public async Task InvokeAsync(string usedBaseName, RootCommandContext context, IEnumerable<string> arguments)
    {
        if (!arguments.Any())
        {
            // Command index
            onFailedCommand.OnNext(new FailedCommandEvent(context, usedBaseName, arguments, FallbackType.Unspecified));
            return;
        }

        await InvokeAnyCommandAsync(context, commandName: arguments.First(), arguments: arguments.Skip(1)).ConfigureAwait(false);
    }
    /// <summary>
    /// Filters out <see cref="Commands">commands</see> that do not have <paramref name="name">the specified name</paramref>.
    /// </summary>
    /// <param name="name">The name of the commands to get</param>
    /// <returns>Filtered commands</returns>
    public IEnumerable<ICommandInfo<MemberInfo>> FilterCommandsByName(string name) =>
        Commands.Where(command => command.HasName(name));
    /// <summary>
    /// Filters <see cref="Commands">commands</see> and invokes any commands that were found. If none are found, <see cref="FailedCommand">failed command event</see> is invoked.
    /// </summary>
    /// <param name="context">The information about the original command</param>
    /// <param name="commandName">The name of the current command used</param>
    /// <param name="arguments">The arguments of the current command used</param>
    protected async Task InvokeAnyCommandAsync(RootCommandContext context, string commandName, IEnumerable<string> arguments)
    {
        // Filter by parameters and names
        var filteredSubCommands =
            FilterCommandsByName(commandName)
                .Where(command =>
                    command is CommandContainerInfo commandContainer || ((CommandInfo)command).HasCorrectCount(arguments.Count())
                );

        // Wait for one of them to be correctly invoked
        foreach (var filteredCommand in filteredSubCommands)
        {
            if (filteredCommand is CommandContainerInfo commandContainer)
            {
                await commandContainer.Instance.InvokeAsync(commandName, context, arguments).ConfigureAwait(false);
                return;
            }
            else
            {
                CommandInfo command = (CommandInfo)filteredCommand;

                try
                {
                    var commandArgs = command.GenerateMethodParameters(arguments);

                    // Check if all commands have correct arguments
                    if (commandArgs is not null)
                    {
                        // Context
                        CommandEvent commandEvent = new(context, commandName, arguments);

                        await command.InvokeAsync(this, commandEvent, commandArgs).ConfigureAwait(false);
                        return;
                    }
                }
#pragma warning disable CS0168
                catch (FormatException _)
                {
                    continue;
                }
#pragma warning restore CS0168
            }
        }

        onFailedCommand.OnNext(new FailedCommandEvent(context, commandName, arguments, FallbackType.NoCommandFound));
    }
    #endregion

    #endregion
}