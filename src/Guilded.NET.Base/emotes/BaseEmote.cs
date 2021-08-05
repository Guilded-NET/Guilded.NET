using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base
{
    /// <summary>
    /// Emote in status or in a message.
    /// </summary>
    public class BaseEmote : ClientObject
    {
        /// <summary>
        /// ID of the emote.
        /// </summary>
        /// <value>Emote ID</value>
        [JsonProperty(Required = Required.Always)]
        public uint Id
        {
            get; set;
        }
        /// <summary>
        /// URL to emote's PNG file.
        /// </summary>
        /// <value>.PNG URL?</value>
        [JsonProperty("png")]
        public Uri Png
        {
            get; set;
        }
        /// <summary>
        /// URL to emote's APNG(Animated PNG) file.
        /// </summary>
        /// <value>.APNG URL?</value>
        [JsonProperty("apng")]
        public Uri Apng
        {
            get; set;
        }
        /// <summary>
        /// URL to emote's WebP file.
        /// </summary>
        /// <value>.WEBP URL?</value>
        [JsonProperty("webp")]
        public Uri Webp
        {
            get; set;
        }
        /// <summary>
        /// Name of the emote.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty(Required = Required.Always)]
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
            obj is BaseEmote emote && emote.Id == Id;
        /// <summary>
        /// Gets a hashcode of this emote.
        /// </summary>
        /// <returns>Emote</returns>
        public override int GetHashCode() =>
            HashCode.Combine(Id, Name);
    }
}