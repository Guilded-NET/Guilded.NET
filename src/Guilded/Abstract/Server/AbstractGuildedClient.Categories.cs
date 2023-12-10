using System;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Events;
using Guilded.Permissions;
using Guilded.Servers;
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
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageChannels" />
    /// <returns>The <see cref="Category">category</see> that was updated by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Category> UpdateCategoryAsync(HashId server, uint category, string name)
    {
        EnforceLimit(nameof(name), name, ServerChannel.NameLimit);

        return GetResponsePropertyAsync<Category>(new RestRequest($"servers/{server}/categories/{category}", Method.Patch)
            .AddJsonBody(new
            {
                name,
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
}