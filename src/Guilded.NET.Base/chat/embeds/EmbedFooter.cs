using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Embeds
{
    /// <summary>
    /// The footer/bottom area of the embed.
    /// </summary>
    /// <seealso cref="EmbedProvider"/>
    /// <seealso cref="EmbedAuthor"/>
    /// <seealso cref="EmbedField"/>
    /// <seealso cref="EmbedMedia"/>
    public class EmbedFooter : BaseObject
    {
        #region JSON properties
        /// <summary>
        /// The description of the footer.
        /// </summary>
        /// <value>Description</value>
        [JsonProperty(Required = Required.Always)]
        public string Text
        {
            get; set;
        }
        /// <summary>
        /// The URL to footer's icon.
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
        /// Creates a new instance of <see cref="EmbedFooter"/> with text <paramref name="text"/>.
        /// </summary>
        /// <param name="text">The description of the footer</param>
        /// <param name="iconUrl">The URL to footer's icon</param>
        public EmbedFooter(string text, Uri iconUrl = null) =>
            (Text, IconUrl) = (text, iconUrl);
        #endregion
    }
}