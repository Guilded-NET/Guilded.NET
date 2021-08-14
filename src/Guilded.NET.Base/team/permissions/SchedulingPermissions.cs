using System;

namespace Guilded.NET.Base.Permissions
{
    /// <summary>
    /// Permissions for scheduling channel.
    /// </summary>
    [Flags]
    public enum SchedulingPermissions
    {
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