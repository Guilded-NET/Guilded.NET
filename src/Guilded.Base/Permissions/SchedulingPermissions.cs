using System;

namespace Guilded.Base.Permissions;

/// <summary>
/// Represents channel permissions related to availability in scheduling channels.
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
/// <seealso cref="RecruitmentPermissions" />
/// <seealso cref="StreamPermissions" />
/// <seealso cref="VoicePermissions" />
/// <seealso cref="XpPermissions" />
[Flags]
public enum SchedulingPermissions
{
    /// <summary>
    /// No given permissions.
    /// </summary>
    None = 0,

    /// <summary>
    /// Allows you to view server member's schedule
    /// </summary>
    GetSchedule = 2,

    /// <summary>
    /// Allows you to let server know your available schedule
    /// </summary>
    CreateSchedule = 1,

    /// <summary>
    /// Allows you to remove availabilities created by others
    /// </summary>
    RemoveSchedule = 8,

    #region Methods
    /// <summary>
    /// All of the permissions combined.
    /// </summary>
    All = CreateSchedule | GetSchedule | RemoveSchedule,

    /// <summary>
    /// All of the manage permissions combined.
    /// </summary>
    /// <remarks>
    /// <para>Sets these permissions:</para>
    /// <list type="bullet">
    ///     <item>
    ///         <description><see cref="RemoveSchedule" /></description>
    ///     </item>
    /// </list>
    /// </remarks>
    Manage = RemoveSchedule,

    /// <summary>
    /// A simple permission combination allowing writing permissions and reading permissions/
    /// </summary>
    /// <remarks>
    /// <para>Sets these permissions:</para>
    /// <list type="bullet">
    ///     <item>
    ///         <description><see cref="CreateSchedule" /></description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="GetSchedule" /></description>
    ///     </item>
    /// </list>
    /// </remarks>
    Basic = CreateSchedule | GetSchedule
    #endregion
}