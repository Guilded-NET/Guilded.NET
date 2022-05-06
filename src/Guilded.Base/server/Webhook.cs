using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base.Content;
using Guilded.Base.Embeds;
using Guilded.Base.Permissions;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Servers;

/// <summary>
/// A channel webhook.
/// </summary>
public class Webhook : ClientObject, ICreatableContent
{
    #region JSON properties
    /// <summary>
    /// Gets the identifier of the webhook.
    /// </summary>
    /// <value>Webhook ID</value>
    public Guid Id { get; set; }
    /// <summary>
    /// Gets the name of the webhook.
    /// </summary>
    /// <value>Name</value>
    public string Name { get; set; }
    /// <summary>
    /// Gets the token of the webhook.
    /// </summary>
    /// <remarks>
    /// <para>This will only be given if you have <see cref="GeneralPermissions.ManageWebhooks">manage webhooks permission</see>.</para>
    /// </remarks>
    /// <value>Token?</value>
    public string? Token { get; set; }
    /// <summary>
    /// Gets the identifier of the channel where the webhook is.
    /// </summary>
    /// <value>Channel ID</value>
    public Guid ChannelId { get; set; }
    /// <summary>
    /// Gets the identifier of the server where the webhook is.
    /// </summary>
    /// <value>Server ID</value>
    public HashId ServerId { get; set; }
    /// <summary>
    /// Gets the date when the webhook was created.
    /// </summary>
    /// <value>Date</value>
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// Gets The identifier of <see cref="User">user</see> that created the webhook.
    /// </summary>
    /// <value>User ID</value>
    public HashId CreatedBy { get; set; }
    /// <summary>
    /// Gets the date when the webhook was deleted.
    /// </summary>
    /// <remarks>
    /// <para>The webhook will remain present after being deleted for the clients to be able to render the webhook's avatar and name.</para>
    /// </remarks>
    /// <value>Date</value>
    public DateTime? DeletedAt { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="Webhook" /> with the specified properties.
    /// </summary>
    /// <param name="id">The identifier of the webhook</param>
    /// <param name="name">The name of the webhook</param>
    /// <param name="token">The token of the webhook</param>
    /// <param name="channelId">The identifier of the channel where webhook is</param>
    /// <param name="serverId">The identifier of the server where webhook is</param>
    /// <param name="createdAt">The date of when the webhook was created</param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> creator of the webhook</param>
    /// <param name="deletedAt">The date of when the webhook was deleted</param>
    [JsonConstructor]
    public Webhook(
        [JsonProperty(Required = Required.Always)]
        Guid id,

        [JsonProperty(Required = Required.Always)]
        string name,

        [JsonProperty(Required = Required.Always)]
        Guid channelId,

        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? token = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? deletedAt = null
    ) =>
        (Id, Name, Token, ChannelId, ServerId, CreatedAt, CreatedBy, DeletedAt) = (id, name, token, channelId, serverId, createdAt, createdBy, deletedAt);
    #endregion

    #region Additional
    /// <inheritdoc cref="BaseGuildedClient.CreateHookMessageAsync(Guid, string, MessageContent)"/>
    public async Task CreateMessageAsync(MessageContent message)
    {
        if (Token is null)
            throw new InvalidOperationException("Cannot execute a webhook without a token: possibly missing manage webhooks permission");
        else if (DeletedAt is not null)
            throw new InvalidOperationException("Cannot execute deleted webhook");
        else await ParentClient.CreateHookMessageAsync(Id, Token, message).ConfigureAwait(false);
    }
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
    public async Task<Webhook> UpdateAsync(string name)
    {
        if (DeletedAt is not null)
            return await ParentClient.UpdateWebhookAsync(ServerId, Id, name).ConfigureAwait(false);
        else throw new InvalidOperationException("Cannot update deleted webhook");
    }
    /// <inheritdoc cref="BaseGuildedClient.DeleteWebhookAsync(HashId, Guid)"/>
    public async Task DeleteAsync()
    {
        if (DeletedAt is null)
            await ParentClient.DeleteWebhookAsync(ServerId, Id).ConfigureAwait(false);
        else throw new InvalidOperationException("Cannot delete already deleted webhook");
    }
    #endregion
}