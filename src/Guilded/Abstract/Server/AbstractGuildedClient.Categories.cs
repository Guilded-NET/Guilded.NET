using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Events;
using Guilded.Permissions;
using Guilded.Servers;
using Guilded.Users;
using RestSharp;

namespace Guilded.Client;

public abstract partial class AbstractGuildedClient
{
    #region Properties Events categories
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a new <see cref="Category">category</see> is created.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CategoryCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="CategoryUpdated" />
    /// <seealso cref="CategoryDeleted" />
    public IObservable<CategoryEvent> CategoryCreated => ((IEventInfo<CategoryEvent>)GuildedEvents["CategoryCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Category">category</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CategoryUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="CategoryCreated" />
    /// <seealso cref="CategoryDeleted" />
    public IObservable<CategoryEvent> CategoryUpdated => ((IEventInfo<CategoryEvent>)GuildedEvents["CategoryUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Category">category</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>CategoryDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="CategoryCreated" />
    /// <seealso cref="CategoryUpdated" />
    public IObservable<CategoryEvent> CategoryDeleted => ((IEventInfo<CategoryEvent>)GuildedEvents["CategoryDeleted"]).Observable;
    #endregion

    #region Properties Events role permissions
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="IServerRolePermission">role permission override</see> is added to a <see cref="ServerChannel">channel</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChannelCategoryRolePermissionCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="ChannelRolePermissionCreated" />
    /// <seealso cref="ChannelRolePermissionUpdated" />
    /// <seealso cref="ChannelRolePermissionDeleted" />
    /// <seealso cref="ChannelUserPermissionCreated" />
    /// <seealso cref="ChannelUserPermissionUpdated" />
    /// <seealso cref="ChannelUserPermissionDeleted" />
    /// <seealso cref="CategoryUserPermissionCreated" />
    /// <seealso cref="CategoryUserPermissionUpdated" />
    /// <seealso cref="CategoryUserPermissionDeleted" />
    /// <seealso cref="CategoryRolePermissionUpdated" />
    /// <seealso cref="CategoryRolePermissionDeleted" />
    public IObservable<CategoryRolePermissionEvent> CategoryRolePermissionCreated => ((IEventInfo<CategoryRolePermissionEvent>)GuildedEvents["ChannelCategoryRolePermissionCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="IServerRolePermission">role permission override</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChannelCategoryRolePermissionUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="ChannelRolePermissionCreated" />
    /// <seealso cref="ChannelRolePermissionUpdated" />
    /// <seealso cref="ChannelRolePermissionDeleted" />
    /// <seealso cref="ChannelUserPermissionCreated" />
    /// <seealso cref="ChannelUserPermissionUpdated" />
    /// <seealso cref="ChannelUserPermissionDeleted" />
    /// <seealso cref="CategoryUserPermissionCreated" />
    /// <seealso cref="CategoryUserPermissionUpdated" />
    /// <seealso cref="CategoryUserPermissionDeleted" />
    /// <seealso cref="CategoryRolePermissionCreated" />
    /// <seealso cref="CategoryRolePermissionDeleted" />
    public IObservable<CategoryRolePermissionEvent> CategoryRolePermissionUpdated => ((IEventInfo<CategoryRolePermissionEvent>)GuildedEvents["ChannelCategoryRolePermissionUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="IServerRolePermission">role permission override</see> is removed from a <see cref="ServerChannel">channel</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChannelCategoryRolePermissionDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="ChannelRolePermissionCreated" />
    /// <seealso cref="ChannelRolePermissionUpdated" />
    /// <seealso cref="ChannelRolePermissionDeleted" />
    /// <seealso cref="ChannelUserPermissionCreated" />
    /// <seealso cref="ChannelUserPermissionUpdated" />
    /// <seealso cref="ChannelUserPermissionDeleted" />
    /// <seealso cref="CategoryUserPermissionCreated" />
    /// <seealso cref="CategoryUserPermissionUpdated" />
    /// <seealso cref="CategoryUserPermissionDeleted" />
    /// <seealso cref="CategoryRolePermissionCreated" />
    /// <seealso cref="CategoryRolePermissionUpdated" />
    public IObservable<ChannelRolePermissionEvent> CategoryRolePermissionDeleted => ((IEventInfo<ChannelRolePermissionEvent>)GuildedEvents["ChannelCategoryRolePermissionDeleted"]).Observable;
    #endregion

    #region Properties Events user permissions
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="IServerUserPermission">user permission override</see> is added to a <see cref="ServerChannel">channel</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChannelCategoryUserPermissionCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="ChannelRolePermissionCreated" />
    /// <seealso cref="ChannelRolePermissionUpdated" />
    /// <seealso cref="ChannelRolePermissionDeleted" />
    /// <seealso cref="ChannelUserPermissionCreated" />
    /// <seealso cref="ChannelUserPermissionUpdated" />
    /// <seealso cref="ChannelUserPermissionDeleted" />
    /// <seealso cref="CategoryUserPermissionUpdated" />
    /// <seealso cref="CategoryUserPermissionDeleted" />
    /// <seealso cref="CategoryRolePermissionCreated" />
    /// <seealso cref="CategoryRolePermissionUpdated" />
    /// <seealso cref="CategoryRolePermissionDeleted" />
    public IObservable<ChannelUserPermissionEvent> CategoryUserPermissionCreated => ((IEventInfo<ChannelUserPermissionEvent>)GuildedEvents["ChannelUserPermissionCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="IServerUserPermission">user permission override</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChannelCategoryUserPermissionUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="ChannelRolePermissionCreated" />
    /// <seealso cref="ChannelRolePermissionUpdated" />
    /// <seealso cref="ChannelRolePermissionDeleted" />
    /// <seealso cref="ChannelUserPermissionCreated" />
    /// <seealso cref="ChannelUserPermissionUpdated" />
    /// <seealso cref="ChannelUserPermissionDeleted" />
    /// <seealso cref="CategoryUserPermissionCreated" />
    /// <seealso cref="CategoryUserPermissionDeleted" />
    /// <seealso cref="CategoryRolePermissionCreated" />
    /// <seealso cref="CategoryRolePermissionUpdated" />
    /// <seealso cref="CategoryRolePermissionDeleted" />
    public IObservable<ChannelUserPermissionEvent> CategoryUserPermissionUpdated => ((IEventInfo<ChannelUserPermissionEvent>)GuildedEvents["ChannelUserPermissionUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="IServerUserPermission">user permission override</see> is removed from a <see cref="ServerChannel">channel</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChannelCategoryUserPermissionDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="ChannelRolePermissionCreated" />
    /// <seealso cref="ChannelRolePermissionUpdated" />
    /// <seealso cref="ChannelRolePermissionDeleted" />
    /// <seealso cref="ChannelUserPermissionCreated" />
    /// <seealso cref="ChannelUserPermissionUpdated" />
    /// <seealso cref="ChannelUserPermissionDeleted" />
    /// <seealso cref="CategoryUserPermissionCreated" />
    /// <seealso cref="CategoryUserPermissionUpdated" />
    /// <seealso cref="CategoryRolePermissionCreated" />
    /// <seealso cref="CategoryRolePermissionUpdated" />
    /// <seealso cref="CategoryRolePermissionDeleted" />
    public IObservable<CategoryUserPermissionEvent> CategoryUserPermissionDeleted => ((IEventInfo<CategoryUserPermissionEvent>)GuildedEvents["ChannelCategoryUserPermissionDeleted"]).Observable;
    #endregion

    #region Methods Channels
    /// <summary>
    /// Gets the specified <paramref name="category" />.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Category">category</see> is</param>
    /// <param name="category">The identifier of the <see cref="Category">category</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The <see cref="Category">category</see> that was specified in the arguments</returns>
    public Task<Category> GetCategoryAsync(HashId server, uint category) =>
        GetResponsePropertyAsync<Category>(new RestRequest($"servers/{server}/categories/{category}", Method.Get), "category");

    /// <summary>
    /// Creates a new channel in the specified <paramref name="server" />.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Category">category</see> will be created</param>
    /// <param name="name">The name of the <see cref="Category">category</see> (max — <c>100</c>)</param>
    /// <param name="group">The identifier of the group where the <see cref="ServerChannel">channel</see> will be created</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <exception cref="ArgumentNullException">The specified <paramref name="name" /> is null, empty or whitespace</exception>
    /// <permission cref="Permission.ManageChannels" />
    /// <returns>The <see cref="Category">category</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Category> CreateCategoryAsync(HashId server, string name, HashId? group = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));

        EnforceLimit(nameof(name), name, ServerChannel.NameLimit);

        return GetResponsePropertyAsync<Category>(new RestRequest($"servers/{server}/categories", Method.Post)
            .AddJsonBody(new
            {
                serverId = server,
                groupId = group,
                name,
            })
        , "category");
    }

    /// <summary>
    /// Edits the specified <paramref name="category" />.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Category">category</see> is</param>
    /// <param name="category">The identifier of the <see cref="Category">category</see> to update</param>
    /// <param name="name">A new name of the <see cref="Category">category</see> (max — <c>100</c>)</param>
    /// <param name="priority">A new position of the <see cref="Category">category</see> (max — <c>100</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageChannels" />
    /// <returns>The <see cref="Category">category</see> that was updated by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Category> UpdateCategoryAsync(HashId server, uint category, string? name = null, int? priority = null)
    {
        EnforceLimitOnNullable(nameof(name), name, ServerChannel.NameLimit);

        return GetResponsePropertyAsync<Category>(new RestRequest($"servers/{server}/categories/{category}", Method.Patch)
            .AddJsonBody(new
            {
                name,
                priority,
            })
        , "category");
    }

    /// <summary>
    /// Deletes the specified <paramref name="category" />.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Category">category</see> is</param>
    /// <param name="category">The identifier of the <see cref="Category">category</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageChannels" />
    public Task DeleteCategoryAsync(HashId server, uint category) =>
        ExecuteRequestAsync(new RestRequest($"servers/{server}/categories/{category}", Method.Delete));
    #endregion

    #region Methods Category role permissions
    /// <summary>
    /// Gets all of the specified <paramref name="category">category's</paramref> <see cref="CategoryRolePermission">role permissions</see>.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Category">category</see> is</param>
    /// <param name="category">The identifier of the <see cref="Category">category</see> to get <see cref="CategoryRolePermission">role permissions</see> of</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The fetched <see cref="CategoryRolePermission">role permissions</see> in the <see cref="Category">category</see></returns>
    public Task<IList<CategoryRolePermission>> GetCategoryRolePermissionsAsync(HashId server, uint category) =>
        GetResponsePropertyAsync<IList<CategoryRolePermission>>(new RestRequest($"servers/{server}/categories/{category}/permissions/roles", Method.Get), "channelCategoryRolePermissions");

    /// <summary>
    /// Gets the <paramref name="category">category's</paramref> <see cref="CategoryRolePermission">permissions</see> of the specified of <see cref="Role">role</see>.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Category">category</see> is</param>
    /// <param name="category">The identifier of the <see cref="Category">category</see> to get <see cref="CategoryRolePermission">role permissions</see> of</param>
    /// <param name="role">The identifier of the <see cref="Role">role</see> to get <see cref="CategoryRolePermission">category permissions</see> of</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The fetched <see cref="CategoryRolePermission">permissions</see> of a <see cref="Role">role</see> in the <see cref="Category">category</see></returns>
    public Task<CategoryRolePermission> GetCategoryRolePermissionAsync(HashId server, uint category, uint role) =>
        GetResponsePropertyAsync<CategoryRolePermission>(new RestRequest($"servers/{server}/categories/{category}/permissions/roles/{role}", Method.Get), "channelCategoryRolePermission");

    /// <summary>
    /// Adds the <see cref="CategoryRolePermission">permissions</see> for the specified of <see cref="Role">role</see> in a <see cref="Category">category</see>.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Category">category</see> is</param>
    /// <param name="category">The identifier of the <see cref="Category">category</see> to add <see cref="CategoryRolePermission">role permissions</see> in</param>
    /// <param name="role">The identifier of the <see cref="Role">role</see> to add <see cref="CategoryRolePermission">category permissions</see> to</param>
    /// <param name="permissions">The dictionary of <see cref="CategoryRolePermission">role category permissions</see> to enable or disable (null — inherit, true — enabled, false — disabled)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The fetched <see cref="CategoryRolePermission">permissions</see> of a <see cref="Role">role</see> in the <see cref="Category">category</see></returns>
    public Task<CategoryRolePermission> AddCategoryRolePermissionAsync(HashId server, uint category, uint role, IDictionary<Permission, bool?> permissions) =>
        GetResponsePropertyAsync<CategoryRolePermission>(new RestRequest($"servers/{server}/categories/{category}/permissions/roles/{role}", Method.Post)
            .AddJsonBody(new
            {
                permissions
            })
        , "channelCategoryRolePermission");

    /// <summary>
    /// Updates the <see cref="CategoryRolePermission">permissions</see> of the specified <see cref="Role">role</see> in a <see cref="Category">category</see>.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Category">category</see> is</param>
    /// <param name="category">The identifier of the <see cref="Category">category</see> where the <see cref="CategoryRolePermission">role permissions</see> are</param>
    /// <param name="role">The identifier of the <see cref="Role">role</see> to update <see cref="CategoryRolePermission">category permissions</see> of</param>
    /// <param name="permissions">The dictionary of <see cref="CategoryRolePermission">role category permissions</see> to enable or disable (null — inherit, true — enabled, false — disabled)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The fetched <see cref="CategoryRolePermission">permissions</see> of a <see cref="Role">role</see> in the <see cref="Category">category</see></returns>
    public Task<CategoryRolePermission> UpdateCategoryRolePermissionAsync(HashId server, uint category, uint role, IDictionary<Permission, bool?> permissions) =>
        GetResponsePropertyAsync<CategoryRolePermission>(new RestRequest($"servers/{server}/categories/{category}/permissions/roles/{role}", Method.Patch)
            .AddJsonBody(new
            {
                permissions
            })
        , "channelCategoryRolePermission");

    /// <summary>
    /// Removes <see cref="CategoryRolePermission">permissions</see> of the specified <see cref="Role">role</see> in a <see cref="Category">category</see>.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Category">category</see> is</param>
    /// <param name="category">The identifier of the <see cref="Category">category</see> where the <see cref="CategoryRolePermission">role permissions</see> are</param>
    /// <param name="role">The identifier of the <see cref="Role">role</see> to remove <see cref="CategoryRolePermission">category permissions</see> from</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    public Task RemoveCategoryRolePermissionAsync(HashId server, uint category, uint role) =>
        ExecuteRequestAsync(new RestRequest($"servers/{server}/categories/{category}/permissions/roles/{role}", Method.Delete));
    #endregion

    #region Methods Category user permissions
    /// <summary>
    /// Gets all of the specified <paramref name="category">category's</paramref> <see cref="CategoryUserPermission">user permissions</see>.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Category">category</see> is</param>
    /// <param name="category">The identifier of the <see cref="Category">category</see> to get <see cref="CategoryUserPermission">user permissions</see> of</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The fetched <see cref="CategoryUserPermission">user permissions</see> in the <see cref="Category">category</see></returns>
    public Task<IList<CategoryUserPermission>> GetCategoryUserPermissionsAsync(HashId server, uint category) =>
        GetResponsePropertyAsync<IList<CategoryUserPermission>>(new RestRequest($"servers/{server}/categories/{category}/permissions/users", Method.Get), "channelCategoryUserPermissions");

    /// <summary>
    /// Gets the <paramref name="category">category's</paramref> <see cref="CategoryUserPermission">permissions</see> of the specified of <see cref="User">user</see>.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Category">category</see> is</param>
    /// <param name="category">The identifier of the <see cref="Category">category</see> to get <see cref="CategoryUserPermission">user permissions</see> of</param>
    /// <param name="user">The identifier of the <see cref="User">user</see> to get <see cref="CategoryUserPermission">category permissions</see> of</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The fetched <see cref="CategoryUserPermission">permissions</see> of a <see cref="User">user</see> in the <see cref="Category">category</see></returns>
    public Task<CategoryUserPermission> GetCategoryUserPermissionAsync(HashId server, uint category, HashId user) =>
        GetResponsePropertyAsync<CategoryUserPermission>(new RestRequest($"servers/{server}/categories/{category}/permissions/users/{user}", Method.Get), "channelCategoryUserPermission");

    /// <summary>
    /// Adds the <see cref="CategoryUserPermission">permissions</see> for the specified of <see cref="User">user</see> in a <see cref="Category">category</see>.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Category">category</see> is</param>
    /// <param name="category">The identifier of the <see cref="Category">category</see> to add <see cref="CategoryUserPermission">user permissions</see> in</param>
    /// <param name="user">The identifier of the <see cref="User">user</see> to add <see cref="CategoryUserPermission">category permissions</see> to</param>
    /// <param name="permissions">The dictionary of <see cref="CategoryUserPermission">user category permissions</see> to enable or disable (null — inherit, true — enabled, false — disabled)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The fetched <see cref="CategoryUserPermission">permissions</see> of a <see cref="User">user</see> in the <see cref="Category">category</see></returns>
    public Task<CategoryUserPermission> AddCategoryUserPermissionAsync(HashId server, uint category, HashId user, IDictionary<Permission, bool?> permissions) =>
        GetResponsePropertyAsync<CategoryUserPermission>(new RestRequest($"servers/{server}/categories/{category}/permissions/users/{user}", Method.Post)
            .AddJsonBody(new
            {
                permissions
            })
        , "channelCategoryUserPermission");

    /// <summary>
    /// Updates the <see cref="CategoryUserPermission">permissions</see> of the specified <see cref="User">user</see> in a <see cref="Category">category</see>.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Category">category</see> is</param>
    /// <param name="category">The identifier of the <see cref="Category">category</see> where the <see cref="CategoryUserPermission">user permissions</see> are</param>
    /// <param name="user">The identifier of the <see cref="User">user</see> to update <see cref="CategoryUserPermission">category permissions</see> of</param>
    /// <param name="permissions">The dictionary of <see cref="CategoryUserPermission">user category permissions</see> to enable or disable (null — inherit, true — enabled, false — disabled)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The fetched <see cref="CategoryUserPermission">permissions</see> of a <see cref="User">user</see> in the <see cref="Category">category</see></returns>
    public Task<CategoryUserPermission> UpdateCategoryUserPermissionAsync(HashId server, uint category, HashId user, IDictionary<Permission, bool?> permissions) =>
        GetResponsePropertyAsync<CategoryUserPermission>(new RestRequest($"servers/{server}/categories/{category}/permissions/users/{user}", Method.Patch)
            .AddJsonBody(new
            {
                permissions
            })
        , "channelCategoryUserPermission");

    /// <summary>
    /// Removes <see cref="CategoryUserPermission">permissions</see> of the specified <see cref="User">user</see> in a <see cref="Category">category</see>.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Category">category</see> is</param>
    /// <param name="category">The identifier of the <see cref="Category">category</see> where the <see cref="CategoryUserPermission">user permissions</see> are</param>
    /// <param name="user">The identifier of the <see cref="User">user</see> to remove <see cref="CategoryUserPermission">category permissions</see> from</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    public Task RemoveCategoryUserPermissionAsync(HashId server, uint category, HashId user) =>
        ExecuteRequestAsync(new RestRequest($"servers/{server}/categories/{category}/permissions/users/{user}", Method.Delete));
    #endregion
}