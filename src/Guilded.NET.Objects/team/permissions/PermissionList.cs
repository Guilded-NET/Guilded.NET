using System;
using System.Collections.Concurrent;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Permissions {
    /// <summary>
    /// Represents role permissions.
    /// </summary>
    public class PermissionList: BaseObject {
        /// <summary>
        /// Represents chat/text channel permissions.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty("chat", NullValueHandling = NullValueHandling.Ignore)]
        public ChatPermissions? Chat {
            get; set;
        } = null;
        /// <summary>
        /// Represents document channel permissions.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty("docs", NullValueHandling = NullValueHandling.Ignore)]
        public DocPermissions? Docs {
            get; set;
        } = null;
        /// <summary>
        /// Represents permissions for forms and polls.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty("forms", NullValueHandling = NullValueHandling.Ignore)]
        public FormPermissions? Forms {
            get; set;
        } = null;
        /// <summary>
        /// List channel permissions.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty("lists", NullValueHandling = NullValueHandling.Ignore)]
        public ListPermissions? Lists {
            get; set;
        } = null;
        /// <summary>
        /// Media channel permissions.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty("media", NullValueHandling = NullValueHandling.Ignore)]
        public MediaPermissions? Media {
            get; set;
        } = null;
        /// <summary>
        /// Forum channel permissions.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty("forums", NullValueHandling = NullValueHandling.Ignore)]
        public ForumPermissions? Forums {
            get; set;
        } = null;
        /// <summary>
        /// General permissions for managing the server.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty("general", NullValueHandling = NullValueHandling.Ignore)]
        public GeneralPermissions? General {
            get; set;
        } = null;
        /// <summary>
        /// Permissions related to streaming channel.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty("streams", NullValueHandling = NullValueHandling.Ignore)]
        public StreamPermissions? Streams {
            get; set;
        } = null;
        /// <summary>
        /// Calendar/event channel permissions.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty("calendar", NullValueHandling = NullValueHandling.Ignore)]
        public CalendarPermissions? Calendar {
            get; set;
        } = null;
        /// <summary>
        /// Scheduling channel permissions.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty("scheduling", NullValueHandling = NullValueHandling.Ignore)]
        public SchedulingPermissions? Scheduling {
            get; set;
        } = null;
        /// <summary>
        /// Competitive permissions.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty("matchmaking", NullValueHandling = NullValueHandling.Ignore)]
        public MatchmakingPermissions? Matchmaking {
            get; set;
        } = null;
        /// <summary>
        /// Permissions for recruitment and applications.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty("recruitment", NullValueHandling = NullValueHandling.Ignore)]
        public RecruitmentPermissions? Recruitment {
            get; set;
        } = null;
        /// <summary>
        /// Announcement channel and overview announcement permissions.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty("announcements", NullValueHandling = NullValueHandling.Ignore)]
        public AnnounPermissions? Announcements {
            get; set;
        } = null;
        /// <summary>
        /// I don't even know what this is tbf.
        /// </summary>
        /// <value>Something I don't know</value>
        [JsonProperty("customization", NullValueHandling = NullValueHandling.Ignore)]
        public CustomPermissions? Customization {
            get; set;
        } = null;
        /// <summary>
        /// Adds up 2 permissions together.
        /// </summary>
        /// <param name="first">First permission list</param>
        /// <param name="second">Second permission list</param>
        /// <returns>Added up permission list</returns>
        public static PermissionList operator+(PermissionList first, PermissionList second) =>
            new PermissionList {
                // ORs chat permissions
                Chat = Concat(first.Chat, second.Chat),
                // ORs docs permissions
                Docs = Concat(first.Docs, second.Docs),
                // ORs form permissions
                Forms = Concat(first.Forms, second.Forms),
                // ORs list permissions
                Lists = Concat(first.Lists, second.Lists),
                // ORs media permissions
                Media = Concat(first.Media, second.Media),
                // ORs forum permissions
                Forums = Concat(first.Forums, second.Forums),
                // ORs stream permissions
                Streams = Concat(first.Streams, second.Streams),
                // ORs calendar permissions
                Calendar = Concat(first.Calendar, second.Calendar),
                // ORs scheduling permissions
                Scheduling = Concat(first.Scheduling, second.Scheduling),
                // ORs matchmaking permissions
                Matchmaking = Concat(first.Matchmaking, second.Matchmaking),
                // ORs recruitment permissions
                Recruitment = Concat(first.Recruitment, second.Recruitment),
                // ORs announcement permissions
                Announcements = Concat(first.Announcements, second.Announcements),
                // ORs customization permissions
                Customization = Concat(first.Customization, second.Customization)
            };
        /// <summary>
        /// Removes second PermissionList instance from first.
        /// </summary>
        /// <param name="first">To remove from</param>
        /// <param name="second">To remove with</param>
        /// <returns>New permission list instance</returns>
        public static PermissionList operator-(PermissionList first, PermissionList second) =>
            new PermissionList {
                // Removes second from first chat permission
                Chat = first.Chat & ~second.Chat,
                // Removes second from first docs permission
                Docs = first.Docs & ~second.Docs,
                // Removes second from first form permission
                Forms = first.Forms & ~second.Forms,
                // Removes second from first list permission
                Lists = first.Lists & ~second.Lists,
                // Removes second from first media permission
                Media = first.Media & ~second.Media,
                // Removes second from first forum permission
                Forums = first.Forums & ~second.Forums,
                // Removes second from first stream permission
                Streams = first.Streams & ~second.Streams,
                // Removes second from first calendar permission
                Calendar = first.Calendar & ~second.Calendar,
                // Removes second from first scheduling permission
                Scheduling = first.Scheduling & ~second.Scheduling,
                // Removes second from first matchmaking permission
                Matchmaking = first.Matchmaking & ~second.Matchmaking,
                // Removes second from first recruitment permission
                Recruitment = first.Recruitment & ~second.Recruitment,
                // Removes second from first announcement permission
                Announcements = first.Announcements & ~second.Announcements,
                // Removes second from first customization permission
                Customization = first.Customization & ~second.Customization
            };
        /// <summary>
        /// Used to concat and check 2 enum values.
        /// </summary>
        /// <param name="first">First to concat</param>
        /// <param name="second">Second to concat</param>
        /// <returns>OR-ed enum value</returns>
        static uint? Concat(uint? first, uint? second) {
            // If one of them is null, return the other
            if(first == null || second == null) return first ?? second;
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
        static T? Concat<T>(T? a, T? b) where T: struct, Enum =>
            (T?)Enum.ToObject(typeof(T), Concat(Convert.ToUInt32(b), Convert.ToUInt32(a)));
        #nullable restore
    }
}