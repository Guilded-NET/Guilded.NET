using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base.Client;
using Guilded.Base.Content;
using Guilded.Base.Embeds;
using Guilded.Base.Servers;
using Newtonsoft.Json;

namespace Guilded.Base.Events;

/// <summary>
/// </summary>
/// <seealso cref="MemberJoinedEvent" />
/// <seealso cref="MemberUpdatedEvent" />
/// <seealso cref="Member" />
public class WebhookEvent
{
    #region Properties
    /// <summary>
    /// Gets <see cref="Servers.Webhook">the webhook</see> that has been created or updated.
    /// </summary>
    /// <value>Webhook</value>
    /// <seealso cref="WebhookEvent" />
    /// <seealso cref="Name" />
    /// <seealso cref="ServerId" />
    public Webhook Webhook { get; }

    /// <summary>
    /// Gets the identifier of <see cref="Server">the server</see> where <see cref="Servers.Webhook">the webhook</see> has been created/updated.
    /// </summary>
    /// <value>Server ID</value>
    /// <seealso cref="WebhookEvent" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="Webhook" />
    public HashId ServerId { get; }
    #endregion

    #region Properties
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
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="WebhookEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of <see cref="Server">the server</see> where <see cref="Servers.Webhook">the webhook</see> got created/updated</param>
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

    #region Methods

    #region Method CreateMessageAsync
    /// <inheritdoc cref="BaseGuildedClient.CreateHookMessageAsync(Guid, string, MessageContent)" />
    public Task CreateMessageAsync(MessageContent message) =>
        Webhook.CreateMessageAsync(message);

    /// <inheritdoc cref="BaseGuildedClient.CreateHookMessageAsync(Guid, string, string?, IList{Embed}?, string?, Uri?)" />
    public Task CreateMessageAsync(string? content = null, IList<Embed>? embeds = null, string? username = null, Uri? avatar = null) =>
        CreateMessageAsync(new MessageContent(content, embeds, username: username, avatar: avatar));

    /// <inheritdoc cref="BaseGuildedClient.CreateHookMessageAsync(Guid, string, string?, string?, Uri?, Embed[])" />
    public Task CreateMessageAsync(string? content = null, string? username = null, Uri? avatar = null, params Embed[] embeds) =>
        CreateMessageAsync(new MessageContent(content, embeds, username: username, avatar: avatar));

    /// <inheritdoc cref="BaseGuildedClient.CreateHookMessageAsync(Guid, string, string, Embed[])" />
    public Task CreateMessageAsync(string content, params Embed[] embeds) =>
        CreateMessageAsync(new MessageContent(content, embeds));

    /// <inheritdoc cref="BaseGuildedClient.CreateHookMessageAsync(Guid, string, string, Embed[])" />
    public Task CreateMessageAsync(params Embed[] embeds) =>
        CreateMessageAsync(new MessageContent(embeds));
    #endregion

    /// <inheritdoc cref="BaseGuildedClient.UpdateWebhookAsync(HashId, Guid, string, Guid?)" />
    public Task<Webhook> UpdateAsync(string name) =>
        Webhook.UpdateAsync(name);

    /// <inheritdoc cref="BaseGuildedClient.DeleteWebhookAsync(HashId, Guid)" />
    public Task DeleteAsync() =>
        Webhook.DeleteAsync();
    #endregion
}