using System.Collections.Generic;
using Guilded.Base;
using Guilded.Servers;

namespace Guilded.Permissions;

/// <summary>
/// Represents a <see cref="Permission">permission</see> list in a <see cref="ServerChannel">channel</see> or <see cref="Category">category</see>.
/// </summary>
/// <seealso cref="IChannelPermission" />
/// <seealso cref="ICategoryPermission" />
/// <seealso cref="IServerUserPermission" />
/// <seealso cref="IServerRolePermission" />
public interface IServerPermission : IHasParentClient, ICreationDated, IUpdatableContent
{
    /// <summary>
    /// Gets the list of <see cref="Permission">permissions</see> that are either denied or granted in a <see cref="ServerChannel">channel</see> or <see cref="Category">category</see>.
    /// </summary>
    /// <value>The list of <see cref="Permission">permissions</see> that are either denied or granted in a <see cref="ServerChannel">channel</see> or <see cref="Category">category</see></value>
    /// <seealso cref="IServerPermission"/>
    public IDictionary<Permission, bool> Permissions { get; }
}

/// <summary>
/// Represents a <see cref="Permission">permission</see> list in a <see cref="ServerChannel">channel</see> or <see cref="Category">category</see>.
/// </summary>
/// <seealso cref="IChannelPermission" />
/// <seealso cref="ICategoryPermission" />
/// <seealso cref="IServerUserPermission" />
/// <seealso cref="IServerRolePermission" />
public interface IServerUserPermission : IServerPermission
{
    /// <summary>
    /// Gets the identifier of the <see cref="Member">member</see> that had their <see cref="Permission">permissions</see> overriden.  
    /// </summary>
    /// <seealso cref="IServerUserPermission" />
    public HashId UserId { get; }
}

/// <summary>
/// Represents a <see cref="Permission">permission</see> list in a <see cref="ServerChannel">channel</see> or <see cref="Category">category</see>.
/// </summary>
/// <seealso cref="IChannelPermission" />
/// <seealso cref="ICategoryPermission" />
/// <seealso cref="IServerUserPermission" />
/// <seealso cref="IServerRolePermission" />
public interface IServerRolePermission : IServerPermission
{
    /// <summary>
    /// Gets the identifier of the <see cref="Role">role</see> that had its <see cref="Permission">permissions</see> overriden.  
    /// </summary>
    /// <seealso cref="IServerUserPermission" />
    public uint RoleId { get; }
}

/// <summary>
/// Represents a <see cref="Permission">permission</see> list in a <see cref="ServerChannel">channel</see>.
/// </summary>
/// <seealso cref="IChannelPermission" />
/// <seealso cref="ICategoryPermission" />
/// <seealso cref="IServerUserPermission" />
/// <seealso cref="IServerRolePermission" />
public interface IChannelPermission : IServerPermission, IChannelBased { }

/// <summary>
/// Represents a <see cref="Permission">permission</see> list in a <see cref="Category">category</see>.
/// </summary>
/// <seealso cref="IChannelPermission" />
/// <seealso cref="IServerUserPermission" />
/// <seealso cref="IServerRolePermission" />
/// <seealso cref="IServerPermission" />
public interface ICategoryPermission : IServerPermission
{
    /// <summary>
    /// Gets the identifier of the <see cref="Category">category</see> where the <see cref="Permission">permissions</see> have been overriden.  
    /// </summary>
    /// <seealso cref="IServerUserPermission" />
    public uint CategoryId { get; }
}