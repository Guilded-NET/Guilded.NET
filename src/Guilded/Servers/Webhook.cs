using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Base.Embeds;
using Guilded.Client;
using Guilded.Content;
using Guilded.Events;
using Guilded.Permissions;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Servers;

/// <summary>
/// Represents a channel webhook. This is a bot-like <see cref="ServerChannel">channel</see> member that creates messages, list items or forum threads once its URL is invoked.
/// </summary>
/// <remarks>
/// <para>As of now, webhooks are only available in <see cref="ChatChannel">chat channels</see> and <see cref="ListChannel">list channels</see>. They cannot appear in <see cref="ChannelType.Chat">chat</see>-like <see cref="ServerChannel">channels</see> like <see cref="VoiceChannel">voice channels</see> or <see cref="StreamChannel">streaming channels</see>.</para>
/// </remarks>
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
    /// Gets the identifier of the <see cref="Webhook">webhook</see>.
    /// </summary>
    /// <value>The identifier of the <see cref="Webhook">webhook</see></value>
    /// <seealso cref="Webhook" />
    /// <seealso cref="Name" />
    /// <seealso cref="Avatar" />
    /// <seealso cref="Token" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="ServerId" />
    public Guid Id { get; }

    /// <summary>
    /// Gets the name of the <see cref="Webhook">webhook</see>.
    /// </summary>
    /// <value>The name of the <see cref="Webhook">webhook</see></value>
    /// <seealso cref="Webhook" />
    /// <seealso cref="Id" />
    /// <seealso cref="Avatar" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="ServerId" />
    public string Name { get; }

    /// <summary>
    /// Gets the display avatar of the <see cref="Webhook">webhook</see>.
    /// </summary>
    /// <value>The display avatar of the <see cref="Webhook">webhook</see></value>
    /// <seealso cref="Webhook" />
    /// <seealso cref="Id" />
    /// <seealso cref="Name" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="ServerId" />
    public Uri? Avatar { get; }

    /// <summary>
    /// Gets the token of the <see cref="Webhook">webhook</see>.
    /// </summary>
    /// <remarks>
    /// <para>This will only be given if you have <see cref="Permission.ManageWebhooks">manage webhooks permission</see>.</para>
    /// </remarks>
    /// <value>The token of the <see cref="Webhook">webhook</see></value>
    /// <seealso cref="Webhook" />
    /// <seealso cref="Name" />
    /// <seealso cref="Id" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="ServerId" />
    public string? Token { get; }

    /// <summary>
    /// Gets the identifier of the channel where the <see cref="Webhook">webhook</see> is.
    /// </summary>
    /// <value>The identifier of the channel where the <see cref="Webhook">webhook</see> is</value>
    /// <seealso cref="Webhook" />
    /// <seealso cref="ServerId" />
    /// <seealso cref="Id" />
    /// <seealso cref="Token" />
    /// <seealso cref="Name" />
    public Guid ChannelId { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="Server">server</see> where the <see cref="Webhook">webhook</see> is.
    /// </summary>
    /// <value>The identifier of the <see cref="Server">server</see> where the <see cref="Webhook">webhook</see> is</value>
    /// <seealso cref="Webhook" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="Id" />
    /// <seealso cref="Token" />
    /// <seealso cref="Name" />
    public HashId ServerId { get; }

    /// <summary>
    /// Gets the date when the <see cref="Webhook">webhook</see> was created.
    /// </summary>
    /// <value>The date when the <see cref="Webhook">webhook</see> was created</value>
    /// <seealso cref="Webhook" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="DeletedAt" />
    public DateTime CreatedAt { get; }

    /// <summary>
    /// Gets the identifier of <see cref="User">user</see> that created the <see cref="Webhook">webhook</see>.
    /// </summary>
    /// <value>The identifier of <see cref="User">user</see> that created the <see cref="Webhook">webhook</see></value>
    /// <seealso cref="Webhook" />
    /// <seealso cref="CreatedAt" />
    /// <seealso cref="DeletedAt" />
    public HashId CreatedBy { get; }

    /// <summary>
    /// Gets the date when the <see cref="Webhook">webhook</see> was deleted.
    /// </summary>
    /// <remarks>
    /// <para><see cref="Webhook">The webhook</see> will remain present after being deleted for the clients to be able to render the webhook's avatar and name.</para>
    /// </remarks>
    /// <value>The date when the <see cref="Webhook">webhook</see> was deleted</value>
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
    [MemberNotNullWhen(true, nameof(Token))]
    public bool IsExecutable => Token is not null;
    #endregion

    #region Properties Events
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when <see cref="Webhook">webhook</see> gets edited.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="Webhook">webhook</see> and <see cref="Server">server</see> specifically.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when <see cref="Webhook">webhook</see> gets edited</returns>
    public IObservable<WebhookEvent> Updated =>
        ParentClient
            .WebhookUpdated
            .HasId(Id)
            .InServer(ServerId);
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="Webhook" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the <see cref="Webhook">webhook</see></param>
    /// <param name="name">The name of the <see cref="Webhook">webhook</see></param>
    /// <param name="avatar">The profile picture of the <see cref="Webhook">webhook</see></param>
    /// <param name="token">The token of the <see cref="Webhook">webhook</see></param>
    /// <param name="channelId">The identifier of the channel where webhook is</param>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where webhook is</param>
    /// <param name="createdAt">The date when the <see cref="Webhook">webhook</see> was created</param>
    /// <param name="createdBy">The identifier of the <see cref="User">user</see> that created the <see cref="Webhook">webhook</see></param>
    /// <param name="deletedAt">The date when the <see cref="Webhook">webhook</see> was deleted</param>
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
        Uri? avatar = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? token = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? deletedAt = null
    ) =>
        (Id, Name, Avatar, Token, ChannelId, ServerId, CreatedAt, CreatedBy, DeletedAt) = (id, name, avatar, token, channelId, serverId, createdAt, createdBy, deletedAt);
    #endregion

    #region Method CreateMessageAsync
    /// <inheritdoc cref="AbstractGuildedClient.CreateHookMessageAsync(Guid, string, MessageContent)" />
    public Task CreateMessageAsync(MessageContent message) =>
        // Not executable
        Token is null
        ? throw new InvalidOperationException("Cannot execute a webhook without a token: possibly missing manage webhooks permission")
        : DeletedAt is not null
        ? throw new InvalidOperationException("Cannot execute deleted webhook")
        : ParentClient.CreateHookMessageAsync(Id, Token, message);

    /// <inheritdoc cref="AbstractGuildedClient.CreateHookMessageAsync(Guid, string, string?, IList{Embed}?, string?, Uri?)" />
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown</param>
    /// <param name="embeds">The list of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    /// <param name="username">The displayed <see cref="UserSummary.Name">name</see> of the webhook</param>
    /// <param name="avatar">The displayed <see cref="UserSummary.Avatar">profile picture</see> of the webhook</param>
    public Task CreateMessageAsync(string? content = null, IList<Embed>? embeds = null, string? username = null, Uri? avatar = null) =>
        CreateMessageAsync(new MessageContent(content, embeds, username: username, avatar: avatar));

    /// <inheritdoc cref="AbstractGuildedClient.CreateHookMessageAsync(Guid, string, string?, IList{Embed}?, string?, Uri?)" />
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown</param>
    /// <param name="username">The displayed <see cref="UserSummary.Name">name</see> of the webhook</param>
    /// <param name="avatar">The displayed <see cref="UserSummary.Avatar">profile picture</see> of the webhook</param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task CreateMessageAsync(string? content = null, string? username = null, Uri? avatar = null, params Embed[] embeds) =>
        CreateMessageAsync(new MessageContent(content, embeds, username: username, avatar: avatar));

    /// <inheritdoc cref="AbstractGuildedClient.CreateHookMessageAsync(Guid, string, string?, IList{Embed}?, string?, Uri?)" />
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown</param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task CreateMessageAsync(string content, params Embed[] embeds) =>
        CreateMessageAsync(new MessageContent(content, embeds));

    /// <inheritdoc cref="AbstractGuildedClient.CreateHookMessageAsync(Guid, string, string?, IList{Embed}?, string?, Uri?)" />
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task CreateMessageAsync(params Embed[] embeds) =>
        CreateMessageAsync(new MessageContent(embeds));
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.UpdateWebhookAsync(HashId, Guid, string, Guid?)" />
    /// <param name="name">The new name of the <see cref="Webhook">webhook</see></param>
    public Task<Webhook> UpdateAsync(string name) =>
        DeletedAt is not null
        ? ParentClient.UpdateWebhookAsync(ServerId, Id, name)
        : throw new InvalidOperationException("Cannot update deleted webhook");

    /// <inheritdoc cref="AbstractGuildedClient.DeleteWebhookAsync(HashId, Guid)" />
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