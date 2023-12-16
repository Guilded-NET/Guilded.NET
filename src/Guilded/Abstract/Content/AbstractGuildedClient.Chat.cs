using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Base.Embeds;
using Guilded.Content;
using Guilded.Events;
using Guilded.Permissions;
using Guilded.Servers;
using Guilded.Users;
using RestSharp;

namespace Guilded.Client;

public abstract partial class AbstractGuildedClient
{
    #region Properties Chat channels
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a new <see cref="Message">message</see> is sent.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChatMessageCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MessageUpdated" />
    /// <seealso cref="MessageDeleted" />
    /// <seealso cref="MessagePinned" />
    /// <seealso cref="MessageUnpinned" />
    /// <seealso cref="MessageReactionAdded" />
    /// <seealso cref="MessageReactionRemoved" />
    public IObservable<MessageEvent> MessageCreated => ((IEventInfo<MessageEvent>)GuildedEvents["ChatMessageCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Message">message</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChatMessageUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MessageCreated" />
    /// <seealso cref="MessageDeleted" />
    /// <seealso cref="MessagePinned" />
    /// <seealso cref="MessageUnpinned" />
    /// <seealso cref="MessageReactionAdded" />
    /// <seealso cref="MessageReactionRemoved" />
    public IObservable<MessageEvent> MessageUpdated => ((IEventInfo<MessageEvent>)GuildedEvents["ChatMessageUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Message">message</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChatMessageDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MessageCreated" />
    /// <seealso cref="MessageUpdated" />
    /// <seealso cref="MessagePinned" />
    /// <seealso cref="MessageUnpinned" />
    /// <seealso cref="MessageReactionAdded" />
    /// <seealso cref="MessageReactionRemoved" />
    public IObservable<MessageDeletedEvent> MessageDeleted => ((IEventInfo<MessageDeletedEvent>)GuildedEvents["ChatMessageDeleted"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Message">message</see> pin is added.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChannelMessagePinned</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MessageCreated" />
    /// <seealso cref="MessageUpdated" />
    /// <seealso cref="MessageDeleted" />
    /// <seealso cref="MessageUnpinned" />
    /// <seealso cref="MessageReactionAdded" />
    /// <seealso cref="MessageReactionRemoved" />
    public IObservable<MessageEvent> MessagePinned => ((IEventInfo<MessageEvent>)GuildedEvents["ChannelMessagePinned"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Message">message</see> pin is removed.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChannelMessageUnpinned</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MessageCreated" />
    /// <seealso cref="MessageUpdated" />
    /// <seealso cref="MessageDeleted" />
    /// <seealso cref="MessagePinned" />
    /// <seealso cref="MessageReactionAdded" />
    /// <seealso cref="MessageReactionRemoved" />
    public IObservable<MessageEvent> MessageUnpinned => ((IEventInfo<MessageEvent>)GuildedEvents["ChannelMessageUnppinned"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Reaction">reaction</see> on a <see cref="Message">message</see> is added.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChannelMessageReactionCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MessageCreated" />
    /// <seealso cref="MessageUpdated" />
    /// <seealso cref="MessageDeleted" />
    /// <seealso cref="MessageReactionRemoved" />
    public IObservable<MessageReactionEvent> MessageReactionAdded => ((IEventInfo<MessageReactionEvent>)GuildedEvents["ChannelMessageReactionCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Reaction">reaction</see> on a <see cref="Message">message</see> is removed.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ChannelMessageReactionDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MessageCreated" />
    /// <seealso cref="MessageUpdated" />
    /// <seealso cref="MessageDeleted" />
    /// <seealso cref="MessageReactionAdded" />
    public IObservable<MessageReactionEvent> MessageReactionRemoved => ((IEventInfo<MessageReactionEvent>)GuildedEvents["ChannelMessageReactionCreated"]).Observable;
    #endregion

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
    public Task<Message> CreateMessageAsync(Guid channel, string? content, IList<Guid>? replyTo, ISet<Uri>? hiddenUrls, bool isPrivate = false, bool isSilent = false, params Embed[] embeds) =>
        CreateMessageAsync(channel, new MessageContent(content, embeds, replyTo, hiddenUrls, isPrivate, isSilent));

    /// <inheritdoc cref="CreateMessageAsync(Guid, string, IList{Embed}?, IList{Guid}?, ISet{Uri}?, bool, bool)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="embeds">The list of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    /// <param name="hiddenUrls">The <see cref="Uri">URLs</see> that will not have <see cref="Embed">link embeds</see></param>
    /// <param name="isPrivate">Whether the reply or mention is private</param>
    /// <param name="isSilent">Whether the reply or mention is silent and does not ping anyone</param>
    /// <param name="replyTo">The array of all <see cref="Message">messages</see> it is replying to (max — <c>5</c>)</param>
    public Task<Message> CreateMessageAsync(Guid channel, string? content, IList<Embed>? embeds, ISet<Uri>? hiddenUrls, bool isPrivate = false, bool isSilent = false, params Guid[] replyTo) =>
        CreateMessageAsync(channel, new MessageContent(content, embeds, replyTo, hiddenUrls, isPrivate, isSilent));

    /// <inheritdoc cref="CreateMessageAsync(Guid, string, IList{Embed}?, IList{Guid}?, ISet{Uri}?, bool, bool)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task<Message> CreateMessageAsync(Guid channel, string? content, params Embed[] embeds) =>
        CreateMessageAsync(channel, new MessageContent(content, embeds));

    /// <inheritdoc cref="CreateMessageAsync(Guid, string, IList{Embed}?, IList{Guid}?, ISet{Uri}?, bool, bool)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task<Message> CreateMessageAsync(Guid channel, params Embed[] embeds) =>
        CreateMessageAsync(channel, new MessageContent { Embeds = embeds });

    /// <inheritdoc cref="CreateMessageAsync(Guid, string, IList{Embed}?, IList{Guid}?, ISet{Uri}?, bool, bool)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="isPrivate">Whether the mention is private</param>
    /// <param name="isSilent">Whether the mention is silent and does not ping anyone</param>
    /// <param name="embeds">The array of all <see cref="Embed">custom embeds</see> in the <see cref="Message">message</see> (max — <c>1</c>)</param>
    public Task<Message> CreateMessageAsync(Guid channel, bool isPrivate = false, bool isSilent = false, params Embed[] embeds) =>
        CreateMessageAsync(channel, new MessageContent { Embeds = embeds, IsPrivate = isPrivate, IsSilent = isSilent });

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
    public Task<Message> UpdateMessageAsync(Guid channel, Guid message, string? content, params Embed[] embeds) =>
        UpdateMessageAsync(channel, message, new MessageContent(content, embeds));

    /// <inheritdoc cref="UpdateMessageAsync(Guid, Guid, MessageContent)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to edit</param>
    /// <param name="content">The new <see cref="Message.Content">text contents</see> of the <see cref="Message">message</see> in Markdown (max — <c>4000</c>)</param>
    /// <param name="embeds">The new <see cref="Embed">custom embeds</see> of the <see cref="Message">message</see> in Markdown (max — <c>1</c>)</param>
    /// <param name="hiddenUrls">The new <see cref="Uri">URLs</see> that will not have <see cref="Embed">link embeds</see></param>
    public Task<Message> UpdateMessageAsync(Guid channel, Guid message, string? content, ISet<Uri>? hiddenUrls, params Embed[] embeds) =>
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

}