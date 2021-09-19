using System;

namespace Guilded.NET.Base.Embeds
{
    /// <summary>
    /// The media found in an embed.
    /// </summary>
    /// <remarks>
    /// <para>Represents an image, a thumbnail or a video in an embed.</para>
    /// <para>Provides both a way to set image's URL and its dimensions</para>
    /// </remarks>
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
        /// <remarks>
        /// <para><see cref="Uri"/> that points to image's/video's source location.</para>
        /// <para>This property will be used to fetch the image/video from the given URL.</para>
        /// </remarks>
        /// <value>URL</value>
        public Uri Url
        {
            get; set;
        }
        /// <summary>
        /// The height of the image/video.
        /// </summary>
        /// <value>Size?</value>
        public uint? Height
        {
            get; set;
        }
        /// <summary>
        /// The width of the image/video.
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
        /// Creates <see cref="EmbedMedia"/> if <paramref name="url"/> isn't null.
        /// </summary>
        /// <remarks>
        /// Checks if <paramref name="url"/> is not null and then creates <see cref="EmbedMedia"/> instance. Used for Embed constructor.
        /// </remarks>
        /// <param name="url">The source URL to the image</param>
        /// <returns><see cref="EmbedMedia"/>?</returns>
        internal static EmbedMedia CreateOrNull(Uri url) =>
            url != null ? new EmbedMedia(url) : null;
        #endregion
    }
}