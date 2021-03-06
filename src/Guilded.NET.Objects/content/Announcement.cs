using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Content
{
    using Chat;
    /// <summary>
    /// Announcement posted in the Overview or announcement channel.
    /// </summary>
    public class Announcement : ChannelPost<GId>
    {
        /// <summary>
        /// Title of the announcement.
        /// </summary>
        /// <value>Title</value>
        [JsonProperty("title", Required = Required.Always)]
        public string Title
        {
            get; set;
        }
        /// <summary>
        /// Content inside this announcement.
        /// </summary>
        /// <value>Message content</value>
        [JsonProperty("content", Required = Required.Always)]
        public MessageContent Content
        {
            get; set;
        }
        /// <summary>
        /// All reactions on this announcement.
        /// </summary>
        /// <value>List of reactions</value>
        [JsonProperty("reactions", Required = Required.Always)]
        public IList<Reaction> Reactions
        {
            get; set;
        }
        /// <summary>
        /// When this announcement was edited. Null if it was not edited.
        /// </summary>
        /// <value>Edited at</value>
        [JsonProperty("editedAt", Required = Required.AllowNull)]
        public DateTime? EditedAt
        {
            get; set;
        }
        /// <summary>
        /// When this announcement was deleted. Null if it was not deleted.
        /// </summary>
        /// <value>Deleted at</value>
        [JsonProperty("deletedAt", Required = Required.AllowNull)]
        public DateTime? DeletedAt
        {
            get; set;
        }
        /// <summary>
        /// If this announcement is pinned.
        /// </summary>
        /// <value>Pinned</value>
        [JsonProperty("isPinned", Required = Required.Always)]
        public bool IsPinned
        {
            get; set;
        }
        /// <summary>
        /// If this announcement is public.
        /// </summary>
        /// <value>Public</value>
        [JsonProperty("isPublic", Required = Required.Always)]
        public bool IsPublic
        {
            get; set;
        }
        /// <summary>
        /// In which group the announcement was posted in.
        /// </summary>
        /// <value>Group ID</value>
        [JsonProperty("groupId", Required = Required.Always)]
        public GId GroupId
        {
            get; set;
        }
    }
}