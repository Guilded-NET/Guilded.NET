using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Client;
using Guilded.Events;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Servers;

/// <summary>
/// Represents the base type for <see cref="Member">member models</see>.
/// </summary>
/// <typeparam name="T">The type of the <see cref="User">user</see> object</typeparam>
/// <seealso cref="Member" />
/// <seealso cref="MemberBan" />
/// <seealso cref="UserSummary" />
/// <seealso cref="Webhook" />
public class MemberSummary<T> : IHasParentClient, IUser, IServerBased where T : UserSummary
{
    #region Properties
    /// <summary>
    /// Gets the <see cref="User">user</see> they are.
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
    /// <value>The list of roles <see cref="Member">member</see> holds</value>
    /// <seealso cref="MemberSummary{T}" />
    /// <seealso cref="Member" />
    /// <seealso cref="Id" />
    public IList<uint> RoleIds { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="Server">server</see> where the <see cref="Member">member</see> is.
    /// </summary>
    /// <value>The identifier of the <see cref="Server">server</see> where the <see cref="Member">member</see> is</value>
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

    /// <inheritdoc cref="IHasParentClient.ParentClient" />
    public AbstractGuildedClient ParentClient => User.ParentClient;
    #endregion

    #region Properties Events
    /// <inheritdoc cref="UserSummary.MemberRemoved" />
    public IObservable<MemberRemovedEvent> Removed =>
        User.MemberRemoved.InServer(ServerId);

    /// <inheritdoc cref="UserSummary.MemberJoined" />
    public IObservable<MemberJoinedEvent> Joined =>
        User.MemberJoined.InServer(ServerId);

    /// <inheritdoc cref="UserSummary.MemberUpdated" />
    public IObservable<MemberUpdatedEvent> Updated =>
        User.MemberUpdated.InServer(ServerId);

    /// <inheritdoc cref="UserSummary.MemberBanAdded" />
    public IObservable<MemberBanEvent> BanAdded =>
        User.MemberBanAdded.InServer(ServerId);

    /// <inheritdoc cref="UserSummary.MemberBanRemoved" />
    public IObservable<MemberBanEvent> BanRemoved =>
        User.MemberBanRemoved.InServer(ServerId);
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="MemberSummary{T}" />.
    /// </summary>
    /// <param name="user">The <see cref="Users.User">user</see> who is present in the <see cref="Server">server</see></param>
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
    /// <inheritdoc cref="AbstractGuildedClient.GetSocialLinkAsync(HashId, HashId, SocialLinkType)" />
    public Task<SocialLink> GetSocialLinkAsync(SocialLinkType linkType) =>
        User.GetSocialLinkAsync(ServerId, linkType);

    /// <inheritdoc cref="AbstractGuildedClient.SetNicknameAsync(HashId, HashId, string)" />
    public Task<string> SetNicknameAsync(string nickname) =>
        User.SetNicknameAsync(ServerId, nickname);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveNicknameAsync(HashId, HashId)" />
    public Task RemoveNicknameAsync() =>
        User.RemoveNicknameAsync(ServerId);

    /// <inheritdoc cref="AbstractGuildedClient.AddMemberRoleAsync(HashId, HashId, uint)" />
    public Task AddRoleAsync(uint role) =>
        User.AddMemberRoleAsync(ServerId, role);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveMemberRoleAsync(HashId, HashId, uint)" />
    public Task RemoveRoleAsync(uint role) =>
        User.RemoveMemberRoleAsync(ServerId, role);

    /// <inheritdoc cref="AbstractGuildedClient.GetMemberPermissionsAsync(HashId, HashId)" />
    public Task<IList<Permission>> GetPermissionsAsync() =>
        User.GetMemberPermissionsAsync(ServerId);

    /// <inheritdoc cref="AbstractGuildedClient.AddXpAsync(HashId, HashId, short)" />
    public Task<long> AddXpAsync(short amount) =>
        User.AddXpAsync(ServerId, amount);

    /// <inheritdoc cref="AbstractGuildedClient.SetXpAsync(HashId, HashId, long)" />
    public Task<long> SetXpAsync(long amount) =>
        User.SetXpAsync(ServerId, amount);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveMemberAsync(HashId, HashId)" />
    public Task RemoveAsync() =>
        User.RemoveMemberAsync(ServerId);

    /// <inheritdoc cref="AbstractGuildedClient.AddMemberBanAsync(HashId, HashId, string?)" />
    public Task AddBanAsync(string? reason = null) =>
        User.AddMemberBanAsync(ServerId, reason);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveMemberBanAsync(HashId, HashId)" />
    public Task RemoveBanAsync() =>
        User.RemoveMemberBanAsync(ServerId);

    /// <inheritdoc cref="AbstractGuildedClient.GetMemberBanAsync(HashId, HashId)" />
    public Task GetBanAsync() =>
        User.GetMemberBanAsync(ServerId);
    #endregion
}

/// <summary>
/// Represents the summary of a <see cref="User">user</see> in a <see cref="Server">server</see> and information about their membership.
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
    /// <param name="user">The <see cref="User">user</see> who is present in the <see cref="Server">server</see></param>
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