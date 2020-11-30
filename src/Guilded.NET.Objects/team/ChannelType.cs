namespace Guilded.NET.Objects.Teams {
    /// <summary>
    /// Represents types of Guilded channels.
    /// </summary>
    public enum ChannelType {
        // Chat & voice
        Chat, Voice, Stream,
        // Information
        Document, List, Announcement,
        // Posting content
        Media, Forum,
        // Calendar and time related
        Event, Scheduling
    }
}