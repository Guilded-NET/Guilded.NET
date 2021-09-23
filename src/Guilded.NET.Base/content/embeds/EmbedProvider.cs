using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Embeds
{
    /// <summary>
    /// The provider/domain of the content.
    /// </summary>
    /// <remarks>
    /// <para>Provides details about domain/provider the content comes from.</para>
    /// <para>This can't be customized. It can only be found while fetching link metadata.</para>
    /// </remarks>
    /// <seealso cref="EmbedFooter"/>
    /// <seealso cref="EmbedAuthor"/>
    /// <seealso cref="EmbedField"/>
    /// <seealso cref="EmbedMedia"/>
    public class EmbedProvider : BaseObject
    {
        /// <summary>
        /// The name of the provider.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty(Required = Required.Always, NullValueHandling = NullValueHandling.Ignore)]
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// The URL of the provider
        /// </summary>
        /// <value>URL?</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Uri Url
        {
            get; set;
        }
    }
}