using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

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
    /// Gets the array of <see cref="CommandAttribute.Aliases">alternative names</see> of the command.
    /// </summary>
    /// <value>Array of names</value>
    string[]? Aliases { get; }
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

    #endregion

    #region Additional
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
    public string[]? Aliases => Attribute.Aliases;
    /// <inheritdoc cref="CommandAttribute.Description" />
    public string? Description => Attribute.Description;
    /// <inheritdoc cref="CommandAttribute.Examples" />
    public string[]? Examples => Attribute.Examples;
    /// <inheritdoc />
    public TMember Member { get; }
    /// <inheritdoc />
    public CommandAttribute Attribute { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="AbstractCommandInfo{TMember}" />.
    /// </summary>
    /// <param name="attribute">The command attribute that was given to the member</param>
    /// <param name="member">The member who was declared as a command</param>
    protected AbstractCommandInfo(CommandAttribute attribute, TMember member) =>
        (Name, Member, Attribute) = (attribute.Name ?? member.Name.ToLowerInvariant(), member, attribute);
    #endregion
}