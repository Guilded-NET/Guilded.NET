using System;

using Newtonsoft.Json;

namespace Guilded.Base.Embeds;

/// <summary>
/// Represents the footer area of an <see cref="Embed">embed</see>.
/// </summary>
/// <seealso cref="Embed" />
/// <seealso cref="EmbedAuthor" />
/// <seealso cref="EmbedField" />
/// <seealso cref="EmbedMedia" />
public class EmbedFooter : BaseObject
{
    #region Properties
    /// <summary>
    /// Gets the text contents of the footer.
    /// </summary>
    /// <remarks>
    /// <para>The provided Markdown will be ignored.</para>
    /// </remarks>
    /// <value>String</value>
    public string Text { get; set; }
    /// <summary>
    /// The URL to the footer's icon.
    /// </summary>
    /// <remarks>
    /// <para>Usually displayed before the footer text.</para>
    /// </remarks>
    /// <value>Image URL?</value>
    public Uri? IconUrl { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="EmbedFooter" /> with text <paramref name="text" />.
    /// </summary>
    /// <param name="text">The text contents of the footer</param>
    /// <param name="iconUrl">The URL to footer's icon</param>
    /// <exception cref="ArgumentNullException">When <paramref name="text" /> is <see langword="null" /></exception>
    /// <returns>New <see cref="EmbedFooter" /> instance</returns>
    /// <seealso cref="EmbedFooter" />
    /// <seealso cref="EmbedFooter(string, string)" />
    [JsonConstructor]
    public EmbedFooter(
        [JsonProperty(Required = Required.Always)]
        string text,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Uri? iconUrl = null
    ) =>
        (Text, IconUrl) = (text, iconUrl);
    /// <inheritdoc cref="EmbedFooter(string, Uri?)" />
    /// <exception cref="ArgumentNullException"><paramref name="iconUrl" /> is <see langword="null" />, empty or whitespace</exception>
    /// <exception cref="UriFormatException"><paramref name="iconUrl" /> has bad <see cref="Uri" /> formatting</exception>
    /// <returns>New <see cref="EmbedFooter" /> instance</returns>
    /// <seealso cref="EmbedFooter" />
    /// <seealso cref="EmbedFooter(string, Uri)" />
    public EmbedFooter(string text, string iconUrl) : this(text, new Uri(iconUrl)) { }
    #endregion
}