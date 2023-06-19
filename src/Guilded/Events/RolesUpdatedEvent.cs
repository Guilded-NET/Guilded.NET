using System;
using System.Collections.Generic;
using System.Linq;
using Guilded.Base;
using Guilded.Servers;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when a <see cref="Member">member</see> receives a role or loses it.
/// </summary>
/// <remarks>
/// <para>This event does not give a list of lost/received events or give a previous role list, so previous role list must be cached, if necessary.</para>
/// </remarks>
/// <seealso cref="MemberUpdatedEvent" />
/// <seealso cref="Member" />
public class RolesUpdatedEvent : ContentModel, IServerBased
{
    #region Properties
    /// <summary>
    /// Gets the list of receiving/losing member and current roles.
    /// </summary>
    /// <remarks>
    /// <para>If only updated users are needed, use <see cref="UpdatedUsers" /> property.</para>
    /// </remarks>
    /// <value>Member and role definition</value>
    /// <seealso cref="RolesUpdatedEvent" />
    /// <seealso cref="UpdatedUsers" />
    /// <seealso cref="ServerId" />
    public IList<UserRoleUpdate> MemberRoleIds { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="Server">server</see> where user's roles were given or removed.
    /// </summary>
    /// <value>Server ID</value>
    /// <seealso cref="RolesUpdatedEvent" />
    /// <seealso cref="MemberRoleIds" />
    /// <seealso cref="UpdatedUsers" />
    public HashId ServerId { get; }
    #endregion

    #region Properties Additional
    /// <summary>
    /// Gets the array of updated users that either lost or received roles.
    /// </summary>
    /// <returns>Array of user IDs</returns>
    /// <seealso cref="RolesUpdatedEvent" />
    /// <seealso cref="MemberRoleIds" />
    /// <seealso cref="ServerId" />
    public HashId[] UpdatedUsers => MemberRoleIds.Select(x => x.Id).ToArray();
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="RolesUpdatedEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where roles were updated</param>
    /// <param name="memberRoleIds">The list of roles and role holders</param>
    /// <returns>New <see cref="RolesUpdatedEvent" /> JSON instance</returns>
    /// <seealso cref="RolesUpdatedEvent" />
    [JsonConstructor]
    public RolesUpdatedEvent(
        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        IList<UserRoleUpdate> memberRoleIds
    ) =>
        (ServerId, MemberRoleIds) = (serverId, memberRoleIds);
    #endregion

    /// <summary>
    /// Defines a role list holder and their role list.
    /// </summary>
    /// <remarks>
    /// <para>Defines a receiving a role holder <see cref="UserId" /> and their current role list <see cref="RoleIds" />. Roles that were added or removed, or previous role list are not provided. If necessary, previous role list should be cached before-hand.</para>
    /// </remarks>
    /// <seealso cref="MemberUpdatedEvent" />
    /// <seealso cref="Member" />
    public class UserRoleUpdate : IModelHasId<HashId>
    {
        #region Properties
        /// <summary>
        /// Gets the identifier of <see cref="User">user</see> that lost or received <see cref="RoleIds">roles</see>.
        /// </summary>
        /// <value><see cref="UserSummary.Id">User ID</see></value>
        public HashId Id { get; }

        /// <inheritdoc cref="Id" />
        [Obsolete($"Use `{nameof(Id)}` instead")]
        public HashId UserId { get; }

        /// <summary>
        /// Gets the list of roles that <see cref="UserId">member</see> is currently holding.
        /// </summary>
        /// <remarks>
        /// <para>Received or removed roles are not provided, so caching of previous role list is necessary if previous role list is needed.</para>
        /// </remarks>
        /// <value>List of role IDs</value>
        public IList<uint> RoleIds { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="UserRoleUpdate" /> from the specified JSON properties.
        /// </summary>
        /// <param name="userId">The identifier of <see cref="User">user</see> who holds the roles</param>
        /// <param name="roleIds">The list of role identifiers user holds</param>
        [JsonConstructor]
        public UserRoleUpdate(
            [JsonProperty(Required = Required.Always)]
            HashId userId,

            [JsonProperty(Required = Required.Always)]
            IList<uint> roleIds
        ) =>
            (Id, RoleIds) = (userId, roleIds);
        #endregion
    }
}