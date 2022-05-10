using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Guilded.Base;
using Guilded.Base.Content;

using RestSharp;

namespace Guilded;

public abstract partial class AbstractGuildedClient
{
    #region Webhook
    /// <inheritdoc />
    public override async Task CreateHookMessageAsync(Guid webhook, string token, MessageContent message) =>
        await ExecuteRequestAsync(new RestRequest(new Uri(GuildedUrl.Media, $"webhooks/{webhook}/{token}"), Method.Post).AddJsonBody(message)).ConfigureAwait(false);
    #endregion

    #region Chat channel
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

        return await GetResponseProperty<Message>(new RestRequest($"channels/{channel}/messages", Method.Post).AddJsonBody(message), "message").ConfigureAwait(false);
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
    public override async Task<Reaction> AddReactionAsync(Guid channel, Guid message, uint emote) =>
        await GetResponseProperty<Reaction>(new RestRequest($"channels/{channel}/content/{message}/emotes/{emote}", Method.Put), "emote").ConfigureAwait(false);
    // /// <inheritdoc />
    // public override async Task RemoveReactionAsync(Guid channelId, Guid messageId, uint emoteId) =>
    //     await ExecuteRequestAsync(new RestRequest($"channels/{channelId}/content/{messageId}/emotes/{emoteId}", Method.Delete)).ConfigureAwait(false);
    #endregion

    #region Forum channels
    /// <inheritdoc />
    public override async Task<ForumThread> CreateForumThreadAsync(Guid channel, string title, string content)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title));
        else if (string.IsNullOrWhiteSpace(content)) throw new ArgumentNullException(nameof(content));

        return await GetResponseProperty<ForumThread>(new RestRequest($"channels/{channel}/forum", Method.Post)
            .AddJsonBody(new
            {
                title,
                content
            })
        , "forumThread").ConfigureAwait(false);
    }
    #endregion

    #region List channels
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
    public override async Task<Doc> GetDocAsync(Guid channelId, uint docId) =>
        await GetResponseProperty<Doc>(new RestRequest($"channels/{channelId}/docs/{docId}", Method.Get), "doc").ConfigureAwait(false);
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

    #region Content
    /// <inheritdoc />
    public override async Task<Reaction> AddReactionAsync(Guid channel, uint content, uint emote) =>
        await GetResponseProperty<Reaction>(new RestRequest($"channels/{channel}/content/{content}/emotes/{emote}", Method.Put), "emote").ConfigureAwait(false);
    // /// <inheritdoc />
    // public override async Task RemoveReactionAsync(Guid channelId, uint contentId, uint emoteId) =>
    //     await ExecuteRequestAsync(new RestRequest($"channels/{channelId}/content/{contentId}/emotes/{emoteId}", Method.Delete)).ConfigureAwait(false);
    #endregion
}