using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base.Client;
using Guilded.Base.Embeds;
using Guilded.Base.Servers;
using Guilded.Base.Users;

namespace Guilded.Base.Content;

/// <summary>
/// Represents <see cref="ChannelContent{TId, TServer}">the content</see> that can be created.
/// </summary>
/// <seealso cref="ChannelContent{TId, TServer}" />
/// <seealso cref="IUpdatableContent" />
/// <seealso cref="IWebhookCreatable" />
/// <seealso cref="IReactibleContent" />
public interface ICreatableContent
{
    #region Properties
    /// <summary>
    /// Gets the identifier of <see cref="User">user</see> that created the content.
    /// </summary>
    /// <remarks>
    /// <para>If <see cref="Webhook">a webhook</see> created <see cref="ChannelContent{TId, TServer}">the content</see>, the value of this property will be <c>Ann6LewA</c>.</para>
    /// </remarks>
    /// <value><see cref="UserSummary.Id">User ID</see></value>
    /// <seealso cref="ICreatableContent" />
    /// <seealso cref="CreatedAt" />
    /// <seealso cref="IUpdatableContent.UpdatedAt" />
    /// <seealso cref="IWebhookCreatable.CreatedByWebhook" />
    HashId CreatedBy { get; }

    /// <summary>
    /// Gets the date when <see cref="ChannelContent{TId, TServer}">the content</see> was created.
    /// </summary>
    /// <value>Date</value>
    /// <seealso cref="ICreatableContent" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="IUpdatableContent.UpdatedAt" />
    /// <seealso cref="IWebhookCreatable.CreatedByWebhook" />
    DateTime CreatedAt { get; }
    #endregion
}

/// <summary>
/// Represents <see cref="ChannelContent{TId, TServer}">the content</see> that can be edited.
/// </summary>
/// <seealso cref="ChannelContent{TId, TServer}" />
/// <seealso cref="ICreatableContent" />
/// <seealso cref="IWebhookCreatable" />
/// <seealso cref="IReactibleContent" />
public interface IUpdatableContent
{
    #region Properties
    /// <summary>
    /// Gets the date when <see cref="ChannelContent{TId, TServer}">the content</see> were edited.
    /// </summary>
    /// <remarks>
    /// <para>Only returns the most recent update.</para>
    /// </remarks>
    /// <value>Date?</value>
    /// <seealso cref="IUpdatableContent" />
    /// <seealso cref="ICreatableContent.CreatedAt" />
    /// <seealso cref="ICreatableContent.CreatedBy" />
    /// <seealso cref="IWebhookCreatable.CreatedByWebhook" />
    DateTime? UpdatedAt { get; }
    #endregion
}

/// <summary>
/// Represents <see cref="ChannelContent{TId, TServer}">the content</see> that can be created by <see cref="Webhook">a webhook</see>.
/// </summary>
/// <seealso cref="ChannelContent{TId, TServer}" />
/// <seealso cref="ICreatableContent" />
/// <seealso cref="IUpdatableContent" />
/// <seealso cref="IReactibleContent" />
public interface IWebhookCreatable
{
    #region Properties
    /// <summary>
    /// Gets the identifier of <see cref="Webhook">the webhook</see> that created <see cref="ChannelContent{TId, TServer}">the content</see>.
    /// </summary>
    /// <value><see cref="Webhook.Id">Webhook ID</see>?</value>
    /// <seealso cref="IWebhookCreatable" />
    /// <seealso cref="ICreatableContent.CreatedAt" />
    /// <seealso cref="ICreatableContent.CreatedBy" />
    /// <seealso cref="IUpdatableContent.UpdatedAt" />
    Guid? CreatedByWebhook { get; }
    #endregion
}

/// <summary>
/// Represents <see cref="ChannelContent{TId, TServer}">the content</see> that can be <see cref="Reaction">reacted</see> on.
/// </summary>
/// <seealso cref="ChannelContent{TId, TServer}" />
/// <seealso cref="ICreatableContent" />
/// <seealso cref="IUpdatableContent" />
/// <seealso cref="IWebhookCreatable" />
public interface IReactibleContent
{
    #region Methods
    /// <inheritdoc cref="BaseGuildedClient.AddReactionAsync(Guid, uint, uint)" />
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to add</param>
    Task AddReactionAsync(uint emote);

    /// <inheritdoc cref="BaseGuildedClient.RemoveReactionAsync(Guid, uint, uint)" />
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to remove</param>
    Task RemoveReactionAsync(uint emote);

    /// <inheritdoc cref="BaseGuildedClient.AddReactionAsync(Guid, uint, uint)" />
    /// <param name="emote">The <see cref="Emote">emote</see> to add</param>
    Task AddReactionAsync(Emote emote) => AddReactionAsync(emote.Id);

    /// <inheritdoc cref="BaseGuildedClient.RemoveReactionAsync(Guid, uint, uint)" />
    /// <param name="emote">The <see cref="Emote">emote</see> to remove</param>
    Task RemoveReactionAsync(Emote emote) => RemoveReactionAsync(emote.Id);
    #endregion
}

/// <summary>
/// Represents <see cref="ChannelContent{TId, TServer}">the channel content</see> that allow inline formatting.
/// </summary>
public interface IContentMarkdown
{
    #region Properties
    /// <summary>
    /// Gets the <see cref="Content.Mentions">mentions</see> found in <see cref="Message.Content">the content</see>.
    /// </summary>
    /// <value><see cref="Content.Mentions" />?</value>
    Mentions? Mentions { get; }
    #endregion
}

/// <summary>
/// Represents <see cref="ChannelContent{TId, TServer}">the channel content</see> that allow block formatting.
/// </summary>
public interface IContentBlockMarkdown : IContentMarkdown
{
    #region Properties
    /// <summary>
    /// Gets the list of <see cref="Embed">custom embeds</see> that <see cref="ChannelContent{TId, TServer}">the channel content</see> contain.
    /// </summary>
    /// <value>List of <see cref="Embed">embeds</see></value>
    IList<Embed>? Embeds { get; }
    #endregion
}