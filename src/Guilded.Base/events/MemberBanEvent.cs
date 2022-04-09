using System;
using Guilded.Base.Servers;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Events;

/// <summary>
/// An event that occurs once a member gets (un)banned.
/// </summary>
/// <remarks>
/// <para>An event of the name <c>TeamMemberBanned</c> or <c>TeamMemberUnbanned</c> and opcode <c>0</c> that occurs once member gets banned or unbanned.</para>
/// </remarks>
/// <seealso cref="RolesUpdatedEvent"/>
/// <seealso cref="XpAddedEvent"/>
/// <seealso cref="MemberJoinedEvent"/>
/// <seealso cref="MemberUpdatedEvent"/>
/// <seealso cref="WelcomeEvent"/>
/// <seealso cref="MemberRemovedEvent"/>
/// <seealso cref="Member"/>
public class MemberBanEvent : BaseObject
{
    #region JSON properties
    /// <summary>
    /// The member's ban information.
    /// </summary>
    /// <remarks>
    /// <para>The information about the ban that member received.</para>
    /// </remarks>
    /// <value>Member ban</value>
    public MemberBan MemberBan { get; }
    /// <summary>
    /// The identifier of the server where member got (un)banned.
    /// </summary>
    /// <remarks>
    /// <para>The identifier of the server where the member got banned or unbanned.</para>
    /// </remarks>
    /// <value>Server ID</value>
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
    /// Creates a new instance of <see cref="MemberBanEvent"/>. This is currently only used in deserialization.
    /// </summary>
    /// <param name="serverId">The identifier of the server where the member got (un)banned</param>
    /// <param name="serverMemberBan">The member's ban information</param>
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