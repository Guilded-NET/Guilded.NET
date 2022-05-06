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
    /// Gets the set nickname of the user in the server.
    /// </summary>
    /// <value>Name?</value>
    public string? Nickname { get; set; }
    /// <summary>
    /// Gets the date when the member joined.
    /// </summary>
    /// <value>Date</value>
    public DateTime JoinedAt { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="Member" />.
    /// </summary>
    /// <param name="user">The user who is a member of the server</param>
    /// <param name="roleIds">The list of roles that member holds</param>
    /// <param name="nickname">The nickname that member has</param>
    /// <param name="joinedAt">the date when the member joined</param>
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