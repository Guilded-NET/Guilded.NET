// using System;

// namespace Guilded.Permissions;

// /// <summary>
// /// Represents channel permissions related to documents.
// /// </summary>
// /// <seealso cref="AnnouncementPermissions" />
// /// <seealso cref="BotPermissions" />
// /// <seealso cref="BracketPermissions" />
// /// <seealso cref="CalendarPermissions" />
// /// <seealso cref="ChatPermissions" />
// /// <seealso cref="CustomPermissions" />
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
// public enum DocPermissions
// {
//     /// <summary>
//     /// No given permissions.
//     /// </summary>
//     None = 0,

//     /// <summary>
//     /// Allows you to view docs
//     /// </summary>
//     GetDoc = 2,

//     /// <summary>
//     /// Allows you to create docs
//     /// </summary>
//     CreateDoc = 1,

//     /// <summary>
//     /// Allows you to update docs created by others and move them to other channels
//     /// </summary>
//     ManageDoc = 4,

//     /// <summary>
//     /// Allows you to remove docs created by others
//     /// </summary>
//     RemoveDoc = 8,

//     #region Properties
//     /// <summary>
//     /// All of the permissions combined.
//     /// </summary>
//     All = CreateDoc | GetDoc | ManageDoc | RemoveDoc,

//     /// <summary>
//     /// All of the manage permissions combined.
//     /// </summary>
//     /// <remarks>
//     /// <para>Sets these permissions:</para>
//     /// <list type="bullet">
//     ///     <item>
//     ///         <description><see cref="ManageDoc" /></description>
//     ///     </item>
//     ///     <item>
//     ///         <description><see cref="RemoveDoc" /></description>
//     ///     </item>
//     /// </list>
//     /// </remarks>
//     Manage = ManageDoc | RemoveDoc,

//     /// <summary>
//     /// A simple permission combination allowing writing permissions and reading permissions.
//     /// </summary>
//     /// <remarks>
//     /// <para>Sets these permissions:</para>
//     /// <list type="bullet">
//     ///     <item>
//     ///         <description><see cref="CreateDoc" /></description>
//     ///     </item>
//     ///     <item>
//     ///         <description><see cref="GetDoc" /></description>
//     ///     </item>
//     /// </list>
//     /// </remarks>
//     Basic = CreateDoc | GetDoc
//     #endregion
// }