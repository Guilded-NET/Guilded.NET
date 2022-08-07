using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Base.Content;
using Guilded.Base.Embeds;
using Guilded.Base.Servers;
using RestSharp;

namespace Guilded;

/// <summary>
/// Represents the <see cref="GuildedWebhookClient">client</see> for <see cref="Webhook">webhook</see> execution.
/// </summary>
/// <remarks>
/// <para>This does not require to be connected. You can use it on a go.</para>
/// </remarks>
/// <example>
/// <para>Here's an example of a <see cref="GuildedWebhookClient">webhook client</see> in action:</para>
/// <code language="csharp">
/// await using var webhookClient = new GuildedWebhookClient("...url here...", "... another webhook's url here...", ...);
///
/// await webhookClient.CreateMessageAsync("Everyone, we have an announcement to make: Stop bullying!");
/// </code>
/// </example>
/// <seealso cref="GuildedBotClient" />
/// <seealso cref="AbstractGuildedClient" />
/// <seealso cref="BaseGuildedClient" />
/// <seealso cref="BaseGuildedService" />
public class GuildedWebhookClient : BaseGuildedService
{
    /// <summary>
    /// Gets the list of all <see cref="Webhook">webhooks</see> this <see cref="GuildedWebhookClient">client</see> will execute.
    /// </summary>
    /// <value>List of <see cref="Webhook">webhooks</see></value>
    public IList<IWebhook> Webhooks { get; }

    #region Constructors
    /// <summary>
    /// Creates a new <see cref="GuildedWebhookClient" /> instance from given <paramref name="webhooks" />.
    /// </summary>
    /// <remarks>
    /// <para>To execute the webhooks, you can use <see cref="CreateMessageAsync(MessageContent)" /> method.</para>
    /// </remarks>
    /// <param name="webhooks">The list of <see cref="Webhook">webhooks</see> that will be executed</param>
    /// <returns>New <see cref="GuildedWebhookClient" /> instance</returns>
    /// <seealso cref="GuildedWebhookClient" />
    /// <seealso cref="GuildedWebhookClient(IList{IWebhook})" />
    /// <seealso cref="GuildedWebhookClient(IWebhook[])" />
    /// <seealso cref="GuildedWebhookClient(Uri[])" />
    /// <seealso cref="GuildedWebhookClient(string[])" />
    /// <seealso cref="GuildedWebhookClient(Guid, string)" />
    public GuildedWebhookClient(IList<IWebhook> webhooks) : base(new Uri(GuildedUrl.Media, "webhooks")) =>
        Webhooks = webhooks;

    /// <inheritdoc cref="GuildedWebhookClient(IList{IWebhook})" />
    public GuildedWebhookClient(params IWebhook[] webhooks) : this((IList<IWebhook>)webhooks) { }

    /// <inheritdoc cref="GuildedWebhookClient(IList{IWebhook})" />
    public GuildedWebhookClient(params Uri[] webhookUrls) : this(webhookUrls.Select(x => new WebhookSkeleton(x)).ToList<IWebhook>()) { }

    /// <inheritdoc cref="GuildedWebhookClient(IList{IWebhook})" />
    public GuildedWebhookClient(params string[] webhookUrls) : this(webhookUrls.Select(x => new WebhookSkeleton(x)).ToList<IWebhook>()) { }

    /// <inheritdoc cref="GuildedWebhookClient(IList{IWebhook})" />
    public GuildedWebhookClient(Guid webhook, string token) : this(new WebhookSkeleton(webhook, token)) { }
    #endregion

    #region Methods
    /// <summary>
    /// Creates a <see cref="Message">message</see> for every <see cref="Webhooks">webhook</see> in the <see cref="GuildedWebhookClient">client</see>.
    /// </summary>
    /// <param name="message">The message to send</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public async Task CreateMessageAsync(MessageContent message)
    {
        foreach (IWebhook webhook in Webhooks)
            await ExecuteRequestAsync(new RestRequest(webhook.Url, Method.Post).AddBody(message)).ConfigureAwait(false);
    }

    /// <summary>
    /// Creates a <see cref="Message">message</see> for every <see cref="Webhooks">webhook</see> in the <see cref="GuildedWebhookClient">client</see>.
    /// </summary>
    /// <remarks>
    /// <para>The <paramref name="content">text content</paramref> will be formatted in Markdown.</para>
    /// </remarks>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of <see cref="Message">the message</see> in Markdown</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public Task CreateMessageAsync(string content) =>
        CreateMessageAsync(new MessageContent(content));

    /// <summary>
    /// Creates a <see cref="Message">message</see> for every <see cref="Webhooks">webhook</see> in the <see cref="GuildedWebhookClient">client</see>.
    /// </summary>
    /// <remarks>
    /// <para>The <paramref name="content">text content</paramref> will be formatted in Markdown.</para>
    /// </remarks>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of <see cref="Message">the message</see> in Markdown</param>
    /// <param name="embeds">The list of <see cref="Embed">all custom embeds</see> in <see cref="Message">the message</see> (max — <c>1</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public Task CreateMessageAsync(string content, IList<Embed> embeds) =>
        CreateMessageAsync(new MessageContent(content) { Embeds = embeds });

    /// <summary>
    /// Creates a <see cref="Message">message</see> for every <see cref="Webhooks">webhook</see> in the <see cref="GuildedWebhookClient">client</see>.
    /// </summary>
    /// <remarks>
    /// <para>The <paramref name="content">text content</paramref> will be formatted in Markdown.</para>
    /// </remarks>
    /// <param name="content">The <see cref="Message.Content">text contents</see> of <see cref="Message">the message</see> in Markdown</param>
    /// <param name="embeds">The array of <see cref="Embed">all custom embeds</see> in <see cref="Message">the message</see> (max — <c>1</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public Task CreateMessageAsync(string content, params Embed[] embeds) =>
        CreateMessageAsync(new MessageContent(content) { Embeds = embeds });

    /// <summary>
    /// Creates a <see cref="Message">message</see> for every <see cref="Webhooks">webhook</see> in the <see cref="GuildedWebhookClient">client</see>.
    /// </summary>
    /// <param name="embeds">The list of <see cref="Embed">all custom embeds</see> in <see cref="Message">the message</see> (max — <c>1</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public Task CreateMessageAsync(IList<Embed> embeds) =>
        CreateMessageAsync(new MessageContent { Embeds = embeds });

    /// <summary>
    /// Creates a <see cref="Message">message</see> for every <see cref="Webhooks">webhook</see> in the <see cref="GuildedWebhookClient">client</see>.
    /// </summary>
    /// <param name="embeds">The array of <see cref="Embed">all custom embeds</see> in <see cref="Message">the message</see> (max — <c>1</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedResourceException" />
    public Task CreateMessageAsync(params Embed[] embeds) =>
        CreateMessageAsync(new MessageContent { Embeds = embeds });
    #endregion
}