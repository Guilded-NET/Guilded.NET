namespace Guilded.NET.Objects.Permissions {
    /// <summary>
    /// Event/calendar channel permissions.
    /// </summary>
    public enum CalendarPermissions {
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
        EditRSVPs = 16
    }
}