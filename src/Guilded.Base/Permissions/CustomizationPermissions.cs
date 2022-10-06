using System;

namespace Guilded.Base.Permissions;

/// <summary>
/// Represents team permissions related to nickname and emoji customizations.
/// </summary>
/// <seealso cref="AnnouncementPermissions" />
/// <seealso cref="BotPermissions" />
/// <seealso cref="BracketPermissions" />
/// <seealso cref="CalendarPermissions" />
/// <seealso cref="ChatPermissions" />
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
public enum CustomPermissions
{
    /// <summary>
    /// No given permissions.
    /// </summary>
    None = 0,

    /// <summary>
    /// Allows the creation and management of server emoji
    /// </summary>
    ManageEmoji = 1,

    /// <summary>
    /// Members with this permission can change their own nickname.
    /// </summary>
    ManageSelfNickname = 16,

    /// <summary>
    /// Members with this permission can change the nickname of others.
    /// </summary>
    ManageMemberNickname = 32,

    #region Methods
    /// <summary>
    /// All of the permissions combined.
    /// </summary>
    All = ManageEmoji | ManageSelfNickname | ManageMemberNickname
    #endregion
}