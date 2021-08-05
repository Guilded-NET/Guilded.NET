using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Embeds
{
    /// <summary>
    /// The author of the embed.
    /// </summary>
    public class EmbedAuthor : BaseObject
    {
        #region JSON properties
        /// <summary>
        /// The name of the embed author.
        /// </summary>
        /// <value>Title?</value>
        [JsonProperty(Required = Required.Always, NullValueHandling = NullValueHandling.Ignore)]
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// The URL that author links.
        /// </summary>
        /// <value>URL?</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Uri Url
        {
            get; set;
        }
        /// <summary>
        /// The URL to author's icon.
        /// </summary>
        /// <value>URL?</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Uri IconUrl
        {
            get; set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// The author of the embed.
        /// </summary>
        /// <param name="name">The name of the embed author</param>
        /// <param name="iconUrl">The URL to author's icon</param>
        /// <param name="url">The URL that author links</param>
        public EmbedAuthor(string name, Uri iconUrl = null, Uri url = null)
        {
            // If you try to set null title
            if(string.IsNullOrWhiteSpace(name)) throw new NullReferenceException($"Argument {nameof(name)} cannot be null, empty or whitespace.");
            (Name, IconUrl, Url) = (name, iconUrl, url);
        }
        #endregion
    }
}