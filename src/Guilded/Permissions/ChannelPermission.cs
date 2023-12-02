using System;
using System.Collections.Generic;
using Guilded.Base;
using Guilded.Servers;
using Newtonsoft.Json;

namespace Guilded.Permissions;

/// <summary>
/// Represents overriden <see cref="Permission">permissions</see> of a <see cref="Role">role</see> in a <see cref="ServerChannel">channel</see>.  
/// </summary>
/// <seealso cref="ChannelUserPermission" />
/// <seealso cref="CategoryRolePermission" />
/// <seealso cref="CategoryUserPermission" />
/// <seealso cref="AbstractServerPermission" /> 
public class ChannelRolePermission : AbstractServerPermission
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="ServerChannel">server channel</see> where the <see cref="Permission">permissions</see> are overriden.
    /// </summary>
    /// <value>The identifier of the <see cref="ServerChannel">server channel</see> where the <see cref="Permission">permissions</see> are overriden</value>
    /// <seealso cref="AbstractServerPermission" />
    /// <seealso cref="ChannelRolePermission" />
    /// <seealso cref="RoleId" />
    public Guid ChannelId { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="Role">server role</see> that has its <see cref="Permission">permissions</see> overriden.
    /// </summary>
    /// <value>The identifier of the <see cref="Role">server role</see> that has its <see cref="Permission">permissions</see> overriden</value>
    /// <seealso cref="AbstractServerPermission" />
    /// <seealso cref="ChannelRolePermission" />
    /// <seealso cref="ChannelId" />
    public uint RoleId { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ChannelRolePermission" /> from the specified JSON properties.
    /// </summary>
    /// <param name="permissions">The list of <see cref="Permission">permissions</see> that are enabled or disabled for specific <see cref="Member">members</see></param>
    /// <param name="channelId">The identifier of the <see cref="ServerChannel">server channel</see> where the <see cref="Permission">permissions</see> are overriden</param>
    /// <param name="roleId">The identifier of the <see cref="Role">server role</see> that has its <see cref="Permission">permissions</see> overriden</param>
    /// <param name="createdAt">The date when the <see cref="AbstractServerPermission">permission change</see> was created</param>
    /// <param name="updatedAt">The date when the <see cref="AbstractServerPermission">permission change</see> was last edited</param>
    /// <returns>New <see cref="ChannelRolePermission" /> JSON instance</returns>
    /// <seealso cref="ChannelRolePermission" />
    [JsonConstructor]
    public ChannelRolePermission(
        [JsonProperty(Required = Required.Always)]
        IDictionary<Permission, bool> permissions,

        [JsonProperty(Required = Required.Always)]
        Guid channelId,

        [JsonProperty(Required = Required.Always)]
        uint roleId,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? updatedAt = null
    ) : base(permissions, createdAt, updatedAt) =>
        (ChannelId, RoleId) = (channelId, roleId);
    #endregion
}

/// <summary>
/// Represents overriden <see cref="Permission">permissions</see> of a <see cref="Role">role</see> in a <see cref="ServerChannel">channel</see>.  
/// </summary>
/// <seealso cref="ChannelRolePermission" />
/// <seealso cref="CategoryUserPermission" />
/// <seealso cref="CategoryRolePermission" />
/// <seealso cref="AbstractServerPermission" /> 
public class ChannelUserPermission : AbstractServerPermission
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="ServerChannel">server channel</see> where the <see cref="Permission">permissions</see> are overriden.
    /// </summary>
    /// <value>The identifier of the <see cref="ServerChannel">server channel</see> where the <see cref="Permission">permissions</see> are overriden</value>
    /// <seealso cref="AbstractServerPermission" />
    /// <seealso cref="ChannelRolePermission" />
    /// <seealso cref="UserId" />
    public Guid ChannelId { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="Member">member</see> that has their <see cref="Permission">permissions</see> overriden.
    /// </summary>
    /// <value>The identifier of the <see cref="Member">member</see> that has their <see cref="Permission">permissions</see> overriden</value>
    /// <seealso cref="AbstractServerPermission" />
    /// <seealso cref="ChannelRolePermission" />
    /// <seealso cref="ChannelId" />
    public HashId UserId { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ChannelRolePermission" /> from the specified JSON properties.
    /// </summary>
    /// <param name="permissions">The list of <see cref="Permission">permissions</see> that are enabled or disabled for specific <see cref="Member">members</see></param>
    /// <param name="channelId">The identifier of the <see cref="ServerChannel">server channel</see> where the <see cref="Permission">permissions</see> were overriden</param>
    /// <param name="userId">The identifier of the <see cref="Member">member</see> that has their <see cref="Permission">permissions</see> overriden</param>
    /// <param name="createdAt">The date when the <see cref="AbstractServerPermission">permission change</see> was created</param>
    /// <param name="updatedAt">The date when the <see cref="AbstractServerPermission">permission change</see> was last edited</param>
    /// <returns>New <see cref="ChannelRolePermission" /> JSON instance</returns>
    /// <seealso cref="ChannelUserPermission" />
    public ChannelUserPermission(
        [JsonProperty(Required = Required.Always)]
        IDictionary<Permission, bool> permissions,

        [JsonProperty(Required = Required.Always)]
        Guid channelId,

        [JsonProperty(Required = Required.Always)]
        HashId userId,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? updatedAt = null
    ) : base(permissions, createdAt, updatedAt) =>
        (ChannelId, UserId) = (channelId, userId);
    #endregion
}