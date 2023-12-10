using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Events;
using Guilded.Permissions;
using Guilded.Servers;
using RestSharp;

namespace Guilded.Client;

public abstract partial class AbstractGuildedClient
{
    #region Properties Events
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a new <see cref="Role">role</see> is created.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>RoleCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="RoleUpdated" />
    /// <seealso cref="RoleDeleted" />
    public IObservable<RoleEvent> RoleCreated => ((IEventInfo<RoleEvent>)GuildedEvents["RoleCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Role">role</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>RoleUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="RoleCreated" />
    /// <seealso cref="RoleDeleted" />
    public IObservable<RoleEvent> RoleUpdated => ((IEventInfo<RoleEvent>)GuildedEvents["RoleUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Role">role</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>RoleDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="RoleCreated" />
    /// <seealso cref="RoleUpdated" />
    public IObservable<RoleEvent> RoleDeleted => ((IEventInfo<RoleEvent>)GuildedEvents["RoleDeleted"]).Observable;
    #endregion

    #region Methods REST
    /// <summary>
    /// Gets a list of <see cref="Role">roles</see>.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> to get <see cref="Role">roles</see> from</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The list of fetched <see cref="Role">role</see> in the specified <paramref name="server" /></returns>
    public Task<IList<Role>> GetRolesAsync(HashId server) =>
        GetResponsePropertyAsync<IList<Role>>(new RestRequest($"servers/{server}/roles", Method.Get), "roles");

    /// <summary>
    /// Gets the specified <paramref name="role" />.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Role">role</see> is</param>
    /// <param name="role">The identifier of the <see cref="Role">role</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The <see cref="Role">role</see> that was specified in the arguments</returns>
    public Task<Role> GetRoleAsync(HashId server, uint role) =>
        GetResponsePropertyAsync<Role>(new RestRequest($"servers/{server}/roles/{role}", Method.Get), "role");

    /// <summary>
    /// Creates a new <see cref="Role">role</see> in the specified <paramref name="server" />.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Role">role</see> will be created</param>
    /// <param name="name">The name of the <see cref="Role">role</see></param>
    /// <param name="isDisplayedSeparately">Whether the <see cref="Role">role</see> displays its <see cref="Member">members</see> separately from others</param>
    /// <param name="isSelfAssignable">Whether <see cref="Member">members</see> are allowed to assign themselves the <see cref="Role">role</see></param>
    /// <param name="isMentionable">Whether the <see cref="Role">role</see> can be mentioned and its <see cref="Member">members</see> get pinged</param>
    /// <param name="colors">The displayed colours of the <see cref="Role">role</see></param>
    /// <param name="permissions">The <see cref="Permission">permissions</see> of the <see cref="Role">role</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <exception cref="ArgumentNullException">The specified <paramref name="name" /> is null, empty or whitespace</exception>
    /// <permission cref="Permission.ManageRoles" />
    /// <returns>The <see cref="Role">role</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Role> CreateRoleAsync(HashId server, string name, bool isDisplayedSeparately = false, bool isSelfAssignable = false, bool isMentionable = false, IList<uint>? colors = null, IList<Permission>? permissions = null) =>
        GetResponsePropertyAsync<Role>(new RestRequest($"servers/{server}/roles", Method.Post)
            .AddBody(new
            {
                name,
                isDisplayedSeparately,
                isSelfAssignable,
                isMentionable,
                permissions,
                colors,
            })
        , "role");

    /// <inheritdoc cref="CreateRoleAsync(HashId, string, bool, bool, bool, IList{uint}?, IList{Permission}?)" />
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Role">role</see> will be created</param>
    /// <param name="name">The name of the <see cref="Role">role</see></param>
    /// <param name="isDisplayedSeparately">Whether the <see cref="Role">role</see> displays its <see cref="Member">members</see> separately from others</param>
    /// <param name="isSelfAssignable">Whether <see cref="Member">members</see> are allowed to assign themselves the <see cref="Role">role</see></param>
    /// <param name="isMentionable">Whether the <see cref="Role">role</see> can be mentioned and its <see cref="Member">members</see> get pinged</param>
    /// <param name="colors">The displayed colours of the <see cref="Role">role</see></param>
    /// <param name="permissions">The <see cref="Permission">permissions</see> of the <see cref="Role">role</see></param>
    public Task<Role> CreateRoleAsync(HashId server, string name, bool isDisplayedSeparately = false, bool isSelfAssignable = false, bool isMentionable = false, IList<Color>? colors = null, IList<Permission>? permissions = null) =>
        CreateRoleAsync(server, name, isDisplayedSeparately, isSelfAssignable, isMentionable, colors?.Select(color => (uint)color.ToArgb()).ToList(), permissions);

    /// <summary>
    /// Updates the specified <paramref name="role" />.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Group">group</see> will be updated</param>
    /// <param name="role">The identifier of the <see cref="Role">role</see> to update</param>
    /// <param name="name">The new name of the <see cref="Role">role</see></param>
    /// <param name="isDisplayedSeparately">Whether the <see cref="Role">role</see> displays its <see cref="Member">members</see> separately from others</param>
    /// <param name="isSelfAssignable">Whether <see cref="Member">members</see> are allowed to assign themselves the <see cref="Role">role</see></param>
    /// <param name="isMentionable">Whether the <see cref="Role">role</see> can be mentioned and its <see cref="Member">members</see> get pinged</param>
    /// <param name="colors">The new displayed colours of the <see cref="Role">role</see></param>
    /// <param name="permissions">The new <see cref="Permission">permissions</see> of the <see cref="Role">role</see></param>
    /// <param name="priority">The position of the <see cref="Role">role</see> in the <see cref="Server">server's</see> role list</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageRoles" />
    /// <returns>The <see cref="Role">role</see> that was updated by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Role> UpdateRoleAsync(HashId server, uint role, string? name = null, bool? isDisplayedSeparately = null, bool? isSelfAssignable = null, bool? isMentionable = null, IList<uint>? colors = null, IList<Permission>? permissions = null, int? priority = null)
    {
        return GetResponsePropertyAsync<Role>(new RestRequest($"servers/{server}/roles/{role}", Method.Patch)
            .AddJsonBody(new
            {
                name,
                isDisplayedSeparately,
                isSelfAssignable,
                isMentionable,
                colors,
                permissions,
                priority,
            })
        , "role");
    }

    /// <inheritdoc cref="UpdateRoleAsync(HashId, uint, string?, bool?, bool?, bool?, IList{uint}?, IList{Permission}?, int?)" />
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Group">group</see> will be updated</param>
    /// <param name="role">The identifier of the <see cref="Role">role</see> to update</param>
    /// <param name="name">The new name of the <see cref="Role">role</see></param>
    /// <param name="isDisplayedSeparately">Whether the <see cref="Role">role</see> displays its <see cref="Member">members</see> separately from others</param>
    /// <param name="isSelfAssignable">Whether <see cref="Member">members</see> are allowed to assign themselves the <see cref="Role">role</see></param>
    /// <param name="isMentionable">Whether the <see cref="Role">role</see> can be mentioned and its <see cref="Member">members</see> get pinged</param>
    /// <param name="colors">The new displayed colours of the <see cref="Role">role</see></param>
    /// <param name="permissions">The new <see cref="Permission">permissions</see> of the <see cref="Role">role</see></param>
    /// <param name="priority">The position of the <see cref="Role">role</see> in the <see cref="Server">server's</see> role list</param>
    public Task<Role> UpdateRoleAsync(HashId server, uint role, string? name = null, bool? isDisplayedSeparately = null, bool? isSelfAssignable = null, bool? isMentionable = null, IList<Color>? colors = null, IList<Permission>? permissions = null, int? priority = null) =>
        UpdateRoleAsync(server, role, name, isDisplayedSeparately, isSelfAssignable, isMentionable, colors?.Select(color => (uint)color.ToArgb()).ToList(), permissions);

    /// <summary>
    /// Deletes the specified <paramref name="role" />.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Role">role</see> will be deleted</param>
    /// <param name="role">The identifier of the <see cref="Role">role</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageRoles" />
    public Task DeleteRoleAsync(HashId server, uint role) =>
        ExecuteRequestAsync(new RestRequest($"servers/{server}/roles/{role}", Method.Delete));

    /// <summary>
    /// Updates the specified <paramref name="role">role's</paramref> <see cref="Permission">permissions</see>.
    /// </summary>
    /// <remarks>
    /// <para>Any permissions that exists in the role, but are not specified will be either left in the role or not added at all if it didn't exist prior.</para>
    /// <para>To remove a permission, specify it with <see langword="false" />. Otherwise, to add a permission, specify it as <see langword="true" />.</para>
    /// </remarks>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Role">role</see> will be updated</param>
    /// <param name="role">The identifier of the <see cref="Role">role</see> to update <see cref="Permission">permissions</see> of </param>
    /// <param name="permissions">The new <see cref="Permission">permissions</see> of the <see cref="Role">role</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageRoles" />
    /// <returns>The <see cref="Role">role</see> that was updated by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Role> UpdateRolePermissionsAsync(HashId server, uint role, IDictionary<Permission, bool> permissions)
    {
        return GetResponsePropertyAsync<Role>(new RestRequest($"servers/{server}/roles/{role}/permissions", Method.Patch)
            .AddJsonBody(new
            {
                permissions,
            })
        , "role");
    }
    #endregion
}