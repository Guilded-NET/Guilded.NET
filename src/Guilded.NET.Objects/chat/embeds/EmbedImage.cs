using System;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Chat
{
    /// <summary>
    /// Thumbnail or image of the embed.
    /// </summary>
    public class EmbedImage : BaseObject
    {
        /// <summary>
        /// URL of the image.
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("url")]
        public Uri Url
        {
            get; set;
        }
    }
}