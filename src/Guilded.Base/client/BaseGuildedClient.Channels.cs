using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Creates a <see cref="Message">message</see> using the specified <paramref name="webhookUrl">webhook</paramref>.
    /// </summary>
    /// <param name="webhookUrl">The URL of the <see cref="Webhook">webhook</see></param>
    /// <param name="message">The message to send</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public abstract Task CreateHookMessageAsync(Uri webhookUrl, MessageContent message);

    /// <summary>
    /// Creates a <see cref="Message">message</see> using the specified <paramref name="webhookUrl">webhook</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>The <paramref name="content">text content</paramref> will be formatted in Markdown.</para>
    /// </remarks>
    /// <param name="webhookUrl">The URL of the <see cref="Webhook">webhook</see></param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public Task CreateHookMessageAsync(Uri webhookUrl, string content) =>
        CreateHookMessageAsync(webhookUrl, new MessageContent(content));

    /// <summary>
    /// Creates a <see cref="Message">message</see> using the specified <paramref name="webhookUrl">webhook</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>The <paramref name="content">text content</paramref> will be formatted in Markdown.</para>
    /// </remarks>
    /// <param name="webhookUrl">The URL of the <see cref="Webhook">webhook</see></param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown</param>
    /// <param name="embeds">The list of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public Task CreateHookMessageAsync(Uri webhookUrl, string content, IList<Embed> embeds) =>
        CreateHookMessageAsync(webhookUrl, new MessageContent(content) { Embeds = embeds });

    /// <summary>
    /// Creates a <see cref="Message">message</see> using the specified <paramref name="webhookUrl">webhook</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>The <paramref name="content">text content</paramref> will be formatted in Markdown.</para>
    /// </remarks>
    /// <param name="webhookUrl">The URL of the <see cref="Webhook">webhook</see></param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown</param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public Task CreateHookMessageAsync(Uri webhookUrl, string content, params Embed[] embeds) =>
        CreateHookMessageAsync(webhookUrl, new MessageContent(content) { Embeds = embeds });

    /// <summary>
    /// Creates a <see cref="Message">message</see> using the specified <paramref name="webhookUrl">webhook</paramref>.
    /// </summary>
    /// <param name="webhookUrl">The URL of the <see cref="Webhook">webhook</see></param>
    /// <param name="embeds">The list of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public Task CreateHookMessageAsync(Uri webhookUrl, IList<Embed> embeds) =>
        CreateHookMessageAsync(webhookUrl, new MessageContent { Embeds = embeds });

    /// <summary>
    /// Creates a <see cref="Message">message</see> using the specified <paramref name="webhookUrl">webhook</paramref>.
    /// </summary>
    /// <param name="webhookUrl">The URL of the <see cref="Webhook">webhook</see></param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public Task CreateHookMessageAsync(Uri webhookUrl, params Embed[] embeds) =>
        CreateHookMessageAsync(webhookUrl, new MessageContent { Embeds = embeds });
    #endregion

    #region Methods CreateHookMessageAsync with webhookId + token
    /// <summary>
    /// Creates a <see cref="Message">message</see> using <paramref name="webhook">the specified webhook</paramref>.
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
    /// Creates a <see cref="Message">message</see> with content containing only <paramref name="content">text</paramref> using a <paramref name="webhook">webhook</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>The <paramref name="content">text content</paramref> will be formatted in Markdown.</para>
    /// </remarks>
    /// <param name="webhook">The identifier of the <see cref="Webhook">webhook</see> to execute</param>
    /// <param name="token">The <see cref="Webhook.Token">required token</see> of the <see cref="Webhook">webhook</see> to execute it</param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public Task CreateHookMessageAsync(Guid webhook, string token, string content) =>
        CreateHookMessageAsync(webhook, token, new MessageContent { Content = content });

    /// <summary>
    /// Creates a <see cref="Message">message</see> with content containing <paramref name="embeds" /> and <paramref name="content">text</paramref> using a <paramref name="webhook">webhook</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>The <paramref name="content">text content</paramref> will be formatted in Markdown.</para>
    /// </remarks>
    /// <param name="webhook">The identifier of the <see cref="Webhook">webhook</see> to execute</param>
    /// <param name="token">The <see cref="Webhook.Token">required token</see> of the <see cref="Webhook">webhook</see> to execute it</param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown</param>
    /// <param name="embeds">The list of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public Task CreateHookMessageAsync(Guid webhook, string token, string content, IList<Embed> embeds) =>
        CreateHookMessageAsync(webhook, token, new MessageContent { Content = content, Embeds = embeds });

    /// <summary>
    /// Creates a <see cref="Message">message</see> with content containing <paramref name="embeds" /> and <paramref name="content">text</paramref> using a <paramref name="webhook">webhook</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>The <paramref name="content">text content</paramref> will be formatted in Markdown.</para>
    /// </remarks>
    /// <param name="webhook">The identifier of the <see cref="Webhook">webhook</see> to execute</param>
    /// <param name="token">The <see cref="Webhook.Token">required token</see> of the <see cref="Webhook">webhook</see> to execute it</param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown</param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public Task CreateHookMessageAsync(Guid webhook, string token, string content, params Embed[] embeds) =>
        CreateHookMessageAsync(webhook, token, content, (IList<Embed>)embeds);

    /// <summary>
    /// Creates a <see cref="Message">message</see> with content containing <paramref name="embeds" /> using a <paramref name="webhook">webhook</paramref>.
    /// </summary>
    /// <param name="webhook">The identifier of the <see cref="Webhook">webhook</see> to execute</param>
    /// <param name="token">The <see cref="Webhook.Token">required token</see> of the <see cref="Webhook">webhook</see> to execute it</param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public Task CreateHookMessageAsync(Guid webhook, string token, IList<Embed> embeds) =>
        CreateHookMessageAsync(webhook, token, new MessageContent { Embeds = embeds });

    /// <summary>
    /// Creates a <see cref="Message">message</see> with content containing <paramref name="embeds" /> using a <paramref name="webhook">webhook</paramref>.
    /// </summary>
    /// <param name="webhook">The identifier of the <see cref="Webhook">webhook</see> to execute</param>
    /// <param name="token">The <see cref="Webhook.Token">required token</see> of the <see cref="Webhook">webhook</see> to execute it</param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public Task CreateHookMessageAsync(Guid webhook, string token, params Embed[] embeds) =>
        CreateHookMessageAsync(webhook, token, (IList<Embed>)embeds);
    #endregion

    #endregion

    #region Methods Chat channels
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
    /// <permission cref="ChatPermissions.ReadMessages" />
    /// <permission cref="GeneralPermissions.AccessModeratorView">Required when viewing <see cref="Message">messages</see> set as <see cref="Message.IsPrivate">private</see> not sent by the <see cref="BaseGuildedClient">client</see> if <paramref name="includePrivate">asked</paramref> by the <see cref="BaseGuildedClient">client</see></permission>
    /// <returns>The list of fetched <see cref="Message">messages</see> in the specified <paramref name="channel" /></returns>
    public abstract Task<IList<Message>> GetMessagesAsync(Guid channel, bool includePrivate = false, uint? limit = null, DateTime? before = null, DateTime? after = null);

    /// <summary>
    /// Gets the <paramref name="message">specified message</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the message it should get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ChatPermissions.ReadMessages" />
    /// <permission cref="GeneralPermissions.AccessModeratorView">Required when viewing <see cref="Message">messages</see> set as <see cref="Message.IsPrivate">private</see> not sent by the <see cref="BaseGuildedClient">client</see></permission>
    /// <returns>Specified <see cref="Message">message</see></returns>
    public abstract Task<Message> GetMessageAsync(Guid channel, Guid message);

    /// <summary>
    /// Creates a <see cref="Message">new message</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The message to send</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <exception cref="ArgumentNullException">When the <see cref="MessageContent.Content">content</see> only consists of whitespace or is <see langword="null" /> and <see cref="MessageContent.Embeds">embeds</see> are also null or its array is empty</exception>
    /// <exception cref="ArgumentOutOfRangeException">When the <see cref="Message.Content" /> is above <see cref="Message.Content">the message content</see> limit of 4000 characters</exception>
    /// <permission cref="ChatPermissions.ReadMessages" />
    /// <permission cref="ChatPermissions.SendMessages">Required when sending a <see cref="Message">message</see> in <see cref="ServerChannel">a top-most channel</see></permission>
    /// <permission cref="ChatPermissions.SendThreadMessages">Required when sending a <see cref="Message">message</see> in <see cref="ServerChannel">a thread</see></permission>
    /// <permission cref="ChatPermissions.SendPrivateMessages">Required when sending a <see cref="Message">message</see> that is set as <see cref="Message.IsPrivate">private</see></permission>
    /// <permission cref="ChatPermissions.UploadMedia">Required when sending a <see cref="Message">message</see> that contains an image or a video</permission>
    /// <permission cref="GeneralPermissions.MentionEveryoneHere">Required when sending a <see cref="Message">message</see> that contains an <c>@everyone</c> or <c>@here</c> mentions</permission>
    /// <returns>The <see cref="Message">message</see> that was created by the <see cref="BaseGuildedClient">client</see></returns>
    public abstract Task<Message> CreateMessageAsync(Guid channel, MessageContent message);

    /// <inheritdoc cref="CreateMessageAsync(Guid, MessageContent)" />
    /// <remarks>
    /// <para>The text <paramref name="content" /> will be formatted in Markdown.</para>
    /// </remarks>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <exception cref="ArgumentNullException">When the <paramref name="content" /> only consists of whitespace or is <see langword="null" /></exception>
    /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="content" /> is above the message limit of 4000 characters</exception>
    /// <returns>The <see cref="Message">message</see> that was created by the <see cref="BaseGuildedClient">client</see></returns>
    public Task<Message> CreateMessageAsync(Guid channel, string content) =>
        CreateMessageAsync(channel, new MessageContent(content));

    /// <inheritdoc cref="CreateMessageAsync(Guid, string)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="isPrivate">Whether the mention is private</param>
    /// <param name="isSilent">Whether the mention is silent and does not ping anyone</param>
    public Task<Message> CreateMessageAsync(Guid channel, string content, bool isPrivate = false, bool isSilent = false) =>
        CreateMessageAsync(channel, new MessageContent(content)
        {
            IsPrivate = isPrivate,
            IsSilent = isSilent
        });

    /// <inheritdoc cref="CreateMessageAsync(Guid, string)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="replyTo">The array of all <see cref="Message">messages</see> it is replying to (max — <c>5</c>)</param>
    public Task<Message> CreateMessageAsync(Guid channel, string content, params Guid[] replyTo) =>
        CreateMessageAsync(channel, new MessageContent(content)
        {
            ReplyMessageIds = replyTo
        });

    /// <inheritdoc cref="CreateMessageAsync(Guid, string)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="isPrivate">Whether the <see cref="Message.ReplyMessageIds">reply</see> is private</param>
    /// <param name="isSilent">Whether the <see cref="Message.ReplyMessageIds">reply</see> is silent and does not ping</param>
    /// <param name="replyTo">The array of all <see cref="Message">messages</see> it is replying to (max — <c>5</c>)</param>
    public Task<Message> CreateMessageAsync(Guid channel, string content, bool isPrivate = false, bool isSilent = false, params Guid[] replyTo) =>
        CreateMessageAsync(channel, new MessageContent(content)
        {
            IsPrivate = isPrivate,
            IsSilent = isSilent,
            ReplyMessageIds = replyTo
        });

    /// <inheritdoc cref="CreateMessageAsync(Guid, MessageContent)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task<Message> CreateMessageAsync(Guid channel, params Embed[] embeds) =>
        CreateMessageAsync(channel, new MessageContent
        {
            Embeds = embeds
        });

    /// <inheritdoc cref="CreateMessageAsync(Guid, Embed[])" />
    /// <remarks>
    /// <para>No text contents of the message will be displayed.</para>
    /// </remarks>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="isPrivate">Whether the mention or the <see cref="Message.ReplyMessageIds">reply</see> is private</param>
    /// <param name="isSilent">Whether the mention or the <see cref="Message.ReplyMessageIds">reply</see> is silent and does not ping</param>
    /// <param name="replyTo">The array of all <see cref="Message">messages</see> it is replying to (max — <c>5</c>)</param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task<Message> CreateMessageAsync(Guid channel, bool isPrivate = false, bool isSilent = false, Guid[]? replyTo = null, params Embed[] embeds) =>
        CreateMessageAsync(channel, new MessageContent
        {
            Embeds = embeds,
            IsPrivate = isPrivate,
            IsSilent = isSilent,
            ReplyMessageIds = replyTo
        });

    /// <inheritdoc cref="CreateMessageAsync(Guid, MessageContent)" />
    /// <remarks>
    /// <para>The <paramref name="content">text contents</paramref> will be formatted in Markdown.</para>
    /// <para><paramref name="embeds">Embeds</paramref> will be displayed alongside <paramref name="content">text content</paramref>.</para>
    /// </remarks>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task<Message> CreateMessageAsync(Guid channel, string content, params Embed[] embeds) =>
        CreateMessageAsync(channel, new MessageContent
        {
            Content = content,
            Embeds = embeds
        });

    /// <inheritdoc cref="CreateMessageAsync(Guid, string, Embed[])" />
    /// <remarks>
    /// <para>The <paramref name="content">text contents</paramref> will be formatted in Markdown.</para>
    /// <para><paramref name="embeds">Embeds</paramref> will be displayed alongside <paramref name="content">text content</paramref>.</para>
    /// </remarks>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="isPrivate">Whether the mention or the <see cref="Message.ReplyMessageIds">reply</see> is private</param>
    /// <param name="isSilent">Whether the mention or the <see cref="Message.ReplyMessageIds">reply</see> is silent and does not ping</param>
    /// <param name="replyTo">The array of all <see cref="Message">messages</see> it is replying to (max — <c>5</c>)</param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task<Message> CreateMessageAsync(Guid channel, string content, bool isPrivate = false, bool isSilent = false, Guid[]? replyTo = null, params Embed[] embeds) =>
        CreateMessageAsync(channel, new MessageContent
        {
            Content = content,
            Embeds = embeds,
            IsPrivate = isPrivate,
            IsSilent = isSilent,
            ReplyMessageIds = replyTo
        });

    /// <summary>
    /// Edits the <paramref name="content">text contents</paramref> of a <paramref name="message">message</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to edit</param>
    /// <param name="content">The <see cref="MessageContent">new contents</see> of the <see cref="Message">message</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <exception cref="ArgumentNullException">When the <paramref name="content" /> is <see langword="null" /></exception>
    /// <permission cref="ChatPermissions.ReadMessages" />
    /// <permission cref="ChatPermissions.SendMessages">Required when editing a <see cref="Message">message</see> in <see cref="ServerChannel">a top-most channel</see></permission>
    /// <permission cref="ChatPermissions.SendThreadMessages">Required when editing a <see cref="Message">message</see> in <see cref="ServerChannel">a thread</see></permission>
    /// <permission cref="ChatPermissions.UploadMedia">Required when adding an image or a video to a <see cref="Message">message</see></permission>
    /// <permission cref="GeneralPermissions.MentionEveryoneHere">Required when adding an <c>@everyone</c> or a <c>@here</c> mention to a <see cref="Message">message</see></permission>
    /// <returns>The <paramref name="message" /> that was updated</returns>
    public abstract Task<Message> UpdateMessageAsync(Guid channel, Guid message, MessageContent content);

    /// <inheritdoc cref="UpdateMessageAsync(Guid, Guid, MessageContent)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to edit</param>
    /// <param name="content">The new <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    public Task<Message> UpdateMessageAsync(Guid channel, Guid message, string content) =>
        UpdateMessageAsync(channel, message, new MessageContent
        {
            Content = content
        });

    /// <inheritdoc cref="UpdateMessageAsync(Guid, Guid, MessageContent)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to edit</param>
    /// <param name="embeds">The new <see cref="Embed">custom embeds</see> of the <see cref="Message">message</see> in Markdown (max — <c>1</c>)</param>
    public Task<Message> UpdateMessageAsync(Guid channel, Guid message, params Embed[] embeds) =>
        UpdateMessageAsync(channel, message, new MessageContent
        {
            Embeds = embeds
        });

    /// <inheritdoc cref="UpdateMessageAsync(Guid, Guid, MessageContent)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to edit</param>
    /// <param name="content">The new <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="embeds">The new <see cref="Embed">custom embeds</see> of the <see cref="Message">message</see> in Markdown (max — <c>1</c>)</param>
    public Task<Message> UpdateMessageAsync(Guid channel, Guid message, string content, params Embed[] embeds) =>
        UpdateMessageAsync(channel, message, new MessageContent
        {
            Content = content,
            Embeds = embeds
        });

    /// <summary>
    /// Deletes the <paramref name="message">specified message</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to delete</param>
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
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The identifier of the emote to add</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ChatPermissions.ReadMessages" />
    public abstract Task AddReactionAsync(Guid channel, Guid message, uint emote);

    /// <inheritdoc cref="AddReactionAsync(Guid, uint, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to add</param>
    public Task AddReactionAsync(Guid channel, Guid message, Emote emote) =>
        AddReactionAsync(channel, message, emote.Id);

    /// <summary>
    /// Removes <paramref name="emote">a reaction</paramref> from the <paramref name="message">specified message</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to remove</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ChatPermissions.ReadMessages" />
    public abstract Task RemoveReactionAsync(Guid channel, Guid message, uint emote);

    /// <inheritdoc cref="RemoveReactionAsync(Guid, uint, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to remove</param>
    public Task RemoveReactionAsync(Guid channel, Guid message, Emote emote) =>
        AddReactionAsync(channel, message, emote.Id);
    #endregion

    #region Methods Forum channels
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
    /// <permission cref="ForumPermissions.ReadForums" />
    /// <returns>The list of fetched <see cref="Topic">forum topics</see> in the specified <paramref name="channel" /></returns>
    public abstract Task<IList<TopicSummary>> GetTopicsAsync(Guid channel, uint? limit = null, DateTime? before = null);

    /// <summary>
    /// Gets the <paramref name="topic">specified topic</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ForumPermissions.ReadForums" />
    /// <returns>Specified <see cref="Topic">topic</see></returns>
    public abstract Task<Topic> GetTopicAsync(Guid channel, uint topic);

    /// <summary>
    /// Creates a new <see cref="Topic">forum post</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="title">The title of <see cref="Topic">the forum post</see></param>
    /// <param name="content">The content of <see cref="Topic">the forum post</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ForumPermissions.ReadForums" />
    /// <permission cref="ForumPermissions.CreateTopics" />
    /// <permission cref="GeneralPermissions.MentionEveryoneHere">Required when posting a <see cref="Topic">forum topic</see> that contains an <c>@everyone</c> or <c>@here</c> mentions</permission>
    /// <returns>The <see cref="Topic">forum topic</see> that was created by the <see cref="BaseGuildedClient">client</see></returns>
    public abstract Task<Topic> CreateTopicAsync(Guid channel, string title, string content);

    /// <summary>
    /// Edits <see cref="Topic">forum post's</see> <paramref name="title" /> and <paramref name="content">contents</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">topic</see> to update</param>
    /// <param name="title">The new title of the <see cref="Topic">forum topic</see></param>
    /// <param name="content">The new contents of the <see cref="Topic">forum topic</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ForumPermissions.ReadForums" />
    /// <permission cref="ForumPermissions.CreateTopics" />
    /// <permission cref="GeneralPermissions.MentionEveryoneHere">Required when adding an <c>@everyone</c> or <c>@here</c> mentions</permission>
    /// <returns>The <paramref name="topic">forum topic</paramref> that was updated by the <see cref="BaseGuildedClient">client</see></returns>
    public abstract Task<Topic> UpdateTopicAsync(Guid channel, uint topic, string title, string content);

    /// <summary>
    /// Deletes a <see cref="Topic">forum post</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">topic</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ForumPermissions.ReadForums" />
    /// <permission cref="DocPermissions.RemoveDocs">Required when deleting <see cref="Topic">forum topic</see> that the <see cref="BaseGuildedClient">client</see> doesn't own</permission>
    public abstract Task DeleteTopicAsync(Guid channel, uint topic);
    #endregion

    #region Methods List channels
    /// <summary>
    /// Gets a set of <see cref="ListItem">list items</see> from the specified <paramref name="channel" />.
    /// </summary>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the channel</see> to get list items from</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ListPermissions.ViewListItems" />
    /// <returns>The list of fetched <see cref="ListItem">list items</see> in the specified <paramref name="channel" /></returns>
    public abstract Task<IList<ListItemSummary>> GetItemsAsync(Guid channel);

    /// <inheritdoc cref="GetItemsAsync(Guid)" />
    /// <param name="channel">The identifier of <see cref="ServerChannel">the channel</see> to get list items from</param>
    [Obsolete($"Use `{nameof(GetItemsAsync)}` instead")]
    public Task<IList<ListItemSummary>> GetListItemsAsync(Guid channel) =>
        GetItemsAsync(channel);

    /// <summary>
    /// Gets the specified <paramref name="listItem">list item</paramref> from a <paramref name="channel">list channel</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="listItem">The identifier of the <see cref="ListItem">list item</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ListPermissions.ViewListItems" />
    /// <returns>Specified <paramref name="listItem">list item</paramref></returns>
    public abstract Task<ListItem> GetItemAsync(Guid channel, Guid listItem);

    /// <inheritdoc cref="GetItemAsync(Guid, Guid)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="listItem">The identifier of the <see cref="ListItem">list item</see> to get</param>
    [Obsolete($"Use `{nameof(GetItemAsync)}` instead")]
    public Task<ListItem> GetListItemAsync(Guid channel, Guid listItem) =>
        GetItemAsync(channel, listItem);

    /// <summary>
    /// Creates a new <see cref="ListItem">list item</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The text content of the <see cref="ListItem">list item</see></param>
    /// <param name="note">The text content of an <see cref="ListItemNote">optional note</see> in the <see cref="ListItem">list item</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ListPermissions.ViewListItems" />
    /// <permission cref="ListPermissions.CreateListItem" />
    /// <permission cref="GeneralPermissions.MentionEveryoneHere">Required when posting <see cref="ListItem">a list item</see> that contains an <c>@everyone</c> or <c>@here</c> mentions</permission>
    /// <returns>The <see cref="ListItem">list item</see> that was created by the <see cref="BaseGuildedClient">client</see></returns>
    public abstract Task<ListItem> CreateItemAsync(Guid channel, string message, string? note = null);

    /// <inheritdoc cref="CreateItemAsync(Guid, string, string)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The text content of the <see cref="ListItem">list item</see></param>
    /// <param name="note">The text content of an <see cref="ListItemNote">optional note</see> in the <see cref="ListItem">list item</see></param>
    [Obsolete($"Use `{nameof(CreateItemAsync)}` instead")]
    public Task<ListItem> CreateListItemAsync(Guid channel, string message, string? note = null) =>
        CreateItemAsync(channel, message, note);

    /// <summary>
    /// Edits the <paramref name="message">text contents</paramref> of the specified <paramref name="listItem">list item</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="listItem">The identifier of the <see cref="ListItem">list item</see> to edit</param>
    /// <param name="message">The new text content of the <see cref="ListItem">list item</see></param>
    /// <param name="note">The new text content of the note in the <see cref="ListItem">list item</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ListPermissions.ViewListItems" />
    /// <permission cref="ListPermissions.ManageListItems">Required when updating <see cref="ListItem">list items</see> the <see cref="BaseGuildedClient">client</see> doesn't own</permission>
    /// <permission cref="GeneralPermissions.MentionEveryoneHere">Required when adding an <c>@everyone</c> or a <c>@here</c> mention to <see cref="ListItem">a list item</see></permission>
    /// <returns>The <paramref name="listItem">list item</paramref> that was updated by the <see cref="BaseGuildedClient">client</see></returns>
    public abstract Task<ListItem> UpdateItemAsync(Guid channel, Guid listItem, string message, string? note = null);

    /// <inheritdoc cref="UpdateItemAsync(Guid, Guid, string, string)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="listItem">The identifier of the <see cref="ListItem">list item</see> to edit</param>
    /// <param name="message">The new text content of the <see cref="ListItem">list item</see></param>
    /// <param name="note">The new text content of the note in the <see cref="ListItem">list item</see></param>
    [Obsolete($"Use `{nameof(UpdateItemAsync)}` instead")]
    public Task<ListItem> UpdateListItemAsync(Guid channel, Guid listItem, string message, string? note = null) =>
        UpdateItemAsync(channel, listItem, message, note);

    /// <summary>
    /// Deletes the specified <paramref name="listItem">list item</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the channel</see> where the <see cref="ListItem">list item</see> is</param>
    /// <param name="listItem">The identifier of the <see cref="ListItem">list item</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ListPermissions.ViewListItems" />
    /// <permission cref="ListPermissions.RemoveListItems">Required when deleting <see cref="ListItem">list items</see> you don't own</permission>
    public abstract Task DeleteItemAsync(Guid channel, Guid listItem);

    /// <inheritdoc cref="DeleteItemAsync(Guid, Guid)" />
    /// <param name="channel">The identifier of <see cref="ServerChannel">the channel</see> where the <see cref="ListItem">list item</see> is</param>
    /// <param name="listItem">The identifier of the <see cref="ListItem">list item</see> to delete</param>
    [Obsolete($"Use `{nameof(DeleteItemAsync)}` instead")]
    public Task DeleteListItemAsync(Guid channel, Guid listItem) =>
        DeleteItemAsync(channel, listItem);

    /// <summary>
    /// Marks the specified <paramref name="listItem">list item</paramref> as <see cref="ListItemBase{T}.IsCompleted">completed</see>.
    /// </summary>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the channel</see> where the <see cref="ListItem">list item</see> is</param>
    /// <param name="listItem">The identifier of the <see cref="ListItem">list item</see> to complete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ListPermissions.ViewListItems" />
    /// <permission cref="ListPermissions.CompleteListItems" />
    public abstract Task CompleteItemAsync(Guid channel, Guid listItem);

    /// <inheritdoc cref="CompleteItemAsync(Guid, Guid)" />
    /// <param name="channel">The identifier of <see cref="ServerChannel">the channel</see> where the <see cref="ListItem">list item</see> is</param>
    /// <param name="listItem">The identifier of the <see cref="ListItem">list item</see> to complete</param>
    [Obsolete($"Use `{nameof(CompleteItemAsync)}` instead")]
    public Task CompleteListItemAsync(Guid channel, Guid listItem) =>
        DeleteItemAsync(channel, listItem);

    /// <summary>
    /// Marks the specified <paramref name="listItem">list item</paramref> as <see cref="ListItemBase{T}.IsCompleted">not completed</see>.
    /// </summary>
    /// <param name="channel">The identifier of <see cref="ServerChannel">the channel</see> where the list item is</param>
    /// <param name="listItem">The identifier of the <see cref="ListItem">list item</see> to complete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ListPermissions.ViewListItems" />
    /// <permission cref="ListPermissions.CompleteListItems" />
    public abstract Task UncompleteItemAsync(Guid channel, Guid listItem);

    /// <inheritdoc cref="UncompleteItemAsync(Guid, Guid)" />
    /// <param name="channel">The identifier of <see cref="ServerChannel">the channel</see> where the <see cref="ListItem">list item</see> is</param>
    /// <param name="listItem">The identifier of the <see cref="ListItem">list item</see> to complete</param>
    [Obsolete($"Use `{nameof(UncompleteItemAsync)}` instead")]
    public Task UncompleteListItemAsync(Guid channel, Guid listItem) =>
        DeleteItemAsync(channel, listItem);
    #endregion

    #region Methods Docs channel
    /// <summary>
    /// Gets a list of <see cref="Doc">documents</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="limit">The limit of how many <see cref="Doc">documents</see> to get (default — <c>25</c>, values — <c>(0, 100]</c>)</param>
    /// <param name="before">The max limit of the creation date of <see cref="Doc">fetched documents</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="DocPermissions.ViewDocs" />
    /// <returns>The list of fetched <see cref="Doc">documents</see> in the specified <paramref name="channel" /></returns>
    public abstract Task<IList<Doc>> GetDocsAsync(Guid channel, uint? limit = null, DateTime? before = null);

    /// <summary>
    /// Gets the <paramref name="doc">specified document</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
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
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="title">The title of <see cref="Doc">the document</see></param>
    /// <param name="content">The Markdown content of <see cref="Doc">the document</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="DocPermissions.ViewDocs" />
    /// <permission cref="DocPermissions.CreateDocs" />
    /// <permission cref="GeneralPermissions.MentionEveryoneHere">Required when posting <see cref="Doc">a document</see> that contains an <c>@everyone</c> or <c>@here</c> mentions</permission>
    /// <returns>The <see cref="Doc">document</see> that was created by the <see cref="BaseGuildedClient">client</see></returns>
    public abstract Task<Doc> CreateDocAsync(Guid channel, string title, string content);

    /// <summary>
    /// Edits <paramref name="content">the text contents</paramref> or <paramref name="title">the title</paramref> of the <paramref name="doc">specified document</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>The <paramref name="doc">updated document</paramref> will be bumped to the top.</para>
    /// </remarks>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="doc">The identifier of the document to update/edit</param>
    /// <param name="title">The new title of this document</param>
    /// <param name="content">The new Markdown content of this document</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="DocPermissions.ViewDocs" />
    /// <permission cref="DocPermissions.ManageDocs">Required when editing <see cref="Doc">documents</see> that the <see cref="BaseGuildedClient">client</see> doesn't own</permission>
    /// <permission cref="GeneralPermissions.MentionEveryoneHere">Required when adding an <c>@everyone</c> or a <c>@here</c> mention to <see cref="Doc">a document</see></permission>
    /// <returns>The <see cref="Doc">document</see> that was updated by the <see cref="BaseGuildedClient">client</see></returns>
    public abstract Task<Doc> UpdateDocAsync(Guid channel, uint doc, string title, string content);

    /// <summary>
    /// Deletes the <paramref name="doc">specified document</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="doc">The identifier of <see cref="Doc">the document</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="DocPermissions.ViewDocs" />
    /// <permission cref="DocPermissions.RemoveDocs">Required when deleting <see cref="Doc">documents</see> that the <see cref="BaseGuildedClient">client</see> doesn't own</permission>
    public abstract Task DeleteDocAsync(Guid channel, uint doc);
    #endregion

    #region Methods Calendar channel

    #region Methods Calendar channel > Event
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
    /// <permission cref="CalendarPermissions.ViewEvents" />
    /// <returns>The list of fetched <see cref="CalendarEvent">calendar events</see> in the specified <paramref name="channel" /></returns>
    public abstract Task<IList<CalendarEvent>> GetEventsAsync(Guid channel, uint? limit = null, DateTime? before = null);

    /// <summary>
    /// Gets the specified <paramref name="calendarEvent">calendar event</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="CalendarPermissions.ViewEvents" />
    /// <returns>Specified <see cref="CalendarEvent">calendar event</see></returns>
    public abstract Task<CalendarEvent> GetEventAsync(Guid channel, uint calendarEvent);

    /// <summary>
    /// Creates a <see cref="CalendarEvent">new calendar event</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="name">The title of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="description">The description of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="location">The physical or non-physical location of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="url">The URL to <see cref="CalendarEvent">the calendar event's</see> services, place or anything related</param>
    /// <param name="color">The colour of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="duration">The duration of the <see cref="CalendarEvent">calendar event</see> in minutes</param>
    /// <param name="rsvpLimit">The limit of how many <see cref="Users.User">users</see> can be invited or attend the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="isPrivate">Whether the <see cref="CalendarEvent">calendar event</see> is private</param>
    /// <param name="startsAt">The date when the <see cref="CalendarEvent">calendar event</see> starts</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="CalendarPermissions.ViewEvents" />
    /// <permission cref="CalendarPermissions.CreateEvents" />
    /// <permission cref="GeneralPermissions.MentionEveryoneHere">Required when adding an <c>@everyone</c> or a <c>@here</c> mention to <see cref="CalendarEvent.Description">the calendar event's description</see></permission>
    /// <returns>The <see cref="CalendarEvent">calendar event</see> that was created by the <see cref="BaseGuildedClient">client</see></returns>
    public abstract Task<CalendarEvent> CreateEventAsync(Guid channel, string name, string? description = null, string? location = null, DateTime? startsAt = null, Uri? url = null, Color? color = null, uint? duration = null, uint? rsvpLimit = null, bool isPrivate = false);

    /// <inheritdoc cref="CreateEventAsync(Guid, string, string, string, DateTime?, Uri?, Color?, uint?, uint?, bool)" />
    public Task<CalendarEvent> CreateEventAsync(Guid channel, string name, string? description = null, string? location = null, DateTime? startsAt = null, Uri? url = null, Color? color = null, TimeSpan? duration = null, uint? rsvpLimit = null, bool isPrivate = false) =>
        CreateEventAsync(channel, name, description, location, startsAt, url, color, (uint?)duration?.TotalMinutes, rsvpLimit, isPrivate);

    /// <summary>
    /// Edits the specified <paramref name="calendarEvent">calendar event</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> to update/edit</param>
    /// <param name="name">The new name of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="description">The new description of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="location">The new location of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="startsAt">The new starting date of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="url">The new URL of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="color">The new colour of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="duration">The new length/duration of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="isPrivate">Whether the <see cref="CalendarEvent">calendar event</see> is now private or not private anymore</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="CalendarPermissions.ViewEvents" />
    /// <permission cref="CalendarPermissions.ManageEvents">Required when editing <see cref="CalendarEvent">calendar events</see> that the <see cref="BaseGuildedClient">client</see> doesn't own</permission>
    /// <permission cref="GeneralPermissions.MentionEveryoneHere">Required when adding an <c>@everyone</c> or a <c>@here</c> mention to <see cref="CalendarEvent.Description">the calendar event's description</see></permission>
    /// <returns>The <see cref="CalendarEvent">calendar event</see> that was updated by the <see cref="BaseGuildedClient">client</see></returns>
    public abstract Task<CalendarEvent> UpdateEventAsync(Guid channel, uint calendarEvent, string? name = null, string? description = null, string? location = null, DateTime? startsAt = null, Uri? url = null, Color? color = null, uint? duration = null, bool? isPrivate = null);

    /// <inheritdoc cref="UpdateEventAsync(Guid, uint, string, string, string, DateTime?, Uri?, Color?, uint?, bool?)" />
    public Task<CalendarEvent> UpdateEventAsync(Guid channel, uint calendarEvent, string? name = null, string? description = null, string? location = null, DateTime? startsAt = null, Uri? url = null, Color? color = null, TimeSpan? duration = null, bool? isPrivate = null) =>
        UpdateEventAsync(channel, calendarEvent, name, description, location, startsAt, url, color, (uint?)duration?.TotalMinutes, isPrivate);

    /// <summary>
    /// Deletes the specified <paramref name="calendarEvent">calendar event</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="CalendarPermissions.ViewEvents" />
    /// <permission cref="CalendarPermissions.RemoveEvents">Required when deleting <see cref="CalendarEvent">calendar event</see> that the <see cref="BaseGuildedClient">client</see> doesn't own</permission>
    public abstract Task DeleteEventAsync(Guid channel, uint calendarEvent);
    #endregion

    #region Methods Calendar channel > Rsvp
    /// <summary>
    /// Gets a list of <see cref="CalendarEvent">calendar events</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> to get <see cref="CalendarRsvp">RSVPs</see> of</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="CalendarPermissions.ViewEvents" />
    /// <returns>The list of fetched <see cref="CalendarRsvp">calendar event RSVPs</see> in the specified <paramref name="channel" /></returns>
    public abstract Task<IList<CalendarRsvp>> GetRsvpsAsync(Guid channel, uint calendarEvent);

    /// <summary>
    /// Gets the specified <paramref name="calendarEvent">calendar event</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> where the <see cref="CalendarRsvp">RSVP</see> is</param>
    /// <param name="user">The identifier of <see cref="Users.User">the user</see> to get <see cref="CalendarRsvp">RSVP</see> of</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="CalendarPermissions.ViewEvents" />
    /// <returns>Specified <see cref="CalendarRsvp">calendar event RSVP</see></returns>
    public abstract Task<CalendarRsvp> GetRsvpAsync(Guid channel, uint calendarEvent, HashId user);

    /// <summary>
    /// Creates or edits a <see cref="CalendarEvent">calendar event</see> <see cref="CalendarRsvp">RSVP</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> where the <see cref="CalendarRsvp">RSVP</see> is</param>
    /// <param name="user">The identifier of <see cref="Users.User">the user</see> to set <see cref="CalendarRsvp">RSVP</see> of</param>
    /// <param name="status">The status of <see cref="CalendarEvent">the RSVP</see> to set</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="CalendarPermissions.ViewEvents" />
    /// <permission cref="CalendarPermissions.EditRSVPs">Required when setting <see cref="CalendarRsvp">calendar event RSVPs</see> that aren't for the <see cref="BaseGuildedClient">client</see></permission>
    /// <returns>Set <see cref="CalendarRsvp">calendar event RSVP</see></returns>
    public abstract Task<CalendarRsvp> SetRsvpAsync(Guid channel, uint calendarEvent, HashId user, CalendarRsvpStatus status);

    /// <summary>
    /// Deletes the specified <see cref="CalendarRsvp">calendar event RSVP</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> where <see cref="CalendarRsvp">the RSVP</see> is</param>
    /// <param name="user">The identifier of <see cref="Users.User">the user</see> to remove <see cref="CalendarRsvp">RSVP</see> of</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="CalendarPermissions.ViewEvents" />
    /// <permission cref="CalendarPermissions.EditRSVPs">Required when removing <see cref="CalendarRsvp">calendar event RSVPs</see> that aren't for the <see cref="BaseGuildedClient">client</see></permission>
    public abstract Task RemoveRsvpAsync(Guid channel, uint calendarEvent, HashId user);
    #endregion

    #endregion

    #region Methods Any Content
    /// <summary>
    /// Adds <paramref name="emote">a reaction</paramref> to the <paramref name="content">specified content</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="content">The identifier of the <see cref="ChannelContent{TId, TServer}">content</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to add</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="DocPermissions.ViewDocs" />
    /// <permission cref="MediaPermissions.SeeMedia" />
    /// <permission cref="ForumPermissions.ReadForums" />
    /// <permission cref="CalendarPermissions.ViewEvents" />
    public abstract Task AddReactionAsync(Guid channel, uint content, uint emote);

    /// <inheritdoc cref="AddReactionAsync(Guid, uint, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="content">The identifier of the <see cref="ChannelContent{TId, TServer}">content</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to add</param>
    public Task AddReactionAsync(Guid channel, uint content, Emote emote) =>
        AddReactionAsync(channel, content, emote.Id);

    /// <summary>
    /// Removes <paramref name="emote">a reaction</paramref> from the <paramref name="content">specified content</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="content">The identifier of the <see cref="ChannelContent{TId, TServer}">content</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to remove</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="DocPermissions.ViewDocs" />
    /// <permission cref="MediaPermissions.SeeMedia" />
    /// <permission cref="ForumPermissions.ReadForums" />
    /// <permission cref="CalendarPermissions.ViewEvents" />
    public abstract Task RemoveReactionAsync(Guid channel, uint content, uint emote);

    /// <inheritdoc cref="RemoveReactionAsync(Guid, uint, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="content">The identifier of the <see cref="ChannelContent{TId, TServer}">content</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to remove</param>
    public Task RemoveReactionAsync(Guid channel, uint content, Emote emote) =>
        RemoveReactionAsync(channel, content, emote.Id);
    #endregion
}