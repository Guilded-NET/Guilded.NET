using System.Collections.Generic;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Servers;

/// <summary>
/// Represents the summary of <see cref="Member">a member</see>.
/// </summary>
/// <typeparam name="T">The type of <see cref="Users.User">the user</see> object</typeparam>
/// <seealso cref="Member" />
/// <seealso cref="MemberBan" />
/// <seealso cref="UserSummary" />
/// <seealso cref="Webhook" />
public class MemberSummary<T> : BaseObject where T : UserSummary
{
    #region Properties
    /// <summary>
    /// Gets <see cref="Users.User">the user</see> they are.
    /// </summary>
    /// <value>User</value>
    /// <seealso cref="MemberSummary{T}" />
    /// <seealso cref="Member" />
    /// <seealso cref="Id" />
    /// <seealso cref="Users.User" />
    /// <seealso cref="UserSummary" />
    public T User { get; }

    /// <summary>
    /// Gets the list of roles <see cref="Member">member</see> holds.
    /// </summary>
    /// <value>List of role IDs</value>
    /// <seealso cref="MemberSummary{T}" />
    /// <seealso cref="Member" />
    /// <seealso cref="Id" />
    public IList<uint> RoleIds { get; }

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
    /// <param name="user"><see cref="Users.User">The user</see> who is present in the server</param>
    /// <param name="roleIds">The list of roles user holds</param>
    /// <returns>New <see cref="MemberSummary{T}" /> JSON instance</returns>
    /// <seealso cref="MemberSummary{T}" />
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