using Guilded.Base.Servers;
using Newtonsoft.Json;

namespace Guilded.Base.Events;

/// <summary>
/// Represents an event with the name <c>TeamMemberUpdated</c> and opcode <c>0</c> that occurs once member receives any update, apart from <see cref="RolesUpdatedEvent">role update</see>.
/// </summary>
/// <seealso cref="RolesUpdatedEvent"/>
/// <seealso cref="XpAddedEvent"/>
/// <seealso cref="WelcomeEvent"/>
/// <seealso cref="WebhookEvent"/>
/// <seealso cref="Member"/>
public class MemberUpdatedEvent : BaseObject, IServerEvent
{
    #region JSON properties
    /// <summary>
    /// Gets the properties that have been updated in the member.
    /// </summary>
    /// <remarks>
    /// <para>As of now, this only means <see cref="MemberUpdate.Nickname"/> has been updated.</para>
    /// </remarks>
    /// <value>Member info</value>
    public MemberUpdate UserInfo { get; }
    /// <summary>
    /// The identifier of the server where the <see cref="UserInfo">member</see> has been updated.
    /// </summary>
    /// <value>Server ID</value>
    public HashId ServerId { get; }
    #endregion

    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="UserInfo">member</see>.
    /// </summary>
    public HashId UserId => UserInfo.Id;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="MemberUpdatedEvent"/> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the server where the <see cref="UserInfo">member</see> has been updated</param>
    /// <param name="userInfo">The properties that have been updated in the member</param>
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
    /// Represents the properties that have been updated in the member.
    /// </summary>
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
        /// Initializes a new instance of <see cref="MemberUpdate" /> from the specified JSON properties.
        /// </summary>
        /// <param name="id">The identifier of the updated user</param>
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