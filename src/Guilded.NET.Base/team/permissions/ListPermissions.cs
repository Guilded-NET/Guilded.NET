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
        /// No given permissions.
        /// </summary>
        None = 0,
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
        ReorderListItems = 32,

        #region Additional
        /// <summary>
        /// All of the permissions combined.
        /// </summary>
        All = CreateListItem | ViewListItems | ManageListItems | RemoveListItems | CompleteListItems | ReorderListItems,
        /// <summary>
        /// All of the manage permissions combined.
        /// </summary>
        Manage = ManageListItems | RemoveListItems | ReorderListItems,
        /// <summary>
        /// A simple permission combination allowing writing permissions and reading permissions.
        /// </summary>
        Basic = CreateListItem | ViewListItems | CompleteListItems
        #endregion
    }
}