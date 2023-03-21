using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Base.Embeds;
using Guilded.Client;
using Guilded.Content;
using Guilded.Servers;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when someone creates, updates or deletes a <see cref="Servers.Webhook">webhook</see>.
/// </summary>
/// <seealso cref="MemberJoinedEvent" />
/// <seealso cref="MemberUpdatedEvent" />
/// <seealso cref="Member" />
public class WebhookEvent : IModelHasId<Guid>, ICreatableContent, IServerBased, IChannelBased
{
    #region Properties
    /// <summary>
    /// Gets the <see cref="Servers.Webhook">webhook</see> that has been created or updated.
    /// </summary>
    /// <value>The <see cref="Servers.Webhook">webhook</see> that has been created or updated</value>
    /// <seealso cref="WebhookEvent" />
    /// <seealso cref="Name" />
    /// <seealso cref="ServerId" />
    public Webhook Webhook { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="Server">server</see> where the <see cref="Servers.Webhook">webhook</see> has been created/updated.
    /// </summary>
    /// <value>The identifier of the <see cref="Server">server</see> where the <see cref="Servers.Webhook">webhook</see> has been created/updated</value>
    /// <seealso cref="WebhookEvent" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="Webhook" />
    public HashId ServerId { get; }
    #endregion

    #region Properties Additional
    /// <inheritdoc cref="Webhook.Id" />
    public Guid Id => Webhook.Id;

    /// <inheritdoc cref="Webhook.Name" />
    public string Name => Webhook.Name;

    /// <inheritdoc cref="Webhook.Token" />
    public string? Token => Webhook.Token;

    /// <inheritdoc cref="Webhook.ChannelId" />
    public Guid ChannelId => Webhook.ChannelId;

    /// <inheritdoc cref="Webhook.CreatedAt" />
    public DateTime CreatedAt => Webhook.CreatedAt;

    /// <inheritdoc cref="Webhook.CreatedBy" />
    public HashId CreatedBy => Webhook.CreatedBy;

    /// <inheritdoc cref="IHasParentClient.ParentClient" />
    public AbstractGuildedClient ParentClient => Webhook.ParentClient;
    #endregion

    #region Properties Events
    /// <inheritdoc cref="Webhook.Updated" />
    public IObservable<WebhookEvent> Updated =>
        Webhook.Updated;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="WebhookEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="Servers.Webhook">webhook</see> got created/updated</param>
    /// <param name="webhook"><see cref="Servers.Webhook">The webhook</see> that got created or updated</param>
    /// <returns>New <see cref="WebhookEvent" /> JSON instance</returns>
    /// <seealso cref="WebhookEvent" />
    [JsonConstructor]
    public WebhookEvent(
        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        Webhook webhook
    ) =>
        (ServerId, Webhook) = (serverId, webhook);
    #endregion

    #region Method CreateMessageAsync
    /// <inheritdoc cref="AbstractGuildedClient.CreateHookMessageAsync(Guid, string, MessageContent)" />
    public Task CreateMessageAsync(MessageContent message) =>
        Webhook.CreateMessageAsync(message);

    /// <inheritdoc cref="AbstractGuildedClient.CreateHookMessageAsync(Guid, string, string?, IList{Embed}?, string?, Uri?)" />
    public Task CreateMessageAsync(string? content = null, IList<Embed>? embeds = null, string? username = null, Uri? avatar = null) =>
        CreateMessageAsync(new MessageContent(content, embeds, username: username, avatar: avatar));

    /// <inheritdoc cref="AbstractGuildedClient.CreateHookMessageAsync(Guid, string, string?, string?, Uri?, Embed[])" />
    public Task CreateMessageAsync(string? content = null, string? username = null, Uri? avatar = null, params Embed[] embeds) =>
        CreateMessageAsync(new MessageContent(content, embeds, username: username, avatar: avatar));

    /// <inheritdoc cref="AbstractGuildedClient.CreateHookMessageAsync(Guid, string, string, Embed[])" />
    public Task CreateMessageAsync(string content, params Embed[] embeds) =>
        CreateMessageAsync(new MessageContent(content, embeds));

    /// <inheritdoc cref="AbstractGuildedClient.CreateHookMessageAsync(Guid, string, string, Embed[])" />
    public Task CreateMessageAsync(params Embed[] embeds) =>
        CreateMessageAsync(new MessageContent(embeds));
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.UpdateWebhookAsync(HashId, Guid, string, Guid?)" />
    public Task<Webhook> UpdateAsync(string name) =>
        Webhook.UpdateAsync(name);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteWebhookAsync(HashId, Guid)" />
    public Task DeleteAsync() =>
        Webhook.DeleteAsync();
    #endregion
}