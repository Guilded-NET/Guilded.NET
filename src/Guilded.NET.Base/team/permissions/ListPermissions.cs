using System;

namespace Guilded.NET.Base.Permissions
{
    /// <summary>
    /// List channel's permissions.
    /// </summary>
    [Flags]
    public enum ListPermissions
    {
        /// <summary>
        /// Allows you to create list items
        /// </summary>
        CreateListItem = 1,
        /// <summary>
        /// Allows you to view list items
        /// </summary>
        ViewListItems = 2,
        /// <summary>
        /// Allows you to edit list item messages by others and move list items to other channels
        /// </summary>
        ManageListItems = 4,
        /// <summary>
        /// Allows you to remove list items created by others
        /// </summary>
        RemoveListItems = 8,
        /// <summary>
        /// Allows you to complete list items created by others
        /// </summary>
        CompleteListItems = 16,
        /// <summary>
        /// Allows you to reorder list items
        /// </summary>
        ReorderListItems = 32
    }
}