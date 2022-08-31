using System;

namespace Guilded.Base.Servers;

/// <summary>
/// Represents the base of a <see cref="Webhook">webhook</see>.
/// </summary>
public interface IWebhook
{
    /// <summary>
    /// Gets the URL for executing <see cref="Webhook">webhooks</see>.
    /// </summary>
    /// <returns>Media <see cref="Uri">URL</see></returns>
    public Uri Url { get; }
}