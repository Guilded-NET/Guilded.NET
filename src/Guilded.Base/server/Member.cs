using System;
using System.Collections.Generic;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Servers;

/// <summary>
/// A server member.
/// </summary>
/// <remarks>
/// <para>The full information about a member in the server.</para>
/// </remarks>
/// <seealso cref="MemberSummary{T}" />
/// <seealso cref="MemberBan" />
/// <seealso cref="UserSummary" />
/// <seealso cref="User" />
/// <seealso cref="Webhook" />
public class Member : MemberSummary<User>
{
    #region JSON properties
    /// <summary>
    /// Gets the set nickname of <see cref="Users.User">the user</see> in the server.
    /// </summary>
    /// <value>Name?</value>
    /// <seealso cref="Member" />
    /// <seealso cref="MemberSummary{T}.Name" />
    /// <seealso cref="MemberSummary{T}.RoleIds" />
    public string? Nickname { get; set; }
    /// <summary>
    /// Gets the date when the member joined.
    /// </summary>
    /// <value>Date</value>
    /// <seealso cref="Member" />
    /// <seealso cref="MemberSummary{T}.Id" />
    public DateTime JoinedAt { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="Member" />f rom the specified JSON properties.
    /// </summary>
    /// <param name="user"><see cref="User">the user</see> who is a member of the server</param>
    /// <param name="roleIds">The list of roles that member holds</param>
    /// <param name="nickname">The nickname that member has</param>
    /// <param name="joinedAt">the date when the member joined</param>
    /// <returns>New <see cref="Member" /> JSON instance</returns>
    /// <seealso cref="Member" />
    [JsonConstructor]
    public Member(
        [JsonProperty(Required = Required.Always)]
        User user,

        [JsonProperty(Required = Required.Always)]
        IList<uint> roleIds,

        [JsonProperty(Required = Required.Always)]
        DateTime joinedAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? nickname = null
    ) : base(user, roleIds) =>
        (Nickname, JoinedAt) = (nickname, joinedAt);
    #endregion
}