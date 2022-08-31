using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Base.Content;

using RestSharp;

namespace Guilded.Client;

public abstract partial class AbstractGuildedClient
{
    #region Methods

    #region Methods Webhook
    /// <inheritdoc />
    public override Task CreateHookMessageAsync(Uri webhookUrl, MessageContent message) =>
        ExecuteRequestAsync(new RestRequest(webhookUrl, Method.Post).AddJsonBody(message));
    #endregion

    #region Methods Chat channel
    /// <inheritdoc />
    public override Task<IList<Message>> GetMessagesAsync(Guid channel, bool includePrivate = false, uint? limit = null, DateTime? before = null, DateTime? after = null) =>
        GetResponseProperty<IList<Message>>(
            new RestRequest($"channels/{channel}/messages", Method.Get)
                // Because it gets uppercased
                .AddQueryParameter("includePrivate", includePrivate ? "true" : "false", encode: false)
                .AddOptionalQuery("limit", limit, encode: false)
                .AddOptionalQuery("before", before)
                .AddOptionalQuery("after", after)
        , "messages");

    /// <inheritdoc />
    public override Task<Message> GetMessageAsync(Guid channel, Guid messageId) =>
        GetResponseProperty<Message>(new RestRequest($"channels/{channel}/messages/{messageId}", Method.Get), "message");

    /// <inheritdoc />
    public override Task<Message> CreateMessageAsync(Guid channel, MessageContent message)
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
            throw new NullReferenceException("Message content cannot be null if there are no embeds");
        }

        return GetResponseProperty<Message>(
            new RestRequest($"channels/{channel}/messages", Method.Post).AddJsonBody(message),
            "message",
            // For slowmode handling
            channel
        );
    }

    /// <inheritdoc />
    public override Task<Message> UpdateMessageAsync(Guid channel, Guid message, MessageContent content) =>
        content is null
        ? throw new ArgumentNullException(nameof(content))
        : GetResponseProperty<Message>(new RestRequest($"channels/{channel}/messages/{message}", Method.Put).AddJsonBody(content), "message");

    /// <inheritdoc />
    public override Task DeleteMessageAsync(Guid channel, Guid message) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/messages/{message}", Method.Delete));

    /// <inheritdoc />
    public override Task AddReactionAsync(Guid channel, Guid message, uint emote) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/content/{message}/emotes/{emote}", Method.Put));

    /// <inheritdoc />
    public override Task RemoveReactionAsync(Guid channelId, Guid messageId, uint emoteId) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channelId}/content/{messageId}/emotes/{emoteId}", Method.Delete));
    #endregion

    #region Methods Forum channels
    /// <inheritdoc />
    public override Task<IList<TopicSummary>> GetTopicsAsync(Guid channel, uint? limit = null, DateTime? before = null) =>
        GetResponseProperty<IList<TopicSummary>>(
            new RestRequest($"channels/{channel}/topics", Method.Get)
                .AddOptionalQuery("limit", limit, encode: false)
                .AddOptionalQuery("before", before)
        , "forumTopics");

    /// <inheritdoc />
    public override Task<Topic> GetTopicAsync(Guid channel, uint topic) =>
        GetResponseProperty<Topic>(new RestRequest($"channels/{channel}/topics/{topic}", Method.Get), "forumTopic");

    /// <inheritdoc />
    public override Task<Topic> CreateTopicAsync(Guid channel, string title, string content) =>
        string.IsNullOrWhiteSpace(title)
        ? throw new ArgumentNullException(nameof(title))
        : string.IsNullOrWhiteSpace(content)
        ? throw new ArgumentNullException(nameof(content))
        : GetResponseProperty<Topic>(new RestRequest($"channels/{channel}/topics", Method.Post)
            .AddJsonBody(new
            {
                title,
                content
            })
        , "forumTopic");

    /// <inheritdoc />
    public override Task<Topic> UpdateTopicAsync(Guid channel, uint topic, string title, string content) =>
        string.IsNullOrWhiteSpace(title)
        ? throw new ArgumentNullException(nameof(title))
        : string.IsNullOrWhiteSpace(content)
        ? throw new ArgumentNullException(nameof(content))
        : GetResponseProperty<Topic>(new RestRequest($"channels/{channel}/topics/{topic}", Method.Patch)
            .AddJsonBody(new
            {
                title,
                content
            })
        , "forumTopic");

    /// <inheritdoc />
    public override Task DeleteTopicAsync(Guid channel, uint topic) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/topics/{topic}", Method.Delete));
    #endregion

    #region Methods List channels
    /// <inheritdoc />
    public override Task<IList<ListItemSummary>> GetListItemsAsync(Guid channel) =>
        GetResponseProperty<IList<ListItemSummary>>(new RestRequest($"channels/{channel}/items", Method.Get), "listItems");

    /// <inheritdoc />
    public override Task<ListItem> GetListItemAsync(Guid channel, Guid listItem) =>
        GetResponseProperty<ListItem>(new RestRequest($"channels/{channel}/items/{listItem}", Method.Get), "listItem");

    /// <inheritdoc />
    public override Task<ListItem> CreateListItemAsync(Guid channel, string message, string? note = null) =>
        string.IsNullOrWhiteSpace(message)
        ? throw new ArgumentNullException(nameof(message))
        : GetResponseProperty<ListItem>(new RestRequest($"channels/{channel}/items", Method.Post)
            .AddJsonBody(new
            {
                message,
                note = new
                {
                    content = note
                }
            })
        , "listItem");

    /// <inheritdoc />
    public override Task<ListItem> UpdateListItemAsync(Guid channel, Guid listItem, string message, string? note = null) =>
        string.IsNullOrWhiteSpace(message) && string.IsNullOrEmpty(note)
        ? throw new ArgumentNullException(nameof(message), "Either the message or the note of the list item's update must be specified")
        : GetResponseProperty<ListItem>(new RestRequest($"channels/{channel}/items/{listItem}", Method.Put)
            .AddJsonBody(new
            {
                message,
                note = new
                {
                    content = note
                }
            })
        , "listItem");

    /// <inheritdoc />
    public override Task DeleteListItemAsync(Guid channel, Guid listItem) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/items/{listItem}", Method.Delete));

    /// <inheritdoc />
    public override Task CompleteListItemAsync(Guid channel, Guid listItem) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/items/{listItem}/complete", Method.Post));

    /// <inheritdoc />
    public override Task UncompleteListItemAsync(Guid channel, Guid listItem) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/items/{listItem}/complete", Method.Delete));
    #endregion

    #region Methods Document channels
    /// <inheritdoc />
    public override Task<IList<Doc>> GetDocsAsync(Guid channel, uint? limit = null, DateTime? before = null) =>
        GetResponseProperty<IList<Doc>>(
            new RestRequest($"channels/{channel}/docs", Method.Get)
                .AddOptionalQuery("limit", limit, encode: false)
                .AddOptionalQuery("before", before)
        , "docs");

    /// <inheritdoc />
    public override Task<Doc> GetDocAsync(Guid channel, uint doc) =>
        GetResponseProperty<Doc>(new RestRequest($"channels/{channel}/docs/{doc}", Method.Get), "doc");

    /// <inheritdoc />
    public override Task<Doc> CreateDocAsync(Guid channel, string title, string content) =>
        string.IsNullOrWhiteSpace(title)
        ? throw new ArgumentNullException(nameof(title))
        : string.IsNullOrWhiteSpace(content)
        ? throw new ArgumentNullException(nameof(content))
        : GetResponseProperty<Doc>(new RestRequest($"channels/{channel}/docs", Method.Post)
            .AddJsonBody(new
            {
                title,
                content
            })
        , "doc");

    /// <inheritdoc />
    public override Task<Doc> UpdateDocAsync(Guid channel, uint doc, string title, string content) =>
        string.IsNullOrWhiteSpace(title)
        ? throw new ArgumentNullException(nameof(title))
        : string.IsNullOrWhiteSpace(content)
        ? throw new ArgumentNullException(nameof(content))
        : GetResponseProperty<Doc>(new RestRequest($"channels/{channel}/docs/{doc}", Method.Put)
            .AddJsonBody(new
            {
                title,
                content
            })
        , "doc");

    /// <inheritdoc />
    public override Task DeleteDocAsync(Guid channel, uint doc) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/docs/{doc}", Method.Delete));
    #endregion

    #region Methods Calendar channels

    #region Methods Calendar channels > Events
    /// <inheritdoc />
    public override Task<IList<CalendarEvent>> GetEventsAsync(Guid channel, uint? limit = null, DateTime? before = null) =>
        GetResponseProperty<IList<CalendarEvent>>(
            new RestRequest($"channels/{channel}/events", Method.Get)
                .AddOptionalQuery("limit", limit, encode: false)
                .AddOptionalQuery("before", before)
        , "calendarEvents");

    /// <inheritdoc />
    public override Task<CalendarEvent> GetEventAsync(Guid channel, uint calendarEvent) =>
        GetResponseProperty<CalendarEvent>(new RestRequest($"channels/{channel}/events/{calendarEvent}", Method.Get), "calendarEvent");

    /// <inheritdoc />
    public override Task<CalendarEvent> CreateEventAsync(Guid channel, string name, string? description = null, string? location = null, DateTime? startsAt = null, Uri? url = null, Color? color = null, uint? duration = null, uint? rsvpLimit = null, bool isPrivate = false) =>
        string.IsNullOrWhiteSpace(name)
        ? throw new ArgumentNullException(nameof(name))
        : GetResponseProperty<CalendarEvent>(new RestRequest($"channels/{channel}/events", Method.Post)
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
        , "calendarEvent");

    /// <inheritdoc />
    public override Task<CalendarEvent> UpdateEventAsync(Guid channel, uint calendarEvent, string? name = null, string? description = null, string? location = null, DateTime? startsAt = null, Uri? url = null, Color? color = null, uint? duration = null, bool? isPrivate = null) =>
        GetResponseProperty<CalendarEvent>(new RestRequest($"channels/{channel}/events/{calendarEvent}", Method.Patch)
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
        , "calendarEvent");

    /// <inheritdoc />
    public override Task DeleteEventAsync(Guid channel, uint calendarEvent) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/events/{calendarEvent}", Method.Delete));
    #endregion

    #region Methods Calendar channels > Rsvp
    /// <inheritdoc />
    public override Task<IList<CalendarRsvp>> GetRsvpsAsync(Guid channel, uint calendarEvent) =>
        GetResponseProperty<IList<CalendarRsvp>>(new RestRequest($"channels/{channel}/events/{calendarEvent}/rsvps", Method.Get), "calendarEventRsvps");

    /// <inheritdoc />
    public override Task<CalendarRsvp> GetRsvpAsync(Guid channel, uint calendarEvent, HashId user) =>
        GetResponseProperty<CalendarRsvp>(new RestRequest($"channels/{channel}/events/{calendarEvent}/rsvps/{user}", Method.Get), "calendarEventRsvp");

    /// <inheritdoc />
    public override Task<CalendarRsvp> SetRsvpAsync(Guid channel, uint calendarEvent, HashId user, CalendarRsvpStatus status) =>
        GetResponseProperty<CalendarRsvp>(new RestRequest($"channels/{channel}/events/{calendarEvent}/rsvps/{user}", Method.Put)
            .AddJsonBody(new
            {
                status
            })
        , "calendarEventRsvp");

    /// <inheritdoc />
    public override Task RemoveRsvpAsync(Guid channel, uint calendarEvent, HashId user) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/events/{calendarEvent}/rsvps/{user}", Method.Delete));
    #endregion

    #endregion

    #region Methods Content
    /// <inheritdoc />
    public override Task AddReactionAsync(Guid channel, uint content, uint emote) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/content/{content}/emotes/{emote}", Method.Put));

    /// <inheritdoc />
    public override Task RemoveReactionAsync(Guid channelId, uint contentId, uint emoteId) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channelId}/content/{contentId}/emotes/{emoteId}", Method.Delete));
    #endregion

    #endregion
}