using System;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Content {
    using Chat;
    /// <summary>
    /// A list item in a list channel.
    /// </summary>
    public class ListItem: ChannelContent<Guid> {
        /// <summary>
        /// The content of this item's title.
        /// </summary>
        /// <value>Title</value>
        [JsonProperty("content", Required = Required.Always)]
        public MessageContent Content {
            get; set;
        }
        /// <summary>
        /// Order priority of this list item.
        /// </summary>
        /// <value>Priority</value>
        [JsonProperty("priority", Required = Required.AllowNull)]
        public long? Priority {
            get; set;
        }
        /// <summary>
        /// Who created this list item.
        /// </summary>
        /// <value>Created by</value>
        [JsonProperty("createdBy", Required = Required.Always)]
        public GId CreatedBy {
            get; set;
        }
        /// <summary>
        /// If this list item has a note.
        /// </summary>
        /// <value>Has a note</value>
        [JsonProperty("hasNote", Required = Required.Always)]
        public bool HasNote {
            get; set;
        }
        /// <summary>
        /// Who created this note.
        /// </summary>
        /// <value>Note created by</value>
        [JsonProperty("noteCreatedBy", Required = Required.AllowNull)]
        public GId NoteCreatedBy {
            get; set;
        }
        /// <summary>
        /// If this note was created by a bot, this is bot's id.
        /// </summary>
        /// <value>Bot ID</value>
        [JsonProperty("noteCreatedByBotId", Required = Required.AllowNull)]
        public Guid? NoteCreatedByBot {
            get; set;
        }
        /// <summary>
        /// When item's note was created.
        /// </summary>
        /// <value>Note created at</value>
        [JsonProperty("noteCreatedAt", Required = Required.AllowNull)]
        public DateTime? NoteCreatedAt {
            get; set;
        }
        /// <summary>
        /// Who updated note of this item.
        /// </summary>
        /// <value>Note updated by</value>
        [JsonProperty("noteUpdatedBy", Required = Required.AllowNull)]
        public GId NoteUpdatedBy {
            get; set;
        }
        /// <summary>
        /// When the note was updated.
        /// </summary>
        /// <value>Note updated at</value>
        [JsonProperty("noteUpdatedAt", Required = Required.AllowNull)]
        public DateTime? NodeUpdatedAt {
            get; set;
        }
        /// <summary>
        /// Who updated this item.
        /// </summary>
        /// <value>Updated by</value>
        [JsonProperty("updatedBy", Required = Required.AllowNull)]
        public GId UpdatedBy {
            get; set;
        }
        /// <summary>
        /// When this item got updated.
        /// </summary>
        /// <value>Updated at</value>
        [JsonProperty("updatedAt", Required = Required.AllowNull)]
        public DateTime? UpdatedAt {
            get; set;
        }
        /// <summary>
        /// Who completed this item.
        /// </summary>
        /// <value>Completed by</value>
        [JsonProperty("completedBy", Required = Required.AllowNull)]
        public GId CompletedBy {
            get; set;
        }
        /// <summary>
        /// When this item was completed.
        /// </summary>
        /// <value>Completed at</value>
        [JsonProperty("completedAt", Required = Required.AllowNull)]
        public DateTime? CompletedAt {
            get; set;
        }
        /// <summary>
        /// Who deleted this item.
        /// </summary>
        /// <value>Deleted by</value>
        [JsonProperty("deletedBy", Required = Required.AllowNull)]
        public GId DeletedBy {
            get; set;
        }
        /// <summary>
        /// When this item was deleted.
        /// </summary>
        /// <value>Deleted at</value>
        [JsonProperty("deletedAt", Required = Required.AllowNull)]
        public DateTime? DeletedAt {
            get; set;
        }
        /// <summary>
        /// ID of item's parent.
        /// </summary>
        /// <value>List item ID</value>
        [JsonProperty("parentId", Required = Required.AllowNull)]
        public Guid? ParentId {
            get; set;
        }
        /// <summary>
        /// ID of the bot who created this list item.
        /// </summary>
        /// <value>Bot ID</value>
        [JsonProperty("botId", Required = Required.AllowNull)]
        public Guid? BotId {
            get; set;
        }
    }
}