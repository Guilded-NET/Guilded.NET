using System.Runtime.Serialization;

namespace Guilded.NET.Objects.Teams {
    /// <summary>
    /// Represents types of Guilded channels.
    /// </summary>
    public enum ChannelType {
        // Chat & voice
        /// <summary>
        /// A channel where you can send messages and talk.
        /// </summary>
        [EnumMember(Value = "chat")]
        Chat,
        /// <summary>
        /// A channel where you can send messages and talk with your voice.
        /// </summary>
        [EnumMember(Value = "voice")]
        Voice,
        /// <summary>
        /// A channel where you can send messages, talk and stream games, or anything else.
        /// </summary>
        [EnumMember(Value = "stream")]
        Stream,
        // Information
        /// <summary>
        /// A channel where you can create documents(such as rules).
        /// </summary>
        [EnumMember(Value = "doc")]
        Document,
        /// <summary>
        /// A channel where there is list of completable items and acts as a to-do list.
        /// </summary>
        [EnumMember(Value = "list")]
        List,
        /// <summary>
        /// A channel where you can announce any news.
        /// </summary>
        [EnumMember(Value = "announcement")]
        Announcement,
        // Posting content
        /// <summary>
        /// A channel where you can submit images and videos.
        /// </summary>
        [EnumMember(Value = "media")]
        Media,
        /// <summary>
        /// A channel where you can create a new topics and discuss about certain things.
        /// </summary>
        [EnumMember(Value = "forum")]
        Forum,
        // Calendar and time related
        /// <summary>
        /// A channel where you can create events which can be joined by others.
        /// </summary>
        [EnumMember(Value = "event")]
        Event,
        /// <summary>
        /// A channel where you can set up your schedule when you are available.
        /// </summary>
        [EnumMember(Value = "scheduling")]
        Scheduling
    }
}