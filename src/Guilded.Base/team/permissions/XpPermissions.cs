using System;

namespace Guilded.Base.Permissions
{
    /// <summary>
    /// Permissions related to XP.
    /// </summary>
    /// <remarks>
    /// <para>Defines team permissions related to XP &amp; levels.</para>
    /// </remarks>
    [Flags]
    public enum XpPermissions
    {
        /// <summary>
        /// No given permissions.
        /// </summary>
        None = 0,
        /// <summary>
        /// Allows you to manage XP on server members
        /// </summary>
        ManageServerXp = 1,

        #region Additional
        /// <summary>
        /// All of the permissions combined.
        /// </summary>
        All = ManageServerXp,
        /// <summary>
        /// All of the manage permissions combined.
        /// </summary>
        /// <remarks>
        /// <para>Sets these permissions:</para>
        /// <list type="bullet">
        ///     <item>
        ///         <description><see cref="ManageServerXp"/></description>
        ///     </item>
        /// </list>
        /// </remarks>
        Manage = ManageServerXp
        #endregion
    }
}