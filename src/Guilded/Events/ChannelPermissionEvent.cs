using System;
using Guilded.Base;
using Guilded.Permissions;
using Guilded.Servers;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when someone creates, updates or deletes a <see cref="ChannelUserPermission">user permission</see> from a <see cref="ServerChannel">server channel</see>.
/// </summary>
/// <seealso cref="ChannelRolePermissionEvent" /> 
/// <seealso cref="CategoryUserPermissionEvent" /> 
/// <seealso cref="CategoryRolePermissionEvent" /> 
public class ChannelUserPermissionEvent : AbstractServerPermissionEvent<ChannelUserPermission>, IChannelPermission, IServerUserPermission
{
    #region Properties Additional
    /// <inheritdoc cref="IServerUserPermission.UserId" />
    public HashId UserId => PermissionOverride.UserId;

    /// <inheritdoc cref="IChannelBased.ChannelId" />
    public Guid ChannelId => PermissionOverride.ChannelId;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ChannelUserPermissionEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the channel event occurred</param>
    /// <param name="channelUserPermission">The <see cref="ChannelUserPermissionEvent">permission</see> received from the event</param>
    /// <returns>New <see cref="ChannelUserPermissionEvent" /> JSON instance</returns>
    /// <seealso cref="ChannelUserPermission" />
    [JsonConstructor]
    public ChannelUserPermissionEvent(
        [JsonProperty(Required = Required.Always)]
        ChannelUserPermission channelUserPermission,

        [JsonProperty(Required = Required.Always)]
        HashId serverId
    ) : base(channelUserPermission, serverId) { }
    #endregion
}

/// <summary>
/// Represents an event that occurs when someone creates, updates or deletes a <see cref="ChannelRolePermission">role permission</see> from a <see cref="ServerChannel">server channel</see>.
/// </summary>
/// <seealso cref="ChannelUserPermissionEvent" /> 
/// <seealso cref="CategoryUserPermissionEvent" /> 
/// <seealso cref="CategoryRolePermissionEvent" /> 
public class ChannelRolePermissionEvent : AbstractServerPermissionEvent<ChannelRolePermission>, IChannelPermission, IServerRolePermission
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
    public ChannelRolePermissionEvent(
        [JsonProperty(Required = Required.Always)]
        ChannelRolePermission channelRolePermission,

        [JsonProperty(Required = Required.Always)]
        HashId serverId
    ) : base(channelRolePermission, serverId) { }
    #endregion
}