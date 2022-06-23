using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RestSharp.Extensions;

namespace Guilded.Commands;

/// <summary>
/// Represents the interface for all commands.
/// </summary>
/// <typeparam name="TMember">The type of the reflection member</typeparam>
public interface ICommandInfo<out TMember> where TMember : MemberInfo
{
    #region Abstract members
    /// <summary>
    /// Gets the <see cref="CommandAttribute.Name">name</see> of the command.
    /// </summary>
    /// <value>Name</value>
    string Name { get; }

    /// <summary>
    /// Gets the member who was declared as a command.
    /// </summary>
    /// <value>Reflection member</value>
    TMember Member { get; }

    /// <summary>
    /// Gets the <see cref="CommandAttribute">command attribute</see> that was given to the <see cref="Member">member</see>.
    /// </summary>
    /// <value>Command attribute</value>
    CommandAttribute Attribute { get; }

    /// <inheritdoc cref="CommandAttribute.Aliases" />
    string[]? Aliases { get; }

    /// <inheritdoc cref="DescriptionAttribute.Text" />
    string? Description { get; }

    /// <inheritdoc cref="ExampleAttribute.Content" />
    IEnumerable<ExampleAttribute>? Examples { get; }
    #endregion

    #region Methods
    /// <summary>
    /// Gets whether the <paramref name="name">given name</paramref> matches command's <see cref="Name">name</see> or its <see cref="Aliases">aliases</see>.
    /// </summary>
    /// <param name="name">The name to check whether the command contains</param>
    /// <returns>Command has <paramref name="name">given name</paramref></returns>
    public bool HasName(string name) =>
        Name == name || (Aliases?.Contains(name) ?? false);
    #endregion
}

/// <summary>
/// Represents the base for information about any type of Guilded.NET command.
/// </summary>
/// <typeparam name="TMember">The type of the member it uses for commands</typeparam>
/// <seealso cref="CommandAttribute" />
/// <seealso cref="CommandParamAttribute" />
/// <seealso cref="ICommandInfo{TMember}" />
/// <seealso cref="CommandInfo" />
/// <seealso cref="CommandContainerInfo" />
public abstract class AbstractCommandInfo<TMember> : ICommandInfo<TMember> where TMember : MemberInfo
{
    #region Properties
    /// <inheritdoc />
    public string Name { get; }

    /// <inheritdoc />
    public TMember Member { get; }

    /// <inheritdoc />
    public CommandAttribute Attribute { get; }

    /// <inheritdoc cref="CommandAttribute.Aliases" />
    public string[]? Aliases => Attribute.Aliases;

    /// <inheritdoc cref="DescriptionAttribute.Text" />
    public string? Description => Member.GetAttribute<DescriptionAttribute>()?.Text;

    /// <inheritdoc cref="ExampleAttribute.Content" />
    public IEnumerable<ExampleAttribute> Examples => Member.GetCustomAttributes<ExampleAttribute>();
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="AbstractCommandInfo{TMember}" />.
    /// </summary>
    /// <param name="attribute">The command attribute that was given to the member</param>
    /// <param name="member">The member who was declared as a command</param>
    protected AbstractCommandInfo(CommandAttribute attribute, TMember member) =>
        (Name, Member, Attribute) = (attribute.Name ?? TransformMethodName(member.Name), member, attribute);
    #endregion

    #region Methods
    private static string TransformMethodName(string name)
    {
        // Trim XCommandAsync(), XCommand(), XAsync()
        string unsuffixedName = TrimSuffix(TrimSuffix(name, "Async"), "Command");

        return unsuffixedName.ToLowerInvariant();
    }

    private static string TrimSuffix(string str, string substring)
    {
        int suffixIndex = str.LastIndexOf(substring);

        return suffixIndex > -1 ? str[..suffixIndex] : str;
    }
    #endregion
}