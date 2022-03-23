using System.Collections.Generic;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Servers;

/// <summary>
/// A summary about a member.
/// </summary>
/// <remarks>
/// <para>A summary object about a server member.</para>
/// </remarks>
/// <typeparam name="T">The type of the user object</typeparam>
public class MemberSummary<T> : BaseObject where T : UserSummary
{
    #region JSON properties
    /// <summary>
    /// The user they are.
    /// </summary>
    /// <remarks>
    /// <para>The user that is the member of the server.</para>
    /// </remarks>
    /// <value>User</value>
    public T User { get; set; }
    /// <summary>
    /// The list of roles user holds.
    /// </summary>
    /// <remarks>
    /// <para>The list of role IDs that user is currently holding.</para>
    /// </remarks>
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
    /// Creates a new instance of <see cref="MemberSummary{T}" />.
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