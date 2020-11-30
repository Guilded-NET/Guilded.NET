using Newtonsoft.Json;

namespace Guilded.NET.Objects {
    /// <summary>
    /// Emote in status or in a message.
    /// </summary>
    public class ChatEmote {
        /// <summary>
        /// ID of the emote.
        /// </summary>
        /// <value>Emote ID</value>
        [JsonProperty("id", Required = Required.Always)]
        public uint Id {
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
        /// Converts emote to chat emote.
        /// </summary>
        /// <param name="emote">Emote to convert</param>
        /// <returns>Chat emote</returns>
        public static ChatEmote From(Emote emote) =>
            new ChatEmote {
                Id = emote.Id,
                APNGUrl = emote.APNGUrl,
                PNGUrl = emote.PNGUrl,
                WebPUrl = emote.WebPUrl
            };
    }
}