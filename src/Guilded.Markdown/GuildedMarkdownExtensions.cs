using System;
using System.Threading.Tasks;
using Guilded.Base.Client;
using Guilded.Base.Content;
using Guilded.Base.Events;
using Guilded.Base.Servers;

namespace Guilded.Markdown;

/// <summary>
/// Provides reaction extensions to the <see cref="BaseGuildedClient">Guilded client</see> and <see cref="ChannelContent{TId, TServer}">Guilded content</see>.
/// </summary>
public static class GuildedMarkdownExtensions
{
    #region Methods Reactions by char
    /// <inheritdoc cref="BaseGuildedClient.AddReactionAsync(Guid, Guid, uint)" />
    /// <param name="client">The <see cref="BaseGuildedClient">client</see> to add reaction with</param>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emotes.BySymbol">symbol</see> of the <see cref="Emote">emote</see> to add</param>
    public static Task AddReactionAsync(this BaseGuildedClient client, Guid channel, Guid message, char emote) =>
        client.AddReactionAsync(channel, message, Emotes.BySymbol[emote]);

    /// <inheritdoc cref="BaseGuildedClient.RemoveReactionAsync(Guid, Guid, uint)" />
    /// <param name="client">The <see cref="BaseGuildedClient">client</see> to remove reaction with</param>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The <see cref="Emotes.BySymbol">symbol</see> of the <see cref="Emote">emote</see> to remove</param>
    public static Task RemoveReactionAsync(this BaseGuildedClient client, Guid channel, Guid message, char emote) =>
        client.RemoveReactionAsync(channel, message, Emotes.BySymbol[emote]);

    /// <inheritdoc cref="BaseGuildedClient.AddReactionAsync(Guid, uint, uint)" />
    /// <param name="client">The <see cref="BaseGuildedClient">client</see> to add reaction with</param>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="content">The identifier of the <see cref="ChannelContent{TId, TServer}">content</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emotes.BySymbol">symbol</see> of the <see cref="Emote">emote</see> to add</param>
    public static Task AddReactionAsync(this BaseGuildedClient client, Guid channel, uint content, char emote) =>
        client.AddReactionAsync(channel, content, Emotes.BySymbol[emote]);

    /// <inheritdoc cref="BaseGuildedClient.RemoveReactionAsync(Guid, uint, uint)" />
    /// <param name="client">The <see cref="BaseGuildedClient">client</see> to remove reaction with</param>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="content">The identifier of the <see cref="ChannelContent{TId, TServer}">content</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The <see cref="Emotes.BySymbol">symbol</see> of the <see cref="Emote">emote</see> to remove</param>
    public static Task RemoveReactionAsync(this BaseGuildedClient client, Guid channel, uint content, char emote) =>
        client.RemoveReactionAsync(channel, content, Emotes.BySymbol[emote]);

    /// <inheritdoc cref="BaseGuildedClient.AddReactionAsync(Guid, uint, uint)" />
    /// <param name="message">The <see cref="Message">message</see> to add the <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emotes.BySymbol">symbol</see> of the <see cref="Emote">emote</see> to add</param>
    public static Task AddReactionAsync(this Message message, char emote) =>
        message.AddReactionAsync(Emotes.BySymbol[emote]);

    /// <inheritdoc cref="BaseGuildedClient.RemoveReactionAsync(Guid, uint, uint)" />
    /// <param name="message">The <see cref="Message">message</see> to remove the <see cref="Reaction">reaction</see> remove</param>
    /// <param name="emote">The <see cref="Emotes.BySymbol">symbol</see> of the <see cref="Emote">emote</see> to remove</param>
    public static Task RemoveReactionAsync(this Message message, char emote) =>
        message.RemoveReactionAsync(Emotes.BySymbol[emote]);

    /// <inheritdoc cref="BaseGuildedClient.AddReactionAsync(Guid, uint, uint)" />
    /// <param name="content">The <see cref="TitledContent">content</see> to add the <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emotes.BySymbol">symbol</see> of the <see cref="Emote">emote</see> to add</param>
    public static Task AddReactionAsync(this TitledContent content, char emote) =>
        content.AddReactionAsync(Emotes.BySymbol[emote]);

    /// <inheritdoc cref="BaseGuildedClient.RemoveReactionAsync(Guid, uint, uint)" />
    /// <param name="content">The <see cref="TitledContent">content</see> to remove the <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The <see cref="Emotes.BySymbol">symbol</see> of the <see cref="Emote">emote</see> to remove</param>
    public static Task RemoveReactionAsync(this TitledContent content, char emote) =>
        content.RemoveReactionAsync(Emotes.BySymbol[emote]);

    /// <inheritdoc cref="BaseGuildedClient.AddReactionAsync(Guid, uint, uint)" />
    /// <param name="docEvent">The event of the <see cref="Doc">doc</see> to add the <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emotes.BySymbol">symbol</see> of the <see cref="Emote">emote</see> to add</param>
    public static Task AddReactionAsync(this DocEvent docEvent, char emote) =>
        docEvent.AddReactionAsync(Emotes.BySymbol[emote]);

    /// <inheritdoc cref="BaseGuildedClient.RemoveReactionAsync(Guid, uint, uint)" />
    /// <param name="docEvent">The event of the <see cref="Doc">doc</see> to remove the <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The <see cref="Emotes.BySymbol">symbol</see> of the <see cref="Emote">emote</see> to remove</param>
    public static Task RemoveReactionAsync(this DocEvent docEvent, char emote) =>
        docEvent.RemoveReactionAsync(Emotes.BySymbol[emote]);

    /// <inheritdoc cref="BaseGuildedClient.AddReactionAsync(Guid, uint, uint)" />
    /// <param name="topicEvent">The event of the <see cref="Topic">forum topic</see> to add the <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emotes.BySymbol">symbol</see> of the <see cref="Emote">emote</see> to add</param>
    public static Task AddReactionAsync(this TopicEvent topicEvent, char emote) =>
        topicEvent.AddReactionAsync(Emotes.BySymbol[emote]);

    /// <inheritdoc cref="BaseGuildedClient.RemoveReactionAsync(Guid, uint, uint)" />
    /// <param name="topicEvent">The event of the <see cref="Topic">forum topic</see> to remove the <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The <see cref="Emotes.BySymbol">symbol</see> of the <see cref="Emote">emote</see> to remove</param>
    public static Task RemoveReactionAsync(this TopicEvent topicEvent, char emote) =>
        topicEvent.RemoveReactionAsync(Emotes.BySymbol[emote]);

    /// <inheritdoc cref="BaseGuildedClient.AddReactionAsync(Guid, uint, uint)" />
    public static Task AddReactionAsync(this CalendarEvent calendarEvent, char emote) =>
        calendarEvent.AddReactionAsync(Emotes.BySymbol[emote]);

    /// <inheritdoc cref="BaseGuildedClient.RemoveReactionAsync(Guid, uint, uint)" />
    public static Task RemoveReactionAsync(this CalendarEvent calendarEvent, char emote) =>
        calendarEvent.RemoveReactionAsync(Emotes.BySymbol[emote]);

    /// <inheritdoc cref="BaseGuildedClient.AddReactionAsync(Guid, uint, uint)" />
    /// <param name="calendarEventEvent">The event of the <see cref="CalendarEvent">calendar event</see> to add the <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emotes.BySymbol">symbol</see> of the <see cref="Emote">emote</see> to add</param>
    public static Task AddReactionAsync(this CalendarEventEvent calendarEventEvent, char emote) =>
        calendarEventEvent.AddReactionAsync(Emotes.BySymbol[emote]);

    /// <inheritdoc cref="BaseGuildedClient.RemoveReactionAsync(Guid, uint, uint)" />
    /// <param name="calendarEventEvent">The event of the <see cref="CalendarEvent">calendar event</see> to remove the <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The <see cref="Emotes.BySymbol">symbol</see> of the <see cref="Emote">emote</see> to remove</param>
    public static Task RemoveReactionAsync(this CalendarEventEvent calendarEventEvent, char emote) =>
        calendarEventEvent.RemoveReactionAsync(Emotes.BySymbol[emote]);
    #endregion
}