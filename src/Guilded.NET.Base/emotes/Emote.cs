using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Guilded.NET.Base
{
    /// <summary>
    /// The information about provided emote.
    /// </summary>
    /// <seealso cref="Chat.ChatEmote"/>
    public class Emote : BaseEmote
    {
        /// <summary>
        /// Who created the emote.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty(Required = Required.Always)]
        public GId CreatedBy
        {
            get; set;
        }
        /// <summary>
        /// When the emote was created.
        /// </summary>
        /// <value>Date</value>
        [JsonProperty(Required = Required.Always)]
        public DateTime CreatedAt
        {
            get; set;
        }
        /// <summary>
        /// ID of the team where emote is in.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty(Required = Required.Always)]
        public GId TeamId
        {
            get; set;
        }
        /// <summary>
        /// Aliases of emote.
        /// </summary>
        /// <value>List of names</value>
        public IList<string> Aliases
        {
            get; set;
        }
        /// <summary>
        /// Whether this emote has been deleted.
        /// </summary>
        /// <value>Deleted</value>
        public bool IsDeleted
        {
            get; set;
        }
        /// <summary>
        /// ID of the Discord emoji Guilded emote is synced with.
        /// </summary>
        /// <value></value>
        public ulong? DiscordEmojiId
        {
            get; set;
        }
    }
}