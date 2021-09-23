using Newtonsoft.Json;

namespace Guilded.NET.Base.Users
{
    /// <summary>
    /// A social media link.
    /// </summary>
    /// <remarks>
    /// <para>Defines what platforms this user has linked to, such as <see cref="SocialLinkType.Twitch"/>.</para>
    /// </remarks>
    /// <seealso cref="Teams.Member"/>
    /// <seealso cref="SocialLinkType"/>
    public class SocialLink : BaseObject
    {
        /// <summary>
        /// The type of social link it is.
        /// </summary>
        /// <value>Social link platform</value>
        [JsonProperty(Required = Required.Always)]
        public SocialLinkType Type
        {
            get; set;
        }
        /// <summary>
        /// The identifier of the user in this service.
        /// </summary>
        /// <remarks>
        /// <para>Defines the identifier of this user in the linked service.</para>
        /// </remarks>
        /// <value>Social link ID</value>
        public string ServiceId
        {
            get; set;
        }
    }
}