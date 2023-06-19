using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base.Embeds;
using Guilded.Client;

namespace Guilded.Content;

/// <summary>
/// Represents the <see cref="ChannelContent{TId, TServer}">content</see> that can be <see cref="Reaction">reacted</see> on.
/// </summary>
/// <seealso cref="ChannelContent{TId, TServer}" />
/// <seealso cref="ICreatableContent" />
/// <seealso cref="IUpdatableContent" />
/// <seealso cref="IWebhookCreated" />
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
    /// Gets the <see cref="Content.Mentions">mentions</see> found in the <see cref="Message.Content">content</see>.
    /// </summary>
    /// <value>The <see cref="Content.Mentions">mentions</see> found in the <see cref="Message.Content">content</see></value>
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
    /// <value>The list of <see cref="Embed">custom embeds</see> that the <see cref="ChannelContent{TId, TServer}">channel content</see> contain</value>
    IList<Embed>? Embeds { get; }
    #endregion
}

/// <summary>
/// Represents the <see cref="ChannelContent{TId, TServer}">channel content</see> that can be set as private.
/// </summary>
public interface IPrivatableContent
{
    #region Properties
    /// <summary>
    /// Gets whether the <see cref="ChannelContent{TId, TServer}">channel content</see> cannot be accessed publicly.
    /// </summary>
    /// <value>Whether the <see cref="ChannelContent{TId, TServer}">channel content</see> cannot be accessed publicly</value>
    public bool IsPrivate { get; }
    #endregion
}