// using System;

// namespace Guilded.Permissions;

// /// <summary>
// /// Represents channel permissions for announcement related things.
// /// </summary>
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
// /// <seealso cref="StreamPermissions" />
// /// <seealso cref="VoicePermissions" />
// /// <seealso cref="XpPermissions" />
// [Flags]
// public enum AnnouncementPermissions
// {
//     /// <summary>
//     /// No given permissions.
//     /// </summary>
//     None = 0,

//     /// <summary>
//     /// Allows you to view announcements
//     /// </summary>
//     GetAnnouncement = 2,

//     /// <summary>
//     /// Allows you to create and remove announcements
//     /// </summary>
//     CreateAnnouncement = 1,

//     /// <summary>
//     /// Allows you to delete announcements by other members or pin any announcement
//     /// </summary>
//     ManageAnnouncement = 4,

//     #region Properties
//     /// <summary>
//     /// All of the permissions combined.
//     /// </summary>
//     All = CreateAnnouncement | GetAnnouncement | ManageAnnouncement,

//     /// <summary>
//     /// All of the manage permissions combined.
//     /// </summary>
//     /// <remarks>
//     /// <para>Sets these permissions:</para>
//     /// <list type="bullet">
//     ///     <item>
//     ///         <description><see cref="ManageAnnouncement" /></description>
//     ///     </item>
//     /// </list>
//     /// </remarks>
//     Manage = ManageAnnouncement,

//     /// <summary>
//     /// A simple permission combination allowing writing permissions and reading permissions.
//     /// </summary>
//     /// <remarks>
//     /// <para>Sets these permissions:</para>
//     /// <list type="bullet">
//     ///     <item>
//     ///         <description><see cref="CreateAnnouncement" /></description>
//     ///     </item>
//     /// </list>
//     /// </remarks>
//     Basic = CreateAnnouncement
//     #endregion
// }