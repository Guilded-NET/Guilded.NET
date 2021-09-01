using System;

namespace Guilded.NET.Base.Embeds
{
    /// <summary>
    /// The image or the thumbnail image in an embed.
    /// </summary>
    /// <seealso cref="EmbedFooter"/>
    /// <seealso cref="EmbedProvider"/>
    /// <seealso cref="EmbedAuthor"/>
    /// <seealso cref="EmbedField"/>
    public class EmbedMedia : BaseObject
    {
        #region JSON properties
        /// <summary>
        /// The source URL to the image.
        /// </summary>
        /// <value>URL</value>
        public Uri Url
        {
            get; set;
        }
        /// <summary>
        /// The height of the image.
        /// </summary>
        /// <value>Size?</value>
        public uint? Height
        {
            get; set;
        }
        /// <summary>
        /// The width of the image.
        /// </summary>
        /// <value>Size?</value>
        public uint? Width
        {
            get; set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="EmbedMedia"/> with optional size parameters.
        /// </summary>
        /// <param name="url">The source URL to the image</param>
        /// <param name="width">The width of the image</param>
        /// <param name="height">The height of the image</param>
        public EmbedMedia(Uri url, uint? width = null, uint? height = null) =>
            (Url, Width, Height) = (url, width, height);
        #endregion

        #region Internal
        /// <summary>
        /// Creates <see cref="EmbedMedia"/> if URL isn't null.
        /// </summary>
        /// <remarks>
        /// Checks if URL is not null and then creates <see cref="EmbedMedia"/> instance. Used for Embed constructor.
        /// </remarks>
        /// <param name="url">The source URL to the image</param>
        /// <returns><see cref="EmbedMedia"/>?</returns>
        internal static EmbedMedia CreateOrNull(Uri url) =>
            url != null ? new EmbedMedia(url) : null;
        #endregion
    }
}