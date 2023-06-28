// using System;

// namespace Guilded.Permissions;

// /// <summary>
// /// Represents channel permissions related to streaming channels.
// /// </summary>
// /// <seealso cref="AnnouncementPermissions" />
// /// <seealso cref="BotPermissions" />
// /// <seealso cref="BracketPermissions" />
// /// <seealso cref="CalendarPermissions" />
// /// <seealso cref="ChatPermissions" />
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
// /// <seealso cref="VoicePermissions" />
// /// <seealso cref="XpPermissions" />
// [Flags]
// public enum StreamPermissions
// {
//     /// <summary>
//     /// Allows you to view streams
//     /// </summary>
//     GetStream = 2,

//     /// <summary>
//     /// Allows you to add a stream and also talk in the stream channel
//     /// </summary>
//     AddStream = 1,

//     /// <summary>
//     /// Allows you to talk in stream channel
//     /// </summary>
//     AttendVoice = 16,

//     /// <summary>
//     /// Allows you to send message in stream channel
//     /// </summary>
//     CreateMessage = 32,

//     #region Properties
//     /// <summary>
//     /// All of the permissions combined.
//     /// </summary>
//     All = AddStream | GetStream | AttendVoice | CreateMessage,

//     /// <summary>
//     /// All of the manage permissions combined.
//     /// </summary>
//     /// <remarks>
//     /// <para>No permissions at this moment.</para>
//     /// </remarks>
//     Manage = 0,

//     /// <summary>
//     /// A simple permission combination allowing writing permissions and reading permissions.
//     /// </summary>
//     /// <remarks>
//     /// <para>Sets these permissions:</para>
//     /// <list type="bullet">
//     ///     <item>
//     ///         <description><see cref="GetStream" /></description>
//     ///     </item>
//     ///     <item>
//     ///         <description><see cref="AttendVoice" /></description>
//     ///     </item>
//     ///     <item>
//     ///         <description><see cref="CreateMessage" /></description>
//     ///     </item>
//     /// </list>
//     /// </remarks>
//     Basic = GetStream | AttendVoice | CreateMessage
//     #endregion
// }