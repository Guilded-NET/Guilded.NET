using Newtonsoft.Json;

namespace Guilded.Base.Users
{
    /// <summary>
    /// A social media link.
    /// </summary>
    /// <remarks>
    /// <para>Defines what platforms this user has linked to, such as <see cref="SocialLinkType.Twitch"/> and provides information about them.</para>
    /// </remarks>
    /// <seealso cref="Servers.Member"/>
    /// <seealso cref="SocialLinkType"/>
    public class SocialLink : BaseObject
    {
        #region JSON properties
        /// <summary>
        /// The type of social link it is.
        /// </summary>
        /// <value>Social link platform</value>
        public SocialLinkType Type { get; }
        /// <summary>
        /// The name or identifier in this social link.
        /// </summary>
        /// <remarks>
        /// <para>Defines a unique name or identifier of the user in the defined social link.</para>
        /// </remarks>
        /// <value>Social link handle</value>
        public string Handle { get; }
        /// <summary>
        /// The identifier of this social link.
        /// </summary>
        /// <remarks>
        /// <para>Defines the identifier of this user in the linked service.</para>
        /// </remarks>
        /// <value>Social link ID</value>
        public string? ServiceId { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="SocialLink"/>. This is currently only used in deserialization.
        /// </summary>
        /// <param name="type">The type of the socials linked</param>
        /// <param name="handle">The name, tag or identifier of the user</param>
        /// <param name="serviceId">The identifier of this social link</param>
        [JsonConstructor]
        public SocialLink(
            [JsonProperty(Required = Required.Always)]
            SocialLinkType type,

            [JsonProperty(Required = Required.Always)]
            string handle,

            [JsonProperty]
            string? serviceId
        ) =>
            (Type, Handle, ServiceId) = (type, handle, serviceId);
        #endregion
    }
}