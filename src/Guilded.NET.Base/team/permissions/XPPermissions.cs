using System;

namespace Guilded.NET.Base.Permissions
{
    /// <summary>
    /// Permissions related to XP and levels.
    /// </summary>
    [Flags]
    public enum XPPermissions
    {
        /// <summary>
        /// Allows you to manage XP on server members
        /// </summary>
        ManageServerXP = 1
    }
}