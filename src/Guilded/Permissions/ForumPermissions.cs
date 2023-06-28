// using System;

// namespace Guilded.Permissions;

// /// <summary>
// /// Represents channel permissions related to forums.
// /// </summary>
// /// <seealso cref="AnnouncementPermissions" />
// /// <seealso cref="BotPermissions" />
// /// <seealso cref="BracketPermissions" />
// /// <seealso cref="CalendarPermissions" />
// /// <seealso cref="ChatPermissions" />
// /// <seealso cref="CustomPermissions" />
// /// <seealso cref="DocPermissions" />
// /// <seealso cref="FormPermissions" />
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
// public enum ForumPermissions
// {
//     /// <summary>
//     /// No given permissions.
//     /// </summary>
//     None = 0,

//     /// <summary>
//     /// Allows you to read forums
//     /// </summary>
//     GetTopic = 2,

//     /// <summary>
//     /// Allows you to create forum topics
//     /// </summary>
//     CreateTopic = 1,

//     /// <summary>
//     /// Allows you to remove topics and replies by others, or move them to other channels
//     /// </summary>
//     ManageTopic = 8,

//     /// <summary>
//     /// Allows you to sticky a topic
//     /// </summary>
//     PinTopic = 16,

//     /// <summary>
//     /// Allows you to lock a topic
//     /// </summary>
//     LockTopic = 32,

//     /// <summary>
//     /// Allows you to create forum topic replies
//     /// </summary>
//     CreateTopicComment = 64,

//     #region Properties
//     /// <summary>
//     /// All of the permissions combined.
//     /// </summary>
//     All = CreateTopic | GetTopic | ManageTopic | PinTopic | LockTopic | CreateTopicComment,

//     /// <summary>
//     /// All of the manage permissions combined.
//     /// </summary>
//     /// <remarks>
//     /// <para>Sets these permissions:</para>
//     /// <list type="bullet">
//     ///     <item>
//     ///         <description><see cref="ManageTopic" /></description>
//     ///     </item>
//     ///     <item>
//     ///         <description><see cref="PinTopic" /></description>
//     ///     </item>
//     ///     <item>
//     ///         <description><see cref="LockTopic" /></description>
//     ///     </item>
//     /// </list>
//     /// </remarks>
//     Manage = ManageTopic | PinTopic | LockTopic,

//     /// <summary>
//     /// A simple permission combination allowing writing permissions and reading permissions.
//     /// </summary>
//     /// <remarks>
//     /// <para>Sets these permissions:</para>
//     /// <list type="bullet">
//     ///     <item>
//     ///         <description><see cref="CreateTopic" /></description>
//     ///     </item>
//     ///     <item>
//     ///         <description><see cref="GetTopic" /></description>
//     ///     </item>
//     ///     <item>
//     ///         <description><see cref="CreateTopicComment" /></description>
//     ///     </item>
//     /// </list>
//     /// </remarks>
//     Basic = CreateTopic | GetTopic | CreateTopicComment
//     #endregion
// }