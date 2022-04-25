using System.Collections.Generic;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Servers;

/// <summary>
/// Represents the summary about a member.
/// </summary>
/// <typeparam name="T">The type of the user object</typeparam>
public class MemberSummary<T> : BaseObject where T : UserSummary
{
    #region JSON properties
    /// <summary>
    /// Gets the user they are.
    /// </summary>
    /// <value>User</value>
    public T User { get; set; }
    /// <summary>
    /// Gets the list of roles user holds.
    /// </summary>
    /// <value>List of role IDs</value>
    public IList<uint> RoleIds { get; set; }
    #endregion

    #region Properties
    /// <inheritdoc cref="UserSummary.Id" />
    public HashId Id => User.Id;
    /// <inheritdoc cref="UserSummary.Name" />
    public string Name => User.Name;
    /// <inheritdoc cref="UserSummary.Type" />
    public UserType Type => User.Type;
    /// <inheritdoc cref="UserSummary.IsBot" />
    public bool IsBot => User.IsBot;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="MemberSummary{T}" />.
    /// </summary>
    /// <param name="user">The user that is present in the server</param>
    /// <param name="roleIds">The list of roles user holds</param>
    [JsonConstructor]
    public MemberSummary(
        [JsonProperty(Required = Required.Always)]
        T user,

        [JsonProperty(Required = Required.Always)]
        IList<uint> roleIds
    ) =>
        (User, RoleIds) = (user, roleIds);
    #endregion
}