using System;
using System.ComponentModel;

using Newtonsoft.Json;

namespace Guilded.NET.Objects
{
    /// <summary>
    /// Emote in status or in a message.
    /// </summary>
    public class ChatEmote : ClientObject
    {
        /// <summary>
        /// ID of the emote.
        /// </summary>
        /// <value>Emote ID</value>
        [JsonProperty("id", Required = Required.Always)]
        public uint Id
        {
            get; set;
        }
        /// <summary>
        /// URL to emote's PNG file.
        /// </summary>
        /// <value>.PNG URL</value>
        [JsonProperty("png")]
        [DefaultValue(null)]
        public Uri PNGUrl
        {
            get; set;
        }
        /// <summary>
        /// URL to emote's APNG(Animated PNG) file.
        /// </summary>
        /// <value>.APNG URL</value>
        [JsonProperty("apng")]
        [DefaultValue(null)]
        public Uri APNGUrl
        {
            get; set;
        }
        /// <summary>
        /// URL to emote's WebP file.
        /// </summary>
        /// <value>.WEBP URL</value>
        [JsonProperty("webp")]
        [DefaultValue(null)]
        public Uri WebPUrl
        {
            get; set;
        }
        /// <summary>
        /// Name of the emote.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty("name", Required = Required.Always)]
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// Checks if object is equal to this chat emote.
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>Equal</returns>
        public override bool Equals(object obj) =>
            obj is ChatEmote emote && emote.Id == Id;
        /// <summary>
        /// Gets a hashcode of this emote.
        /// </summary>
        /// <returns>Emote</returns>
        public override int GetHashCode() =>
            HashCode.Combine(Id, Name);
    }
}