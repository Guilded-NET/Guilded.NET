using System;

using Guilded.NET.Objects.Content;
using Guilded.NET.Objects.Teams;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Events {
    /// <summary>
    /// When a forum post, media, document, schedule, event, etc. gets updated.
    /// </summary>
    public class ContentUpdatedEvent: TeamEvent {
        /// <summary>
        /// Type of the channel.
        /// </summary>
        /// <value>Team</value>
        [JsonProperty("contentType", Required = Required.Always)]
        public ChannelType ContentType {
            get; set;
        }
        /// <summary>
        /// When the post was created.
        /// </summary>
        /// <value>When the post was created</value>
        [JsonProperty("createdAt", Required = Required.Always)]
        public DateTime CreatedAt {
            get; set;
        }
        /// <summary>
        /// Who updated the content.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("updatedBy", Required = Required.Always)]
        public GId UpdatedBy {
            get; set;
        }
        /// <summary>
        /// A forum post posted in the channel.
        /// </summary>
        /// <value>Forum post</value>
        [JsonProperty("thread")]
        public ForumPost Thread {
            get; set;
        }
        /// <summary>
        /// A media which was posted in the channel.
        /// </summary>
        /// <value>Media</value>
        [JsonProperty("media")]
        public GuildedMedia Media {
            get; set;
        }
        /// <summary>
        /// A document which was posted in the channel.
        /// </summary>
        /// <value>Document</value>
        [JsonProperty("document")]
        public GuildedDocument Document {
            get; set;
        }
        /// <summary>
        /// Event which will occur on set date and will last for specific period of time.
        /// </summary>
        /// <value>Calendar event</value>
        [JsonProperty("event")]
        public CalendarEvent Event {
            get; set;
        }
        /// <summary>
        /// A schedule when an author is available.
        /// </summary>
        /// <value>Schedule</value>
        [JsonProperty("availability")]
        public Availability Availability {
            get; set;
        }
        /// <summary>
        /// An announcement post.
        /// </summary>
        /// <value>Announcement</value>
        [JsonProperty("announcement")]
        public Announcement Announcement {
            get; set;
        }
        /// <summary>
        /// A completable list item in a list channel.
        /// </summary>
        /// <value>List item</value>
        [JsonProperty("listItem")]
        public ListItem List {
            get; set;
        }
    }
}