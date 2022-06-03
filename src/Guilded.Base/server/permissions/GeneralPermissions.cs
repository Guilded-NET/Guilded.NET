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
    UpdateServer = 4,

    /// <summary>
    /// Allows you to directly invite members to <see cref="Server">the server</see>
    /// </summary>
    InviteMembers = 16,

    /// <summary>
    /// Allows you to kick or ban members from <see cref="Server">the server</see>
    /// </summary>
    KickBanMembers = 32,

    /// <summary>
    /// Allows you to create new channels and edit or delete existing ones
    /// </summary>
    ManageChannels = 1024,

    /// <summary>
    /// Allows you to create new webhooks and edit or delete existing ones
    /// </summary>
    ManageWebhooks = 2048,

    /// <summary>
    /// Allows you to create new groups and edit or delete existing ones
    /// </summary>
    ManageGroups = 4096,

    /// <summary>
    /// Allows you to use @everyone and @here mentions
    /// </summary>
    MentionEveryoneHere = 8192,

    /// <summary>
    /// Allows you to update <see cref="Server">the server's</see> roles
    /// </summary>
    ManageRoles = 16384,

    /// <summary>
    /// Allows you to bypass channel's slowmode settings
    /// </summary>
    SlowmodeException = 65536,

    /// <summary>
    /// Allows you to see private messages
    /// </summary>
    AccessModeratorView = 32768,

    #region Additional
    /// <summary>
    /// All of the permissions combined.
    /// </summary>
    All = UpdateServer | KickBanMembers | ManageChannels | ManageWebhooks | ManageGroups | MentionEveryoneHere | ManageRoles,

    /// <summary>
    /// All of the manage permissions combined.
    /// </summary>
    /// <remarks>
    /// <para>Sets these permissions:</para>
    /// <list type="bullet">
    ///     <item>
    ///         <description><see cref="UpdateServer" /></description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="KickBanMembers" /></description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="ManageChannels" /></description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="ManageWebhooks" /></description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="ManageGroups" /></description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="ManageRoles" /></description>
    ///     </item>
    /// </list>
    /// </remarks>
    Manage = UpdateServer | KickBanMembers | ManageChannels | ManageWebhooks | ManageGroups | ManageRoles
    #endregion
}