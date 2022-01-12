using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Guilded.NET.Base;
using Guilded.NET.Base.Content;
using Guilded.NET.Base.Embeds;
using Guilded.NET.Base.Permissions;
using RestSharp;

namespace Guilded.NET
{
    public abstract partial class AbstractGuildedClient
    {
        #region Webhook
        /// <inheritdoc/>
        private async Task CreateHookMessageAsync(Guid webhookId, string token, MessageContent message) =>
            await ExecuteRequestAsync(new RestRequest(new Uri(GuildedUrl.Media, $"webhooks/{webhookId}/{token}"), Method.POST).AddJsonBody(message)).ConfigureAwait(false);
        /// <inheritdoc/>
        public override async Task CreateHookMessageAsync(Guid webhookId, string token, string content) =>
            await CreateHookMessageAsync(webhookId, token, new MessageContent(content)).ConfigureAwait(false);
        /// <inheritdoc/>
        public override async Task CreateHookMessageAsync(Guid webhookId, string token, string content, IList<Embed> embeds) =>
            await CreateHookMessageAsync(webhookId, token, new MessageContent(content) { Embeds = embeds }).ConfigureAwait(false);
        /// <inheritdoc/>
        public override async Task CreateHookMessageAsync(Guid webhookId, string token, IList<Embed> embeds) =>
            await CreateHookMessageAsync(webhookId, token, new MessageContent { Embeds = embeds }).ConfigureAwait(false);
        #endregion

        #region Chat channel
        /// <inheritdoc/>
        public override async Task<IList<Message>> GetMessagesAsync(Guid channelId, bool includePrivate = false) =>
            await GetObject<IList<Message>>(new RestRequest($"channels/{channelId}/messages?includePrivate={includePrivate}", Method.GET), "messages").ConfigureAwait(false);
        /// <inheritdoc/>
        public override async Task<Message> GetMessageAsync(Guid channelId, Guid messageId) =>
            await GetObject<Message>(new RestRequest($"channels/{channelId}/messages/{messageId}", Method.GET), "message").ConfigureAwait(false);
        /// <inheritdoc/>
        public override async Task<Message> CreateMessageAsync(Guid channelId, MessageContent message)
        {
            if (string.IsNullOrWhiteSpace(message?.Content))
            {
                throw new ArgumentNullException(nameof(message.Content));
            }
            else if (message.Content.Length > MessageLimit)
            {
                throw new ArgumentOutOfRangeException(nameof(message.Content), message.Content, $"{nameof(message.Content)} exceeds the 4000 character message limit");
            }
            return await GetObject<Message>(new RestRequest($"channels/{channelId}/messages", Method.POST).AddJsonBody(message), "message").ConfigureAwait(false);
        }
        /// <inheritdoc/>
        public override async Task<Message> UpdateMessageAsync(Guid channelId, Guid messageId, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentNullException(nameof(content));
            else if (content.Length > MessageLimit)
                throw new ArgumentOutOfRangeException(nameof(content), content, $"{nameof(content)} exceeds the 4000 character message limit");
            else
                return await GetObject<Message>(new RestRequest($"channels/{channelId}/messages/{messageId}", Method.PUT).AddJsonBody(new MessageContent(content)), "message").ConfigureAwait(false);
        }
        /// <inheritdoc/>
        public override async Task DeleteMessageAsync(Guid channelId, Guid messageId) =>
            await ExecuteRequestAsync(new RestRequest($"channels/{channelId}/messages/{messageId}", Method.DELETE)).ConfigureAwait(false);
        /// <inheritdoc/>
        public override async Task<Reaction> AddReactionAsync(Guid channelId, Guid messageId, uint emoteId) =>
            await GetObject<Reaction>(new RestRequest($"channels/{channelId}/content/{messageId}/emotes/{emoteId}", Method.PUT), "emote").ConfigureAwait(false);
        /// <inheritdoc/>
        public override async Task RemoveReactionAsync(Guid channelId, Guid messageId, uint emoteId) =>
            await ExecuteRequestAsync(new RestRequest($"channels/{channelId}/content/{messageId}/emotes/{emoteId}", Method.DELETE)).ConfigureAwait(false);
        #endregion

        #region Forum channels
        /// <inheritdoc/>
        public override async Task<ForumThread> CreateForumThreadAsync(Guid channelId, string title, string content)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title));
            else if (string.IsNullOrWhiteSpace(content)) throw new ArgumentNullException(nameof(content));

            return await GetObject<ForumThread>(new RestRequest($"channels/{channelId}/forum", Method.POST)
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
        public override async Task<ListItem> CreateListItemAsync(Guid channelId, string message, string? note = null)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            return await GetObject<ListItem>(new RestRequest($"channels/{channelId}/list", Method.POST)
                .AddJsonBody(new
                {
                    message,
                    note
                })
            , "listItem").ConfigureAwait(false);
        }
        #endregion

        #region Document channels
        /// <inheritdoc/>
        public override async Task<IList<Doc>> GetDocsAsync(Guid channelId) =>
            await GetObject<IList<Doc>>(new RestRequest($"channels/{channelId}/docs", Method.GET), "docs").ConfigureAwait(false);
        /// <inheritdoc/>
        public override async Task<Doc> GetDocAsync(Guid channelId, uint docId) =>
            await GetObject<Doc>(new RestRequest($"channels/{channelId}/docs/{docId}", Method.GET), "doc").ConfigureAwait(false);
        /// <inheritdoc/>
        public override async Task<Doc> CreatedDocAsync(Guid channelId, string title, string content)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title));
            else if (string.IsNullOrWhiteSpace(content)) throw new ArgumentNullException(nameof(content));

            return await GetObject<Doc>(new RestRequest($"channels/{channelId}/docs", Method.POST)
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

            return await GetObject<Doc>(new RestRequest($"channels/{channelId}/docs/{docId}", Method.PUT)
                .AddJsonBody(new
                {
                    title,
                    content
                })
            , "doc").ConfigureAwait(false);
        }
        /// <inheritdoc/>
        public override async Task DeleteDocAsync(Guid channelId, uint docId) =>
            await ExecuteRequestAsync(new RestRequest($"channels/{channelId}/docs/{docId}", Method.DELETE)).ConfigureAwait(false);
        #endregion

        #region Content
        /// <inheritdoc/>
        public override async Task<Reaction> AddReactionAsync(Guid channelId, uint contentId, uint emoteId) =>
            await GetObject<Reaction>(new RestRequest($"channels/{channelId}/content/{contentId}/emotes/{emoteId}", Method.PUT), "emote").ConfigureAwait(false);
        /// <inheritdoc/>
        public override async Task RemoveReactionAsync(Guid channelId, uint contentId, uint emoteId) =>
            await ExecuteRequestAsync(new RestRequest($"channels/{channelId}/content/{contentId}/emotes/{emoteId}", Method.DELETE)).ConfigureAwait(false);
        #endregion
    }
}