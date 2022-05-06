using System;
using Newtonsoft.Json;

namespace Guilded.Base.Users;

/// <summary>
/// Represents the extended information about a user.
/// </summary>
/// <seealso cref="SocialLink" />
/// <seealso cref="UserSummary" />
public class User : UserSummary
{
    #region JSON properties
    /// <summary>
    /// Gets the date when the user has registered their account.
    /// </summary>
    /// <value>Date</value>
    public DateTime CreatedAt { get; }
    /// <summary>
    /// Gets the global banner of the user.
    /// </summary>
    /// <value>Media URL?</value>
    public Uri? Banner { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="UserSummary"/> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of <see cref="User">user</see></param>
    /// <param name="type">The type of the user they are</param>
    /// <param name="name">The global username of the user</param>
    /// <param name="createdAt">the date when the user was created</param>
    /// <param name="avatar">The global avatar of the user</param>
    /// <param name="banner">The global banner of the user</param>
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