using System;

namespace Guilded.Base;

/// <summary>
/// Represents the base of a <see cref="T:Guilded.Servers.Webhook">webhook</see>.
/// </summary>
public interface IWebhook
{
    /// <summary>
    /// Gets the URL for executing <see cref="T:Guilded.Servers.Webhook">webhooks</see>.
    /// </summary>
    /// <returns>Media <see cref="Uri">URL</see></returns>
    public Uri Url { get; }
}