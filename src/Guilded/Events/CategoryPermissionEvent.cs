using System;
using Guilded.Base;
using Guilded.Permissions;
using Guilded.Servers;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when someone creates, updates or deletes a <see cref="CategoryUserPermission">user permission</see> from a <see cref="ServerChannel">server channel</see>.
/// </summary>
/// <seealso cref="CategoryRolePermissionEvent" /> 
/// <seealso cref="ChannelUserPermissionEvent" /> 
/// <seealso cref="ChannelRolePermissionEvent" /> 
public class CategoryUserPermissionEvent : AbstractServerPermissionEvent<CategoryUserPermission>, ICategoryPermission, IServerUserPermission
{
    #region Properties Additional
    /// <inheritdoc cref="IServerUserPermission.UserId" />
    public HashId UserId => PermissionOverride.UserId;

    /// <inheritdoc cref="ICategoryPermission.CategoryId" />
    public uint CategoryId => PermissionOverride.CategoryId;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CategoryUserPermissionEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the channel event occurred</param>
    /// <param name="CategoryUserPermission">The <see cref="CategoryUserPermissionEvent">permission</see> received from the event</param>
    /// <returns>New <see cref="CategoryUserPermissionEvent" /> JSON instance</returns>
    /// <seealso cref="CategoryUserPermission" />
    [JsonConstructor]
    public CategoryUserPermissionEvent(
        [JsonProperty(Required = Required.Always)]
        CategoryUserPermission CategoryUserPermission,

        [JsonProperty(Required = Required.Always)]
        HashId serverId
    ) : base(CategoryUserPermission, serverId) { }
    #endregion
}

/// <summary>
/// Represents an event that occurs when someone creates, updates or deletes a <see cref="ChannelRolePermission">role permission</see> from a <see cref="ServerChannel">server channel</see>.
/// </summary>
/// <seealso cref="CategoryUserPermissionEvent" /> 
/// <seealso cref="ChannelUserPermissionEvent" /> 
/// <seealso cref="ChannelRolePermissionEvent" /> 
public class CategoryRolePermissionEvent : AbstractServerPermissionEvent<ChannelRolePermission>, IChannelPermission, IServerRolePermission
{
    #region Properties Additional
    /// <inheritdoc cref="IServerRolePermission.RoleId" />
    public uint RoleId => PermissionOverride.RoleId;

    /// <inheritdoc cref="IChannelBased.ChannelId" />
    public Guid ChannelId => PermissionOverride.ChannelId;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ChannelRolePermissionEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the channel event occurred</param>
    /// <param name="channelRolePermission">The <see cref="ChannelRolePermissionEvent">permission</see> received from the event</param>
    /// <returns>New <see cref="ChannelRolePermissionEvent" /> JSON instance</returns>
    /// <seealso cref="ChannelRolePermission" />
    [JsonConstructor]
    public CategoryRolePermissionEvent(
        [JsonProperty(Required = Required.Always)]
        ChannelRolePermission channelRolePermission,

        [JsonProperty(Required = Required.Always)]
        HashId serverId
    ) : base(channelRolePermission, serverId) { }
    #endregion
}