// using System;

// namespace Guilded.Permissions;

// /// <summary>
// /// Represents channel permissions for chat &amp; text related things.
// /// </summary>
// /// <seealso cref="AnnouncementPermissions" />
// /// <seealso cref="BotPermissions" />
// /// <seealso cref="BracketPermissions" />
// /// <seealso cref="CalendarPermissions" />
// /// <seealso cref="CustomPermissions" />
// /// <seealso cref="DocPermissions" />
// /// <seealso cref="FormPermissions" />
// /// <seealso cref="ForumPermissions" />
// /// <seealso cref="GeneralPermissions" />
// /// <seealso cref="ListPermissions" />
// /// <seealso cref="MatchmakingPermissions" />
// /// <seealso cref="MediaPermissions" />
// /// <seealso cref="RecruitmentPermissions" />
// /// <seealso cref="SchedulingPermissions" />
// /// <seealso cref="StreamPermissions" />
// /// <seealso cref="VoicePermissions" />
// /// <seealso cref="XpPermissions" />
// [Flags]
// public enum ChatPermissions
// {
//     /// <summary>
//     /// No given permissions.
//     /// </summary>
//     None = 0,

//     /// <summary>
//     /// Allows you to read chat messages
//     /// </summary>
//     GetMessage = 2,

//     /// <summary>
//     /// Allows you to send chat messages
//     /// </summary>
//     CreateMessage = 1,

//     /// <summary>
//     /// Allows you to delete chat messages by other members or pin any message
//     /// </summary>
//     ManageMessage = 4,

//     /// <summary>
//     /// Allows you to create threads in the channel
//     /// </summary>
//     CreateThread = 16,

//     /// <summary>
//     /// Allows you to reply to threads in the channel
//     /// </summary>
//     CreateThreadMessage = 32,

//     /// <summary>
//     /// Allows you to archive and restore threads
//     /// </summary>
//     ManageThread = 64,

//     /// <summary>
//     /// Allows you to send messages with images and videos
//     /// </summary>
//     AddMedia = 128,

//     /// <summary>
//     /// Allows you to send private mentions and replies
//     /// </summary>
//     CreatePrivateMessage = 256,

//     #region Properties
//     /// <summary>
//     /// All of the permissions combined.
//     /// </summary>
//     All = CreateMessage | GetMessage | ManageMessage | CreateThread | CreateThreadMessage | ManageThread,

//     /// <summary>
//     /// All of the manage permissions combined.
//     /// </summary>
//     /// <remarks>
//     /// <para>Sets these permissions:</para>
//     /// <list type="bullet">
//     ///     <item>
//     ///         <description><see cref="ManageMessage" /></description>
//     ///     </item>
//     ///     <item>
//     ///         <description><see cref="CreateThread" /></description>
//     ///     </item>
//     ///     <item>
//     ///         <description><see cref="ManageThread" /></description>
//     ///     </item>
//     /// </list>
//     /// </remarks>
//     Manage = ManageMessage | CreateThread | ManageThread,

//     /// <summary>
//     /// A simple permission combination allowing writing permissions and reading permissions.
//     /// </summary>
//     /// <remarks>
//     /// <para>Sets these permissions:</para>
//     /// <list type="bullet">
//     ///     <item>
//     ///         <description><see cref="CreateMessage" /></description>
//     ///     </item>
//     ///     <item>
//     ///         <description><see cref="GetMessage" /></description>
//     ///     </item>
//     ///     <item>
//     ///         <description><see cref="CreateThreadMessage" /></description>
//     ///     </item>
//     /// </list>
//     /// </remarks>
//     Basic = CreateMessage | GetMessage | CreateThreadMessage
//     #endregion
// }