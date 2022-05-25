using Guilded.Base.Servers;
using Newtonsoft.Json;

namespace Guilded.Base.Events;

/// <summary>
/// Represents an event with the name <c>TeamMemberUpdated</c> and opcode <c>0</c> that occurs once member receives any update, apart from <see cref="RolesUpdatedEvent">role update</see>.
/// </summary>
/// <seealso cref="RolesUpdatedEvent" />
/// <seealso cref="XpAddedEvent" />
/// <seealso cref="MemberJoinedEvent" />
/// <seealso cref="MemberRemovedEvent" />
/// <seealso cref="Member" />
public class MemberUpdatedEvent : BaseModel, IServerEvent
{
    #region Properties
    /// <summary>
    /// Gets the properties that have been updated in the member.
    /// </summary>
    /// <remarks>
    /// <para>As of now, this only means <see cref="MemberUpdate.Nickname" /> has been updated.</para>
    /// </remarks>
    /// <value>Member info</value>
    /// <seealso cref="MemberUpdatedEvent" />
    /// <seealso cref="UserId" />
    /// <seealso cref="ServerId" />
    public MemberUpdate UserInfo { get; }

    /// <summary>
    /// The identifier of the server where the <see cref="UserInfo">member</see> has been updated.
    /// </summary>
    /// <value>Server ID</value>
    /// <seealso cref="MemberUpdatedEvent" />
    /// <seealso cref="UserId" />
    /// <seealso cref="UserInfo" />
    public HashId ServerId { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="UserInfo">member</see>.
    /// </summary>
    /// <value><see cref="Users.UserSummary.Id">User ID</see></value>
    /// <seealso cref="MemberUpdatedEvent" />
    /// <seealso cref="UserInfo" />
    /// <seealso cref="ServerId" />
    public HashId UserId => UserInfo.Id;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="MemberUpdatedEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the server where the <see cref="UserInfo">member</see> has been updated</param>
    /// <param name="userInfo">The properties that have been updated in the member</param>
    /// <returns>New <see cref="MemberUpdatedEvent" /> JSON instance</returns>
    /// <seealso cref="MemberUpdatedEvent" />
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
    public class MemberUpdate : BaseModel
    {
        #region Properties
        /// <inheritdoc cref="Users.UserSummary.Id" />
        public HashId Id { get; set; }

        /// <inheritdoc cref="Member.Nickname" />
        public string? Nickname { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="MemberUpdate" /> from the specified JSON properties.
        /// </summary>
        /// <param name="id">The identifier of the updated user</param>
        /// <param name="nickname">The updated nickname of <see cref="Users.User">the user</see></param>
        /// <returns>New <see cref="MemberUpdate" /> JSON instance</returns>
        /// <seealso cref="MemberUpdate" />
        [JsonConstructor]
        public MemberUpdate(
            [JsonProperty(Required = Required.Always)]
            HashId id,

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            string? nickname = null
        ) =>
            (Id, Nickname) = (id, nickname);
        #endregion
    }
}