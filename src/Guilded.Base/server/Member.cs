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
/// <seealso cref="UserSummary" />
/// <seealso cref="User" />
public class Member : MemberSummary<User>
{
    #region JSON properties
    /// <summary>
    /// The nickname of the user.
    /// </summary>
    /// <remarks>
    /// <para>The name of the user that is only present in the server.</para>
    /// </remarks>
    /// <value>Name?</value>
    public string? Nickname { get; set; }
    /// <summary>
    /// The date of when the member joined.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="DateTime" /> of when the member has joined the server.</para>
    /// </remarks>
    /// <value>Date</value>
    public DateTime JoinedAt { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Creates a new instance of <see cref="Member" />.
    /// </summary>
    /// <param name="user">The user that is a member of the server</param>
    /// <param name="roleIds">The list of roles that member holds</param>
    /// <param name="nickname">The nickname that member has</param>
    /// <param name="joinedAt">The date of when the member joined</param>
    [JsonConstructor]
    public Member(
        [JsonProperty(Required = Required.Always)]
        User user,

        [JsonProperty(Required = Required.Always)]
        IList<uint> roleIds,

        [JsonProperty]
        string? nickname,

        [JsonProperty(Required = Required.Always)]
        DateTime joinedAt
    ) : base(user, roleIds) =>
        (Nickname, JoinedAt) = (nickname, joinedAt);
    #endregion
}