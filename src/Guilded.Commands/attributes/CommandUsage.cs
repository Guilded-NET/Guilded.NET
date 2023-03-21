using System;

namespace Guilded.Commands;

/// <summary>
/// Represents where the <see cref="CommandAttribute">commands</see> can be used.
/// </summary>
[Flags]
public enum CommandArea
{
    /// <summary>
    /// The <see cref="CommandAttribute">commands</see> can be used only in servers.
    /// </summary>
    Servers = 1,

    /// <summary>
    /// The <see cref="CommandAttribute">commands</see> can be used only in DMs.
    /// </summary>
    Dms = 2,

    /// <summary>
    /// The <see cref="CommandAttribute">commands</see> can be anywhere.
    /// </summary>
    Anywhere = Servers | Dms
}