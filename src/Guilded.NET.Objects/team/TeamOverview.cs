using System.Collections.Generic;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Teams {
    using Content;
    /// <summary>
    /// An overview page inside of a team.
    /// </summary>
    public class TeamOverview: ClientObject {
        /// <summary>
        /// All global announcements in this team.
        /// </summary>
        /// <value>List of announcements</value>
        [JsonProperty("announcements", Required = Required.Always)]
        public IList<Announcement> Announcements {
            get; set;
        }
        /// <summary>
        /// All of the upcoming events in this team.
        /// </summary>
        /// <value>List of calendar events</value>
        [JsonProperty("events", Required = Required.Always)]
        public IList<CalendarEvent> Events {
            get; set;
        }
        /// <summary>
        /// All of the forum channel posts in this team.
        /// </summary>
        /// <value>List of forum posts</value>
        [JsonProperty("threads", Required = Required.Always)]
        public IList<ForumPost> ForumThreads {
            get; set;
        }
    }
}