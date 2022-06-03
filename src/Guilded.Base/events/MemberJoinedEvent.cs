using System;
using Guilded.Base.Servers;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Events;

/// <summary>
/// Represents an event that occurs once <see cref="Servers.Member">a member</see> joins <see cref="Server">a server</see>.
/// </summary>
/// <seealso cref="RolesUpdatedEvent" />
/// <seealso cref="XpAddedEvent" />
/// <seealso cref="MemberUpdatedEvent" />
/// <seealso cref="WebhookEvent" />
/// <seealso cref="Servers.Member" />
public class MemberJoinedEvent : BaseModel, IServerEvent
{
    #region Properties
    /// <summary>
    /// Gets the member who has joined.
    /// </summary>
    /// <value>Member</value>
    /// <seealso cref="MemberJoinedEvent" />
    /// <seealso cref="Name" />
    /// <seealso cref="ServerId" />
    public Member Member { get; }

    /// <summary>
    /// Gets the identifier of <see cref="Server">the server</see> where the member has joined.
    /// </summary>
    /// <value>Server ID</value>
    /// <seealso cref="MemberJoinedEvent" />
    /// <seealso cref="Member" />
    public HashId ServerId { get; }

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
    /// Initializes a new instance of <see cref="MemberJoinedEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of <see cref="Server">the server</see> where the member joined</param>
    /// <param name="member">The member who has joined</param>
    /// <returns>New <see cref="MemberJoinedEvent" /> JSON instance</returns>
    /// <seealso cref="MemberJoinedEvent" />
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