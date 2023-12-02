using System;
using System.Collections.Generic;
using System.Linq;
using Guilded.Servers;

namespace Guilded.Permissions;

/// <summary>
/// Represents a <see cref="Permission">permission</see> list of a <see cref="Member">member</see> or <see cref="Role">role</see>.   
/// </summary>
/// <seealso cref="IChannelPermission"/>
/// <seealso cref="ICategoryPermission"/>
/// <seealso cref="IServerUserPermission"/>
/// <seealso cref="IServerRolePermission"/>
public abstract class AbstractServerPermission : ContentModel, IServerPermission, ICreationDated, IUpdatableContent
{
    #region Properties
    /// <summary>
    /// Gets the date when the <see cref="AbstractServerPermission">permission change</see> was created.
    /// </summary>
    /// <value>The date when the <see cref="AbstractServerPermission">permission change</see> was created</value>
    /// <seealso cref="AbstractServerPermission" />
    /// <seealso cref="UpdatedAt" />
    public DateTime CreatedAt { get; }

    /// <summary>
    /// Gets the date when the <see cref="AbstractServerPermission">permission change</see> was last edited.
    /// </summary>
    /// <value>The date when the <see cref="AbstractServerPermission">permission change</see> was last edited</value>
    /// <seealso cref="AbstractServerPermission" />
    /// <seealso cref="CreatedAt" />
    public DateTime? UpdatedAt { get; }

    /// <summary>
    /// Gets the list of <see cref="Permission">permissions</see> that are enabled or disabled for specific <see cref="Member">members</see>.
    /// </summary>
    /// <value>The list of <see cref="Permission">permissions</see> that are enabled or disabled for specific <see cref="Member">members</see></value>
    /// <seealso cref="AbstractServerPermission" />
    /// <seealso cref="EnabledPermissions" />
    /// <seealso cref="DisabledPermissions" />
    public IDictionary<Permission, bool> Permissions { get; }

    /// <summary>
    /// Gets the list of <see cref="Permission">permissions</see> that are enabled for specific <see cref="Member">members</see>.
    /// </summary>
    /// <value>The list of <see cref="Permission">permissions</see> that are enabled for specific <see cref="Member">members</see></value>
    /// <seealso cref="AbstractServerPermission" />
    /// <seealso cref="Permissions" />
    /// <seealso cref="DisabledPermissions" />
    public IEnumerable<Permission> EnabledPermissions => Permissions.Where(pair => pair.Value).Select(pair => pair.Key);

    /// <summary>
    /// Gets the list of <see cref="Permission">permissions</see> that are disabled for specific <see cref="Member">members</see>.
    /// </summary>
    /// <value>The list of <see cref="Permission">permissions</see> that are disabled for specific <see cref="Member">members</see></value>
    /// <seealso cref="AbstractServerPermission" />
    /// <seealso cref="Permissions" />
    /// <seealso cref="EnabledPermissions" />
    public IEnumerable<Permission> DisabledPermissions => Permissions.Where(pair => !pair.Value).Select(pair => pair.Key);
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new <see cref="AbstractServerPermission" /> instance.
    /// </summary>
    /// <param name="permissions">The list of <see cref="Permission">permissions</see> that are enabled or disabled for specific <see cref="Member">members</see></param>
    /// <param name="createdAt">The date when the <see cref="AbstractServerPermission">permission change</see> was created</param>
    /// <param name="updatedAt">The date when the <see cref="AbstractServerPermission">permission change</see> was last edited</param>
    /// <returns>Initialized <see cref="AbstractServerPermission" /></returns>
    /// <seealso cref="AbstractServerPermission" />
    protected AbstractServerPermission(IDictionary<Permission, bool> permissions, DateTime createdAt, DateTime? updatedAt) =>
        (Permissions, CreatedAt, UpdatedAt) = (permissions, createdAt, updatedAt);
    #endregion
}