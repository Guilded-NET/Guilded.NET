using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base
{
    /// <summary>
    /// An emote in status or in a message.
    /// </summary>
    /// <seealso cref="Emote"/>
    /// <seealso cref="Chat.ChatEmote"/>
    public class BaseEmote : ClientObject
    {
        /// <summary>
        /// The identifier of the emote.
        /// </summary>
        /// <value>Emote ID</value>
        [JsonProperty(Required = Required.Always)]
        public uint Id
        {
            get; set;
        }
        /// <summary>
        /// The source URL to emote's PNG file.
        /// </summary>
        /// <value>.PNG URL?</value>
        [JsonProperty("png")]
        public Uri Png
        {
            get; set;
        }
        /// <summary>
        /// The source URL to emote's APNG(Animated PNG) file.
        /// </summary>
        /// <value>.APNG URL?</value>
        [JsonProperty("apng")]
        public Uri Apng
        {
            get; set;
        }
        /// <summary>
        /// The source URL to emote's WebP file.
        /// </summary>
        /// <value>.WEBP URL?</value>
        [JsonProperty("webp")]
        public Uri Webp
        {
            get; set;
        }
        /// <summary>
        /// The name of the emote.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty(Required = Required.Always)]
        public string Name
        {
            get; set;
        }

        #region Overrides
        /// <summary>
        /// Returns whether this and <paramref name="obj"/> are equal to each other.
        /// </summary>
        /// <param name="obj">Another object to compare</param>
        /// <returns>Are equal</returns>
        public override bool Equals(object obj) =>
            obj is BaseEmote emote && emote.Id == Id;
        /// <summary>
        /// Gets a hashcode of this object.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() =>
            HashCode.Combine(Id, Name);
        #endregion
    }
}