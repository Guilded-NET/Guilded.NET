using Guilded.Base.Servers;
using Newtonsoft.Json;

namespace Guilded.Base.Events;

/// <summary>
/// An event that occurs once a member joins.
/// </summary>
/// <remarks>
/// <para>An event of the name <c>TeamMemberRemoved</c> and opcode <c>0</c> that occurs once member leaves or gets kicked.</para>
/// </remarks>
/// <seealso cref="RolesUpdatedEvent"/>
/// <seealso cref="XpAddedEvent"/>
/// <seealso cref="MemberJoinedEvent"/>
/// <seealso cref="MemberUpdatedEvent"/>
/// <seealso cref="WelcomeEvent"/>
/// <seealso cref="Member"/>
public class MemberRemovedEvent : BaseObject
{
    #region JSON properties
    /// <summary>
    /// The identifier of the member that got kicked or left.
    /// </summary>
    /// <remarks>
    /// <para>The identifier of the member that got kicked or left the server.</para>
    /// </remarks>
    /// <value>User ID</value>
    public HashId UserId { get; }
    /// <summary>
    /// The identifier of the server where member got kicked or left.
    /// </summary>
    /// <remarks>
    /// <para>The identifier of the server where the member got kicked or left.</para>
    /// </remarks>
    /// <value>Server ID</value>
    public HashId ServerId { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Creates a new instance of <see cref="MemberRemovedEvent"/>. This is currently only used in deserialization.
    /// </summary>
    /// <param name="serverId">The identifier of the server where the member got kicked or left</param>
    /// <param name="userId">The identifier of the member that got kicked or left</param>
    [JsonConstructor]
    public MemberRemovedEvent(
        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        HashId userId
    ) =>
        (ServerId, UserId) = (serverId, userId);
    #endregion
}