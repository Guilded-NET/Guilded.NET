using System;

namespace Guilded.NET.Base.Permissions
{
    /// <summary>
    /// Event/calendar channel permissions.
    /// </summary>
    [Flags]
    public enum CalendarPermissions
    {
        /// <summary>
        /// Allows you to create events
        /// </summary>
        CreateEvents = 1,
        /// <summary>
        /// Allows you to view events
        /// </summary>
        ViewEvents = 2,
        /// <summary>
        /// Allows you to update events created by others and move them to other channel
        /// </summary>
        ManageEvents = 4,
        /// <summary>
        /// Allows you to remove events created by others
        /// </summary>
        RemoveEvents = 8,
        /// <summary>
        /// Allows you to edit RSVP status for members in an event
        /// </summary>
        EditRSVPs = 16,

        #region Additional
        /// <summary>
        /// All of the permissions combined.
        /// </summary>
        All = CreateEvents | ViewEvents | ManageEvents | RemoveEvents | EditRSVPs,
        /// <summary>
        /// All of the manage permissions combined.
        /// </summary>
        Manage = ManageEvents | RemoveEvents | EditRSVPs,
        /// <summary>
        /// A simple permission combination allowing writing permissions and reading permissions.
        /// </summary>
        Basic = CreateEvents | ViewEvents
        #endregion
    }
}