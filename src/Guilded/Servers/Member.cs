using System;
using System.Collections.Generic;
using Guilded.Base;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Servers;

/// <summary>
/// Represents a <see cref="User">user</see> in a <see cref="Server">server</see> and information about their membership.
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
    /// Gets the nickname of the <see cref="Member">member</see> that has been set in the <see cref="Server">server</see>.
    /// </summary>
    /// <value>The nickname of the <see cref="Member">member</see> that has been set in the <see cref="Server">server</see></value>
    /// <seealso cref="Member" />
    /// <seealso cref="MemberSummary{T}.Name" />
    /// <seealso cref="MemberSummary{T}.RoleIds" />
    public string? Nickname { get; }

    /// <summary>
    /// Gets the date when the <see cref="Member">member</see> joined.
    /// </summary>
    /// <value>The date when the <see cref="Member">member</see> joined.</value>
    /// <seealso cref="Member" />
    /// <seealso cref="MemberSummary{T}.Id" />
    public DateTime JoinedAt { get; }

    /// <summary>
    /// Gets whether the <see cref="Member">member</see> is the owner of the <see cref="Server">server</see>.
    /// </summary>
    /// <remarks>
    /// <para>This can be useful for checking if individual is member an owner of a particular <see cref="Server">server</see>. Never go through every member in a <see cref="Server">server</see> to see who is the owner of it. In those scenarios you should use <see cref="Server.OwnerId" />.</para>
    /// </remarks>
    /// <value>Whether the <see cref="Member">member</see> is the owner of the <see cref="Server">server</see></value>
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
    /// <param name="user">The <see cref="User">user</see> who is a <see cref="Member">member</see> of the <see cref="Server">server</see></param>
    /// <param name="roleIds">The list of roles that the <see cref="Member">member</see> holds</param>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="Member">member</see> is</param>
    /// <param name="joinedAt">The date when the <see cref="Member">member</see> joined</param>
    /// <param name="nickname">The nickname of the <see cref="Member">member</see> that has been set in the <see cref="Server">server</see></param>
    /// <param name="isOwner">Whether the <see cref="Member">member</see> is the owner of the <see cref="Server">server</see></param>
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