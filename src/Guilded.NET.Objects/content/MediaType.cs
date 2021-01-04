namespace Guilded.NET.Objects.Content {
    /// <summary>
    /// What kind of media this is.
    /// </summary>
    public enum MediaType {
        /// <summary>
        /// A PNG, APNG, GIF, JPG, JPEG or any other file.
        /// </summary>
        Image,
        /// <summary>
        /// A video file which can be played and be viewed.
        /// </summary>
        Video,
        /// <summary>
        /// A file which can be played and produces audio.
        /// </summary>
        Audio
    }
}