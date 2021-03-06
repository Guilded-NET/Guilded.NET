using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Content
{
    using Chat;
    /// <summary>
    /// An event which is in calendar channel.
    /// </summary>
    public class CalendarEvent : ChannelPost<uint>
    {
        /// <summary>
        /// An event which is in calendar channel.
        /// </summary>
        public CalendarEvent() =>
            AllowedRoleIds = null;
        /// <summary>
        /// Title of the post.
        /// </summary>
        /// <value>Title</value>
        [JsonProperty("name", Required = Required.Always)]
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// If the event repeats through multiple months.
        /// </summary>
        /// <value>Repeats</value>
        [JsonProperty("repeats", Required = Required.Always)]
        public bool Repeats
        {
            get; set;
        }
        /// <summary>
        /// Location where the event will start.
        /// </summary>
        /// <value>Location</value>
        [JsonProperty("location", Required = Required.AllowNull)]
        public string Location
        {
            get; set;
        }
        /// <summary>
        /// The description of this event.
        /// </summary>
        /// <value>Message content</value>
        [JsonProperty("description", Required = Required.AllowNull)]
        public MessageContent Description
        {
            get; set;
        }
        /// <summary>
        /// URL to this event's page.
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("link", Required = Required.AllowNull)]
        public Uri Link
        {
            get; set;
        }
        /// <summary>
        /// When event will happen.
        /// </summary>
        /// <value>Event will happen at</value>
        [JsonProperty("happensAt", Required = Required.Always)]
        public DateTime? HappensAt
        {
            get; set;
        }
        /// <summary>
        /// When event participants were notified.
        /// </summary>
        /// <value>Participants notified at</value>
        [JsonProperty("notifiedAt", Required = Required.AllowNull)]
        public DateTime? NotifiedAt
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [JsonProperty("familyId", Required = Required.Always)]
        public Guid? FamilyId
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [JsonProperty("isSynthetic", Required = Required.Always)]
        public bool IsSynthetic
        {
            get; set;
        }
        /// <summary>
        /// Duration of how many minutes this event is going to go live.
        /// </summary>
        /// <value>Minutes</value>
        [JsonProperty("durationInMinutes", Required = Required.Always)]
        public double Duration
        {
            get; set;
        }
        /// <summary>
        /// If this event is publicly visible.
        /// </summary>
        /// <value>Public</value>
        [JsonProperty("isPublic", Required = Required.Always)]
        public bool IsPublic
        {
            get; set;
        }
        /// <summary>
        /// Colour of this event.
        /// </summary>
        /// <value>Colour label</value>
        [JsonProperty("colorLabel", Required = Required.Always)]
        public string Color
        {
            get; set;
        }
        /// <summary>
        /// If this event is happening all day.
        /// </summary>
        /// <value>All day event</value>
        [JsonProperty("isAllDay", Required = Required.Always)]
        public bool IsAllDay
        {
            get; set;
        }
        /// <summary>
        /// At which timezone this event is happening.
        /// </summary>
        /// <value>Timezone</value>
        [JsonProperty("happensAtClientTimezone", Required = Required.AllowNull)]
        public string HappensAtClientTimezone
        {
            get; set;
        }
        /// <summary>
        /// If this event is open to join.
        /// </summary>
        /// <value>Open</value>
        [JsonProperty("isOpen", Required = Required.Always)]
        public bool IsOpen
        {
            get; set;
        }
        /// <summary>
        /// Limit of how many members can join this event.
        /// </summary>
        /// <value>RSVP Limit</value>
        [JsonProperty("rsvpLimit", Required = Required.AllowNull)]
        public uint? RsvpLimit
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [JsonProperty("autofillWaitlist", Required = Required.Always)]
        public bool AutofillWaitlist
        {
            get; set;
        }
        /// <summary>
        /// Notify event members.
        /// </summary>
        /// <value>Notify members</value>
        [JsonProperty("rsvpNotificationsEnabled", Required = Required.Always)]
        public bool RsvpNotificationsEnabled
        {
            get; set;
        }
        /// <summary>
        /// Whether or not it should show a countdown.
        /// </summary>
        /// <value>Countdown</value>
        [JsonProperty("eventCountdownEnabled", Required = Required.Always)]
        public bool CountdownEnabled
        {
            get; set;
        }
        /// <summary>
        /// If it should remind that event is recurring.
        /// </summary>
        /// <value>Recurring reminder</value>
        [JsonProperty("eventRecurringReminderEnabled", Required = Required.Always)]
        public bool RecurringReminderEnabled
        {
            get; set;
        }
        /// <summary>
        /// If the role should be mentioned when the event starts.
        /// </summary>
        /// <value>Role mention</value>
        [JsonProperty("roleMentioningEnabled", Required = Required.Always)]
        public bool RoleMentioningEnabled
        {
            get; set;
        }
        /// <summary>
        /// If this event should mention everyone when it starts.
        /// </summary>
        /// <value>Mention @everyone</value>
        [JsonProperty("mentionEveryone", Required = Required.Always)]
        public bool MentionEveryone
        {
            get; set;
        }
        /// <summary>
        /// Roles which are allowed to attend this event.
        /// </summary>
        /// <value>Roles allowed</value>
        [JsonProperty("allowedRoleIds")]
        public IList<uint> AllowedRoleIds
        {
            get; set;
        }
    }
}