using System;

namespace Guilded.Base.Permissions
{
    /// <summary>
    /// Permissions related to announcements.
    /// </summary>
    /// <remarks>
    /// <para>Defines channel permissions for announcement related things.</para>
    /// </remarks>
    [Flags]
    public enum AnnouncementPermissions
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
        /// <remarks>
        /// <para>Sets these permissions:</para>
        /// <list type="bullet">
        ///     <item>
        ///         <description><see cref="ManageAnnouncements"/></description>
        ///     </item>
        /// </list>
        /// </remarks>
        Manage = ManageAnnouncements,
        /// <summary>
        /// A simple permission combination allowing writing permissions and reading permissions.
        /// </summary>
        /// <remarks>
        /// <para>Sets these permissions:</para>
        /// <list type="bullet">
        ///     <item>
        ///         <description><see cref="CreateAnnouncements"/></description>
        ///     </item>
        /// </list>
        /// </remarks>
        Basic = CreateAnnouncements
        #endregion
    }
}