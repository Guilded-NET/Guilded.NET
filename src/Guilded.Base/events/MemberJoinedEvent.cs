using System;
using Guilded.Base.Servers;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Events;

/// <summary>
/// Represents an event with the name <c>TeamMemberJoined</c> and opcode <c>0</c> that occurs once <see cref="Member">member</see> joins a <see cref="ServerId">server</see>.
/// </summary>
/// <seealso cref="RolesUpdatedEvent"/>
/// <seealso cref="XpAddedEvent"/>
/// <seealso cref="MemberUpdatedEvent"/>
/// <seealso cref="WebhookEvent"/>
/// <seealso cref="WelcomeEvent"/>
/// <seealso cref="Servers.Member"/>
public class MemberJoinedEvent : BaseObject, IServerEvent
{
    #region JSON properties
    /// <summary>
    /// Gets the member who has joined.
    /// </summary>
    /// <value>Member</value>
    public Member Member { get; }
    /// <summary>
    /// Gets the identifier of the server where the member has joined.
    /// </summary>
    /// <value>Server ID</value>
    public HashId ServerId { get; }
    #endregion

    #region Properties
    /// <inheritdoc cref="MemberSummary{User}.Id" />
    public HashId UserId => Member.Id;
    /// <inheritdoc cref="MemberSummary{User}.Name" />
    public string Name => Member.Name;
    /// <inheritdoc cref="MemberSummary{User}.Type" />
    public UserType Type => Member.Type;
    /// <inheritdoc cref="MemberSummary{User}.IsBot" />
    public bool IsBot => Member.IsBot;
    /// <inheritdoc cref="Member.JoinedAt" />
    public DateTime JoinedAt => Member.JoinedAt;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="MemberJoinedEvent"/> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the server where the member joined</param>
    /// <param name="member">The member who has joined</param>
    [JsonConstructor]
    public MemberJoinedEvent(
        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        Member member
    ) =>
        (ServerId, Member) = (serverId, member);
    #endregion
}