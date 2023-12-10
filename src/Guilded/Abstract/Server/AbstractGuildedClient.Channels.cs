using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Content;
using Guilded.Events;
using Guilded.Permissions;
using Guilded.Servers;
using RestSharp;

namespace Guilded.Client;

public abstract partial class AbstractGuildedClient
{
    #region Properties Channels
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when <see cref="Webhook">a new webhook</see> is created.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerWebhookCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ChannelCreated" />
    /// <seealso cref="ChannelUpdated" />
    /// <seealso cref="ChannelDeleted" />
    public IObservable<WebhookEvent> WebhookCreated => ((IEventInfo<WebhookEvent>)GuildedEvents["ServerWebhookCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Webhook">webhook</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerWebhookUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="ChannelCreated" />
    /// <seealso cref="ChannelUpdated" />
    /// <seealso cref="ChannelDeleted" />
    public IObservable<WebhookEvent> WebhookUpdated => ((IEventInfo<WebhookEvent>)GuildedEvents["ServerWebhookUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a new <see cref="ServerChannel">channel</see> is created.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerChannelCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ChannelUpdated" />
    /// <seealso cref="ChannelDeleted" />
    /// <seealso cref="ChannelArchived" />
    /// <seealso cref="ChannelRestored" />
    public IObservable<ChannelEvent> ChannelCreated => ((IEventInfo<ChannelEvent>)GuildedEvents["ServerChannelCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="ServerChannel">channel</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerChannelUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ChannelCreated" />
    /// <seealso cref="ChannelDeleted" />
    /// <seealso cref="ChannelArchived" />
    /// <seealso cref="ChannelRestored" />
    public IObservable<ChannelEvent> ChannelUpdated => ((IEventInfo<ChannelEvent>)GuildedEvents["ServerChannelUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="ServerChannel">channel</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerChannelDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ChannelCreated" />
    /// <seealso cref="ChannelUpdated" />
    /// <seealso cref="ChannelArchived" />
    /// <seealso cref="ChannelRestored" />
    public IObservable<ChannelEvent> ChannelDeleted => ((IEventInfo<ChannelEvent>)GuildedEvents["ServerChannelDeleted"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="ServerChannel">channel</see> is archived.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChannelArchived</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ChannelCreated" />
    /// <seealso cref="ChannelUpdated" />
    /// <seealso cref="ChannelDeleted" />
    /// <seealso cref="ChannelRestored" />
    public IObservable<ChannelEvent> ChannelArchived => ((IEventInfo<ChannelEvent>)GuildedEvents["ChannelArchived"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="ServerChannel">channel</see> is restored from being archived.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChannelRestored</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ChannelCreated" />
    /// <seealso cref="ChannelUpdated" />
    /// <seealso cref="ChannelDeleted" />
    /// <seealso cref="ChannelArchived" />
    public IObservable<ChannelEvent> ChannelRestored => ((IEventInfo<ChannelEvent>)GuildedEvents["ChannelRestored"]).Observable;
    #endregion

    #region Properties Role channel permissions
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="IServerRolePermission">role permission override</see> is added to a <see cref="ServerChannel">channel</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChannelRolePermissionCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
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
    /// <seealso cref="CategoryRolePermissionDeleted" />
    public IObservable<ChannelRolePermissionEvent> ChannelRolePermissionCreated => ((IEventInfo<ChannelRolePermissionEvent>)GuildedEvents["ChannelRolePermissionCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="IServerRolePermission">role permission override</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChannelRolePermissionUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="ChannelRolePermissionCreated" />
    /// <seealso cref="ChannelRolePermissionDeleted" />
    /// <seealso cref="ChannelUserPermissionCreated" />
    /// <seealso cref="ChannelUserPermissionUpdated" />
    /// <seealso cref="ChannelUserPermissionDeleted" />
    /// <seealso cref="CategoryUserPermissionCreated" />
    /// <seealso cref="CategoryUserPermissionUpdated" />
    /// <seealso cref="CategoryUserPermissionDeleted" />
    /// <seealso cref="CategoryRolePermissionCreated" />
    /// <seealso cref="CategoryRolePermissionUpdated" />
    /// <seealso cref="CategoryRolePermissionDeleted" />
    public IObservable<ChannelRolePermissionEvent> ChannelRolePermissionUpdated => ((IEventInfo<ChannelRolePermissionEvent>)GuildedEvents["ChannelRolePermissionUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="IServerRolePermission">role permission override</see> is removed from a <see cref="ServerChannel">channel</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChannelRolePermissionDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="ChannelRolePermissionCreated" />
    /// <seealso cref="ChannelRolePermissionUpdated" />
    /// <seealso cref="ChannelUserPermissionCreated" />
    /// <seealso cref="ChannelUserPermissionUpdated" />
    /// <seealso cref="ChannelUserPermissionDeleted" />
    /// <seealso cref="CategoryUserPermissionCreated" />
    /// <seealso cref="CategoryUserPermissionUpdated" />
    /// <seealso cref="CategoryUserPermissionDeleted" />
    /// <seealso cref="CategoryRolePermissionCreated" />
    /// <seealso cref="CategoryRolePermissionUpdated" />
    /// <seealso cref="CategoryRolePermissionDeleted" />
    public IObservable<ChannelRolePermissionEvent> ChannelRolePermissionDeleted => ((IEventInfo<ChannelRolePermissionEvent>)GuildedEvents["ChannelRolePermissionDeleted"]).Observable;
    #endregion

    #region Properties User channel permissions
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="IServerUserPermission">user permission override</see> is added to a <see cref="ServerChannel">channel</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChannelUserPermissionCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="ChannelRolePermissionCreated" />
    /// <seealso cref="ChannelRolePermissionUpdated" />
    /// <seealso cref="ChannelRolePermissionDeleted" />
    /// <seealso cref="ChannelUserPermissionUpdated" />
    /// <seealso cref="ChannelUserPermissionDeleted" />
    /// <seealso cref="CategoryUserPermissionCreated" />
    /// <seealso cref="CategoryUserPermissionUpdated" />
    /// <seealso cref="CategoryUserPermissionDeleted" />
    /// <seealso cref="CategoryRolePermissionCreated" />
    /// <seealso cref="CategoryRolePermissionUpdated" />
    /// <seealso cref="CategoryRolePermissionDeleted" />
    public IObservable<ChannelUserPermissionEvent> ChannelUserPermissionCreated => ((IEventInfo<ChannelUserPermissionEvent>)GuildedEvents["ChannelUserPermissionCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="IServerUserPermission">user permission override</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChannelUserPermissionUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="ChannelRolePermissionCreated" />
    /// <seealso cref="ChannelRolePermissionUpdated" />
    /// <seealso cref="ChannelRolePermissionDeleted" />
    /// <seealso cref="ChannelUserPermissionCreated" />
    /// <seealso cref="ChannelUserPermissionDeleted" />
    /// <seealso cref="CategoryUserPermissionCreated" />
    /// <seealso cref="CategoryUserPermissionUpdated" />
    /// <seealso cref="CategoryUserPermissionDeleted" />
    /// <seealso cref="CategoryRolePermissionCreated" />
    /// <seealso cref="CategoryRolePermissionUpdated" />
    /// <seealso cref="CategoryRolePermissionDeleted" />
    public IObservable<ChannelUserPermissionEvent> ChannelUserPermissionUpdated => ((IEventInfo<ChannelUserPermissionEvent>)GuildedEvents["ChannelUserPermissionUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="IServerUserPermission">user permission override</see> is removed from a <see cref="ServerChannel">channel</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChannelUserPermissionDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="ChannelRolePermissionCreated" />
    /// <seealso cref="ChannelRolePermissionUpdated" />
    /// <seealso cref="ChannelRolePermissionDeleted" />
    /// <seealso cref="ChannelUserPermissionCreated" />
    /// <seealso cref="ChannelUserPermissionUpdated" />
    /// <seealso cref="CategoryUserPermissionCreated" />
    /// <seealso cref="CategoryUserPermissionUpdated" />
    /// <seealso cref="CategoryUserPermissionDeleted" />
    /// <seealso cref="CategoryRolePermissionCreated" />
    /// <seealso cref="CategoryRolePermissionUpdated" />
    /// <seealso cref="CategoryRolePermissionDeleted" />
    public IObservable<ChannelUserPermissionEvent> ChannelUserPermissionDeleted => ((IEventInfo<ChannelUserPermissionEvent>)GuildedEvents["ChannelUserPermissionDeleted"]).Observable;
    #endregion

    #region Methods Webhooks
    /// <summary>
    /// Gets a list of <see cref="Webhook">webhooks</see>.
    /// </summary>
    /// <remarks>
    /// <para>If <paramref name="channel" /> parameter is given, it gets all of the channel <see cref="Webhook">webhooks</see> instead.</para>
    /// </remarks>
    /// <param name="server">The identifier of the <see cref="Server">server</see> to get <see cref="Webhook">webhooks</see> from</param>
    /// <param name="channel">The identifier of the <see cref="ServerChannel">channel</see> to get <see cref="Webhook">webhooks</see> from</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The list of fetched <see cref="Webhook">webhooks</see> in the specified <paramref name="channel" /></returns>
    public Task<IList<Webhook>> GetWebhooksAsync(HashId server, Guid? channel = null) =>
        GetResponsePropertyAsync<IList<Webhook>>(new RestRequest($"servers/{server}/webhooks", Method.Get)
            .AddOptionalQuery("channelId", channel)
        , "webhooks");

    /// <summary>
    /// Gets the specified <paramref name="webhook" />.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> the <see cref="Webhook">webhook</see> is</param>
    /// <param name="webhook">The identifier of the <see cref="Webhook">webhook</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The <see cref="Webhook">webhook</see> that was specified in the arguments</returns>
    public Task<Webhook> GetWebhookAsync(HashId server, Guid webhook) =>
        GetResponsePropertyAsync<Webhook>(new RestRequest($"servers/{server}/webhooks/{webhook}", Method.Get), "webhook");

    /// <summary>
    /// Creates a new <see cref="Webhook">webhook</see> in the specified <paramref name="channel" />.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Webhook">webhook</see> will be created</param>
    /// <param name="channel">The identifier of the <see cref="ServerChannel">channel</see> where the <see cref="Webhook">webhook</see> will be created</param>
    /// <param name="name">The name of the <see cref="Webhook">webhook</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <exception cref="ArgumentNullException">The specified <paramref name="name" /> is null, empty or whitespace</exception>
    /// <permission cref="Permission.ManageWebhooks" />
    /// <returns>The <see cref="Webhook">webhook</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Webhook> CreateWebhookAsync(HashId server, Guid channel, string name) =>
        string.IsNullOrWhiteSpace(name)
        ? throw new ArgumentNullException(nameof(name))
        : GetResponsePropertyAsync<Webhook>(new RestRequest($"servers/{server}/webhooks", Method.Post)
            .AddJsonBody(new
            {
                name,
                channelId = channel
            })
        , "webhook");

    /// <summary>
    /// Edits the specified <paramref name="webhook" />.
    /// </summary>
    /// <remarks>
    /// <para><see cref="Webhook" /> can moved between <see cref="ServerChannel">channels</see> using <paramref name="newChannel" /> parameter.</para>
    /// </remarks>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Webhook">webhook</see> is</param>
    /// <param name="webhook">The identifier of the <see cref="Webhook">webhook</see> to update</param>
    /// <param name="name">The new name of the <see cref="Webhook">webhook</see></param>
    /// <param name="newChannel">The identifier of the <see cref="ServerChannel">channel</see> where the <see cref="Webhook">webhook</see> will be moved to</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageWebhooks" />
    /// <exception cref="ArgumentNullException">The specified <paramref name="name" /> is null, empty or whitespace</exception>
    /// <returns>The <see cref="Webhook">webhook</see> that was updated by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Webhook> UpdateWebhookAsync(HashId server, Guid webhook, string name, Guid? newChannel = null) =>
        string.IsNullOrWhiteSpace(name)
        ? throw new ArgumentNullException(nameof(name))
        : GetResponsePropertyAsync<Webhook>(new RestRequest($"servers/{server}/webhooks/{webhook}", Method.Put)
            .AddJsonBody(new
            {
                name,
                channelId = newChannel
            })
        , "webhook");

    /// <summary>
    /// Deletes the specified <paramref name="webhook" />.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Webhook">webhook</see> is</param>
    /// <param name="webhook">The identifier of the <see cref="Webhook">webhook</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageWebhooks" />
    public Task DeleteWebhookAsync(HashId server, Guid webhook) =>
        ExecuteRequestAsync(new RestRequest($"servers/{server}/webhooks/{webhook}", Method.Delete));
    #endregion

    #region Methods Channels
    /// <summary>
    /// Gets the specified <paramref name="channel" />.
    /// </summary>
    /// <param name="channel">The identifier of the <see cref="ServerChannel">channel</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The <see cref="ServerChannel">channel</see> that was specified in the arguments</returns>
    public Task<ServerChannel> GetChannelAsync(Guid channel) =>
        GetResponsePropertyAsync<ServerChannel>(new RestRequest($"channels/{channel}", Method.Get), "channel");

    /// <summary>
    /// Creates a new channel in the specified <paramref name="server" />.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="ServerChannel">channel</see> will be created</param>
    /// <param name="name">The name of the <see cref="ServerChannel">channel</see> (max — <c>100</c>)</param>
    /// <param name="type">The type of the content that the <see cref="ServerChannel">channel</see> will hold</param>
    /// <param name="topic">The topic describing what the <see cref="ServerChannel">channel</see> is about (max — <c>512</c>)</param>
    /// <param name="isPublic">Whether the contents of the channel are publicly viewable</param>
    /// <param name="group">The identifier of the group where the <see cref="ServerChannel">channel</see> will be created</param>
    /// <param name="category">The identifier of the category where the <see cref="ServerChannel">channel</see> will be created</param>
    /// <param name="parent">The identifier of the <see cref="ServerChannel">parent channel</see> where the <see cref="ServerChannel">thread</see> will be created</param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> from where the <see cref="ServerChannel">thread</see> will be created</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <exception cref="ArgumentNullException">The specified <paramref name="name" /> is null, empty or whitespace</exception>
    /// <permission cref="Permission.ManageChannels" />
    /// <returns>The <see cref="ServerChannel">channel</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<ServerChannel> CreateChannelAsync(HashId server, string name, ChannelType type = ChannelType.Chat, string? topic = null, bool? isPublic = null, HashId? group = null, uint? category = null, Guid? parent = null, Guid? message = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));

        EnforceLimit(nameof(name), name, ServerChannel.NameLimit);
        EnforceLimitOnNullable(nameof(topic), topic, ServerChannel.TopicLimit);

        return GetResponsePropertyAsync<ServerChannel>(new RestRequest($"channels", Method.Post)
            .AddJsonBody(new
            {
                serverId = server,
                groupId = group,
                categoryId = category,
                parentId = parent,
                messageId = message,
                name,
                type,
                topic,
                isPublic
            })
        , "channel");
    }

    /// <summary>
    /// Edits the specified <paramref name="channel" />.
    /// </summary>
    /// <param name="channel">The identifier of the <see cref="ServerChannel">channel</see> to update</param>
    /// <param name="name">A new name of the <see cref="ServerChannel">channel</see> (max — <c>100</c>)</param>
    /// <param name="topic">A new topic describing what the <see cref="ServerChannel">channel</see> is about (max — <c>512</c>)</param>
    /// <param name="isPublic">Whether the contents of the channel are publicly viewable</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageChannels" />
    /// <returns>The <see cref="ServerChannel">channel</see> that was updated by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<ServerChannel> UpdateChannelAsync(Guid channel, string? name = null, string? topic = null, bool? isPublic = null)
    {
        return GetResponsePropertyAsync<ServerChannel>(new RestRequest($"channels/{channel}", Method.Patch)
            .AddJsonBody(new
            {
                name,
                topic,
                isPublic
            })
        , "channel");
    }

    /// <summary>
    /// Deletes the specified <paramref name="channel" />.
    /// </summary>
    /// <param name="channel">The identifier of the <see cref="ServerChannel">channel</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageChannels" />
    public Task DeleteChannelAsync(Guid channel) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}", Method.Delete));
    #endregion
}