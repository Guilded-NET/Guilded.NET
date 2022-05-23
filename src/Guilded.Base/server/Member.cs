using System;
using System.Collections.Generic;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Servers;

/// <summary>
/// Represents information about <see cref="User">the user</see> in a server.
/// </summary>
/// <seealso cref="MemberSummary{T}" />
/// <seealso cref="MemberBan" />
/// <seealso cref="UserSummary" />
/// <seealso cref="User" />
/// <seealso cref="Webhook" />
public class Member : MemberSummary<User>
{
    #region Properties
    /// <summary>
    /// Gets the set nickname of <see cref="Member">the member</see> in the server.
    /// </summary>
    /// <value>Name?</value>
    /// <seealso cref="Member" />
    /// <seealso cref="MemberSummary{T}.Name" />
    /// <seealso cref="MemberSummary{T}.RoleIds" />
    public string? Nickname { get; }

    /// <summary>
    /// Gets the date when <see cref="Member">the member</see> joined.
    /// </summary>
    /// <value>Date</value>
    /// <seealso cref="Member" />
    /// <seealso cref="MemberSummary{T}.Id" />
    public DateTime JoinedAt { get; }

    /// <summary>
    /// Gets whether <see cref="Member">the member</see> is the owner of the server.
    /// </summary>
    /// <value>Member is owner</value>
    /// <seealso cref="Member" />
    public bool IsOwner { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="Member" />f rom the specified JSON properties.
    /// </summary>
    /// <param name="user"><see cref="User">the user</see> who is a member of the server</param>
    /// <param name="roleIds">The list of roles that member holds</param>
    /// <param name="nickname">The nickname that member has</param>
    /// <param name="isOwner">Whether <see cref="Member">the member</see> is the owner of the server</param>
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
        string? nickname = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool isOwner = false
    ) : base(user, roleIds) =>
        (Nickname, JoinedAt, IsOwner) = (nickname, joinedAt, isOwner);
    #endregion
}