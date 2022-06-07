using System;
using Newtonsoft.Json;

namespace Guilded.Base.Embeds;

/// <summary>
/// Represents an image, a thumbnail or a video in an <see cref="Embed">embed</see>.
/// </summary>
/// <seealso cref="Embed" />
/// <seealso cref="EmbedFooter" />
/// <seealso cref="EmbedAuthor" />
/// <seealso cref="EmbedField" />
public class EmbedMedia : BaseModel
{
    #region Properties
    /// <summary>
    /// The source URL to the image.
    /// </summary>
    /// <remarks>
    /// <para><see cref="Uri" /> that points to image's/video's source location. This property will be used to fetch the image/video from the given URL.</para>
    /// </remarks>
    /// <value>URL</value>
    public Uri Url { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="EmbedMedia" /> with optional size parameters.
    /// </summary>
    /// <param name="url">The source URL to the image</param>
    /// <returns>New <see cref="EmbedMedia" /> instance</returns>
    /// <seealso cref="EmbedMedia" />
    /// <seealso cref="EmbedMedia(string)" />
    [JsonConstructor]
    public EmbedMedia(
        [JsonProperty(Required = Required.Always)]
        Uri url
    ) =>
        Url = url;

    /// <summary>
    /// Initializes a new instance of <see cref="EmbedMedia" /> with optional size parameters.
    /// </summary>
    /// <param name="url">The source URL to the image</param>
    /// <exception cref="ArgumentNullException"><paramref name="url" /> is <see langword="null" />, empty or whitespace</exception>
    /// <exception cref="UriFormatException"><paramref name="url" /> has bad <see cref="Uri" /> formatting</exception>
    /// <returns>New <see cref="EmbedMedia" /> instance</returns>
    /// <seealso cref="EmbedMedia" />
    /// <seealso cref="EmbedMedia(Uri)" />
    public EmbedMedia(string url) : this(new Uri(url)) { }
    #endregion

    #region Internal
    /// <summary>
    /// Creates <see cref="EmbedMedia" /> if <paramref name="url" /> isn't <see langword="null" />.
    /// </summary>
    /// <remarks>
    /// <para>Checks if <paramref name="url" /> is not <see langword="null" /> and then creates <see cref="EmbedMedia" /> instance.</para>
    /// <para>Only used in <see cref="Embed(Uri, Uri)" /></para>
    /// </remarks>
    /// <param name="url">The source URL to the image</param>
    /// <returns><see cref="EmbedMedia" />?</returns>
    internal static EmbedMedia? CreateOrNull(Uri? url) =>
        url is not null ? new EmbedMedia(url) : null;
    #endregion
}