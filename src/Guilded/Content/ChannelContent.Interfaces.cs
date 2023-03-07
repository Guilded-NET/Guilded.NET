using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Base.Embeds;
using Guilded.Client;
using Guilded.Servers;
using Guilded.Users;

namespace Guilded.Content;

/// <summary>
/// Represents the <see cref="ChannelContent{TId, TServer}">content</see> that can be created by a <see cref="User">user</see>.
/// </summary>
public interface IUserCreated
{
    /// <summary>
    /// Gets the identifier of <see cref="User">user</see> that created the content.
    /// </summary>
    /// <remarks>
    /// <para>If a <see cref="Webhook">webhook</see> created the <see cref="ChannelContent{TId, TServer}">content</see>, the value of this property will be <c>Ann6LewA</c>.</para>
    /// </remarks>
    /// <value><see cref="UserSummary.Id">User ID</see></value>
    /// <seealso cref="IUserCreated" />
    /// <seealso cref="ICreatableContent" />
    /// <seealso cref="IWebhookCreatable.CreatedByWebhook" />
    /// <seealso cref="ICreatableContent.CreatedAt" />
    /// <seealso cref="IUpdatableContent.UpdatedAt" />
    HashId CreatedBy { get; }
}

/// <summary>
/// Represents the <see cref="ChannelContent{TId, TServer}">content</see> that can be created and has specified <see cref="CreatedAt">creation date</see>.
/// </summary>
/// <seealso cref="ChannelContent{TId, TServer}" />
/// <seealso cref="IUpdatableContent" />
/// <seealso cref="IWebhookCreatable" />
/// <seealso cref="IReactibleContent" />
public interface ICreatableContent : IUserCreated
{
    #region Properties
    /// <summary>
    /// Gets the date when the <see cref="ChannelContent{TId, TServer}">content</see> was created.
    /// </summary>
    /// <value>Date</value>
    /// <seealso cref="ICreatableContent" />
    /// <seealso cref="IUserCreated" />
    /// <seealso cref="IUserCreated.CreatedBy" />
    /// <seealso cref="IUpdatableContent.UpdatedAt" />
    /// <seealso cref="IWebhookCreatable.CreatedByWebhook" />
    DateTime CreatedAt { get; }
    #endregion
}

/// <summary>
/// Represents the <see cref="ChannelContent{TId, TServer}">content</see> that can be edited.
/// </summary>
/// <seealso cref="ChannelContent{TId, TServer}" />
/// <seealso cref="ICreatableContent" />
/// <seealso cref="IWebhookCreatable" />
/// <seealso cref="IReactibleContent" />
public interface IUpdatableContent
{
    #region Properties
    /// <summary>
    /// Gets the date when the <see cref="ChannelContent{TId, TServer}">content</see> were edited.
    /// </summary>
    /// <remarks>
    /// <para>Only returns the most recent update.</para>
    /// </remarks>
    /// <value>Date?</value>
    /// <seealso cref="IUpdatableContent" />
    /// <seealso cref="ICreatableContent.CreatedAt" />
    /// <seealso cref="IUserCreated.CreatedBy" />
    /// <seealso cref="IWebhookCreatable.CreatedByWebhook" />
    DateTime? UpdatedAt { get; }
    #endregion
}

/// <summary>
/// Represents the <see cref="ChannelContent{TId, TServer}">content</see> that can be created by a <see cref="Webhook">webhook</see>.
/// </summary>
/// <seealso cref="ChannelContent{TId, TServer}" />
/// <seealso cref="ICreatableContent" />
/// <seealso cref="IUpdatableContent" />
/// <seealso cref="IReactibleContent" />
public interface IWebhookCreatable
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="Webhook">webhook</see> that created the <see cref="ChannelContent{TId, TServer}">content</see>.
    /// </summary>
    /// <value><see cref="Webhook.Id">Webhook ID</see>?</value>
    /// <seealso cref="IWebhookCreatable" />
    /// <seealso cref="ICreatableContent.CreatedAt" />
    /// <seealso cref="IUserCreated.CreatedBy" />
    /// <seealso cref="IUpdatableContent.UpdatedAt" />
    Guid? CreatedByWebhook { get; }
    #endregion
}

/// <summary>
/// Represents the <see cref="ChannelContent{TId, TServer}">content</see> that can be <see cref="Reaction">reacted</see> on.
/// </summary>
/// <seealso cref="ChannelContent{TId, TServer}" />
/// <seealso cref="ICreatableContent" />
/// <seealso cref="IUpdatableContent" />
/// <seealso cref="IWebhookCreatable" />
public interface IReactibleContent
{
    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.AddReactionAsync(Guid, uint, uint)" />
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to add</param>
    Task AddReactionAsync(uint emote);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveReactionAsync(Guid, uint, uint)" />
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to remove</param>
    Task RemoveReactionAsync(uint emote);

    /// <inheritdoc cref="AbstractGuildedClient.AddReactionAsync(Guid, uint, uint)" />
    /// <param name="emote">The <see cref="Emote">emote</see> to add</param>
    public Task AddReactionAsync(Emote emote) => AddReactionAsync(emote.Id);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveReactionAsync(Guid, uint, uint)" />
    /// <param name="emote">The <see cref="Emote">emote</see> to remove</param>
    public Task RemoveReactionAsync(Emote emote) => RemoveReactionAsync(emote.Id);
    #endregion
}

/// <summary>
/// Represents the <see cref="ChannelContent{TId, TServer}">channel content</see> that allow inline formatting.
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
/// Represents the <see cref="ChannelContent{TId, TServer}">channel content</see> that allow block formatting.
/// </summary>
public interface IContentBlockMarkdown : IContentMarkdown
{
    #region Properties
    /// <summary>
    /// Gets the list of <see cref="Embed">custom embeds</see> that the <see cref="ChannelContent{TId, TServer}">channel content</see> contain.
    /// </summary>
    /// <value>List of <see cref="Embed">embeds</see></value>
    IList<Embed>? Embeds { get; }
    #endregion
}

/// <summary>
/// Represents the <see cref="ChannelContent{TId, TServer}">channel content</see> that can be set as private.
/// </summary>
public interface IPrivatableContent
{
    /// <summary>
    /// Gets whether the <see cref="ChannelContent{TId, TServer}">channel content</see> cannot be accessed publicly.
    /// </summary>
    /// <value>Whether the <see cref="ChannelContent{TId, TServer}">channel content</see> cannot be accessed publicly</value>
    public bool IsPrivate { get; }
}