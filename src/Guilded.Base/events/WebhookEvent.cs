using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base.Content;
using Guilded.Base.Embeds;
using Guilded.Base.Servers;
using Newtonsoft.Json;

namespace Guilded.Base.Events;

/// <summary>
/// An event that occurs once a webhook gets created/updated.
/// </summary>
/// <remarks>
/// <para>An event of the name <c>TeamWebhookCreated</c> or <c>TeamWebhookUpdated</c> and opcode <c>0</c> that occurs once webhook gets created or updated.</para>
/// </remarks>
/// <seealso cref="RolesUpdatedEvent"/>
/// <seealso cref="XpAddedEvent"/>
/// <seealso cref="MemberJoinedEvent"/>
/// <seealso cref="MemberUpdatedEvent"/>
/// <seealso cref="WelcomeEvent"/>
/// <seealso cref="Member"/>
public class WebhookEvent : BaseObject
{
    #region JSON properties
    /// <summary>
    /// The webhook that got created or updated.
    /// </summary>
    /// <remarks>
    /// <para>The webhook that was created or updated.</para>
    /// </remarks>
    /// <value>Webhook</value>
    public Webhook Webhook { get; }
    /// <summary>
    /// The identifier of the server where webhook got created/updated.
    /// </summary>
    /// <remarks>
    /// <para>The identifier of the server where the webhook was created or updated.</para>
    /// </remarks>
    /// <value>Server ID</value>
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
    /// Creates a new instance of <see cref="WebhookEvent"/>. This is currently only used in deserialization.
    /// </summary>
    /// <param name="serverId">The identifier of the server where the webhook got created/updated</param>
    /// <param name="webhook">The webhook that got created or updated</param>
    [JsonConstructor]
    public WebhookEvent(
        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        Webhook webhook
    ) =>
        (ServerId, Webhook) = (serverId, webhook);
    #endregion

    #region Additional
    /// <inheritdoc cref="BaseGuildedClient.CreateHookMessageAsync(Guid, string, MessageContent)"/>
    public async Task CreateMessageAsync(MessageContent message) =>
        await Webhook.CreateMessageAsync(message).ConfigureAwait(false);
    /// <inheritdoc cref="BaseGuildedClient.CreateHookMessageAsync(Guid, string, string)"/>
    public async Task CreateMessageAsync(string message) =>
        await CreateMessageAsync(new MessageContent { Content = message }).ConfigureAwait(false);
    /// <inheritdoc cref="BaseGuildedClient.CreateHookMessageAsync(Guid, string, string, Embed[])"/>
    public async Task CreateMessageAsync(string message, params Embed[] embeds) =>
        await CreateMessageAsync(new MessageContent { Content = message, Embeds = embeds }).ConfigureAwait(false);
    /// <inheritdoc cref="BaseGuildedClient.CreateHookMessageAsync(Guid, string, string, IList{Embed})"/>
    public async Task CreateMessageAsync(string message, IList<Embed> embeds) =>
        await CreateMessageAsync(new MessageContent { Content = message, Embeds = embeds }).ConfigureAwait(false);
    /// <inheritdoc cref="BaseGuildedClient.CreateHookMessageAsync(Guid, string, Embed[])"/>
    public async Task CreateMessageAsync(params Embed[] embeds) =>
        await CreateMessageAsync(new MessageContent { Embeds = embeds }).ConfigureAwait(false);
    /// <inheritdoc cref="BaseGuildedClient.CreateHookMessageAsync(Guid, string, IList{Embed})"/>
    public async Task CreateMessageAsync(IList<Embed> embeds) =>
        await CreateMessageAsync(new MessageContent { Embeds = embeds }).ConfigureAwait(false);
    /// <inheritdoc cref="BaseGuildedClient.UpdateWebhookAsync(HashId, Guid, string, Guid?)"/>
    public async Task<Webhook> UpdateAsync(string name) =>
        await Webhook.UpdateAsync(name).ConfigureAwait(false);
    /// <inheritdoc cref="BaseGuildedClient.DeleteWebhookAsync(HashId, Guid)"/>
    public async Task DeleteAsync() =>
        await Webhook.DeleteAsync().ConfigureAwait(false);
    #endregion
}