using System;

namespace Guilded.Base.Permissions;

/// <summary>
/// Represents team permissions for tournament bracket related things.
/// </summary>
/// <seealso cref="AnnouncementPermissions" />
/// <seealso cref="BotPermissions" />
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
/// <seealso cref="SchedulingPermissions" />
/// <seealso cref="StreamPermissions" />
/// <seealso cref="VoicePermissions" />
/// <seealso cref="XpPermissions" />
[Flags]
public enum BracketPermissions
{
    /// <summary>
    /// No given permissions.
    /// </summary>
    None = 0,

    /// <summary>
    /// Allows you to report match scores on behalf of your server
    /// </summary>
    ReportScore = 1,

    /// <summary>
    /// Allows you to view tournament brackets
    /// </summary>
    GetBracket = 2,

    #region Methods
    /// <summary>
    /// All of the permissions combined.
    /// </summary>
    All = ReportScore | GetBracket
    #endregion
}