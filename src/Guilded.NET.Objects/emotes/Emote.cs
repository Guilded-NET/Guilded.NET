using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;

namespace Guilded.NET.Objects {
    /// <summary>
    /// Guilded emote object.
    /// </summary>
    public class Emote: BaseObject {
        /// <summary>
        /// Who created the emote.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("createdBy")]
        public GId Author {
            get; set;
        }
        /// <summary>
        /// When the emote was created.
        /// </summary>
        /// <value>Date</value>
        [JsonProperty("createdAt")]
        public DateTime CreatedAt {
            get; set;
        }
        /// <summary>
        /// ID of the team where emote is in.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty("teamId")]
        public GId TeamId {
            get; set;
        } = null;
        /// <summary>
        /// ID of the emote.
        /// </summary>
        /// <value>Emote ID</value>
        [JsonProperty("id", Required = Required.Always)]
        public uint Id {
            get; set;
        }
        /// <summary>
        /// Aliases of emote.
        /// </summary>
        /// <value>List of names</value>
        [JsonProperty("aliases")]
        public IList<string> Aliases {
            get; set;
        }
        /// <summary>
        /// URL to emote's PNG file.
        /// </summary>
        /// <value>.PNG URL</value>
        [JsonProperty("png")]
        public string PNGUrl {
            get; set;
        }
        /// <summary>
        /// URL to emote's APNG(Animated PNG) file.
        /// </summary>
        /// <value>.APNG URL</value>
        [JsonProperty("apng")]
        public string APNGUrl {
            get; set;
        }
        /// <summary>
        /// URL to emote's WebP file.
        /// </summary>
        /// <value>.WEBP URL</value>
        [JsonProperty("webp")]
        public string WebPUrl {
            get; set;
        }
        /// <summary>
        /// Name of the emote.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty("name")]
        public string Name {
            get; set;
        }
        /// <summary>
        /// Whether or not this emote has been deleted.
        /// </summary>
        /// <value>Deleted</value>
        [JsonProperty("isDeleted")]
        public bool IsDeleted {
            get; set;
        } = false;
        /// <summary>
        /// ID of the Discord emoji Guilded emote is synced with.
        /// </summary>
        /// <value></value>
        [JsonProperty("discordEmojiId")]
        public ulong? DiscordEmojiId {
            get; set;
        } = null;
    }
}