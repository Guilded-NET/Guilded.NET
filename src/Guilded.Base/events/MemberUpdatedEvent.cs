using Guilded.Base.Servers;
using Newtonsoft.Json;

namespace Guilded.Base.Events;

/// <summary>
/// An event that occurs once a member gets updated.
/// </summary>
/// <remarks>
/// <para>An event of the name <c>TeamMemberUpdated</c> and opcode <c>0</c> that occurs once member receives any update, apart from role update(see <see cref="RolesUpdatedEvent"/>).</para>
/// </remarks>
/// <seealso cref="RolesUpdatedEvent"/>
/// <seealso cref="XpAddedEvent"/>
/// <seealso cref="WelcomeEvent"/>
/// <seealso cref="Member"/>
public class MemberUpdatedEvent : BaseObject
{
    #region JSON properties
    /// <summary>
    /// The info about updated member.
    /// </summary>
    /// <remarks>
    /// <para>Defines an update that the member has received.</para>
    /// <para>As of now, this only means <see cref="Member.Nickname"/> has been updated.</para>
    /// </remarks>
    /// <value>Member info</value>
    public Member UserInfo { get; }
    /// <summary>
    /// The identifier of the server where member was updated.
    /// </summary>
    /// <remarks>
    /// <para>The identifier of the server where the member was given a new nickname, lost a nickname or any other update occurred.</para>
    /// </remarks>
    /// <value>Server ID</value>
    public HashId ServerId { get; }
    #endregion

    #region Properties
    /// <summary>
    /// The identifier of the member.
    /// </summary>
    /// <remarks>
    /// <para>Gets the identifier of the updated member.</para>
    /// <para>Fetched from <see cref="UserInfo"/> property.</para>
    /// </remarks>
    public HashId MemberId => UserInfo.Id;
    #endregion

    #region Constructors
    /// <summary>
    /// Creates a new instance of <see cref="MemberUpdatedEvent"/>. This is currently only used in deserialization.
    /// </summary>
    /// <param name="serverId">The identifier of the server where the member was updated</param>
    /// <param name="userInfo">The info about updated member</param>
    [JsonConstructor]
    public MemberUpdatedEvent(
        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        Member userInfo
    ) =>
        (ServerId, UserInfo) = (serverId, userInfo);
    #endregion
}