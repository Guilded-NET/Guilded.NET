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
public class EmbedFooter : BaseModel
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
    [JsonProperty("icon_url", NullValueHandling = NullValueHandling.Ignore)]
    public Uri? IconUrl { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="EmbedFooter" /> with text <paramref name="text" />.
    /// </summary>
    /// <param name="text">The text contents of the footer</param>
    /// <param name="icon">The URL to footer's icon</param>
    /// <exception cref="ArgumentNullException">When <paramref name="text" /> is <see langword="null" /></exception>
    /// <returns>New <see cref="EmbedFooter" /> instance</returns>
    /// <seealso cref="EmbedFooter" />
    /// <seealso cref="EmbedFooter(string, string)" />
    /// <seealso cref="EmbedFooter(object, Uri)" />
    /// <seealso cref="EmbedFooter(object, string)" />
    [JsonConstructor]
    public EmbedFooter(
        [JsonProperty(Required = Required.Always)]
        string text,

        [JsonProperty("icon_url", NullValueHandling = NullValueHandling.Ignore)]
        Uri? icon = null
    ) =>
        (Text, IconUrl) = (text, icon);

    /// <inheritdoc cref="EmbedFooter(string, Uri?)" />
    /// <exception cref="ArgumentNullException"><paramref name="icon" /> is <see langword="null" />, empty or whitespace</exception>
    /// <exception cref="UriFormatException"><paramref name="icon" /> has bad <see cref="Uri" /> formatting</exception>
    /// <returns>New <see cref="EmbedFooter" /> instance</returns>
    /// <seealso cref="EmbedFooter" />
    /// <seealso cref="EmbedFooter(string, Uri)" />
    /// <seealso cref="EmbedFooter(object, Uri)" />
    /// <seealso cref="EmbedFooter(object, string)" />
    public EmbedFooter(string text, string icon) : this(text, new Uri(icon)) { }

    /// <inheritdoc cref="EmbedFooter(string, Uri?)" />
    /// <returns>New <see cref="EmbedFooter" /> instance</returns>
    /// <seealso cref="EmbedFooter" />
    /// <seealso cref="EmbedFooter(string, Uri)" />
    /// <seealso cref="EmbedFooter(string, string)" />
    /// <seealso cref="EmbedFooter(object, string)" />
    public EmbedFooter(object? value, Uri? icon = null) : this(value?.ToString() ?? string.Empty, icon) { }

    /// <inheritdoc cref="EmbedFooter(string, Uri?)" />
    /// <returns>New <see cref="EmbedFooter" /> instance</returns>
    /// <seealso cref="EmbedFooter" />
    /// <seealso cref="EmbedFooter(string, Uri)" />
    /// <seealso cref="EmbedFooter(string, string)" />
    /// <seealso cref="EmbedFooter(object, Uri)" />
    public EmbedFooter(object? value, string icon) : this(value?.ToString() ?? string.Empty, icon) { }
    #endregion
}