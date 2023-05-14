using System;
using System.Threading.Tasks;
using Guilded.Client;
using Guilded.Content;
using Guilded.Events;
using Guilded.Servers;

namespace Guilded.Markdown;

/// <summary>
/// Provides reaction extensions to the <see cref="AbstractGuildedClient">Guilded client</see> and <see cref="ChannelContent{TId, TServer}">Guilded content</see>.
/// </summary>
public static class GuildedMarkdownExtensions
{
    #region Methods Reactions by char
    // /// <inheritdoc cref="AbstractGuildedClient.AddReactionAsync(Guid, Guid, uint)" />
    // /// <param name="client">The <see cref="AbstractGuildedClient">client</see> to add reaction with</param>
    // /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    // /// <param name="message">The identifier of the <see cref="Message">message</see> to add a <see cref="Reaction">reaction</see> to</param>
    // /// <param name="emote">The <see cref="Emotes.BySymbol">symbol</see> of the <see cref="Emote">emote</see> to add</param>
    // public static Task AddReactionAsync(this AbstractGuildedClient client, Guid channel, Guid message, char emote) =>
    //     client.AddReactionAsync(channel, message, Emotes.BySymbol[emote]);

    // /// <inheritdoc cref="AbstractGuildedClient.RemoveReactionAsync(Guid, Guid, uint)" />
    // /// <param name="client">The <see cref="AbstractGuildedClient">client</see> to remove reaction with</param>
    // /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    // /// <param name="message">The identifier of the <see cref="Message">message</see> to remove a <see cref="Reaction">reaction</see> from</param>
    // /// <param name="emote">The <see cref="Emotes.BySymbol">symbol</see> of the <see cref="Emote">emote</see> to remove</param>
    // public static Task RemoveReactionAsync(this AbstractGuildedClient client, Guid channel, Guid message, char emote) =>
    //     client.RemoveReactionAsync(channel, message, Emotes.BySymbol[emote]);

    // /// <inheritdoc cref="AbstractGuildedClient.AddReactionAsync(Guid, uint, uint)" />
    // /// <param name="client">The <see cref="AbstractGuildedClient">client</see> to add reaction with</param>
    // /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    // /// <param name="content">The identifier of the <see cref="ChannelContent{TId, TServer}">content</see> to add a <see cref="Reaction">reaction</see> to</param>
    // /// <param name="emote">The <see cref="Emotes.BySymbol">symbol</see> of the <see cref="Emote">emote</see> to add</param>
    // public static Task AddReactionAsync(this AbstractGuildedClient client, Guid channel, uint content, char emote) =>
    //     client.AddReactionAsync(channel, content, Emotes.BySymbol[emote]);

    // /// <inheritdoc cref="AbstractGuildedClient.RemoveReactionAsync(Guid, uint, uint)" />
    // /// <param name="client">The <see cref="AbstractGuildedClient">client</see> to remove reaction with</param>
    // /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    // /// <param name="content">The identifier of the <see cref="ChannelContent{TId, TServer}">content</see> to remove a <see cref="Reaction">reaction</see> from</param>
    // /// <param name="emote">The <see cref="Emotes.BySymbol">symbol</see> of the <see cref="Emote">emote</see> to remove</param>
    // public static Task RemoveReactionAsync(this AbstractGuildedClient client, Guid channel, uint content, char emote) =>
    //     client.RemoveReactionAsync(channel, content, Emotes.BySymbol[emote]);

    /// <inheritdoc cref="AbstractGuildedClient.AddReactionAsync(Guid, uint, uint)" />
    /// <param name="message">The <see cref="Message">message</see> to add the <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emotes.BySymbol">symbol</see> of the <see cref="Emote">emote</see> to add</param>
    public static Task AddReactionAsync(this Message message, char emote) =>
        message.AddReactionAsync(Emotes.BySymbol[emote]);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveReactionAsync(Guid, uint, uint)" />
    /// <param name="message">The <see cref="Message">message</see> to remove the <see cref="Reaction">reaction</see> remove</param>
    /// <param name="emote">The <see cref="Emotes.BySymbol">symbol</see> of the <see cref="Emote">emote</see> to remove</param>
    public static Task RemoveReactionAsync(this Message message, char emote) =>
        message.RemoveReactionAsync(Emotes.BySymbol[emote]);

    /// <inheritdoc cref="AbstractGuildedClient.AddReactionAsync(Guid, uint, uint)" />
    /// <param name="content">The <see cref="TitledContent{T}">content</see> to add the <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emotes.BySymbol">symbol</see> of the <see cref="Emote">emote</see> to add</param>
    public static Task AddReactionAsync<T>(this TitledContent<T> content, char emote) where T : notnull =>
        content.AddReactionAsync(Emotes.BySymbol[emote]);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveReactionAsync(Guid, uint, uint)" />
    /// <param name="content">The <see cref="TitledContent{T}">content</see> to remove the <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The <see cref="Emotes.BySymbol">symbol</see> of the <see cref="Emote">emote</see> to remove</param>
    public static Task RemoveReactionAsync<T>(this TitledContent<T> content, char emote) where T : notnull =>
        content.RemoveReactionAsync(Emotes.BySymbol[emote]);

    /// <inheritdoc cref="AbstractGuildedClient.AddReactionAsync(Guid, uint, uint)" />
    /// <param name="docEvent">The event of the <see cref="Doc">doc</see> to add the <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emotes.BySymbol">symbol</see> of the <see cref="Emote">emote</see> to add</param>
    public static Task AddReactionAsync(this DocEvent docEvent, char emote) =>
        docEvent.AddReactionAsync(Emotes.BySymbol[emote]);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveReactionAsync(Guid, uint, uint)" />
    /// <param name="docEvent">The event of the <see cref="Doc">doc</see> to remove the <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The <see cref="Emotes.BySymbol">symbol</see> of the <see cref="Emote">emote</see> to remove</param>
    public static Task RemoveReactionAsync(this DocEvent docEvent, char emote) =>
        docEvent.RemoveReactionAsync(Emotes.BySymbol[emote]);

    /// <inheritdoc cref="AbstractGuildedClient.AddReactionAsync(Guid, uint, uint)" />
    /// <param name="topicEvent">The event of the <see cref="Topic">forum topic</see> to add the <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emotes.BySymbol">symbol</see> of the <see cref="Emote">emote</see> to add</param>
    public static Task AddReactionAsync(this TopicEvent topicEvent, char emote) =>
        topicEvent.AddReactionAsync(Emotes.BySymbol[emote]);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveReactionAsync(Guid, uint, uint)" />
    /// <param name="topicEvent">The event of the <see cref="Topic">forum topic</see> to remove the <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The <see cref="Emotes.BySymbol">symbol</see> of the <see cref="Emote">emote</see> to remove</param>
    public static Task RemoveReactionAsync(this TopicEvent topicEvent, char emote) =>
        topicEvent.RemoveReactionAsync(Emotes.BySymbol[emote]);

    /// <inheritdoc cref="AbstractGuildedClient.AddAnnouncementReactionAsync(Guid, Base.HashId, Emote)" />
    /// <param name="announcementEvent">The event of the <see cref="Announcement">announcement</see> to add the <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emotes.BySymbol">symbol</see> of the <see cref="Emote">emote</see> to add</param>
    public static Task AddReactionAsync(this AnnouncementEvent announcementEvent, char emote) =>
        announcementEvent.AddReactionAsync(Emotes.BySymbol[emote]);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveAnnouncementReactionAsync(Guid, Base.HashId, uint)" />
    /// <param name="announcementEvent">The event of the <see cref="Announcement">announcement</see> to remove the <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The <see cref="Emotes.BySymbol">symbol</see> of the <see cref="Emote">emote</see> to remove</param>
    public static Task RemoveReactionAsync(this AnnouncementEvent announcementEvent, char emote) =>
        announcementEvent.RemoveReactionAsync(Emotes.BySymbol[emote]);

    /// <inheritdoc cref="AbstractGuildedClient.AddReactionAsync(Guid, uint, uint)" />
    public static Task AddReactionAsync(this CalendarEvent calendarEvent, char emote) =>
        calendarEvent.AddReactionAsync(Emotes.BySymbol[emote]);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveReactionAsync(Guid, uint, uint)" />
    public static Task RemoveReactionAsync(this CalendarEvent calendarEvent, char emote) =>
        calendarEvent.RemoveReactionAsync(Emotes.BySymbol[emote]);

    /// <inheritdoc cref="AbstractGuildedClient.AddReactionAsync(Guid, uint, uint)" />
    /// <param name="calendarEventEvent">The event of the <see cref="CalendarEvent">calendar event</see> to add the <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emotes.BySymbol">symbol</see> of the <see cref="Emote">emote</see> to add</param>
    public static Task AddReactionAsync(this CalendarEventEvent calendarEventEvent, char emote) =>
        calendarEventEvent.AddReactionAsync(Emotes.BySymbol[emote]);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveReactionAsync(Guid, uint, uint)" />
    /// <param name="calendarEventEvent">The event of the <see cref="CalendarEvent">calendar event</see> to remove the <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The <see cref="Emotes.BySymbol">symbol</see> of the <see cref="Emote">emote</see> to remove</param>
    public static Task RemoveReactionAsync(this CalendarEventEvent calendarEventEvent, char emote) =>
        calendarEventEvent.RemoveReactionAsync(Emotes.BySymbol[emote]);
    #endregion
}