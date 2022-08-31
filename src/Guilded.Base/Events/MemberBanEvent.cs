using System;
using Guilded.Base.Servers;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Events;

/// <summary>
/// Represents an event that occurs when <see cref="Member">a memner</see> gets banned or unbanned.
/// </summary>
/// <seealso cref="RolesUpdatedEvent" />
/// <seealso cref="XpAddedEvent" />
/// <seealso cref="MemberJoinedEvent" />
/// <seealso cref="MemberUpdatedEvent" />
/// <seealso cref="MemberRemovedEvent" />
/// <seealso cref="Member" />
public class MemberBanEvent
{
    #region Properties
    /// <summary>
    /// Gets the information about the member's ban.
    /// </summary>
    /// <value>Member ban</value>
    /// <seealso cref="MemberBanEvent" />
    /// <seealso cref="User" />
    /// <seealso cref="ServerId" />
    public MemberBan MemberBan { get; }

    /// <summary>
    /// Gets the identifier of <see cref="Server">the server</see> where member has been banned/unbanned.
    /// </summary>
    /// <value>Server ID</value>
    /// <seealso cref="MemberBanEvent" />
    /// <seealso cref="MemberBan" />
    public HashId ServerId { get; }
    #endregion

    #region Properties
    /// <inheritdoc cref="MemberBan.User" />
    public UserSummary User => MemberBan.User;

    /// <inheritdoc cref="MemberBan.Reason" />
    public string? Reason => MemberBan.Reason;

    /// <inheritdoc cref="MemberBan.CreatedAt" />
    public DateTime CreatedAt => MemberBan.CreatedAt;

    /// <inheritdoc cref="MemberBan.CreatedBy" />
    public HashId CreatedBy => MemberBan.CreatedBy;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="MemberBanEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of <see cref="Server">the server</see> where member got banned/unbanned</param>
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