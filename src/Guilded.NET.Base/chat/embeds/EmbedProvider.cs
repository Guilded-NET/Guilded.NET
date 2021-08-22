using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Embeds
{
    /// <summary>
    /// The provider/domain of an embed.
    /// </summary>
    /// <remarks>
    /// The domain name and URL in an embed. Currently unavailable to be customized.
    /// </remarks>
    /// <seealso cref="EmbedFooter"/>
    /// <seealso cref="EmbedAuthor"/>
    /// <seealso cref="EmbedField"/>
    /// <seealso cref="EmbedMedia"/>
    public class EmbedProvider : BaseObject
    {
        /// <summary>
        /// The name of a provider.
        /// </summary>
        /// <value>Title</value>
        [JsonProperty(Required = Required.Always, NullValueHandling = NullValueHandling.Ignore)]
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// The URL of a provider
        /// </summary>
        /// <value>URL?</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Uri Url
        {
            get; set;
        }
    }
}