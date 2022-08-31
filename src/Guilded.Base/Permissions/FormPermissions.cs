using System;

namespace Guilded.Base.Permissions;

/// <summary>
/// Represents team permissions related to forms &amp; polls.
/// </summary>
/// <seealso cref="AnnouncementPermissions" />
/// <seealso cref="BotPermissions" />
/// <seealso cref="BracketPermissions" />
/// <seealso cref="CalendarPermissions" />
/// <seealso cref="ChatPermissions" />
/// <seealso cref="CustomPermissions" />
/// <seealso cref="DocPermissions" />
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
public enum FormPermissions
{
    /// <summary>
    /// No given permissions.
    /// </summary>
    None = 0,

    /// <summary>
    /// Allows you to view all form responses
    /// </summary>
    FormResponses = 2,

    /// <summary>
    /// Allows you to view all poll results
    /// </summary>
    PollResults = 16,

    #region Methods
    /// <summary>
    /// All of the permissions combined.
    /// </summary>
    All = FormResponses | PollResults,

    /// <summary>
    /// A simple permission combination allowing writing permissions and reading permissions.
    /// </summary>
    /// <remarks>
    /// <para>Sets these permissions:</para>
    /// <list type="bullet">
    ///     <item>
    ///         <description><see cref="PollResults" /></description>
    ///     </item>
    /// </list>
    /// </remarks>
    Basic = PollResults
    #endregion
}