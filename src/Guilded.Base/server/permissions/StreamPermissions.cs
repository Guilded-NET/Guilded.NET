using System;

namespace Guilded.Base.Permissions;

/// <summary>
/// Represents channel permissions related to streaming channels.
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
/// <seealso cref="VoicePermissions" />
/// <seealso cref="XpPermissions" />
[Flags]
public enum StreamPermissions
{
    /// <summary>
    /// Allows you to add a stream and also talk in the stream channel
    /// </summary>
    AddStream = 1,
    /// <summary>
    /// Allows you to view streams
    /// </summary>
    ViewStreams = 2,
    /// <summary>
    /// Allows you to talk in stream channel
    /// </summary>
    JoinVoice = 16,
    /// <summary>
    /// Allows you to send message in stream channel
    /// </summary>
    SendMessages = 32,

    #region Additional
    /// <summary>
    /// All of the permissions combined.
    /// </summary>
    All = AddStream | ViewStreams | JoinVoice | SendMessages,
    /// <summary>
    /// All of the manage permissions combined.
    /// </summary>
    /// <remarks>
    /// <para>No permissions at this moment.</para>
    /// </remarks>
    Manage = 0,
    /// <summary>
    /// A simple permission combination allowing writing permissions and reading permissions.
    /// </summary>
    /// <remarks>
    /// <para>Sets these permissions:</para>
    /// <list type="bullet">
    ///     <item>
    ///         <description><see cref="ViewStreams" /></description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="JoinVoice" /></description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="SendMessages" /></description>
    ///     </item>
    /// </list>
    /// </remarks>
    Basic = ViewStreams | JoinVoice | SendMessages
    #endregion
}