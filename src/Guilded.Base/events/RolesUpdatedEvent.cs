using System.Collections.Generic;
using System.Linq;
using Guilded.Base.Servers;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Events;

/// <summary>
/// Represents an event with the name <c>teamRolesUpdated</c> and opcode <c>0</c> that occurs once <see cref="RolesUpdated.UserId">role holder</see> either loses a <see cref="RolesUpdated.RoleIds">role</see> or receives it.
/// </summary>
/// <remarks>
/// <para>This event does not give a list of lost/received events or give a previous role list, so previous role list must be cached, if necessary.</para>
/// </remarks>
/// <seealso cref="MemberUpdatedEvent" />
/// <seealso cref="XpAddedEvent" />
/// <seealso cref="Member" />
public class RolesUpdatedEvent : BaseObject, IServerEvent
{
    #region JSON properties
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
    public IList<RolesUpdated> MemberRoleIds { get; }
    /// <summary>
    /// Gets the identifier of the server where user's roles were given or removed.
    /// </summary>
    /// <value>Server ID</value>
    /// <seealso cref="RolesUpdatedEvent" />
    /// <seealso cref="MemberRoleIds" />
    /// <seealso cref="UpdatedUsers" />
    public HashId ServerId { get; }
    #endregion

    #region Properties
    /// <summary>
    /// Gets the array of updated users that either lost or received roles.
    /// </summary>
    /// <returns>Array of user IDs</returns>
    /// <seealso cref="RolesUpdatedEvent" />
    /// <seealso cref="MemberRoleIds" />
    /// <seealso cref="ServerId" />
    public HashId[] UpdatedUsers => MemberRoleIds.Select(x => x.UserId).ToArray();
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="RolesUpdatedEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the server where roles were updated</param>
    /// <param name="memberRoleIds">The list of roles and role holders</param>
    /// <returns>New <see cref="RolesUpdatedEvent" /> JSON instance</returns>
    /// <seealso cref="RolesUpdatedEvent" />
    [JsonConstructor]
    public RolesUpdatedEvent(
        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        IList<RolesUpdated> memberRoleIds
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
    public class RolesUpdated
    {
        #region JSON properties
        /// <summary>
        /// Gets the identifier of <see cref="User">user</see> that lost or received <see cref="RoleIds">roles</see>.
        /// </summary>
        /// <value><see cref="Users.UserSummary.Id">User ID</see></value>
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
        /// Initializes a new instance of <see cref="RolesUpdated" /> from the specified JSON properties.
        /// </summary>
        /// <param name="userId">The identifier of <see cref="User">user</see> who holds the roles</param>
        /// <param name="roleIds">The list of role identifiers user holds</param>
        [JsonConstructor]
        public RolesUpdated(
            [JsonProperty(Required = Required.Always)]
            HashId userId,

            [JsonProperty(Required = Required.Always)]
            IList<uint> roleIds
        ) =>
            (UserId, RoleIds) = (userId, roleIds);
        #endregion
    }
}