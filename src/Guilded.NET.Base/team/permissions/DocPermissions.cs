using System;

namespace Guilded.NET.Base.Permissions
{
    /// <summary>
    /// Permissions of document/doc channel.
    /// </summary>
    [Flags]
    public enum DocPermissions
    {
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
        Manage = ManageDocs | RemoveDocs,
        /// <summary>
        /// A simple permission combination allowing writing permissions and reading permissions.
        /// </summary>
        Basic = CreateDocs | ViewDocs
        #endregion
    }
}