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
    public DateTime CreatedAt { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="UserSummary"/> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of <see cref="User">user</see></param>
    /// <param name="type">The type of the user they are</param>
    /// <param name="name">The global username of the user</param>
    /// <param name="createdAt">The date of when the user was created</param>
    [JsonConstructor]
    public User(
        [JsonProperty(Required = Required.Always)]
        HashId id,

        [JsonProperty(Required = Required.Always)]
        UserType type,

        [JsonProperty(Required = Required.Always)]
        string name,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt
    ) : base(id, type, name) =>
        CreatedAt = createdAt;
    #endregion
}