using Newtonsoft.Json;

namespace Guilded.NET.Base.Users
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
        public string Bio
        {
            get; set;
        }
        /// <summary>
        /// User's tagline.
        /// </summary>
        /// <value>Tagline</value>
        public string TagLine
        {
            get; set;
        }
    }
}