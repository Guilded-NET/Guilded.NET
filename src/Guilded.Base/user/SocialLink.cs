using Newtonsoft.Json;

namespace Guilded.Base.Users;

/// <summary>
/// Represents a social media link of a <see cref="User">user</see>.
/// </summary>
/// <seealso cref="Me" />
/// <seealso cref="User" />
/// <seealso cref="Servers.Member" />
/// <seealso cref="UserSummary" />
/// <seealso cref="SocialLinkType" />
public class SocialLink : BaseObject
{
    #region JSON properties
    /// <summary>
    /// Gets the type of social link it is.
    /// </summary>
    /// <value>Social link platform</value>
    /// <seealso cref="SocialLink" />
    /// <seealso cref="ServiceId" />
    /// <seealso cref="Handle" />
    public SocialLinkType Type { get; }
    /// <summary>
    /// Gets the name, unique identifier or unique tag of the <see cref="User">user</see> in this social link.
    /// </summary>
    /// <value>Social link handle</value>
    /// <seealso cref="SocialLink" />
    /// <seealso cref="ServiceId" />
    /// <seealso cref="Type" />
    public string? Handle { get; }
    /// <summary>
    /// Gets the unique identifier of the <see cref="User">user</see> in this social link.
    /// </summary>
    /// <value>Social link ID</value>
    /// <seealso cref="SocialLink" />
    /// <seealso cref="Handle" />
    /// <seealso cref="Type" />
    public string? ServiceId { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="SocialLink" /> from the specified JSON properties.
    /// </summary>
    /// <param name="type">The type of social link it is</param>
    /// <param name="handle">The name, unique identifier or unique tag of the <see cref="User">user</see> in this social link</param>
    /// <param name="serviceId">The unique identifier of the <see cref="User">user</see> in this social link</param>
    /// <returns>New <see cref="SocialLink" /> JSON instance</returns>
    /// <seealso cref="SocialLink" />
    [JsonConstructor]
    public SocialLink(
        [JsonProperty(Required = Required.Always)]
        SocialLinkType type,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? handle = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? serviceId = null
    ) =>
        (Type, Handle, ServiceId) = (type, handle, serviceId);
    #endregion
}