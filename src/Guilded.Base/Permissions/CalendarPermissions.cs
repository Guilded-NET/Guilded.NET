using System;

namespace Guilded.Base.Permissions;

/// <summary>
/// Represents channel permissions for calendar and event related things.
/// </summary>
/// <seealso cref="AnnouncementPermissions" />
/// <seealso cref="BotPermissions" />
/// <seealso cref="BracketPermissions" />
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
public enum CalendarPermissions
{
    /// <summary>
    /// No given permissions.
    /// </summary>
    None = 0,

    /// <summary>
    /// Allows you to view events
    /// </summary>
    GetEvent = 2,

    /// <summary>
    /// Allows you to create events
    /// </summary>
    CreateEvent = 1,

    /// <summary>
    /// Allows you to update events created by others and move them to other channel
    /// </summary>
    ManageEvent = 4,

    /// <summary>
    /// Allows you to remove events created by others
    /// </summary>
    RemoveEvent = 8,

    /// <summary>
    /// Allows you to edit RSVP status for members in an event
    /// </summary>
    ManageRsvp = 16,

    #region Methods
    /// <summary>
    /// All of the permissions combined.
    /// </summary>
    All = CreateEvent | GetEvent | ManageEvent | RemoveEvent | ManageRsvp,

    /// <summary>
    /// All of the manage permissions combined.
    /// </summary>
    /// <remarks>
    /// <para>Sets these permissions:</para>
    /// <list type="bullet">
    ///     <item>
    ///         <description><see cref="ManageEvent" /></description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="RemoveEvent" /></description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="ManageRsvp" /></description>
    ///     </item>
    /// </list>
    /// </remarks>
    ManageAll = ManageEvent | RemoveEvent | ManageRsvp,

    /// <summary>
    /// A simple permission combination allowing writing permissions and reading permissions.
    /// </summary>
    /// <remarks>
    /// <para>Sets these permissions:</para>
    /// <list type="bullet">
    ///     <item>
    ///         <description><see cref="CreateEvent" /></description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="GetEvent" /></description>
    ///     </item>
    /// </list>
    /// </remarks>
    Basic = CreateEvent | GetEvent
    #endregion
}