namespace Guilded.NET.Objects.Teams {
    /// <summary>
    /// Represents types of Guilded channels.
    /// </summary>
    public enum ChannelType {
        // Chat & voice
        /// <summary>
        /// A channel where you can send messages and talk.
        /// </summary>
        Chat,
        /// <summary>
        /// A channel where you can send messages and talk with your voice.
        /// </summary>
        Voice,
        /// <summary>
        /// A channel where you can send messages, talk and stream games, or anything else.
        /// </summary>
        Stream,
        // Information
        /// <summary>
        /// A channel where you can create documents(such as rules).
        /// </summary>
        Document,
        /// <summary>
        /// A channel where there is list of completable items and acts as a to-do list.
        /// </summary>
        List,
        /// <summary>
        /// A channel where you can announce any news.
        /// </summary>
        Announcement,
        // Posting content
        /// <summary>
        /// A channel where you can submit images and videos.
        /// </summary>
        Media,
        /// <summary>
        /// A channel where you can create a new topics and discuss about certain things.
        /// </summary>
        Forum,
        // Calendar and time related
        /// <summary>
        /// A channel where you can create events which can be joined by others.
        /// </summary>
        Event,
        /// <summary>
        /// A channel where you can set up your schedule when you are available.
        /// </summary>
        Scheduling
    }
}