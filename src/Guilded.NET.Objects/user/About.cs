using Newtonsoft.Json;

namespace Guilded.NET.Objects.Users
{
    /// <summary>
    /// User's about information.
    /// </summary>
    public class About : BaseObject
    {
        /// <summary>
        /// Bio of the user.
        /// </summary>
        /// <value>Bio</value>
        [JsonProperty("bio")]
        public string Bio
        {
            get; set;
        }
        /// <summary>
        /// User's tagline.
        /// </summary>
        /// <value>Tagline</value>
        [JsonProperty("tagLine")]
        public string TagLine
        {
            get; set;
        }
    }
}