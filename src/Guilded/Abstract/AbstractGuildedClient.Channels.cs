using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Base.Embeds;
using Guilded.Content;
using Guilded.Permissions;
using Guilded.Servers;
using Guilded.Users;
using Newtonsoft.Json.Linq;
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
    /// <permission cref="ChatPermissions.GetMessage" />
    /// <permission cref="GeneralPermissions.GetPrivateMessage">Required when viewing <see cref="Message">messages</see> set as <see cref="Message.IsPrivate">private</see> not sent by the <see cref="AbstractGuildedClient">client</see> if <paramref name="includePrivate" /> is set as true</permission>
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
    /// <permission cref="ChatPermissions.GetMessage" />
    /// <permission cref="GeneralPermissions.GetPrivateMessage">Required when viewing <see cref="Message">messages</see> set as <see cref="Message.IsPrivate">private</see> not sent by the <see cref="AbstractGuildedClient">client</see></permission>
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
    /// <permission cref="ChatPermissions.GetMessage" />
    /// <permission cref="ChatPermissions.CreateMessage">Required when sending a <see cref="Message">message</see> in <see cref="ServerChannel">a top-most channel</see></permission>
    /// <permission cref="ChatPermissions.CreateThreadMessage">Required when sending a <see cref="Message">message</see> in <see cref="ServerChannel">a thread</see></permission>
    /// <permission cref="ChatPermissions.CreatePrivateMessage">Required when sending a <see cref="Message">message</see> that is set as <see cref="Message.IsPrivate">private</see></permission>
    /// <permission cref="ChatPermissions.AddMedia">Required when sending a <see cref="Message">message</see> that contains an image or a video</permission>
    /// <permission cref="GeneralPermissions.AddEveryoneMention">Required when sending a <see cref="Message">message</see> that contains an <c>@everyone</c> or <c>@here</c> mentions</permission>
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
    /// <param name="isPrivate">Whether the mention is private</param>
    /// <param name="isSilent">Whether the mention is silent and does not ping anyone</param>
    /// <exception cref="ArgumentNullException">When the <paramref name="content" /> only consists of whitespace or is <see langword="null" /></exception>
    /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="content" /> is above the message limit of 4000 characters</exception>
    /// <returns>The <see cref="Message">message</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Message> CreateMessageAsync(Guid channel, string? content = null, IList<Embed>? embeds = null, IList<Guid>? replyTo = null, bool isPrivate = false, bool isSilent = false) =>
        CreateMessageAsync(channel, new MessageContent(content, embeds, replyTo, isPrivate, isSilent));

    /// <inheritdoc cref="CreateMessageAsync(Guid, string, IList{Embed}?, IList{Guid}?, bool, bool)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="replyTo">The list of all <see cref="Message">messages</see> it is replying to (max — <c>5</c>)</param>
    /// <param name="isPrivate">Whether the mention is private</param>
    /// <param name="isSilent">Whether the mention is silent and does not ping anyone</param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task<Message> CreateMessageAsync(Guid channel, string? content = null, IList<Guid>? replyTo = null, bool isPrivate = false, bool isSilent = false, params Embed[] embeds) =>
        CreateMessageAsync(channel, new MessageContent(content, embeds, replyTo, isPrivate, isSilent));

    /// <inheritdoc cref="CreateMessageAsync(Guid, string, IList{Embed}?, IList{Guid}?, bool, bool)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="embeds">The list of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    /// <param name="isPrivate">Whether the mention is private</param>
    /// <param name="isSilent">Whether the mention is silent and does not ping anyone</param>
    /// <param name="replyTo">The array of all <see cref="Message">messages</see> it is replying to (max — <c>5</c>)</param>
    public Task<Message> CreateMessageAsync(Guid channel, string? content = null, IList<Embed>? embeds = null, bool isPrivate = false, bool isSilent = false, params Guid[] replyTo) =>
        CreateMessageAsync(channel, new MessageContent(content, embeds, replyTo, isPrivate, isSilent));

    /// <inheritdoc cref="CreateMessageAsync(Guid, string, IList{Embed}?, IList{Guid}?, bool, bool)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task<Message> CreateMessageAsync(Guid channel, string content, params Embed[] embeds) =>
        CreateMessageAsync(channel, new MessageContent(content, embeds));

    /// <inheritdoc cref="CreateMessageAsync(Guid, string, IList{Embed}?, IList{Guid}?, bool, bool)" />
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
    /// <permission cref="ChatPermissions.GetMessage" />
    /// <permission cref="ChatPermissions.CreateMessage">Required when editing a <see cref="Message">message</see> in <see cref="ServerChannel">a top-most channel</see></permission>
    /// <permission cref="ChatPermissions.CreateThreadMessage">Required when editing a <see cref="Message">message</see> in <see cref="ServerChannel">a thread</see></permission>
    /// <permission cref="ChatPermissions.AddMedia">Required when adding an image or a video to a <see cref="Message">message</see></permission>
    /// <permission cref="GeneralPermissions.AddEveryoneMention">Required when adding an <c>@everyone</c> or a <c>@here</c> mention to a <see cref="Message">message</see></permission>
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
    public Task<Message> UpdateMessageAsync(Guid channel, Guid message, string? content = null, IList<Embed>? embeds = null) =>
        UpdateMessageAsync(channel, message, new MessageContent(content, embeds));

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
    /// <permission cref="ChatPermissions.GetMessage" />
    /// <permission cref="ChatPermissions.ManageMessage">Required when deleting messages made by others</permission>
    /// <permission cref="GeneralPermissions.GetPrivateMessage">Required for deleting messages set as <see cref="Message.IsPrivate">private</see> made by others</permission>
    public Task DeleteMessageAsync(Guid channel, Guid message) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/messages/{message}", Method.Delete));
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
    /// <permission cref="ForumPermissions.GetTopic" />
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
    /// <permission cref="ForumPermissions.GetTopic" />
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
    /// <permission cref="ForumPermissions.GetTopic" />
    /// <permission cref="ForumPermissions.CreateTopic" />
    /// <permission cref="GeneralPermissions.AddEveryoneMention">Required when posting a <see cref="Topic">forum topic</see> that contains an <c>@everyone</c> or <c>@here</c> mentions</permission>
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
    /// <permission cref="ForumPermissions.GetTopic" />
    /// <permission cref="ForumPermissions.CreateTopic" />
    /// <permission cref="GeneralPermissions.AddEveryoneMention">Required when adding an <c>@everyone</c> or <c>@here</c> mentions</permission>
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
    /// <permission cref="ForumPermissions.GetTopic" />
    /// <permission cref="ForumPermissions.ManageTopic">Required when deleting <see cref="Topic">forum topic</see> that the <see cref="AbstractGuildedClient">client</see> doesn't own</permission>
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
    /// <permission cref="ForumPermissions.GetTopic" />
    /// <permission cref="ForumPermissions.ManageTopic" />
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
    /// <permission cref="ForumPermissions.GetTopic" />
    /// <permission cref="ForumPermissions.ManageTopic" />
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
    /// <permission cref="ForumPermissions.GetTopic" />
    /// <permission cref="ForumPermissions.LockTopic" />
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
    /// <permission cref="ForumPermissions.GetTopic" />
    /// <permission cref="ForumPermissions.LockTopic" />
    public Task UnlockTopicAsync(Guid channel, uint topic) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/topics/{topic}/lock", Method.Delete));
    #endregion

    #region Methods Forum channels > Topic replies
    /// <summary>
    /// Gets a list of <see cref="TopicComment">forum topic comments</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to get <see cref="TopicComment">forum topic replies</see> of</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ForumPermissions.GetTopic" />
    /// <returns>The list of fetched <see cref="TopicComment">forum topic replies</see> in the specified <paramref name="topic" /></returns>
    public Task<IList<TopicComment>> GetTopicCommentsAsync(Guid channel, uint topic) =>
        GetResponsePropertyAsync<IList<TopicComment>>(new RestRequest($"channels/{channel}/topics/{topic}/comments", Method.Get), "forumTopicComments");

    /// <summary>
    /// Gets the <paramref name="topicComment">specified forum topic reply</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> where the <see cref="TopicComment">forum topic comment</see> is</param>
    /// <param name="topicComment">The identifier of the <see cref="TopicComment">forum topic comment</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ForumPermissions.GetTopic" />
    /// <returns>The <see cref="TopicComment">forum topic reply</see> that was specified in the arguments</returns>
    public Task<TopicComment> GetTopicCommentAsync(Guid channel, uint topic, uint topicComment) =>
        GetResponsePropertyAsync<TopicComment>(new RestRequest($"channels/{channel}/topics/{topic}/comments/{topicComment}", Method.Get), "forumTopicComment");

    /// <summary>
    /// Creates a new <see cref="Topic">forum topic</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> where the <see cref="TopicComment">forum topic comment</see> should be</param>
    /// <param name="content">The content of the <see cref="TopicComment">forum topic comment</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ForumPermissions.GetTopic" />
    /// <permission cref="ForumPermissions.CreateTopicComment" />
    /// <permission cref="GeneralPermissions.AddEveryoneMention">Required when posting a <see cref="Topic">forum topic</see> that contains an <c>@everyone</c> or <c>@here</c> mentions</permission>
    /// <returns>The <see cref="TopicComment">forum topic comment</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<TopicComment> CreateTopicCommentAsync(Guid channel, uint topic, string content) =>
        string.IsNullOrWhiteSpace(content)
        ? throw new ArgumentNullException(nameof(content))
        : GetResponsePropertyAsync<TopicComment>(new RestRequest($"channels/{channel}/topics/{topic}/comments", Method.Post).AddJsonBody(new { content }), "forumTopicComment");

    /// <summary>
    /// Edits <see cref="TopicComment">forum topic comment's</see> <paramref name="content" />.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> where the <see cref="TopicComment">forum topic comment</see> is</param>
    /// <param name="topicComment">The identifier of the <see cref="TopicComment">forum topic comment</see> to update</param>
    /// <param name="content">The new contents of the <see cref="TopicComment">forum topic comment</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ForumPermissions.GetTopic" />
    /// <permission cref="ForumPermissions.CreateTopic" />
    /// <permission cref="GeneralPermissions.AddEveryoneMention">Required when adding an <c>@everyone</c> or <c>@here</c> mentions</permission>
    /// <returns>The <paramref name="topic">forum topic</paramref> that was updated by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<TopicComment> UpdateTopicCommentAsync(Guid channel, uint topic, uint topicComment, string content) =>
        string.IsNullOrWhiteSpace(content)
        ? throw new ArgumentNullException(nameof(content))
        : GetResponsePropertyAsync<TopicComment>(new RestRequest($"channels/{channel}/topics/{topic}/comments/{topicComment}", Method.Patch).AddJsonBody(new { content }), "forumTopicComment");

    /// <summary>
    /// Deletes a <see cref="TopicComment">forum topic comment</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> where the <see cref="TopicComment">forum topic comment</see> is</param>
    /// <param name="topicComment">The identifier of the <see cref="TopicComment">forum topic comment</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ForumPermissions.GetTopic" />
    /// <permission cref="ForumPermissions.ManageTopic">Required when deleting <see cref="Topic">forum topic</see> that the <see cref="AbstractGuildedClient">client</see> doesn't own</permission>
    public Task DeleteTopicCommentAsync(Guid channel, uint topic, uint topicComment) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/topics/{topic}/comments/{topicComment}", Method.Delete));
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
    /// <permission cref="ListPermissions.GetItem" />
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
    /// <permission cref="ListPermissions.GetItem" />
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
    /// <permission cref="ListPermissions.GetItem" />
    /// <permission cref="ListPermissions.CreateItem" />
    /// <permission cref="GeneralPermissions.AddEveryoneMention">Required when posting <see cref="Item">a list item</see> that contains an <c>@everyone</c> or <c>@here</c> mentions</permission>
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
    /// <permission cref="ListPermissions.GetItem" />
    /// <permission cref="ListPermissions.ManageItem">Required when updating <see cref="Item">list items</see> the <see cref="AbstractGuildedClient">client</see> doesn't own</permission>
    /// <permission cref="GeneralPermissions.AddEveryoneMention">Required when adding an <c>@everyone</c> or a <c>@here</c> mention to <see cref="Item">a list item</see></permission>
    /// <returns>The <paramref name="listItem">list item</paramref> that was updated by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Item> UpdateItemAsync(Guid channel, Guid listItem, string message, string? note = null) =>
        string.IsNullOrWhiteSpace(message) && string.IsNullOrEmpty(note)
        ? throw new ArgumentNullException(nameof(message), "Either the message or the note of the list item's update must be specified")
        : GetResponsePropertyAsync<Item>(new RestRequest($"channels/{channel}/items/{listItem}", Method.Put)
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
    /// Deletes the specified <paramref name="listItem">list item</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the <see cref="ServerChannel">channel</see> where the <see cref="Item">list item</see> is</param>
    /// <param name="listItem">The identifier of the <see cref="Item">list item</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ListPermissions.GetItem" />
    /// <permission cref="ListPermissions.RemoveItem">Required when deleting <see cref="Item">list items</see> you don't own</permission>
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
    /// <permission cref="ListPermissions.GetItem" />
    /// <permission cref="ListPermissions.CompleteItem" />
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
    /// <permission cref="ListPermissions.GetItem" />
    /// <permission cref="ListPermissions.CompleteItem" />
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
    /// <permission cref="DocPermissions.GetDoc" />
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
    /// <permission cref="DocPermissions.GetDoc" />
    /// <returns>The <see cref="Doc">document</see> that was specified in the arguments</returns>
    public Task<Doc> GetDocAsync(Guid channel, uint doc) =>
        GetResponsePropertyAsync<Doc>(new RestRequest($"channels/{channel}/docs/{doc}", Method.Get), "doc");

    /// <summary>
    /// Creates a <see cref="Doc">new document</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="title">The title of the <see cref="Doc">document</see></param>
    /// <param name="content">The Markdown content of the <see cref="Doc">document</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="DocPermissions.GetDoc" />
    /// <permission cref="DocPermissions.CreateDoc" />
    /// <permission cref="GeneralPermissions.AddEveryoneMention">Required when posting <see cref="Doc">a document</see> that contains an <c>@everyone</c> or <c>@here</c> mentions</permission>
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
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="doc">The identifier of the <see cref="Doc">document</see> to update/edit</param>
    /// <param name="title">The new title of this <see cref="Doc">document</see></param>
    /// <param name="content">The new Markdown content of this <see cref="Doc">document</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="DocPermissions.GetDoc" />
    /// <permission cref="DocPermissions.ManageDoc">Required when editing <see cref="Doc">documents</see> that the <see cref="AbstractGuildedClient">client</see> doesn't own</permission>
    /// <permission cref="GeneralPermissions.AddEveryoneMention">Required when adding an <c>@everyone</c> or a <c>@here</c> mention to <see cref="Doc">a document</see></permission>
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
    /// <permission cref="DocPermissions.GetDoc" />
    /// <permission cref="DocPermissions.RemoveDoc">Required when deleting <see cref="Doc">documents</see> that the <see cref="AbstractGuildedClient">client</see> doesn't own</permission>
    public Task DeleteDocAsync(Guid channel, uint doc) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/docs/{doc}", Method.Delete));
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
    /// <permission cref="CalendarPermissions.GetEvent" />
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
    /// <permission cref="CalendarPermissions.GetEvent" />
    /// <returns>The <see cref="CalendarEvent">calendar event</see> that was specified in the arguments</returns>
    public Task<CalendarEvent> GetEventAsync(Guid channel, uint calendarEvent) =>
        GetResponsePropertyAsync<CalendarEvent>(new RestRequest($"channels/{channel}/events/{calendarEvent}", Method.Get), "calendarEvent");

    /// <summary>
    /// Creates a <see cref="CalendarEvent">new calendar event</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="name">The title of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="description">The description of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="location">The physical or non-physical location of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="url">The URL to the <see cref="CalendarEvent">calendar event's</see> services, place or anything related</param>
    /// <param name="color">The colour of the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="duration">The duration of the <see cref="CalendarEvent">calendar event</see> in minutes</param>
    /// <param name="rsvpLimit">The limit of how many <see cref="Users.User">users</see> can be invited or attend the <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="isPrivate">Whether the <see cref="CalendarEvent">calendar event</see> is private</param>
    /// <param name="startsAt">The date when the <see cref="CalendarEvent">calendar event</see> starts</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="CalendarPermissions.GetEvent" />
    /// <permission cref="CalendarPermissions.CreateEvent" />
    /// <permission cref="GeneralPermissions.AddEveryoneMention">Required when adding an <c>@everyone</c> or a <c>@here</c> mention to the <see cref="CalendarEvent.Description">calendar event's description</see></permission>
    /// <returns>The <see cref="CalendarEvent">calendar event</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<CalendarEvent> CreateEventAsync(Guid channel, string name, string? description = null, string? location = null, DateTime? startsAt = null, Uri? url = null, Color? color = null, uint? duration = null, uint? rsvpLimit = null, bool isPrivate = false) =>
        string.IsNullOrWhiteSpace(name)
        ? throw new ArgumentNullException(nameof(name))
        : GetResponsePropertyAsync<CalendarEvent>(new RestRequest($"channels/{channel}/events", Method.Post)
            .AddJsonBody(new
            {
                name,
                description,
                location,
                startsAt,
                url,
                color,
                duration,
                rsvpLimit,
                isPrivate
            })
        , "calendarEvent");

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
    /// <permission cref="CalendarPermissions.GetEvent" />
    /// <permission cref="CalendarPermissions.ManageEvent">Required when editing <see cref="CalendarEvent">calendar events</see> that the <see cref="AbstractGuildedClient">client</see> doesn't own</permission>
    /// <permission cref="GeneralPermissions.AddEveryoneMention">Required when adding an <c>@everyone</c> or a <c>@here</c> mention to the <see cref="CalendarEvent.Description">calendar event's description</see></permission>
    /// <returns>The <see cref="CalendarEvent">calendar event</see> that was updated by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<CalendarEvent> UpdateEventAsync(Guid channel, uint calendarEvent, string? name = null, string? description = null, string? location = null, DateTime? startsAt = null, Uri? url = null, Color? color = null, uint? duration = null, bool? isPrivate = null) =>
        GetResponsePropertyAsync<CalendarEvent>(new RestRequest($"channels/{channel}/events/{calendarEvent}", Method.Patch)
            .AddJsonBody(new
            {
                name,
                description,
                location,
                startsAt,
                url,
                color,
                duration,
                isPrivate
            })
        , "calendarEvent");

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
    /// <permission cref="CalendarPermissions.GetEvent" />
    /// <permission cref="CalendarPermissions.RemoveEvent">Required when deleting <see cref="CalendarEvent">calendar event</see> that the <see cref="AbstractGuildedClient">client</see> doesn't own</permission>
    public Task DeleteEventAsync(Guid channel, uint calendarEvent) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/events/{calendarEvent}", Method.Delete));
    #endregion

    #region Methods Calendar channels > Rsvp
    /// <summary>
    /// Gets a list of <see cref="CalendarEvent">calendar events</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> to get <see cref="CalendarRsvp">RSVPs</see> of</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="CalendarPermissions.GetEvent" />
    /// <returns>The list of fetched <see cref="CalendarRsvp">calendar event RSVPs</see> in the specified <paramref name="channel" /></returns>
    public Task<IList<CalendarRsvp>> GetRsvpsAsync(Guid channel, uint calendarEvent) =>
        GetResponsePropertyAsync<IList<CalendarRsvp>>(new RestRequest($"channels/{channel}/events/{calendarEvent}/rsvps", Method.Get), "calendarEventRsvps");

    /// <summary>
    /// Gets the specified <paramref name="calendarEvent">calendar event</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> where the <see cref="CalendarRsvp">RSVP</see> is</param>
    /// <param name="user">The identifier of the <see cref="User">user</see> to get <see cref="CalendarRsvp">RSVP</see> of</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="CalendarPermissions.GetEvent" />
    /// <returns>The <see cref="CalendarRsvp">calendar event RSVP</see> that was specified in the arguments</returns>
    public Task<CalendarRsvp> GetRsvpAsync(Guid channel, uint calendarEvent, HashId user) =>
        GetResponsePropertyAsync<CalendarRsvp>(new RestRequest($"channels/{channel}/events/{calendarEvent}/rsvps/{user}", Method.Get), "calendarEventRsvp");

    /// <summary>
    /// Creates or edits a <see cref="CalendarEvent">calendar event</see> <see cref="CalendarRsvp">RSVP</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> where the <see cref="CalendarRsvp">RSVP</see> is</param>
    /// <param name="user">The identifier of the <see cref="User">user</see> to set <see cref="CalendarRsvp">RSVP</see> of</param>
    /// <param name="status">The status of the <see cref="CalendarEvent">calendar RSVP</see> to set</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="CalendarPermissions.GetEvent" />
    /// <permission cref="CalendarPermissions.ManageRsvp">Required when setting <see cref="CalendarRsvp">calendar event RSVPs</see> that aren't for the <see cref="AbstractGuildedClient">client</see></permission>
    /// <returns>Set <see cref="CalendarRsvp">calendar event RSVP</see></returns>
    public Task<CalendarRsvp> SetRsvpAsync(Guid channel, uint calendarEvent, HashId user, CalendarRsvpStatus status) =>
        GetResponsePropertyAsync<CalendarRsvp>(new RestRequest($"channels/{channel}/events/{calendarEvent}/rsvps/{user}", Method.Put)
            .AddJsonBody(new
            {
                status
            })
        , "calendarEventRsvp");

    /// <summary>
    /// Deletes the specified <see cref="CalendarRsvp">calendar event RSVP</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> where the <see cref="CalendarRsvp">calendar RSVP</see> is</param>
    /// <param name="user">The identifier of the <see cref="User">user</see> to remove <see cref="CalendarRsvp">RSVP</see> of</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="CalendarPermissions.GetEvent" />
    /// <permission cref="CalendarPermissions.ManageRsvp">Required when removing <see cref="CalendarRsvp">calendar event RSVPs</see> that aren't for the <see cref="AbstractGuildedClient">client</see></permission>
    public Task RemoveRsvpAsync(Guid channel, uint calendarEvent, HashId user) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/events/{calendarEvent}/rsvps/{user}", Method.Delete));
    #endregion

    #region Methods Reactions > Messages
    /// <summary>
    /// Adds an <paramref name="emote" /> as a reaction to the specified <paramref name="message" />.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to add</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ChatPermissions.GetMessage" />
    public Task AddMessageReactionAsync(Guid channel, Guid message, uint emote) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/content/{message}/emotes/{emote}", Method.Put));

    /// <inheritdoc cref="AddTopicReactionAsync(Guid, uint, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to add</param>
    public Task AddMessageReactionAsync(Guid channel, Guid message, Emote emote) =>
        AddMessageReactionAsync(channel, message, emote.Id);

    /// <summary>
    /// Removes an <paramref name="emote" /> as a reaction from the specified <paramref name="message" />.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to remove</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="ChatPermissions.GetMessage" />
    public Task RemoveMessageReactionAsync(Guid channel, Guid message, uint emote) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/content/{message}/emotes/{emote}", Method.Delete));

    /// <inheritdoc cref="RemoveTopicReactionAsync(Guid, uint, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to remove</param>
    public Task RemoveMessageReactionAsync(Guid channel, Guid message, Emote emote) =>
        RemoveMessageReactionAsync(channel, message, emote.Id);
    #endregion

    #region Methods Reactions > Topics
    /// <summary>
    /// Adds an <paramref name="emote" /> as a reaction to the specified <paramref name="topic">forum topic</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to add</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="DocPermissions.GetDoc" />
    /// <permission cref="MediaPermissions.GetMedia" />
    /// <permission cref="ForumPermissions.GetTopic" />
    /// <permission cref="CalendarPermissions.GetEvent" />
    public Task AddTopicReactionAsync(Guid channel, uint topic, uint emote) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/topics/{topic}/emotes/{emote}", Method.Put));

    /// <inheritdoc cref="AddTopicReactionAsync(Guid, uint, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to add</param>
    public Task AddTopicReactionAsync(Guid channel, uint topic, Emote emote) =>
        AddTopicReactionAsync(channel, topic, emote.Id);

    /// <summary>
    /// Removes an <paramref name="emote" /> as a reaction from the specified <paramref name="topic">forum topic</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to remove</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="DocPermissions.GetDoc" />
    /// <permission cref="MediaPermissions.GetMedia" />
    /// <permission cref="ForumPermissions.GetTopic" />
    /// <permission cref="CalendarPermissions.GetEvent" />
    public Task RemoveTopicReactionAsync(Guid channel, uint topic, uint emote) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/topics/{topic}/emotes/{emote}", Method.Delete));

    /// <inheritdoc cref="RemoveTopicReactionAsync(Guid, uint, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to remove</param>
    public Task RemoveTopicReactionAsync(Guid channel, uint topic, Emote emote) =>
        RemoveTopicReactionAsync(channel, topic, emote.Id);
    #endregion

    #region Methods Reactions > Vague
    /// <inheritdoc cref="AddMessageReactionAsync(Guid, Guid, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to add</param>
    public Task AddReactionAsync(Guid channel, Guid message, uint emote) =>
        AddMessageReactionAsync(channel, message, emote);

    /// <inheritdoc cref="AddMessageReactionAsync(Guid, Guid, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to add</param>
    public Task AddReactionAsync(Guid channel, Guid message, Emote emote) =>
        AddMessageReactionAsync(channel, message, emote);

    /// <inheritdoc cref="RemoveMessageReactionAsync(Guid, Guid, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to remove</param>
    public Task RemoveReactionAsync(Guid channel, Guid message, uint emote) =>
        RemoveMessageReactionAsync(channel, message, emote);

    /// <inheritdoc cref="RemoveMessageReactionAsync(Guid, Guid, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to remove</param>
    public Task RemoveReactionAsync(Guid channel, Guid message, Emote emote) =>
        RemoveMessageReactionAsync(channel, message, emote);

    /// <inheritdoc cref="AddMessageReactionAsync(Guid, Guid, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Message">message</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to add</param>
    public Task AddReactionAsync(Guid channel, uint topic, uint emote) =>
        AddTopicReactionAsync(channel, topic, emote);

    /// <inheritdoc cref="AddMessageReactionAsync(Guid, Guid, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to add</param>
    public Task AddReactionAsync(Guid channel, uint topic, Emote emote) =>
        AddTopicReactionAsync(channel, topic, emote);

    /// <inheritdoc cref="RemoveMessageReactionAsync(Guid, Guid, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to remove</param>
    public Task RemoveReactionAsync(Guid channel, uint topic, uint emote) =>
        RemoveTopicReactionAsync(channel, topic, emote);

    /// <inheritdoc cref="RemoveMessageReactionAsync(Guid, Guid, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to remove</param>
    public Task RemoveReactionAsync(Guid channel, uint topic, Emote emote) =>
        RemoveTopicReactionAsync(channel, topic, emote);
    #endregion
}
