using System;

namespace Guilded.Commands;

/// <summary>
/// Defines possible usage location for <see cref="CommandAttribute">a command</see>.
/// </summary>
/// <example>
/// <para>The example code below showcases the use of <see cref="UsageAreaAttribute" /> to make a command server-only:</para>
/// <code language="csharp">
/// [UsageArea(CommandArea.Servers)]
/// [Command]
/// public class Config
/// {
///     // ...
/// }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class UsageAreaAttribute : Attribute
{
    #region Properties
    /// <summary>
    /// Gets the area where <see cref="CommandAttribute">commands</see> can be used.
    /// </summary>
    /// <value>The area where <see cref="CommandAttribute">commands</see> can be used</value>
    public CommandArea Area { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Defines possible usage location for <see cref="CommandAttribute">a command</see>.
    /// </summary>
    /// <param name="area">The area where <see cref="CommandAttribute">commands</see> can be used</param>
    public UsageAreaAttribute(CommandArea area) =>
        Area = area;
    #endregion
}