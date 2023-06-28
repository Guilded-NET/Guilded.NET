// using System;

// namespace Guilded.Permissions;

// /// <summary>
// /// Represents channel permissions related to media in media channels.
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
// /// <seealso cref="RecruitmentPermissions" />
// /// <seealso cref="SchedulingPermissions" />
// /// <seealso cref="StreamPermissions" />
// /// <seealso cref="VoicePermissions" />
// /// <seealso cref="XpPermissions" />
// [Flags]
// public enum MediaPermissions
// {
//     /// <summary>
//     /// No given permissions.
//     /// </summary>
//     None = 0,

//     /// <summary>
//     /// Allows you to see media
//     /// </summary>
//     GetMedia = 2,

//     /// <summary>
//     /// Allows you to create media
//     /// </summary>
//     CreateMedia = 1,

//     /// <summary>
//     /// Allows you to edit media created by others and move media items to other channels
//     /// </summary>
//     ManageMedia = 4,

//     /// <summary>
//     /// Allows you to remove media created by others
//     /// </summary>
//     RemoveMedia = 8,

//     #region Properties
//     /// <summary>
//     /// All of the permissions combined.
//     /// </summary>
//     All = CreateMedia | GetMedia | ManageMedia | RemoveMedia,

//     /// <summary>
//     /// All of the manage permissions combined.
//     /// </summary>
//     /// <remarks>
//     /// <para>Sets these permissions:</para>
//     /// <list type="bullet">
//     ///     <item>
//     ///         <description><see cref="ManageMedia" /></description>
//     ///     </item>
//     ///     <item>
//     ///         <description><see cref="RemoveMedia" /></description>
//     ///     </item>
//     /// </list>
//     /// </remarks>
//     Manage = ManageMedia | RemoveMedia,

//     /// <summary>
//     /// A simple permission combination allowing writing permissions and reading permissions.
//     /// </summary>
//     /// <remarks>
//     /// <para>Sets these permissions:</para>
//     /// <list type="bullet">
//     ///     <item>
//     ///         <description><see cref="CreateMedia" /></description>
//     ///     </item>
//     ///     <item>
//     ///         <description><see cref="GetMedia" /></description>
//     ///     </item>
//     /// </list>
//     /// </remarks>
//     Basic = CreateMedia | GetMedia
//     #endregion
// }