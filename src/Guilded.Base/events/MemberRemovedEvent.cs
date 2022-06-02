using Guilded.Base.Servers;
using Newtonsoft.Json;

namespace Guilded.Base.Events;

/// <summary>
/// Represents an event that occurs when <see cref="Member">a member</see> leaves a server or gets removed from it.
/// </summary>
/// <seealso cref="RolesUpdatedEvent" />
/// <seealso cref="XpAddedEvent" />
/// <seealso cref="MemberJoinedEvent" />
/// <seealso cref="MemberUpdatedEvent" />
/// <seealso cref="MemberBanEvent" />
/// <seealso cref="Member" />
public class MemberRemovedEvent : BaseModel, IServerEvent
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the member who has been kicked or has left.
    /// </summary>
    /// <value><see cref="Users.UserSummary.Id">User ID</see></value>
    /// <seealso cref="MemberRemovedEvent" />
    /// <seealso cref="ServerId" />
    /// <seealso cref="IsKick" />
    /// <seealso cref="IsBan" />
    public HashId UserId { get; }

    /// <summary>
    /// Gets the identifier of the server where the member has been kicked or has left.
    /// </summary>
    /// <value>Server ID</value>
    /// <seealso cref="MemberRemovedEvent" />
    /// <seealso cref="UserId" />
    public HashId ServerId { get; }

    /// <summary>
    /// Gets whether <see cref="Users.User">the user</see> has been kicked.
    /// </summary>
    /// <value>User was kicked</value>
    /// <seealso cref="MemberRemovedEvent" />
    /// <seealso cref="UserId" />
    /// <seealso cref="IsBan" />
    /// <seealso cref="ServerId" />
    public bool IsKick { get; }

    /// <summary>
    /// Gets whether <see cref="Users.User">the user</see> has been banned.
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
    /// <param name="serverId">The identifier of the server where the member got kicked or left</param>
    /// <param name="userId">The identifier of the member who got kicked or left</param>
    /// <param name="isKick">Whether <see cref="Users.User">the user</see> has been kicked</param>
    /// <param name="isBan">Whether <see cref="Users.User">the user</see> has been banned</param>
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
        (ServerId, UserId, IsKick, IsBan) = (serverId, userId, isKick, isBan);
    #endregion
}