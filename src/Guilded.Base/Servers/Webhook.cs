using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base.Client;
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
public class Webhook : ContentModel, ICreatableContent, IServerBased, IChannelBased, IWebhook
{
    #region Fields
    /// <summary>
    /// The max characters a <see cref="Webhook">webhook</see> <see cref="Name">name</see> can have.
    /// </summary>
    public const int NameLimit = 128;
    #endregion

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
    /// <para>This will only be given if you have <see cref="GeneralPermissions.ManageWebhook">manage webhooks permission</see>.</para>
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
    /// Gets the identifier of the <see cref="Server">server</see> where <see cref="Webhook">the webhook</see> is.
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

    /// <inheritdoc />
    public Uri Url => IsExecutable ? CreateUrl(Id, Token!) : throw new InvalidOperationException("Cannot execute a webhook without knowing its token. Use `IsExecutable` property to know if a token exists");

    /// <summary>
    /// Gets whether the <see cref="Webhook">webhook</see> can be executed.
    /// </summary>
    /// <returns><see cref="Webhook" /> is executable</returns>
    public bool IsExecutable => Token is not null;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="Webhook" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of <see cref="Webhook">the webhook</see></param>
    /// <param name="name">The name of <see cref="Webhook">the webhook</see></param>
    /// <param name="token">The token of <see cref="Webhook">the webhook</see></param>
    /// <param name="channelId">The identifier of the channel where webhook is</param>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where webhook is</param>
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
    public Task CreateMessageAsync(MessageContent message) =>
        // Not executable
        Token is null
        ? throw new InvalidOperationException("Cannot execute a webhook without a token: possibly missing manage webhooks permission")
        : DeletedAt is not null
        ? throw new InvalidOperationException("Cannot execute deleted webhook")
        : ParentClient.CreateHookMessageAsync(Id, Token, message);

    /// <inheritdoc cref="BaseGuildedClient.CreateHookMessageAsync(Guid, string, string?, IList{Embed}?, string?, Uri?)" />
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown</param>
    /// <param name="embeds">The list of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    /// <param name="username">The displayed <see cref="UserSummary.Name">name</see> of the webhook</param>
    /// <param name="avatar">The displayed <see cref="UserSummary.Avatar">profile picture</see> of the webhook</param>
    public Task CreateMessageAsync(string? content = null, IList<Embed>? embeds = null, string? username = null, Uri? avatar = null) =>
        CreateMessageAsync(new MessageContent(content, embeds, username: username, avatar: avatar));

    /// <inheritdoc cref="BaseGuildedClient.CreateHookMessageAsync(Guid, string, string?, IList{Embed}?, string?, Uri?)" />
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown</param>
    /// <param name="username">The displayed <see cref="UserSummary.Name">name</see> of the webhook</param>
    /// <param name="avatar">The displayed <see cref="UserSummary.Avatar">profile picture</see> of the webhook</param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task CreateMessageAsync(string? content = null, string? username = null, Uri? avatar = null, params Embed[] embeds) =>
        CreateMessageAsync(new MessageContent(content, embeds, username: username, avatar: avatar));

    /// <inheritdoc cref="BaseGuildedClient.CreateHookMessageAsync(Guid, string, string?, IList{Embed}?, string?, Uri?)" />
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown</param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task CreateMessageAsync(string content, params Embed[] embeds) =>
        CreateMessageAsync(new MessageContent(content, embeds));

    /// <inheritdoc cref="BaseGuildedClient.CreateHookMessageAsync(Guid, string, string?, IList{Embed}?, string?, Uri?)" />
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task CreateMessageAsync(params Embed[] embeds) =>
        CreateMessageAsync(new MessageContent(embeds));
    #endregion

    /// <inheritdoc cref="BaseGuildedClient.UpdateWebhookAsync(HashId, Guid, string, Guid?)" />
    /// <param name="name">The new name of the <see cref="Webhook">webhook</see></param>
    public Task<Webhook> UpdateAsync(string name) =>
        DeletedAt is not null
        ? ParentClient.UpdateWebhookAsync(ServerId, Id, name)
        : throw new InvalidOperationException("Cannot update deleted webhook");

    /// <inheritdoc cref="BaseGuildedClient.DeleteWebhookAsync(HashId, Guid)" />
    public Task DeleteAsync() =>
        DeletedAt is null
        ? ParentClient.DeleteWebhookAsync(ServerId, Id)
        : throw new InvalidOperationException("Cannot delete already deleted webhook");
    #endregion

    #region Static methods
    /// <summary>
    /// Generates a URL from given <see cref="Webhook">webhook</see> credentials.
    /// </summary>
    /// <param name="webhook">The identifier of the <see cref="Webhook">webhook</see></param>
    /// <param name="token">The secret token of the <see cref="Webhook">webhook</see></param>
    /// <returns><see cref="Url">Webhook URL</see></returns>
    public static Uri CreateUrl(Guid webhook, string token) =>
        new(GuildedUrl.Media, $"/webhooks/{webhook}/{token}");

    /// <inheritdoc cref="CreateUrl(Guid, string)" />
    /// <param name="webhook">The identifier of the <see cref="Webhook">webhook</see></param>
    /// <param name="token">The secret token of the <see cref="Webhook">webhook</see></param>
    public static Uri CreateUrl(string webhook, string token) =>
        CreateUrl(new Guid(webhook), token);
    #endregion
}