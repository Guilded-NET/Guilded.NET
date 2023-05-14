using System;
using Guilded.Base;
using Newtonsoft.Json;

namespace Guilded.Users;

/// <summary>
/// Represents the extended information about a user.
/// </summary>
/// <seealso cref="SocialLink" />
/// <seealso cref="UserSummary" />
/// <seealso cref="ClientUser" />
public class User : UserSummary
{
    #region Properties
    /// <summary>
    /// Gets the date when the <see cref="User">user</see> has registered their account.
    /// </summary>
    /// <value>The date when the <see cref="User">user</see> has registered their account</value>
    /// <seealso cref="User" />
    /// <seealso cref="UserSummary" />
    public DateTime CreatedAt { get; }

    /// <summary>
    /// Gets the global banner of the <see cref="User">user</see>.
    /// </summary>
    /// <value>The global banner of the <see cref="User">user</see></value>
    /// <seealso cref="User" />
    /// <seealso cref="UserSummary" />
    /// <seealso cref="UserSummary.Avatar" />
    /// <seealso cref="UserSummary.Name" />
    /// <seealso cref="Status" />
    public Uri? Banner { get; }

    /// <summary>
    /// Gets the global <see cref="UserStatus">status</see> of the <see cref="User">user</see>.
    /// </summary>
    /// <value>The global <see cref="UserStatus">status</see> of the <see cref="User">user</see></value>
    /// <seealso cref="User" />
    /// <seealso cref="UserSummary.Avatar" />
    /// <seealso cref="Banner" />
    public UserStatus? Status { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="User" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of <see cref="User">user</see></param>
    /// <param name="name">The global username of the <see cref="User">user</see></param>
    /// <param name="createdAt">The date when the <see cref="User">user</see> was created</param>
    /// <param name="avatar">The global avatar of the <see cref="User">user</see></param>
    /// <param name="banner">The global banner of the <see cref="User">user</see></param>
    /// <param name="type">The type of the <see cref="User">user</see> they are</param>
    /// <param name="status">The type of the <see cref="User">user</see> they are</param>
    /// <returns>New <see cref="User" /> JSON instance</returns>
    /// <seealso cref="User" />
    [JsonConstructor]
    public User(
        [JsonProperty(Required = Required.Always)]
        HashId id,

        [JsonProperty(Required = Required.Always)]
        string name,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Uri? avatar = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Uri? banner = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        UserType type = UserType.User,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        UserStatus? status = null
    ) : base(id, name, avatar, type) =>
        (Banner, Status, CreatedAt) = (banner, status, createdAt);
    #endregion
}