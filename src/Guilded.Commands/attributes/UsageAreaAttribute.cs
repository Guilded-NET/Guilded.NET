using System;

namespace Guilded.Commands;

/// <summary>
/// Defines possible usage location for <see cref="CommandAttribute">a command</see>.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class UsageAreaAttribute : Attribute
{
    #region Properties
    /// <summary>
    /// Gets the area where <see cref="CommandAttribute">commands</see> can be used.
    /// </summary>
    /// <value>Area</value>
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