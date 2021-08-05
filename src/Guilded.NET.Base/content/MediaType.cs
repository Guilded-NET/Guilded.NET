using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Guilded.NET.Base.Content
{
    /// <summary>
    /// What kind of media this is.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter), true)]
    public enum MediaType
    {
        /// <summary>
        /// A PNG, APNG, GIF, JPG, JPEG or any other file.
        /// </summary>
        Image,
        /// <summary>
        /// A video file which can be played and be viewed.
        /// </summary>
        Video
    }
}