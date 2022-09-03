using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base.Client;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Servers;

/// <summary>
/// Represents the base type for <see cref="Member">member models</see>.
/// </summary>
/// <typeparam name="T">The type of <see cref="Users.User">the user</see> object</typeparam>
/// <seealso cref="Member" />
/// <seealso cref="MemberBan" />
/// <seealso cref="UserSummary" />
/// <seealso cref="Webhook" />
public class MemberSummary<T> : IModelHasId<HashId> where T : UserSummary
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

    /// <summary>
    /// Gets the identifier of the <see cref="Server">server</see> where the <see cref="Member">member</see> is.
    /// </summary>
    /// <value><see cref="Server.Id">Server ID</see></value>
    public HashId ServerId { get; }

    /// <inheritdoc cref="UserSummary.Id" />
    public HashId Id => User.Id;

    /// <inheritdoc cref="UserSummary.Name" />
    public string Name => User.Name;

    /// <inheritdoc cref="UserSummary.Avatar" />
    public Uri? Avatar => User.Avatar;

    /// <inheritdoc cref="UserSummary.Type" />
    public UserType Type => User.Type;

    /// <inheritdoc cref="UserSummary.IsBot" />
    public bool IsBot => User.IsBot;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="MemberSummary{T}" />.
    /// </summary>
    /// <param name="user"><see cref="Users.User">The user</see> who is present in <see cref="Server">the server</see></param>
    /// <param name="roleIds">The list of roles user holds</param>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="Member">member</see> is</param>
    /// <returns>New <see cref="MemberSummary{T}" /> JSON instance</returns>
    /// <seealso cref="MemberSummary{T}" />
    public MemberSummary(
        T user,

        IList<uint> roleIds,

        HashId serverId
    ) =>
        (User, RoleIds, ServerId) = (user, roleIds, serverId);
    #endregion

    #region Methods
    /// <inheritdoc cref="BaseGuildedClient.GetSocialLinkAsync(HashId, HashId, SocialLinkType)" />
    public Task<SocialLink> GetSocialLinkAsync(SocialLinkType linkType) =>
        User.GetSocialLinkAsync(ServerId, linkType);

    /// <inheritdoc cref="BaseGuildedClient.UpdateNicknameAsync(HashId, HashId, string)" />
    public Task<string> UpdateNicknameAsync(string nickname) =>
        User.UpdateNicknameAsync(ServerId, nickname);

    /// <inheritdoc cref="BaseGuildedClient.DeleteNicknameAsync(HashId, HashId)" />
    public Task DeleteNicknameAsync() =>
        User.DeleteNicknameAsync(ServerId);

    /// <inheritdoc cref="BaseGuildedClient.AddRoleAsync(HashId, HashId, uint)" />
    public Task AddRoleAsync(uint role) =>
        User.AddRoleAsync(ServerId, role);

    /// <inheritdoc cref="BaseGuildedClient.RemoveRoleAsync(HashId, HashId, uint)" />
    public Task RemoveRoleAsync(uint role) =>
        User.RemoveRoleAsync(ServerId, role);

    /// <inheritdoc cref="BaseGuildedClient.AddXpAsync(HashId, HashId, short)" />
    public Task<long> AddXpAsync(short amount) =>
        User.AddXpAsync(ServerId, amount);

    /// <inheritdoc cref="BaseGuildedClient.RemoveMemberAsync(HashId, HashId)" />
    public Task RemoveAsync() =>
        User.RemoveMemberAsync(ServerId);

    /// <inheritdoc cref="BaseGuildedClient.AddMemberBanAsync(HashId, HashId, string?)" />
    public Task AddBanAsync(string? reason = null) =>
        User.AddMemberBanAsync(ServerId, reason);

    /// <inheritdoc cref="BaseGuildedClient.RemoveMemberBanAsync(HashId, HashId)" />
    public Task RemoveBanAsync() =>
        User.RemoveMemberBanAsync(ServerId);

    /// <inheritdoc cref="BaseGuildedClient.GetBanAsync(HashId, HashId)" />
    public Task GetBanAsync() =>
        User.GetBanAsync(ServerId);
    #endregion
}

/// <summary>
/// Represents the summary of <see cref="Member">a member</see>.
/// </summary>
/// <seealso cref="Member" />
/// <seealso cref="MemberBan" />
/// <seealso cref="UserSummary" />
/// <seealso cref="Webhook" />
public class MemberSummary : MemberSummary<UserSummary>
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="MemberSummary{T}" />.
    /// </summary>
    /// <param name="user"><see cref="User">The user</see> who is present in <see cref="Server">the server</see></param>
    /// <param name="roleIds">The list of roles user holds</param>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="Member">member</see> is</param>
    /// <returns>New <see cref="MemberSummary{T}" /> JSON instance</returns>
    /// <seealso cref="MemberSummary{T}" />
    [JsonConstructor]
    public MemberSummary(
        [JsonProperty(Required = Required.Always)]
        UserSummary user,

        [JsonProperty(Required = Required.Always)]
        IList<uint> roleIds,

        [JsonProperty(Required = Required.Always)]
        HashId serverId
    ) : base(user, roleIds, serverId) { }
    #endregion
}