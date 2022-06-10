using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Guilded.Base.Content;
using Guilded.Base.Embeds;
using Guilded.Base.Permissions;
using Guilded.Base.Servers;

namespace Guilded.Base;

public abstract partial class BaseGuildedClient
{
    #region Methods Webhook

    #region Methods CreateHookMessageAsync with URL
    /// <summary>
    /// Creates <see cref="Message">a message</see> using <paramref name="webhookUrl">the specified webhook</paramref>.
    /// </summary>
    /// <param name="webhookUrl">The URL of <see cref="Webhook">the webhook</see></param>
    /// <param name="message">The message to send</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public abstract Task CreateHookMessageAsync(Uri webhookUrl, MessageContent message);

    /// <summary>
    /// Creates <see cref="Message">a message</see> using <paramref name="webhookUrl">the specified webhook</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>The <paramref name="content">text content</paramref> will be formatted in Markdown.</para>
    /// </remarks>
    /// <param name="webhookUrl">The URL of <see cref="Webhook">the webhook</see></param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of <see cref="Message">the message</see> in Markdown</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public async Task CreateHookMessageAsync(Uri webhookUrl, string content) =>
        await CreateHookMessageAsync(webhookUrl, new MessageContent(content)).ConfigureAwait(false);

    /// <summary>
    /// Creates <see cref="Message">a message</see> using <paramref name="webhookUrl">the specified webhook</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>The <paramref name="content">text content</paramref> will be formatted in Markdown.</para>
    /// </remarks>
    /// <param name="webhookUrl">The URL of <see cref="Webhook">the webhook</see></param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of <see cref="Message">the message</see> in Markdown</param>
    /// <param name="embeds">The list of <see cref="Embed">all custom embeds</see> in <see cref="Message">the message</see> (max — <c>1</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public async Task CreateHookMessageAsync(Uri webhookUrl, string content, IList<Embed> embeds) =>
        await CreateHookMessageAsync(webhookUrl, new MessageContent(content) { Embeds = embeds }).ConfigureAwait(false);

    /// <summary>
    /// Creates <see cref="Message">a message</see> using <paramref name="webhookUrl">the specified webhook</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>The <paramref name="content">text content</paramref> will be formatted in Markdown.</para>
    /// </remarks>
    /// <param name="webhookUrl">The URL of <see cref="Webhook">the webhook</see></param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of <see cref="Message">the message</see> in Markdown</param>
    /// <param name="embeds">The array of <see cref="Embed">all custom embeds</see> in <see cref="Message">the message</see> (max — <c>1</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public async Task CreateHookMessageAsync(Uri webhookUrl, string content, params Embed[] embeds) =>
        await CreateHookMessageAsync(webhookUrl, new MessageContent(content) { Embeds = embeds }).ConfigureAwait(false);

    /// <summary>
    /// Creates <see cref="Message">a message</see> using <paramref name="webhookUrl">the specified webhook</paramref>.
    /// </summary>
    /// <param name="webhookUrl">The URL of <see cref="Webhook">the webhook</see></param>
    /// <param name="embeds">The list of <see cref="Embed">all custom embeds</see> in <see cref="Message">the message</see> (max — <c>1</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public async Task CreateHookMessageAsync(Uri webhookUrl, IList<Embed> embeds) =>
        await CreateHookMessageAsync(webhookUrl, new MessageContent { Embeds = embeds }).ConfigureAwait(false);

    /// <summary>
    /// Creates <see cref="Message">a message</see> using <paramref name="webhookUrl">the specified webhook</paramref>.
    /// </summary>
    /// <param name="webhookUrl">The URL of <see cref="Webhook">the webhook</see></param>
    /// <param name="embeds">The array of <see cref="Embed">all custom embeds</see> in <see cref="Message">the message</see> (max — <c>1</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public async Task CreateHookMessageAsync(Uri webhookUrl, params Embed[] embeds) =>
        await CreateHookMessageAsync(webhookUrl, new MessageContent { Embeds = embeds }).ConfigureAwait(false);
    #endregion

    #region Methods CreateHookMessageAsync with webhookId + token
    /// <summary>
    /// Creates <see cref="Message">a message</see> using <paramref name="webhook">the specified webhook</paramref>.
    /// </summary>
    /// <param name="webhook">The identifier of <see cref="Webhook">the webhook</see> to execute</param>
    /// <param name="token">The <see cref="Webhook.Token">required token</see> of <see cref="Webhook">the webhook</see> to execute it</param>
    /// <param name="message">The message to send</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public async Task CreateHookMessageAsync(Guid webhook, string token, MessageContent message) =>
        await CreateHookMessageAsync(new Uri(GuildedUrl.Media, $"webhooks/{webhook}/{token}"), message).ConfigureAwait(false);

    /// <summary>
    /// Creates <see cref="Message">a message</see> with content containing only <paramref name="content">text</paramref> using a <paramref name="webhook">webhook</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>The <paramref name="content">text content</paramref> will be formatted in Markdown.</para>
    /// </remarks>
    /// <param name="webhook">The identifier of <see cref="Webhook">the webhook</see> to execute</param>
    /// <param name="token">The <see cref="Webhook.Token">required token</see> of <see cref="Webhook">the webhook</see> to execute it</param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of <see cref="Message">the message</see> in Markdown</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public async Task CreateHookMessageAsync(Guid webhook, string token, string content) =>
        await CreateHookMessageAsync(webhook, token, new MessageContent { Content = content }).ConfigureAwait(false);

    /// <summary>
    /// Creates <see cref="Message">a message</see> with content containing <paramref name="embeds" /> and <paramref name="content">text</paramref> using a <paramref name="webhook">webhook</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>The <paramref name="content">text content</paramref> will be formatted in Markdown.</para>
    /// </remarks>
    /// <param name="webhook">The identifier of <see cref="Webhook">the webhook</see> to execute</param>
    /// <param name="token">The <see cref="Webhook.Token">required token</see> of <see cref="Webhook">the webhook</see> to execute it</param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of <see cref="Message">the message</see> in Markdown</param>
    /// <param name="embeds">The list of <see cref="Embed">all custom embeds</see> in <see cref="Message">the message</see> (max — <c>1</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public async Task CreateHookMessageAsync(Guid webhook, string token, string content, IList<Embed> embeds) =>
        await CreateHookMessageAsync(webhook, token, new MessageContent { Content = content, Embeds = embeds }).ConfigureAwait(false);

    /// <summary>
    /// Creates <see cref="Message">a message</see> with content containing <paramref name="embeds" /> and <paramref name="content">text</paramref> using a <paramref name="webhook">webhook</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>The <paramref name="content">text content</paramref> will be formatted in Markdown.</para>
    /// </remarks>
    /// <param name="webhook">The identifier of <see cref="Webhook">the webhook</see> to execute</param>
    /// <param name="token">The <see cref="Webhook.Token">required token</see> of <see cref="Webhook">the webhook</see> to execute it</param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of <see cref="Message">the message</see> in Markdown</param>
    /// <param name="embeds">The array of <see cref="Embed">all custom embeds</see> in <see cref="Message">the message</see> (max — <c>1</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public async Task CreateHookMessageAsync(Guid webhook, string token, string content, params Embed[] embeds) =>
        await CreateHookMessageAsync(webhook, token, content, (IList<Embed>)embeds).ConfigureAwait(false);

    /// <summary>
    /// Creates <see cref="Message">a message</see> with content containing <paramref name="embeds" /> using a <paramref name="webhook">webhook</paramref>.
    /// </summary>
    /// <param name="webhook">The identifier of <see cref="Webhook">the webhook</see> to execute</param>
    /// <param name="token">The <see cref="Webhook.Token">required token</see> of <see cref="Webhook">the webhook</see> to execute it</param>
    /// <param name="embeds">The array of <see cref="Embed">all custom embeds</see> in <see cref="Message">the message</see> (max — <c>1</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public async Task CreateHookMessageAsync(Guid webhook, string token, IList<Embed> embeds) =>
        await CreateHookMessageAsync(webhook, token, new MessageContent { Embeds = embeds }).ConfigureAwait(false);

    /// <summary>
    /// Creates <see cref="Message">a message</see> with content containing <paramref name="embeds" /> using a <paramref name="webhook">webhook</paramref>.
    /// </summary>
    /// <param name="webhook">The identifier of <see cref="Webhook">the webhook</see> to execute</param>
    /// <param name="token">The <see cref="Webhook.Token">required token</see> of <see cref="Webhook">the webhook</see> to execute it</param>
    /// <param name="embeds">The array of <see cref="Embed">all custom embeds</see> in <see cref="Message">the message</see> (max — <c>1</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public async Task CreateHookMessageAsync(Guid webhook, string token, params Embed[] embeds) =>
        await CreateHookMessageAsync(webhook, token, (IList<Embed>)embeds).ConfigureAwait(false);
    #endregion

    #endregion

    #region Methods Chat channels
    /// <summary>
    /// Gets a list of <see cref="Message">messages</see> from the <paramref name="channel">specified channel</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>By default, private <see cref="Message">messages</see> will not be fetched. However, if private <see cref="Message">messages</see> need to be included, <paramref name="includePrivate" /> parameter can be set as <see langword="true" />.</para>
    /// </remarks>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the parent channel</see></param>
    /// <param name="includePrivate">Whether to get private replies or not</param>
    /// <param name="limit">The limit of how many messages to get (default — <c>50</c>, min — <c>1</c>, max — <c>100</c>)</param>
    /// <param name="before">The max limit of the creation date of fetched messages</param>
    /// <param name="after">The min limit of the creation date of fetched messages</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ChatPermissions.ReadMessages" />
    /// <permission cref="GeneralPermissions.AccessModeratorView">Required when viewing <see cref="Message">messages</see> set as <see cref="Message.IsPrivate">private</see> not sent by <see cref="BaseGuildedClient">the client</see> if <paramref name="includePrivate">asked</paramref> by <see cref="BaseGuildedClient">the client</see></permission>
    /// <returns>List of <see cref="Message">messages</see></returns>
    public abstract Task<IList<Message>> GetMessagesAsync(Guid channel, bool includePrivate = false, uint? limit = null, DateTime? before = null, DateTime? after = null);

    /// <summary>
    /// Gets the <paramref name="message">specified message</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the parent channel</see></param>
    /// <param name="message">The identifier of the message it should get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ChatPermissions.ReadMessages" />
    /// <permission cref="GeneralPermissions.AccessModeratorView">Required when viewing <see cref="Message">messages</see> set as <see cref="Message.IsPrivate">private</see> not sent by <see cref="BaseGuildedClient">the client</see></permission>
    /// <returns><paramref name="message">Specified message</paramref></returns>
    public abstract Task<Message> GetMessageAsync(Guid channel, Guid message);

    /// <summary>
    /// Creates a <see cref="Message">new message</see>.
    /// </summary>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the parent channel</see></param>
    /// <param name="message">The message to send</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <exception cref="ArgumentNullException">When the <see cref="MessageContent.Content">content</see> only consists of whitespace or is <see langword="null" /> and <see cref="MessageContent.Embeds">embeds</see> are also null or its array is empty</exception>
    /// <exception cref="ArgumentOutOfRangeException">When the <see cref="Message.Content" /> is above <see cref="Message.Content">the message content</see> limit of 4000 characters</exception>
    /// <permission cref="ChatPermissions.ReadMessages" />
    /// <permission cref="ChatPermissions.SendMessages">Required when sending <see cref="Message">a message</see> in <see cref="ServerChannel">a top-most channel</see></permission>
    /// <permission cref="ChatPermissions.SendThreadMessages">Required when sending <see cref="Message">a message</see> in <see cref="ServerChannel">a thread</see></permission>
    /// <permission cref="ChatPermissions.SendPrivateMessages">Required when sending <see cref="Message">a message</see> that is set as <see cref="Message.IsPrivate">private</see></permission>
    /// <permission cref="ChatPermissions.UploadMedia">Required when sending <see cref="Message">a message</see> that contains an image or a video</permission>
    /// <permission cref="GeneralPermissions.MentionEveryoneHere">Required when sending <see cref="Message">a message</see> that contains an <c>@everyone</c> or <c>@here</c> mentions</permission>
    /// <returns>Created <see cref="Message">message</see></returns>
    public abstract Task<Message> CreateMessageAsync(Guid channel, MessageContent message);

    /// <inheritdoc cref="CreateMessageAsync(Guid, MessageContent)" />
    /// <remarks>
    /// <para>The text <paramref name="content" /> will be formatted in Markdown.</para>
    /// </remarks>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the parent channel</see></param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of <see cref="Message">the message</see> in Markdown (max — <c>4000</c>)</param>
    /// <exception cref="ArgumentNullException">When the <paramref name="content" /> only consists of whitespace or is <see langword="null" /></exception>
    /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="content" /> is above the message limit of 4000 characters</exception>
    /// <returns>Created <see cref="Message">message</see></returns>
    public async Task<Message> CreateMessageAsync(Guid channel, string content) =>
        await CreateMessageAsync(channel, new MessageContent(content)).ConfigureAwait(false);

    /// <inheritdoc cref="CreateMessageAsync(Guid, string)" />
    /// <param name="channel">The identifier of <see cref="ServerChannel">the parent channel</see></param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of <see cref="Message">the message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="isPrivate">Whether the mention is private</param>
    /// <param name="isSilent">Whether the mention is silent and does not ping anyone</param>
    public async Task<Message> CreateMessageAsync(Guid channel, string content, bool isPrivate = false, bool isSilent = false) =>
        await CreateMessageAsync(channel, new MessageContent(content)
        {
            IsPrivate = isPrivate,
            IsSilent = isSilent
        }).ConfigureAwait(false);

    /// <inheritdoc cref="CreateMessageAsync(Guid, string)" />
    /// <param name="channel">The identifier of <see cref="ServerChannel">the parent channel</see></param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of <see cref="Message">the message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="replyTo">The array of all messages it is replying to (max — <c>5</c>)</param>
    public async Task<Message> CreateMessageAsync(Guid channel, string content, params Guid[] replyTo) =>
        await CreateMessageAsync(channel, new MessageContent(content)
        {
            ReplyMessageIds = replyTo
        }).ConfigureAwait(false);

    /// <inheritdoc cref="CreateMessageAsync(Guid, string)" />
    /// <param name="channel">The identifier of <see cref="ServerChannel">the parent channel</see></param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of <see cref="Message">the message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="isPrivate">Whether <see cref="Message.ReplyMessageIds">the reply</see> is private</param>
    /// <param name="isSilent">Whether <see cref="Message.ReplyMessageIds">the reply</see> is silent and does not ping</param>
    /// <param name="replyTo">The array of all messages it is replying to (max — <c>5</c>)</param>
    public async Task<Message> CreateMessageAsync(Guid channel, string content, bool isPrivate = false, bool isSilent = false, params Guid[] replyTo) =>
        await CreateMessageAsync(channel, new MessageContent(content)
        {
            IsPrivate = isPrivate,
            IsSilent = isSilent,
            ReplyMessageIds = replyTo
        }).ConfigureAwait(false);

    /// <inheritdoc cref="CreateMessageAsync(Guid, MessageContent)" />
    /// <param name="channel">The identifier of <see cref="ServerChannel">the parent channel</see></param>
    /// <param name="embeds">The array of <see cref="Embed">all custom embeds</see> in <see cref="Message">the message</see> (max — <c>1</c>)</param>
    public async Task<Message> CreateMessageAsync(Guid channel, params Embed[] embeds) =>
        await CreateMessageAsync(channel, new MessageContent
        {
            Embeds = embeds
        }).ConfigureAwait(false);

    /// <inheritdoc cref="CreateMessageAsync(Guid, Embed[])" />
    /// <remarks>
    /// <para>No text contents of the message will be displayed.</para>
    /// </remarks>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the parent channel</see></param>
    /// <param name="isPrivate">Whether the mention or <see cref="Message.ReplyMessageIds">the reply</see> is private</param>
    /// <param name="isSilent">Whether the mention or <see cref="Message.ReplyMessageIds">the reply</see> is silent and does not ping</param>
    /// <param name="replyTo">The array of all messages it is replying to (max — <c>5</c>)</param>
    /// <param name="embeds">The array of <see cref="Embed">all custom embeds</see> in <see cref="Message">the message</see> (max — <c>1</c>)</param>
    public async Task<Message> CreateMessageAsync(Guid channel, bool isPrivate = false, bool isSilent = false, Guid[]? replyTo = null, params Embed[] embeds) =>
        await CreateMessageAsync(channel, new MessageContent
        {
            Embeds = embeds,
            IsPrivate = isPrivate,
            IsSilent = isSilent,
            ReplyMessageIds = replyTo
        }).ConfigureAwait(false);

    /// <inheritdoc cref="CreateMessageAsync(Guid, MessageContent)" />
    /// <remarks>
    /// <para>The <paramref name="content">text contents</paramref> will be formatted in Markdown.</para>
    /// <para><paramref name="embeds">Embeds</paramref> will be displayed alongside <paramref name="content">text content</paramref>.</para>
    /// </remarks>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the parent channel</see></param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of <see cref="Message">the message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="embeds">The array of <see cref="Embed">all custom embeds</see> in <see cref="Message">the message</see> (max — <c>1</c>)</param>
    public async Task<Message> CreateMessageAsync(Guid channel, string content, params Embed[] embeds) =>
        await CreateMessageAsync(channel, new MessageContent
        {
            Content = content,
            Embeds = embeds
        }).ConfigureAwait(false);

    /// <inheritdoc cref="CreateMessageAsync(Guid, string, Embed[])" />
    /// <remarks>
    /// <para>The <paramref name="content">text contents</paramref> will be formatted in Markdown.</para>
    /// <para><paramref name="embeds">Embeds</paramref> will be displayed alongside <paramref name="content">text content</paramref>.</para>
    /// </remarks>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the parent channel</see></param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of <see cref="Message">the message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="isPrivate">Whether the mention or <see cref="Message.ReplyMessageIds">the reply</see> is private</param>
    /// <param name="isSilent">Whether the mention or <see cref="Message.ReplyMessageIds">the reply</see> is silent and does not ping</param>
    /// <param name="replyTo">The array of all <see cref="Message">messages</see> it is replying to (max — <c>5</c>)</param>
    /// <param name="embeds">The array of <see cref="Embed">all custom embeds</see> in <see cref="Message">the message</see> (max — <c>1</c>)</param>
    public async Task<Message> CreateMessageAsync(Guid channel, string content, bool isPrivate = false, bool isSilent = false, Guid[]? replyTo = null, params Embed[] embeds) =>
        await CreateMessageAsync(channel, new MessageContent
        {
            Content = content,
            Embeds = embeds,
            IsPrivate = isPrivate,
            IsSilent = isSilent,
            ReplyMessageIds = replyTo
        }).ConfigureAwait(false);

    /// <summary>
    /// Edits the <paramref name="content">text contents</paramref> of a <paramref name="message">message</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the parent channel</see></param>
    /// <param name="message">The identifier of <see cref="Message">the message</see> to edit</param>
    /// <param name="content">The <see cref="MessageContent">new contents</see> of <see cref="Message">the message</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <exception cref="ArgumentNullException">When the <paramref name="content" /> is <see langword="null" /></exception>
    /// <permission cref="ChatPermissions.ReadMessages" />
    /// <permission cref="ChatPermissions.SendMessages">Required when editing <see cref="Message">a message</see> in <see cref="ServerChannel">a top-most channel</see></permission>
    /// <permission cref="ChatPermissions.SendThreadMessages">Required when editing <see cref="Message">a message</see> in <see cref="ServerChannel">a thread</see></permission>
    /// <permission cref="ChatPermissions.UploadMedia">Required when adding an image or a video to <see cref="Message">a message</see></permission>
    /// <permission cref="GeneralPermissions.MentionEveryoneHere">Required when adding an <c>@everyone</c> or a <c>@here</c> mention to <see cref="Message">a message</see></permission>
    /// <returns>Updated <paramref name="message">message</paramref></returns>
    public abstract Task<Message> UpdateMessageAsync(Guid channel, Guid message, MessageContent content);

    /// <inheritdoc cref="UpdateMessageAsync(Guid, Guid, MessageContent)" />
    /// <param name="channel">The identifier of <see cref="ServerChannel">the parent channel</see></param>
    /// <param name="message">The identifier of <see cref="Message">the message</see> to edit</param>
    /// <param name="content">The <see cref="Message.Content">new text contents</see> of <see cref="Message">the message</see> in Markdown (max — <c>4000</c>)</param>
    public async Task<Message> UpdateMessageAsync(Guid channel, Guid message, string content) =>
        await UpdateMessageAsync(channel, message, new MessageContent
        {
            Content = content
        });

    /// <inheritdoc cref="UpdateMessageAsync(Guid, Guid, MessageContent)" />
    /// <param name="channel">The identifier of <see cref="ServerChannel">the parent channel</see></param>
    /// <param name="message">The identifier of <see cref="Message">the message</see> to edit</param>
    /// <param name="embeds">The <see cref="Embed">new custom embeds</see> of <see cref="Message">the message</see> in Markdown (max — <c>1</c>)</param>
    public async Task<Message> UpdateMessageAsync(Guid channel, Guid message, params Embed[] embeds) =>
        await UpdateMessageAsync(channel, message, new MessageContent
        {
            Embeds = embeds
        });

    /// <inheritdoc cref="UpdateMessageAsync(Guid, Guid, MessageContent)" />
    /// <param name="channel">The identifier of <see cref="ServerChannel">the parent channel</see></param>
    /// <param name="message">The identifier of <see cref="Message">the message</see> to edit</param>
    /// <param name="content">The <see cref="Message.Content">new text contents</see> of <see cref="Message">the message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="embeds">The <see cref="Embed">new custom embeds</see> of <see cref="Message">the message</see> in Markdown (max — <c>1</c>)</param>
    public async Task<Message> UpdateMessageAsync(Guid channel, Guid message, string content, params Embed[] embeds) =>
        await UpdateMessageAsync(channel, message, new MessageContent
        {
            Content = content,
            Embeds = embeds
        });

    /// <summary>
    /// Deletes the <paramref name="message">specified message</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the parent channel</see></param>
    /// <param name="message">The identifier of <see cref="Message">the message</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ChatPermissions.ReadMessages" />
    /// <permission cref="ChatPermissions.ManageMessages">Required when deleting messages made by others</permission>
    /// <permission cref="GeneralPermissions.AccessModeratorView">Required for deleting messages set as <see cref="Message.IsPrivate">private</see> made by others</permission>
    public abstract Task DeleteMessageAsync(Guid channel, Guid message);

    /// <summary>
    /// Adds <paramref name="emote">a reaction</paramref> to the <paramref name="message">specified message</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the parent channel</see></param>
    /// <param name="message">The identifier of <see cref="Message">the message</see> to add <see cref="Reaction">a reaction</see> on</param>
    /// <param name="emote">The identifier of the emote to add</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ChatPermissions.ReadMessages" />
    /// <returns>Added <see cref="Reaction">reaction</see></returns>
    public abstract Task<Reaction> AddReactionAsync(Guid channel, Guid message, uint emote);
    #endregion

    #region Methods Forum channels
    /// <summary>
    /// Creates a <see cref="Topic">new forum post</see>.
    /// </summary>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the parent channel</see></param>
    /// <param name="title">The title of <see cref="Topic">the forum post</see></param>
    /// <param name="content">The content of <see cref="Topic">the forum post</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ForumPermissions.ReadForums" />
    /// <permission cref="ForumPermissions.CreateTopics" />
    /// <permission cref="GeneralPermissions.MentionEveryoneHere">Required when posting <see cref="Topic">a forum thread</see> that contains an <c>@everyone</c> or <c>@here</c> mentions</permission>
    /// <returns>Created <see cref="Topic">forum thread</see></returns>
    public abstract Task<Topic> CreateTopicAsync(Guid channel, string title, string content);
    #endregion

    #region Methods List channels
    /// <summary>
    /// Gets a set of <see cref="ListItem">list items</see> from the <paramref name="channel">specified channel</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the channel</see> to get list items from</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ListPermissions.ViewListItems" />
    /// <returns>List of <see cref="ListItem">list items</see></returns>
    public abstract Task<IList<ListItemSummary>> GetListItemsAsync(Guid channel);

    /// <summary>
    /// Gets the <paramref name="listItem">specified list item</paramref> from a <paramref name="channel">list channel</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the parent channel</see></param>
    /// <param name="listItem">The identifier of <see cref="ListItem">the list item</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ListPermissions.ViewListItems" />
    /// <returns><paramref name="listItem">Specified list item</paramref></returns>
    public abstract Task<ListItem> GetListItemAsync(Guid channel, Guid listItem);

    /// <summary>
    /// Creates a <see cref="ListItem">new list item</see>.
    /// </summary>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the parent channel</see></param>
    /// <param name="message">The text content of <see cref="ListItem">the list item</see></param>
    /// <param name="note">The text content of an <see cref="ListItemNote">optional note</see> in <see cref="ListItem">the list item</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ListPermissions.ViewListItems" />
    /// <permission cref="ListPermissions.CreateListItem" />
    /// <permission cref="GeneralPermissions.MentionEveryoneHere">Required when posting <see cref="ListItem">a list item</see> that contains an <c>@everyone</c> or <c>@here</c> mentions</permission>
    /// <returns>Created <see cref="ListItem">list item</see></returns>
    public abstract Task<ListItem> CreateListItemAsync(Guid channel, string message, string? note = null);

    /// <summary>
    /// Edits the <paramref name="message">text contents</paramref> of the <paramref name="listItem">specified list item</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the parent channel</see></param>
    /// <param name="listItem">The identifier of <see cref="ListItem">the list item</see> to edit</param>
    /// <param name="message">The new text content of <see cref="ListItem">the list item</see></param>
    /// <param name="note">The new text content of the note in <see cref="ListItem">the list item</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ListPermissions.ViewListItems" />
    /// <permission cref="ListPermissions.ManageListItems">Required when updating <see cref="ListItem">list items</see> <see cref="BaseGuildedClient">the client</see> doesn't own</permission>
    /// <permission cref="GeneralPermissions.MentionEveryoneHere">Required when adding an <c>@everyone</c> or a <c>@here</c> mention to <see cref="ListItem">a list item</see></permission>
    /// <returns>Updated <see cref="ListItem">list item</see></returns>
    public abstract Task<ListItem> UpdateListItemAsync(Guid channel, Guid listItem, string message, string? note = null);

    /// <summary>
    /// Deletes the <paramref name="listItem">specified list item</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the channel</see> where <see cref="ListItem">the list item</see> is</param>
    /// <param name="listItem">The identifier of <see cref="ListItem">the list item</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ListPermissions.ViewListItems" />
    /// <permission cref="ListPermissions.RemoveListItems">Required when deleting <see cref="ListItem">list items</see> you don't own</permission>
    public abstract Task DeleteListItemAsync(Guid channel, Guid listItem);

    /// <summary>
    /// Marks the <paramref name="listItem">specified list item</paramref> as <see cref="ListItemBase{T}.IsCompleted">completed</see>.
    /// </summary>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the channel</see> where <see cref="ListItem">the list item</see> is</param>
    /// <param name="listItem">The identifier of <see cref="ListItem">the list item</see> to complete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ListPermissions.ViewListItems" />
    /// <permission cref="ListPermissions.CompleteListItems" />
    public abstract Task CompleteListItemAsync(Guid channel, Guid listItem);

    /// <summary>
    /// Marks the <paramref name="listItem">specified list item</paramref> as <see cref="ListItemBase{T}.IsCompleted">not completed</see>.
    /// </summary>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the channel</see> where the list item is</param>
    /// <param name="listItem">The identifier of <see cref="ListItem">the list item</see> to complete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ListPermissions.ViewListItems" />
    /// <permission cref="ListPermissions.CompleteListItems" />
    public abstract Task UncompleteListItemAsync(Guid channel, Guid listItem);
    #endregion

    #region Methods Docs channel
    /// <summary>
    /// Gets a list of <see cref="Doc">documents</see>.
    /// </summary>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the parent channel</see></param>
    /// <param name="limit">The limit of how many <see cref="Doc">documents</see> to get (default — <c>25</c>, values — <c>(0, 100]</c>)</param>
    /// <param name="before">The max limit of the creation date of <see cref="Doc">fetched documents</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="DocPermissions.ViewDocs" />
    /// <returns>List of <see cref="Doc">documents</see></returns>
    public abstract Task<IList<Doc>> GetDocsAsync(Guid channel, uint? limit = null, DateTime? before = null);

    /// <summary>
    /// Gets the <paramref name="doc">specified document</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the parent channel</see></param>
    /// <param name="doc">The identifier of <see cref="Doc">the document</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="DocPermissions.ViewDocs" />
    /// <returns>Specified <see cref="Doc">document</see></returns>
    public abstract Task<Doc> GetDocAsync(Guid channel, uint doc);

    /// <summary>
    /// Creates a <see cref="Doc">new document</see>.
    /// </summary>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the parent channel</see></param>
    /// <param name="title">The title of <see cref="Doc">the document</see></param>
    /// <param name="content">The Markdown content of <see cref="Doc">the document</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="DocPermissions.ViewDocs" />
    /// <permission cref="DocPermissions.CreateDocs" />
    /// <permission cref="GeneralPermissions.MentionEveryoneHere">Required when posting <see cref="Doc">a document</see> that contains an <c>@everyone</c> or <c>@here</c> mentions</permission>
    /// <returns>Created <see cref="Doc">document</see></returns>
    public abstract Task<Doc> CreateDocAsync(Guid channel, string title, string content);

    /// <summary>
    /// Edits <paramref name="content">the text contents</paramref> or <paramref name="title">the title</paramref> of the <paramref name="doc">specified document</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>The <paramref name="doc">updated document</paramref> will be bumped to the top.</para>
    /// </remarks>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the parent channel</see></param>
    /// <param name="doc">The identifier of the document to update/edit</param>
    /// <param name="title">The new title of this document</param>
    /// <param name="content">The new Markdown content of this document</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="DocPermissions.ViewDocs" />
    /// <permission cref="DocPermissions.ManageDocs">Required when editing <see cref="Doc">documents</see> that <see cref="BaseGuildedClient">the client</see> doesn't own</permission>
    /// <permission cref="GeneralPermissions.MentionEveryoneHere">Required when adding an <c>@everyone</c> or a <c>@here</c> mention to <see cref="Doc">a document</see></permission>
    /// <returns>Updated <see cref="Doc">document</see></returns>
    public abstract Task<Doc> UpdateDocAsync(Guid channel, uint doc, string title, string content);

    /// <summary>
    /// Deletes the <paramref name="doc">specified document</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the parent channel</see></param>
    /// <param name="doc">The identifier of <see cref="Doc">the document</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="DocPermissions.ViewDocs" />
    /// <permission cref="DocPermissions.RemoveDocs">Required when deleting <see cref="Doc">documents</see> that <see cref="BaseGuildedClient">the client</see> doesn't own</permission>
    public abstract Task DeleteDocAsync(Guid channel, uint doc);
    #endregion

    #region Methods Any Content
    /// <summary>
    /// Adds <paramref name="emote">a reaction</paramref> to the <paramref name="content">specified content</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the parent channel</see></param>
    /// <param name="content">The identifier of <see cref="ChannelContent{TId, TServer}">the content</see> to add <see cref="Reaction">a reaction</see> on</param>
    /// <param name="emote">The identifier of the emote to add</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="DocPermissions.ViewDocs" />
    /// <permission cref="MediaPermissions.SeeMedia" />
    /// <permission cref="ForumPermissions.ReadForums" />
    /// <permission cref="CalendarPermissions.ViewEvents" />
    /// <returns>Added <see cref="Reaction">reaction</see></returns>
    public abstract Task<Reaction> AddReactionAsync(Guid channel, uint content, uint emote);
    #endregion
}