using System;
using System.Collections.Generic;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Servers;

/// <summary>
/// Represents information about <see cref="User">the user</see> in <see cref="Server">a server</see>.
/// </summary>
/// <seealso cref="MemberSummary{T}" />
/// <seealso cref="MemberBan" />
/// <seealso cref="UserSummary" />
/// <seealso cref="User" />
/// <seealso cref="Webhook" />
public class Member : MemberSummary<User>
{
    #region Fields
    /// <summary>
    /// The highest number of XP you can give to a <see cref="Member">member</see>.
    /// </summary>
    public const short MaxAddXp = 1000;

    /// <summary>
    /// The lowest number of XP you can give to a <see cref="Member">member</see>.
    /// </summary>
    public const short MinAddXp = -1000;

    /// <summary>
    /// The highest number of XP you can set to a <see cref="Member">member</see>.
    /// </summary>
    public const long MaxSetXp = 1000000000;

    /// <summary>
    /// The lowest number of XP you can set to a <see cref="Member">member</see>.
    /// </summary>
    public const long MinSetXp = -1000000000;
    #endregion

    #region Properties
    /// <summary>
    /// Gets the set nickname of <see cref="Member">the member</see> in <see cref="Server">the server</see>.
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
    /// Gets whether <see cref="Member">the member</see> is the owner of <see cref="Server">the server</see>.
    /// </summary>
    /// <value>Member is owner</value>
    /// <seealso cref="Member" />
    public bool IsOwner { get; }

    /// <inheritdoc cref="User.CreatedAt" />
    public DateTime CreatedAt => User.CreatedAt;

    /// <inheritdoc cref="User.Banner" />
    public Uri? Banner => User.Banner;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="Member" />f rom the specified JSON properties.
    /// </summary>
    /// <param name="user"><see cref="User">the user</see> who is a member of <see cref="Server">the server</see></param>
    /// <param name="roleIds">The list of roles that member holds</param>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="Member">member</see> is</param>
    /// <param name="joinedAt">the date when the member joined</param>
    /// <param name="nickname">The nickname that member has</param>
    /// <param name="isOwner">Whether <see cref="Member">the member</see> is the owner of <see cref="Server">the server</see></param>
    /// <returns>New <see cref="Member" /> JSON instance</returns>
    /// <seealso cref="Member" />
    [JsonConstructor]
    public Member(
        [JsonProperty(Required = Required.Always)]
        User user,

        [JsonProperty(Required = Required.Always)]
        IList<uint> roleIds,

        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        DateTime joinedAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? nickname = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool isOwner = false
    ) : base(user, roleIds, serverId) =>
        (Nickname, JoinedAt, IsOwner) = (nickname, joinedAt, isOwner);
    #endregion
}