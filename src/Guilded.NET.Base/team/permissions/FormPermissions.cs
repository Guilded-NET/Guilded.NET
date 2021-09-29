using System;

namespace Guilded.NET.Base.Permissions
{
    /// <summary>
    /// Permissions related to forms &amp; polls.
    /// </summary>
    /// <remarks>
    /// <para>Defines team permissions related to forms &amp; polls.</para>
    /// </remarks>
    [Flags]
    public enum FormPermissions
    {
        /// <summary>
        /// No given permissions.
        /// </summary>
        None = 0,
        /// <summary>
        /// Allows you to view all form responses
        /// </summary>
        FormResponses = 2,
        /// <summary>
        /// Allows you to view all poll results
        /// </summary>
        PollResults = 16,

        #region Additional
        /// <summary>
        /// All of the permissions combined.
        /// </summary>
        All = FormResponses | PollResults,
        /// <summary>
        /// A simple permission combination allowing writing permissions and reading permissions.
        /// </summary>
        /// <remarks>
        /// <para>Sets these permissions:</para>
        /// <list type="bullet">
        ///     <item>
        ///         <description><see cref="PollResults"/></description>
        ///     </item>
        /// </list>
        /// </remarks>
        Basic = PollResults
        #endregion
    }
}