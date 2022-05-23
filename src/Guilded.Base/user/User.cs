using System;
using Newtonsoft.Json;

namespace Guilded.Base.Users;

/// <summary>
/// Represents the extended information about a user.
/// </summary>
/// <seealso cref="SocialLink" />
/// <seealso cref="UserSummary" />
/// <seealso cref="Me" />
public class User : UserSummary
{
    #region Properties
    /// <summary>
    /// Gets the date when <see cref="Users.User">the user</see> has registered their account.
    /// </summary>
    /// <value>Date</value>
    /// <seealso cref="User" />
    /// <seealso cref="UserSummary" />
    public DateTime CreatedAt { get; }

    /// <summary>
    /// Gets the global banner of <see cref="Users.User">the user</see>.
    /// </summary>
    /// <value>Media URL?</value>
    /// <seealso cref="User" />
    /// <seealso cref="UserSummary" />
    /// <seealso cref="UserSummary.Avatar" />
    /// <seealso cref="UserSummary.Name" />
    public Uri? Banner { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="UserSummary" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of <see cref="User">user</see></param>
    /// <param name="type">The type of <see cref="Users.User">the user</see> they are</param>
    /// <param name="name">The global username of <see cref="Users.User">the user</see></param>
    /// <param name="createdAt">the date when <see cref="Users.User">the user</see> was created</param>
    /// <param name="avatar">The global avatar of <see cref="Users.User">the user</see></param>
    /// <param name="banner">The global banner of <see cref="Users.User">the user</see></param>
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
        UserType type = UserType.User
    ) : base(id, name, avatar, type) =>
        (Banner, CreatedAt) = (banner, createdAt);
    #endregion
}