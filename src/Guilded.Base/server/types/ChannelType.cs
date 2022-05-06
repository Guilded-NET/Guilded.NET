namespace Guilded.Base.Servers;

/// <summary>
/// Represents the type of content that <see cref="ServerChannel">a channel</see> serves.
/// </summary>
/// <seealso cref="ServerChannel" />
/// <seealso cref="Member" />
/// <seealso cref="Webhook" />
public enum ChannelType
{
    /// <summary>
    /// Announcement posts containing news and new information.
    /// </summary>
    Announcements,
    /// <summary>
    /// Chat with messages and threaded messages.
    /// </summary>
    Chat,
    /// <summary>
    /// Events in calendar system.
    /// </summary>
    Calendar,
    /// <summary>
    /// Traditional forum threads with replies in them.
    /// </summary>
    Forums,
    /// <summary>
    /// Posts containing images and videos.
    /// </summary>
    Media,
    /// <summary>
    /// Documents containing any information.
    /// </summary>
    Docs,
    /// <summary>
    /// A normal <see cref="Chat">chat channel</see> with voice chat capibilities.
    /// </summary>
    Voice,
    /// <summary>
    /// A list of completable tasks.
    /// </summary>
    List,
    /// <summary>
    /// People's availability time.
    /// </summary>
    Scheduling,
    /// <summary>
    /// A <see cref="Voice">voice channel</see> without voice rooms and with screensharing and camera capibilities.
    /// </summary>
    Stream
}