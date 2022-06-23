using System;

namespace Guilded.Base.Permissions;

/// <summary>
/// Represents channel permissions related to documents.
/// </summary>
/// <seealso cref="AnnouncementPermissions" />
/// <seealso cref="BotPermissions" />
/// <seealso cref="BracketPermissions" />
/// <seealso cref="CalendarPermissions" />
/// <seealso cref="ChatPermissions" />
/// <seealso cref="CustomPermissions" />
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
public enum DocPermissions
{
    /// <summary>
    /// No given permissions.
    /// </summary>
    None = 0,

    /// <summary>
    /// Allows you to create docs
    /// </summary>
    CreateDocs = 1,

    /// <summary>
    /// Allows you to view docs
    /// </summary>
    ViewDocs = 2,

    /// <summary>
    /// Allows you to update docs created by others and move them to other channels
    /// </summary>
    ManageDocs = 4,

    /// <summary>
    /// Allows you to remove docs created by others
    /// </summary>
    RemoveDocs = 8,

    #region Methods
    /// <summary>
    /// All of the permissions combined.
    /// </summary>
    All = CreateDocs | ViewDocs | ManageDocs | RemoveDocs,

    /// <summary>
    /// All of the manage permissions combined.
    /// </summary>
    /// <remarks>
    /// <para>Sets these permissions:</para>
    /// <list type="bullet">
    ///     <item>
    ///         <description><see cref="ManageDocs" /></description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="RemoveDocs" /></description>
    ///     </item>
    /// </list>
    /// </remarks>
    Manage = ManageDocs | RemoveDocs,

    /// <summary>
    /// A simple permission combination allowing writing permissions and reading permissions.
    /// </summary>
    /// <remarks>
    /// <para>Sets these permissions:</para>
    /// <list type="bullet">
    ///     <item>
    ///         <description><see cref="CreateDocs" /></description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="ViewDocs" /></description>
    ///     </item>
    /// </list>
    /// </remarks>
    Basic = CreateDocs | ViewDocs
    #endregion
}