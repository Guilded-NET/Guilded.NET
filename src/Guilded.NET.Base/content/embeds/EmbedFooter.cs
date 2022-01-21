using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Embeds
{
    /// <summary>
    /// The footer of an embed.
    /// </summary>
    /// <remarks>
    /// <para>The bottom area of an embed that defines a side information about something, such as post likes. Footers can also have timestamps, but that can be used by setting <see cref="Embed.Timestamp"/> property. Timestamps are not officially part of footers, but that's the way it is displayed.</para>
    /// </remarks>
    /// <seealso cref="EmbedAuthor"/>
    /// <seealso cref="EmbedField"/>
    /// <seealso cref="EmbedMedia"/>
    public class EmbedFooter : BaseObject
    {
        #region JSON properties
        /// <summary>
        /// The description of the footer.
        /// </summary>
        /// <remarks>
        /// <para>The piece of text that will be displayed in the footer.</para>
        /// <para>The provided Markdown will be ignored.</para>
        /// </remarks>
        /// <value>Description</value>
        public string Text { get; set; }
        /// <summary>
        /// The source URL of footer's icon.
        /// </summary>
        /// <remarks>
        /// <para>The icon of the footer.</para>
        /// <para>Usually displayed before the footer text.</para>
        /// </remarks>
        /// <value>URL?</value>
        public Uri? IconUrl { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="EmbedFooter"/> with text <paramref name="text"/>.
        /// </summary>
        /// <param name="text">The description of the footer</param>
        /// <param name="iconUrl">The URL to footer's icon</param>
        /// <exception cref="ArgumentNullException">When <paramref name="text"/> is <see langword="null"/></exception>
        [JsonConstructor]
        public EmbedFooter(
            [JsonProperty(Required = Required.Always)]
            string text,

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            Uri? iconUrl = null
        ) =>
            (Text, IconUrl) = (text, iconUrl);
        /// <summary>
        /// Creates a new instance of <see cref="EmbedFooter"/> with text <paramref name="text"/>.
        /// </summary>
        /// <param name="text">The description of the footer</param>
        /// <param name="iconUrl">The URL to footer's icon</param>
        /// <exception cref="ArgumentNullException"><paramref name="iconUrl"/> is <see langword="null"/>, empty or whitespace</exception>
        /// <exception cref="UriFormatException"><paramref name="iconUrl"/> has bad <see cref="Uri"/> formatting</exception>
        public EmbedFooter(string text, string iconUrl) : this(text, new Uri(iconUrl)) { }
        #endregion
    }
}