using System;

namespace Guilded.NET.Base.Permissions
{
    /// <summary>
    /// Permissions related to calendar.
    /// </summary>
    /// <remarks>
    /// <para>Defines channel permissions for calendar and event related things.</para>
    /// </remarks>
    [Flags]
    public enum CalendarPermissions
    {
        /// <summary>
        /// No given permissions.
        /// </summary>
        None = 0,
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
        /// <remarks>
        /// <para>Sets these permissions:</para>
        /// <list type="bullet">
        ///     <item>
        ///         <description><see cref="ManageEvents"/></description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="RemoveEvents"/></description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="EditRSVPs"/></description>
        ///     </item>
        /// </list>
        /// </remarks>
        Manage = ManageEvents | RemoveEvents | EditRSVPs,
        /// <summary>
        /// A simple permission combination allowing writing permissions and reading permissions.
        /// </summary>
        /// <remarks>
        /// <para>Sets these permissions:</para>
        /// <list type="bullet">
        ///     <item>
        ///         <description><see cref="CreateEvents"/></description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="ViewEvents"/></description>
        ///     </item>
        /// </list>
        /// </remarks>
        Basic = CreateEvents | ViewEvents
        #endregion
    }
}