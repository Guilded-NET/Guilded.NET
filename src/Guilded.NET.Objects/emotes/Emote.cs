using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Guilded.NET.Objects
{
    /// <summary>
    /// Guilded emote object.
    /// </summary>
    public class Emote : ChatEmote
    {
        /// <summary>
        /// Guilded emote object.
        /// </summary>
        public Emote() =>
            DiscordEmojiId = null;
        /// <summary>
        /// Who created the emote.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("createdBy", Required = Required.Always)]
        public GId Author
        {
            get; set;
        }
        /// <summary>
        /// When the emote was created.
        /// </summary>
        /// <value>Date</value>
        [JsonProperty("createdAt", Required = Required.Always)]
        public DateTime CreatedAt
        {
            get; set;
        }
        /// <summary>
        /// ID of the team where emote is in.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty("teamId", Required = Required.Always)]
        public GId TeamId
        {
            get; set;
        }
        /// <summary>
        /// Aliases of emote.
        /// </summary>
        /// <value>List of names</value>
        [JsonProperty("aliases")]
        public IList<string> Aliases
        {
            get; set;
        }
        /// <summary>
        /// Whether or not this emote has been deleted.
        /// </summary>
        /// <value>Deleted</value>
        [JsonProperty("isDeleted")]
        public bool IsDeleted
        {
            get; set;
        }
        /// <summary>
        /// ID of the Discord emoji Guilded emote is synced with.
        /// </summary>
        /// <value></value>
        [JsonProperty("discordEmojiId")]
        public ulong? DiscordEmojiId
        {
            get; set;
        }
    }
}