// using System;
// using Guilded.Servers;

// namespace Guilded.Permissions;

// /// <summary>
// /// Represents team permissions related to tournaments &amp; scrims.
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
// /// <seealso cref="MediaPermissions" />
// /// <seealso cref="RecruitmentPermissions" />
// /// <seealso cref="SchedulingPermissions" />
// /// <seealso cref="StreamPermissions" />
// /// <seealso cref="VoicePermissions" />
// /// <seealso cref="XpPermissions" />
// [Flags]
// public enum MatchmakingPermissions
// {
//     /// <summary>
//     /// No given permissions.
//     /// </summary>
//     None = 0,

//     /// <summary>
//     /// Allows you to create matchmaking scrims
//     /// </summary>
//     CreateScrim = 1,

//     /// <summary>
//     /// Allows you to use the <see cref="Server">server</see> to create and manage tournaments
//     /// </summary>
//     AttendTournament = 4,

//     /// <summary>
//     /// Allows you to register the <see cref="Server">server</see> for tournaments
//     /// </summary>
//     CreateTournament = 16,

//     #region Properties
//     /// <summary>
//     /// All of the permissions combined.
//     /// </summary>
//     All = CreateScrim | AttendTournament | CreateTournament
//     #endregion
// }