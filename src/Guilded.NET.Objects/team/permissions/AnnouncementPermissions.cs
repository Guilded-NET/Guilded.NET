using System;

namespace Guilded.NET.Objects.Permissions
{
    /// <summary>
    /// Permissions related to announcement channel(and Overview channel, if it's CreateAnnouncements).
    /// </summary>
    [Flags]
    public enum AnnounPermissions
    {
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
        ManageAnnouncements = 4
    }
}