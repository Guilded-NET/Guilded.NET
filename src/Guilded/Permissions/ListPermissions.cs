// using System;

// namespace Guilded.Permissions;

// /// <summary>
// /// Represents channel permissions related to lists/tasks in list channels.
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
// /// <seealso cref="MatchmakingPermissions" />
// /// <seealso cref="MediaPermissions" />
// /// <seealso cref="RecruitmentPermissions" />
// /// <seealso cref="SchedulingPermissions" />
// /// <seealso cref="StreamPermissions" />
// /// <seealso cref="VoicePermissions" />
// /// <seealso cref="XpPermissions" />
// [Flags]
// public enum ListPermissions
// {
//     /// <summary>
//     /// No given permissions.
//     /// </summary>
//     None = 0,

//     /// <summary>
//     /// Allows you to view list items
//     /// </summary>
//     GetItem = 2,

//     /// <summary>
//     /// Allows you to create list items
//     /// </summary>
//     CreateItem = 1,

//     /// <summary>
//     /// Allows you to edit list item messages by others and move list items to other channels
//     /// </summary>
//     ManageItem = 4,

//     /// <summary>
//     /// Allows you to remove list items created by others
//     /// </summary>
//     RemoveItem = 8,

//     /// <summary>
//     /// Allows you to complete list items created by others
//     /// </summary>
//     CompleteItem = 16,

//     /// <summary>
//     /// Allows you to reorder list items
//     /// </summary>
//     MoveItem,

//     #region Properties
//     /// <summary>
//     /// All of the permissions combined.
//     /// </summary>
//     All = CreateItem | GetItem | ManageItem | RemoveItem | CompleteItem | MoveItem,

//     /// <summary>
//     /// All of the manage permissions combined.
//     /// </summary>
//     /// <remarks>
//     /// <para>Sets these permissions:</para>
//     /// <list type="bullet">
//     ///     <item>
//     ///         <description><see cref="ManageItem" /></description>
//     ///     </item>
//     ///     <item>
//     ///         <description><see cref="RemoveItem" /></description>
//     ///     </item>
//     ///     <item>
//     ///         <description><see cref="MoveItem" /></description>
//     ///     </item>
//     /// </list>
//     /// </remarks>
//     Manage = ManageItem | RemoveItem | MoveItem,

//     /// <summary>
//     /// A simple permission combination allowing writing permissions and reading permissions.
//     /// </summary>
//     /// <remarks>
//     /// <para>Sets these permissions:</para>
//     /// <list type="bullet">
//     ///     <item>
//     ///         <description><see cref="CreateItem" /></description>
//     ///     </item>
//     ///     <item>
//     ///         <description><see cref="GetItem" /></description>
//     ///     </item>
//     ///     <item>
//     ///         <description><see cref="CompleteItem" /></description>
//     ///     </item>
//     /// </list>
//     /// </remarks>
//     Basic = CreateItem | GetItem | CompleteItem
//     #endregion
// }