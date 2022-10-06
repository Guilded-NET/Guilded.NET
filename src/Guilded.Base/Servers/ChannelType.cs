using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Guilded.Base.Servers;

/// <summary>
/// Represents the type of content that <see cref="ServerChannel">a channel</see> serves.
/// </summary>
/// <seealso cref="ServerChannel" />
/// <seealso cref="Member" />
/// <seealso cref="Webhook" />
[JsonConverter(typeof(StringEnumConverter))]
public enum ChannelType
{
    /// <summary>
    /// Announcement posts containing news and new information.
    /// </summary>
    /// <seealso cref="ChannelType" />
    /// <seealso cref="AnnouncementChannel" />
    /// <seealso cref="Chat" />
    /// <seealso cref="Calendar" />
    /// <seealso cref="Docs" />
    /// <seealso cref="Forums" />
    /// <seealso cref="List" />
    /// <seealso cref="Media" />
    /// <seealso cref="Scheduling" />
    /// <seealso cref="Stream" />
    /// <seealso cref="Voice" />
    Announcements,

    /// <summary>
    /// Chat with messages and threaded messages.
    /// </summary>
    /// <seealso cref="ChannelType" />
    /// <seealso cref="ChatChannel" />
    /// <seealso cref="Announcements" />
    /// <seealso cref="Calendar" />
    /// <seealso cref="Docs" />
    /// <seealso cref="Forums" />
    /// <seealso cref="List" />
    /// <seealso cref="Media" />
    /// <seealso cref="Scheduling" />
    /// <seealso cref="Stream" />
    /// <seealso cref="Voice" />
    Chat,

    /// <summary>
    /// Events in calendar system.
    /// </summary>
    /// <seealso cref="ChannelType" />
    /// <seealso cref="CalendarChannel" />
    /// <seealso cref="Announcements" />
    /// <seealso cref="Chat" />
    /// <seealso cref="Docs" />
    /// <seealso cref="Forums" />
    /// <seealso cref="List" />
    /// <seealso cref="Media" />
    /// <seealso cref="Scheduling" />
    /// <seealso cref="Stream" />
    /// <seealso cref="Voice" />
    Calendar,

    /// <summary>
    /// Documents containing any information.
    /// </summary>
    /// <seealso cref="ChannelType" />
    /// <seealso cref="DocChannel" />
    /// <seealso cref="Announcements" />
    /// <seealso cref="Chat" />
    /// <seealso cref="Calendar" />
    /// <seealso cref="Forums" />
    /// <seealso cref="List" />
    /// <seealso cref="Media" />
    /// <seealso cref="Scheduling" />
    /// <seealso cref="Stream" />
    /// <seealso cref="Voice" />
    Docs,

    /// <summary>
    /// Traditional forum threads with replies in them.
    /// </summary>
    /// <seealso cref="ChannelType" />
    /// <seealso cref="ForumChannel" />
    /// <seealso cref="Announcements" />
    /// <seealso cref="Chat" />
    /// <seealso cref="Calendar" />
    /// <seealso cref="Docs" />
    /// <seealso cref="List" />
    /// <seealso cref="Media" />
    /// <seealso cref="Scheduling" />
    /// <seealso cref="Stream" />
    /// <seealso cref="Voice" />
    Forums,

    /// <summary>
    /// A list of completable tasks.
    /// </summary>
    /// <seealso cref="ChannelType" />
    /// <seealso cref="ListChannel" />
    /// <seealso cref="Announcements" />
    /// <seealso cref="Chat" />
    /// <seealso cref="Calendar" />
    /// <seealso cref="Docs" />
    /// <seealso cref="Forums" />
    /// <seealso cref="Media" />
    /// <seealso cref="Scheduling" />
    /// <seealso cref="Stream" />
    /// <seealso cref="Voice" />
    List,

    /// <summary>
    /// Posts containing images and videos.
    /// </summary>
    /// <seealso cref="ChannelType" />
    /// <seealso cref="MediaChannel" />
    /// <seealso cref="Announcements" />
    /// <seealso cref="Chat" />
    /// <seealso cref="Calendar" />
    /// <seealso cref="Docs" />
    /// <seealso cref="Forums" />
    /// <seealso cref="List" />
    /// <seealso cref="Scheduling" />
    /// <seealso cref="Stream" />
    /// <seealso cref="Voice" />
    Media,

    /// <summary>
    /// People's availability time.
    /// </summary>
    /// <seealso cref="ChannelType" />
    /// <seealso cref="SchedulingChannel" />
    /// <seealso cref="Announcements" />
    /// <seealso cref="Chat" />
    /// <seealso cref="Calendar" />
    /// <seealso cref="Docs" />
    /// <seealso cref="Forums" />
    /// <seealso cref="List" />
    /// <seealso cref="Media" />
    /// <seealso cref="Stream" />
    /// <seealso cref="Voice" />
    Scheduling,

    /// <summary>
    /// A <see cref="Voice">voice channel</see> without voice rooms and with screensharing and camera capibilities.
    /// </summary>
    /// <seealso cref="ChannelType" />
    /// <seealso cref="StreamChannel" />
    /// <seealso cref="Announcements" />
    /// <seealso cref="Chat" />
    /// <seealso cref="Calendar" />
    /// <seealso cref="Docs" />
    /// <seealso cref="Forums" />
    /// <seealso cref="List" />
    /// <seealso cref="Media" />
    /// <seealso cref="Scheduling" />
    /// <seealso cref="Voice" />
    Stream,

    /// <summary>
    /// A normal <see cref="Chat">chat channel</see> with voice chat capibilities.
    /// </summary>
    /// <seealso cref="ChannelType" />
    /// <seealso cref="VoiceChannel" />
    /// <seealso cref="Announcements" />
    /// <seealso cref="Chat" />
    /// <seealso cref="Calendar" />
    /// <seealso cref="Docs" />
    /// <seealso cref="Forums" />
    /// <seealso cref="List" />
    /// <seealso cref="Media" />
    /// <seealso cref="Scheduling" />
    /// <seealso cref="Stream" />
    Voice
}