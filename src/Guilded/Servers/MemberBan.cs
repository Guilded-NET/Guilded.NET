using System;
using Guilded.Base;
using Guilded.Client;
using Guilded.Content;
using Guilded.Events;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Servers;

/// <summary>
/// Represents the information of <see cref="Users.User">user's</see> ban.
/// </summary>
/// <seealso cref="Member" />
/// <seealso cref="MemberSummary{T}" />
/// <seealso cref="Users.User" />
public class MemberBan : IHasParentClient, ICreatableContent, IServerBased
{
    #region Properties
    /// <summary>
    /// Gets the <see cref="Users.User">user</see> who has been banned.
    /// </summary>
    /// <value>The <see cref="Users.User">user</see> who has been banned</value>
    /// <seealso cref="MemberBan" />
    /// <seealso cref="ServerId" />
    /// <seealso cref="Reason" />
    /// <seealso cref="CreatedAt" />
    /// <seealso cref="CreatedBy" />
    public UserSummary User { get; }

    /// <summary>
    /// Gets the reason why the <see cref="User">user</see> has been banned, if the reason was specified by <see cref="Server">server's</see> staff <see cref="Member">member</see>.
    /// </summary>
    /// <value>The reason why the <see cref="User">user</see> has been banned</value>
    /// <seealso cref="MemberBan" />
    /// <seealso cref="ServerId" />
    /// <seealso cref="User" />
    /// <seealso cref="CreatedAt" />
    /// <seealso cref="CreatedBy" />
    public string? Reason { get; }

    /// <summary>
    /// Gets the date when the <see cref="User">user</see> has been banned.
    /// </summary>
    /// <value>The date when the <see cref="User">user</see> has been banned</value>
    /// <seealso cref="MemberBan" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="Reason" />
    /// <seealso cref="User" />
    /// <seealso cref="ServerId" />
    public DateTime CreatedAt { get; }

    /// <summary>
    /// Gets the identifier of the staff <see cref="Member">member</see> who has banned the <see cref="User">user</see>.
    /// </summary>
    /// <value>The identifier of the staff <see cref="Member">member</see> who has banned the <see cref="User">user</see></value>
    /// <seealso cref="MemberBan" />
    /// <seealso cref="CreatedAt" />
    /// <seealso cref="User" />
    /// <seealso cref="ServerId" />
    /// <seealso cref="Reason" />
    public HashId CreatedBy { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="Server">server</see> where the <see cref="User">user</see> has been banned.
    /// </summary>
    /// <value>The identifier of the <see cref="Server">server</see> where the <see cref="User">user</see> has been banned</value>
    /// <seealso cref="MemberBan" />
    /// <seealso cref="User" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="CreatedAt" />
    /// <seealso cref="Reason" />
    public HashId ServerId { get; }

    /// <inheritdoc cref="IHasParentClient.ParentClient" />
    public AbstractGuildedClient ParentClient => User.ParentClient;
    #endregion

    #region Properties Events
    /// <inheritdoc cref="UserSummary.MemberBanAdded" />
    public IObservable<MemberBanEvent> Added =>
        User.MemberBanAdded.InServer(ServerId);

    /// <inheritdoc cref="UserSummary.MemberBanRemoved" />
    public IObservable<MemberBanEvent> Removed =>
        User.MemberBanRemoved.InServer(ServerId);
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="MemberBan" /> with the provided details.
    /// </summary>
    /// <param name="user">The <see cref="Users.User">user</see> who has been banned</param>
    /// <param name="createdBy">The identifier of the staff <see cref="Member">member</see> who has banned the <see cref="User">user</see></param>
    /// <param name="createdAt">The date when the <see cref="User">user</see> has been banned</param>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="User">user</see> has been banned</param>
    /// <param name="reason">The reason why the <see cref="User">user</see> has been banned, if the reason was specified</param>
    /// <returns>New <see cref="MemberBan" /> JSON instance</returns>
    /// <seealso cref="MemberBan" />
    [JsonConstructor]
    public MemberBan(
        [JsonProperty(Required = Required.Always)]
        UserSummary user,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? reason = null
    ) =>
        (User, CreatedBy, CreatedAt, Reason, ServerId) = (user, createdBy, createdAt, reason, serverId);
    #endregion
}