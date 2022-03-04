using System;

namespace Guilded.Base.Permissions
{
    /// <summary>
    /// Permissions related to documents.
    /// </summary>
    /// <remarks>
    /// <para>Defines channel permissions related to documents.</para>
    /// </remarks>
    [Flags]
    public enum DocPermissions
    {
        /// <summary>
        /// No given permissions.
        /// </summary>
        None = 0,
        /// <summary>
        /// Allows you to create docs
        /// </summary>
        CreateDocs = 1,
        /// <summary>
        /// Allows you to view docs
        /// </summary>
        ViewDocs = 2,
        /// <summary>
        /// Allows you to update docs created by others and move them to other channels
        /// </summary>
        ManageDocs = 4,
        /// <summary>
        /// Allows you to remove docs created by others
        /// </summary>
        RemoveDocs = 8,

        #region Additional
        /// <summary>
        /// All of the permissions combined.
        /// </summary>
        All = CreateDocs | ViewDocs | ManageDocs | RemoveDocs,
        /// <summary>
        /// All of the manage permissions combined.
        /// </summary>
        /// <remarks>
        /// <para>Sets these permissions:</para>
        /// <list type="bullet">
        ///     <item>
        ///         <description><see cref="ManageDocs"/></description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="RemoveDocs"/></description>
        ///     </item>
        /// </list>
        /// </remarks>
        Manage = ManageDocs | RemoveDocs,
        /// <summary>
        /// A simple permission combination allowing writing permissions and reading permissions.
        /// </summary>
        /// <remarks>
        /// <para>Sets these permissions:</para>
        /// <list type="bullet">
        ///     <item>
        ///         <description><see cref="CreateDocs"/></description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="ViewDocs"/></description>
        ///     </item>
        /// </list>
        /// </remarks>
        Basic = CreateDocs | ViewDocs
        #endregion
    }
}