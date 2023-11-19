using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Base.Embeds;
using Guilded.Content;
using Guilded.Servers;
using Guilded.Users;
using RestSharp;

namespace Guilded.Client;

public abstract partial class AbstractGuildedClient
{
    #region Methods CreateHookMessageAsync with URL
    /// <summary>
    /// Creates a <see cref="Message">message</see> using the webhook specified by its <paramref name="webhookUrl">webhook URL</paramref>.
    /// </summary>
    /// <param name="webhookUrl">The URL of the <see cref="Webhook">webhook</see></param>
    /// <param name="message">The message to send</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public Task CreateHookMessageAsync(Uri webhookUrl, MessageContent message) =>
        ExecuteRequestAsync(new RestRequest(webhookUrl, Method.Post).AddJsonBody(message));

    /// <summary>
    /// Creates a <see cref="Message">message</see> using the specified the webhook specified by its <paramref name="webhookUrl">webhook URL</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>The text <paramref name="content" /> will be formatted in Markdown.</para>
    /// </remarks>
    /// <param name="webhookUrl">The URL of the <see cref="Webhook">webhook</see></param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown</param>
    /// <param name="embeds">The list of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    /// <param name="username">The displayed <see cref="UserSummary.Name">name</see> of the webhook</param>
    /// <param name="avatar">The displayed <see cref="UserSummary.Avatar">profile picture</see> of the webhook</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public Task CreateHookMessageAsync(Uri webhookUrl, string? content = null, IList<Embed>? embeds = null, string? username = null, Uri? avatar = null) =>
        CreateHookMessageAsync(webhookUrl, new MessageContent(content, embeds, username: username, avatar: avatar));

    /// <inheritdoc cref="CreateHookMessageAsync(Uri, string, IList{Embed}, string, Uri?)" />
    /// <param name="webhookUrl">The URL of the <see cref="Webhook">webhook</see></param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown</param>
    /// <param name="username">The displayed <see cref="UserSummary.Name">name</see> of the webhook</param>
    /// <param name="avatar">The displayed <see cref="UserSummary.Avatar">profile picture</see> of the webhook</param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task CreateHookMessageAsync(Uri webhookUrl, string? content = null, string? username = null, Uri? avatar = null, params Embed[] embeds) =>
        CreateHookMessageAsync(webhookUrl, new MessageContent(content, embeds, username: username, avatar: avatar));

    /// <inheritdoc cref="CreateHookMessageAsync(Uri, string, IList{Embed}, string, Uri?)" />
    /// <param name="webhookUrl">The URL of the <see cref="Webhook">webhook</see></param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown</param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task CreateHookMessageAsync(Uri webhookUrl, string content, params Embed[] embeds) =>
        CreateHookMessageAsync(webhookUrl, new MessageContent(content, embeds));

    /// <inheritdoc cref="CreateHookMessageAsync(Uri, string, IList{Embed}, string, Uri?)" />
    /// <param name="webhookUrl">The URL of the <see cref="Webhook">webhook</see></param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task CreateHookMessageAsync(Uri webhookUrl, params Embed[] embeds) =>
        CreateHookMessageAsync(webhookUrl, new MessageContent(embeds));
    #endregion

    #region Methods CreateHookMessageAsync with webhookId + token
    /// <summary>
    /// Creates a <see cref="Message">message</see> using the specified <paramref name="webhook" />.
    /// </summary>
    /// <param name="webhook">The identifier of the <see cref="Webhook">webhook</see> to execute</param>
    /// <param name="token">The <see cref="Webhook.Token">required token</see> of the <see cref="Webhook">webhook</see> to execute it</param>
    /// <param name="message">The message to send</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public Task CreateHookMessageAsync(Guid webhook, string token, MessageContent message) =>
        CreateHookMessageAsync(new Uri(GuildedUrl.Media, $"webhooks/{webhook}/{token}"), message);

    /// <summary>
    /// Creates a <see cref="Message">message</see> using using the specified <paramref name="webhook" />.
    /// </summary>
    /// <remarks>
    /// <para>The text <paramref name="content" /> will be formatted in Markdown.</para>
    /// </remarks>
    /// <param name="webhook">The identifier of the <see cref="Webhook">webhook</see> to execute</param>
    /// <param name="token">The <see cref="Webhook.Token">required token</see> of the <see cref="Webhook">webhook</see> to execute it</param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown</param>
    /// <param name="embeds">The list of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    /// <param name="username">The displayed <see cref="UserSummary.Name">name</see> of the webhook</param>
    /// <param name="avatar">The displayed <see cref="UserSummary.Avatar">profile picture</see> of the webhook</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public Task CreateHookMessageAsync(Guid webhook, string token, string? content = null, IList<Embed>? embeds = null, string? username = null, Uri? avatar = null) =>
        CreateHookMessageAsync(webhook, token, new MessageContent(content, embeds, username: username, avatar: avatar));

    /// <inheritdoc cref="CreateHookMessageAsync(Uri, string, IList{Embed}, string, Uri?)" />
    /// <param name="webhook">The identifier of the <see cref="Webhook">webhook</see> to execute</param>
    /// <param name="token">The <see cref="Webhook.Token">required token</see> of the <see cref="Webhook">webhook</see> to execute it</param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown</param>
    /// <param name="username">The displayed <see cref="UserSummary.Name">name</see> of the webhook</param>
    /// <param name="avatar">The displayed <see cref="UserSummary.Avatar">profile picture</see> of the webhook</param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task CreateHookMessageAsync(Guid webhook, string token, string? content = null, string? username = null, Uri? avatar = null, params Embed[] embeds) =>
        CreateHookMessageAsync(webhook, token, new MessageContent(content, embeds, username: username, avatar: avatar));

    /// <inheritdoc cref="CreateHookMessageAsync(Uri, string, IList{Embed}, string, Uri?)" />
    /// <param name="webhook">The identifier of the <see cref="Webhook">webhook</see> to execute</param>
    /// <param name="token">The <see cref="Webhook.Token">required token</see> of the <see cref="Webhook">webhook</see> to execute it</param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown</param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task CreateHookMessageAsync(Guid webhook, string token, string content, params Embed[] embeds) =>
        CreateHookMessageAsync(webhook, token, new MessageContent(content, embeds));

    /// <inheritdoc cref="CreateHookMessageAsync(Uri, string, IList{Embed}, string, Uri?)" />
    /// <param name="webhook">The identifier of the <see cref="Webhook">webhook</see> to execute</param>
    /// <param name="token">The <see cref="Webhook.Token">required token</see> of the <see cref="Webhook">webhook</see> to execute it</param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task CreateHookMessageAsync(Guid webhook, string token, params Embed[] embeds) =>
        CreateHookMessageAsync(webhook, token, new MessageContent(embeds));
    #endregion

    #region Methods Chat channel
    /// <summary>
    /// Gets a list of <see cref="Message">messages</see> from the specified <paramref name="channel" />.
    /// </summary>
    /// <remarks>
    /// <para>By default, private <see cref="Message">messages</see> will not be fetched. However, if private <see cref="Message">messages</see> need to be included, <paramref name="includePrivate" /> parameter can be set as <see langword="true" />.</para>
    /// </remarks>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="includePrivate">Whether to get private replies or not</param>
    /// <param name="limit">The limit of how many messages to get (default — <c>50</c>, min — <c>1</c>, max — <c>100</c>)</param>
    /// <param name="before">The max limit of the creation date of fetched messages</param>
    /// <param name="after">The min limit of the creation date of fetched messages</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetMessages" />
    /// <permission cref="Permission.GetPrivateContent">Required when viewing <see cref="Message">messages</see> set as <see cref="Message.IsPrivate">private</see> not sent by the <see cref="AbstractGuildedClient">client</see> if <paramref name="includePrivate" /> is set as true</permission>
    /// <returns>The list of fetched <see cref="Message">messages</see> in the specified <paramref name="channel" /></returns>
    public Task<IList<Message>> GetMessagesAsync(Guid channel, bool includePrivate = false, uint? limit = null, DateTime? before = null, DateTime? after = null) =>
        GetResponsePropertyAsync<IList<Message>>(
            new RestRequest($"channels/{channel}/messages", Method.Get)
                // Because it gets uppercased
                .AddQueryParameter("includePrivate", includePrivate ? "true" : "false", encode: false)
                .AddOptionalQuery("limit", limit, encode: false)
                .AddOptionalQuery("before", before)
                .AddOptionalQuery("after", after)
        , "messages");

    /// <summary>
    /// Gets the specified <paramref name="message" />.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the message it should get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetMessages" />
    /// <permission cref="Permission.GetPrivateContent">Required when viewing <see cref="Message">messages</see> set as <see cref="Message.IsPrivate">private</see> not sent by the <see cref="AbstractGuildedClient">client</see></permission>
    /// <returns>The <see cref="Message">message</see> that was specified in the arguments</returns>
    public Task<Message> GetMessageAsync(Guid channel, Guid message) =>
        GetResponsePropertyAsync<Message>(new RestRequest($"channels/{channel}/messages/{message}", Method.Get), "message");

    /// <summary>
    /// Creates a new <see cref="Message">message</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The message to send</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <exception cref="ArgumentNullException">When the <see cref="MessageContent.Content">content</see> only consists of whitespace or is <see langword="null" /> and <see cref="MessageContent.Embeds">embeds</see> are also null or its array is empty</exception>
    /// <exception cref="ArgumentOutOfRangeException">When the <see cref="Message.Content" /> is above the <see cref="Message.Content">message content</see> limit of 4000 characters</exception>
    /// <permission cref="Permission.GetMessages" />
    /// <permission cref="Permission.CreateMessages">Required when sending a <see cref="Message">message</see> in <see cref="ServerChannel">a top-most channel</see></permission>
    /// <permission cref="Permission.CreateThreadMessages">Required when sending a <see cref="Message">message</see> in <see cref="ServerChannel">a thread</see></permission>
    /// <permission cref="Permission.CreatePrivateMessages">Required when sending a <see cref="Message">message</see> that is set as <see cref="Message.IsPrivate">private</see></permission>
    /// <permission cref="Permission.CreateMessageMedia">Required when sending a <see cref="Message">message</see> that contains an image or a video</permission>
    /// <permission cref="Permission.UseEveryoneMention">Required when sending a <see cref="Message">message</see> that contains an <c>@everyone</c> or <c>@here</c> mentions</permission>
    /// <returns>The <see cref="Message">message</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Message> CreateMessageAsync(Guid channel, MessageContent message)
    {
        if (message is null)
        {
            throw new ArgumentNullException(nameof(message));
        }
        else if (
            // No content and no embeds
            (message.Content is null && message.OnlyText) ||
            // Whitespace content
            (message.Content is not null && string.IsNullOrWhiteSpace(message.Content)))
        {
            throw new ArgumentNullException(nameof(message.Content), "Message content cannot be null if there are no embeds");
        }

        return GetResponsePropertyAsync<Message>(
            new RestRequest($"channels/{channel}/messages", Method.Post).AddJsonBody(message),
            "message",
            // For slowmode handling
            channel
        );
    }

    /// <summary>
    /// Creates a new <see cref="Message">message</see>.
    /// </summary>
    /// <remarks>
    /// <para>The text <paramref name="content" /> will be formatted in Markdown.</para>
    /// </remarks>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="embeds">The list of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    /// <param name="replyTo">The list of all <see cref="Message">messages</see> it is replying to (max — <c>5</c>)</param>
    /// <param name="hiddenUrls">The <see cref="Uri">URLs</see> that will not have <see cref="Embed">link embeds</see></param>
    /// <param name="isPrivate">Whether the mention is private</param>
    /// <param name="isSilent">Whether the mention is silent and does not ping anyone</param>
    /// <exception cref="ArgumentNullException">When the <paramref name="content" /> only consists of whitespace or is <see langword="null" /></exception>
    /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="content" /> is above the message limit of 4000 characters</exception>
    /// <returns>The <see cref="Message">message</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Message> CreateMessageAsync(Guid channel, string? content = null, IList<Embed>? embeds = null, IList<Guid>? replyTo = null, ISet<Uri>? hiddenUrls = null, bool isPrivate = false, bool isSilent = false) =>
        CreateMessageAsync(channel, new MessageContent(content, embeds, replyTo, hiddenUrls, isPrivate, isSilent));

    /// <inheritdoc cref="CreateMessageAsync(Guid, string, IList{Embed}?, IList{Guid}?, ISet{Uri}?, bool, bool)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="replyTo">The list of all <see cref="Message">messages</see> it is replying to (max — <c>5</c>)</param>
    /// <param name="hiddenUrls">The <see cref="Uri">URLs</see> that will not have <see cref="Embed">link embeds</see></param>
    /// <param name="isPrivate">Whether the mention is private</param>
    /// <param name="isSilent">Whether the mention is silent and does not ping anyone</param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task<Message> CreateMessageAsync(Guid channel, string? content = null, IList<Guid>? replyTo = null, ISet<Uri>? hiddenUrls = null, bool isPrivate = false, bool isSilent = false, params Embed[] embeds) =>
        CreateMessageAsync(channel, new MessageContent(content, embeds, replyTo, hiddenUrls, isPrivate, isSilent));

    /// <inheritdoc cref="CreateMessageAsync(Guid, string, IList{Embed}?, IList{Guid}?, ISet{Uri}?, bool, bool)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="embeds">The list of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    /// <param name="hiddenUrls">The <see cref="Uri">URLs</see> that will not have <see cref="Embed">link embeds</see></param>
    /// <param name="isPrivate">Whether the mention is private</param>
    /// <param name="isSilent">Whether the mention is silent and does not ping anyone</param>
    /// <param name="replyTo">The array of all <see cref="Message">messages</see> it is replying to (max — <c>5</c>)</param>
    public Task<Message> CreateMessageAsync(Guid channel, string? content = null, IList<Embed>? embeds = null, ISet<Uri>? hiddenUrls = null, bool isPrivate = false, bool isSilent = false, params Guid[] replyTo) =>
        CreateMessageAsync(channel, new MessageContent(content, embeds, replyTo, hiddenUrls, isPrivate, isSilent));

    /// <inheritdoc cref="CreateMessageAsync(Guid, string, IList{Embed}?, IList{Guid}?, ISet{Uri}?, bool, bool)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task<Message> CreateMessageAsync(Guid channel, string content, params Embed[] embeds) =>
        CreateMessageAsync(channel, new MessageContent(content, embeds));

    /// <inheritdoc cref="CreateMessageAsync(Guid, string, IList{Embed}?, IList{Guid}?, ISet{Uri}?, bool, bool)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task<Message> CreateMessageAsync(Guid channel, params Embed[] embeds) =>
        CreateMessageAsync(channel, new MessageContent { Embeds = embeds });

    /// <summary>
    /// Edits the text <paramref name="content" /> of a <paramref name="message" />.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to edit</param>
    /// <param name="content">The <see cref="MessageContent">new contents</see> of the <see cref="Message">message</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <exception cref="ArgumentNullException">When the <paramref name="content" /> is <see langword="null" /></exception>
    /// <permission cref="Permission.GetMessages" />
    /// <permission cref="Permission.CreateMessages">Required when editing a <see cref="Message">message</see> in <see cref="ServerChannel">a top-most channel</see></permission>
    /// <permission cref="Permission.CreateThreadMessages">Required when editing a <see cref="Message">message</see> in <see cref="ServerChannel">a thread</see></permission>
    /// <permission cref="Permission.CreateMessageMedia">Required when adding an image or a video to a <see cref="Message">message</see></permission>
    /// <permission cref="Permission.UseEveryoneMention">Required when adding an <c>@everyone</c> or a <c>@here</c> mention to a <see cref="Message">message</see></permission>
    /// <returns>The <paramref name="message" /> that was updated</returns>
    public Task<Message> UpdateMessageAsync(Guid channel, Guid message, MessageContent content) =>
        content is null
        ? throw new ArgumentNullException(nameof(content))
        : GetResponsePropertyAsync<Message>(new RestRequest($"channels/{channel}/messages/{message}", Method.Put).AddJsonBody(content), "message");

    /// <inheritdoc cref="UpdateMessageAsync(Guid, Guid, MessageContent)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to edit</param>
    /// <param name="content">The new <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="embeds">The new <see cref="Embed">custom embeds</see> of the <see cref="Message">message</see> in Markdown (max — <c>1</c>)</param>
    /// <param name="hiddenUrls">The new <see cref="Uri">URLs</see> that will not have <see cref="Embed">link embeds</see></param>
    public Task<Message> UpdateMessageAsync(Guid channel, Guid message, string? content = null, IList<Embed>? embeds = null, ISet<Uri>? hiddenUrls = null) =>
        UpdateMessageAsync(channel, message, new MessageContent(content, embeds, hiddenLinkPreviewUrls: hiddenUrls));

    /// <inheritdoc cref="UpdateMessageAsync(Guid, Guid, MessageContent)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to edit</param>
    /// <param name="content">The new <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="embeds">The new <see cref="Embed">custom embeds</see> of the <see cref="Message">message</see> in Markdown (max — <c>1</c>)</param>
    public Task<Message> UpdateMessageAsync(Guid channel, Guid message, string? content = null, params Embed[] embeds) =>
        UpdateMessageAsync(channel, message, new MessageContent(content, embeds));

    /// <inheritdoc cref="UpdateMessageAsync(Guid, Guid, MessageContent)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to edit</param>
    /// <param name="content">The new <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="embeds">The new <see cref="Embed">custom embeds</see> of the <see cref="Message">message</see> in Markdown (max — <c>1</c>)</param>
    /// <param name="hiddenUrls">The new <see cref="Uri">URLs</see> that will not have <see cref="Embed">link embeds</see></param>
    public Task<Message> UpdateMessageAsync(Guid channel, Guid message, string? content = null, ISet<Uri>? hiddenUrls = null, params Embed[] embeds) =>
        UpdateMessageAsync(channel, message, new MessageContent(content, embeds, hiddenLinkPreviewUrls: hiddenUrls));

    /// <inheritdoc cref="UpdateMessageAsync(Guid, Guid, MessageContent)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to edit</param>
    /// <param name="embeds">The new <see cref="Embed">custom embeds</see> of the <see cref="Message">message</see> in Markdown (max — <c>1</c>)</param>
    public Task<Message> UpdateMessageAsync(Guid channel, Guid message, params Embed[] embeds) =>
        UpdateMessageAsync(channel, message, new MessageContent { Embeds = embeds });

    /// <summary>
    /// Deletes the specified <paramref name="message" />.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetMessages" />
    /// <permission cref="Permission.ManageMessages">Required when deleting messages made by others</permission>
    /// <permission cref="Permission.GetPrivateContent">Required for deleting messages set as <see cref="Message.IsPrivate">private</see> made by others</permission>
    public Task DeleteMessageAsync(Guid channel, Guid message) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/messages/{message}", Method.Delete));

    /// <summary>
    /// Adds the pin to the specified <paramref name="message" />.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to pin</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetMessages" />
    /// <permission cref="Permission.ManageMessages" />
    public Task PinMessageAsync(Guid channel, Guid message) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/messages/{message}/pin", Method.Post));

    /// <summary>
    /// Removes the pin from the specified <paramref name="message" />.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to remove pin from</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetMessages" />
    /// <permission cref="Permission.ManageMessages" />
    public Task UnpinMessageAsync(Guid channel, Guid message) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/messages/{message}/pin", Method.Delete));
    #endregion

    #region Methods Forum channels > Topics
    /// <summary>
    /// Gets a list of <see cref="Topic">forum topics</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="limit">The limit of how many <see cref="Topic">topics</see> to get (default — <c>25</c>, values — <c>(0, 100]</c>)</param>
    /// <param name="before">The max limit of the creation date of fetched <see cref="Topic">topics</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetTopics" />
    /// <returns>The list of fetched <see cref="Topic">forum topics</see> in the specified <paramref name="channel" /></returns>
    public Task<IList<TopicSummary>> GetTopicsAsync(Guid channel, uint? limit = null, DateTime? before = null) =>
        GetResponsePropertyAsync<IList<TopicSummary>>(
            new RestRequest($"channels/{channel}/topics", Method.Get)
                .AddOptionalQuery("limit", limit, encode: false)
                .AddOptionalQuery("before", before)
        , "forumTopics");

    /// <summary>
    /// Gets the <paramref name="topic">specified forum topic</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetTopics" />
    /// <returns>The <see cref="Topic">forum topic</see> that was specified in the arguments</returns>
    public Task<Topic> GetTopicAsync(Guid channel, uint topic) =>
        GetResponsePropertyAsync<Topic>(new RestRequest($"channels/{channel}/topics/{topic}", Method.Get), "forumTopic");

    /// <summary>
    /// Creates a new <see cref="Topic">forum topic</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="title">The title of the <see cref="Topic">forum topic</see></param>
    /// <param name="content">The content of the <see cref="Topic">forum topic</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetTopics" />
    /// <permission cref="Permission.CreateTopics" />
    /// <permission cref="Permission.UseEveryoneMention">Required when posting a <see cref="Topic">forum topic</see> that contains an <c>@everyone</c> or <c>@here</c> mentions</permission>
    /// <returns>The <see cref="Topic">forum topic</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Topic> CreateTopicAsync(Guid channel, string title, string content) =>
        string.IsNullOrWhiteSpace(title)
        ? throw new ArgumentNullException(nameof(title))
        : string.IsNullOrWhiteSpace(content)
        ? throw new ArgumentNullException(nameof(content))
        : GetResponsePropertyAsync<Topic>(new RestRequest($"channels/{channel}/topics", Method.Post)
            .AddJsonBody(new
            {
                title,
                content
            })
        , "forumTopic");

    /// <summary>
    /// Edits <see cref="Topic">forum topic's</see> <paramref name="title" /> and <paramref name="content" />.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to update</param>
    /// <param name="title">The new title of the <see cref="Topic">forum topic</see></param>
    /// <param name="content">The new contents of the <see cref="Topic">forum topic</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetTopics" />
    /// <permission cref="Permission.CreateTopics" />
    /// <permission cref="Permission.UseEveryoneMention">Required when adding an <c>@everyone</c> or <c>@here</c> mentions</permission>
    /// <returns>The <paramref name="topic">forum topic</paramref> that was updated by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Topic> UpdateTopicAsync(Guid channel, uint topic, string title, string content) =>
        string.IsNullOrWhiteSpace(title)
        ? throw new ArgumentNullException(nameof(title))
        : string.IsNullOrWhiteSpace(content)
        ? throw new ArgumentNullException(nameof(content))
        : GetResponsePropertyAsync<Topic>(new RestRequest($"channels/{channel}/topics/{topic}", Method.Patch)
            .AddJsonBody(new
            {
                title,
                content
            })
        , "forumTopic");

    /// <summary>
    /// Deletes a <see cref="Topic">forum topic</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetTopics" />
    /// <permission cref="Permission.ManageTopics">Required when deleting <see cref="Topic">forum topic</see> that the <see cref="AbstractGuildedClient">client</see> doesn't own</permission>
    public Task DeleteTopicAsync(Guid channel, uint topic) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/topics/{topic}", Method.Delete));

    /// <summary>
    /// Pins a <see cref="Topic">forum topic</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to pin</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetTopics" />
    /// <permission cref="Permission.ManageTopics" />
    public Task PinTopicAsync(Guid channel, uint topic) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/topics/{topic}/pin", Method.Put));

    /// <inheritdoc cref="PinTopicAsync(Guid, uint)" />
    [Obsolete($"Use `{nameof(PinTopicAsync)}` instead")]
    public Task AddTopicPinAsync(Guid channel, uint topic) =>
        PinTopicAsync(channel, topic);

    /// <summary>
    /// Unpins a <see cref="Topic">forum topic</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to unpin</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetTopics" />
    /// <permission cref="Permission.ManageTopics" />
    public Task UnpinTopicAsync(Guid channel, uint topic) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/topics/{topic}/pin", Method.Delete));

    /// <inheritdoc cref="UnpinTopicAsync(Guid, uint)" />
    [Obsolete($"Use `{nameof(UnpinTopicAsync)}` instead")]
    public Task RemoveTopicPinAsync(Guid channel, uint topic) =>
        UnpinTopicAsync(channel, topic);

    /// <summary>
    /// Locks a <see cref="Topic">forum topic</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to lock</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetTopics" />
    /// <permission cref="Permission.LockTopics" />
    public Task LockTopicAsync(Guid channel, uint topic) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/topics/{topic}/lock", Method.Put));

    /// <summary>
    /// Unlocks a <see cref="Topic">forum topic</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to unlock</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetTopics" />
    /// <permission cref="Permission.LockTopics" />
    public Task UnlockTopicAsync(Guid channel, uint topic) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/topics/{topic}/lock", Method.Delete));
    #endregion

    #region Methods List channels
    /// <summary>
    /// Gets a set of <see cref="Item">list items</see> from the specified <paramref name="channel" />.
    /// </summary>
    /// <param name="channel">The identifier of the <see cref="ServerChannel">channel</see> to get list items from</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetItems" />
    /// <returns>The list of fetched <see cref="Item">list items</see> in the specified <paramref name="channel" /></returns>
    public Task<IList<ItemSummary>> GetItemsAsync(Guid channel) =>
        GetResponsePropertyAsync<IList<ItemSummary>>(new RestRequest($"channels/{channel}/items", Method.Get), "listItems");

    /// <summary>
    /// Gets the specified <paramref name="listItem">list item</paramref> from a <paramref name="channel">list channel</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="listItem">The identifier of the <see cref="Item">list item</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetItems" />
    /// <returns>The <see cref="Item">list item</see> that was specified in the arguments</returns>
    public Task<Item> GetItemAsync(Guid channel, Guid listItem) =>
        GetResponsePropertyAsync<Item>(new RestRequest($"channels/{channel}/items/{listItem}", Method.Get), "listItem");

    /// <summary>
    /// Creates a new <see cref="Item">list item</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The text content of the <see cref="Item">list item</see></param>
    /// <param name="note">The text content of an <see cref="ItemNote">optional note</see> in the <see cref="Item">list item</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetItems" />
    /// <permission cref="Permission.CreateItems" />
    /// <permission cref="Permission.UseEveryoneMention">Required when posting a <see cref="Item">list item</see> that contains an <c>@everyone</c> or <c>@here</c> mentions</permission>
    /// <returns>The <see cref="Item">list item</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Item> CreateItemAsync(Guid channel, string message, string? note = null) =>
        string.IsNullOrWhiteSpace(message)
        ? throw new ArgumentNullException(nameof(message))
        : GetResponsePropertyAsync<Item>(new RestRequest($"channels/{channel}/items", Method.Post)
            .AddJsonBody(new
            {
                message,
                note = new
                {
                    content = note
                }
            })
        , "listItem");

    /// <summary>
    /// Edits the <paramref name="message">text contents</paramref> of the specified <paramref name="listItem">list item</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="listItem">The identifier of the <see cref="Item">list item</see> to edit</param>
    /// <param name="message">The new text content of the <see cref="Item">list item</see></param>
    /// <param name="note">The new text content of the note in the <see cref="Item">list item</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetItems" />
    /// <permission cref="Permission.UpdateItems">Required when updating <see cref="Item">list items</see> the <see cref="AbstractGuildedClient">client</see> doesn't own</permission>
    /// <permission cref="Permission.UseEveryoneMention">Required when adding an <c>@everyone</c> or a <c>@here</c> mention to a <see cref="Item">list item</see></permission>
    /// <returns>The <paramref name="listItem">list item</paramref> that was updated by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Item> UpdateItemAsync(Guid channel, Guid listItem, string? message = null, string? note = null)
    {
        if (message is null && note is null)
            throw new ArgumentNullException(nameof(message), "Either the message or the note of the list item's update must be specified");
        else if (message is not null && string.IsNullOrWhiteSpace(message))
            throw new ArgumentNullException(nameof(message), $"{nameof(message)} cannot be an empty or whitespace-only. Set it to null if you don't want to update the message of a list item.");
        else if (note is not null && string.IsNullOrWhiteSpace(note))
            throw new ArgumentNullException(nameof(note), $"{nameof(note)} cannot be an empty or whitespace-only. Set it to null if you don't want to update the note of a list item.");

        return GetResponsePropertyAsync<Item>(new RestRequest($"channels/{channel}/items/{listItem}", Method.Patch)
            .AddJsonBody(new
            {
                message,
                note = note is not null ? new
                {
                    content = note
                } : null
            })
        , "listItem");
    }

    /// <summary>
    /// Deletes the specified <paramref name="listItem">list item</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the <see cref="ServerChannel">channel</see> where the <see cref="Item">list item</see> is</param>
    /// <param name="listItem">The identifier of the <see cref="Item">list item</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetItems" />
    /// <permission cref="Permission.DeleteItems">Required when deleting <see cref="Item">list items</see> you don't own</permission>
    public Task DeleteItemAsync(Guid channel, Guid listItem) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/items/{listItem}", Method.Delete));

    /// <summary>
    /// Marks the specified <paramref name="listItem">list item</paramref> as <see cref="ItemBase{T}.IsCompleted">completed</see>.
    /// </summary>
    /// <param name="channel">The identifier of the <see cref="ServerChannel">channel</see> where the <see cref="Item">list item</see> is</param>
    /// <param name="listItem">The identifier of the <see cref="Item">list item</see> to complete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetItems" />
    /// <permission cref="Permission.CompleteItems" />
    public Task CompleteItemAsync(Guid channel, Guid listItem) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/items/{listItem}/complete", Method.Post));

    /// <summary>
    /// Marks the specified <paramref name="listItem">list item</paramref> as <see cref="ItemBase{T}.IsCompleted">not completed</see>.
    /// </summary>
    /// <param name="channel">The identifier of the <see cref="ServerChannel">channel</see> where the list item is</param>
    /// <param name="listItem">The identifier of the <see cref="Item">list item</see> to complete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetItems" />
    /// <permission cref="Permission.CompleteItems" />
    public Task UncompleteItemAsync(Guid channel, Guid listItem) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/items/{listItem}/complete", Method.Delete));
    #endregion

    #region Methods Document channels
    /// <summary>
    /// Gets a list of <see cref="Doc">documents</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="limit">The limit of how many <see cref="Doc">documents</see> to get (default — <c>25</c>, values — <c>(0, 100]</c>)</param>
    /// <param name="before">The max limit of the creation date of the fetched <see cref="Doc">documents</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetDocs" />
    /// <returns>The list of fetched <see cref="Doc">documents</see> in the specified <paramref name="channel" /></returns>
    public Task<IList<Doc>> GetDocsAsync(Guid channel, uint? limit = null, DateTime? before = null) =>
        GetResponsePropertyAsync<IList<Doc>>(
            new RestRequest($"channels/{channel}/docs", Method.Get)
                .AddOptionalQuery("limit", limit, encode: false)
                .AddOptionalQuery("before", before)
        , "docs");

    /// <summary>
    /// Gets the specified <paramref name="doc">document</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="doc">The identifier of the <see cref="Doc">document</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetDocs" />
    /// <returns>The <see cref="Doc">document</see> that was specified in the arguments</returns>
    public Task<Doc> GetDocAsync(Guid channel, uint doc) =>
        GetResponsePropertyAsync<Doc>(new RestRequest($"channels/{channel}/docs/{doc}", Method.Get), "doc");

    /// <summary>
    /// Creates a new <see cref="Doc">document</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="title">The title of the <see cref="Doc">document</see></param>
    /// <param name="content">The Markdown content of the <see cref="Doc">document</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetDocs" />
    /// <permission cref="Permission.CreateDocs" />
    /// <permission cref="Permission.UseEveryoneMention">Required when posting a <see cref="Doc">document</see> that contains an <c>@everyone</c> or <c>@here</c> mentions</permission>
    /// <returns>The <see cref="Doc">document</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Doc> CreateDocAsync(Guid channel, string title, string content) =>
        string.IsNullOrWhiteSpace(title)
        ? throw new ArgumentNullException(nameof(title))
        : string.IsNullOrWhiteSpace(content)
        ? throw new ArgumentNullException(nameof(content))
        : GetResponsePropertyAsync<Doc>(new RestRequest($"channels/{channel}/docs", Method.Post)
            .AddJsonBody(new
            {
                title,
                content
            })
        , "doc");

    /// <summary>
    /// Edits the text <paramref name="content" /> or the <paramref name="title" /> of the specified <paramref name="doc">document</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>The updated <paramref name="doc">document</paramref> will be bumped to the top.</para>
    /// </remarks>
    /// <param name="channel">The identifier of the parent <see cref="DocChannel">channel</see></param>
    /// <param name="doc">The identifier of the <see cref="Doc">document</see> to update/edit</param>
    /// <param name="title">The new title of this <see cref="Doc">document</see></param>
    /// <param name="content">The new Markdown content of this <see cref="Doc">document</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetDocs" />
    /// <permission cref="Permission.UpdateDocs">Required when editing <see cref="Doc">documents</see> that the <see cref="AbstractGuildedClient">client</see> doesn't own</permission>
    /// <permission cref="Permission.UseEveryoneMention">Required when adding an <c>@everyone</c> or a <c>@here</c> mention to a <see cref="Doc">document</see></permission>
    /// <returns>The <see cref="Doc">document</see> that was updated by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Doc> UpdateDocAsync(Guid channel, uint doc, string title, string content) =>
        string.IsNullOrWhiteSpace(title)
        ? throw new ArgumentNullException(nameof(title))
        : string.IsNullOrWhiteSpace(content)
        ? throw new ArgumentNullException(nameof(content))
        : GetResponsePropertyAsync<Doc>(new RestRequest($"channels/{channel}/docs/{doc}", Method.Put)
            .AddJsonBody(new
            {
                title,
                content
            })
        , "doc");

    /// <summary>
    /// Deletes the specified <paramref name="doc">document</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="doc">The identifier of the <see cref="Doc">document</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetDocs" />
    /// <permission cref="Permission.DeleteDocs">Required when deleting <see cref="Doc">documents</see> that the <see cref="AbstractGuildedClient">client</see> doesn't own</permission>
    public Task DeleteDocAsync(Guid channel, uint doc) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/docs/{doc}", Method.Delete));
    #endregion

    #region Methods Announcement channels
    /// <summary>
    /// Gets a list of <see cref="Announcement">announcements</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="AnnouncementChannel">channel</see></param>
    /// <param name="limit">The limit of how many <see cref="Announcement">announcements</see> to get (default — <c>25</c>, values — <c>(0, 100]</c>)</param>
    /// <param name="before">The max limit of the creation date of the fetched <see cref="Announcement">announcements</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetAnnouncements" />
    /// <returns>The list of fetched <see cref="Announcement">announcements</see> in the specified <paramref name="channel" /></returns>
    public Task<IList<Announcement>> GetAnnouncementsAsync(Guid channel, uint? limit = null, DateTime? before = null) =>
        GetResponsePropertyAsync<IList<Announcement>>(
            new RestRequest($"channels/{channel}/announcements", Method.Get)
                .AddOptionalQuery("limit", limit, encode: false)
                .AddOptionalQuery("before", before)
        , "docs");

    /// <summary>
    /// Gets the specified <paramref name="announcement" />.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="announcement">The identifier of the <see cref="Announcement">announcement</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetAnnouncements" />
    /// <returns>The <see cref="Announcement">announcement</see> that was specified in the arguments</returns>
    public Task<Announcement> GetAnnouncementAsync(Guid channel, HashId announcement) =>
        GetResponsePropertyAsync<Announcement>(new RestRequest($"channels/{channel}/announcements/{announcement}", Method.Get), "doc");

    /// <summary>
    /// Creates a new <see cref="Announcement">announcement</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="title">The title of the <see cref="Announcement">announcement</see></param>
    /// <param name="content">The Markdown content of the <see cref="Announcement">announcement</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetAnnouncements" />
    /// <permission cref="Permission.CreateAnnouncements" />
    /// <permission cref="Permission.UseEveryoneMention">Required when posting a <see cref="Announcement">announcement</see> that contains an <c>@everyone</c> or <c>@here</c> mentions</permission>
    /// <returns>The <see cref="Announcement">announcement</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Announcement> CreateAnnouncementAsync(Guid channel, string title, string content) =>
        string.IsNullOrWhiteSpace(title)
        ? throw new ArgumentNullException(nameof(title))
        : string.IsNullOrWhiteSpace(content)
        ? throw new ArgumentNullException(nameof(content))
        : GetResponsePropertyAsync<Announcement>(new RestRequest($"channels/{channel}/announcements", Method.Post)
            .AddJsonBody(new
            {
                title,
                content
            })
        , "announcement");

    /// <summary>
    /// Edits the text <paramref name="content" /> or the <paramref name="title" /> of the specified <paramref name="announcement" />.
    /// </summary>
    /// <remarks>
    /// <para>The updated <paramref name="announcement" /> will be bumped to the top.</para>
    /// </remarks>
    /// <param name="channel">The identifier of the parent <see cref="AnnouncementChannel">channel</see></param>
    /// <param name="announcement">The identifier of the <see cref="Announcement">announcement</see> to update/edit</param>
    /// <param name="title">The new title of this <see cref="Announcement">announcement</see></param>
    /// <param name="content">The new Markdown content of this <see cref="Announcement">announcement</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetAnnouncements" />
    /// <permission cref="Permission.ManageAnnouncements">Required when editing <see cref="Announcement">announcements</see> that the <see cref="AbstractGuildedClient">client</see> doesn't own</permission>
    /// <permission cref="Permission.UseEveryoneMention">Required when adding an <c>@everyone</c> or a <c>@here</c> mention to a <see cref="Doc">document</see></permission>
    /// <returns>The <see cref="Announcement">announcement</see> that was updated by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Announcement> UpdateAnnouncementAsync(Guid channel, HashId announcement, string title, string content) =>
        string.IsNullOrWhiteSpace(title)
        ? throw new ArgumentNullException(nameof(title))
        : string.IsNullOrWhiteSpace(content)
        ? throw new ArgumentNullException(nameof(content))
        : GetResponsePropertyAsync<Announcement>(new RestRequest($"channels/{channel}/announcements/{announcement}", Method.Patch)
            .AddJsonBody(new
            {
                title,
                content
            })
        , "announcement");

    /// <summary>
    /// Deletes the specified <paramref name="announcement" />.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="announcement">The identifier of the <see cref="Announcement">announcement</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetAnnouncements" />
    /// <permission cref="Permission.ManageAnnouncements">Required when deleting <see cref="Announcement">announcements</see> that the <see cref="AbstractGuildedClient">client</see> doesn't own</permission>
    public Task DeleteAnnouncementAsync(Guid channel, HashId announcement) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/announcements/{announcement}", Method.Delete));
    #endregion

    #region Methods Calendar channels > Events
    /// <summary>
    /// Gets a list of <see cref="CalendarEvent">calendar events</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="limit">The limit of how many <see cref="CalendarEvent">calendar events</see> to get (default — <c>25</c>, values — <c>(0, 100]</c>)</param>
    /// <param name="before">The max limit of the creation date of fetched <see cref="CalendarEvent">calendar events</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetEvents" />
    /// <returns>The list of fetched <see cref="CalendarEvent">calendar events</see> in the specified <paramref name="channel" /></returns>
    public Task<IList<CalendarEvent>> GetEventsAsync(Guid channel, uint? limit = null, DateTime? before = null) =>
        GetResponsePropertyAsync<IList<CalendarEvent>>(
            new RestRequest($"channels/{channel}/events", Method.Get)
                .AddOptionalQuery("limit", limit, encode: false)
                .AddOptionalQuery("before", before)
        , "calendarEvents");

    /// <summary>
    /// Gets the specified <paramref name="calendarEvent">calendar event</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetEvents" />
    /// <returns>The <see cref="CalendarEvent">calendar event</see> that was specified in the arguments</returns>
    public Task<CalendarEvent> GetEventAsync(Guid channel, uint calendarEvent) =>
        GetResponsePropertyAsync<CalendarEvent>(new RestRequest($"channels/{channel}/events/{calendarEvent}", Method.Get), "calendarEvent");

    /// <summary>
    /// Creates a new <see cref="CalendarEvent">calendar event</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The creation information about the <see cref="CalendarEvent">calendar event</see> being created</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetEvents" />
    /// <permission cref="Permission.CreateEvents" />
    /// <permission cref="Permission.UseEveryoneMention">Required when adding an <c>@everyone</c> or a <c>@here</c> mention to the <see cref="CalendarEvent.Description">calendar event's description</see></permission>
    /// <returns>The <see cref="CalendarEvent">calendar event</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<CalendarEvent> CreateEventAsync(Guid channel, CalendarEventContent calendarEvent)
    {
        if (calendarEvent is null)
            throw new ArgumentNullException(nameof(calendarEvent));
        // Either ignoring the non-existence of `?` or `CalendarEventContent` was used
        else if (string.IsNullOrWhiteSpace(calendarEvent.Name))
            throw new ArgumentNullException(nameof(calendarEvent), $"{nameof(calendarEvent)} cannot have a null, empty or whitespace-only name.");
        // Either null or non-empty values are allowed
        else if (calendarEvent.Description is not null && string.IsNullOrWhiteSpace(calendarEvent.Description))
            throw new ArgumentNullException(nameof(calendarEvent), $"{nameof(calendarEvent)} cannot have an empty or whitespace-only description. Set it to null if you don't want a description.");
        else if (calendarEvent.Location is not null && string.IsNullOrWhiteSpace(calendarEvent.Location))
            throw new ArgumentNullException(nameof(calendarEvent), $"{nameof(calendarEvent)} cannot have an empty or whitespace-only location. Set it to null if you don't want a location.");
        else if (calendarEvent.Duration == 0)
            throw new ArgumentNullException(nameof(calendarEvent), $"{nameof(calendarEvent)} cannot have a 0 minute duration.");

        return GetResponsePropertyAsync<CalendarEvent>(new RestRequest($"channels/{channel}/events", Method.Post).AddJsonBody(calendarEvent), "calendarEvent");
    }

    /// <summary>
    /// Creates a new <see cref="CalendarEvent">calendar event</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="name">The title of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="description">The description of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="location">The physical or non-physical location of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="startsAt">The date when the <see cref="CalendarEvent">calendar event</see> starts</param>
    /// <param name="url">The URL to the <see cref="CalendarEvent">calendar event's</see> services, place or anything related</param>
    /// <param name="color">The colour of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="duration">The duration of the <see cref="CalendarEvent">calendar event</see> in minutes</param>
    /// <param name="rsvpLimit">The limit of how many <see cref="User">users</see> can be invited or attend the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="isPrivate">Whether the <see cref="CalendarEvent">calendar event</see> is private</param>
    /// <param name="rsvpDisabled">Whether <see cref="Member">members</see> can attend the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="autofillWaitlist">Whether <see cref="Member">members</see> in the waitlist should be added to the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="isAllDay">Whether the <see cref="CalendarEvent">calendar event</see> lasts all day</param>
    /// <param name="roleIds">The list of identifiers of roles to restrict <see cref="CalendarEvent">calendar events</see></param>
    /// <param name="repeatInfo">The information about <see cref="CalendarEventSeries">calendar event repetition</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetEvents" />
    /// <permission cref="Permission.CreateEvents" />
    /// <permission cref="Permission.UseEveryoneMention">Required when adding an <c>@everyone</c> or a <c>@here</c> mention to the <see cref="CalendarEvent.Description">calendar event's description</see></permission>
    /// <returns>The <see cref="CalendarEvent">calendar event</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<CalendarEvent> CreateEventAsync(
        Guid channel,
        string name,
        string? description = null,
        string? location = null,
        DateTime? startsAt = null,
        Uri? url = null,
        Color? color = null,
        uint? duration = null,
        uint? rsvpLimit = null,
        bool isPrivate = false,
        bool rsvpDisabled = false,
        bool autofillWaitlist = false,
        bool isAllDay = false,
        IList<uint>? roleIds = null,
        CalendarEventRepetition? repeatInfo = null
    ) =>
        CreateEventAsync(channel, new CalendarEventContent(name, description, location, startsAt, url, color, duration, rsvpLimit, isPrivate, rsvpDisabled, autofillWaitlist, isAllDay, roleIds, repeatInfo));

    /// <inheritdoc cref="CreateEventAsync(Guid, string, string?, string?, DateTime?, Uri?, Color?, uint?, uint?, bool, bool, bool, bool, IList{uint}?, CalendarEventRepetition)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="name">The title of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="description">The description of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="location">The physical or non-physical location of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="startsAt">The date when the <see cref="CalendarEvent">calendar event</see> starts</param>
    /// <param name="url">The URL to the <see cref="CalendarEvent">calendar event's</see> services, place or anything related</param>
    /// <param name="color">The colour of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="duration">The duration of the <see cref="CalendarEvent">calendar event</see> in minutes</param>
    /// <param name="rsvpLimit">The limit of how many <see cref="User">users</see> can be invited or attend the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="isPrivate">Whether the <see cref="CalendarEvent">calendar event</see> is private</param>
    /// <param name="rsvpDisabled">Whether <see cref="Member">members</see> can attend the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="autofillWaitlist">Whether <see cref="Member">members</see> in the waitlist should be added to the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="isAllDay">Whether the <see cref="CalendarEvent">calendar event</see> lasts all day</param>
    /// <param name="roleIds">The list of identifiers of roles to restrict <see cref="CalendarEvent">calendar events</see></param>
    /// <param name="repeatInfo">The information about <see cref="CalendarEventSeries">calendar event repetition</see></param>
    public Task<CalendarEvent> CreateEventAsync(
        Guid channel,
        string name,
        string? description = null,
        string? location = null,
        DateTime? startsAt = null,
        Uri? url = null,
        Color? color = null,
        TimeSpan? duration = null,
        uint? rsvpLimit = null,
        bool isPrivate = false,
        bool rsvpDisabled = false,
        bool autofillWaitlist = false,
        bool isAllDay = false,
        IList<uint>? roleIds = null,
        CalendarEventRepetition? repeatInfo = null
    ) =>
        CreateEventAsync(channel, name, description, location, startsAt, url, color, (uint?)duration?.TotalMinutes, rsvpLimit, isPrivate, rsvpDisabled, autofillWaitlist, isAllDay, roleIds, repeatInfo);

    /// <summary>
    /// Edits the specified <paramref name="calendarEvent">calendar event</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> to update/edit</param>
    /// <param name="calendarEventContent">The new contents of the <see cref="CalendarEvent">calendar event</see> that is being updated</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetEvents" />
    /// <permission cref="Permission.UpdateEvents">Required when editing <see cref="CalendarEvent">calendar events</see> that the <see cref="AbstractGuildedClient">client</see> doesn't own</permission>
    /// <permission cref="Permission.UseEveryoneMention">Required when adding an <c>@everyone</c> or a <c>@here</c> mention to the <see cref="CalendarEvent.Description">calendar event's description</see></permission>
    /// <returns>The <see cref="CalendarEvent">calendar event</see> that was updated by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<CalendarEvent> UpdateEventAsync(Guid channel, uint calendarEvent, CalendarEventContent calendarEventContent) =>
        GetResponsePropertyAsync<CalendarEvent>(new RestRequest($"channels/{channel}/events/{calendarEvent}", Method.Patch).AddJsonBody(calendarEventContent), "calendarEvent");

    /// <inheritdoc cref="UpdateEventAsync(Guid, uint, CalendarEventContent)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> to update/edit</param>
    /// <param name="name">The new name of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="description">The new description of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="location">The new location of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="startsAt">The new starting date of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="url">The new URL of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="color">The new colour of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="duration">The new length/duration of the <see cref="CalendarEvent">calendar event</see> in minutes</param>
    /// <param name="rsvpLimit">The limit of how many <see cref="User">users</see> can be invited or attend the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="isPrivate">Whether the <see cref="CalendarEvent">calendar event</see> is now private or not private anymore</param>
    /// <param name="rsvpDisabled">Whether <see cref="Member">members</see> can attend the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="autofillWaitlist">Whether <see cref="Member">members</see> in the waitlist should be added to the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="isAllDay">Whether the <see cref="CalendarEvent">calendar event</see> lasts all day</param>
    /// <param name="roleIds">The list of identifiers of roles to restrict <see cref="CalendarEvent">calendar events</see></param>
    public Task<CalendarEvent> UpdateEventAsync(
        Guid channel,
        uint calendarEvent,
        string? name = null,
        string? description = null,
        string? location = null,
        DateTime? startsAt = null,
        Uri? url = null,
        Color? color = null,
        uint? duration = null,
        uint? rsvpLimit = null,
        bool? isPrivate = null,
        bool? rsvpDisabled = null,
        bool? autofillWaitlist = null,
        bool? isAllDay = null,
        IList<uint>? roleIds = null
    ) =>
        UpdateEventAsync(channel, calendarEvent, new CalendarEventContent(name, description, location, startsAt, url, color, duration, rsvpLimit, isPrivate, rsvpDisabled, autofillWaitlist, isAllDay, roleIds));

    /// <inheritdoc cref="UpdateEventAsync(Guid, uint, CalendarEventContent)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> to update/edit</param>
    /// <param name="name">The new name of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="description">The new description of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="location">The new location of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="startsAt">The new starting date of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="url">The new URL of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="color">The new colour of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="duration">The new length/duration of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="rsvpLimit">The limit of how many <see cref="User">users</see> can be invited or attend the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="isPrivate">Whether the <see cref="CalendarEvent">calendar event</see> is now private or not private anymore</param>
    /// <param name="rsvpDisabled">Whether <see cref="Member">members</see> can attend the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="autofillWaitlist">Whether <see cref="Member">members</see> in the waitlist should be added to the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="isAllDay">Whether the <see cref="CalendarEvent">calendar event</see> lasts all day</param>
    /// <param name="roleIds">The list of identifiers of roles to restrict <see cref="CalendarEvent">calendar events</see></param>
    public Task<CalendarEvent> UpdateEventAsync(
        Guid channel,
        uint calendarEvent,
        string? name = null,
        string? description = null,
        string? location = null,
        DateTime? startsAt = null,
        Uri? url = null,
        Color? color = null,
        TimeSpan? duration = null,
        uint? rsvpLimit = null,
        bool? isPrivate = null,
        bool? rsvpDisabled = null,
        bool? autofillWaitlist = null,
        bool? isAllDay = null,
        IList<uint>? roleIds = null
    ) =>
        UpdateEventAsync(channel, calendarEvent, name, description, location, startsAt, url, color, (uint?)duration?.TotalMinutes, rsvpLimit, isPrivate, rsvpDisabled, autofillWaitlist, isAllDay, roleIds);

    /// <summary>
    /// Deletes the specified <paramref name="calendarEvent">calendar event</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetEvents" />
    /// <permission cref="Permission.DeleteEvents">Required when deleting <see cref="CalendarEvent">calendar event</see> that the <see cref="AbstractGuildedClient">client</see> doesn't own</permission>
    public Task DeleteEventAsync(Guid channel, uint calendarEvent) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/events/{calendarEvent}", Method.Delete));

    /// <summary>
    /// Edits <see cref="CalendarEvent">calendar events</see> in the specified <paramref name="calendarEventSeries">calendar event series</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEventSeries">The identifier of the <see cref="CalendarEventSeries">calendar event series</see> to update/edit <see cref="CalendarEvent">calendar events</see> in</param>
    /// <param name="calendarEventSeriesContent">The new contents of all the <see cref="CalendarEvent">calendar events</see> in the <see cref="CalendarEventSeries">series</see> or other informations</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetEvents" />
    /// <permission cref="Permission.UpdateEvents">Required when editing <see cref="CalendarEvent">calendar events</see> that the <see cref="AbstractGuildedClient">client</see> doesn't own</permission>
    /// <permission cref="Permission.UseEveryoneMention">Required when adding an <c>@everyone</c> or a <c>@here</c> mention to the <see cref="CalendarEvent.Description">calendar event's description</see></permission>
    public Task UpdateEventSeriesAsync(
        Guid channel,
        Guid calendarEventSeries,
        CalendarEventSeriesContent calendarEventSeriesContent
    ) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/event_series/{calendarEventSeries}", Method.Patch).AddJsonBody(calendarEventSeriesContent));

    /// <inheritdoc cref="UpdateEventSeriesAsync(Guid, Guid, CalendarEventSeriesContent)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEventSeries">The identifier of the <see cref="CalendarEventSeries">calendar event series</see> to update/edit <see cref="CalendarEvent">calendar events</see> in </param>
    /// <param name="name">The title of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="description">The description of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="location">The physical or non-physical location of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="startsAt">The date when the <see cref="CalendarEvent">calendar event</see> starts</param>
    /// <param name="url">The URL to the <see cref="CalendarEvent">calendar event's</see> services, place or anything related</param>
    /// <param name="color">The colour of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="duration">The duration of the <see cref="CalendarEvent">calendar event</see> in minutes</param>
    /// <param name="rsvpLimit">The limit of how many <see cref="User">users</see> can be invited or attend the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="isPrivate">Whether the <see cref="CalendarEvent">calendar event</see> is private</param>
    /// <param name="rsvpDisabled">Whether <see cref="Member">members</see> can attend the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="autofillWaitlist">Whether <see cref="Member">members</see> in the waitlist should be added to the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="isAllDay">Whether the <see cref="CalendarEvent">calendar event</see> lasts all day</param>
    /// <param name="roleIds">The list of identifiers of roles to restrict <see cref="CalendarEvent">calendar events</see></param>
    /// <param name="repeatInfo">The information about <see cref="CalendarEventSeries">calendar event repetition</see></param>
    /// <param name="calendarEvent">From which <see cref="CalendarEvent">calendar event</see> onwards the <see cref="CalendarEventSeries">calendar event series</see> should be updated</param>
    public Task UpdateEventSeriesAsync(
        Guid channel,
        Guid calendarEventSeries,
        string? name = null,
        string? description = null,
        string? location = null,
        DateTime? startsAt = null,
        Uri? url = null,
        Color? color = null,
        uint? duration = null,
        uint? rsvpLimit = null,
        bool? isPrivate = null,
        bool? rsvpDisabled = null,
        bool? autofillWaitlist = null,
        bool? isAllDay = null,
        IList<uint>? roleIds = null,
        CalendarEventRepetition? repeatInfo = null,
        uint? calendarEvent = null
    ) =>
        UpdateEventSeriesAsync(channel, calendarEventSeries, new CalendarEventSeriesContent(name, description, location, startsAt, url, color, duration, rsvpLimit, isPrivate, rsvpDisabled, autofillWaitlist, isAllDay, roleIds, repeatInfo, calendarEvent));

    /// <inheritdoc cref="UpdateEventSeriesAsync(Guid, Guid, CalendarEventSeriesContent)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEventSeries">The identifier of the <see cref="CalendarEventSeries">calendar event series</see> to update/edit <see cref="CalendarEvent">calendar events</see> in </param>
    /// <param name="name">The title of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="description">The description of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="location">The physical or non-physical location of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="startsAt">The date when the <see cref="CalendarEvent">calendar event</see> starts</param>
    /// <param name="url">The URL to the <see cref="CalendarEvent">calendar event's</see> services, place or anything related</param>
    /// <param name="color">The colour of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="duration">The duration of the <see cref="CalendarEvent">calendar event</see> in minutes</param>
    /// <param name="rsvpLimit">The limit of how many <see cref="User">users</see> can be invited or attend the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="isPrivate">Whether the <see cref="CalendarEvent">calendar event</see> is private</param>
    /// <param name="rsvpDisabled">Whether <see cref="Member">members</see> can attend the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="autofillWaitlist">Whether <see cref="Member">members</see> in the waitlist should be added to the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="isAllDay">Whether the <see cref="CalendarEvent">calendar event</see> lasts all day</param>
    /// <param name="roleIds">The list of identifiers of roles to restrict <see cref="CalendarEvent">calendar events</see></param>
    /// <param name="repeatInfo">The information about <see cref="CalendarEventSeries">calendar event repetition</see></param>
    /// <param name="calendarEvent">From which <see cref="CalendarEvent">calendar event</see> onwards the <see cref="CalendarEventSeries">calendar event series</see> should be updated</param>
    public Task UpdateEventSeriesAsync(
        Guid channel,
        Guid calendarEventSeries,
        string? name = null,
        string? description = null,
        string? location = null,
        DateTime? startsAt = null,
        Uri? url = null,
        Color? color = null,
        TimeSpan? duration = null,
        uint? rsvpLimit = null,
        bool? isPrivate = null,
        bool? rsvpDisabled = null,
        bool? autofillWaitlist = null,
        bool? isAllDay = null,
        IList<uint>? roleIds = null,
        CalendarEventRepetition? repeatInfo = null,
        uint? calendarEvent = null
    ) =>
        UpdateEventSeriesAsync(channel, calendarEventSeries, name, description, location, startsAt, url, color, (uint?)duration?.TotalMinutes, rsvpLimit, isPrivate, rsvpDisabled, autofillWaitlist, isAllDay, roleIds, repeatInfo, calendarEvent);

    /// <summary>
    /// Deletes <see cref="CalendarEvent">calendar events</see> in the specified <paramref name="calendarEventSeries">calendar event series</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEventSeries">The identifier of the <see cref="CalendarEventSeries">calendar event series</see> to update/edit <see cref="CalendarEvent">calendar events</see> in </param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> after which all other <see cref="CalendarEvent">calendar events</see> in the <see cref="CalendarEventSeries">series</see> should be deleted</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetEvents" />
    /// <permission cref="Permission.DeleteEvents">Required when deleting <see cref="CalendarEvent">calendar event</see> that the <see cref="AbstractGuildedClient">client</see> doesn't own</permission>
    public Task DeleteEventSeriesAsync(Guid channel, Guid calendarEventSeries, uint? calendarEvent = null) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/event_series/{calendarEventSeries}", Method.Delete)
            .AddJsonBody(new
            {
                calendarEventId = calendarEvent
            })
        );
    #endregion
}
