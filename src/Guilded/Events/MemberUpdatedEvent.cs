using System;
using Guilded.Base;
using Guilded.Client;
using Guilded.Servers;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when member's server profile receives any kind of change, besides <see cref="RolesUpdatedEvent">change in their role list</see>.
/// </summary>
/// <seealso cref="RolesUpdatedEvent" />
/// <seealso cref="MemberJoinedEvent" />
/// <seealso cref="MemberRemovedEvent" />
/// <seealso cref="Member" />
public class MemberUpdatedEvent : IModelHasId<HashId>, IServerBased
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
    /// The identifier of the <see cref="Server">server</see> where the <see cref="UserInfo">member</see> has been updated.
    /// </summary>
    /// <value>Server ID</value>
    /// <seealso cref="MemberUpdatedEvent" />
    /// <seealso cref="UserId" />
    /// <seealso cref="UserInfo" />
    public HashId ServerId { get; }
    #endregion

    #region Properties Additional
    /// <summary>
    /// Gets the identifier of the <see cref="UserInfo">member</see>.
    /// </summary>
    /// <value><see cref="UserSummary.Id">User ID</see></value>
    /// <seealso cref="MemberUpdatedEvent" />
    /// <seealso cref="UserInfo" />
    /// <seealso cref="ServerId" />
    public HashId Id => UserInfo.Id;

    /// <inheritdoc cref="Id" />
    [Obsolete($"Use `{nameof(Id)}` instead")]
    public HashId UserId => UserInfo.Id;

    /// <inheritdoc cref="IHasParentClient.ParentClient" />
    public AbstractGuildedClient ParentClient => UserInfo.ParentClient;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="MemberUpdatedEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="UserInfo">member</see> has been updated</param>
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
    /// <seealso cref="UserSummary" />
    /// <seealso cref="MemberSummary{T}" />
    /// <seealso cref="User" />
    /// <seealso cref="Member" />
    public class MemberUpdate : ContentModel, IModelHasId<HashId>
    {
        #region Properties
        /// <inheritdoc cref="UserSummary.Id" />
        public HashId Id { get; set; }

        /// <inheritdoc cref="Member.Nickname" />
        public string? Nickname { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="MemberUpdate" /> from the specified JSON properties.
        /// </summary>
        /// <param name="id">The identifier of the updated user</param>
        /// <param name="nickname">The updated nickname of the <see cref="User">user</see></param>
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