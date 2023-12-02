using System;
using System.Collections.Generic;
using Guilded.Base;
using Guilded.Client;
using Guilded.Permissions;
using Guilded.Servers;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when someone creates, updates or deletes a <see cref="AbstractServerPermission">permission</see> from a <see cref="ServerChannel">server channel</see> or <see cref="Category">category</see>.
/// </summary>
/// <seealso cref="ChannelUserPermissionEvent" />
/// <seealso cref="ChannelRolePermissionEvent" />
/// <seealso cref="CategoryUserPermissionEvent" />
/// <seealso cref="CategoryRolePermissionEvent" />
/// <seealso cref="CategoryEvent" />
/// <seealso cref="ChannelEvent" />
public abstract class AbstractServerPermissionEvent<T> : IServerPermission, ICreationDated, IUpdatableContent where T : IServerPermission
{
    #region Properties
    /// <summary>
    /// Gets the <see cref="AbstractServerPermissionEvent{T}">permission</see> received from the event.
    /// </summary>
    /// <value>The <see cref="AbstractServerPermissionEvent{T}">permission</see> received from the event</value>
    /// <seealso cref="AbstractServerPermissionEvent{T}" />
    /// <seealso cref="Permissions" />
    /// <seealso cref="ServerId" />
    public T PermissionOverride { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="Server">server</see> where the event occurred.
    /// </summary>
    /// <value>The identifier of the <see cref="Server">server</see> where the event occurred</value>
    /// <seealso cref="AbstractServerPermissionEvent{T}" />
    /// <seealso cref="PermissionOverride" />
    public HashId ServerId { get; }
    #endregion

    #region Properties Additional
    /// <inheritdoc cref="IServerPermission.Permissions" />
    public IDictionary<Permission, bool> Permissions => PermissionOverride.Permissions;

    /// <inheritdoc cref="ICreationDated.CreatedAt" />
    public DateTime CreatedAt => PermissionOverride.CreatedAt;

    /// <inheritdoc cref="IUpdatableContent.UpdatedAt" />
    public DateTime? UpdatedAt => PermissionOverride.UpdatedAt;

    /// <inheritdoc cref="IHasParentClient.ParentClient" />
    public AbstractGuildedClient ParentClient => PermissionOverride.ParentClient;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="AbstractServerPermissionEvent{T}" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the channel event occurred</param>
    /// <param name="permissionOverride">The <see cref="AbstractServerPermissionEvent{T}">permission</see> received from the event</param>
    /// <returns>New <see cref="AbstractServerPermissionEvent{T}" /> JSON instance</returns>
    /// <seealso cref="AbstractServerPermissionEvent{T}" />
    protected AbstractServerPermissionEvent(T permissionOverride, HashId serverId) =>
        (ServerId, PermissionOverride) = (serverId, permissionOverride);
    #endregion
}