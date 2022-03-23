using System;
using Newtonsoft.Json;

namespace Guilded.Base.Users;

/// <summary>
/// Global information about a user.
/// </summary>
/// <remarks>
/// <para>Defines a normal user with extended information.</para>
/// </remarks>
/// <seealso cref="SocialLink" />
/// <seealso cref="UserSummary" />
public class User : UserSummary
{
    #region JSON properties
    /// <summary>
    /// The date when the user was created.
    /// </summary>
    /// <remarks>
    /// <para>The date of when the user has registered their account or when the bot was created.</para>
    /// </remarks>
    /// <value>Date</value>
    public DateTime CreatedAt { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Creates a new instance of <see cref="UserSummary"/> with specified properties.
    /// </summary>
    /// <param name="id">The identifier of the user</param>
    /// <param name="type">The type of user they are</param>
    /// <param name="name">The name of the user</param>
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