using System;

namespace Guilded.Base.Permissions;

/// <summary>
/// Represents channel permissions for announcement related things.
/// </summary>
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
/// <seealso cref="XpPermissions" />
[Flags]
public enum AnnouncementPermissions
{
    /// <summary>
    /// No given permissions.
    /// </summary>
    None = 0,

    /// <summary>
    /// Allows you to create and remove announcements
    /// </summary>
    CreateAnnouncements = 1,

    /// <summary>
    /// Allows you to view announcements
    /// </summary>
    ViewAnnouncements = 2,

    /// <summary>
    /// Allows you to delete announcements by other members or pin any announcement
    /// </summary>
    ManageAnnouncements = 4,

    #region Methods
    /// <summary>
    /// All of the permissions combined.
    /// </summary>
    All = CreateAnnouncements | ViewAnnouncements | ManageAnnouncements,

    /// <summary>
    /// All of the manage permissions combined.
    /// </summary>
    /// <remarks>
    /// <para>Sets these permissions:</para>
    /// <list type="bullet">
    ///     <item>
    ///         <description><see cref="ManageAnnouncements" /></description>
    ///     </item>
    /// </list>
    /// </remarks>
    Manage = ManageAnnouncements,

    /// <summary>
    /// A simple permission combination allowing writing permissions and reading permissions.
    /// </summary>
    /// <remarks>
    /// <para>Sets these permissions:</para>
    /// <list type="bullet">
    ///     <item>
    ///         <description><see cref="CreateAnnouncements" /></description>
    ///     </item>
    /// </list>
    /// </remarks>
    Basic = CreateAnnouncements
    #endregion
}