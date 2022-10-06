using System;

namespace Guilded.Base.Permissions;

/// <summary>
/// Represents team permissions related to XP &amp; levels.
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
/// <seealso cref="SchedulingPermissions" />
/// <seealso cref="StreamPermissions" />
/// <seealso cref="VoicePermissions" />
[Flags]
public enum XpPermissions
{
    /// <summary>
    /// No given permissions.
    /// </summary>
    None = 0,

    /// <summary>
    /// Allows you to manage XP on server members
    /// </summary>
    ManageXp = 1,

    #region Methods
    /// <summary>
    /// All of the permissions combined.
    /// </summary>
    All = ManageXp,

    /// <summary>
    /// All of the manage permissions combined.
    /// </summary>
    /// <remarks>
    /// <para>Sets these permissions:</para>
    /// <list type="bullet">
    ///     <item>
    ///         <description><see cref="ManageXp" /></description>
    ///     </item>
    /// </list>
    /// </remarks>
    Manage = ManageXp
    #endregion
}