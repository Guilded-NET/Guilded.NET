using System;
using Guilded.Base;
using Guilded.Client;
using Guilded.Servers;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs once <see cref="Servers.Member">a member</see> joins a <see cref="Server">server</see>.
/// </summary>
/// <seealso cref="RolesUpdatedEvent" />
/// <seealso cref="MemberUpdatedEvent" />
/// <seealso cref="WebhookEvent" />
/// <seealso cref="Servers.Member" />
public class MemberJoinedEvent : IUser, IServerBased
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
    /// Gets the identifier of the <see cref="Server">server</see> where the member has joined.
    /// </summary>
    /// <value>Server ID</value>
    /// <seealso cref="MemberJoinedEvent" />
    /// <seealso cref="Member" />
    public HashId ServerId { get; }
    #endregion

    #region Properties Additional
    /// <inheritdoc cref="MemberSummary{User}.Id" />
    [Obsolete($"Use `{nameof(Id)}` instead")]
    public HashId UserId => Member.Id;

    /// <inheritdoc cref="MemberSummary{User}.Id" />
    public HashId Id => Member.Id;

    /// <inheritdoc cref="MemberSummary{User}.User" />
    public User User => Member.User;

    /// <inheritdoc cref="MemberSummary{User}.Name" />
    public string Name => Member.Name;

    /// <inheritdoc cref="MemberSummary{User}.Avatar" />
    public Uri? Avatar => Member.Avatar;

    /// <inheritdoc cref="Member.Banner" />
    public Uri? Banner => Member.Banner;

    /// <inheritdoc cref="MemberSummary{User}.Type" />
    public UserType Type => Member.Type;

    /// <inheritdoc cref="MemberSummary{User}.IsBot" />
    public bool IsBot => Member.IsBot;

    /// <inheritdoc cref="Member.JoinedAt" />
    public DateTime JoinedAt => Member.JoinedAt;

    /// <inheritdoc cref="IHasParentClient.ParentClient" />
    public AbstractGuildedClient ParentClient => User.ParentClient;
    #endregion

    #region Properties Events
    /// <inheritdoc cref="UserSummary.MemberRemoved" />
    public IObservable<MemberRemovedEvent> Removed =>
        Member.Removed;

    /// <inheritdoc cref="UserSummary.MemberJoined" />
    public IObservable<MemberJoinedEvent> Joined =>
        Member.Joined;

    /// <inheritdoc cref="UserSummary.MemberUpdated" />
    public IObservable<MemberUpdatedEvent> Updated =>
        Member.Updated;

    /// <inheritdoc cref="UserSummary.MemberBanAdded" />
    public IObservable<MemberBanEvent> BanAdded =>
        Member.BanAdded;

    /// <inheritdoc cref="UserSummary.MemberBanRemoved" />
    public IObservable<MemberBanEvent> BanRemoved =>
        Member.BanRemoved;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="MemberJoinedEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the member joined</param>
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