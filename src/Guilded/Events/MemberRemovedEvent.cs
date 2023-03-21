using System;
using Guilded.Base;
using Guilded.Servers;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when a <see cref="Member">member</see> leaves a <see cref="Server">server</see> or gets removed from it.
/// </summary>
/// <seealso cref="RolesUpdatedEvent" />
/// <seealso cref="MemberJoinedEvent" />
/// <seealso cref="MemberUpdatedEvent" />
/// <seealso cref="MemberBanEvent" />
/// <seealso cref="Member" />
public class MemberRemovedEvent : ContentModel, IModelHasId<HashId>, IServerBased
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the member who has been kicked or has left.
    /// </summary>
    /// <value><see cref="UserSummary.Id">User ID</see></value>
    /// <seealso cref="MemberRemovedEvent" />
    /// <seealso cref="ServerId" />
    /// <seealso cref="IsKick" />
    /// <seealso cref="IsBan" />
    public HashId Id { get; }

    /// <inheritdoc cref="Id" />
    [Obsolete($"Use `{nameof(Id)}` instead")]
    public HashId UserId { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="Server">server</see> where the member has been kicked or has left.
    /// </summary>
    /// <value>Server ID</value>
    /// <seealso cref="MemberRemovedEvent" />
    /// <seealso cref="UserId" />
    public HashId ServerId { get; }

    /// <summary>
    /// Gets whether the <see cref="User">user</see> has been kicked.
    /// </summary>
    /// <value>User was kicked</value>
    /// <seealso cref="MemberRemovedEvent" />
    /// <seealso cref="UserId" />
    /// <seealso cref="IsBan" />
    /// <seealso cref="ServerId" />
    public bool IsKick { get; }

    /// <summary>
    /// Gets whether the <see cref="User">user</see> has been banned.
    /// </summary>
    /// <value>User was banned</value>
    /// <seealso cref="MemberRemovedEvent" />
    /// <seealso cref="UserId" />
    /// <seealso cref="IsKick" />
    /// <seealso cref="ServerId" />
    public bool IsBan { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="MemberRemovedEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the member got kicked or left</param>
    /// <param name="userId">The identifier of the member who got kicked or left</param>
    /// <param name="isKick">Whether the <see cref="User">user</see> has been kicked</param>
    /// <param name="isBan">Whether the <see cref="User">user</see> has been banned</param>
    /// <returns>New <see cref="MemberRemovedEvent" /> JSON instance</returns>
    /// <seealso cref="MemberRemovedEvent" />
    [JsonConstructor]
    public MemberRemovedEvent(
        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        HashId userId,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool isKick = false,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool isBan = false
    ) =>
        (ServerId, Id, IsKick, IsBan) = (serverId, userId, isKick, isBan);
    #endregion
}