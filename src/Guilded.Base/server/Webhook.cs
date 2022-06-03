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
/// Represents a channel webhook. This is a bot-like <see cref="ServerChannel">channel</see> member that creates messages, list items or forum threads once its URL is invoked.
/// </summary>
/// <seealso cref="Member" />
/// <seealso cref="ServerChannel" />
/// <seealso cref="MemberSummary{T}" />
public class Webhook : ContentModel, ICreatableContent
{
    #region Properties
    /// <summary>
    /// Gets the identifier of <see cref="Webhook">the webhook</see>.
    /// </summary>
    /// <value><see cref="Id">Webhook ID</see></value>
    /// <seealso cref="Webhook" />
    /// <seealso cref="Name" />
    /// <seealso cref="Token" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="ServerId" />
    public Guid Id { get; }

    /// <summary>
    /// Gets the name of <see cref="Webhook">the webhook</see>.
    /// </summary>
    /// <value>Name</value>
    /// <seealso cref="Webhook" />
    /// <seealso cref="Id" />
    /// <seealso cref="Token" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="ServerId" />
    public string Name { get; }

    /// <summary>
    /// Gets the token of <see cref="Webhook">the webhook</see>.
    /// </summary>
    /// <remarks>
    /// <para>This will only be given if you have <see cref="GeneralPermissions.ManageWebhooks">manage webhooks permission</see>.</para>
    /// </remarks>
    /// <value>Token?</value>
    /// <seealso cref="Webhook" />
    /// <seealso cref="Name" />
    /// <seealso cref="Id" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="ServerId" />
    public string? Token { get; }

    /// <summary>
    /// Gets the identifier of the channel where <see cref="Webhook">the webhook</see> is.
    /// </summary>
    /// <value><see cref="ServerChannel.Id">Channel ID</see></value>
    /// <seealso cref="Webhook" />
    /// <seealso cref="ServerId" />
    /// <seealso cref="Id" />
    /// <seealso cref="Token" />
    /// <seealso cref="Name" />
    public Guid ChannelId { get; }

    /// <summary>
    /// Gets the identifier of <see cref="Server">the server</see> where <see cref="Webhook">the webhook</see> is.
    /// </summary>
    /// <value>Server ID</value>
    /// <seealso cref="Webhook" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="Id" />
    /// <seealso cref="Token" />
    /// <seealso cref="Name" />
    public HashId ServerId { get; }

    /// <summary>
    /// Gets the date when <see cref="Webhook">the webhook</see> was created.
    /// </summary>
    /// <value>Date</value>
    /// <seealso cref="Webhook" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="DeletedAt" />
    public DateTime CreatedAt { get; }

    /// <summary>
    /// Gets the identifier of <see cref="User">user</see> that created <see cref="Webhook">the webhook</see>.
    /// </summary>
    /// <value><see cref="UserSummary.Id">User ID</see></value>
    /// <seealso cref="Webhook" />
    /// <seealso cref="CreatedAt" />
    /// <seealso cref="DeletedAt" />
    public HashId CreatedBy { get; }

    /// <summary>
    /// Gets the date when <see cref="Webhook">the webhook</see> was deleted.
    /// </summary>
    /// <remarks>
    /// <para><see cref="Webhook">The webhook</see> will remain present after being deleted for the clients to be able to render the webhook's avatar and name.</para>
    /// </remarks>
    /// <value>Date</value>
    /// <seealso cref="Webhook" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="CreatedAt" />
    public DateTime? DeletedAt { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="Webhook" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of <see cref="Webhook">the webhook</see></param>
    /// <param name="name">The name of <see cref="Webhook">the webhook</see></param>
    /// <param name="token">The token of <see cref="Webhook">the webhook</see></param>
    /// <param name="channelId">The identifier of the channel where webhook is</param>
    /// <param name="serverId">The identifier of <see cref="Server">the server</see> where webhook is</param>
    /// <param name="createdAt">the date when <see cref="Webhook">the webhook</see> was created</param>
    /// <param name="createdBy">The identifier of <see cref="User">the user</see> that created <see cref="Webhook">the webhook</see></param>
    /// <param name="deletedAt">the date when <see cref="Webhook">the webhook</see> was deleted</param>
    /// <returns>New <see cref="Webhook" /> JSON instance</returns>
    /// <seealso cref="Webhook" />
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

    #region Methods

    #region Method CreateMessageAsync
    /// <inheritdoc cref="BaseGuildedClient.CreateHookMessageAsync(Guid, string, MessageContent)" />
    public async Task CreateMessageAsync(MessageContent message)
    {
        if (Token is null)
            throw new InvalidOperationException("Cannot execute a webhook without a token: possibly missing manage webhooks permission");
        else if (DeletedAt is not null)
            throw new InvalidOperationException("Cannot execute deleted webhook");
        else await ParentClient.CreateHookMessageAsync(Id, Token, message).ConfigureAwait(false);
    }

    /// <inheritdoc cref="BaseGuildedClient.CreateHookMessageAsync(Guid, string, string)" />
    public async Task CreateMessageAsync(string message) =>
        await CreateMessageAsync(new MessageContent { Content = message }).ConfigureAwait(false);

    /// <inheritdoc cref="BaseGuildedClient.CreateHookMessageAsync(Guid, string, string, Embed[])" />
    public async Task CreateMessageAsync(string message, params Embed[] embeds) =>
        await CreateMessageAsync(new MessageContent { Content = message, Embeds = embeds }).ConfigureAwait(false);

    /// <inheritdoc cref="BaseGuildedClient.CreateHookMessageAsync(Guid, string, string, IList{Embed})" />
    public async Task CreateMessageAsync(string message, IList<Embed> embeds) =>
        await CreateMessageAsync(new MessageContent { Content = message, Embeds = embeds }).ConfigureAwait(false);

    /// <inheritdoc cref="BaseGuildedClient.CreateHookMessageAsync(Guid, string, Embed[])" />
    public async Task CreateMessageAsync(params Embed[] embeds) =>
        await CreateMessageAsync(new MessageContent { Embeds = embeds }).ConfigureAwait(false);

    /// <inheritdoc cref="BaseGuildedClient.CreateHookMessageAsync(Guid, string, IList{Embed})" />
    public async Task CreateMessageAsync(IList<Embed> embeds) =>
        await CreateMessageAsync(new MessageContent { Embeds = embeds }).ConfigureAwait(false);
    #endregion

    /// <inheritdoc cref="BaseGuildedClient.UpdateWebhookAsync(HashId, Guid, string, Guid?)" />
    public async Task<Webhook> UpdateAsync(string name)
    {
        if (DeletedAt is not null)
            return await ParentClient.UpdateWebhookAsync(ServerId, Id, name).ConfigureAwait(false);
        else throw new InvalidOperationException("Cannot update deleted webhook");
    }

    /// <inheritdoc cref="BaseGuildedClient.DeleteWebhookAsync(HashId, Guid)" />
    public async Task DeleteAsync()
    {
        if (DeletedAt is null)
            await ParentClient.DeleteWebhookAsync(ServerId, Id).ConfigureAwait(false);
        else throw new InvalidOperationException("Cannot delete already deleted webhook");
    }
    #endregion
}