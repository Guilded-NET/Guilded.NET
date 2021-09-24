using System;

namespace Guilded.NET.Base.Permissions
{
    /// <summary>
    /// Permissions related to scheduling.
    /// </summary>
    /// <remarks>
    /// <para>Defines permissions related to availability in scheduling channels.</para>
    /// </remarks>
    [Flags]
    public enum SchedulingPermissions
    {
        /// <summary>
        /// No given permissions.
        /// </summary>
        None = 0,
        /// <summary>
        /// Allows you to let server know your available schedule
        /// </summary>
        CreateSchedule = 1,
        /// <summary>
        /// Allows you to view server member's schedule
        /// </summary>
        ViewSchedules = 2,
        /// <summary>
        /// Allows you to remove availabilities created by others
        /// </summary>
        DeleteSchedule = 8,

        #region Additional
        /// <summary>
        /// All of the permissions combined.
        /// </summary>
        All = CreateSchedule | ViewSchedules | DeleteSchedule,
        /// <summary>
        /// All of the manage permissions combined.
        /// </summary>
        Manage = DeleteSchedule,
        /// <summary>
        /// A simple permission combination allowing writing permissions and reading permissions/
        /// </summary>
        Basic = CreateSchedule | ViewSchedules
        #endregion
    }
}