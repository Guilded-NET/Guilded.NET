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
        DeleteSchedule = 8
    }
}