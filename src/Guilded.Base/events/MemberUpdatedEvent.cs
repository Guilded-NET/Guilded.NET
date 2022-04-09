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
/// <seealso cref="WebhookEvent"/>
/// <seealso cref="Member"/>
public class MemberUpdatedEvent : BaseObject
{
    #region JSON properties
    /// <summary>
    /// The info about updated member.
    /// </summary>
    /// <remarks>
    /// <para>Defines an update that the member has received.</para>
    /// <para>As of now, this only means <see cref="MemberUpdate.Nickname"/> has been updated.</para>
    /// </remarks>
    /// <value>Member info</value>
    public MemberUpdate UserInfo { get; }
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
    /// </remarks>
    public HashId UserId => UserInfo.Id;
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
        MemberUpdate userInfo
    ) =>
        (ServerId, UserInfo) = (serverId, userInfo);
    #endregion

    /// <summary>
    /// The updated information about a member.
    /// </summary>
    /// <remarks>
    /// <para>The information that has been updated about a member.</para>
    /// </remarks>
    /// <seealso cref="Users.UserSummary" />
    /// <seealso cref="MemberSummary{T}" />
    /// <seealso cref="Users.User" />
    /// <seealso cref="Member" />
    public class MemberUpdate : BaseObject
    {
        /// <inheritdoc cref="Users.UserSummary.Id" />
        public HashId Id { get; set; }
        /// <inheritdoc cref="Member.Nickname" />
        public string? Nickname { get; set; }
        /// <summary>
        /// Creates a new instance of <see cref="MemberUpdate" />.
        /// </summary>
        /// <param name="id">The identifier of the user</param>
        /// <param name="nickname">The updated nickname of the user</param>
        [JsonConstructor]
        public MemberUpdate(
            [JsonProperty(Required = Required.Always)]
            HashId id,

            [JsonProperty]
            string? nickname
        ) =>
            (Id, Nickname) = (id, nickname);
    }
}