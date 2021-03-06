using System;

namespace Guilded.NET.Objects.Permissions
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
        RemoveDocs = 8
    }
}