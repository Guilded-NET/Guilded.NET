using System;
using Guilded.Base.Servers;

namespace Guilded.Base.Permissions;

/// <summary>
/// Represents team and channel permissions related to server management.
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
/// <seealso cref="ListPermissions" />
/// <seealso cref="MatchmakingPermissions" />
/// <seealso cref="MediaPermissions" />
/// <seealso cref="RecruitmentPermissions" />
/// <seealso cref="SchedulingPermissions" />
/// <seealso cref="StreamPermissions" />
/// <seealso cref="VoicePermissions" />
/// <seealso cref="XpPermissions" />
[Flags]
public enum GeneralPermissions
{
    /// <summary>
    /// No given permissions.
    /// </summary>
    None = 0,

    /// <summary>
    /// Allows you to update server's settings
    /// </summary>
    ManageServer = 4,

    /// <summary>
    /// Allows you to directly invite members to the <see cref="Server">server</see>
    /// </summary>
    CreateInvite = 16,

    /// <summary>
    /// Allows you to kick or ban members from the <see cref="Server">server</see>
    /// </summary>
    RemoveMember = 32,

    /// <summary>
    /// Allows you to create new channels and edit or delete existing ones
    /// </summary>
    ManageChannel = 1024,

    /// <summary>
    /// Allows you to create new webhooks and edit or delete existing ones
    /// </summary>
    ManageWebhook = 2048,

    /// <summary>
    /// Allows you to create new groups and edit or delete existing ones
    /// </summary>
    ManageGroup = 4096,

    /// <summary>
    /// Allows you to use @everyone and @here mentions
    /// </summary>
    AddEveryoneMention = 8192,

    /// <summary>
    /// Allows you to update <see cref="Server">the server's</see> roles
    /// </summary>
    ManageRole = 16384,

    /// <summary>
    /// Allows you to bypass channel's slowmode settings
    /// </summary>
    ExcemptSlowmode = 65536,

    /// <summary>
    /// Allows you to see private messages
    /// </summary>
    GetPrivateMessage = 32768,

    #region Methods
    /// <summary>
    /// All of the permissions combined.
    /// </summary>
    All = ManageServer | RemoveMember | ManageChannel | ManageWebhook | ManageGroup | AddEveryoneMention | ManageRole,

    /// <summary>
    /// All of the manage permissions combined.
    /// </summary>
    /// <remarks>
    /// <para>Sets these permissions:</para>
    /// <list type="bullet">
    ///     <item>
    ///         <description><see cref="ManageServer" /></description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="RemoveMember" /></description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="ManageChannel" /></description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="ManageWebhook" /></description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="ManageGroup" /></description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="ManageRole" /></description>
    ///     </item>
    /// </list>
    /// </remarks>
    Manage = ManageServer | RemoveMember | ManageChannel | ManageWebhook | ManageGroup | ManageRole
    #endregion
}