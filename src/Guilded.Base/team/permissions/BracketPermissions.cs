using System;

namespace Guilded.Base.Permissions
{
    /// <summary>
    /// Permissions related to brackets.
    /// </summary>
    /// <remarks>
    /// <para>Defines team permissions for tournament bracket related things.</para>
    /// </remarks>
    [Flags]
    public enum BracketPermissions
    {
        /// <summary>
        /// No given permissions.
        /// </summary>
        None = 0,
        /// <summary>
        /// Allows you to report match scores on behalf of your server
        /// </summary>
        ReportScores = 1,
        /// <summary>
        /// Allows you to view tournament brackets
        /// </summary>
        ViewBrackets = 2,

        #region Additional
        /// <summary>
        /// All of the permissions combined.
        /// </summary>
        All = ReportScores | ViewBrackets
        #endregion
    }
}