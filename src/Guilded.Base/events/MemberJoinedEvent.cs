using System;
using Guilded.Base.Servers;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Events;

/// <summary>
/// An event that occurs once a member joins.
/// </summary>
/// <remarks>
/// <para>An event of the name <c>TeamMemberJoined</c> and opcode <c>0</c> that occurs once member receives any update, apart from role update(see <see cref="RolesUpdatedEvent"/>).</para>
/// </remarks>
/// <seealso cref="RolesUpdatedEvent"/>
/// <seealso cref="XpAddedEvent"/>
/// <seealso cref="MemberUpdatedEvent"/>
/// <seealso cref="WebhookEvent"/>
/// <seealso cref="WelcomeEvent"/>
/// <seealso cref="Servers.Member"/>
public class MemberJoinedEvent : BaseObject
{
    #region JSON properties
    /// <summary>
    /// The member that joined.
    /// </summary>
    /// <remarks>
    /// <para>The member that joined the server.</para>
    /// </remarks>
    /// <value>Member</value>
    public Member Member { get; }
    /// <summary>
    /// The identifier of the server where member joined.
    /// </summary>
    /// <remarks>
    /// <para>The identifier of the server where the member joined.</para>
    /// </remarks>
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
    /// Creates a new instance of <see cref="MemberJoinedEvent"/>. This is currently only used in deserialization.
    /// </summary>
    /// <param name="serverId">The identifier of the server where the member joined</param>
    /// <param name="member">The member that joined</param>
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