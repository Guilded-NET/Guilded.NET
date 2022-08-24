using System;
using Guilded.Base;
using Guilded.Base.Servers;

namespace Guilded.Webhook;

/// <summary>
/// Represents the barebones of a <see cref="Base.Servers.Webhook">webhook</see> that can be executed.
/// </summary>
/// <seealso cref="Webhook" />
public class WebhookSkeleton : IWebhook
{
    /// <inheritdoc />
    public Uri Url { get; }

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="WebhookSkeleton" /> from given <paramref name="url" />.
    /// </summary>
    /// <param name="url">The <see cref="Base.Servers.Webhook.Url">URL for executing</see> a <see cref="Base.Servers.Webhook">webhook</see></param>
    /// <seealso cref="WebhookSkeleton" />
    /// <seealso cref="WebhookSkeleton(string)" />
    /// <seealso cref="WebhookSkeleton(Guid, string)" />
    /// <seealso cref="WebhookSkeleton(string, string)" />
    public WebhookSkeleton(Uri url) =>
        Url = url;

    /// <summary>
    /// Initializes a new instance of <see cref="WebhookSkeleton" /> from given <paramref name="url" />.
    /// </summary>
    /// <param name="url">The <see cref="Base.Servers.Webhook.Url">URL for executing</see> a <see cref="Base.Servers.Webhook">webhook</see></param>
    /// <seealso cref="WebhookSkeleton" />
    /// <seealso cref="WebhookSkeleton(Uri)" />
    /// <seealso cref="WebhookSkeleton(Guid, string)" />
    /// <seealso cref="WebhookSkeleton(string, string)" />
    public WebhookSkeleton(string url) : this(new Uri(url)) { }

    /// <summary>
    /// Initializes a new instance of <see cref="WebhookSkeleton" /> from given <paramref name="token" /> and <paramref name="webhook">webhook ID</paramref>.
    /// </summary>
    /// <param name="webhook">The ID of the <see cref="Base.Servers.Webhook">webhook</see> to execute</param>
    /// <param name="token">The secret token of the <see cref="Base.Servers.Webhook">webhook</see> to use for execution</param>
    /// <seealso cref="WebhookSkeleton" />
    /// <seealso cref="WebhookSkeleton(Uri)" />
    /// <seealso cref="WebhookSkeleton(string)" />
    /// <seealso cref="WebhookSkeleton(string, string)" />
    public WebhookSkeleton(Guid webhook, string token) : this(new Uri(GuildedUrl.Media, $"/webhooks/{webhook}/{token}")) { }

    /// <summary>
    /// Initializes a new instance of <see cref="WebhookSkeleton" /> from given <paramref name="token" /> and <paramref name="webhook">webhook ID</paramref>.
    /// </summary>
    /// <param name="webhook">The ID of the <see cref="Base.Servers.Webhook">webhook</see> to execute</param>
    /// <param name="token">The secret token of the <see cref="Base.Servers.Webhook">webhook</see> to use for execution</param>
    /// <seealso cref="WebhookSkeleton" />
    /// <seealso cref="WebhookSkeleton(Uri)" />
    /// <seealso cref="WebhookSkeleton(string)" />
    /// <seealso cref="WebhookSkeleton(Guid, string)" />
    public WebhookSkeleton(string webhook, string token) : this(new Guid(webhook), token) { }
    #endregion
}