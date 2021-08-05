using System;

namespace Guilded.NET.Base.Permissions
{
    /// <summary>
    /// All of the bracket permissions for team tournaments.
    /// </summary>
    [Flags]
    public enum BracketPermissions
    {
        /// <summary>
        /// Allows you to report match scores on behalf of your server
        /// </summary>
        ReportScores = 1,
        /// <summary>
        /// Allows you to view tournament brackets
        /// </summary>
        ViewBrackets = 2
    }
}