using System;
using Guilded.Base;
using Guilded.Client;
using Guilded.Servers;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when a <see cref="Member">member</see> gets banned or unbanned.
/// </summary>
/// <seealso cref="RolesUpdatedEvent" />
/// <seealso cref="MemberJoinedEvent" />
/// <seealso cref="MemberUpdatedEvent" />
/// <seealso cref="MemberRemovedEvent" />
/// <seealso cref="Member" />
public class MemberBanEvent : IUser, IServerBased
{
    #region Properties
    /// <summary>
    /// Gets the information about the <see cref="Servers.MemberBan">member's ban</see>.
    /// </summary>
    /// <value>Member ban</value>
    /// <seealso cref="MemberBanEvent" />
    /// <seealso cref="User" />
    /// <seealso cref="ServerId" />
    public MemberBan MemberBan { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="Server">server</see> where the <see cref="Member">member</see> has been banned/unbanned.
    /// </summary>
    /// <value>The identifier of the <see cref="Server">server</see> where the <see cref="Member">member</see> has been banned/unbanned</value>
    /// <seealso cref="MemberBanEvent" />
    /// <seealso cref="MemberBan" />
    public HashId ServerId { get; }
    #endregion

    #region Properties Additional
    /// <inheritdoc cref="MemberBan.User" />
    public HashId Id => User.Id;

    /// <inheritdoc cref="MemberBan.User" />
    public UserSummary User => MemberBan.User;

    /// <inheritdoc cref="UserSummary.Name" />
    public string Name => User.Name;

    /// <inheritdoc cref="MemberBan.Reason" />
    public string? Reason => MemberBan.Reason;

    /// <inheritdoc cref="MemberBan.CreatedAt" />
    public DateTime CreatedAt => MemberBan.CreatedAt;

    /// <inheritdoc cref="MemberBan.CreatedBy" />
    public HashId CreatedBy => MemberBan.CreatedBy;

    /// <inheritdoc cref="IHasParentClient.ParentClient" />
    public AbstractGuildedClient ParentClient => User.ParentClient;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="MemberBanEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where member got banned/unbanned</param>
    /// <param name="serverMemberBan">The information about the member's ban</param>
    /// <returns>New <see cref="MemberBanEvent" /> JSON instance</returns>
    /// <seealso cref="MemberBanEvent" />
    [JsonConstructor]
    public MemberBanEvent(
        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        MemberBan serverMemberBan
    ) =>
        (ServerId, MemberBan) = (serverId, serverMemberBan);
    #endregion
}