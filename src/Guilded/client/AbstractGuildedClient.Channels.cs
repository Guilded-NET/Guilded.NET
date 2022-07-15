using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Base.Content;

using RestSharp;

namespace Guilded;

public abstract partial class AbstractGuildedClient
{
    #region Methods

    #region Methods Webhook
    /// <inheritdoc />
    public override async Task CreateHookMessageAsync(Uri webhookUrl, MessageContent message) =>
        await ExecuteRequestAsync(new RestRequest(webhookUrl, Method.Post).AddJsonBody(message)).ConfigureAwait(false);
    #endregion

    #region Methods Chat channel
    /// <inheritdoc />
    public override async Task<IList<Message>> GetMessagesAsync(Guid channel, bool includePrivate = false, uint? limit = null, DateTime? before = null, DateTime? after = null) =>
        await GetResponseProperty<IList<Message>>(
            new RestRequest($"channels/{channel}/messages", Method.Get)
                // Because it gets uppercased
                .AddQueryParameter("includePrivate", includePrivate ? "true" : "false", encode: false)
                .AddOptionalQuery("limit", limit, encode: false)
                .AddOptionalQuery("before", before)
                .AddOptionalQuery("after", after)
        , "messages").ConfigureAwait(false);

    /// <inheritdoc />
    public override async Task<Message> GetMessageAsync(Guid channel, Guid messageId) =>
        await GetResponseProperty<Message>(new RestRequest($"channels/{channel}/messages/{messageId}", Method.Get), "message").ConfigureAwait(false);

    /// <inheritdoc />
    public override async Task<Message> CreateMessageAsync(Guid channel, MessageContent message)
    {
        if (message is null)
        {
            throw new ArgumentNullException(nameof(message));
        }
        else if (
            // No content and no embeds
            (message.Content is null && message.OnlyText) ||
            // Whitespace content
            (message.Content is not null && string.IsNullOrWhiteSpace(message.Content)))
        {
            throw new ArgumentNullException(nameof(message.Content));
        }

        return await GetResponseProperty<Message>(
            new RestRequest($"channels/{channel}/messages", Method.Post).AddJsonBody(message),
            "message",
            // For slowmode handling
            channel
        ).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public override async Task<Message> UpdateMessageAsync(Guid channel, Guid message, MessageContent content)
    {
        if (content is null)
            throw new ArgumentNullException(nameof(content));
        else
            return await GetResponseProperty<Message>(new RestRequest($"channels/{channel}/messages/{message}", Method.Put).AddJsonBody(content), "message").ConfigureAwait(false);
    }

    /// <inheritdoc />
    public override async Task DeleteMessageAsync(Guid channel, Guid message) =>
        await ExecuteRequestAsync(new RestRequest($"channels/{channel}/messages/{message}", Method.Delete)).ConfigureAwait(false);

    /// <inheritdoc />
    public override async Task AddReactionAsync(Guid channel, Guid message, uint emote) =>
        await ExecuteRequestAsync(new RestRequest($"channels/{channel}/content/{message}/emotes/{emote}", Method.Put)).ConfigureAwait(false);

    /// <inheritdoc />
    public override async Task RemoveReactionAsync(Guid channelId, Guid messageId, uint emoteId) =>
        await ExecuteRequestAsync(new RestRequest($"channels/{channelId}/content/{messageId}/emotes/{emoteId}", Method.Delete)).ConfigureAwait(false);
    #endregion

    #region Methods Forum channels
    /// <inheritdoc />
    public override async Task<Topic> CreateTopicAsync(Guid channel, string title, string content)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title));
        else if (string.IsNullOrWhiteSpace(content)) throw new ArgumentNullException(nameof(content));

        return await GetResponseProperty<Topic>(new RestRequest($"channels/{channel}/topics", Method.Post)
            .AddJsonBody(new
            {
                title,
                content
            })
        , "forumTopic").ConfigureAwait(false);
    }
    #endregion

    #region Methods List channels
    /// <inheritdoc />
    public override async Task<IList<ListItemSummary>> GetListItemsAsync(Guid channel) =>
        await GetResponseProperty<IList<ListItemSummary>>(new RestRequest($"channels/{channel}/items", Method.Get), "listItems").ConfigureAwait(false);

    /// <inheritdoc />
    public override async Task<ListItem> GetListItemAsync(Guid channel, Guid listItem) =>
        await GetResponseProperty<ListItem>(new RestRequest($"channels/{channel}/items/{listItem}", Method.Get), "listItem").ConfigureAwait(false);

    /// <inheritdoc />
    public override async Task<ListItem> CreateListItemAsync(Guid channel, string message, string? note = null)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentNullException(nameof(message));

        return await GetResponseProperty<ListItem>(new RestRequest($"channels/{channel}/items", Method.Post)
            .AddJsonBody(new
            {
                message,
                note = new
                {
                    content = note
                }
            })
        , "listItem").ConfigureAwait(false);
    }

    /// <inheritdoc />
    public override async Task<ListItem> UpdateListItemAsync(Guid channel, Guid listItem, string message, string? note = null)
    {
        if (string.IsNullOrWhiteSpace(message) && string.IsNullOrEmpty(note))
            throw new ArgumentNullException(nameof(message), "Either the message or the note of the list item's update must be specified");

        return await GetResponseProperty<ListItem>(new RestRequest($"channels/{channel}/items/{listItem}", Method.Put)
            .AddJsonBody(new
            {
                message,
                note = new
                {
                    content = note
                }
            })
        , "listItem").ConfigureAwait(false);
    }

    /// <inheritdoc />
    public override async Task DeleteListItemAsync(Guid channel, Guid listItem) =>
        await ExecuteRequestAsync(new RestRequest($"channels/{channel}/items/{listItem}", Method.Delete)).ConfigureAwait(false);

    /// <inheritdoc />
    public override async Task CompleteListItemAsync(Guid channel, Guid listItem) =>
        await ExecuteRequestAsync(new RestRequest($"channels/{channel}/items/{listItem}/complete", Method.Post)).ConfigureAwait(false);

    /// <inheritdoc />
    public override async Task UncompleteListItemAsync(Guid channel, Guid listItem) =>
        await ExecuteRequestAsync(new RestRequest($"channels/{channel}/items/{listItem}/complete", Method.Delete)).ConfigureAwait(false);
    #endregion

    #region Document channels
    /// <inheritdoc />
    public override async Task<IList<Doc>> GetDocsAsync(Guid channel, uint? limit = null, DateTime? before = null) =>
        await GetResponseProperty<IList<Doc>>(
            new RestRequest($"channels/{channel}/docs", Method.Get)
                .AddOptionalQuery("limit", limit, encode: false)
                .AddOptionalQuery("before", before)
        , "docs").ConfigureAwait(false);

    /// <inheritdoc />
    public override async Task<Doc> GetDocAsync(Guid channel, uint doc) =>
        await GetResponseProperty<Doc>(new RestRequest($"channels/{channel}/docs/{doc}", Method.Get), "doc").ConfigureAwait(false);

    /// <inheritdoc />
    public override async Task<Doc> CreateDocAsync(Guid channel, string title, string content)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title));
        else if (string.IsNullOrWhiteSpace(content)) throw new ArgumentNullException(nameof(content));

        return await GetResponseProperty<Doc>(new RestRequest($"channels/{channel}/docs", Method.Post)
            .AddJsonBody(new
            {
                title,
                content
            })
        , "doc").ConfigureAwait(false);
    }

    /// <inheritdoc />
    public override async Task<Doc> UpdateDocAsync(Guid channel, uint doc, string title, string content)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title));
        else if (string.IsNullOrWhiteSpace(content)) throw new ArgumentNullException(nameof(content));

        return await GetResponseProperty<Doc>(new RestRequest($"channels/{channel}/docs/{doc}", Method.Put)
            .AddJsonBody(new
            {
                title,
                content
            })
        , "doc").ConfigureAwait(false);
    }

    /// <inheritdoc />
    public override async Task DeleteDocAsync(Guid channel, uint doc) =>
        await ExecuteRequestAsync(new RestRequest($"channels/{channel}/docs/{doc}", Method.Delete)).ConfigureAwait(false);
    #endregion

    #region Methods Calendar channels

    #region Methods Calendar channels > Events
    /// <inheritdoc />
    public override async Task<IList<CalendarEvent>> GetEventsAsync(Guid channel, uint? limit = null, DateTime? before = null) =>
        await GetResponseProperty<IList<CalendarEvent>>(
            new RestRequest($"channels/{channel}/events", Method.Get)
                .AddOptionalQuery("limit", limit, encode: false)
                .AddOptionalQuery("before", before)
        , "calendarEvents").ConfigureAwait(false);

    /// <inheritdoc />
    public override async Task<CalendarEvent> GetEventAsync(Guid channel, uint calendarEvent) =>
        await GetResponseProperty<CalendarEvent>(new RestRequest($"channels/{channel}/events/{calendarEvent}", Method.Get), "calendarEvent").ConfigureAwait(false);

    /// <inheritdoc />
    public override async Task<CalendarEvent> CreateEventAsync(Guid channel, string name, string? description = null, string? location = null, DateTime? startsAt = null, Uri? url = null, Color? color = null, uint? duration = null, uint? rsvpLimit = null, bool isPrivate = false)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));

        return await GetResponseProperty<CalendarEvent>(new RestRequest($"channels/{channel}/events", Method.Post)
            .AddJsonBody(new
            {
                name,
                description,
                location,
                startsAt,
                url,
                color,
                duration,
                rsvpLimit,
                isPrivate
            })
        , "calendarEvent").ConfigureAwait(false);
    }

    /// <inheritdoc />
    public override async Task<CalendarEvent> UpdateEventAsync(Guid channel, uint calendarEvent, string? name = null, string? description = null, string? location = null, DateTime? startsAt = null, Uri? url = null, Color? color = null, uint? duration = null, bool? isPrivate = null) =>
        await GetResponseProperty<CalendarEvent>(new RestRequest($"channels/{channel}/events/{calendarEvent}", Method.Put)
            .AddJsonBody(new
            {
                name,
                description,
                location,
                startsAt,
                url,
                color,
                duration,
                isPrivate
            })
        , "calendarEvent").ConfigureAwait(false);

    /// <inheritdoc />
    public override async Task DeleteEventAsync(Guid channel, uint calendarEvent) =>
        await ExecuteRequestAsync(new RestRequest($"channels/{channel}/events/{calendarEvent}", Method.Delete)).ConfigureAwait(false);
    #endregion

    #region Methods Calendar channels > Rsvp
    /// <inheritdoc />
    public override async Task<IList<CalendarRsvp>> GetRsvpsAsync(Guid channel, uint calendarEvent) =>
        await GetResponseProperty<IList<CalendarRsvp>>(new RestRequest($"channels/{channel}/events/{calendarEvent}/rsvps", Method.Get), "calendarEventRsvps").ConfigureAwait(false);

    /// <inheritdoc />
    public override async Task<CalendarRsvp> GetRsvpAsync(Guid channel, uint calendarEvent, HashId user) =>
        await GetResponseProperty<CalendarRsvp>(new RestRequest($"channels/{channel}/events/{calendarEvent}/rsvps/{user}", Method.Get), "calendarEventRsvp").ConfigureAwait(false);

    /// <inheritdoc />
    public override async Task<CalendarRsvp> SetRsvpAsync(Guid channel, uint calendarEvent, HashId user, CalendarRsvpStatus status) =>
        await GetResponseProperty<CalendarRsvp>(new RestRequest($"channels/{channel}/events/{calendarEvent}/rsvps/{user}", Method.Put)
            .AddJsonBody(new
            {
                status
            })
        , "calendarEventRsvp").ConfigureAwait(false);

    /// <inheritdoc />
    public override async Task RemoveRsvpAsync(Guid channel, uint calendarEvent, HashId user) =>
        await ExecuteRequestAsync(new RestRequest($"channels/{channel}/events/{calendarEvent}/rsvps/{user}", Method.Delete)).ConfigureAwait(false);
    #endregion

    #endregion

    #region Methods Content
    /// <inheritdoc />
    public override async Task AddReactionAsync(Guid channel, uint content, uint emote) =>
        await ExecuteRequestAsync(new RestRequest($"channels/{channel}/content/{content}/emotes/{emote}", Method.Put)).ConfigureAwait(false);

    /// <inheritdoc />
    public override async Task RemoveReactionAsync(Guid channelId, uint contentId, uint emoteId) =>
        await ExecuteRequestAsync(new RestRequest($"channels/{channelId}/content/{contentId}/emotes/{emoteId}", Method.Delete)).ConfigureAwait(false);
    #endregion

    #endregion
}