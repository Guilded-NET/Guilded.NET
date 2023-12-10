using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Client;
using Guilded.Permissions;
using Guilded.Servers;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when someone creates, updates or deletes a <see cref="Servers.Role">role</see>.
/// </summary>
/// <seealso cref="ServerEvent" />
/// <seealso cref="GroupEvent" />
/// <seealso cref="ChannelEvent" />
public class RoleEvent : IModelHasId<uint>, ICreationDated, IUpdatableContent, IServerBased
{
    #region Properties
    /// <summary>
    /// Gets the <see cref="Servers.Role">role</see> received from the event.
    /// </summary>
    /// <value>The <see cref="Servers.Role">role</see> received from the event</value>
    /// <seealso cref="RoleEvent" />
    /// <seealso cref="Name" />
    /// <seealso cref="Icon" />
    /// <seealso cref="ServerId" />
    public Role Role { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="Servers.Server">server</see> where the event occurred.
    /// </summary>
    /// <value>The identifier of the <see cref="Servers.Server">server</see> where the event occurred</value>
    /// <seealso cref="RoleEvent" />
    /// <seealso cref="Role" />
    /// <seealso cref="Id" />
    public HashId ServerId { get; }
    #endregion

    #region Properties Additional
    /// <inheritdoc cref="Group.Id" />
    public uint Id => Role.Id;

    /// <inheritdoc cref="Role.BotUserId" />
    public HashId? BotUserId => Role.BotUserId;

    /// <inheritdoc cref="Role.Name" />
    public string Name => Role.Name;

    /// <inheritdoc cref="Role.Icon" />
    public Uri? Icon => Role.Icon;

    /// <inheritdoc cref="Role.Position" />
    [Obsolete($"Use {nameof(Priority)} instead")]
    public int Position => Role.Position;

    /// <inheritdoc cref="Role.Priority" />
    public int Priority => Role.Priority;

    /// <inheritdoc cref="Role.Colors" />
    public IList<Color>? Colors => Role.Colors;

    /// <inheritdoc cref="Role.CreatedAt" />
    public DateTime CreatedAt => Role.CreatedAt;

    /// <inheritdoc cref="Role.UpdatedAt" />
    public DateTime? UpdatedAt => Role.UpdatedAt;

    /// <inheritdoc cref="Role.IsBase" />
    public bool IsBase => Role.IsBase;

    /// <inheritdoc cref="Role.IsDisplayedSeparately" />
    public bool IsDisplayedSeparately => Role.IsDisplayedSeparately;

    /// <inheritdoc cref="Role.IsMentionable" />
    public bool IsMentionable => Role.IsMentionable;

    /// <inheritdoc cref="Role.IsSelfAssignable" />
    public bool IsSelfAssignable => Role.IsSelfAssignable;

    /// <inheritdoc cref="Role.Permissions" />
    public IList<Permission> Permissions => Role.Permissions;

    /// <inheritdoc cref="IHasParentClient.ParentClient" />
    public AbstractGuildedClient ParentClient => Role.ParentClient;
    #endregion

    #region Properties Events
    /// <inheritdoc cref="Role.Updated" />
    public IObservable<RoleEvent> Updated =>
        Role.Updated;

    /// <inheritdoc cref="Role.Deleted" />
    public IObservable<RoleEvent> Deleted =>
        Role.Deleted;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="RoleEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="RoleEvent">role event</see> occurred</param>
    /// <param name="role">The <see cref="Servers.Role">role</see> received from the event</param>
    /// <returns>New <see cref="RoleEvent" /> JSON instance</returns>
    /// <seealso cref="RoleEvent" />
    [JsonConstructor]
    public RoleEvent(
        [JsonProperty(Required = Required.Always)]
        Role role,

        [JsonProperty(Required = Required.Always)]
        HashId serverId
    ) =>
        (ServerId, Role) = (serverId, role);
    #endregion


    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.UpdateRoleAsync(HashId, uint, string?, bool?, bool?, bool?, IList{uint}?, IList{Permission}?, int?)" />
    /// <param name="name">The new name of the <see cref="Role">role</see></param>
    /// <param name="isDisplayedSeparately">Whether the <see cref="Role">role</see> displays its <see cref="Member">members</see> separately from others</param>
    /// <param name="isSelfAssignable">Whether <see cref="Member">members</see> are allowed to assign themselves the <see cref="Role">role</see></param>
    /// <param name="isMentionable">Whether the <see cref="Role">role</see> can be mentioned and its <see cref="Member">members</see> get pinged</param>
    /// <param name="colors">The new displayed colours of the <see cref="Role">role</see></param>
    /// <param name="permissions">The new <see cref="Permission">permissions</see> of the <see cref="Role">role</see></param>
    /// <param name="priority">The position of the <see cref="Role">role</see> in the <see cref="Server">server's</see> role list</param>
    public Task<Role> UpdateAsync(string? name = null, bool? isDisplayedSeparately = null, bool? isSelfAssignable = null, bool? isMentionable = null, IList<uint>? colors = null, IList<Permission>? permissions = null, int? priority = null) =>
        Role.UpdateAsync(name, isDisplayedSeparately, isSelfAssignable, isMentionable, colors, permissions, priority);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateRoleAsync(HashId, uint, string?, bool?, bool?, bool?, IList{uint}?, IList{Permission}?, int?)" />
    /// <param name="name">The new name of the <see cref="Role">role</see></param>
    /// <param name="isDisplayedSeparately">Whether the <see cref="Role">role</see> displays its <see cref="Member">members</see> separately from others</param>
    /// <param name="isSelfAssignable">Whether <see cref="Member">members</see> are allowed to assign themselves the <see cref="Role">role</see></param>
    /// <param name="isMentionable">Whether the <see cref="Role">role</see> can be mentioned and its <see cref="Member">members</see> get pinged</param>
    /// <param name="colors">The new displayed colours of the <see cref="Role">role</see></param>
    /// <param name="permissions">The new <see cref="Permission">permissions</see> of the <see cref="Role">role</see></param>
    /// <param name="priority">The position of the <see cref="Role">role</see> in the <see cref="Server">server's</see> role list</param>
    public Task<Role> UpdateAsync(string? name = null, bool? isDisplayedSeparately = null, bool? isSelfAssignable = null, bool? isMentionable = null, IList<Color>? colors = null, IList<Permission>? permissions = null, int? priority = null) =>
        Role.UpdateAsync(name, isDisplayedSeparately, isSelfAssignable, isMentionable, colors, permissions, priority);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteRoleAsync(HashId, uint)" />
    public Task DeleteAsync() =>
        Role.DeleteAsync();

    /// <inheritdoc cref="AbstractGuildedClient.UpdateRolePermissionsAsync(HashId, uint, IDictionary{Permission, bool})" />
    /// <param name="permissions">The new <see cref="Permission">permissions</see> of the <see cref="Role">role</see></param>
    public Task<Role> UpdatePermissionsAsync(IDictionary<Permission, bool> permissions) =>
        Role.UpdatePermissionsAsync(permissions);
    #endregion
}