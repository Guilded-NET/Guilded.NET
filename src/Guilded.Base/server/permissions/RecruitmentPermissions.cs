using System;

namespace Guilded.Base.Permissions;

/// <summary>
/// Represents team permissions related to recruiting in applications.
/// </summary>
/// <seealso cref="AnnouncementPermissions" />
/// <seealso cref="BotPermissions" />
/// <seealso cref="BracketPermissions" />
/// <seealso cref="CalendarPermissions" />
/// <seealso cref="ChatPermissions" />
/// <seealso cref="CustomPermissions" />
/// <seealso cref="DocPermissions" />
/// <seealso cref="FormPermissions" />
/// <seealso cref="ForumPermissions" />
/// <seealso cref="GeneralPermissions" />
/// <seealso cref="ListPermissions" />
/// <seealso cref="MatchmakingPermissions" />
/// <seealso cref="MediaPermissions" />
/// <seealso cref="SchedulingPermissions" />
/// <seealso cref="StreamPermissions" />
/// <seealso cref="VoicePermissions" />
/// <seealso cref="XpPermissions" />
[Flags]
public enum RecruitmentPermissions
{
    /// <summary>
    /// No given permissions.
    /// </summary>
    None = 0,

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
    /// <remarks>
    /// <para>Sets these permissions:</para>
    /// <list type="bullet">
    ///     <item>
    ///         <description><see cref="ApproveApplications" /></description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="EditApplications" /></description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="ModifyStatus" /></description>
    ///     </item>
    /// </list>
    /// </remarks>
    Manage = ApproveApplications | EditApplications | ModifyStatus
    #endregion
}