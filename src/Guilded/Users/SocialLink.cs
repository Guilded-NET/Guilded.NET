using System;
using Guilded.Base;
using Newtonsoft.Json;

namespace Guilded.Users;

/// <summary>
/// Represents a social media link of a <see cref="User">user</see>.
/// </summary>
/// <seealso cref="ClientUser" />
/// <seealso cref="User" />
/// <seealso cref="Servers.Member" />
/// <seealso cref="UserSummary" />
/// <seealso cref="SocialLinkType" />
public class SocialLink : ContentModel
{
    #region Properties
    /// <summary>
    /// Gets the type of <see cref="SocialLink">social link</see> it is.
    /// </summary>
    /// <value>The type of <see cref="SocialLink">social link</see> it is</value>
    /// <seealso cref="SocialLink" />
    /// <seealso cref="ServiceId" />
    /// <seealso cref="Handle" />
    public SocialLinkType Type { get; }

    /// <summary>
    /// Gets the <see cref="User">user</see> whose <see cref="SocialLink">social link</see> it is.
    /// </summary>
    /// <value>The <see cref="User">user</see> whose <see cref="SocialLink">social link</see> it is</value>
    /// <seealso cref="SocialLink" />
    /// <seealso cref="UserId" />
    /// <seealso cref="Type" />
    /// <seealso cref="ServiceId" />
    public HashId UserId { get; }

    /// <summary>
    /// Gets the name, unique identifier or unique tag of the <see cref="User">user</see> in the <see cref="SocialLink">social link</see>.
    /// </summary>
    /// <value>The name, unique identifier or unique tag of the <see cref="User">user</see> in the <see cref="SocialLink">social link</see></value>
    /// <seealso cref="SocialLink" />
    /// <seealso cref="ServiceId" />
    /// <seealso cref="UserId" />
    /// <seealso cref="Type" />
    public string? Handle { get; }

    /// <summary>
    /// Gets the unique identifier of the <see cref="User">user</see> in the <see cref="SocialLink">social link</see>.
    /// </summary>
    /// <value>The unique identifier of the <see cref="User">user</see> in the <see cref="SocialLink">social link</see></value>
    /// <seealso cref="SocialLink" />
    /// <seealso cref="Handle" />
    /// <seealso cref="UserId" />
    /// <seealso cref="Type" />
    public string? ServiceId { get; }

    /// <summary>
    /// Gets the date when the <see cref="SocialLink">social link</see> was created at.
    /// </summary>
    /// <value>The date when the <see cref="SocialLink">social link</see> was created at</value>
    /// <seealso cref="SocialLink" />
    /// <seealso cref="UserId" />
    /// <seealso cref="Type" />
    /// <seealso cref="ServiceId" />
    public DateTime? CreatedAt { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="SocialLink" /> from the specified JSON properties.
    /// </summary>
    /// <param name="type">The type of social link it is</param>
    /// <param name="userId">The <see cref="User">user</see> whose <see cref="SocialLink">social link</see> it is</param>
    /// <param name="handle">The name, unique identifier or unique tag of the <see cref="User">user</see> in this social link</param>
    /// <param name="serviceId">The unique identifier of the <see cref="User">user</see> in this social link</param>
    /// <param name="createdAt">The date when the <see cref="SocialLink">social link</see> was created at</param>
    /// <returns>New <see cref="SocialLink" /> JSON instance</returns>
    /// <seealso cref="SocialLink" />
    [JsonConstructor]
    public SocialLink(
        [JsonProperty(Required = Required.Always)]
        SocialLinkType type,

        [JsonProperty(Required = Required.Always)]
        HashId userId,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? handle = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? serviceId = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? createdAt = null
    ) =>
        (Type, UserId, Handle, ServiceId, CreatedAt) = (type, userId, handle, serviceId, createdAt);
    #endregion
}