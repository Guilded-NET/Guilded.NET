using System;

namespace Guilded.Commands;

/// <summary>
/// Represents where <see cref="CommandAttribute">the commands</see> can be used.
/// </summary>
[Flags]
public enum CommandArea
{
    /// <summary>
    /// <see cref="CommandAttribute">The commands</see> can be used only in servers.
    /// </summary>
    Servers = 1,

    /// <summary>
    /// <see cref="CommandAttribute">The commands</see> can be used only in DMs.
    /// </summary>
    Dms = 2,

    /// <summary>
    /// <see cref="CommandAttribute">The commands</see> can be anywhere.
    /// </summary>
    Anywhere = Servers | Dms
}