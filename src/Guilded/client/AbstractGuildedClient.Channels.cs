using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Base.Content;
using Guilded.Base.Embeds;
using RestSharp;

namespace Guilded
{
    public abstract partial class AbstractGuildedClient
    {
        #region Webhook
        /// <inheritdoc/>
        private async Task CreateHookMessageAsync(Guid webhookId, string token, MessageContent message) =>
            await ExecuteRequestAsync(new RestRequest(new Uri(GuildedUrl.Media, $"webhooks/{webhookId}/{token}"), Method.Post).AddJsonBody(message)).ConfigureAwait(false);
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
            await GetResponseProperty<IList<Message>>(new RestRequest($"channels/{channelId}/messages?includePrivate={includePrivate}", Method.Get), "messages").ConfigureAwait(false);
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
        public override async Task<ListItem> CreateListItemAsync(Guid channelId, string message, string? note = null)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            return await GetResponseProperty<ListItem>(new RestRequest($"channels/{channelId}/list", Method.Post)
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
            await GetResponseProperty<IList<Doc>>(new RestRequest($"channels/{channelId}/docs", Method.Get), "docs").ConfigureAwait(false);
        /// <inheritdoc/>
        public override async Task<Doc> GetDocAsync(Guid channelId, uint docId) =>
            await GetResponseProperty<Doc>(new RestRequest($"channels/{channelId}/docs/{docId}", Method.Get), "doc").ConfigureAwait(false);
        /// <inheritdoc/>
        public override async Task<Doc> CreatedDocAsync(Guid channelId, string title, string content)
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
}