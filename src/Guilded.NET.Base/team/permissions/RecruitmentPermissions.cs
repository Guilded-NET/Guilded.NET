using System;

namespace Guilded.NET.Base.Permissions
{
    /// <summary>
    /// All of the permissions related to applications and recruiting.
    /// </summary>
    [Flags]
    public enum RecruitmentPermissions
    {
        /// <summary>
        /// Allows you to approve server and game applications
        /// </summary>
        ApproveApplications = 1,
        /// <summary>
        /// Allows you to view server and game applications
        /// </summary>
        ViewApplications = 2,
        /// <summary>
        /// Allows you to edit server and game applications, and toggle accepting applications
        /// </summary>
        EditApplications = 4,
        /// <summary>
        /// Allows you to indicate interest in a player instead of upvote
        /// </summary>
        IndicateInterest = 16,
        /// <summary>
        /// Allows you to modify the Find Player status for server listing card
        /// </summary>
        ModifyStatus = 32,

        #region Additional
        /// <summary>
        /// All of the permissions combined.
        /// </summary>
        All = ApproveApplications | ViewApplications | EditApplications | IndicateInterest | ModifyStatus,
        /// <summary>
        /// All of the manage permissions combined.
        /// </summary>
        Manage = ApproveApplications | EditApplications | ModifyStatus
        #endregion
    }
}