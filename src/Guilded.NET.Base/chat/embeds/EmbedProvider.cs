using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Embeds
{
    /// <summary>
    /// The provider of the embed.
    /// </summary>
    public class EmbedProvider : BaseObject
    {
        /// <summary>
        /// The name of the provider.
        /// </summary>
        /// <value>Title</value>
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
        #region Constructors
        /// <summary>
        /// The author of the embed.
        /// </summary>
        /// <param name="name">The name of the embed author</param>=
        /// <param name="url">The URL that author links</param>
        public EmbedProvider(string name, Uri url = null) =>
            (Name, Url) = (name, url);
        #endregion
    }
}