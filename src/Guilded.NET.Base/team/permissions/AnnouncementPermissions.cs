using System;

namespace Guilded.NET.Base.Permissions
{
    /// <summary>
    /// Permissions related to announcements.
    /// </summary>
    /// <remarks>
    /// <para>Defines permissions for announcement related things.</para>
    /// </remarks>
    [Flags]
    public enum AnnounPermissions
    {
        /// <summary>
        /// No given permissions.
        /// </summary>
        None = 0,
        /// <summary>
        /// Allows you to create and remove announcements
        /// </summary>
        CreateAnnouncements = 1,
        /// <summary>
        /// Allows you to view announcements
        /// </summary>
        ViewAnnouncements = 2,
        /// <summary>
        /// Allows you to delete announcements by other members or pin any announcement
        /// </summary>
        ManageAnnouncements = 4,

        #region Additional
        /// <summary>
        /// All of the permissions combined.
        /// </summary>
        All = CreateAnnouncements | ViewAnnouncements | ManageAnnouncements,
        /// <summary>
        /// All of the manage permissions combined.
        /// </summary>
        Manage = ManageAnnouncements,
        /// <summary>
        /// A simple permission combination allowing writing permissions and reading permissions.
        /// </summary>
        Basic = CreateAnnouncements
        #endregion
    }
}