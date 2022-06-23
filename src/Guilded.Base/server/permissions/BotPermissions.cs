using System;

namespace Guilded.Base.Permissions;

/// <summary>
/// Represents team permissions for flowbots related things.
/// </summary>
/// <seealso cref="AnnouncementPermissions" />
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
/// <seealso cref="SchedulingPermissions" />
/// <seealso cref="StreamPermissions" />
/// <seealso cref="VoicePermissions" />
/// <seealso cref="XpPermissions" />
[Flags]
public enum BotPermissions
{
    /// <summary>
    /// No given permissions.
    /// </summary>
    None = 0,

    /// <summary>
    /// Allows you to create and edit bots for automated workflows.
    /// NOTE: For now, bots do not enforce permissions. Anyone with this permission
    /// can create bots to work around their role's existing permissions.
    /// </summary>
    ManageBots = 1,

    #region Methods
    /// <summary>
    /// All of the permissions combined.
    /// </summary>
    All = ManageBots
    #endregion
}