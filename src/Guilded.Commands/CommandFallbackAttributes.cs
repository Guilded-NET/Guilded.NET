using System;

namespace Guilded.Commands;

/// <summary>
/// Marks method as a no specified (sub-)command fallback.
/// </summary>
/// <remarks>
/// <para>Whenever command has wrong arguments, the method with <see cref="UnknownCommandAttribute" /> will be executed.</para>
/// </remarks>
[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public sealed class UnknownCommandAttribute : Attribute
{
    /// <summary>
    /// Sets method as an unknown command fallback.
    /// </summary>
    public UnknownCommandAttribute() { }
}
/// <summary>
/// Marks method as a fallback to types marked as <see cref="CommandAttribute">commands</see> whenever subcommand is not specified.
/// </summary>
[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public sealed class CommandIndexAttribute : Attribute
{
    /// <summary>
    /// Sets method as a sub-command fallback.
    /// </summary>
    public CommandIndexAttribute() { }
}