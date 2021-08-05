using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;

namespace Guilded.NET
{
    using Base;
    using Base.Chat;
    using Base.Teams;
    using Base.Embeds;
    using Base.Content;
    /// <summary>
    /// Logged-in user in Guilded.
    /// </summary>
    public abstract partial class BasicGuildedClient
    {
        #region Webhook
        /// <summary>
        /// Creates a webhook in a given channel.
        /// </summary>
        /// <param name="channelId">The identifier of the channel to create webhook in</param>
        /// <param name="name">The name of the webhook</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Created webhook</returns>
        public override async Task<Webhook> CreateWebhookAsync(Guid channelId, string name) =>
            await GetObject<Webhook>("webhooks", Method.POST, body: new
            {
                channelId,
                name
            });
        /// <summary>
        /// Updates webhook's name or profile picture.
        /// </summary>
        /// <param name="channelId">The identifier of the channel where the webhook is</param>
        /// <param name="webhookId">The identifier of the webhook to update</param>
        /// <param name="name">The new name of the webhook</param>
        /// <param name="avatar">The new profile picture/icon of the webhook</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Updated webhook</returns>
        public override async Task<Webhook> UpdateWebhookAsync(Guid channelId, Guid webhookId, string name = null, Uri avatar = null)
        {
            // If both arguments are null
            if (name is null && avatar is null) throw new ArgumentException($"Both {nameof(name)} and {nameof(avatar)} cannot be null");
            // Creates a dictionary for the Webhook update
            Dictionary<string, object> change = new Dictionary<string, object>() {
                {"channelId", channelId}
            };
            if (!(name is null)) change.Add("name", name);
            if (!(avatar is null)) change.Add("iconUrl", avatar);
            // Sets a new name, avatar or both
            return await GetObject<Webhook>($"webhooks/{webhookId}", Method.PUT, body: change);
        }
        /// <summary>
        /// Deletes a given webhook.
        /// </summary>
        /// <param name="webhookId">The identifier of the webhook to delete</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Deleted webhook</returns>
        public override async Task<Webhook> DeleteWebhookAsync(Guid webhookId) =>
            await GetObject<Webhook>($"webhooks/{webhookId}", Method.DELETE);
        /// <summary>
        /// Posts a message using a webhook.
        /// </summary>
        /// <param name="webhookId">The identifier of the webhook</param>
        /// <param name="token">The token of this webhook</param>
        /// <param name="content">The message to send using the webhook</param>
        /// <param name="embeds">An array of embeds to send</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        public override async Task ExecuteWebhookAsync(Guid webhookId, string token, string content = null, params Embed[] embeds) =>
            await ExecuteRequest(new Uri(GuildedUrl.Media, $"webhooks/{webhookId}/{token}"), Method.POST, body: new
            {
                content,
                embeds
            });
        #endregion

        #region Chat channel
        /// <summary>
        /// Gets messages with a specific limit.
        /// </summary>
        /// <param name="channelId">Channel to get messages in</param>
        /// <param name="limit">How many messages it should get</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>List of messages</returns>
        public override async Task<IList<Message>> GetMessagesAsync(Guid channelId, uint limit = 50) =>
            // TODO: Add limit query
            await GetObject<IList<Message>>($"channels/{channelId}/messages", Method.GET, key: "messages");
        /// <summary>
        /// Gets a message in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel where that message is</param>
        /// <param name="messageId">ID of message it should get</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Message</returns>
        public override async Task<Message> GetMessageAsync(Guid channelId, Guid messageId) =>
            await GetObject<Message>($"channels/{channelId}/messages/{messageId}", Method.GET, key: "message");
        /// <summary>
        /// Sends a message into the chat.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="content">The contents of the message in rich text markup</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Message created</returns>
        public override async Task<Message> SendMessageAsync(Guid channelId, MessageContent content) =>
            await GetObject<Message>($"channels/{channelId}/messages", Method.POST, "message", new { content });
        /// <summary>
        /// Sends a message into the chat.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Message created</returns>
        public override async Task<Message> SendMessageAsync(Guid channelId, string content) =>
            await GetObject<Message>($"channels/{channelId}/messages", Method.POST, "message", new { content });
        /// <summary>
        /// Edits message of the bot posted in the chat.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="messageId">ID of the message to edit</param>
        /// <param name="content">The new content of the message in rich text markup</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Message edited</returns>
        public override async Task<Message> EditMessageAsync(Guid channelId, Guid messageId, MessageContent content) =>
            await GetObject<Message>($"channels/{channelId}/messages/{messageId}", Method.PUT, "message", new { content });
        /// <summary>
        /// Edits message of the bot posted in the chat.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="messageId">ID of the message to edit</param>
        /// <param name="content">The new content of the message in Markdown plain text</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Message edited</returns>
        public override async Task<Message> EditMessageAsync(Guid channelId, Guid messageId, string content) =>
            await GetObject<Message>($"channels/{channelId}/messages/{messageId}", Method.PUT, "message", new { content });
        /// <summary>
        /// Deletes a message posted in the chat.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="messageId">ID of the message to delete</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        public override async Task DeleteMessageAsync(Guid channelId, Guid messageId) =>
            await ExecuteRequest($"channels/{channelId}/messages/{messageId}", Method.DELETE);
        /// <summary>
        /// Add a reaction to a specific message.
        /// </summary>
        /// <param name="channelId">ID of the channel where the message is in</param>
        /// <param name="messageId">ID of the message to add a reaction on</param>
        /// <param name="emoteId">ID of the emote to add</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Reaction added</returns>
        public override async Task<Reaction> AddReactionAsync(Guid channelId, Guid messageId, uint emoteId) =>
            await GetObject<Reaction>($"channels/{channelId}/content/{messageId}/emotes/{emoteId}", Method.PUT, key: "emote");
        /// <summary>
        /// Removes a reaction from a specific message.
        /// </summary>
        /// <param name="channelId">ID of the channel where the message is in</param>
        /// <param name="messageId">ID of the message to remove a reaction from</param>
        /// <param name="emoteId">ID of the emote to remove</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        public override async Task RemoveReactionAsync(Guid channelId, Guid messageId, uint emoteId) =>
            await ExecuteRequest($"channels/{channelId}/content/{messageId}/emotes/{emoteId}", Method.DELETE);
        /// <summary>
        /// Starts typing in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel where the bot should type</param>
        public void StartTyping(Guid channelId)
        {
            // Makes sure that the main/default WebSocket exists
            if (!Websockets.ContainsKey(""))
                throw new KeyNotFoundException("Could not find default WebSocket");
            // Sends typing data to the WebSocket
            Websockets[""].Send($"42[\"ChatChannelTyping\",{{\"channelId\":\"{channelId}\"}}]");
        }
        #endregion

        #region Forum channels
        /// <summary>
        /// Creates a forum post in a forum channel.
        /// </summary>
        /// <param name="channelId">The identifier of the channel to post in</param>
        /// <param name="title">The title of the forum post</param>
        /// <param name="content">The content of the forum post</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Forum post created</returns>
        public override async Task<ForumThread> CreateForumPostAsync(Guid channelId, string title, string content) =>
            await GetObject<ForumThread>($"channels/{channelId}/forum", Method.POST, "forumThread", new
            {
                title,
                content
            });
        /// <summary>
        /// Creates a forum post in a forum channel.
        /// </summary>
        /// <param name="channelId">The identifier of the channel to post in</param>
        /// <param name="title">The title of the forum post</param>
        /// <param name="content">The content of the forum post</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>Forum post created</returns>
        public override async Task<ForumThread> CreateForumPostAsync(Guid channelId, string title, MessageContent content) =>
            await GetObject<ForumThread>($"channels/{channelId}/forum", Method.POST, "forumThread", new
            {
                title,
                content
            });
        #endregion

        #region List channels
        /// <summary>
        /// Creates a new list item in a list channel.
        /// </summary>
        /// <param name="channelId">The identifier of the channel to create in</param>
        /// <param name="message">The title content of this list item</param>
        /// <param name="note">The note of this list item</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>List item created</returns>
        public override async Task<ListItem> CreateListItemAsync(Guid channelId, string message, string note = null) =>
            await GetObject<ListItem>($"channels/{channelId}/list", Method.POST, "listItem", new
            {
                message,
                note
            });
        /// <summary>
        /// Creates a new list item in a list channel.
        /// </summary>
        /// <param name="channelId">The identifier of the channel to create in</param>
        /// <param name="message">The title content of this list item</param>
        /// <param name="note">The note of this list item</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>List item created</returns>
        public override async Task<ListItem> CreateListItemAsync(Guid channelId, MessageContent message, MessageContent note = null) =>
            await GetObject<ListItem>($"channels/{channelId}/list", Method.POST, "listItem", new
            {
                message,
                note
            });
        #endregion
    }
}