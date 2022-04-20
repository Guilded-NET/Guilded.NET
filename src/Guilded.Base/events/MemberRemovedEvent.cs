using Guilded.Base.Servers;
using Newtonsoft.Json;

namespace Guilded.Base.Events;

/// <summary>
/// Represents an event with the name <c>TeamMemberRemoved</c> and opcode <c>0</c> that occurs once member leaves or gets <see cref="IsKick">kicked</see>.
/// </summary>
/// <seealso cref="RolesUpdatedEvent"/>
/// <seealso cref="XpAddedEvent"/>
/// <seealso cref="MemberJoinedEvent"/>
/// <seealso cref="MemberUpdatedEvent"/>
/// <seealso cref="MemberBanEvent"/>
/// <seealso cref="WelcomeEvent"/>
/// <seealso cref="Member"/>
public class MemberRemovedEvent : BaseObject, IServerEvent
{
    #region JSON properties
    /// <summary>
    /// Gets the identifier of the member that has been kicked or has left.
    /// </summary>
    /// <value>User ID</value>
    public HashId UserId { get; }
    /// <summary>
    /// Gets the identifier of the server where the member has been kicked or has left.
    /// </summary>
    /// <value>Server ID</value>
    public HashId ServerId { get; }
    /// <summary>
    /// Gets whether the user has been kicked.
    /// </summary>
    /// <value>User was kicked</value>
    public bool IsKick { get; }
    /// <summary>
    /// Gets whether the user has been banned.
    /// </summary>
    /// <value>User was banned</value>
    public bool IsBan { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="MemberRemovedEvent"/> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the server where the member got kicked or left</param>
    /// <param name="userId">The identifier of the member that got kicked or left</param>
    /// <param name="isKick">Whether the user has been kicked</param>
    /// <param name="isBan">Whether the user has been banned</param>
    [JsonConstructor]
    public MemberRemovedEvent(
        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        HashId userId,

        [JsonProperty(Required = Required.Always)]
        bool isKick,

        [JsonProperty(Required = Required.Always)]
        bool isBan
    ) =>
        (ServerId, UserId, IsKick, IsBan) = (serverId, userId, isKick, isBan);
    #endregion
}