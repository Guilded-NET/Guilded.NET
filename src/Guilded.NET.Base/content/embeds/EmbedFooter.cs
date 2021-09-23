using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Embeds
{
    /// <summary>
    /// The footer of an embed.
    /// </summary>
    /// <remarks>
    /// <para>The bottom area of an embed that provides further information about anything.</para>
    /// <para>Footers can also have timestamps, but that can be used by setting <see cref="Embed.Timestamp"/> property. Timestamps are
    /// not officially part of footers, but that's the most common way they are displayed by the clients and official Guilded app.</para>
    /// </remarks>
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
        /// <remarks>
        /// <para>The piece of text that will be displayed in the footer.</para>
        /// <para>The footer text usually ignores any provided Markdown by clients like
        /// Guilded official app, but may be displayed by other clients.</para>
        /// </remarks>
        /// <value>Description</value>
        [JsonProperty(Required = Required.Always)]
        public string Text
        {
            get; set;
        }
        /// <summary>
        /// The source URL of footer's icon.
        /// </summary>
        /// <remarks>
        /// <para>The icon of the footer.</para>
        /// <para>Displayed to the left side of the text in Guilded official app.</para>
        /// </remarks>
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