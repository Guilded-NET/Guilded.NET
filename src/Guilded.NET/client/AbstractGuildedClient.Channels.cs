using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace Guilded.NET
{
    using Base;
    using Base.Embeds;
    using Base.Content;
    using Base.Permissions;
    public abstract partial class AbstractGuildedClient
    {
        private const int messageLimit = 4000;

        #region Webhook
        /// <inheritdoc/>
        private async Task CreateHookMessageAsync(Guid webhookId, string token, CreatableMessage message) =>
            await ExecuteRequest(new Uri(GuildedUrl.Media, $"webhooks/{webhookId}/{token}"), Method.POST, body: message).ConfigureAwait(false);
        /// <inheritdoc/>
        public override async Task CreateHookMessageAsync(Guid webhookId, string token, string content) =>
            await CreateHookMessageAsync(webhookId, token, new CreatableMessage { Content = content }).ConfigureAwait(false);
        /// <inheritdoc/>
        public override async Task CreateHookMessageAsync(Guid webhookId, string token, string content, IList<Embed> embeds) =>
            await CreateHookMessageAsync(webhookId, token, new CreatableMessage { Content = content, Embeds = embeds }).ConfigureAwait(false);
        /// <inheritdoc/>
        public override async Task CreateHookMessageAsync(Guid webhookId, string token, IList<Embed> embeds) =>
            await CreateHookMessageAsync(webhookId, token, new CreatableMessage { Embeds = embeds }).ConfigureAwait(false);
        #endregion

        #region Chat channel
        /// <inheritdoc/>
        public override async Task<IList<Message>> GetMessagesAsync(Guid channelId, bool includePrivate = false) =>
            await GetObject<IList<Message>>($"channels/{channelId}/messages?includePrivate={includePrivate}", Method.GET, key: "messages").ConfigureAwait(false);
        /// <inheritdoc/>
        public override async Task<Message> GetMessageAsync(Guid channelId, Guid messageId) =>
            await GetObject<Message>($"channels/{channelId}/messages/{messageId}", Method.GET, key: "message").ConfigureAwait(false);
        /// <summary>
        /// Creates a message in chat.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new chat messsage in the specified channel.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="message">The message to send</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for sending a message in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for sending a message in a thread</permission>
        /// <returns>Message created</returns>
        private async Task<Message> CreateMessageAsync(Guid channelId, CreatableMessage message) =>
            await GetObject<Message>($"channels/{channelId}/messages", Method.POST, "message", message).ConfigureAwait(false);
        /// <inheritdoc/>
        public override async Task<Message> CreateMessageAsync(Guid channelId, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentNullException(nameof(content));
            else if (content.Length > messageLimit)
                throw new ArgumentOutOfRangeException(nameof(content), content, $"{nameof(content)} exceeds the 4000 character message limit");
            else
                return await CreateMessageAsync(channelId, new CreatableMessage { Content = content }).ConfigureAwait(false);
        }
        /// <inheritdoc/>
        public override async Task<Message> CreateMessageAsync(Guid channelId, string content, params Guid[] replyMessageIds)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }
            else if (content.Length > messageLimit)
            {
                throw new ArgumentOutOfRangeException(nameof(content), content, $"{nameof(content)} exceeds the 4000 character message limit");
            }
            else
            {
                return await CreateMessageAsync(channelId, new CreatableMessage
                {
                    Content = content,
                    ReplyMessageIds = replyMessageIds
                }).ConfigureAwait(false);
            }
        }
        /// <inheritdoc/>
        public override async Task<Message> CreateMessageAsync(Guid channelId, string content, bool isPrivate, params Guid[] replyMessageIds)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }
            else if (content.Length > messageLimit)
            {
                throw new ArgumentOutOfRangeException(nameof(content), content, $"{nameof(content)} exceeds the 4000 character message limit");
            }
            else
            {
                return await CreateMessageAsync(channelId, new CreatableMessage
                {
                    Content = content,
                    IsPrivate = isPrivate,
                    ReplyMessageIds = replyMessageIds
                }).ConfigureAwait(false);
            }
        }
        /// <inheritdoc/>
        public override async Task<Message> UpdateMessageAsync(Guid channelId, Guid messageId, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }
            else if (content.Length > messageLimit)
            {
                throw new ArgumentOutOfRangeException(nameof(content), content, $"{nameof(content)} exceeds the 4000 character message limit");
            }
            else
            {
                return await CreateMessageAsync(channelId, new CreatableMessage
                {
                    Content = content
                }).ConfigureAwait(false);
            }
        }
        /// <inheritdoc/>
        public override async Task DeleteMessageAsync(Guid channelId, Guid messageId) =>
            await ExecuteRequest($"channels/{channelId}/messages/{messageId}", Method.DELETE).ConfigureAwait(false);
        /// <inheritdoc/>
        public override async Task<Reaction> AddReactionAsync(Guid channelId, Guid messageId, uint emoteId) =>
            await GetObject<Reaction>($"channels/{channelId}/content/{messageId}/emotes/{emoteId}", Method.PUT, key: "emote").ConfigureAwait(false);
        /// <inheritdoc/>
        public override async Task RemoveReactionAsync(Guid channelId, Guid messageId, uint emoteId) =>
            await ExecuteRequest($"channels/{channelId}/content/{messageId}/emotes/{emoteId}", Method.DELETE).ConfigureAwait(false);
        #endregion

        #region Forum channels
        /// <inheritdoc/>
        public override async Task<ForumThread> CreateForumThreadAsync(Guid channelId, string title, string content)
        {
            if(string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException(nameof(title));
            else if(string.IsNullOrWhiteSpace(content))
                throw new ArgumentNullException(nameof(content));

            return await GetObject<ForumThread>($"channels/{channelId}/forum", Method.POST, "forumThread", new
            {
                title,
                content
            }).ConfigureAwait(false);
        }
        #endregion

        #region List channels
        /// <inheritdoc/>
        public override async Task<ListItem> CreateListItemAsync(Guid channelId, string message, string note = null)
        {
            if(string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            return await GetObject<ListItem>($"channels/{channelId}/list", Method.POST, "listItem", new
            {
                message,
                note
            }).ConfigureAwait(false);
        }
        #endregion

        #region Content
        /// <inheritdoc/>
        public override async Task<Reaction> AddReactionAsync(Guid channelId, uint contentId, uint emoteId) =>
            await GetObject<Reaction>($"channels/{channelId}/content/{contentId}/emotes/{emoteId}", Method.PUT, key: "emote").ConfigureAwait(false);
        /// <inheritdoc/>
        public override async Task RemoveReactionAsync(Guid channelId, uint contentId, uint emoteId) =>
            await ExecuteRequest($"channels/{channelId}/content/{contentId}/emotes/{emoteId}", Method.DELETE).ConfigureAwait(false);
        #endregion
    }
}