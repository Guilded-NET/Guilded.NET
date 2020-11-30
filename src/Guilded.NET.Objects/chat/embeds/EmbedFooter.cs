using System;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Footer of the embed.
    /// </summary>
    public class EmbedFooter: BaseObject {
        /// <summary>
        /// Footer icon URL.
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("iconUrl", NullValueHandling = NullValueHandling.Ignore)]
        public Uri IconUrl {
            get; set;
        } = null;
        /// <summary>
        /// Description of the embed footer.
        /// </summary>
        /// <value>Description</value>
        [JsonProperty("text", Required = Required.Always)]
        public string Text {
            get; set;
        }
        /// <summary>
        /// Generates embed footer.
        /// </summary>
        /// <param name="text">Text of the footer</param>
        /// <param name="iconUrl">Icon of the footer</param>
        /// <returns>Footer</returns>
        public static EmbedFooter Generate(string text, Uri iconUrl = null) =>
            new EmbedFooter {
                Text = text,
                IconUrl = iconUrl
            };
    }
}