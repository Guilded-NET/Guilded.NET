using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Permissions
{
    /// <summary>
    /// Represents role permissions.
    /// </summary>
    public class PermissionList : BaseObject
    {
        /// <summary>
        /// Represents chat/text channel permissions.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ChatPermissions? Chat
        {
            get; set;
        }
        /// <summary>
        /// Represents voice channel permissions.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public VoicePermissions? Voice
        {
            get; set;
        }
        /// <summary>
        /// Represents document channel permissions.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DocPermissions? Docs
        {
            get; set;
        }
        /// <summary>
        /// Represents permissions for forms and polls.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public FormPermissions? Forms
        {
            get; set;
        }
        /// <summary>
        /// List channel permissions.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ListPermissions? Lists
        {
            get; set;
        }
        /// <summary>
        /// Media channel permissions.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MediaPermissions? Media
        {
            get; set;
        }
        /// <summary>
        /// Forum channel permissions.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ForumPermissions? Forums
        {
            get; set;
        }
        /// <summary>
        /// General permissions for managing the server.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public GeneralPermissions? General
        {
            get; set;
        }
        /// <summary>
        /// Permissions related to streaming channel.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public StreamPermissions? Streams
        {
            get; set;
        }
        /// <summary>
        /// Calendar/event channel permissions.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public CalendarPermissions? Calendar
        {
            get; set;
        }
        /// <summary>
        /// Scheduling channel permissions.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public SchedulingPermissions? Scheduling
        {
            get; set;
        }
        /// <summary>
        /// Competitive permissions.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MatchmakingPermissions? Matchmaking
        {
            get; set;
        }
        /// <summary>
        /// Permissions for recruitment and applications.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public RecruitmentPermissions? Recruitment
        {
            get; set;
        }
        /// <summary>
        /// Announcement channel and overview announcement permissions.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AnnounPermissions? Announcements
        {
            get; set;
        }
        /// <summary>
        /// Permissions related to name and emote customization.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public CustomPermissions? Customization
        {
            get; set;
        }
        /// <summary>
        /// Permissions related to member XP and levels.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public XPPermissions? Xp
        {
            get; set;
        }
        /// <summary>
        /// Adds up 2 permissions together.
        /// </summary>
        /// <param name="first">First permission list</param>
        /// <param name="second">Second permission list</param>
        /// <returns>Added up permission list</returns>
        public static PermissionList operator +(PermissionList first, PermissionList second) =>
            new PermissionList
            {
                Chat = Concat(first?.Chat, second?.Chat),
                General = Concat(first?.General, second?.General),
                Voice = Concat(first?.Voice, second?.Voice),
                Docs = Concat(first?.Docs, second?.Docs),
                Forms = Concat(first?.Forms, second?.Forms),
                Lists = Concat(first?.Lists, second?.Lists),
                Media = Concat(first?.Media, second?.Media),
                Forums = Concat(first?.Forums, second?.Forums),
                Streams = Concat(first?.Streams, second?.Streams),
                Calendar = Concat(first?.Calendar, second?.Calendar),
                Scheduling = Concat(first?.Scheduling, second?.Scheduling),
                Matchmaking = Concat(first?.Matchmaking, second?.Matchmaking),
                Recruitment = Concat(first?.Recruitment, second?.Recruitment),
                Announcements = Concat(first?.Announcements, second?.Announcements),
                Customization = Concat(first?.Customization, second?.Customization),
                Xp = Concat(first?.Xp, second?.Xp)
            };
        /// <summary>
        /// Removes second PermissionList instance from first.
        /// </summary>
        /// <param name="first">To remove from</param>
        /// <param name="second">To remove with</param>
        /// <returns>New permission list instance</returns>
        public static PermissionList operator -(PermissionList first, PermissionList second) =>
            new PermissionList
            {
                Chat = Substract(first?.Chat, second?.Chat),
                General = Substract(first?.General, second?.General),
                Voice = Substract(first?.Voice, second?.Voice),
                Docs = Substract(first?.Docs, second?.Docs),
                Forms = Substract(first?.Forms, second?.Forms),
                Lists = Substract(first?.Lists, second?.Lists),
                Media = Substract(first?.Media, second?.Media),
                Forums = Substract(first?.Forums, second?.Forums),
                Streams = Substract(first?.Streams, second?.Streams),
                Calendar = Substract(first?.Calendar, second?.Calendar),
                Scheduling = Substract(first?.Scheduling, second?.Scheduling),
                Matchmaking = Substract(first?.Matchmaking, second?.Matchmaking),
                Recruitment = Substract(first?.Recruitment, second?.Recruitment),
                Announcements = Substract(first?.Announcements, second?.Announcements),
                Customization = Substract(first?.Customization, second?.Customization),
                Xp = Substract(first?.Xp, second?.Xp)
            };
        /// <summary>
        /// Used to concat and check 2 enum values.
        /// </summary>
        /// <param name="first">First to concat</param>
        /// <param name="second">Second to concat</param>
        /// <returns>OR-ed enum value</returns>
        private static uint? Concat(uint? first, uint? second)
        {
            // If one of them is null, return the other
            if (first is null || second is null) return first ?? second;
            // Else, return both OR-ed
            else return first | second;
        }
        /// <summary>
        /// Used to concat and check 2 enum values.
        /// </summary>
        /// <param name="first">First to concat</param>
        /// <param name="second">Second to concat</param>
        /// <returns>OR-ed enum value</returns>
#nullable enable
        private static T? Concat<T>(T? first, T? second) where T : struct, Enum =>
            (T?)Enum.ToObject(typeof(T), Concat(Convert.ToUInt32(first), Convert.ToUInt32(second)));
#nullable restore
        /// <summary>
        /// Substracts second enum value from first.
        /// </summary>
        /// <param name="first">To substract from</param>
        /// <param name="second">To substract with</param>
        /// <returns>New enum value</returns>
        private static uint? Substract(uint? first, uint? second)
        {
            // If first one is null, then we can't substract
            if (first is null) return null;
            // Substract second from first. If second is null, then we will substract nothing
            else return first & (~second ?? 0);
        }
        /// <summary>
        /// Substracts second enum value from first.
        /// </summary>
        /// <param name="first">To substract from</param>
        /// <param name="second">To substract with</param>
        /// <returns>New enum value</returns>
#nullable enable
        private static T? Substract<T>(T? first, T? second) where T : struct, Enum =>
            (T?)Enum.ToObject(typeof(T), Substract(Convert.ToUInt32(first), Convert.ToUInt32(second)));
#nullable restore
    }
}