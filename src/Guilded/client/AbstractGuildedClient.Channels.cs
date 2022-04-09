using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Guilded.Base;
using Guilded.Base.Content;

using RestSharp;

namespace Guilded;

public abstract partial class AbstractGuildedClient
{
    #region Webhook
    /// <inheritdoc/>
    public override async Task CreateHookMessageAsync(Guid webhookId, string token, MessageContent message) =>
        await ExecuteRequestAsync(new RestRequest(new Uri(GuildedUrl.Media, $"webhooks/{webhookId}/{token}"), Method.Post).AddJsonBody(message)).ConfigureAwait(false);
    #endregion

    #region Chat channel
    /// <inheritdoc/>
    public override async Task<IList<Message>> GetMessagesAsync(Guid channelId, bool includePrivate = false, uint? limit = null, DateTime? before = null, DateTime? after = null) =>
        await GetResponseProperty<IList<Message>>(
            new RestRequest($"channels/{channelId}/messages", Method.Get)
                .AddQueryParameter("includePrivate", includePrivate, encode: false)
                .AddOptionalQuery("limit", limit, encode: false)
                .AddOptionalQuery("before", before)
                .AddOptionalQuery("after", after)
        , "messages").ConfigureAwait(false);
    /// <inheritdoc/>
    public override async Task<Message> GetMessageAsync(Guid channelId, Guid messageId) =>
        await GetResponseProperty<Message>(new RestRequest($"channels/{channelId}/messages/{messageId}", Method.Get), "message").ConfigureAwait(false);
    /// <inheritdoc/>
    public override async Task<Message> CreateMessageAsync(Guid channelId, MessageContent message)
    {
        if (string.IsNullOrWhiteSpace(message?.Content))
        {
            throw new ArgumentNullException(nameof(message.Content));
        }
        else if (message.Content.Length > Message.ContentLimit)
        {
            throw new ArgumentOutOfRangeException(nameof(message.Content), message.Content, $"{nameof(message.Content)} exceeds the 4000 character message limit");
        }
        return await GetResponseProperty<Message>(new RestRequest($"channels/{channelId}/messages", Method.Post).AddJsonBody(message), "message").ConfigureAwait(false);
    }
    /// <inheritdoc/>
    public override async Task<Message> UpdateMessageAsync(Guid channelId, Guid messageId, string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentNullException(nameof(content));
        else if (content.Length > Message.ContentLimit)
            throw new ArgumentOutOfRangeException(nameof(content), content, $"{nameof(content)} exceeds the 4000 character message limit");
        else
            return await GetResponseProperty<Message>(new RestRequest($"channels/{channelId}/messages/{messageId}", Method.Put).AddJsonBody(new MessageContent(content)), "message").ConfigureAwait(false);
    }
    /// <inheritdoc/>
    public override async Task DeleteMessageAsync(Guid channelId, Guid messageId) =>
        await ExecuteRequestAsync(new RestRequest($"channels/{channelId}/messages/{messageId}", Method.Delete)).ConfigureAwait(false);
    /// <inheritdoc/>
    public override async Task<Reaction> AddReactionAsync(Guid channelId, Guid messageId, uint emoteId) =>
        await GetResponseProperty<Reaction>(new RestRequest($"channels/{channelId}/content/{messageId}/emotes/{emoteId}", Method.Put), "emote").ConfigureAwait(false);
    /// <inheritdoc/>
    public override async Task RemoveReactionAsync(Guid channelId, Guid messageId, uint emoteId) =>
        await ExecuteRequestAsync(new RestRequest($"channels/{channelId}/content/{messageId}/emotes/{emoteId}", Method.Delete)).ConfigureAwait(false);
    #endregion

    #region Forum channels
    /// <inheritdoc/>
    public override async Task<ForumThread> CreateForumThreadAsync(Guid channelId, string title, string content)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title));
        else if (string.IsNullOrWhiteSpace(content)) throw new ArgumentNullException(nameof(content));

        return await GetResponseProperty<ForumThread>(new RestRequest($"channels/{channelId}/forum", Method.Post)
            .AddJsonBody(new
            {
                title,
                content
            })
        , "forumThread").ConfigureAwait(false);
    }
    #endregion

    #region List channels
    /// <inheritdoc/>
    public override async Task<IList<ListItem<ListItemNoteSummary>>> GetListItemsAsync(Guid channelId) =>
        await GetResponseProperty<IList<ListItem<ListItemNoteSummary>>>(new RestRequest($"channels/{channelId}/items", Method.Get), "listItems").ConfigureAwait(false);
    /// <inheritdoc/>
    public override async Task<ListItem<ListItemNote>> GetListItemAsync(Guid channelId, Guid listItemId) =>
        await GetResponseProperty<ListItem<ListItemNote>>(new RestRequest($"channels/{channelId}/items/{listItemId}", Method.Get), "listItem").ConfigureAwait(false);
    /// <inheritdoc/>
    public override async Task<ListItem<ListItemNote>> CreateListItemAsync(Guid channelId, string message, string? note = null)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentNullException(nameof(message));

        return await GetResponseProperty<ListItem<ListItemNote>>(new RestRequest($"channels/{channelId}/items", Method.Post)
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
    /// <inheritdoc/>
    public override async Task<ListItem<ListItemNote>> UpdateListItemAsync(Guid channelId, Guid listItemId, string? message = null, string? note = null)
    {
        if (string.IsNullOrWhiteSpace(message) && string.IsNullOrEmpty(note))
            throw new ArgumentNullException(nameof(message), "Either the message or the note of the list item's update must be specified");

        return await GetResponseProperty<ListItem<ListItemNote>>(new RestRequest($"channels/{channelId}/items/{listItemId}", Method.Put)
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
    /// <inheritdoc/>
    public override async Task DeleteListItemAsync(Guid channelId, Guid listItemId) =>
        await ExecuteRequestAsync(new RestRequest($"channels/{channelId}/items/{listItemId}", Method.Delete)).ConfigureAwait(false);
    #endregion

    #region Document channels
    /// <inheritdoc/>
    public override async Task<IList<Doc>> GetDocsAsync(Guid channelId, uint? limit = null, DateTime? before = null) =>
        await GetResponseProperty<IList<Doc>>(
            new RestRequest($"channels/{channelId}/docs", Method.Get)
                .AddOptionalQuery("limit", limit, encode: false)
                .AddOptionalQuery("before", before)
        , "docs").ConfigureAwait(false);
    /// <inheritdoc/>
    public override async Task<Doc> GetDocAsync(Guid channelId, uint docId) =>
        await GetResponseProperty<Doc>(new RestRequest($"channels/{channelId}/docs/{docId}", Method.Get), "doc").ConfigureAwait(false);
    /// <inheritdoc/>
    public override async Task<Doc> CreateDocAsync(Guid channelId, string title, string content)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title));
        else if (string.IsNullOrWhiteSpace(content)) throw new ArgumentNullException(nameof(content));

        return await GetResponseProperty<Doc>(new RestRequest($"channels/{channelId}/docs", Method.Post)
            .AddJsonBody(new
            {
                title,
                content
            })
        , "doc").ConfigureAwait(false);
    }
    /// <inheritdoc/>
    public override async Task<Doc> UpdateDocAsync(Guid channelId, uint docId, string title, string content)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title));
        else if (string.IsNullOrWhiteSpace(content)) throw new ArgumentNullException(nameof(content));

        return await GetResponseProperty<Doc>(new RestRequest($"channels/{channelId}/docs/{docId}", Method.Put)
            .AddJsonBody(new
            {
                title,
                content
            })
        , "doc").ConfigureAwait(false);
    }
    /// <inheritdoc/>
    public override async Task DeleteDocAsync(Guid channelId, uint docId) =>
        await ExecuteRequestAsync(new RestRequest($"channels/{channelId}/docs/{docId}", Method.Delete)).ConfigureAwait(false);
    #endregion

    #region Content
    /// <inheritdoc/>
    public override async Task<Reaction> AddReactionAsync(Guid channelId, uint contentId, uint emoteId) =>
        await GetResponseProperty<Reaction>(new RestRequest($"channels/{channelId}/content/{contentId}/emotes/{emoteId}", Method.Put), "emote").ConfigureAwait(false);
    /// <inheritdoc/>
    public override async Task RemoveReactionAsync(Guid channelId, uint contentId, uint emoteId) =>
        await ExecuteRequestAsync(new RestRequest($"channels/{channelId}/content/{contentId}/emotes/{emoteId}", Method.Delete)).ConfigureAwait(false);
    #endregion
}