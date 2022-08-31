using System;
using Guilded.Base.Servers;

namespace Guilded.Base.Permissions;

/// <summary>
/// Represents team permissions related to tournaments &amp; scrims.
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
/// <seealso cref="MediaPermissions" />
/// <seealso cref="RecruitmentPermissions" />
/// <seealso cref="SchedulingPermissions" />
/// <seealso cref="StreamPermissions" />
/// <seealso cref="VoicePermissions" />
/// <seealso cref="XpPermissions" />
[Flags]
public enum MatchmakingPermissions
{
    /// <summary>
    /// No given permissions.
    /// </summary>
    None = 0,

    /// <summary>
    /// Allows you to create matchmaking scrims
    /// </summary>
    CreateScrims = 1,

    /// <summary>
    /// Allows you to use <see cref="Server">the server</see> to create and manage tournaments
    /// </summary>
    RegisterForTournaments = 4,

    /// <summary>
    /// Allows you to register <see cref="Server">the server</see> for tournaments
    /// </summary>
    CreateTournaments = 16,

    #region Methods
    /// <summary>
    /// All of the permissions combined.
    /// </summary>
    All = CreateScrims | RegisterForTournaments | CreateTournaments
    #endregion
}