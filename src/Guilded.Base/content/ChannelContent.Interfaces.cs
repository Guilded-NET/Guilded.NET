using System;
using System.Threading.Tasks;
using Guilded.Base.Servers;
using Guilded.Base.Users;

namespace Guilded.Base.Content;

/// <summary>
/// Represents the content that can be created.
/// </summary>
public interface ICreatableContent
{
    /// <summary>
    /// Gets the identifier of <see cref="User">user</see> creator of the content.
    /// </summary>
    /// <remarks>
    /// <para>If <see cref="Webhook">a webhook</see> created the content, the value of this property will be <c>Ann6LewA</c>.</para>
    /// </remarks>
    /// <value>User ID</value>
    HashId CreatedBy { get; }
    /// <summary>
    /// Gets the date when the content was created.
    /// </summary>
    /// <value>Date</value>
    DateTime CreatedAt { get; }
}
/// <summary>
/// Represents the content that can be updated.
/// </summary>
public interface IUpdatableContent
{
    /// <summary>
    /// Gets the date when the content was edited.
    /// </summary>
    /// <remarks>
    /// <para>Only returns the most recent update.</para>
    /// </remarks>
    /// <value>Date?</value>
    DateTime? UpdatedAt { get; }
}
/// <summary>
/// Represents the content that can be created by a webhook.
/// </summary>
public interface IWebhookCreatable
{
    /// <summary>
    /// Gets the identifier of the webhook creator of the content.
    /// </summary>
    /// <remarks>
    /// <note type="note">Currently, only chat messages can be created by Webhooks.</note>
    /// </remarks>
    /// <value>Webhook ID?</value>
    Guid? CreatedByWebhook { get; }
}
/// <summary>
/// Represents the content that can be reacted on.
/// </summary>
public interface IReactibleContent
{
    /// <inheritdoc cref="BaseGuildedClient.AddReactionAsync(Guid, uint, uint)"/>
    /// <param name="emoteId">The identifier of the emote to add</param>
    Task<Reaction> AddReactionAsync(uint emoteId);
    // /// <inheritdoc cref="BaseGuildedClient.RemoveReactionAsync(Guid, uint, uint)"/>
    // /// <param name="emoteId">The identifier of the emote to remove</param>
    // Task RemoveReactionAsync(uint emoteId);
}