using System;
using System.Threading.Tasks;

namespace Guilded.Base.Content;

/// <summary>
/// The content that can be created.
/// </summary>
/// <remarks>
/// <para>The interface for channel content that can be created at any time.</para>
/// </remarks>
public interface ICreatableContent
{
    /// <summary>
    /// The identifier of the user creator of the content.
    /// </summary>
    /// <remarks>
    /// <para>The identifier of the user that created this content.</para>
    /// <para>If webhook or bot created this reaction, the value of this property will be <c>Ann6LewA</c>.</para>
    /// </remarks>
    /// <value>User ID</value>
    HashId CreatedBy { get; }
    /// <summary>
    /// The date of when the content was created.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="DateTime"/> of when the content was created.</para>
    /// </remarks>
    /// <value>Date</value>
    DateTime CreatedAt { get; }
}
/// <summary>
/// The content that can be updated.
/// </summary>
/// <remarks>
/// <para>The interface for channel content that can be updated/edited at any time.</para>
/// </remarks>
public interface IUpdatableContent
{
    /// <summary>
    /// The date of when the content was updated.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="DateTime"/> of when the content was updated/edited. Only returns the most recent update.</para>
    /// </remarks>
    /// <value>Date?</value>
    DateTime? UpdatedAt { get; }
}
/// <summary>
/// The content that can be created by a webhook.
/// </summary>
/// <remarks>
/// <para>The interface for channel content that can be created by a webhook.</para>
/// </remarks>
public interface IWebhookCreatable
{
    /// <summary>
    /// The identifier of the webhook creator of the content.
    /// </summary>
    /// <remarks>
    /// <para>The identifier of the webhook that created this content.</para>
    /// <note type="note">Currently, only chat messages can be created by Webhooks.</note>
    /// </remarks>
    /// <value>Webhook ID?</value>
    Guid? CreatedByWebhook { get; }
}
/// <summary>
/// The content that can be reacted on.
/// </summary>
/// <remarks>
/// <para>The content that can have reactions.</para>
/// </remarks>
public interface IReactibleContent
{
    /// <inheritdoc cref="BaseGuildedClient.AddReactionAsync(Guid, uint, uint)"/>
    /// <param name="emoteId">The identifier of the emote to add</param>
    Task<Reaction> AddReactionAsync(uint emoteId);
    // /// <inheritdoc cref="BaseGuildedClient.RemoveReactionAsync(Guid, uint, uint)"/>
    // /// <param name="emoteId">The identifier of the emote to remove</param>
    // Task RemoveReactionAsync(uint emoteId);
}